using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;

namespace DbFacade.DataLayer.ConnectionService
{
    internal partial class DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader>
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
        where TDbTransaction : DbTransaction
        where TDbDataReader : DbDataReader
    {
        private static IDbResponse<TDbDataModel> BuildRepsonse<TDbDataModel>(
            Guid commandId,
            int returnValue,
            DbDataReader dbDataReader = null,
            IDictionary<string, object> outputValues = null)
            where TDbDataModel : DbDataModel
        {
            var responseObj = DbResponseFactory<TDbDataModel>.Create(returnValue, outputValues);
            if(responseObj is List<TDbDataModel> _responseObj && dbDataReader != null)
            {
                while (dbDataReader.Read())
                    _responseObj.Add(DbDataModel.ToDbDataModel<TDbDataModel>(
                        commandId,
                        Enumerable.Range(0, dbDataReader.FieldCount).ToDictionary(dbDataReader.GetName, dbDataReader.GetValue)
                        ));
                dbDataReader.Close();
            }
            return responseObj;
        }

        public static IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(
            DbCommandMethod<TDbParams, TDbDataModel> config, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : DbParamsModel
        {
            using (TDbConnection dbConnection = config.DbConnectionConfig.GetDbConnection(parameters) as TDbConnection)
            {
                if (dbConnection != null)
                {
                    dbConnection.Open();
                    using (var dbCommand =
                        dbConnection.GetDbCommand<TDbConnection, TDbCommand, TDbParameter, TDbParams>(config.DbCommandText, config.DbParams, parameters))
                    {
                        try
                        {
                            if (config.DbCommandText.IsTransaction)
                                using (var transaction = dbConnection.BeginTransaction() as TDbTransaction)
                                {
                                    if (transaction != null)
                                    {
                                        try
                                        {
                                            dbCommand.Transaction = transaction;
                                            dbCommand.ExecuteNonQuery();
                                        }
                                        catch
                                        {
                                            transaction.Rollback();
                                            throw;
                                        }

                                        transaction.Commit();
                                        return BuildRepsonse<TDbDataModel>(
                                            config.DbCommandText.CommandId,
                                            dbCommand.GetReturnValue(), null,
                                            dbCommand.GetOutputValues());
                                    }

                                    throw new FacadeException("Invalid Transaction Definition");
                                }

                            using (var dbDataReader = dbCommand.ExecuteReader() as TDbDataReader)
                            {
                                return BuildRepsonse<TDbDataModel>(
                                    config.DbCommandText.CommandId,
                                    dbCommand.GetReturnValue(),
                                    dbDataReader,
                                    dbCommand.GetOutputValues());
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            throw new SQLExecutionException("A SQL Error has occurred", config.DbCommandText,
                                sqlEx);
                        }
                        catch (Exception ex)
                        {
                            throw new FacadeException("Unknown Error", ex);
                        }
                    }
                }

                throw new FacadeException("Invalid Connection Definition");
            }

            throw new FacadeException("Invalid Config");
        }
    }
}