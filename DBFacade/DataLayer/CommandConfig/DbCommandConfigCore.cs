using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DBFacade.DataLayer.CommandConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    /// <typeparam name="TConnection">The type of the connection.</typeparam>
    /// <seealso cref="DbCommandConfigBase" />
    /// <seealso cref="IDbCommandConfig" />
    internal abstract class DbCommandConfigCore<TDbParams, TConnection> : DbCommandConfigBase, IDbCommandConfig
        where TDbParams : IDbParamsModel
        where TConnection : IDbConnectionConfig
    {
        /// <summary>
        /// Gets the database parameters.
        /// </summary>
        /// <value>
        /// The database parameters.
        /// </value>
        public IDbCommandConfigParams<TDbParams> DbParams { get; private set; }
        /// <summary>
        /// The default configuration parameters
        /// </summary>
        private DbCommandConfigParams<TDbParams> DefaultConfigParams = new DbCommandConfigParams<TDbParams>() { };
        /// <summary>
        /// Gets the database command.
        /// </summary>
        /// <value>
        /// The database command.
        /// </value>
        public IDbCommandText<TConnection> DbCommand { get; private set; }
        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <value>
        /// The return value.
        /// </value>
        public string ReturnValue { get; private set; }
        /// <summary>
        /// Gets or sets the type of the database command.
        /// </summary>
        /// <value>
        /// The type of the database command.
        /// </value>
        private CommandType DbCommandType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType)
        {
            Init(dbCommand, dbCommandType, DefaultConfigParams, null, ParamsValidatorEmpty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams)
        {
            Init(dbCommand, dbCommandType, dbParams, null, ParamsValidatorEmpty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="validator">The validator.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
        {
            Init(dbCommand, dbCommandType, dbParams, null, validator);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand)
        {
            Init(dbCommand, CommandType.StoredProcedure, DefaultConfigParams, null, ParamsValidatorEmpty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, null, ParamsValidatorEmpty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="validator">The validator.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, null, validator);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="returnValue">The return value.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, string returnValue)
        {
            Init(dbCommand, dbCommandType, DefaultConfigParams, returnValue, ParamsValidatorEmpty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnValue">The return value.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue)
        {
            Init(dbCommand, dbCommandType, dbParams, returnValue, ParamsValidatorEmpty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="validator">The validator.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator)
        {
            Init(dbCommand, dbCommandType, dbParams, returnValue, validator);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="returnValue">The return value.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, string returnValue)
        {
            Init(dbCommand, CommandType.StoredProcedure, DefaultConfigParams, returnValue, ParamsValidatorEmpty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnValue">The return value.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, string returnValue)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, returnValue, ParamsValidatorEmpty);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigCore{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="validator">The validator.</param>
        public DbCommandConfigCore(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator)
        {
            Init(dbCommand, CommandType.StoredProcedure, dbParams, returnValue, validator);
        }

        /// <summary>
        /// Initializes the specified database command.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="validator">The validator.</param>
        private void Init(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator)
        {
            ReturnValue = returnValue;
            DbCommand = dbCommand;
            DbCommandType = dbCommandType;
            DbParams = dbParams;
            ParamsValidator = validator;
        }
        /// <summary>
        /// Gets the database connection core.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <returns></returns>
        protected override Con GetDbConnectionCore<Con>()
        {
            return (Con)DbConnectionService.GetDbConnection<TConnection>().GetDbConnection();
        }
        /// <summary>
        /// Determines whether [has stored procedure core].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has stored procedure core]; otherwise, <c>false</c>.
        /// </returns>
        protected override bool HasStoredProcedureCore()
        {
            if (GetDBConnectionConfigCore().CheckStoredProcAvailability())
            {
                string[] storedProcNames = DbConnectionService.GetAvailableStoredProcedured<TConnection>().Select(sproc => sproc.Name).ToArray();
                return Array.IndexOf(storedProcNames, DbCommand.CommandText()) != -1;
            }
            return true;
        }
        /// <summary>
        /// Gets the missing stored procedure exception core.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected override MissingStoredProcedureException GetMissingStoredProcedureExceptionCore(string message)
        {
            return new MissingStoredProcedureException(message + DbCommand.Label());
        }
        /// <summary>
        /// Gets the SQL execution exception core.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        protected override SQLExecutionException GetSQLExecutionExceptionCore(string message, Exception e)
        {
            return new SQLExecutionException(message + DbCommand.Label(), e);
        }
        /// <summary>
        /// Gets the database connection configuration core.
        /// </summary>
        /// <returns></returns>
        protected override IDbConnectionConfig GetDBConnectionConfigCore()
        {
            return DbConnectionService.GetDbConnection<TConnection>();
        }
        /// <summary>
        /// Gets the database command core.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <typeparam name="Prm">The type of the rm.</typeparam>
        /// <param name="dbMethodParams">The database method parameters.</param>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        protected override Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
        {
            Cmd dbCommand = (Cmd)dbConnection.CreateCommand();
            dbCommand = AddParams<Cmd, Prm>(dbCommand, (TDbParams)dbMethodParams);
            dbCommand.CommandText = DbCommand.CommandText();
            dbCommand.CommandType = DbCommandType;
            return dbCommand;
        }
        /// <summary>
        /// Determines whether [has return value].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has return value]; otherwise, <c>false</c>.
        /// </returns>
        private bool HasReturnValue()
        {
            return !string.IsNullOrEmpty(ReturnValue);
        }

        /// <summary>
        /// Gets the return value core.
        /// </summary>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        protected override object GetReturnValueCore<Cmd>(Cmd dbCommand)
        {
            if (HasReturnValue())
            {
                return dbCommand.Parameters["@" + ReturnValue].Value;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Sets the return value core.
        /// </summary>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="value">The value.</param>
        protected override void SetReturnValueCore<Cmd>(Cmd dbCommand, object value)
        {
            if (HasReturnValue())
            {
                dbCommand.Parameters["@" + ReturnValue].Value = value;
            }
        }
        /// <summary>
        /// Adds the parameters.
        /// </summary>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <typeparam name="Prm">The type of the rm.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbMethodParams">The database method parameters.</param>
        /// <returns></returns>
        private Cmd AddParams<Cmd, Prm>(Cmd dbCommand, TDbParams dbMethodParams)
        where Cmd : DbCommand
        where Prm : DbParameter
        {

            foreach (KeyValuePair<string, DbCommandParameterConfig<TDbParams>> config in DbParams)
            {
                Prm dbParameter = (Prm)dbCommand.CreateParameter();

                string paramKey = "@" + config.Key;

                dbParameter.DbType = config.Value.DBType;
                dbParameter.Direction = ParameterDirection.Input;
                dbParameter.IsNullable = config.Value.IsNullable();
                dbParameter.ParameterName = paramKey;
                dbParameter.Value = config.Value.GetParam(dbMethodParams);
                dbCommand.Parameters.Add(dbParameter);

            }
            if (HasReturnValue())
            {
                Prm dbParameter = (Prm)dbCommand.CreateParameter();
                dbParameter.Direction = ParameterDirection.ReturnValue;
                dbParameter.ParameterName = "@" + ReturnValue;
                dbCommand.Parameters.Add(dbParameter);
            }
            return dbCommand;
        }

        /// <summary>
        /// Validates the core.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        protected override IValidationResult ValidateCore(IDbParamsModel paramsModel)
        {
            if (DbParams != null && DbParams.ParamsCount() > 0)
            {
                return ParamsValidator.Validate((TDbParams)paramsModel);
            }
            return ValidationResult.PassingValidation();
        }

        /// <summary>
        /// Gets or sets the parameters validator.
        /// </summary>
        /// <value>
        /// The parameters validator.
        /// </value>
        private Validator<TDbParams> ParamsValidator { get; set; }
        /// <summary>
        /// The parameters validator empty
        /// </summary>
        private static Validator<TDbParams> ParamsValidatorEmpty = new Validator<TDbParams>();

    }
}
