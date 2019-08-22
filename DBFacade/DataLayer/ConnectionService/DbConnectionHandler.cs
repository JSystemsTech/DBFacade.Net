using System;
using System.Data.Common;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.CommandConfig;
using System.Data.SqlClient;
using DBFacade.Exceptions;

namespace DBFacade.DataLayer.ConnectionService
{
    internal partial class DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader, TDbManifest>        
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
        where TDbTransaction : DbTransaction
        where TDbDataReader : DbDataReader
        where TDbManifest : DbManifest
    {
        private static IDbResponse<TDbDataModel> BuildResonse<TDbManifestMethod, TDbDataModel>(object returnValue, DbDataReader dbDataReader = null)
            where TDbDataModel : DbDataModel
            where TDbManifestMethod : TDbManifest
        {
            DbResponse<TDbManifestMethod, TDbDataModel> responseObj = new DbResponse<TDbManifestMethod, TDbDataModel>(returnValue);
            if (dbDataReader != null)
            {
                while (dbDataReader.Read())
                {
                    responseObj.Add(DbDataModel.ToDbDataModel<TDbDataModel, TDbManifestMethod>(dbDataReader));
                }
                dbDataReader.Close();
            }
            return responseObj;
        }
        public static IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            IDbCommandConfig config = method.GetConfig();
            if (parameters._GetRunMode() == MethodRunMode.Test)
            {
                using(DbDataReader dbDataReader = parameters._GetResponseData())
                {
                    return BuildResonse<TDbManifestMethod, TDbDataModel>(parameters._GetReturnValue(), dbDataReader);
                }                
            }
            else
            {
                using (TDbConnection dbConnection = config.GetDBConnectionConfig().GetDbConnection() as TDbConnection)
                {
                    dbConnection.Open();
                    using (TDbCommand dbCommand = config.GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(parameters, dbConnection))
                    {
                        try
                        {
                            if (config.IsTransaction())
                            {
                                using (TDbTransaction transaction = dbConnection.BeginTransaction() as TDbTransaction)
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
                                    return BuildResonse<TDbManifestMethod, TDbDataModel>(config.GetReturnValue(dbCommand));
                                }                                
                            }
                            else
                            {
                                using (TDbDataReader dbDataReader = dbCommand.ExecuteReader() as TDbDataReader) {
                                    return BuildResonse<TDbManifestMethod, TDbDataModel>(config.GetReturnValue(dbCommand), dbDataReader);
                                }                                
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            throw new SQLExecutionException("A SQL Error has occurred", config.GetDbCommandText(), sqlEx);
                        }
                        catch (Exception Ex)
                        {
                            throw new FacadeException("Unknown Error", Ex);
                        }
                    }                    
                }
            }
        }

    }
}
