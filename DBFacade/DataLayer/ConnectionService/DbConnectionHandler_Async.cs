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

    internal partial class DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader, TDbMethodManifest>        
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
        where TDbTransaction : DbTransaction
        where TDbDataReader : DbDataReader
        where TDbMethodManifest : DbMethodManifest
    {
        private async static Task<IDbResponse<TDbDataModel>> BuildResonseAsync<TDbMethodManifestMethod, TDbDataModel>(object returnValue, DbDataReader dbDataReader = null)
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            DbResponse<TDbMethodManifestMethod, TDbDataModel> responseObj = new DbResponse<TDbMethodManifestMethod, TDbDataModel>(returnValue);
            if (dbDataReader != null)
            {
                while (await dbDataReader.ReadAsync())
                {
                    responseObj.Add(DbDataModel.ToDbDataModel<TDbDataModel, TDbMethodManifestMethod>(dbDataReader));
                }
                dbDataReader.Close();
            }
            return responseObj;
        }
        public static async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            IDbCommandConfigInternal config = await method.GetConfigAsync() as IDbCommandConfigInternal;
            IInternalDbParamsModel parametersModel = parameters as IInternalDbParamsModel;
            if (parametersModel.RunMode == MethodRunMode.Test)
            {
                return await BuildResonseAsync<TDbMethodManifestMethod, TDbDataModel>(parametersModel.ReturnValue, parametersModel.ResponseData);
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
                                    return await BuildResonseAsync<TDbMethodManifestMethod, TDbDataModel>(config.GetReturnValue(dbCommand));
                                }                                
                            }
                            else
                            {
                                using (TDbDataReader dbDataReader = await dbCommand.ExecuteReaderAsync() as TDbDataReader) {
                                    return await BuildResonseAsync<TDbMethodManifestMethod, TDbDataModel>(config.GetReturnValue(dbCommand), dbDataReader);
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
