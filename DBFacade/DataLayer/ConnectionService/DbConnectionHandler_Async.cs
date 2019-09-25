using System;
using System.Data.Common;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.CommandConfig;
using System.Data.SqlClient;
using DBFacade.Exceptions;
using System.Threading.Tasks;

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
        private async static Task<IDbResponse<TDbDataModel>> BuildResonseAsync<TDbManifestMethod, TDbDataModel>(object returnValue, DbDataReader dbDataReader = null)
            where TDbDataModel : DbDataModel
            where TDbManifestMethod : TDbManifest
        {
            DbResponse<TDbManifestMethod, TDbDataModel> responseObj = new DbResponse<TDbManifestMethod, TDbDataModel>(returnValue);
            if (dbDataReader != null)
            {
                while (await dbDataReader.ReadAsync())
                {
                    responseObj.Add(DbDataModel.ToDbDataModel<TDbDataModel, TDbManifestMethod>(dbDataReader));
                }
                dbDataReader.Close();
            }
            return responseObj;
        }
        public static async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            IDbCommandConfigInternal config = await method.GetConfigAsync() as IDbCommandConfigInternal;
            IInternalDbParamsModel parametersModel = parameters as IInternalDbParamsModel;
            if (parametersModel.RunMode == MethodRunMode.Test)
            {
                return await BuildResonseAsync<TDbManifestMethod, TDbDataModel>(parametersModel.ReturnValue, parametersModel.ResponseData);
            }
            else
            {
                IDbConnectionConfigInternal connectionConfig = await config.GetDbConnectionConfigAsync();
                using (TDbConnection dbConnection = connectionConfig.DbConnection as TDbConnection)
                {
                    await dbConnection.OpenAsync();
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
                                        await dbCommand.ExecuteNonQueryAsync();
                                    }
                                    catch
                                    {
                                        transaction.Rollback();
                                        throw;
                                    }
                                    transaction.Commit();
                                    return await BuildResonseAsync<TDbManifestMethod, TDbDataModel>(config.GetReturnValue(dbCommand));
                                }                                
                            }
                            else
                            {
                                using (TDbDataReader dbDataReader = await dbCommand.ExecuteReaderAsync() as TDbDataReader) {
                                    return await BuildResonseAsync<TDbManifestMethod, TDbDataModel>(config.GetReturnValue(dbCommand), dbDataReader);
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
