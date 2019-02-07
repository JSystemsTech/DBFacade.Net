using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using static DomainFacade.DataLayer.DbConnectionService;

namespace DomainFacade.DataLayer.DbManifest
{
    public class DbCommandConfigParams<T> : Dictionary<string, DbCommandParameterConfig<T>> where T : IDbParamsModel
    { }
    public abstract class DbCommandConfig
    {
        public virtual Type GetDbMethodCallType() { return typeof(object); }
        public bool IsTransaction()
        {
            return GetDbMethodCallType() == typeof(DbMethodCallType.Transaction);
        }
        public bool IsTransactionWithReturn()
        {
            return GetDbMethodCallType() == typeof(DbMethodCallType.TransactionWithReturn);
        }
        public bool IsFetchRecord()
        {
            return GetDbMethodCallType() == typeof(DbMethodCallType.FetchRecord);
        }
        public bool IsFetchRecordWithReturn()
        {
            return GetDbMethodCallType() == typeof(DbMethodCallType.FetchRecordWithReturn);
        }
        public bool IsFetchRecords()
        {
            return GetDbMethodCallType() == typeof(DbMethodCallType.FetchRecords);
        }
        public bool IsFetchRecordsWithReturn()
        {
            return GetDbMethodCallType() == typeof(DbMethodCallType.FetchRecordsWithReturn);
        }
        public Con GetDbConnection<Con>()
            where Con : DbConnection
        {
            return GetDbConnectionCore<Con>();
        }
        protected abstract Con GetDbConnectionCore<Con>() where Con : DbConnection;
        public bool HasStoredProcedure()
        {
            return HasStoredProcedureCore();
        }
        protected abstract bool HasStoredProcedureCore();
        public Cmd GetDbCommand<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
            where Con : DbConnection
            where Cmd : DbCommand
            where Prm : DbParameter
        {
            return GetDbCommandCore<Con, Cmd, Prm>(dbMethodParams, dbConnection);
        }
        protected abstract Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
            where Con : DbConnection
            where Cmd : DbCommand
            where Prm : DbParameter;
        public object GetReturnValue<Cmd>(Cmd dbCommand)
            where Cmd : DbCommand
        {
            return GetReturnValueCore(dbCommand);
        }
        protected abstract object GetReturnValueCore<Cmd>(Cmd dbCommand) where Cmd : DbCommand;

        public Type GetDBConnectionType() { return GetDBConnectionTypeCore(); }
        protected abstract Type GetDBConnectionTypeCore();

