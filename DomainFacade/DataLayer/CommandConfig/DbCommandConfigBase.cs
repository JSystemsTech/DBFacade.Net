using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using System;
using System.Data.Common;

namespace DomainFacade.DataLayer.CommandConfig
{
    internal abstract class DbCommandConfigBase
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
}
