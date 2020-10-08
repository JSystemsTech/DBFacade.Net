using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using System.Linq;

namespace DbFacade.DataLayer.CommandConfig
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

        #region Async Methods



        private DbCommandConfig() { }
        internal static async Task<DbCommandConfig<TDbParams, TDbConnectionConfig>> FetchConfigAsync(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null)
        => await CreateAsync(dbCommandText, dbCommandType, dbParams, null, false, returnParam);
        internal static async Task<DbCommandConfig<TDbParams, TDbConnectionConfig>> FetchConfigAsync(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null)
        => await CreateAsync(dbCommandText, dbCommandType, dbParams, validator, false, returnParam);

        internal static async Task<DbCommandConfig<TDbParams, TDbConnectionConfig>> TransactionConfigAsync(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null)
        => await CreateAsync(dbCommandText, dbCommandType, dbParams, null, true, returnParam);
        internal static async Task<DbCommandConfig<TDbParams, TDbConnectionConfig>> TransactionConfigAsync(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null)
        => await CreateAsync(dbCommandText, dbCommandType, dbParams, validator, true, returnParam);

        private static async Task<DbCommandConfig<TDbParams, TDbConnectionConfig>> CreateAsync(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
           IDbCommandConfigParams<TDbParams> dbParams = null, Validator<TDbParams> validator = null,
           bool isTransaction = false, string returnParam = null)
        {
            DbCommandConfig<TDbParams, TDbConnectionConfig> config = new DbCommandConfig<TDbParams, TDbConnectionConfig>();
            await config.InitAsync(dbCommandText, dbCommandType, dbParams, validator, isTransaction, returnParam);
            return config;
        }
        private async Task InitAsync(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
           IDbCommandConfigParams<TDbParams> dbParams = null, Validator<TDbParams> validator = null,
           bool isTransaction = false, string returnParam = null)
        {
            DbCommandTextPrivate = dbCommandText;
            DbCommandType = dbCommandType;
            DbParams = dbParams ?? new DbCommandConfigParams<TDbParams>();
            ParamsValidator = validator ?? new Validator<TDbParams>();
            Transaction = isTransaction;
            ReturnParam = string.IsNullOrWhiteSpace(returnParam) ? ReturnParamDefault : returnParam;
            await Task.CompletedTask;
        }
        #endregion

        
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
        public async Task<TDbCommand> GetDbCommandAsync<TDbConnection, TDbCommand, TDbParameter>(
            IDbParamsModel tDbMethodManifestMethodParams, TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            var dbCommand = dbConnection.CreateCommand() as TDbCommand;
            dbCommand = await AddParamsAsync<TDbCommand, TDbParameter>(dbCommand, (TDbParams)tDbMethodManifestMethodParams);
            dbCommand.CommandText = DbCommandTextPrivate.CommandText;
            dbCommand.CommandType = DbCommandType;
            await Task.CompletedTask;
            return dbCommand;
        }
        public int GetReturnValue<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
            => GetReturnValueParam(dbCommand) is DbParameter parameter && parameter.Value is int value? value : -1;
        
        private DbParameter GetReturnValueParam<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
        => dbCommand.Parameters.Cast<DbParameter>().Where(entry => entry.Direction == ParameterDirection.ReturnValue).FirstOrDefault();

        public async Task<int> GetReturnValueAsync<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
            => await GetReturnValueParamAsync(dbCommand) is DbParameter parameter && parameter.Value is int value ? value : -1;

        private async Task<DbParameter> GetReturnValueParamAsync<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
        {
            DbParameter parameter = dbCommand.Parameters.Cast<DbParameter>().Where(entry => entry.Direction == ParameterDirection.ReturnValue).FirstOrDefault();
            await Task.CompletedTask;
            return parameter;
        }

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
        private async Task<IEnumerable<DbParameter>> GetOutputParamsAsync<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
        {
            IEnumerable<DbParameter>  parameters = dbCommand.Parameters.Cast<DbParameter>().Where(entry => entry.Direction == ParameterDirection.Output);
            await Task.CompletedTask;
            return parameters;
        }
        
        public async Task<IDictionary<string, object>> GetOutputValuesAsync<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
        {
            Dictionary<string, object> outputValues = new Dictionary<string, object>();
            foreach (DbParameter outputParam in await GetOutputParamsAsync(dbCommand))
            {
                string key = outputParam.ParameterName.Replace("@", string.Empty);
                outputValues.Add(key, dbCommand.Parameters[outputParam.ParameterName].Value);
            }
            await Task.CompletedTask;
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
        private async Task<TDbParameter> CreateParameterAsync<TDbCommand, TDbParameter>(TDbCommand dbCommand,
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
            await Task.CompletedTask;
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
                    if (paramConfig.IsOutput)
                    {
                        dbParameter.Size = paramConfig.OutputSize;
                    }
                    else
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
        private async Task<TDbCommand> AddParamsAsync<TDbCommand, TDbParameter>(TDbCommand dbCommand,
            TDbParams tDbMethodManifestMethodParams)
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            foreach (KeyValuePair<string, IDbCommandParameterConfig<TDbParams>> config in DbParams)
            {
                if (config.Value is IInternalDbCommandParameterConfig<TDbParams> paramConfig)
                {
                    var dbParameter = await CreateParameterAsync<TDbCommand, TDbParameter>(dbCommand,
                         paramConfig.IsOutput ? ParameterDirection.Output : ParameterDirection.Input, config.Key);
                    dbParameter.DbType = paramConfig.DbType;
                    dbParameter.IsNullable = paramConfig.IsNullable;
                    if (paramConfig.IsOutput)
                    {
                        dbParameter.Size = paramConfig.OutputSize;
                    }
                    else
                    {
                        dbParameter.Value = await paramConfig.ValueAsync(tDbMethodManifestMethodParams);
                    }
                    dbCommand.Parameters.Add(dbParameter);
                }
            }

            /*Add Return param*/
            dbCommand.Parameters.Add(CreateParameter<TDbCommand, TDbParameter>(dbCommand, ParameterDirection.ReturnValue, ReturnParam));
            await Task.CompletedTask;
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