using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using System.Linq;

namespace DBFacade.DataLayer.CommandConfig
{
    internal class DbCommandConfig<TDbParams, TDbConnectionConfig> : SafeDisposableBase, IDbCommandConfigInternal
        where TDbParams : IDbParamsModel
        where TDbConnectionConfig : IDbConnectionConfig
    {
        internal static DbCommandConfig<TDbParams, TDbConnectionConfig> FetchConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null)
        => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbCommandType, dbParams, null, false, returnParam);
        internal static DbCommandConfig<TDbParams, TDbConnectionConfig> FetchConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null)
        => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbCommandType, dbParams, validator, false, returnParam);

        internal static DbCommandConfig<TDbParams, TDbConnectionConfig> TransactionConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null)
        => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbCommandType, dbParams, null, true, returnParam);
        internal static DbCommandConfig<TDbParams, TDbConnectionConfig> TransactionConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null)
        => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbCommandType, dbParams, validator, true, returnParam);

        private DbCommandConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
           IDbCommandConfigParams<TDbParams> dbParams = null, Validator<TDbParams> validator = null,
           bool isTransaction = false, string returnParam = null)
        {
            DbCommandTextPrivate = dbCommandText;
            DbCommandType = dbCommandType;
            DbParams = dbParams ?? new DbCommandConfigParams<TDbParams>();
            ParamsValidator = validator ?? new Validator<TDbParams>();
            Transaction = isTransaction;
            ReturnParam = string.IsNullOrWhiteSpace(returnParam) ? ReturnParamDefault : returnParam;
        }
        private IDbCommandConfigParams<TDbParams> DbParams { get; set; }
        private Validator<TDbParams> ParamsValidator { get; set; }
        private IDbCommandText<TDbConnectionConfig> DbCommandTextPrivate { get; set; }
        private CommandType DbCommandType { get; set; }
        private const string ReturnParamDefault = "DbFacade_DbCallReturn";
        private string ReturnParam { get; set; }
        private bool Transaction { get; set; }

        public IDbConnectionConfigInternal DbConnectionConfig =>
            DbConnectionConfigManager.Resolve<TDbConnectionConfig>();

        public async Task<IDbConnectionConfigInternal> GetDbConnectionConfigAsync()
        {
            return await DbConnectionConfigManager.ResolveAsync<TDbConnectionConfig>();
        }

        public IDbCommandText DbCommandText => DbCommandTextPrivate;

        public TDbCommand GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(
            IDbParamsModel tDbMethodManifestMethodParams, TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            var dbCommand = dbConnection.CreateCommand() as TDbCommand;
            dbCommand = AddParams<TDbCommand, TDbParameter>(dbCommand, (TDbParams)tDbMethodManifestMethodParams);
            dbCommand.CommandText = DbCommandTextPrivate.CommandText;
            dbCommand.CommandType = DbCommandType;
            return dbCommand;
        }
        public int GetReturnValue<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
            => GetReturnValueParam(dbCommand) is DbParameter parameter && parameter.Value is int value? value : -1;
        
        private DbParameter GetReturnValueParam<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
        => dbCommand.Parameters.Cast<DbParameter>().Where(entry => entry.Direction == ParameterDirection.ReturnValue).FirstOrDefault();

        private IEnumerable<DbParameter> GetOutputParams<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
        => dbCommand.Parameters.Cast<DbParameter>().Where(entry => entry.Direction == ParameterDirection.Output);
        public IDictionary<string, object> GetOutputValues<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
        {
            Dictionary<string, object> outputValues = new Dictionary<string, object>();
            foreach (DbParameter outputParam in GetOutputParams(dbCommand))
            {
                string key = outputParam.ParameterName.Replace("@", string.Empty);
                outputValues.Add(key, dbCommand.Parameters[outputParam.ParameterName].Value);
            }
            return outputValues;
        }
        public IValidationResult Validate(IDbParamsModel paramsModel)
        {
            return DbParams != null && DbParams.Count > 0
                ? ParamsValidator.Validate((TDbParams) paramsModel)
                : ValidationResult.PassingValidation();
        }

        public async Task<IValidationResult> ValidateAsync(IDbParamsModel paramsModel)
        {
            return DbParams != null && DbParams.Count > 0
                ? await ParamsValidator.ValidateAsync((TDbParams) paramsModel)
                : ValidationResult.PassingValidation();
        }

        public bool IsTransaction => Transaction;

       
        private string GetFullParamName(string name) => $"@{name}";
        

        private TDbParameter CreateParameter<TDbCommand, TDbParameter>(TDbCommand dbCommand,
            ParameterDirection direction, string parameterName)
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            var parameter = dbCommand.CreateParameter() as TDbParameter;
            if (parameter != null)
            {
                parameter.Direction = direction;
                parameter.ParameterName = GetFullParamName(parameterName);
            }

            return parameter;
        }

        private TDbCommand AddParams<TDbCommand, TDbParameter>(TDbCommand dbCommand,
            TDbParams tDbMethodManifestMethodParams)
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            foreach (KeyValuePair<string, IDbCommandParameterConfig<TDbParams>> config in DbParams)
            {
                if (config.Value is IInternalDbCommandParameterConfig<TDbParams> paramConfig)
                {
                    var dbParameter = CreateParameter<TDbCommand, TDbParameter>(dbCommand,
                         paramConfig.IsOutput ? ParameterDirection.Output : ParameterDirection.Input, config.Key);
                    dbParameter.DbType = paramConfig.DbType;
                    dbParameter.IsNullable = paramConfig.IsNullable;
                    if (!paramConfig.IsOutput)
                    {
                        dbParameter.Value = paramConfig.Value(tDbMethodManifestMethodParams);
                    }
                    dbCommand.Parameters.Add(dbParameter);
                }
            }               
        
            /*Add Return param*/
            dbCommand.Parameters.Add(CreateParameter<TDbCommand, TDbParameter>(dbCommand,ParameterDirection.ReturnValue, ReturnParam));
            return dbCommand;
        }

        protected override void OnDispose(bool calledFromDispose)
        {
        }

        protected override void OnDisposeComplete()
        {
        }
    }
}