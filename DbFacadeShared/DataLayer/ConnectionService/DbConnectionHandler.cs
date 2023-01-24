using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using DbFacadeShared.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DbFacade.DataLayer.ConnectionService
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal partial class DbConnectionHandler<TDbDataModel, TDbParams>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Builds the repsonse.
        /// </summary>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="ds">The ds.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private static IDbResponse<TDbDataModel> BuildRepsonse(
            bool rawDataOnly,
            IDbCommandSettings dbCommandSettings,
            DataSet ds = null,
            IDictionary<string, object> outputValues = null)
        {
            var responseObj = DbResponseFactory<TDbDataModel>.Create(dbCommandSettings, default(int), outputValues);
            if (responseObj is DbResponse<TDbDataModel> _responseObj && ds != null)
            {
                _responseObj.InitDataSets(DbDataSet.CreateDataSets(dbCommandSettings, ds), ds, rawDataOnly);
            }
            return responseObj;
        }
        /// <summary>
        /// Executes the transaction.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        /// <exception cref="SQLExecutionException">Unable to create transaction object for type {typeof(TDbTransaction).Name}
        /// or
        /// or
        /// Unknown Error</exception>
        /// <exception cref="FacadeException">Invalid Transaction Definition</exception>
        private static IDbResponse<TDbDataModel> ExecuteTransaction(DbCommandMethod<TDbParams, TDbDataModel> config, IDbConnection dbConnection, IDbCommand dbCommand)
        {
            IDbTransaction transaction;
            try
            {
                transaction = dbConnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new SQLExecutionException($"Unable to create transaction", config.DbCommandText, ex);
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
                    IDbResponse<TDbDataModel> response = BuildRepsonse(
                        true,
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
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <param name="createDbDataAdapter">The create database data adapter.</param>
        /// <returns></returns>
        /// <exception cref="SQLExecutionException">Unknown Error</exception>
        private static IDbResponse<TDbDataModel> ExecuteQuery(DbCommandMethod<TDbParams, TDbDataModel> config, IDbCommand dbCommand, bool rawDataOnly, Func<IDbDataAdapter> createDbDataAdapter)
        {
            IDbResponse<TDbDataModel> response;
            try
            {
                DataSet ds = new DataSet($"{config.DbCommandText.Label} Result DataSet");
                IDbDataAdapter da = createDbDataAdapter();
                da.SelectCommand = dbCommand;
                da.Fill(ds);

                response = BuildRepsonse(
                    rawDataOnly,
                config.DbCommandText,
                    ds,
                    dbCommand.GetOutputValues());
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
        /// <summary>
        /// Executes the database action.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <param name="createDbDataAdapter">The create database data adapter.</param>
        /// <returns></returns>
        /// <exception cref="SQLExecutionException">Unable to open Connection</exception>
        /// <exception cref="FacadeException">Invalid Connection Definition</exception>
        public static IDbResponse<TDbDataModel> ExecuteDbAction(
            DbCommandMethod<TDbParams, TDbDataModel> config, TDbParams parameters, bool rawDataOnly, Func<IDbDataAdapter> createDbDataAdapter)
        {
            using (IDbConnection dbConnection = config.DbConnectionConfig.GetDbConnection(config.DbCommandText))
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
                        dbConnection.GetDbCommand(config.DbCommandText, config.DbParams, parameters))
                    {
                        return config.DbCommandText.IsTransaction ?
                            ExecuteTransaction(config, dbConnection, dbCommand) :
                            ExecuteQuery(config, dbCommand, rawDataOnly, createDbDataAdapter);
                    }
                }

                throw new FacadeException("Invalid Connection Definition");
            }
        }
    }
}