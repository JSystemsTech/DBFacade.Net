using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.Exceptions;
using DbFacade.Factories;
using System;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    internal class DbCommandMethod<TDbParams, TDbDataModel> : SafeDisposableBase, IDbCommandMethod<TDbParams, TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>Gets or sets the command method identifier.</summary>
        /// <value>The command method identifier.</value>
        public Guid MethodId { get; protected set; }
        /// <summary>
        /// Gets or sets the database parameters.
        /// </summary>
        /// <value>
        /// The database parameters.
        /// </value>
        public IDbCommandConfigParams<TDbParams> DbParams { get; protected set; }
        /// <summary>
        /// Gets or sets the parameters validator.
        /// </summary>
        /// <value>
        /// The parameters validator.
        /// </value>
        protected IValidator<TDbParams> ParamsValidator { get; set; }
        /// <summary>
        /// Gets or sets the database command text.
        /// </summary>
        /// <value>
        /// The database command text.
        /// </value>
        public DbCommandSettingsBase DbCommandText { get; protected set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has validation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has validation; otherwise, <c>false</c>.
        /// </value>
        protected bool HasValidation { get; set; }
        /// <summary>
        /// Gets a value indicating whether [missing validation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [missing validation]; otherwise, <c>false</c>.
        /// </value>
        protected bool MissingValidation { get; private set; }

        /// <summary>
        /// Gets or sets the on before actions.
        /// </summary>
        /// <value>
        /// The on before actions.
        /// </value>
        protected Action<TDbParams>[] OnBeforeActions { get; set; }
        /// <summary>
        /// Gets or sets the on before asynchronous actions.
        /// </summary>
        /// <value>
        /// The on before asynchronous actions.
        /// </value>
        protected Func<TDbParams, Task>[] OnBeforeAsyncActions { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandMethod{TDbParams, TDbDataModel}" /> class.
        /// </summary>
        protected DbCommandMethod() {
            MethodId = Guid.NewGuid();
        }

        /// <summary>
        /// Creates the specified database command.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        internal static DbCommandMethod<TDbParams, TDbDataModel> Create(
           DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
            Action<IValidator<TDbParams>> validatorInitializer = null
            )
        => Create(dbCommand, parametersInitializer, validatorInitializer, p => { });

        /// <summary>
        /// Creates the specified database command.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeActions">The on before actions.</param>
        /// <returns></returns>
        internal static DbCommandMethod<TDbParams, TDbDataModel> Create(
           DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
            Action<IValidator<TDbParams>> validatorInitializer,
            params Action<TDbParams>[] onBeforeActions
            )
        {
            DbCommandConfigParams<TDbParams> dbParams = DbCommandConfigParams<TDbParams>.Create(parametersInitializer);
            IValidator<TDbParams> validator = ValidatorFactory.Create(validatorInitializer);
            return new DbCommandMethod<TDbParams, TDbDataModel>()
            {
                DbCommandText = dbCommand,
                DbParams = dbParams,
                ParamsValidator = validator,
                HasValidation = dbParams.Count > 0 && validator.Count > 0,
                OnBeforeActions = onBeforeActions
            };
        }


        #region Async Methods

        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        protected async Task InitAsync()
        {
            HasValidation = DbParams.Count > 0 && ParamsValidator.Count > 0;
            MissingValidation = DbParams.Count > 0 && ParamsValidator.Count == 0 && DbCommandText.RequiresValidation;
            OnBeforeAsyncActions = new Func<TDbParams, Task>[0];
            await Task.CompletedTask;
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="validator">The validator.</param>
        /// <param name="onBeforeAsyncActions">The on before asynchronous actions.</param>
        protected async Task InitAsync(DbCommandSettingsBase dbCommand,
            IDbCommandConfigParams<TDbParams> dbParams,
            IValidator<TDbParams> validator,
            Func<TDbParams, Task>[] onBeforeAsyncActions
            )
        {
            DbCommandText = dbCommand;
            DbParams = dbParams;
            ParamsValidator = validator;
            HasValidation = DbParams.Count > 0 && ParamsValidator.Count > 0;
            MissingValidation = DbParams.Count > 0 && ParamsValidator.Count == 0 && DbCommandText.RequiresValidation;
            OnBeforeAsyncActions = onBeforeAsyncActions;
            await Task.CompletedTask;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        internal static async Task<DbCommandMethod<TDbParams, TDbDataModel>> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
            Func<IValidator<TDbParams>, Task> validatorInitializer = null
            )
        => await CreateAsync(dbCommand, parametersInitializer, validatorInitializer, async p => { await Task.CompletedTask; });
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeAsyncActions">The on before asynchronous actions.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Called when [before].
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <exception cref="FacadeException">
        /// Validation required for {(paramsModel is TDbParams pm? pm.GetType().Name: "(null)")} for command '{DbCommandText.Label}'
        /// or
        /// An Error occured before calling '{DbCommandText.Label}'
        /// </exception>
        /// <exception cref="ValidationException&lt;TDbParams&gt;"></exception>
        public void OnBefore(TDbParams paramsModel)
        {
            if (MissingValidation)
            {
                throw new FacadeException($"Validation required for {(paramsModel is TDbParams pm ? pm.GetType().Name : "(null)")} for command '{DbCommandText.Label}'");
            }
            var validationResult = HasValidation
                ? ParamsValidator.Validate(paramsModel)
                : ValidationResult.PassingValidation;
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult, paramsModel,
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

        /// <summary>
        /// Called when [before asynchronous].
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <exception cref="FacadeException">
        /// Validation required for {(paramsModel is TDbParams pm ? pm.GetType().Name : "(null)")} for command '{DbCommandText.Label}'
        /// or
        /// An Error occured before calling '{DbCommandText.Label}'
        /// </exception>
        /// <exception cref="ValidationException&lt;TDbParams&gt;"></exception>
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
                throw new ValidationException(validationResult, paramsModel,
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

        /// <summary>
        /// Executes the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        public IDbResponse<TDbDataModel> Execute(TDbParams parameters, bool rawDataOnly = false)
            => DbCommandText.DbConnection.ExecuteDbAction(this, parameters, rawDataOnly);
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync(TDbParams parameters, bool rawDataOnly = false)
            => await DbCommandText.DbConnection.ExecuteDbActionAsync(this, parameters, rawDataOnly);



        /// <summary>
        /// Called when [dispose].
        /// </summary>
        /// <param name="calledFromDispose">if set to <c>true</c> [called from dispose].</param>
        protected override void OnDispose(bool calledFromDispose)
        {
        }

        /// <summary>
        /// Called when [dispose complete].
        /// </summary>
        protected override void OnDisposeComplete()
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    internal class ParameterlessDbCommandMethod<TDbDataModel> : DbCommandMethod<object, TDbDataModel>, IParameterlessDbCommandMethod<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterlessDbCommandMethod{TDbDataModel}" /> class.
        /// </summary>
        public ParameterlessDbCommandMethod():base() { }

        /// <summary>
        /// Creates the specified database command.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        internal static ParameterlessDbCommandMethod<TDbDataModel> Create(
            DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<object>> parametersInitializer = null)
        {
            DbCommandConfigParams<object> dbParmas = DbCommandConfigParams<object>.Create(parametersInitializer);
            return new ParameterlessDbCommandMethod<TDbDataModel>()
            {
                DbCommandText = dbCommand,
                DbParams = DbCommandConfigParams<object>.Create(parametersInitializer),
                ParamsValidator = ValidatorFactory.Create((Action<IValidator<object>>)null)
            };
        }

        #region Async Methods

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        internal static async Task<ParameterlessDbCommandMethod<TDbDataModel>> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<object>, Task> parametersInitializer = null
            )
        {
            DbCommandConfigParams<object> dbParams = await DbCommandConfigParams<object>.CreateAsync(parametersInitializer);
            ParameterlessDbCommandMethod<TDbDataModel> config = new ParameterlessDbCommandMethod<TDbDataModel>()
            {
                DbCommandText = dbCommand,
                DbParams = await DbCommandConfigParams<object>.CreateAsync(parametersInitializer),
                ParamsValidator = ValidatorFactory.Create((Action<IValidator<object>>)null)
            };
            await config.InitAsync();
            return config;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        public IDbResponse<TDbDataModel> Execute(bool rawDataOnly = false) => Execute(null, rawDataOnly);
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        public async Task<IDbResponse<TDbDataModel>> ExecuteAsync(bool rawDataOnly = false) => await ExecuteAsync(null, rawDataOnly);

        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal class DbCommandMethod<TDbParams> : DbCommandMethod<TDbParams, DbDataModel>, IDbCommandMethod<TDbParams>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandMethod{TDbParams}" /> class.
        /// </summary>
        public DbCommandMethod():base() { }

        /// <summary>
        /// Creates the specified database command.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        internal static new DbCommandMethod<TDbParams> Create(
               DbCommandSettingsBase dbCommand,
                Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
                Action<IValidator<TDbParams>> validatorInitializer = null
                )
            => Create(dbCommand, parametersInitializer, validatorInitializer, p => { });


        /// <summary>
        /// Creates the specified database command.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeActions">The on before actions.</param>
        /// <returns></returns>
        internal new static DbCommandMethod<TDbParams> Create(
           DbCommandSettingsBase dbCommand,
            Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
            Action<IValidator<TDbParams>> validatorInitializer,
            params Action<TDbParams>[] onBeforeActions
            )
        {
            DbCommandConfigParams<TDbParams> dbParams = DbCommandConfigParams<TDbParams>.Create(parametersInitializer);
            IValidator<TDbParams> validator = ValidatorFactory.Create(validatorInitializer);
            return new DbCommandMethod<TDbParams>()
            {
                DbCommandText = dbCommand,
                DbParams = dbParams,
                ParamsValidator = validator,
                HasValidation = dbParams.Count > 0 && validator.Count > 0,
                OnBeforeActions = onBeforeActions
            };
        }


        #region Async Methods

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        internal static new async Task<DbCommandMethod<TDbParams>> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
            Func<IValidator<TDbParams>, Task> validatorInitializer = null
            )
        => await CreateAsync(dbCommand, parametersInitializer, validatorInitializer, async p => { await Task.CompletedTask; });
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeAsyncActions">The on before asynchronous actions.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Executes the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        public new IDbResponse Execute(TDbParams parameters, bool rawDataOnly = false) => (IDbResponse)base.Execute(parameters, rawDataOnly);
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        public new async Task<IDbResponse> ExecuteAsync(TDbParams parameters, bool rawDataOnly = false) => (IDbResponse)await base.ExecuteAsync(parameters, rawDataOnly);

    }
    /// <summary>
    /// 
    /// </summary>
    internal class DbCommandMethod : DbCommandMethod<object, DbDataModel>, IDbCommandMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandMethod" /> class.
        /// </summary>
        public DbCommandMethod():base() { }

        /// <summary>
        /// Creates the specified database command.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="validator">The validator.</param>
        /// <returns></returns>
        internal static DbCommandMethod Create(
            DbCommandSettingsBase dbCommand,
            IDbCommandConfigParams<object> dbParams,
            IValidator<object> validator)
        {
            return new DbCommandMethod()
            {
                DbCommandText = dbCommand,
                DbParams = dbParams,
                ParamsValidator = validator,
                HasValidation = dbParams.Count > 0 && validator.Count > 0,
                OnBeforeActions = new Action<object>[0]
            };
        }
        /// <summary>
        /// Creates the specified database command.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        internal static DbCommandMethod Create(
               DbCommandSettingsBase dbCommand,
                Action<IDbCommandConfigParams<object>> parametersInitializer = null
                )
        {
            DbCommandConfigParams<object> dbParams = DbCommandConfigParams<object>.Create(parametersInitializer);
            IValidator<object> validator = ValidatorFactory.Create((Action<IValidator<object>>)null);
            var config = Create(dbCommand, dbParams, validator);

            return config;
        }

        #region Async Methods

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        internal static async Task<DbCommandMethod> CreateAsync(
           DbCommandSettingsBase dbCommand,
            Func<IDbCommandConfigParams<object>, Task> parametersInitializer = null
            )
        {
            DbCommandConfigParams<object> dbParams = await DbCommandConfigParams<object>.CreateAsync(parametersInitializer);
            IValidator<object> validator = await ValidatorFactory.CreateAsync((Func<IValidator<object>, Task>)null);
            DbCommandMethod config = new DbCommandMethod();
            await config.InitAsync(dbCommand, dbParams, validator, new Func<object, Task>[0]);
            return config;
        }
        #endregion
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        public IDbResponse Execute() => (IDbResponse)Execute(null, true);
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IDbResponse> ExecuteAsync() => (IDbResponse)await ExecuteAsync(null, true);


    }
}