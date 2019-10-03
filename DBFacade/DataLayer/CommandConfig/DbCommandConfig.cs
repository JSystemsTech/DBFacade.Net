using DbFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.CommandConfig
{
    internal class DbCommandConfig<TDbParams, TDbConnectionConfig> : SafeDisposableBase, IDbCommandConfigInternal
        where TDbParams : IDbParamsModel
        where TDbConnectionConfig : IDbConnectionConfig
    {        
        private IDbCommandConfigParams<TDbParams> DbParams { get; set; }
        private Validator<TDbParams> ParamsValidator { get; set; }
        private IDbCommandText<TDbConnectionConfig> DbCommandTextPrivate { get; set; }
        private CommandType DbCommandType { get; set; }
        private string ReturnParam{ get; set; }
        private bool IsOutput { get; set; }        
        private bool Transaction { get; set; }


        
        internal DbCommandConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, dbCommandType, null, null, returnParam, isOutput, isTransaction);
        }
        internal DbCommandConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, dbCommandType, dbParams, null, returnParam, isOutput, isTransaction);
        }
        internal DbCommandConfig(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, dbCommandType, dbParams, validator, returnParam, isOutput, isTransaction);
        }        
        
        private void Init(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams=null, Validator<TDbParams> validator = null, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            ReturnParam = returnParam;
            DbCommandTextPrivate = dbCommandText;
            DbCommandType = dbCommandType;
            DbParams = dbParams != null ? dbParams : new DbCommandConfigParams<TDbParams>();
            ParamsValidator = validator != null ? validator : new Validator<TDbParams>();
            IsOutput = isOutput;
            Transaction = isTransaction;
        }

        public IDbConnectionConfigInternal DbConnectionConfig => DbConnectionConfigManager.Resolve<TDbConnectionConfig>();
        public async Task<IDbConnectionConfigInternal> GetDbConnectionConfigAsync()=> await DbConnectionConfigManager.ResolveAsync<TDbConnectionConfig>();
        public IDbCommandText DbCommandText => DbCommandTextPrivate;

        public TDbCommand GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(IDbParamsModel TDbMethodManifestMethodParams, TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            TDbCommand dbCommand = dbConnection.CreateCommand() as TDbCommand;
            dbCommand = AddParams<TDbCommand, TDbParameter>(dbCommand, (TDbParams)TDbMethodManifestMethodParams);
            dbCommand.CommandText = DbCommandTextPrivate.CommandText;
            dbCommand.CommandType = DbCommandType;
            return dbCommand;
        }        
        private bool HasReturnValue()
        {
            return !string.IsNullOrEmpty(ReturnParam);
        }
        private string GetFullParamName(string name)
        {
            return $"@{name}";
        }
        public object GetReturnValue<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand
            => HasReturnValue() ? dbCommand.Parameters[GetFullParamName(ReturnParam)].Value : null;
        private TDbParameter CreateParameter<TDbCommand, TDbParameter>(TDbCommand dbCommand, TDbParams TDbMethodManifestMethodParams, ParameterDirection direction, string parameterName)
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            TDbParameter parameter = dbCommand.CreateParameter() as TDbParameter;
            parameter.Direction = direction;
            parameter.ParameterName = GetFullParamName(parameterName);
            return parameter;
        }

        private TDbCommand AddParams<TDbCommand, TDbParameter>(TDbCommand dbCommand, TDbParams TDbMethodManifestMethodParams)
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {            
            foreach (KeyValuePair<string, IDbCommandParameterConfig<TDbParams>> config in DbParams)
            {
                IInternalDbCommandParameterConfig<TDbParams> paramConfig = config.Value as IInternalDbCommandParameterConfig<TDbParams>;
                TDbParameter dbParameter = CreateParameter<TDbCommand, TDbParameter>(dbCommand, TDbMethodManifestMethodParams, ParameterDirection.Input, config.Key);
                dbParameter.DbType = paramConfig.DbType;
                dbParameter.IsNullable = paramConfig.IsNullable;
                dbParameter.Value = paramConfig.Value(TDbMethodManifestMethodParams);
                dbCommand.Parameters.Add(dbParameter);
            }
            if (HasReturnValue())
            {
                dbCommand.Parameters.Add(CreateParameter<TDbCommand, TDbParameter>(dbCommand, TDbMethodManifestMethodParams, IsOutput ? ParameterDirection.Output : ParameterDirection.ReturnValue, ReturnParam));
            }
            return dbCommand;
        }

        public IValidationResult Validate(IDbParamsModel paramsModel)
            => (DbParams != null && DbParams.Count > 0) ?
                ParamsValidator.Validate((TDbParams)paramsModel) :
                ValidationResult.PassingValidation();
        public async Task<IValidationResult> ValidateAsync(IDbParamsModel paramsModel)
            => (DbParams != null && DbParams.Count > 0) ?
                await ParamsValidator.ValidateAsync((TDbParams)paramsModel) :
                ValidationResult.PassingValidation();

        public bool IsTransaction => Transaction;

        protected override void OnDispose(bool calledFromDispose) { }

        protected override void OnDisposeComplete() { }
    }
}
