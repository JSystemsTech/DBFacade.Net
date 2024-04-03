using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.Utils;
using System;
using System.Data;

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
        /// Creates the parameterless method.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        IParameterlessDbCommandMethod<TDbDataModel> CreateParameterlessMethod<TDbDataModel>(Action<IDbCommandConfigParams<object>> parametersInitializer = null)
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

    }
    public interface IDbCommandSettings
    {
        /// <summary>Gets the command identifier.</summary>
        /// <value>The command identifier.</value>
        Guid CommandId { get; }
        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>
        /// The command text.
        /// </value>
        string CommandText { get; }
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        string Label { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    internal abstract class DbCommandSettingsBase : IDbCommandSettings
    {
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
        /// <summary>Gets or sets the database connection.</summary>
        /// <value>The database connection.</value>
        public IDbConnectionCore DbConnection { get; protected set; }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    internal class DbCommandSettings : DbCommandSettingsBase, IDbCommandConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommand{TDbConnectionConfig}"/> class.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="isTransaction">if set to <c>true</c> [is transaction].</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        private DbCommandSettings(
            string commandText,
            string label,
            CommandType commandType,
            bool isTransaction,
            bool requiresValidation,
            IDbConnectionCore dbConnection)
        {
            CommandId = Guid.NewGuid();
            CommandText = commandText;
            Label = label;
            CommandType = commandType;
            IsTransaction = isTransaction;
            RequiresValidation = requiresValidation;
            DbConnection = dbConnection;
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
        public static DbCommandSettings Create(
            IDbConnectionCore dbConnection,
            string commandText,
            string label,
            CommandType commandType = CommandType.StoredProcedure,
            bool isTransaction = false,
            bool requiresValidation = false) => new DbCommandSettings(commandText, label, commandType, isTransaction, requiresValidation, dbConnection);

        /// <summary>
        /// Creates the method.
        /// </summary>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        public IDbCommandMethod CreateMethod(Action<IDbCommandConfigParams<object>> parametersInitializer = null)
        => DbCommandMethod.Create(this, parametersInitializer);

        /// <summary>
        /// Creates the parameterless method.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
        public IParameterlessDbCommandMethod<TDbDataModel> CreateParameterlessMethod<TDbDataModel>(Action<IDbCommandConfigParams<object>> parametersInitializer = null)
            where TDbDataModel : DbDataModel
        => ParameterlessDbCommandMethod<TDbDataModel>.Create(this, parametersInitializer);


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
        => DbCommandMethod<TDbParams>.Create(this, parametersInitializer, validatorInitializer);

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
        => DbCommandMethod<TDbParams>.Create(this, parametersInitializer, validatorInitializer, onBeforeActions);


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
        => DbCommandMethod<TDbParams, TDbDataModel>.Create(this, parametersInitializer, validatorInitializer);

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
        => DbCommandMethod<TDbParams, TDbDataModel>.Create(this, parametersInitializer, validatorInitializer, onBeforeActions);

    }
}