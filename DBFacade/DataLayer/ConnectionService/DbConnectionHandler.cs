using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Exceptions;

namespace DBFacade.DataLayer.ConnectionService
{
    internal partial class DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader,
        TDbMethodManifest>
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
        where TDbTransaction : DbTransaction
        where TDbDataReader : DbDataReader
        where TDbMethodManifest : DbMethodManifest
    {
        private static IDbResponse<TDbDataModel> BuildRepsonse<TDbMethodManifestMethod, TDbDataModel>(int returnValue,
            DbDataReader dbDataReader = null,
            IDictionary<string, object> outputValues = null)
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var responseObj = new DbResponse<TDbMethodManifestMethod, TDbDataModel>(returnValue, outputValues);
            if (dbDataReader != null)
            {
                while (dbDataReader.Read())
                    responseObj.Add(DbDataModel.ToDbDataModel<TDbDataModel, TDbMethodManifestMethod>(dbDataReader));
                dbDataReader.Close();
            }

            return responseObj;
        }

        public static IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            if (method.Config is IDbCommandConfigInternal config)
            {
                using (var dbConnection = config.DbConnectionConfig.GetDbConnection(parameters) as TDbConnection)
                {
                    if (dbConnection != null)
                    {
                        dbConnection.Open();
                        using (var dbCommand =
                            config.GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(parameters, dbConnection))
                        {
                            try
                            {
                                if (config.IsTransaction)
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
                                            return BuildRepsonse<TDbMethodManifestMethod, TDbDataModel>(
                                                config.GetReturnValue(dbCommand), null, 
                                                config.GetOutputValues(dbCommand));
                                        }

                                        throw new FacadeException("Invalid Transaction Definition");
                                    }

                                using (var dbDataReader = dbCommand.ExecuteReader() as TDbDataReader)
                                {
                                    return BuildRepsonse<TDbMethodManifestMethod, TDbDataModel>(
                                        config.GetReturnValue(dbCommand),
                                        dbDataReader,
                                        config.GetOutputValues(dbCommand));
                                }
                            }
                            catch (SqlException sqlEx)
                            {
                                throw new SQLExecutionException("A SQL Error has occurred", config.DbCommandText,
                                    sqlEx);
                            }
                            catch (DataModelConstructionException)
                            {
                                throw;
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

            throw new FacadeException("Invalid Config");
        }
    }
}