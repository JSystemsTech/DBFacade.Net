using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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
        private static async Task<IDbResponse<TDbDataModel>> BuildRepsonseAsync<TDbDataModel>(
            Guid commandId,
            int returnValue, 
            DbDataReader dbDataReader = null,
            IDictionary<string, object> outputValues = null)
            where TDbDataModel : DbDataModel
        {
            var responseObj = await DbResponseFactory<TDbDataModel>.CreateAsync(commandId, returnValue, outputValues);
            if (responseObj is List<TDbDataModel> _responseObj && dbDataReader != null)
            {
                while (await dbDataReader.ReadAsync())
                {
                    ConcurrentDictionary<string, object> dataRow = new ConcurrentDictionary<string, object>();
                    foreach (int ordinal in Enumerable.Range(0, dbDataReader.FieldCount))
                    {
                        dataRow.TryAdd(dbDataReader.GetName(ordinal), dbDataReader.GetValue(ordinal));
                    }
                    _responseObj.Add(await DbDataModel.ToDbDataModelAsync<TDbDataModel>(commandId, dataRow));
                }                    
                dbDataReader.Close();
            }
            return responseObj;
        }

        public static async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> config, TDbParams parameters, MockResponseData mockResponseData)
            where TDbDataModel : DbDataModel
        {
            var connectionConfig = config.DbConnectionConfig;
            using (var dbConnection = await connectionConfig.GetDbConnectionAsync(parameters, mockResponseData) as TDbConnection)
            {
                if (dbConnection != null)
                {
                    await dbConnection.OpenAsync();
                    using (var dbCommand =
                        await dbConnection.GetDbCommandAsync<TDbConnection, TDbCommand, TDbParameter, TDbParams>(config.DbCommandText, config.DbParams, parameters))
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
                                            await dbCommand.ExecuteNonQueryAsync();
                                        }
                                        catch
                                        {
                                            transaction.Rollback();
                                            throw;
                                        }

                                        transaction.Commit();
                                        return await BuildRepsonseAsync<TDbDataModel>(
                                            config.DbCommandText.CommandId,
                                            await dbCommand.GetReturnValueAsync(),
                                            null,
                                            await dbCommand.GetOutputValuesAsync());
                                    }

                                    throw new FacadeException("Invalid Transaction Definition");
                                }

                            using (var dbDataReader = await dbCommand.ExecuteReaderAsync() as TDbDataReader)
                            {
                                return await BuildRepsonseAsync<TDbDataModel>(
                                    config.DbCommandText.CommandId,
                                    await dbCommand.GetReturnValueAsync(),
                                    dbDataReader,
                                    await dbCommand.GetOutputValuesAsync());
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