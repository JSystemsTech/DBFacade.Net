using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DbConnectionConfigBase : IDbConnectionConfig, IAsyncInit
    {
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        public async Task InitAsync()
        {
            await Task.CompletedTask;
        }
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mockResponseData">The mock response data.</param>
        /// <returns></returns>
        internal abstract IDbConnection GetDbConnection(object parameters, MockResponseData mockResponseData = null);
        /// <summary>
        /// Gets the database connection asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mockResponseData">The mock response data.</param>
        /// <returns></returns>
        internal abstract Task<IDbConnection> GetDbConnectionAsync(object parameters, MockResponseData mockResponseData = null);
        /// <summary>
        /// Executes the database action.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="commandConfig">The command configuration.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mockResponseData">The mock response data.</param>
        /// <returns></returns>
        internal abstract IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, MockResponseData mockResponseData = null)
            where TDbDataModel : DbDataModel;

        /// <summary>
        /// Executes the database action asynchronous.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="commandConfig">The command configuration.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mockResponseData">The mock response data.</param>
        /// <returns></returns>
        internal abstract Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, MockResponseData mockResponseData = null)
            where TDbDataModel : DbDataModel;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataReader">The type of the database data reader.</typeparam>
    /// <typeparam name="TDbConnection">The type of the database connection.</typeparam>
    /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
    /// <typeparam name="TDbTransaction">The type of the database transaction.</typeparam>
    /// <typeparam name="TDbParameter">The type of the database parameter.</typeparam>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class DbConnectionConfig<TDbDataReader, TDbConnection, TDbCommand, TDbTransaction, TDbParameter,TDbConnectionConfig>: DbConnectionConfigBase
        where TDbDataReader : DbDataReader
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbTransaction : DbTransaction
        where TDbParameter : DbParameter
        where TDbConnectionConfig : DbConnectionConfigBase

    {

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mockResponseData">The mock response data.</param>
        /// <returns></returns>
        internal override sealed IDbConnection GetDbConnection(object parameters, MockResponseData mockResponseData = null) => ResolveDbConnection(mockResponseData);
        /// <summary>
        /// Gets the database connection asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mockResponseData">The mock response data.</param>
        /// <returns></returns>
        internal override sealed async Task<IDbConnection> GetDbConnectionAsync(object parameters, MockResponseData mockResponseData = null) => await ResolveDbConnectionAsync(mockResponseData);
        internal override sealed IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig,TDbParams parameters, MockResponseData mockResponseData = null)
        {
            try
            {
                if (commandConfig.DbConnectionConfig == this)
                {
                    commandConfig.OnBefore(parameters);
                    return mockResponseData != null ?
                  DbConnectionHandler<MockDbConnection, MockDbCommand, MockDbParameter, MockDbTransaction, DbDataReader>
                      .ExecuteDbAction(commandConfig, parameters, mockResponseData) :
                  DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader>
                      .ExecuteDbAction(commandConfig, parameters, mockResponseData);
                }
                else
                {
                    throw new FacadeException($"'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'");
                }
            }
            catch (ValidationException<TDbParams> valEx)
            {
                OnValidationError(valEx);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText.CommandId, valEx);
            }
            catch (SQLExecutionException SQLEx)
            {
                OnSQLExecutionError(SQLEx);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText.CommandId, SQLEx);
            }
            catch (FacadeException fEx)
            {
                OnFacadeError(fEx);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText.CommandId, fEx);
            }
            catch (Exception ex)
            {
                OnError(ex);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText.CommandId, ex);
            }
        }       

        internal override sealed async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, MockResponseData mockResponseData = null)
        {
            try
            {
                if (commandConfig.DbConnectionConfig == this)
                {
                    await commandConfig.OnBeforeAsync(parameters);
                    return mockResponseData != null ?
                         await DbConnectionHandler<MockDbConnection, MockDbCommand, MockDbParameter, MockDbTransaction, DbDataReader>
                              .ExecuteDbActionAsync(commandConfig, parameters, mockResponseData) :
                         await DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader>
                              .ExecuteDbActionAsync(commandConfig, parameters, mockResponseData);
                }
                else
                {
                    throw new FacadeException($"'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'");
                }
            }
            catch (ValidationException<TDbParams> valEx)
            {
                await OnValidationErrorAsync(valEx);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText.CommandId, valEx);
            }
            catch (SQLExecutionException SQLEx)
            {
                await OnSQLExecutionErrorAsync(SQLEx);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText.CommandId, SQLEx);
            }
            catch (FacadeException fEx)
            {
                await OnFacadeErrorAsync(fEx);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText.CommandId, fEx);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText.CommandId, ex);
            }
        }



        /// <summary>
        /// Resolves the database connection.
        /// </summary>
        /// <param name="mockResponseData">The mock response data.</param>
        /// <returns></returns>
        private IDbConnection ResolveDbConnection(MockResponseData mockResponseData = null)
        {
            IDbConnection resolvedDbConnection = mockResponseData != null ? new MockDbConnection(mockResponseData) :
            DbProviderFactories.GetFactory(GetDbConnectionProvider()).CreateConnection() is TDbConnection dbConnection ? dbConnection : default(IDbConnection);

            if (resolvedDbConnection != default(IDbConnection))
            {
                resolvedDbConnection.ConnectionString = GetDbConnectionString();
                return resolvedDbConnection;
            }

            return null;
        }
        /// <summary>
        /// Resolves the database connection asynchronous.
        /// </summary>
        /// <param name="mockResponseData">The mock response data.</param>
        /// <returns></returns>
        private async Task<IDbConnection> ResolveDbConnectionAsync(MockResponseData mockResponseData = null)
        {
            IDbConnection resolvedDbConnection = mockResponseData != null ? new MockDbConnection(mockResponseData) :
                DbProviderFactories.GetFactory(await GetDbConnectionProviderAsync()).CreateConnection() is TDbConnection dbConnection ? dbConnection : 
                default(IDbConnection);
            
            if (resolvedDbConnection != default(IDbConnection))
            {
                resolvedDbConnection.ConnectionString = await GetDbConnectionStringAsync();
                return resolvedDbConnection;
            }
            await Task.CompletedTask;
            return null;
        }

        protected virtual string GetDbConnectionString()
        {
            throw new NotImplementedException("You must impliment the method GetDbConnectionString");
        }
        protected virtual string GetDbConnectionProvider()
        {
            throw new NotImplementedException("You must impliment the method GetDbConnectionProvider");
        }
        protected virtual async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException("You must impliment the method GetDbConnectionStringAsync");
        }
        protected virtual async Task<string> GetDbConnectionProviderAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException("You must impliment the method GetDbConnectionProviderAsync");
        }
        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected virtual void OnError(Exception ex) { }
        /// <summary>
        /// Called when [validation error].
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="ex">The ex.</param>
        protected virtual void OnValidationError<TDbParams>(ValidationException<TDbParams> ex) => OnError(ex);
        /// <summary>
        /// Called when [SQL execution error].
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected virtual void OnSQLExecutionError(SQLExecutionException ex) => OnError(ex);
        /// <summary>
        /// Called when [facade error].
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected virtual void OnFacadeError(FacadeException ex) => OnError(ex);

        /// <summary>
        /// Called when [error asynchronous].
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected virtual async Task OnErrorAsync(Exception ex) { await Task.CompletedTask;  }
        /// <summary>
        /// Called when [validation error asynchronous].
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="ex">The ex.</param>
        protected virtual async Task OnValidationErrorAsync<TDbParams>(ValidationException<TDbParams> ex) => await OnErrorAsync(ex);
        /// <summary>
        /// Called when [SQL execution error asynchronous].
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected virtual async Task OnSQLExecutionErrorAsync(SQLExecutionException ex) => await OnErrorAsync(ex);
        /// <summary>
        /// Called when [facade error asynchronous].
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected virtual async Task OnFacadeErrorAsync(FacadeException ex) => await OnErrorAsync(ex);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        SqlConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SqlDataReader, SqlConnection, SqlCommand,
            SqlTransaction, SqlParameter, TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        SqLiteConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SQLiteDataReader, SQLiteConnection,
            SQLiteCommand, SQLiteTransaction, SQLiteParameter, TDbConnectionConfig>
        where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OleDbConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OleDbDataReader, OleDbConnection, OleDbCommand,
            OleDbTransaction, OleDbParameter, TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OdbcConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OdbcDataReader, OdbcConnection, OdbcCommand,
            OdbcTransaction, OdbcParameter, TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OracleConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OracleDataReader, OracleConnection,
            OracleCommand, OracleTransaction, OracleParameter, TDbConnectionConfig>
        where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        DefaultConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<DbDataReader, DbConnection, DbCommand,
            DbTransaction, DbParameter, TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
    }
}