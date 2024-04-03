using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Factories;
using DbFacade.Utils;
using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.ConnectionService
{



    /// <summary>
    ///   <br />
    /// </summary>
    internal interface IDbConnectionCore
    {
        /// <summary>Executes the database action.</summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="commandConfig">The command configuration.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false)
                where TDbDataModel : DbDataModel;
        /// <summary>Executes the database action asynchronous.</summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="commandConfig">The command configuration.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false)
                where TDbDataModel : DbDataModel;
        /// <summary>Resolves the database connection.</summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="method"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        IDbConnection ResolveDbConnection(IDbCommandSettings dbCommandSettings, IDbMethod method);
    }
    /// <summary>
    ///   <br />
    /// </summary>
    public abstract class DbConnectionConfig<TDbConnectionConfig>: IDbConnectionConfig 
        where TDbConnectionConfig: IDbConnectionConfig
    {
        private class DbConnectionCore : IDbConnectionCore
        {
            public IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false) 
                where TDbDataModel : DbDataModel
                => ExecuteDbActionCore(commandConfig, parameters, rawDataOnly);
            public async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false)
                where TDbDataModel : DbDataModel
                => await ExecuteDbActionAsyncCore(commandConfig, parameters, rawDataOnly);
            public IDbConnection ResolveDbConnection(IDbCommandSettings dbCommandSettings, IDbMethod method)
                => ResolveDbConnectionCore(dbCommandSettings, method);

        }
        /// <summary>Resolves the database connection.</summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="method"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        private static IDbConnection ResolveDbConnectionCore(IDbCommandSettings dbCommandSettings, IDbMethod method)
        {
            if (_GetConnectionOptions().TryGetMockDbConnection(dbCommandSettings, method, out IDbConnection connection))
            {
                return connection;
            }
            return _GetConnectionOptions().CreateDbConnection(_GetConnectionOptions().GetConnectionString());
        }

        private static readonly ConcurrentDictionary<Type, ConnectionOptions> ConfigurationOptionsStore = new ConcurrentDictionary<Type, ConnectionOptions>();
        private static ConnectionOptions _GetConnectionOptions()
        {
            Type key = typeof(TDbConnectionConfig);
            if (ConfigurationOptionsStore.TryGetValue(key, out ConnectionOptions config))
            {
                return config;
            }
            else
            {
                ConnectionOptions newConfig = new ConnectionOptions();
                ConfigurationOptionsStore.TryAdd(key, newConfig);
                return newConfig;
            }
        }
        protected static IConnectionOptions GetConnectionOptions() => _GetConnectionOptions();
        private static IDbResponse<TDbDataModel> ExecuteDbActionCore<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false)
            where TDbDataModel : DbDataModel
        {
            try
            {
                if (!_GetConnectionOptions().IsConfigured)
                {
                    throw new DbConnectionConfigNotRegisteredException();
                }
                commandConfig.OnBefore(parameters);
                return DbConnectionHandler<TDbDataModel, TDbParams>.ExecuteDbAction(
                    commandConfig, 
                    parameters, 
                    rawDataOnly, 
                    () => _GetConnectionOptions().UseMockConnection ? new MockDataAdapter() : _GetConnectionOptions().GetDbDataAdapter()
                    );
            }
            catch (ValidationException valEx)
            {
                _GetConnectionOptions().OnValidationError(valEx, commandConfig.DbCommandText);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText, valEx);
            }
            catch (SQLExecutionException SQLEx)
            {
                _GetConnectionOptions().OnSQLExecutionError(SQLEx, commandConfig.DbCommandText);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText, SQLEx);
            }
            catch (FacadeException fEx)
            {
                _GetConnectionOptions().OnFacadeError(fEx, commandConfig.DbCommandText);
                return DbResponseFactory<TDbDataModel>.CreateErrorResponse(commandConfig.DbCommandText, fEx);
            }
            catch (Exception ex)
            {
                _GetConnectionOptions().OnError(ex, commandConfig.DbCommandText);
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
        private static async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsyncCore<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false)
            where TDbDataModel : DbDataModel
        {
            try
            {
                if (!_GetConnectionOptions().IsConfigured)
                {
                    throw new DbConnectionConfigNotRegisteredException();
                }
                await commandConfig.OnBeforeAsync(parameters);
                return await DbConnectionHandler<TDbDataModel, TDbParams>.ExecuteDbActionAsync(
                    commandConfig, 
                    parameters, 
                    rawDataOnly, 
                    () => _GetConnectionOptions().UseMockConnection ? new MockDataAdapter() : _GetConnectionOptions().GetDbDataAdapter()
                    );
            }
            catch (ValidationException valEx)
            {
                await _GetConnectionOptions().OnValidationErrorAsync(valEx, commandConfig.DbCommandText);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText, valEx);
            }
            catch (SQLExecutionException SQLEx)
            {
                await _GetConnectionOptions().OnSQLExecutionErrorAsync(SQLEx, commandConfig.DbCommandText);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText, SQLEx);
            }
            catch (FacadeException fEx)
            {
                await _GetConnectionOptions().OnFacadeErrorAsync(fEx, commandConfig.DbCommandText);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText, fEx);
            }
            catch (Exception ex)
            {
                await _GetConnectionOptions().OnErrorAsync(ex, commandConfig.DbCommandText);
                return await DbResponseFactory<TDbDataModel>.CreateErrorResponseAsync(commandConfig.DbCommandText, ex);
            }
        }


        /// <summary>
        /// Creates the fetch command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        internal static IDbCommandConfig CreateFetchCommandCore(string commandText, string label, bool requiresValidation = false)
        => DbCommandSettings.Create(new DbConnectionCore(), commandText, label, CommandType.StoredProcedure, false, requiresValidation);
        /// <summary>
        /// Creates the transaction command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        internal static IDbCommandConfig CreateTransactionCommandCore(string commandText, string label, bool requiresValidation = true)
        => DbCommandSettings.Create(new DbConnectionCore(), commandText, label, CommandType.StoredProcedure, true, requiresValidation);
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
        internal static IDbCommandConfig CreateFetchCommandCore(string commandText, string label, CommandType commandType, bool requiresValidation = false)
        => DbCommandSettings.Create(new DbConnectionCore(), commandText, label, commandType, false, requiresValidation);
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
        internal static IDbCommandConfig CreateTransactionCommandCore(string commandText, string label, CommandType commandType, bool requiresValidation = true)
        => DbCommandSettings.Create(new DbConnectionCore(), commandText, label, commandType, true, requiresValidation);

        /// <summary>
        /// Creates the schema factory.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns></returns>
        internal static IDbCommandConfigSchemaFactory CreateSchemaFactoryCore(string schema)
            => new DbCommandConfigSchemaFactory(new DbConnectionCore(), schema);
        /// <summary>
        /// Creates the schema factory.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns></returns>
        internal static IDbCommandConfigSchemaFactoryFull CreateSchemaFactoryFullCore(string schema)
            => new DbCommandConfigSchemaFactoryFull(new DbConnectionCore(), schema);

        /// <summary>Enables the mock mode.</summary>
        public static void EnableMockMode() => _GetConnectionOptions().EnableMockMode();
        /// <summary>Disables the mock mode.</summary>
        public static void DisableMockMode() => _GetConnectionOptions().DisableMockMode();
    }

    /// <summary>
    ///   <br />
    /// </summary>
    public abstract class DbConnectionConfigNoTransaction<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig>
        where TDbConnectionConfig : IDbConnectionConfig
    {
        

        
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
        => CreateFetchCommandCore(commandText, label, requiresValidation);
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
        => CreateFetchCommandCore(commandText, label, commandType, requiresValidation);


        /// <summary>
        /// Creates the schema factory.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns></returns>
        protected static IDbCommandConfigSchemaFactory CreateSchemaFactory(string schema)
            => CreateSchemaFactoryCore(schema);
        /// <summary>
        /// The dbo
        /// </summary>
        public static IDbCommandConfigSchemaFactory Dbo = CreateSchemaFactory("dbo");
    }
    /// <summary>
    ///   <br />
    /// </summary>
    public abstract class DbConnectionConfigFull<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig>
        where TDbConnectionConfig : IDbConnectionConfig
    {

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
        => CreateFetchCommandCore(commandText, label, requiresValidation);
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
        => CreateFetchCommandCore(commandText, label, commandType, requiresValidation);

        /// <summary>
        /// Creates the transaction command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        internal static IDbCommandConfig CreateTransactionCommand(string commandText, string label, bool requiresValidation = true)
        => CreateTransactionCommandCore(commandText, label, requiresValidation);
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
        internal static IDbCommandConfig CreateTransactionCommand(string commandText, string label, CommandType commandType, bool requiresValidation = true)
        => CreateTransactionCommandCore(commandText, label, commandType, requiresValidation);


        /// <summary>
        /// Creates the schema factory.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns></returns>
        protected static IDbCommandConfigSchemaFactoryFull CreateSchemaFactory(string schema)
            => CreateSchemaFactoryFullCore(schema);
        /// <summary>
        /// The dbo
        /// </summary>
        public static IDbCommandConfigSchemaFactoryFull Dbo = CreateSchemaFactory("dbo");
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class SqlConnectionConfig<TDbConnectionConfig> :
        DbConnectionConfigFull<TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig 
    {
        public static void Configure(OnGetConnectionString getConnectionString, Action<IErrorHandlerOptions> handler)
        {
            handler(GetConnectionOptions().ErrorHandlerOptions);
            GetConnectionOptions().SetOnGetDbDataAdapter(() => new SqlDataAdapter());
            GetConnectionOptions().SetOnGetConnectionString(getConnectionString);
            GetConnectionOptions().SetOnCreateDbConnection(connectionString => new SqlConnection(connectionString));
        }
        public static void Configure(OnGetConnectionString getConnectionString, SqlCredential credential, Action<IErrorHandlerOptions> handler)
        {
            handler(GetConnectionOptions().ErrorHandlerOptions);
            GetConnectionOptions().SetOnGetDbDataAdapter(() => new SqlDataAdapter());
            GetConnectionOptions().SetOnGetConnectionString(getConnectionString);
            GetConnectionOptions().SetOnCreateDbConnection(connectionString => new SqlConnection(connectionString, credential));
        }
    }

    

    public interface IConnectionOptions
    {
        IErrorHandlerOptions ErrorHandlerOptions { get; }
        void SetOnGetDbDataAdapter(OnGetDbDataAdapter handler);
        void SetOnCreateDbConnection(OnCreateDbConnection handler);
        void SetOnGetConnectionString(OnGetConnectionString handler);
    }
    internal class ConnectionOptions: IConnectionOptions
    {

        private IErrorHandlerOptionsInternal _ErrorHandlerOptions { get; set; }
        public IErrorHandlerOptions ErrorHandlerOptions => _ErrorHandlerOptions;

        public OnGetDbDataAdapter GetDbDataAdapter { get; private set; }
        public void SetOnGetDbDataAdapter(OnGetDbDataAdapter handler) { GetDbDataAdapter = handler; }
        public OnCreateDbConnection CreateDbConnection { get; private set; }
        public void SetOnCreateDbConnection(OnCreateDbConnection handler) { CreateDbConnection = handler; }
        public OnGetConnectionString GetConnectionString { get; private set; }
        public void SetOnGetConnectionString(OnGetConnectionString handler) { GetConnectionString = handler; }

        public void OnError(Exception ex, IDbCommandSettings commandSettings) => _ErrorHandlerOptions.OnErrorHandler(ex, commandSettings);
        public void OnValidationError(ValidationException ex, IDbCommandSettings commandSettings) => _ErrorHandlerOptions.OnValidationErrorHandler(ex, commandSettings);
        public void OnSQLExecutionError(SQLExecutionException ex, IDbCommandSettings commandSettings) => _ErrorHandlerOptions.OnSQLExecutionErrorHandler(ex, commandSettings);
        public void OnFacadeError(FacadeException ex, IDbCommandSettings commandSettings) => _ErrorHandlerOptions.OnFacadeErrorHandler(ex, commandSettings);


        public async Task OnErrorAsync(Exception ex, IDbCommandSettings commandSettings) => await _ErrorHandlerOptions.OnErrorHandlerAsync(ex, commandSettings);
        public async Task OnValidationErrorAsync(ValidationException ex, IDbCommandSettings commandSettings) => await _ErrorHandlerOptions.OnValidationErrorHandlerAsync(ex, commandSettings);
        public async Task OnSQLExecutionErrorAsync(SQLExecutionException ex, IDbCommandSettings commandSettings) => await _ErrorHandlerOptions.OnSQLExecutionErrorHandlerAsync(ex, commandSettings);
        public async Task OnFacadeErrorAsync(FacadeException ex, IDbCommandSettings commandSettings) => await _ErrorHandlerOptions.OnFacadeErrorHandlerAsync(ex, commandSettings);



        public void EnableMockMode() => UseMockConnection = true;
        public void DisableMockMode() => UseMockConnection = false;
        internal bool UseMockConnection { get; private set; }
        internal bool IsConfigured => GetDbDataAdapter != null && CreateDbConnection != null && GetConnectionString != null;
        internal bool TryGetMockDbConnection(IDbCommandSettings dbCommandSettings, IDbMethod method, out IDbConnection connection)
        {
            if (UseMockConnection)
            {
                using (MeasurableMetric.Create("ConnectionOptions_TryGetMockDbConnection"))
                {
                    connection = new MockDbConnection(
                    MockDataResponseFactory.TryGetMockResponseData(method, out MockResponseData mockResponseDataForMethod) ? mockResponseDataForMethod :
                    MockDataResponseFactory.TryGetMockResponseData(dbCommandSettings, out MockResponseData mockResponseData) ? mockResponseData :
                    MockResponseData.Create());
                }
                return true;
            }
            connection = default(IDbConnection);
            return false;
        }
        public ConnectionOptions() { 
            _ErrorHandlerOptions = new ErrorHandlerOptions();
        }
    }
    
    public static class MockDataResponseFactory
    {
        private static readonly ConcurrentDictionary<Guid, MockResponseData> DbCommandData = new ConcurrentDictionary<Guid, MockResponseData>();
        private static readonly ConcurrentDictionary<Guid, MockResponseData> DbMethodData = new ConcurrentDictionary<Guid, MockResponseData>();

        internal static bool TryGetMockResponseData(this IDbCommandSettings dbCommandSettings, out MockResponseData response)
        => DbCommandData.TryGetValue(dbCommandSettings.CommandId, out response);
        internal static bool TryGetMockResponseData(this IDbMethod method, out MockResponseData response)
        => DbMethodData.TryGetValue(method.MethodId, out response);
        public static void AddMockResponseData(this IDbCommandConfig config , MockResponseData response)
        => AddMockResponseToDbCommandData(config.CommandId, response);
        public static void AddMockResponseData(this IDbMethod method, MockResponseData response)
        => AddMockResponseToDbCommandData(method.MethodId, response);
        private static void AddMockResponseToDbCommandData(this Guid commandId, MockResponseData response)
        {
            DbCommandData.TryRemove(commandId, out MockResponseData r1);
            DbCommandData.TryAdd(commandId, response);
        }
        private static void AddMockResponseToDbMethodData(this Guid methodId, MockResponseData response)
        {
            DbMethodData.TryRemove(methodId, out MockResponseData r1);
            DbMethodData.TryAdd(methodId, response);
        }
        public static void Clear() { 
            DbCommandData.Clear();
            DbMethodData.Clear();
        }
    }   
    
    
    public interface IErrorHandlerOptions
    {
        void SetOnErrorHandler(OnErrorHandler handler);
        void SetOnValidationErrorHandler(OnValidationErrorHandler handler);
        void SetOnSQLExecutionErrorHandler(OnSQLExecutionErrorHandler handler);
        void SetOnFacadeErrorHandler(OnFacadeErrorHandler handler);

        void SetOnErrorHandlerAsync(OnErrorHandlerAsync handler);
        void SetOnValidationErrorHandlerAsync(OnValidationErrorHandlerAsync handler);
        void SetOnSQLExecutionErrorHandlerAsync(OnSQLExecutionErrorHandlerAsync handler);
        void SetOnFacadeErrorHandlerAsync(OnFacadeErrorHandlerAsync handler);
    }
    internal interface IErrorHandlerOptionsInternal: IErrorHandlerOptions 
    {
        OnErrorHandler OnErrorHandler { get; }
        OnValidationErrorHandler OnValidationErrorHandler { get; }
        OnSQLExecutionErrorHandler OnSQLExecutionErrorHandler { get; }
        OnFacadeErrorHandler OnFacadeErrorHandler { get; }

        OnErrorHandlerAsync OnErrorHandlerAsync { get; }
        OnValidationErrorHandlerAsync OnValidationErrorHandlerAsync { get; }
        OnSQLExecutionErrorHandlerAsync OnSQLExecutionErrorHandlerAsync { get; }
        OnFacadeErrorHandlerAsync OnFacadeErrorHandlerAsync { get; }
    }
    internal class ErrorHandlerOptions: IErrorHandlerOptionsInternal
    {


        public OnErrorHandler OnErrorHandler {  get; private set; }
        public OnValidationErrorHandler OnValidationErrorHandler { get; private set; }
        public OnSQLExecutionErrorHandler OnSQLExecutionErrorHandler { get; private set; }
        public OnFacadeErrorHandler OnFacadeErrorHandler { get; private set; }
        
        public OnErrorHandlerAsync OnErrorHandlerAsync { get; private set; }
        public OnValidationErrorHandlerAsync OnValidationErrorHandlerAsync { get; private set; }
        public OnSQLExecutionErrorHandlerAsync OnSQLExecutionErrorHandlerAsync { get; private set; }
        public OnFacadeErrorHandlerAsync OnFacadeErrorHandlerAsync { get; private set; }
        public ErrorHandlerOptions()
        {
            OnErrorHandler = (ex, commandSettings) => { };
            OnValidationErrorHandler = (ex, commandSettings) => OnErrorHandler(ex, commandSettings);
            OnSQLExecutionErrorHandler = (ex, commandSettings) => OnErrorHandler(ex, commandSettings);
            OnFacadeErrorHandler = (ex, commandSettings) => OnErrorHandler(ex, commandSettings);

            OnErrorHandlerAsync = (ex, commandSettings) => Task.CompletedTask; 
            OnValidationErrorHandlerAsync = (ex, commandSettings) => OnErrorHandlerAsync(ex, commandSettings);
            OnSQLExecutionErrorHandlerAsync = (ex, commandSettings) => OnErrorHandlerAsync(ex, commandSettings);
            OnFacadeErrorHandlerAsync = (ex, commandSettings) => OnErrorHandlerAsync(ex, commandSettings);
        }
        public void SetOnErrorHandler(OnErrorHandler handler) { OnErrorHandler = handler; }
        public void SetOnValidationErrorHandler(OnValidationErrorHandler handler) { OnValidationErrorHandler = handler; }
        public void SetOnSQLExecutionErrorHandler(OnSQLExecutionErrorHandler handler) { OnSQLExecutionErrorHandler = handler; }
        public void SetOnFacadeErrorHandler(OnFacadeErrorHandler handler) { OnFacadeErrorHandler = handler; }

        public void SetOnErrorHandlerAsync(OnErrorHandlerAsync handler) { OnErrorHandlerAsync = handler; }
        public void SetOnValidationErrorHandlerAsync(OnValidationErrorHandlerAsync handler) { OnValidationErrorHandlerAsync = handler; }
        public void SetOnSQLExecutionErrorHandlerAsync(OnSQLExecutionErrorHandlerAsync handler) { OnSQLExecutionErrorHandlerAsync = handler; }
        public void SetOnFacadeErrorHandlerAsync(OnFacadeErrorHandlerAsync handler) { OnFacadeErrorHandlerAsync = handler; }
    }
}