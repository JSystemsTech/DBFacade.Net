﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.Exceptions;
using DbFacade.Factories;
using DbFacade.Services;

namespace DbFacade.DataLayer.CommandConfig
{
    internal class DbCommandMethod<TDbParams, TDbDataModel> : SafeDisposableBase, IDbCommandMethod<TDbParams, TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        public DbConnectionConfigBase DbConnectionConfig { get; protected set; }
        public IDbCommandConfigParams<TDbParams> DbParams { get; protected set; }
        protected IValidator<TDbParams> ParamsValidator { get; set; }
        public DbCommandSettingsBase DbCommandText { get; protected set; }
        protected bool HasValidation { get; set; }
        protected bool MissingValidation { get; private set; }

        protected Action<TDbParams>[] OnBeforeActions { get; set; }
        protected Func<TDbParams, Task>[] OnBeforeAsyncActions { get; set; }
        protected DbCommandMethod() { }

        internal static DbCommandMethod<TDbParams, TDbDataModel> Create<TDbConnectionConfig>(
           DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
            Action<IValidator<TDbParams>> validatorInitializer = null
            ) where TDbConnectionConfig : DbConnectionConfigBase
        => Create<TDbConnectionConfig>(dbCommand, parametersInitializer, validatorInitializer, p => { });

        internal static DbCommandMethod<TDbParams, TDbDataModel> Create<TDbConnectionConfig>(
           DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
            Action<IValidator<TDbParams>> validatorInitializer,
            params Action<TDbParams>[] onBeforeActions
            ) where TDbConnectionConfig : DbConnectionConfigBase
        {
            DbCommandConfigParams<TDbParams> dbParams = DbCommandConfigParams<TDbParams>.Create(parametersInitializer);
            IValidator<TDbParams> validator = ValidatorFactory.Create(validatorInitializer);
            return new DbCommandMethod<TDbParams, TDbDataModel>()
            {
                DbCommandText = dbCommand,
                DbConnectionConfig = DbConnectionService.Get<TDbConnectionConfig>() is TDbConnectionConfig dbConnectionConfig ? dbConnectionConfig : null,
                DbParams = dbParams,
                ParamsValidator = validator,
                HasValidation = dbParams.Count > 0 && validator.Count > 0,
                OnBeforeActions = onBeforeActions
            };
        }




        #region Async Methods

