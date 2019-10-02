using System;
using System.Data.Common;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.CommandConfig;
using System.Data.SqlClient;
using DBFacade.Exceptions;

namespace DBFacade.DataLayer.ConnectionService
{
    internal partial class DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader, TDbMethodManifest>        
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
        where TDbTransaction : DbTransaction
        where TDbDataReader : DbDataReader
        where TDbMethodManifest : DbMethodManifest
    {
        private static IDbResponse<TDbDataModel> BuildResonse<TDbMethodManifestMethod, TDbDataModel>(object returnValue, DbDataReader dbDataReader = null)
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            DbResponse<TDbMethodManifestMethod, TDbDataModel> responseObj = new DbResponse<TDbMethodManifestMethod, TDbDataModel>(returnValue);
            if (dbDataReader != null)
            {
                while (dbDataReader.Read())
                {
                    responseObj.Add(DbDataModel.ToDbDataModel<TDbDataModel, TDbMethodManifestMethod>(dbDataReader));
                }
                dbDataReader.Close();
            }
            return responseObj;
        }
        public static IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            IDbCommandConfigInternal config = method.Config as IDbCommandConfigInternal;
            IInternalDbParamsModel parametersModel = parameters as IInternalDbParamsModel;
            if (parametersModel.RunMode == MethodRunMode.Test)
            {
                using(DbDataReader dbDataReader = parametersModel.ResponseData)
                {
                    return BuildResonse<TDbMethodManifestMethod, TDbDataModel>(parametersModel.ReturnValue, dbDataReader);
                }                
            }
            else
            {
                using (TDbConnection dbConnection = config.DbConnectionConfig.DbConnection as TDbConnection)
                {
                    dbConnection.Open();
                    using (TDbCommand dbCommand = config.GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(parametersModel, dbConnection))
                    {
                        try
                        {
                            if (config.IsTransaction)
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
                                    return BuildResonse<TDbMethodManifestMethod, TDbDataModel>(config.GetReturnValue(dbCommand));
                                }                                
                            }
                            else
                            {
                                using (TDbDataReader dbDataReader = dbCommand.ExecuteReader() as TDbDataReader) {
                                    return BuildResonse<TDbMethodManifestMethod, TDbDataModel>(config.GetReturnValue(dbCommand), dbDataReader);
                                }                                
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            throw new SQLExecutionException("A SQL Error has occurred", config.DbCommandText, sqlEx);
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
