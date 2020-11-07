using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.Exceptions;
using DbFacade.Factories;

namespace DbFacade.DataLayer.CommandConfig
{
    internal class DbCommandMethod<TDbParams, TDbDataModel> : SafeDisposableBase, IDbCommandMethod<TDbParams, TDbDataModel>
        where TDbParams : DbParamsModel
        where TDbDataModel : DbDataModel
    {
        public DbConnectionConfigBase DbConnectionConfig { get; private set; }
        public IDbCommandConfigParams<TDbParams> DbParams { get; private set; }
        protected IValidator<TDbParams> ParamsValidator { get; set; }
        public DbCommandSettingsBase DbCommandText { get; private set; }
        protected bool HasValidation { get; set; }
        public bool MissingValidation { get; private set; }

        protected Action<TDbParams>[] OnBeforeActions { get; set; }
        protected Func<TDbParams, Task>[] OnBeforeAsyncActions { get; set; }
        protected DbCommandMethod() { }
        protected DbCommandMethod(
            DbCommandSettingsBase dbCommand, 
            IDbCommandConfigParams<TDbParams> dbParams,
            IValidator<TDbParams> validator,
            Action<TDbParams>[] onBeforeActions)
        {            
            DbCommandText = dbCommand;
            DbConnectionConfig = dbCommand.GetConnection() is DbConnectionConfigBase dbConnectionConfig ? dbConnectionConfig : null;
            DbParams = dbParams;
            ParamsValidator = validator;
            HasValidation = DbParams.Count > 0 && ParamsValidator.Count > 0;
            MissingValidation = DbParams.Count > 0 && ParamsValidator.Count == 0 && DbCommandText.EnforceValidation;
            OnBeforeActions = onBeforeActions;
        }

        
        internal static DbCommandMethod<TDbParams, TDbDataModel> Create(
           DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
            Action<IValidator<TDbParams>> validatorInitializer = null
            )
        => Create(dbCommand, parametersInitializer, validatorInitializer, p => { });
        
        internal static DbCommandMethod<TDbParams, TDbDataModel> Create(
           DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
            Action<IValidator<TDbParams>> validatorInitializer,
            params Action<TDbParams>[] onBeforeActions
            )
        {
            DbCommandConfigParams<TDbParams> dbParams = DbCommandConfigParams<TDbParams>.Create(parametersInitializer);
            IValidator<TDbParams> validator = ValidatorFactory.Create(validatorInitializer);            
            return new DbCommandMethod<TDbParams, TDbDataModel>(dbCommand, dbParams, validator, onBeforeActions);
        }


        #region Async Methods

        protected async Task InitAsync(DbCommandSettingsBase dbCommand,
            IDbCommandConfigParams<TDbParams> dbParams,
            IValidator<TDbParams> validator,
            Func<TDbParams, Task>[] onBeforeAsyncActions
            )
        {
            DbCommandText = dbCommand;
            DbConnectionConfig = await dbCommand.GetConnectionAsync() is DbConnectionConfigBase dbConnectionConfig ? dbConnectionConfig : null;
            DbParams = dbParams;
            ParamsValidator = validator;
            HasValidation = DbParams.Count > 0 && ParamsValidator.Count > 0;
            MissingValidation = DbParams.Count > 0 && ParamsValidator.Count == 0 && DbCommandText.EnforceValidation;
            OnBeforeAsyncActions = onBeforeAsyncActions;
            
        }
        internal static async Task<DbCommandMethod<TDbParams, TDbDataModel>> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
            Func<IValidator<TDbParams>, Task> validatorInitializer = null
            )
        => await CreateAsync(dbCommand, parametersInitializer, validatorInitializer, async p => { await Task.CompletedTask; });
        internal static async Task<DbCommandMethod<TDbParams, TDbDataModel>> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
            Func<IValidator<TDbParams>, Task> validatorInitializer,
            params Func<TDbParams, Task>[] onBeforeAsyncActions
            )
        {
            DbCommandConfigParams<TDbParams> dbParams = await DbCommandConfigParams<TDbParams>.CreateAsync(parametersInitializer);
            IValidator<TDbParams> validator = await ValidatorFactory.CreateAsync(validatorInitializer);
            DbCommandMethod<TDbParams, TDbDataModel> config = new DbCommandMethod<TDbParams, TDbDataModel>();
            await config.InitAsync(dbCommand, dbParams, validator, onBeforeAsyncActions);
            return config;
        }
        #endregion


        public void OnBefore(TDbParams paramsModel)
        {
            if (MissingValidation)
            {
                throw new FacadeException($"Validation required for {paramsModel.GetType().Name} for command '{DbCommandText.Label}'");
            }
            var validationResult = HasValidation
                ? ParamsValidator.Validate(paramsModel)
                : ValidationResult.PassingValidation;
            if (!validationResult.IsValid)
            {
                throw new ValidationException<TDbParams>(validationResult, paramsModel,
                    $"{paramsModel.GetType().Name} values failed to pass validation for command '{DbCommandText.Label}'");
            }
            try
            {                
                if (OnBeforeActions != null)
                {
                    foreach (Action<TDbParams> handler in OnBeforeActions)
                    {
                        handler(paramsModel);
                    }
                }
            }
            catch(Exception e)
            {
                throw new FacadeException($"An Error occured before calling '{DbCommandText.Label}'", e);
            } 
        }

        public async Task OnBeforeAsync(TDbParams paramsModel)
        {
            if (MissingValidation)
            {
                throw new FacadeException($"Validation required for {paramsModel.GetType().Name} for command '{DbCommandText.Label}'");
            }
            var validationResult = HasValidation
                ? await ParamsValidator.ValidateAsync(paramsModel)
                : ValidationResult.PassingValidation;
            if (!validationResult.IsValid)
            {
                throw new ValidationException<TDbParams>(validationResult, paramsModel,
                    $"{paramsModel.GetType().Name} values failed to pass validation for command '{DbCommandText.Label}'");
            }
            try
            {
                if(OnBeforeAsyncActions != null)
                {
                    foreach (Func<TDbParams, Task> handler in OnBeforeAsyncActions)
                    {
                        await handler(paramsModel);
                    }
                }
                if (OnBeforeActions != null)
                {
                    foreach (Action<TDbParams> handler in OnBeforeActions)
                    {
                        handler(paramsModel);
                    }
                    await Task.CompletedTask;
                }
            }
            catch (Exception e)
            {
                throw new FacadeException($"An Error occured before calling '{DbCommandText.Label}'", e);
            }
        }

        public IDbResponse<TDbDataModel> Execute(TDbParams parameters)
        => DbConnectionConfig.ExecuteDbAction(this, parameters);

        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync(TDbParams parameters)
        => await DbConnectionConfig.ExecuteDbActionAsync(this, parameters);

        
        public IDbResponse<TDbDataModel> Mock(TDbParams parameters,int returnValue, IDictionary<string, object> outputValues = null)
        {
            parameters.RunAsTest(returnValue,outputValues);
            return Execute(parameters);
        }
        public IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue, IDictionary<string, object> outputValues = null)
        {
            parameters.RunAsTest(responseData, returnValue,outputValues);
            return Execute(parameters);
        }
        public IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, T responseData, int returnValue, IDictionary<string, object> outputValues = null)
        {
            parameters.RunAsTest(responseData, returnValue,outputValues);
            return Execute(parameters);
        }

        public async Task<IDbResponse<TDbDataModel>> MockAsync(TDbParams parameters, int returnValue, IDictionary<string, object> outputValues = null)
        {
            await parameters.RunAsTestAsync(returnValue,outputValues);
            return await ExecuteAsync(parameters);
        }
        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue, IDictionary<string, object> outputValues = null)
        {
            await parameters.RunAsTestAsync(responseData, returnValue,outputValues);
            return await ExecuteAsync(parameters);
        }
        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, T responseData, int returnValue, IDictionary<string, object> outputValues = null)
        {
            await parameters.RunAsTestAsync(responseData, returnValue,outputValues);
            return await ExecuteAsync(parameters);
        }

        protected override void OnDispose(bool calledFromDispose)
        {
        }

        protected override void OnDisposeComplete()
        {
        }
    }

    internal class ParameterlessDbCommandMethod<TDbDataModel> : DbCommandMethod<DbParamsModel, TDbDataModel>, IParameterlessDbCommandMethod<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        public ParameterlessDbCommandMethod() { }
        public ParameterlessDbCommandMethod(
            DbCommandSettingsBase dbCommand,
            IDbCommandConfigParams<DbParamsModel> dbParams,
            IValidator<DbParamsModel> validator) : base(dbCommand, dbParams, validator, new Action<DbParamsModel>[0]) { }
        internal static ParameterlessDbCommandMethod<TDbDataModel> Create(
               DbCommandSettingsBase dbCommand,
                Action<IDbCommandConfigParams<DbParamsModel>> parametersInitializer = null
                )
        {
            DbCommandConfigParams<DbParamsModel> dbParams = DbCommandConfigParams<DbParamsModel>.Create(parametersInitializer);
            IValidator<DbParamsModel> validator = ValidatorFactory.Create((Action<IValidator<DbParamsModel>>)null);
            return new ParameterlessDbCommandMethod<TDbDataModel>(dbCommand, dbParams, validator);
        }

        #region Async Methods

        internal static async Task<ParameterlessDbCommandMethod<TDbDataModel>> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<DbParamsModel>, Task> parametersInitializer = null
            )
        {
            DbCommandConfigParams<DbParamsModel> dbParams = await DbCommandConfigParams<DbParamsModel>.CreateAsync(parametersInitializer);
            IValidator<DbParamsModel> validator = await ValidatorFactory.CreateAsync((Func<IValidator<DbParamsModel>, Task>)null);
            ParameterlessDbCommandMethod<TDbDataModel> config = new ParameterlessDbCommandMethod<TDbDataModel>();
            await config.InitAsync(dbCommand, dbParams, validator, new Func<DbParamsModel, Task>[0]);
            return config;
        }

        public IDbResponse<TDbDataModel> Execute() => Execute(new DbParamsModel());
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync() => await ExecuteAsync(new DbParamsModel());
        public IDbResponse<TDbDataModel> Mock(int returnValue, IDictionary<string, object> outputValues = null) => Mock(new DbParamsModel(), returnValue,outputValues);
        public IDbResponse<TDbDataModel> Mock<T>(IEnumerable<T> responseData, int returnValue, IDictionary<string, object> outputValues = null) => Mock(new DbParamsModel(), responseData, returnValue,outputValues);
        public IDbResponse<TDbDataModel> Mock<T>(T responseData, int returnValue, IDictionary<string, object> outputValues = null) => Mock(new DbParamsModel(), responseData, returnValue,outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync(int returnValue, IDictionary<string, object> outputValues = null) => await MockAsync(new DbParamsModel(), returnValue,outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(IEnumerable<T> responseData, int returnValue, IDictionary<string, object> outputValues = null) => await MockAsync(new DbParamsModel(), responseData, returnValue,outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(T responseData, int returnValue, IDictionary<string, object> outputValues = null) => await MockAsync(new DbParamsModel(), responseData, returnValue,outputValues);

        #endregion
    }
    internal class DbCommandMethod<TDbParams> : DbCommandMethod<TDbParams, DbDataModel>, IDbCommandMethod<TDbParams>
        where TDbParams : DbParamsModel
    {
        public DbCommandMethod() { }
        public DbCommandMethod(
            DbCommandSettingsBase dbCommand,
            IDbCommandConfigParams<TDbParams> dbParams,
            IValidator<TDbParams> validator,
            Action<TDbParams>[] onBeforeActions) : base(dbCommand, dbParams, validator, onBeforeActions) { }
        internal static new DbCommandMethod<TDbParams> Create(
               DbCommandSettingsBase dbCommand,
                Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
                Action<IValidator<TDbParams>> validatorInitializer = null
                )
            => Create(dbCommand, parametersInitializer, validatorInitializer, p => { });

        internal static new DbCommandMethod<TDbParams> Create(
           DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
            Action<IValidator<TDbParams>> validatorInitializer,
            params Action<TDbParams>[] onBeforeActions
            )
        {
            DbCommandConfigParams<TDbParams> dbParams = DbCommandConfigParams<TDbParams>.Create(parametersInitializer);
            IValidator<TDbParams> validator = ValidatorFactory.Create(validatorInitializer);
            return new DbCommandMethod<TDbParams>(dbCommand, dbParams, validator, onBeforeActions);
        }


        #region Async Methods

        internal static new async Task<DbCommandMethod<TDbParams>> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
            Func<IValidator<TDbParams>, Task> validatorInitializer = null
            )
        => await CreateAsync(dbCommand, parametersInitializer, validatorInitializer, async p => { await Task.CompletedTask; });
        internal static new async Task<DbCommandMethod<TDbParams>> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
            Func<IValidator<TDbParams>, Task> validatorInitializer,
            params Func<TDbParams, Task>[] onBeforeAsyncActions
            )
        {
            DbCommandConfigParams<TDbParams> dbParams = await DbCommandConfigParams<TDbParams>.CreateAsync(parametersInitializer);
            IValidator<TDbParams> validator = await ValidatorFactory.CreateAsync(validatorInitializer);
            DbCommandMethod<TDbParams> config = new DbCommandMethod<TDbParams>();
            await config.InitAsync(dbCommand, dbParams, validator, onBeforeAsyncActions);
            return config;
        }


        #endregion
        public new IDbResponse Execute(TDbParams parameters) => (IDbResponse)base.Execute(parameters);
        public new async Task<IDbResponse> ExecuteAsync(TDbParams parameters) => (IDbResponse)await base.ExecuteAsync(parameters);
        public new IDbResponse Mock(TDbParams parameters,int returnValue, IDictionary<string, object> outputValues = null) => (IDbResponse)base.Mock(parameters, returnValue,outputValues);

        public new async Task<IDbResponse> MockAsync(TDbParams parameters,int returnValue, IDictionary<string, object> outputValues = null) => (IDbResponse)await base.MockAsync(parameters, returnValue,outputValues);


    }
    internal class DbCommandMethod : DbCommandMethod<DbParamsModel, DbDataModel>, IDbCommandMethod
    {
        public DbCommandMethod() { }
        public DbCommandMethod(
            DbCommandSettingsBase dbCommand,
            IDbCommandConfigParams<DbParamsModel> dbParams,
            IValidator<DbParamsModel> validator) : base(dbCommand, dbParams, validator, new Action<DbParamsModel>[0]) { }
        internal static DbCommandMethod Create(
               DbCommandSettingsBase dbCommand,
                Action<IDbCommandConfigParams<DbParamsModel>> parametersInitializer = null
                )
        {
            DbCommandConfigParams<DbParamsModel> dbParams = DbCommandConfigParams<DbParamsModel>.Create(parametersInitializer);
            IValidator<DbParamsModel> validator = ValidatorFactory.Create((Action<IValidator<DbParamsModel>>)null);
            return new DbCommandMethod(dbCommand, dbParams, validator);
        }

        #region Async Methods

        internal static async Task<DbCommandMethod> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<DbParamsModel>, Task> parametersInitializer = null
            )
        {
            DbCommandConfigParams<DbParamsModel> dbParams = await DbCommandConfigParams<DbParamsModel>.CreateAsync(parametersInitializer);
            IValidator<DbParamsModel> validator = await ValidatorFactory.CreateAsync((Func<IValidator<DbParamsModel>, Task>)null);
            DbCommandMethod config = new DbCommandMethod();
            await config.InitAsync(dbCommand, dbParams, validator, new Func<DbParamsModel, Task>[0]);
            return config;
        }
        #endregion
        public IDbResponse Execute() => (IDbResponse)Execute(new DbParamsModel());
        public async Task<IDbResponse> ExecuteAsync() => (IDbResponse)await ExecuteAsync(new DbParamsModel());
        public IDbResponse Mock(int returnValue, IDictionary<string, object> outputValues = null) => (IDbResponse)Mock(new DbParamsModel(), returnValue,outputValues);
        public async Task<IDbResponse> MockAsync(int returnValue, IDictionary<string, object> outputValues = null) => (IDbResponse)await MockAsync(new DbParamsModel(), returnValue,outputValues);


    }
}