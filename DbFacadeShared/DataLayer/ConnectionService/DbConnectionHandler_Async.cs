﻿using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
        /// Builds the repsonse asynchronous.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="dbDataReader">The database data reader.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        private static async Task<IDbResponse<TDbDataModel>> BuildRepsonseAsync<TDbDataModel>(
            Guid commandId,
            DbDataReader dbDataReader = null,
            IDictionary<string, object> outputValues = null)
            where TDbDataModel : DbDataModel
        {
            var responseObj = await DbResponseFactory<TDbDataModel>.CreateAsync(commandId, default(int), outputValues);
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
                    IDbResponse<TDbDataModel> response;
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
                                        response = await BuildRepsonseAsync<TDbDataModel>(
                                            config.DbCommandText.CommandId,
                                            null,
                                            await dbCommand.GetOutputValuesAsync());
                                        if (response is DbResponse<TDbDataModel> transactionResp)
                                        {
                                            transactionResp.ReturnValue = await dbCommand.GetReturnValueAsync();
                                        }
                                        return response;
                                    }
                                    else
                                    {
                                        throw new FacadeException("Invalid Transaction Definition");
                                    }                                    
                                }

                            using (var dbDataReader = await dbCommand.ExecuteReaderAsync() as TDbDataReader)
                            {
                                response = await BuildRepsonseAsync<TDbDataModel>(
                                    config.DbCommandText.CommandId,
                                    dbDataReader,
                                    await dbCommand.GetOutputValuesAsync());
                            }
                            if (response is DbResponse<TDbDataModel> resp)
                            {
                                resp.ReturnValue = await dbCommand.GetReturnValueAsync();
                            }
                            return response;
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