using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using DbFacadeShared.DataLayer.Models;
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
        private static async Task<IDbDataSet> GetNextDataSetAsync(IDbCommandSettings dbCommandSettings, DbDataReader dbDataReader)
        {
            DbDataSet dataSet = DbDataSet.Create(dbCommandSettings);
            if (dbDataReader.HasRows)
            {
                while (dbDataReader.Read())
                {
                    ConcurrentDictionary<string, object> dataRow = new ConcurrentDictionary<string, object>();
                    foreach (int ordinal in Enumerable.Range(0, dbDataReader.FieldCount))
                    {
                        dataRow.TryAdd(dbDataReader.GetName(ordinal), dbDataReader.GetValue(ordinal));
                    }
                    dataSet.Add(dataRow);
                }
            }
            await Task.CompletedTask;
            return dataSet;
        }
        private static async Task<IEnumerable<IDbDataSet>> GetDataSetsAsync(IDbCommandSettings dbCommandSettings, DbDataReader dbDataReader)
        {
            List<IDbDataSet> dataSets = new List<IDbDataSet>();
            dataSets.Add(GetNextDataSet(dbCommandSettings,dbDataReader)); //Add first set of Data
            while (dbDataReader.NextResult()) //Add remaining sets of Data is they exist
            {
                dataSets.Add(await GetNextDataSetAsync(dbCommandSettings,dbDataReader)); //Add first set of Data
            }
            return dataSets;
        }
        /// <summary>
        /// Builds the repsonse asynchronous.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="dbDataReader">The database data reader.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        private static async Task<IDbResponse<TDbDataModel>> BuildRepsonseAsync<TDbDataModel>(
            IDbCommandSettings dbCommandSettings,
            DbDataReader dbDataReader = null,
            IDictionary<string, object> outputValues = null)
            where TDbDataModel : DbDataModel
        {
            var responseObj = await DbResponseFactory<TDbDataModel>.CreateAsync(dbCommandSettings, default(int), outputValues);
            if (responseObj is DbResponse<TDbDataModel> _responseObj && dbDataReader != null)
            {
                //ignore setting data when data type is the abstract base class
                if (typeof(TDbDataModel) != typeof(DbDataModel))
                {
                    _responseObj.InitDataSets(await GetDataSetsAsync(dbCommandSettings, dbDataReader));
                }                                        
                dbDataReader.Close();
            }
            return responseObj;
        }
        /// <summary>Executes the transaction asynchronous.</summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="dbCommand">The database command.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DbFacade.Exceptions.SQLExecutionException">Unable to create transaction object for type {typeof(TDbTransaction).Name}
        /// or
        /// or
        /// Unknown Error</exception>
        /// <exception cref="DbFacade.Exceptions.FacadeException">Invalid Transaction Definition</exception>
        private static async Task<IDbResponse<TDbDataModel>> ExecuteTransactionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> config, TDbConnection dbConnection, TDbCommand dbCommand)
        where TDbDataModel : DbDataModel
        {
            TDbTransaction transaction;
            try
            {
                transaction = dbConnection.BeginTransaction() as TDbTransaction;
            }
            catch (Exception ex)
            {
                throw new SQLExecutionException($"Unable to create transaction object for type {typeof(TDbTransaction).Name}", config.DbCommandText, ex);
            }
            using (transaction)
            {
                if (transaction != null)
                {
                    try
                    {
                        dbCommand.Transaction = transaction;
                        await dbCommand.ExecuteNonQueryAsync();
                    }
                    catch (SqlException sqlEx)
                    {
                        transaction.Rollback();
                        throw new SQLExecutionException(config.DbCommandText, sqlEx);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new SQLExecutionException("Unknown Error", config.DbCommandText, ex);
                    }

                    transaction.Commit();
                    IDbResponse<TDbDataModel>  response = await BuildRepsonseAsync<TDbDataModel>(
                        config.DbCommandText,
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
        }
        /// <summary>Executes the query asynchronous.</summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="dbCommand">The database command.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="DbFacade.Exceptions.SQLExecutionException">Unknown Error</exception>
        private static async Task<IDbResponse<TDbDataModel>> ExecuteQueryAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> config, TDbConnection dbConnection, TDbCommand dbCommand)
        where TDbDataModel : DbDataModel
        {
            try
            {
                IDbResponse<TDbDataModel> response;
                using (var dbDataReader = await dbCommand.ExecuteReaderAsync() as TDbDataReader)
                {
                    response = await BuildRepsonseAsync<TDbDataModel>(
                        config.DbCommandText,
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
                throw new SQLExecutionException(config.DbCommandText, sqlEx);
            }
            catch (Exception ex)
            {
                throw new SQLExecutionException("Unknown Error", config.DbCommandText, ex);
            }
        }
        public static async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> config, TDbParams parameters)
            where TDbDataModel : DbDataModel
        {
            var connectionConfig = config.DbConnectionConfig;
            using (var dbConnection = await connectionConfig.GetDbConnectionAsync(config.DbCommandText) as TDbConnection)
            {
                if (dbConnection != null)
                {
                    try
                    {
                        await dbConnection.OpenAsync();
                    }catch (Exception ex)
                    {
                        throw new SQLExecutionException("Unable to open Connection", config.DbCommandText, ex);
                    }
                    
                    using (var dbCommand =
                        await dbConnection.GetDbCommandAsync<TDbConnection, TDbCommand, TDbParameter, TDbParams>(config.DbCommandText, config.DbParams, parameters))
                    {
                        return config.DbCommandText.IsTransaction ?
                           await  ExecuteTransactionAsync(config, dbConnection, dbCommand) :
                            await ExecuteQueryAsync(config, dbConnection, dbCommand);
                    }
                }

                throw new FacadeException("Invalid Connection Definition");
            }
        }
    }
}