using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Factories;
using DbFacade.Utils;
using DbFacadeShared.DataLayer.ConnectionService.MockDb;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        internal abstract IDbConnection GetDbConnection(IDbCommandSettings dbCommandSettings);
        /// <summary>
        /// Gets the database connection asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        internal abstract Task<IDbConnection> GetDbConnectionAsync(IDbCommandSettings dbCommandSettings);

        /// <summary>Gets the database data adapter.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        protected abstract IDbDataAdapter GetDbDataAdapter();
        /// <summary>Creates the database connection.</summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        protected abstract IDbConnection CreateDbConnection(string connectionString);
        /// <summary>
        /// Executes the database action.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="commandConfig">The command configuration.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        internal abstract IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false)
            where TDbDataModel : DbDataModel;


        /// <summary>
        /// Executes the database action asynchronous.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="commandConfig">The command configuration.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        internal abstract Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false)
            where TDbDataModel : DbDataModel;


        /// <summary>
        /// Gets or sets the mock data response dictionary.
        /// </summary>
        /// <value>
        /// The mock data response dictionary.
        /// </value>
        protected IDictionary<Guid, MockResponseData> MockDataResponseDictionary { get; set; }
        /// <summary>
        /// Enables the mock mode.
        /// </summary>
        /// <param name="mockDataResponseDictionary">The mock data response dictionary.</param>
        internal void EnableMockMode(IDictionary<Guid, MockResponseData> mockDataResponseDictionary)
        {
            MockDataResponseDictionary = mockDataResponseDictionary;
        }
        /// <summary>
        /// Disables the mock mode.
        /// </summary>
        internal void DisableMockMode()
        {
            MockDataResponseDictionary = null;
        }
        /// <summary>
        /// Gets a value indicating whether [use mock connection].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use mock connection]; otherwise, <c>false</c>.
        /// </value>
        protected bool UseMockConnection => MockDataResponseDictionary != null;
    }
    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="TDbDataReader">The type of the database data reader.</typeparam>
    /// <typeparam name="TDbConnection">The type of the database connection.</typeparam>
    /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
    /// <typeparam name="TDbTransaction">The type of the database transaction.</typeparam>
    /// <typeparam name="TDbParameter">The type of the database parameter.</typeparam>
    /// <typeparam name="TDbDataAdapter">The type of the database data adapter.</typeparam>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class DbConnectionConfig<TDbConnectionConfig> : DbConnectionConfigBase
        where TDbConnectionConfig : DbConnectionConfigBase

    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        internal override sealed IDbConnection GetDbConnection(IDbCommandSettings dbCommandSettings) => ResolveDbConnection(dbCommandSettings);
        /// <summary>
        /// Gets the database connection asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        internal override sealed async Task<IDbConnection> GetDbConnectionAsync(IDbCommandSettings dbCommandSettings) => await ResolveDbConnectionAsync(dbCommandSettings);

       
        /// <summary>
        /// Executes the database action.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="commandConfig">The command configuration.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        /// <exception cref="FacadeException">'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'</exception>
        internal override sealed IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig,TDbParams parameters, bool rawDataOnly = false)
        {
            try
            {
                if (commandConfig.DbConnectionConfig == this)
                {
                    commandConfig.OnBefore(parameters);
                    return DbConnectionHandler<TDbDataModel, TDbParams>.ExecuteDbAction(commandConfig, parameters, rawDataOnly, () => UseMockConnection ? new MockDataAdapter(): GetDbDataAdapter());
                }
                else
                {
                    throw new FacadeException($"'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'");
                }
            }
            catch (ValidationException<TDbParams> valEx)
            {
                OnValidationError(valEx, commandConfig.DbCommandText);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText, valEx);
            }
            catch (SQLExecutionException SQLEx)
            {
                OnSQLExecutionError(SQLEx, commandConfig.DbCommandText);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText, SQLEx);
            }
            catch (FacadeException fEx)
            {
                OnFacadeError(fEx, commandConfig.DbCommandText);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText, fEx);
            }
            catch (Exception ex)
            {
                OnError(ex, commandConfig.DbCommandText);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText, ex);
            }
        }


        /// <summary>
        /// Executes the database action asynchronous.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="commandConfig">The command configuration.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        /// <exception cref="FacadeException">'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'</exception>
        internal override sealed async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false)
        {
            try
            {
                if (commandConfig.DbConnectionConfig == this)
                {
                    await commandConfig.OnBeforeAsync(parameters);
                    return await DbConnectionHandler<TDbDataModel, TDbParams>.ExecuteDbActionAsync(commandConfig, parameters, rawDataOnly, () => UseMockConnection ? new MockDataAdapter() : GetDbDataAdapter());                    
                }
                else
                {
                    throw new FacadeException($"'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'");
                }
            }
            catch (ValidationException<TDbParams> valEx)
            {
                await OnValidationErrorAsync(valEx, commandConfig.DbCommandText);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText, valEx);
            }
            catch (SQLExecutionException SQLEx)
            {
                await OnSQLExecutionErrorAsync(SQLEx, commandConfig.DbCommandText);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText, SQLEx);
            }
            catch (FacadeException fEx)
            {
                await OnFacadeErrorAsync(fEx, commandConfig.DbCommandText);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText, fEx);
            }
            catch (Exception ex)
            {
                await OnErrorAsync(ex, commandConfig.DbCommandText);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText, ex);
            }
        }



        /// <summary>
        /// Resolves the database connection.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        private IDbConnection ResolveDbConnection(IDbCommandSettings dbCommandSettings)
        {
            if (UseMockConnection)
            {
                bool hasValue = MockDataResponseDictionary.TryGetValue(dbCommandSettings.CommandId, out MockResponseData mockResponseData);
                return new MockDbConnection(hasValue ? mockResponseData : MockResponseData.Create());
            }
            return CreateDbConnection(GetDbConnectionString());
        }
        /// <summary>
        /// Resolves the database connection asynchronous.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        private async Task<IDbConnection> ResolveDbConnectionAsync(IDbCommandSettings dbCommandSettings)
        {

            IDbConnection resolvedDbConnection;
            if (UseMockConnection)
            {
                bool hasValue = MockDataResponseDictionary.TryGetValue(dbCommandSettings.CommandId, out MockResponseData mockResponseData);
                resolvedDbConnection = new MockDbConnection(hasValue ? mockResponseData : MockResponseData.Create());
            }
            else
            {
                resolvedDbConnection = CreateDbConnection(await GetDbConnectionStringAsync());
            }
            
            await Task.CompletedTask;
            return resolvedDbConnection;
        }

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">You must impliment the method GetDbConnectionString</exception>
        protected virtual string GetDbConnectionString()
        {
            throw new NotImplementedException("You must impliment the method GetDbConnectionString");
        }
        
        /// <summary>
        /// Gets the database connection string asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">You must impliment the method GetDbConnectionStringAsync</exception>
        protected virtual async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException("You must impliment the method GetDbConnectionStringAsync");
        }
        
        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="commandSettings">The command settings.</param>
        protected virtual void OnError(Exception ex, IDbCommandSettings commandSettings) { }
        /// <summary>
        /// Called when [validation error].
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="ex">The ex.</param>
        /// <param name="commandSettings">The command settings.</param>
        protected virtual void OnValidationError<TDbParams>(ValidationException<TDbParams> ex, IDbCommandSettings commandSettings) => OnError(ex, commandSettings);
        /// <summary>
        /// Called when [SQL execution error].
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="commandSettings">The command settings.</param>
        protected virtual void OnSQLExecutionError(SQLExecutionException ex, IDbCommandSettings commandSettings) => OnError(ex, commandSettings);
        /// <summary>
        /// Called when [facade error].
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="commandSettings">The command settings.</param>
        protected virtual void OnFacadeError(FacadeException ex, IDbCommandSettings commandSettings) => OnError(ex, commandSettings);

        /// <summary>
        /// Called when [error asynchronous].
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="commandSettings">The command settings.</param>
        protected virtual async Task OnErrorAsync(Exception ex, IDbCommandSettings commandSettings) { await Task.CompletedTask;  }
        /// <summary>
        /// Called when [validation error asynchronous].
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="ex">The ex.</param>
        /// <param name="commandSettings">The command settings.</param>
        protected virtual async Task OnValidationErrorAsync<TDbParams>(ValidationException<TDbParams> ex, IDbCommandSettings commandSettings) => await OnErrorAsync(ex, commandSettings);
        /// <summary>
        /// Called when [SQL execution error asynchronous].
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="commandSettings">The command settings.</param>
        protected virtual async Task OnSQLExecutionErrorAsync(SQLExecutionException ex, IDbCommandSettings commandSettings) => await OnErrorAsync(ex, commandSettings);
        /// <summary>
        /// Called when [facade error asynchronous].
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="commandSettings">The command settings.</param>
        protected virtual async Task OnFacadeErrorAsync(FacadeException ex, IDbCommandSettings commandSettings) => await OnErrorAsync(ex, commandSettings);


        /// <summary>
        /// Creates the fetch command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbCommandConfig CreateFetchCommand(string commandText, string label, bool requiresValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, false, requiresValidation);
        /// <summary>
        /// Creates the transaction command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbCommandConfig CreateTransactionCommand(string commandText, string label, bool requiresValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, true, requiresValidation);
        /// <summary>
        /// Creates the fetch command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbCommandConfig CreateFetchCommand(string commandText, string label, CommandType commandType, bool requiresValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, false, requiresValidation);
        /// <summary>
        /// Creates the transaction command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbCommandConfig CreateTransactionCommand(string commandText, string label, CommandType commandType, bool requiresValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, true, requiresValidation);

        /// <summary>
        /// Creates the schema factory.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns></returns>
        protected static IDbCommandConfigSchemaFactory CreateSchemaFactory(string schema)
            => new DbCommandConfigSchemaFactory<TDbConnectionConfig>(schema);
        /// <summary>
        /// The dbo
        /// </summary>
        public static IDbCommandConfigSchemaFactory Dbo = CreateSchemaFactory("dbo");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        SqlConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
        protected sealed override IDbDataAdapter GetDbDataAdapter() => new SqlDataAdapter();
        protected sealed override IDbConnection CreateDbConnection(string connectionString)
            => GetCredential() is SqlCredential credential ?
            new SqlConnection(connectionString, credential) :
            new SqlConnection(connectionString);
        

        /// <summary>Gets the credential.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        protected virtual SqlCredential GetCredential() => null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        SqLiteConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig>
        where TDbConnectionConfig : DbConnectionConfigBase
    {
        protected sealed override IDbDataAdapter GetDbDataAdapter() => new SQLiteDataAdapter();
        protected sealed override IDbConnection CreateDbConnection(string connectionString) => new SQLiteConnection(connectionString);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OleDbConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
        protected sealed override IDbDataAdapter GetDbDataAdapter() => new OleDbDataAdapter();
        protected sealed override IDbConnection CreateDbConnection(string connectionString) => new OleDbConnection(connectionString);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OdbcConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
        protected sealed override IDbDataAdapter GetDbDataAdapter() => new OdbcDataAdapter();
        protected sealed override IDbConnection CreateDbConnection(string connectionString) => new OdbcConnection(connectionString);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OracleConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig>
        where TDbConnectionConfig : DbConnectionConfigBase
    {
        protected sealed override IDbDataAdapter GetDbDataAdapter() => new OracleDataAdapter();
        protected sealed override IDbConnection CreateDbConnection(string connectionString) 
            => GetCredential() is OracleCredential credential ? 
            new OracleConnection(connectionString, credential): 
            new OracleConnection(connectionString);


        /// <summary>Gets the credential.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        protected virtual OracleCredential GetCredential() => null;
    }
}