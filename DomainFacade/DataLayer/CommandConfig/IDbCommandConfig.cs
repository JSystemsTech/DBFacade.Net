using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.Exceptions;
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
        object GetReturnValue<Cmd>(Cmd dbCommand)
            where Cmd : DbCommand;
        void SetReturnValue<Cmd>(Cmd dbCommand, object value)
            where Cmd : DbCommand;
        IDbConnectionConfig GetDBConnectionConfig();
        IValidationResult Validate(IDbParamsModel paramsModel);
        bool HasStoredProcedure();
        Con GetDbConnection<Con>()
            where Con : DbConnection;
        bool IsTransaction();
        MissingStoredProcedureException GetMissingStoredProcedureException(string message);
        SQLExecutionException GetSQLExecutionException(string message, Exception e);
    }

}
