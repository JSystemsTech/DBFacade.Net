using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbCommandConfig
    {
        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        /// <value>
        /// The command identifier.
        /// </value>
        Guid CommandId { get; }
        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        IDbCommandMethod CreateMethod(Action<IDbCommandConfigParams<object>> parametersInitializer = null);
        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        Task<IDbCommandMethod> CreateMethodAsync(Func<IDbCommandConfigParams<object>, Task> parametersInitializer = null);



        /// <summary>
        /// Creates the parameterless method.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        IParameterlessDbCommandMethod<TDbDataModel> CreateParameterlessMethod<TDbDataModel>(Action<IDbCommandConfigParams<object>> parametersInitializer = null)
            where TDbDataModel : DbDataModel;
        /// <summary>
        /// Creates the parameterless method asynchronous.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        Task<IParameterlessDbCommandMethod<TDbDataModel>> CreateParameterlessMethodAsync<TDbDataModel>(Func<IDbCommandConfigParams<object>, Task> parametersInitializer = null)
            where TDbDataModel : DbDataModel;

        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        IDbCommandMethod<TDbParams> CreateMethod<TDbParams>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
        Action<IValidator<TDbParams>> validatorInitializer = null);

        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeActions">The on before actions.</param>
        /// <returns></returns>
        IDbCommandMethod<TDbParams> CreateMethod<TDbParams>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
        Action<IValidator<TDbParams>> validatorInitializer,
        params Action<TDbParams>[] onBeforeActions);


        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        IDbCommandMethod<TDbParams, TDbDataModel> CreateMethod<TDbParams, TDbDataModel>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
        Action<IValidator<TDbParams>> validatorInitializer = null)
            where TDbDataModel : DbDataModel;

        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeActions">The on before actions.</param>
        /// <returns></returns>
        IDbCommandMethod<TDbParams, TDbDataModel> CreateMethod<TDbParams, TDbDataModel>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
        Action<IValidator<TDbParams>> validatorInitializer,
        params Action<TDbParams>[] onBeforeActions)
         where TDbDataModel : DbDataModel;



        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        Task<IDbCommandMethod<TDbParams>> CreateMethodAsync<TDbParams>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
        Func<IValidator<TDbParams>, Task> validatorInitializer = null);

        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeAsyncActions">The on before asynchronous actions.</param>
        /// <returns></returns>
        Task<IDbCommandMethod<TDbParams>> CreateMethodAsync<TDbParams>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
        Func<IValidator<TDbParams>, Task> validatorInitializer,
        params Func<TDbParams, Task>[] onBeforeAsyncActions);

        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        Task<IDbCommandMethod<TDbParams, TDbDataModel>> CreateMethodAsync<TDbParams, TDbDataModel>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
        Func<IValidator<TDbParams>, Task> validatorInitializer = null)
            where TDbDataModel : DbDataModel;

        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeAsyncActions">The on before asynchronous actions.</param>
        /// <returns></returns>
        Task<IDbCommandMethod<TDbParams, TDbDataModel>> CreateMethodAsync<TDbParams, TDbDataModel>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
        Func<IValidator<TDbParams>, Task> validatorInitializer,
        params Func<TDbParams, Task>[] onBeforeAsyncActions)
            where TDbDataModel : DbDataModel;

    }
    /// <summary>
    /// 
    /// </summary>
    internal abstract class DbCommandSettingsBase{
        /// <summary>
        /// Gets or sets the command identifier.
        /// </summary>
        /// <value>
        /// The command identifier.
        /// </value>
        public Guid CommandId { get; protected set; }
        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>
        /// The command text.
        /// </value>
        public string CommandText { get; protected set; }
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; protected set; }
        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>
        /// The type of the command.
        /// </value>
        public CommandType CommandType { get; protected set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is transaction.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is transaction; otherwise, <c>false</c>.
        /// </value>
        public bool IsTransaction { get; protected set; }
        /// <summary>
        /// Gets or sets a value indicating whether [requires validation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [requires validation]; otherwise, <c>false</c>.
        /// </value>
        public bool RequiresValidation { get; protected set; }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    internal class DbCommand<TDbConnectionConfig> : DbCommandSettingsBase, IDbCommandConfig
        where TDbConnectionConfig : DbConnectionConfigBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommand{TDbConnectionConfig}"/> class.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="isTransaction">if set to <c>true</c> [is transaction].</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        private DbCommand(
            string commandText,
            string label,
            CommandType commandType = CommandType.StoredProcedure,
            bool isTransaction = false,
            bool requiresValidation = false)
        {
            CommandId = Guid.NewGuid();
            CommandText = commandText;
            Label = label;
            CommandType = commandType;
            IsTransaction = isTransaction;
            RequiresValidation = requiresValidation;
        }

        /// <summary>
        /// Creates the specified command text.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="isTransaction">if set to <c>true</c> [is transaction].</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        public static DbCommand<TDbConnectionConfig> Create(
            string commandText,
            string label,
            CommandType commandType = CommandType.StoredProcedure,
            bool isTransaction = false,
            bool requiresValidation = false) => new DbCommand<TDbConnectionConfig>(commandText, label, commandType, isTransaction, requiresValidation);
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="isTransaction">if set to <c>true</c> [is transaction].</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        public static async Task<DbCommand<TDbConnectionConfig>> CreateAsync(
            string commandText,
            string label,
            CommandType commandType = CommandType.StoredProcedure,
            bool isTransaction = false,
            bool requiresValidation = false)
        {
            DbCommand<TDbConnectionConfig> command = new DbCommand<TDbConnectionConfig>(commandText, label, commandType, isTransaction, requiresValidation);
            await Task.CompletedTask;
            return command;
        }
        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        public IDbCommandMethod CreateMethod(Action<IDbCommandConfigParams<object>> parametersInitializer = null)
        => DbCommandMethod.Create<TDbConnectionConfig>(this, parametersInitializer);

        /// <summary>
        /// Creates the parameterless method.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        public IParameterlessDbCommandMethod<TDbDataModel> CreateParameterlessMethod<TDbDataModel>(Action<IDbCommandConfigParams<object>> parametersInitializer = null)
            where TDbDataModel : DbDataModel
        => ParameterlessDbCommandMethod<TDbDataModel>.Create<TDbConnectionConfig>(this, parametersInitializer);


        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        public IDbCommandMethod<TDbParams> CreateMethod<TDbParams>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
        Action<IValidator<TDbParams>> validatorInitializer = null)
        => DbCommandMethod<TDbParams>.Create<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer);

        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeActions">The on before actions.</param>
        /// <returns></returns>
        public IDbCommandMethod<TDbParams> CreateMethod<TDbParams>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
        Action<IValidator<TDbParams>> validatorInitializer,
        params Action<TDbParams>[] onBeforeActions)
        => DbCommandMethod<TDbParams>.Create<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer, onBeforeActions);


        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        public IDbCommandMethod<TDbParams, TDbDataModel> CreateMethod<TDbParams, TDbDataModel>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null,
        Action<IValidator<TDbParams>> validatorInitializer = null)
            where TDbDataModel : DbDataModel
        => DbCommandMethod<TDbParams, TDbDataModel>.Create<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer);

        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeActions">The on before actions.</param>
        /// <returns></returns>
        public IDbCommandMethod<TDbParams, TDbDataModel> CreateMethod<TDbParams, TDbDataModel>(
         Action<IDbCommandConfigParams<TDbParams>> parametersInitializer,
        Action<IValidator<TDbParams>> validatorInitializer,
        params Action<TDbParams>[] onBeforeActions)
         where TDbDataModel : DbDataModel
        => DbCommandMethod<TDbParams, TDbDataModel>.Create<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer, onBeforeActions);


        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        public async Task<IDbCommandMethod> CreateMethodAsync(Func<IDbCommandConfigParams<object>, Task> parametersInitializer = null)
        => await DbCommandMethod.CreateAsync<TDbConnectionConfig>(this, parametersInitializer);

        /// <summary>
        /// Creates the parameterless method asynchronous.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        public async Task<IParameterlessDbCommandMethod<TDbDataModel>> CreateParameterlessMethodAsync<TDbDataModel>(Func<IDbCommandConfigParams<object>, Task> parametersInitializer = null)
            where TDbDataModel : DbDataModel
        => await ParameterlessDbCommandMethod<TDbDataModel>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer);
        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        public async Task<IDbCommandMethod<TDbParams>> CreateMethodAsync<TDbParams>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
        Func<IValidator<TDbParams>, Task> validatorInitializer = null)
        => await DbCommandMethod<TDbParams>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer);

        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeAsyncActions">The on before asynchronous actions.</param>
        /// <returns></returns>
        public async Task<IDbCommandMethod<TDbParams>> CreateMethodAsync<TDbParams>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
        Func<IValidator<TDbParams>, Task> validatorInitializer,
        params Func<TDbParams,Task>[] onBeforeAsyncActions)
        => await DbCommandMethod<TDbParams>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer, onBeforeAsyncActions);

        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        public async Task<IDbCommandMethod<TDbParams, TDbDataModel>> CreateMethodAsync<TDbParams, TDbDataModel>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null,
        Func<IValidator<TDbParams>, Task> validatorInitializer = null)
            where TDbDataModel : DbDataModel
        => await DbCommandMethod<TDbParams, TDbDataModel>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer);

        /// <summary>
        /// Creates the method asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <param name="onBeforeAsyncActions">The on before asynchronous actions.</param>
        /// <returns></returns>
        public async Task<IDbCommandMethod<TDbParams, TDbDataModel>> CreateMethodAsync<TDbParams, TDbDataModel>(
            Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer,
        Func<IValidator<TDbParams>, Task> validatorInitializer,
        params Func<TDbParams, Task>[] onBeforeAsyncActions)
            where TDbDataModel : DbDataModel
        => await DbCommandMethod<TDbParams, TDbDataModel>.CreateAsync<TDbConnectionConfig>(this, parametersInitializer, validatorInitializer, onBeforeAsyncActions);

    }
}