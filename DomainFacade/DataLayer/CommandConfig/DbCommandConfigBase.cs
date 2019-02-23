using DomainFacade.DataLayer.Models;
using System;
using System.Data.Common;

namespace DomainFacade.DataLayer.CommandConfig
{
    internal abstract class DbCommandConfigBase
    {
        public bool IsTransaction()
        {
            return Transaction;
        }
        internal bool Transaction = false;
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
        
        public void SetReturnValue<Cmd>(Cmd dbCommand, object value)
            where Cmd : DbCommand
        {
            SetReturnValueCore(dbCommand, value);
        }
        protected abstract void SetReturnValueCore<Cmd>(Cmd dbCommand, object value) where Cmd : DbCommand;

        public Type GetDBConnectionType() { return GetDBConnectionTypeCore(); }
        protected abstract Type GetDBConnectionTypeCore();

        public bool Validate(IDbParamsModel paramsModel) { return ValidateCore(paramsModel); }
        protected abstract bool ValidateCore(IDbParamsModel paramsModel);
    }
}