        public bool Validate(IDbParamsModel paramsModel) { return ValidateCore(paramsModel); }
        protected abstract bool ValidateCore(IDbParamsModel paramsModel);
    }
    public abstract class DbCommandConfig<T, C>: DbCommandConfig where T : IDbParamsModel where C: DbConnectionCore
    {
        public DbCommandConfigParams<T> DbParams { get; private set; }
        private DbCommandConfigParams<T> DefaultConfigParams = new DbCommandConfigParams<T> { };
        public DbCommandText<C> DbCommand { get; private set; }
        public string ReturnValue { get; private set; }
        private CommandType DbCommandType { get; set; }

        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType)
        {           
            init(dbCommand, dbCommandType, DefaultConfigParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams)
        {
            init(dbCommand, dbCommandType, dbParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, Validator<T> validator)
        {
            init(dbCommand, dbCommandType, dbParams, null, validator);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand)
        {
            init(dbCommand, CommandType.StoredProcedure, DefaultConfigParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams)
        {
            init(dbCommand, CommandType.StoredProcedure, dbParams, null, ParamsValidatorEmpty);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, Validator<T> validator)
        {
            init(dbCommand, CommandType.StoredProcedure, dbParams, null, validator);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, string returnValue)
        {
            init(dbCommand, dbCommandType, DefaultConfigParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, string returnValue)
        {
            init(dbCommand, dbCommandType, dbParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator)
        {
            init(dbCommand, dbCommandType, dbParams, returnValue, validator);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, string returnValue)
        {
            init(dbCommand, CommandType.StoredProcedure, DefaultConfigParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, string returnValue)
        {
            init(dbCommand, CommandType.StoredProcedure, dbParams, returnValue, ParamsValidatorEmpty);
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator)
        {
            init(dbCommand, CommandType.StoredProcedure, dbParams, returnValue, validator);
        }
       
        private void init(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator)
        {
            ReturnValue = returnValue;
            DbCommand = dbCommand;
            DbCommandType = dbCommandType;
            DbParams = dbParams;
            ParamsValidator = validator;
        }
        protected override Con GetDbConnectionCore<Con>()
        {
            return (Con)DbConnectionService.GetDbConnection<C>().GetDbConnection();
        }
        protected override bool HasStoredProcedureCore()
        {
            C Connection = DbConnectionService.GetDbConnection<C>();
            if (Connection.CheckStoredProcAvailability())
            {
                string[] storedProcNames = GetAvailableStoredProcedured<C>().Select(sproc => sproc.Name).ToArray();
                return Array.IndexOf(storedProcNames, DbCommand.CommandText) != -1;
            }
            return true;
        }
        protected override Type GetDBConnectionTypeCore()
        {
            return typeof(C);
        }
        public Cmd GetDbCommand<Con, Cmd, Prm>(T dbMethodParams, Con dbConnection)
            where Con : DbConnection
            where Cmd : DbCommand
            where Prm : DbParameter
        {
            return GetDbCommandCore<Con, Cmd, Prm>(dbMethodParams, dbConnection);
        }
        protected override Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
        {
            Cmd dbCommand = (Cmd)dbConnection.CreateCommand();
            dbCommand = AddParams<Cmd, Prm>(dbCommand, (T)dbMethodParams);
            dbCommand.CommandText = DbCommand.CommandText;
            dbCommand.CommandType = DbCommandType;
            return dbCommand;
        }
        public bool HasReturnValue()
        {
            return !string.IsNullOrEmpty(ReturnValue);
        }
        
        protected override object GetReturnValueCore<Cmd>(Cmd dbCommand)
        {
            if (HasReturnValue())
            {
                return dbCommand.Parameters["@" + ReturnValue].Value;
            }
            else
            {
                throw new Exception("no return value specified");
            }
        }
        private Cmd AddParams<Cmd, Prm>(Cmd dbCommand, T dbMethodParams)
        where Cmd : DbCommand
        where Prm : DbParameter
        {
            
            foreach (KeyValuePair<string, DbCommandParameterConfig<T>> config in DbParams)
            {
                Prm dbParameter = (Prm)dbCommand.CreateParameter();
                
                string paramKey = "@" + config.Key;
                
                    dbParameter.DbType = config.Value.DBType;
                    dbParameter.Direction = ParameterDirection.Input;
                    dbParameter.IsNullable = config.Value.IsNullable;
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

        protected override bool ValidateCore(IDbParamsModel paramsModel)
        {
            if( DbParams != null && DbParams.Count > 0)
            {
                return ParamsValidator.Validate((T)paramsModel);
            }
            return true;
        }
        private Validator<T> ParamsValidator { get; set; }
        private static Validator<T> ParamsValidatorEmpty = new Validator<T>();
       
    }
    public class DbCommandConfig<T,C, TDbMethod> : DbCommandConfig<T, C>
        where T : IDbParamsModel 
        where C : DbConnectionCore
        where TDbMethod : DbMethodCallType
    {
        public override Type GetDbMethodCallType() { return typeof(TDbMethod); }
        public DbCommandConfig(DbCommandText<C> dbCommand) : base(dbCommand) { }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams) : base(dbCommand, dbParams) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams) : base(dbCommand, dbCommandType, dbParams) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, Validator<T> validator) : base(dbCommand, dbParams, validator) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, Validator<T> validator) : base(dbCommand, dbCommandType, dbParams, validator) { }

        public DbCommandConfig(DbCommandText<C> dbCommand, string returnValue) : base(dbCommand, returnValue) { }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbParams, returnValue) { }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbCommandType, dbParams, returnValue) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator) : base(dbCommand, dbParams, returnValue, validator) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator) : base(dbCommand, dbCommandType, dbParams, returnValue, validator) { }
    }

}
