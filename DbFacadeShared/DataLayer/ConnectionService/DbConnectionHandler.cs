using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using DbFacadeShared.DataLayer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
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
        private static IDbDataSet GetNextDataSet(IDbCommandSettings dbCommandSettings, DbDataReader dbDataReader)
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
            return dataSet;
        }
        private static IEnumerable<IDbDataSet> GetDataSets(IDbCommandSettings dbCommandSettings, DbDataReader dbDataReader)
        {
            List<IDbDataSet> dataSets = new List<IDbDataSet>();
            dataSets.Add(GetNextDataSet(dbCommandSettings,dbDataReader)); //Add first set of Data
            while (dbDataReader.NextResult()) //Add remaining sets of Data is they exist
            {
                dataSets.Add(GetNextDataSet(dbCommandSettings,dbDataReader)); //Add first set of Data
            }
            return dataSets;
        }
        /// <summary>
        /// Builds the repsonse.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="dbDataReader">The database data reader.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        private static IDbResponse<TDbDataModel> BuildRepsonse<TDbDataModel>(
            IDbCommandSettings dbCommandSettings,
            DbDataReader dbDataReader = null,
            IDictionary<string, object> outputValues = null)
            where TDbDataModel : DbDataModel
        {
            var responseObj = DbResponseFactory<TDbDataModel>.Create(dbCommandSettings, default(int), outputValues);
            if(responseObj is DbResponse<TDbDataModel> _responseObj && dbDataReader != null)
            {
                //ignore setting data when data type is the abstract base class
                
                if (typeof(TDbDataModel) != typeof(DbDataModel))
                {
                    _responseObj.InitDataSets(GetDataSets(dbCommandSettings,dbDataReader));
                } 
                dbDataReader.Close();
            }
            return responseObj;
        }
        private static IDbResponse<TDbDataModel> ExecuteTransaction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> config, TDbConnection dbConnection, TDbCommand dbCommand)
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
                        dbCommand.ExecuteNonQuery();
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
                    IDbResponse<TDbDataModel> response = BuildRepsonse<TDbDataModel>(
                        config.DbCommandText,
                        null,
                        dbCommand.GetOutputValues());
                    if (response is DbResponse<TDbDataModel> transactionResp)
                    {
                        transactionResp.ReturnValue = dbCommand.GetReturnValue();
                    }

                    return response;
                }
                else
                {
                    throw new FacadeException("Invalid Transaction Definition");
                }


            }
        }
        private static IDbResponse<TDbDataModel> ExecuteQuery<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> config, TDbConnection dbConnection, TDbCommand dbCommand)
        where TDbDataModel : DbDataModel
        {
            IDbResponse<TDbDataModel> response;
            try
            {
                using (var dbDataReader = dbCommand.ExecuteReader() as TDbDataReader)
                {
                    response = BuildRepsonse<TDbDataModel>(
                        config.DbCommandText,
                        dbDataReader,
                        dbCommand.GetOutputValues());
                }
                if (response is DbResponse<TDbDataModel> resp)
                {
                    resp.ReturnValue = dbCommand.GetReturnValue();
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
        public static IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(
            DbCommandMethod<TDbParams, TDbDataModel> config, TDbParams parameters)
            where TDbDataModel : DbDataModel
        {
            using (TDbConnection dbConnection = config.DbConnectionConfig.GetDbConnection(config.DbCommandText) as TDbConnection)
            {
                if (dbConnection != null)
                {
                    try
                    {
                        dbConnection.Open();
                    }
                    catch (Exception ex)
                    {
                        throw new SQLExecutionException("Unable to open Connection", config.DbCommandText, ex);
                    }
                    using (var dbCommand =
                        dbConnection.GetDbCommand<TDbConnection, TDbCommand, TDbParameter, TDbParams>(config.DbCommandText, config.DbParams, parameters))
                    {
                        return config.DbCommandText.IsTransaction ?
                            ExecuteTransaction(config, dbConnection, dbCommand) :
                            ExecuteQuery(config, dbConnection, dbCommand);                        
                    }
                }

                throw new FacadeException("Invalid Connection Definition");
            }
        }
    }
}