        protected async Task InitAsync()
        {
            HasValidation = DbParams.Count > 0 && ParamsValidator.Count > 0;
            MissingValidation = DbParams.Count > 0 && ParamsValidator.Count == 0 && DbCommandText.RequiresValidation;
            OnBeforeAsyncActions = new Func<TDbParams, Task>[0];
            await Task.CompletedTask;
        }
        protected async Task InitAsync<TDbConnectionConfig>(DbCommandSettingsBase dbCommand,
            IDbCommandConfigParams<TDbParams> dbParams,
            IValidator<TDbParams> validator,
            Func<TDbParams, Task>[] onBeforeAsyncActions
            )
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            DbCommandText = dbCommand;
            DbConnectionConfig = await DbConnectionService.GetAsync<TDbConnectionConfig>() is DbConnectionConfigBase dbConnectionConfig ? dbConnectionConfig : null;
            DbParams = dbParams;
            ParamsValidator = validator;
            HasValidation = DbParams.Count > 0 && ParamsValidator.Count > 0;
            MissingValidation = DbParams.Count > 0 && ParamsValidator.Count == 0 && DbCommandText.RequiresValidation;
            OnBeforeAsyncActions = onBeforeAsyncActions;

        }
        internal static async Task<DbCommandMethod<TDbParams, TDbDataModel>> CreateAsync<TDbConnectionConfig>(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
            Func<IValidator<TDbParams>, Task> validatorInitializer = null
            ) where TDbConnectionConfig : DbConnectionConfigBase
        => await CreateAsync<TDbConnectionConfig>(dbCommand, parametersInitializer, validatorInitializer, async p => { await Task.CompletedTask; });
        internal static async Task<DbCommandMethod<TDbParams, TDbDataModel>> CreateAsync<TDbConnectionConfig>(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
            Func<IValidator<TDbParams>, Task> validatorInitializer,
            params Func<TDbParams, Task>[] onBeforeAsyncActions
            ) where TDbConnectionConfig : DbConnectionConfigBase
        {
            DbCommandConfigParams<TDbParams> dbParams = await DbCommandConfigParams<TDbParams>.CreateAsync(parametersInitializer);
            IValidator<TDbParams> validator = await ValidatorFactory.CreateAsync(validatorInitializer);
            DbCommandMethod<TDbParams, TDbDataModel> config = new DbCommandMethod<TDbParams, TDbDataModel>();
            await config.InitAsync<TDbConnectionConfig>(dbCommand, dbParams, validator, onBeforeAsyncActions);
            return config;
        }
        #endregion


        public void OnBefore(TDbParams paramsModel)
        {
            if (MissingValidation)
            {
                throw new FacadeException($"Validation required for {(paramsModel is TDbParams pm? pm.GetType().Name: "(null)")} for command '{DbCommandText.Label}'");
            }
            var validationResult = HasValidation
                ? ParamsValidator.Validate(paramsModel)
                : ValidationResult.PassingValidation;
            if (!validationResult.IsValid)
            {
                throw new ValidationException<TDbParams>(validationResult, paramsModel,
                    $"{(paramsModel is TDbParams pm ? pm.GetType().Name : "(null)")} values failed to pass validation for command '{DbCommandText.Label}'");
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
            catch (Exception e)
            {
                throw new FacadeException($"An Error occured before calling '{DbCommandText.Label}'", e);
            }
        }

        public async Task OnBeforeAsync(TDbParams paramsModel)
        {
            if (MissingValidation)
            {
                throw new FacadeException($"Validation required for {(paramsModel is TDbParams pm ? pm.GetType().Name : "(null)")} for command '{DbCommandText.Label}'");
            }
            var validationResult = HasValidation
                ? await ParamsValidator.ValidateAsync(paramsModel)
                : ValidationResult.PassingValidation;
            if (!validationResult.IsValid)
            {
                throw new ValidationException<TDbParams>(validationResult, paramsModel,
                    $"{(paramsModel is TDbParams pm ? pm.GetType().Name : "(null)")} values failed to pass validation for command '{DbCommandText.Label}'");
            }
            try
            {
                if (OnBeforeAsyncActions != null)
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

        public IDbResponse<TDbDataModel> Mock(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null)
            => DbConnectionConfig.ExecuteDbAction(this, parameters, MockResponseData.Create(outputValues, returnValue));
        public IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null)
            => DbConnectionConfig.ExecuteDbAction(this, parameters, MockResponseData.Create(responseData, outputValues, returnValue));

        public async Task<IDbResponse<TDbDataModel>> MockAsync(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null)
            => await DbConnectionConfig.ExecuteDbActionAsync(this, parameters, await MockResponseData.CreateAsync(outputValues, returnValue));
        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null)
            => await DbConnectionConfig.ExecuteDbActionAsync(this, parameters, await MockResponseData.CreateAsync(responseData, outputValues, returnValue));
        

        protected override void OnDispose(bool calledFromDispose)
        {
        }

        protected override void OnDisposeComplete()
        {
        }
    }

    internal class ParameterlessDbCommandMethod<TDbDataModel> : DbCommandMethod<object, TDbDataModel>, IParameterlessDbCommandMethod<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        public ParameterlessDbCommandMethod() { }   

        internal static ParameterlessDbCommandMethod<TDbDataModel> Create<TDbConnectionConfig>(
            DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<object>> parametersInitializer = null)
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            return new ParameterlessDbCommandMethod<TDbDataModel>()
            {
                DbCommandText = dbCommand,
                DbConnectionConfig = DbConnectionService.Get<TDbConnectionConfig>() is TDbConnectionConfig dbConnectionConfig ? dbConnectionConfig : null,
                DbParams = DbCommandConfigParams<object>.Create(parametersInitializer),
                ParamsValidator = ValidatorFactory.Create((Action<IValidator<object>>)null)
            };
        }

        #region Async Methods

        internal static async Task<ParameterlessDbCommandMethod<TDbDataModel>> CreateAsync<TDbConnectionConfig>(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<object>, Task> parametersInitializer = null
            )
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            ParameterlessDbCommandMethod<TDbDataModel> config = new ParameterlessDbCommandMethod<TDbDataModel>()
            {
                DbCommandText = dbCommand,
                DbConnectionConfig = DbConnectionService.Get<TDbConnectionConfig>() is TDbConnectionConfig dbConnectionConfig ? dbConnectionConfig : null,
                DbParams = await DbCommandConfigParams<object>.CreateAsync(parametersInitializer),
                ParamsValidator = ValidatorFactory.Create((Action<IValidator<object>>)null)
            };
            await config.InitAsync();
            return config;
        }

        public IDbResponse<TDbDataModel> Execute() => Execute(null);
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync() => await ExecuteAsync(null);
        public IDbResponse<TDbDataModel> Mock(int returnValue, Action<IDictionary<string, object>> outputValues = null) => Mock(null, returnValue,outputValues);
        public IDbResponse<TDbDataModel> Mock<T>(IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null) => Mock(null, responseData, returnValue,outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync(int returnValue, Action<IDictionary<string, object>> outputValues = null) => await MockAsync(null, returnValue,outputValues);

        public async Task<IDbResponse<TDbDataModel>> MockAsync<T>(IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null) => await MockAsync(null, responseData, returnValue,outputValues);

        #endregion
    }
    internal class DbCommandMethod<TDbParams> : DbCommandMethod<TDbParams, DbDataModel>, IDbCommandMethod<TDbParams>
    {
        public DbCommandMethod() { }
        
        internal static new DbCommandMethod<TDbParams> Create<TDbConnectionConfig>(
               DbCommandSettingsBase dbCommand,
                Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
                Action<IValidator<TDbParams>> validatorInitializer = null
                )
            where TDbConnectionConfig : DbConnectionConfigBase
            => Create<TDbConnectionConfig>(dbCommand, parametersInitializer, validatorInitializer, p => { });

        
        internal new static DbCommandMethod<TDbParams> Create<TDbConnectionConfig>(
           DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
            Action<IValidator<TDbParams>> validatorInitializer,
            params Action<TDbParams>[] onBeforeActions
            )
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            DbCommandConfigParams<TDbParams> dbParams = DbCommandConfigParams<TDbParams>.Create(parametersInitializer);
            IValidator<TDbParams> validator = ValidatorFactory.Create(validatorInitializer);
            return new DbCommandMethod<TDbParams>()
            {
                DbCommandText = dbCommand,
                DbConnectionConfig = DbConnectionService.Get<TDbConnectionConfig>() is TDbConnectionConfig dbConnectionConfig ? dbConnectionConfig : null,
                DbParams = dbParams,
                ParamsValidator = validator,
                HasValidation = dbParams.Count > 0 && validator.Count > 0,
                OnBeforeActions = onBeforeActions
            };
        }


        #region Async Methods

        internal static new async Task<DbCommandMethod<TDbParams>> CreateAsync<TDbConnectionConfig>(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
            Func<IValidator<TDbParams>, Task> validatorInitializer = null
            ) where TDbConnectionConfig : DbConnectionConfigBase
        => await CreateAsync<TDbConnectionConfig>(dbCommand, parametersInitializer, validatorInitializer, async p => { await Task.CompletedTask; });
        internal static new async Task<DbCommandMethod<TDbParams>> CreateAsync<TDbConnectionConfig>(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
            Func<IValidator<TDbParams>, Task> validatorInitializer,
            params Func<TDbParams, Task>[] onBeforeAsyncActions
            ) where TDbConnectionConfig : DbConnectionConfigBase
        {
            DbCommandConfigParams<TDbParams> dbParams = await DbCommandConfigParams<TDbParams>.CreateAsync(parametersInitializer);
            IValidator<TDbParams> validator = await ValidatorFactory.CreateAsync(validatorInitializer);
            DbCommandMethod<TDbParams> config = new DbCommandMethod<TDbParams>();
            await config.InitAsync<TDbConnectionConfig>(dbCommand, dbParams, validator, onBeforeAsyncActions);
            return config;
        }


        #endregion
        public new IDbResponse Execute(TDbParams parameters) => (IDbResponse)base.Execute(parameters);
        public new async Task<IDbResponse> ExecuteAsync(TDbParams parameters) => (IDbResponse)await base.ExecuteAsync(parameters);
        public new IDbResponse Mock(TDbParams parameters,int returnValue, Action<IDictionary<string, object>> outputValues = null) => (IDbResponse)base.Mock(parameters, returnValue,outputValues);

        public new async Task<IDbResponse> MockAsync(TDbParams parameters,int returnValue, Action<IDictionary<string, object>> outputValues = null) => (IDbResponse)await base.MockAsync(parameters, returnValue,outputValues);


    }
    internal class DbCommandMethod : DbCommandMethod<object, DbDataModel>, IDbCommandMethod
    {
        public DbCommandMethod() { }
        
        internal static DbCommandMethod Create<TDbConnectionConfig>(
            DbCommandSettingsBase dbCommand,
            IDbCommandConfigParams<object> dbParams,
            IValidator<object> validator)
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            return new DbCommandMethod()
            {
                DbCommandText = dbCommand,
                DbConnectionConfig = DbConnectionService.Get<TDbConnectionConfig>() is TDbConnectionConfig dbConnectionConfig ? dbConnectionConfig : null,
                DbParams = dbParams,
                ParamsValidator = validator,
                HasValidation = dbParams.Count > 0 && validator.Count > 0,
                OnBeforeActions = new Action<object>[0]
            };
        }
        internal static DbCommandMethod Create<TDbConnectionConfig>(
               DbCommandSettingsBase dbCommand,
                Action<IDbCommandConfigParams<object>> parametersInitializer = null
                )
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            DbCommandConfigParams<object> dbParams = DbCommandConfigParams<object>.Create(parametersInitializer);
            IValidator<object> validator = ValidatorFactory.Create((Action<IValidator<object>>)null);
            return Create<TDbConnectionConfig>(dbCommand, dbParams, validator);
        }

        #region Async Methods

        internal static async Task<DbCommandMethod> CreateAsync<TDbConnectionConfig>(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<object>, Task> parametersInitializer = null
            )
            where TDbConnectionConfig : DbConnectionConfigBase
        {
            DbCommandConfigParams<object> dbParams = await DbCommandConfigParams<object>.CreateAsync(parametersInitializer);
            IValidator<object> validator = await ValidatorFactory.CreateAsync((Func<IValidator<object>, Task>)null);
            DbCommandMethod config = new DbCommandMethod();
            await config.InitAsync<TDbConnectionConfig>(dbCommand, dbParams, validator, new Func<object, Task>[0]);
            return config;
        }
        #endregion
        public IDbResponse Execute() => (IDbResponse)Execute(null);
        public async Task<IDbResponse> ExecuteAsync() => (IDbResponse)await ExecuteAsync(null);
        public IDbResponse Mock(int returnValue, Action<IDictionary<string, object>> outputValues = null) => (IDbResponse)Mock(null, returnValue,outputValues);
        public async Task<IDbResponse> MockAsync(int returnValue, Action<IDictionary<string, object>> outputValues = null) => (IDbResponse)await MockAsync(null, returnValue,outputValues);


    }
}