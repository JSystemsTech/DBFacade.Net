using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using System;
using System.Data.Common;

namespace DBFacade.DataLayer.CommandConfig
{
    public interface IDbCommandConfig: IDisposable
    {
        TDbCommand GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(IDbParamsModel TDbManifestMethodParams, TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter;        
        object GetReturnValue<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand;          
        IDbConnectionConfig GetDBConnectionConfig();
        IDbCommandText GetDbCommandText();
        IValidationResult Validate(IDbParamsModel paramsModel);
        bool IsTransaction();
    }
}
