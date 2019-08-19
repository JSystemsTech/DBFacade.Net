using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.Services;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DBFacade.DataLayer.CommandConfig
{
    internal class DbCommandConfig<TDbParams, TConnection>: IDbCommandConfig
        where TDbParams : IDbParamsModel
        where TConnection : IDbConnectionConfig
    {        
        private IDbCommandConfigParams<TDbParams> DbParams { get; set; }
        private Validator<TDbParams> ParamsValidator { get; set; }
        private IDbCommandText<TConnection> DbCommandText { get; set; }
        private CommandType DbCommandType { get; set; }
        private string ReturnParam{ get; set; }
        private bool IsOutput { get; set; }        
        private bool Transaction { get; set; }
        private bool Disposed { get; set; }

        public DbCommandConfig(IDbCommandText<TConnection> dbCommandText, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, null, null, CommandType.StoredProcedure, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommandText, CommandType dbCommandType, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, null, null, dbCommandType, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, dbParams, null, CommandType.StoredProcedure, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, CommandType dbCommandType, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, dbParams, null, dbCommandType, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, dbParams, validator, CommandType.StoredProcedure, returnParam, isOutput, isTransaction);
        }
        public DbCommandConfig(IDbCommandText<TConnection> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, CommandType dbCommandType, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            Init(dbCommandText, dbParams, validator, dbCommandType, returnParam, isOutput, isTransaction);
        }        
        
        private void Init(IDbCommandText<TConnection> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, CommandType dbCommandType = CommandType.StoredProcedure, string returnParam = null, bool isOutput = false, bool isTransaction = false)
        {
            ReturnParam = returnParam;
            DbCommandText = dbCommandText;
            DbCommandType = dbCommandType;
            DbParams = dbParams != null ? dbParams : new DbCommandConfigParams<TDbParams>();
            ParamsValidator = validator != null ? validator : new Validator<TDbParams>();
            IsOutput = isOutput;
            Transaction = isTransaction;
        }
        
        public IDbConnectionConfig GetDBConnectionConfig() => InstanceResolvers.Get<IDbConnectionConfig>().Get<TConnection>();
        public IDbCommandText GetDbCommandText() => DbCommandText;

        public TDbCommand GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(IDbParamsModel dbMethodParams, TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            TDbCommand dbCommand = dbConnection.CreateCommand() as TDbCommand;
            dbCommand = AddParams<TDbCommand, TDbParameter>(dbCommand, (TDbParams)dbMethodParams);
            dbCommand.CommandText = DbCommandText.CommandText();
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
        private TDbParameter CreateParameter<TDbCommand, TDbParameter>(TDbCommand dbCommand, TDbParams dbMethodParams, ParameterDirection direction, string parameterName)
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {
            TDbParameter parameter = dbCommand.CreateParameter() as TDbParameter;
            parameter.Direction = direction;
            parameter.ParameterName = GetFullParamName(parameterName);
            return parameter;
        }
        private TDbCommand AddParams<TDbCommand, TDbParameter>(TDbCommand dbCommand, TDbParams dbMethodParams)
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
        {            
            foreach (KeyValuePair<string, DbCommandParameterConfig<TDbParams>> config in DbParams)
            {
                TDbParameter dbParameter = CreateParameter<TDbCommand, TDbParameter>(dbCommand, dbMethodParams, ParameterDirection.Input, config.Key);
                dbParameter.DbType = config.Value.DBType;
                dbParameter.IsNullable = config.Value.IsNullable();
                dbParameter.Value = config.Value.GetParam(dbMethodParams);
                dbCommand.Parameters.Add(dbParameter);
            }
            if (HasReturnValue())
            {
                dbCommand.Parameters.Add(CreateParameter<TDbCommand, TDbParameter>(dbCommand, dbMethodParams, IsOutput ? ParameterDirection.Output : ParameterDirection.ReturnValue, ReturnParam));
            }
            return dbCommand;
        }

        public IValidationResult Validate(IDbParamsModel paramsModel)
            => (DbParams != null && DbParams.ParamsCount() > 0) ?
                ParamsValidator.Validate((TDbParams)paramsModel) :
                ValidationResult.PassingValidation();     
        
        public bool IsTransaction() => Transaction;
        
        public void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;
            }
        }        
    }
}
