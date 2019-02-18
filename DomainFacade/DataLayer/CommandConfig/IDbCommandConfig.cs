using DomainFacade.DataLayer.Models;
using System;
using System.Data.Common;

namespace DomainFacade.DataLayer.CommandConfig
{
    public interface IDbCommandConfig
    {
        Cmd GetDbCommand<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
            where Con : DbConnection
            where Cmd : DbCommand
            where Prm : DbParameter;
        bool HasReturnValue();
        object GetReturnValue<Cmd>(Cmd dbCommand)
            where Cmd : DbCommand;
        void SetReturnValue<Cmd>(Cmd dbCommand, object value)
            where Cmd : DbCommand;
        Type GetDBConnectionType();
        bool Validate(IDbParamsModel paramsModel);
        bool HasStoredProcedure();
        Con GetDbConnection<Con>()
            where Con : DbConnection;
        Type GetDbMethodCallType();
        bool IsTransaction();
        bool IsTransactionWithReturn();
        bool IsFetchRecord();
        bool IsFetchRecordWithReturn();
        bool IsFetchRecords();
        bool IsFetchRecordsWithReturn();
    }

}
