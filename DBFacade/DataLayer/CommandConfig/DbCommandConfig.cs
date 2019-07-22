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
    internal class DbCommandConfig<TDbParams, TConnection> : DbCommandConfigBase, IDbCommandConfig
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
        public string ReturnParam{ get; private set; }
        public bool IsOutput { get; private set; }
        /// <summary>
        /// Gets or sets the type of the database command.
        /// </summary>
        /// <value>
        /// The type of the database command.
        /// </value>
        private CommandType DbCommandType { get; set; }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommand, DefaultConfigParams, ParamsValidatorEmpty, CommandType.StoredProcedure, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommand, DefaultConfigParams, ParamsValidatorEmpty, dbCommandType, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommand, dbParams, ParamsValidatorEmpty, CommandType.StoredProcedure, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, CommandType dbCommandType, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommand, dbParams, ParamsValidatorEmpty, dbCommandType, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommand, dbParams, ParamsValidatorEmpty, CommandType.StoredProcedure, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, CommandType dbCommandType, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommand, dbParams, ParamsValidatorEmpty, dbCommandType, returnParam, isOutput, isTransaction);
        }        
        
        private void Init(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, CommandType dbCommandType = CommandType.StoredProcedure, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            ReturnParam = returnParam;
            DbCommand = dbCommand;
            DbCommandType = dbCommandType;
            DbParams = dbParams;
            ParamsValidator = validator;
            IsOutput = isOutput;
            Transaction = isTransaction;
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
            return !string.IsNullOrEmpty(ReturnParam);
        }
        private string GetFullParamName(string name)
        {
            return "@" + name;
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
                return dbCommand.Parameters[GetFullParamName(ReturnParam)].Value;
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
                dbCommand.Parameters[GetFullParamName(ReturnParam)].Value = value;
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

                string paramKey = GetFullParamName(config.Key);

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
                if (IsOutput)
                {
                    dbParameter.Direction = ParameterDirection.Output;
                }
                dbParameter.ParameterName = GetFullParamName(ReturnParam);
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
