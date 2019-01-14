using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using static DomainFacade.DataLayer.Models.DbResponse;

namespace DomainFacade.DataLayer.DbManifest
{
    public class DbCommandConfigParams<T> : Dictionary<string, DbCommandParameterConfig<T>> where T : IDbParamsModel
    { }
    public abstract class DbCommandConfig
    {
        public DbMethodType DBMethodType { get; private set; }
        internal void SetDbMethod(DbMethodType dbMethodType) { DBMethodType = dbMethodType; }

        public bool IsTransaction()
        {
            return DBMethodType == DbMethodType.Transaction;
        }
        public bool IsTransactionWithReturn()
        {
            return DBMethodType == DbMethodType.TransactionWithReturn;
        }
        public bool IsFetchRecord()
        {
            return DBMethodType == DbMethodType.FetchRecord;
        }
        public bool IsFetchRecordWithReturn()
        {
            return DBMethodType == DbMethodType.FetchRecordWithReturn;
        }
        public bool IsFetchRecords()
        {
            return DBMethodType == DbMethodType.FetchRecords;
        }
        public bool IsFetchRecordsWithReturn()
        {
            return DBMethodType == DbMethodType.FetchRecordsWithReturn;
        }
        public Con GetDbConnection<Con>()
            where Con : DbConnection
        {
            return GetDbConnectionCore<Con>();
        }
        protected abstract Con GetDbConnectionCore<Con>() where Con : DbConnection;
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
    }
    public class DbCommandConfig<T, C>: DbCommandConfig where T : IDbParamsModel where C: DbConnectionCore
    {
        public DbCommandConfigParams<T> DbParams { get; private set; }

        public DbCommandText<C> DbCommand { get; private set; }
        public string ReturnValue { get; private set; }
        private CommandType DbCommandType { get; set; }
        
        

        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType) {
            DbCommand = dbCommand;
            DbCommandType = dbCommandType;
            DbParams = new DbCommandConfigParams<T> { };
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams)
        {
            DbCommand = dbCommand;
            DbCommandType = dbCommandType;
            DbParams = dbParams;
        }
        public DbCommandConfig(DbCommandText<C> dbCommand)
        {
            DbCommand = dbCommand;
            DbCommandType = CommandType.StoredProcedure;
            DbParams = new DbCommandConfigParams<T> { };
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams)
        {
            DbCommand = dbCommand;
            DbCommandType = CommandType.StoredProcedure;
            DbParams = dbParams;
        }

        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, string returnValue)
        {
            ReturnValue = returnValue;
            DbCommand = dbCommand;
            DbCommandType = dbCommandType;
            DbParams = new DbCommandConfigParams<T> { };
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, string returnValue)
        {
            ReturnValue = returnValue;
            DbCommand = dbCommand;
            DbCommandType = dbCommandType;
            DbParams = dbParams;
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, string returnValue)
        {
            ReturnValue = returnValue;
            DbCommand = dbCommand;
            DbCommandType = CommandType.StoredProcedure;
            DbParams = new DbCommandConfigParams<T> { };
        }
        public DbCommandConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, string returnValue)
        {
            ReturnValue = returnValue;
            DbCommand = dbCommand;
            DbCommandType = CommandType.StoredProcedure;
            DbParams = dbParams;
        }
        protected override Con GetDbConnectionCore<Con>()
        {
            return (Con)GenericInstance<C>.GetInstance().GetDbConnection();
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
        
        public class TransactionConfig : DbCommandConfig<T, C>
        {
            public TransactionConfig(DbCommandText<C> dbCommand) : base(dbCommand) { SetDbMethod(DbMethodType.Transaction); }
                public TransactionConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams) : base(dbCommand, dbParams) { SetDbMethod(DbMethodType.Transaction); }
            public TransactionConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams) : base(dbCommand, dbCommandType, dbParams) { SetDbMethod(DbMethodType.Transaction); }
        }
        public class TransactionConfigWithReturn : DbCommandConfig<T, C>
        {
            public TransactionConfigWithReturn(DbCommandText<C> dbCommand, string returnValue) : base(dbCommand, returnValue) { SetDbMethod(DbMethodType.TransactionWithReturn); }
            public TransactionConfigWithReturn(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbParams, returnValue) { SetDbMethod(DbMethodType.TransactionWithReturn); }
            public TransactionConfigWithReturn(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbCommandType, dbParams, returnValue) { SetDbMethod(DbMethodType.TransactionWithReturn); }
        }
        public class FetchRecordConfig: DbCommandConfig<T, C>
        {
            public FetchRecordConfig(DbCommandText<C> dbCommand) : base(dbCommand) { SetDbMethod(DbMethodType.FetchRecord); }
            public FetchRecordConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams) : base(dbCommand, dbParams) { SetDbMethod(DbMethodType.FetchRecord); }
            public FetchRecordConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams): base(dbCommand, dbCommandType, dbParams){ SetDbMethod(DbMethodType.FetchRecord); }
        }
        public class FetchRecordConfigWithReturn : DbCommandConfig<T, C>
        {
            public FetchRecordConfigWithReturn(DbCommandText<C> dbCommand, string returnValue) : base(dbCommand, returnValue) { SetDbMethod(DbMethodType.FetchRecordWithReturn); }
            public FetchRecordConfigWithReturn(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbParams, returnValue) { SetDbMethod(DbMethodType.FetchRecordWithReturn); }
            public FetchRecordConfigWithReturn(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbCommandType, dbParams, returnValue) { SetDbMethod(DbMethodType.FetchRecordWithReturn); }
        }
        public class FetchRecordsConfig : DbCommandConfig<T, C>
        {
            public FetchRecordsConfig(DbCommandText<C> dbCommand) : base(dbCommand) { SetDbMethod(DbMethodType.FetchRecords); }
            public FetchRecordsConfig(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams) : base(dbCommand, dbParams) { SetDbMethod(DbMethodType.FetchRecords); }
            public FetchRecordsConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams) : base(dbCommand, dbCommandType, dbParams) { SetDbMethod(DbMethodType.FetchRecords); }
        }
        public class FetchRecordsConfigWithReturn : DbCommandConfig<T, C>
        {
            public FetchRecordsConfigWithReturn(DbCommandText<C> dbCommand, string returnValue) : base(dbCommand, returnValue) { SetDbMethod(DbMethodType.FetchRecordsWithReturn); }
            public FetchRecordsConfigWithReturn(DbCommandText<C> dbCommand, DbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbParams, returnValue) { SetDbMethod(DbMethodType.FetchRecordsWithReturn); }
            public FetchRecordsConfigWithReturn(DbCommandText<C> dbCommand, CommandType dbCommandType, DbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbCommandType, dbParams, returnValue) { SetDbMethod(DbMethodType.FetchRecordsWithReturn); }
        }
    }
}
