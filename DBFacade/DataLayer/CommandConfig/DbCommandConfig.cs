using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;

namespace DBFacade.DataLayer.CommandConfig
{
    internal class DbCommandConfig<TDbParams, TDbConnectionConfig> : SafeDisposableBase, IDbCommandConfigInternal
        where TDbParams : IDbParamsModel
        where TDbConnectionConfig : IDbConnectionConfig
    {
        internal DbCommandConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, dbCommandType, null, null, returnParam, isOutput, isTransaction);
        }

        internal DbCommandConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false,
            bool isTransaction = false)
        {
            Init(dbCommandText, dbCommandType, dbParams, null, returnParam, isOutput, isTransaction);
        }

        internal DbCommandConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null,
            bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, dbCommandType, dbParams, validator, returnParam, isOutput, isTransaction);
        }

        private IDbCommandConfigParams<TDbParams> DbParams { get; set; }
        private Validator<TDbParams> ParamsValidator { get; set; }
        private IDbCommandText<TDbConnectionConfig> DbCommandTextPrivate { get; set; }
        private CommandType DbCommandType { get; set; }
        private string ReturnParam { get; set; }
        private bool IsOutput { get; set; }
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

        public object GetReturnValue<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
        {
            return HasReturnValue() ? dbCommand.Parameters[GetFullParamName(ReturnParam)].Value : null;
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

        private void Init(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams = null, Validator<TDbParams> validator = null,
            string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            ReturnParam = returnParam;
            DbCommandTextPrivate = dbCommandText;
            DbCommandType = dbCommandType;
            DbParams = dbParams ?? new DbCommandConfigParams<TDbParams>();
            ParamsValidator = validator ?? new Validator<TDbParams>();
            IsOutput = isOutput;
            Transaction = isTransaction;
        }

        private bool HasReturnValue()
        {
            return !string.IsNullOrEmpty(ReturnParam);
        }

        private string GetFullParamName(string name)
        {
            return $"@{name}";
        }

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
                if (config.Value is IInternalDbCommandParameterConfig<TDbParams> paramConfig)
                {
                    var dbParameter = CreateParameter<TDbCommand, TDbParameter>(dbCommand,
                         ParameterDirection.Input, config.Key);
                    dbParameter.DbType = paramConfig.DbType;
                    dbParameter.IsNullable = paramConfig.IsNullable;
                    dbParameter.Value = paramConfig.Value(tDbMethodManifestMethodParams);
                    dbCommand.Parameters.Add(dbParameter);
                }

            if (HasReturnValue())
                dbCommand.Parameters.Add(CreateParameter<TDbCommand, TDbParameter>(dbCommand,
                    
                    IsOutput ? ParameterDirection.Output : ParameterDirection.ReturnValue, ReturnParam));
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