using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace DbFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnection">The type of the database connection.</typeparam>
    /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
    /// <typeparam name="TDbParameter">The type of the database parameter.</typeparam>
    /// <typeparam name="TDbTransaction">The type of the database transaction.</typeparam>
    /// <typeparam name="TDbDataReader">The type of the database data reader.</typeparam>
    internal partial class DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader>
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
        where TDbTransaction : DbTransaction
        where TDbDataReader : DbDataReader
    {
        /// <summary>
        /// Builds the repsonse.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="dbDataReader">The database data reader.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        private static IDbResponse<TDbDataModel> BuildRepsonse<TDbDataModel>(
            Guid commandId,
            int returnValue,
            DbDataReader dbDataReader = null,
            IDictionary<string, object> outputValues = null)
            where TDbDataModel : DbDataModel
        {
            var responseObj = DbResponseFactory<TDbDataModel>.Create(commandId, returnValue, outputValues);
            if(responseObj is List<TDbDataModel> _responseObj && dbDataReader != null)
            {
                while (dbDataReader.Read())
                {
                    ConcurrentDictionary<string, object> dataRow = new ConcurrentDictionary<string, object>();
                    foreach(int ordinal in Enumerable.Range(0, dbDataReader.FieldCount))
                    {
                        dataRow.TryAdd(dbDataReader.GetName(ordinal), dbDataReader.GetValue(ordinal));
                    }
                    _responseObj.Add(DbDataModel.ToDbDataModel<TDbDataModel>(commandId,dataRow));
                }
                    
                dbDataReader.Close();
            }
            return responseObj;
        }

        public static IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(
            DbCommandMethod<TDbParams, TDbDataModel> config, TDbParams parameters, MockResponseData mockResponseData = null)
            where TDbDataModel : DbDataModel
        {
            using (TDbConnection dbConnection = config.DbConnectionConfig.GetDbConnection(parameters, mockResponseData) as TDbConnection)
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
        }
    }
}