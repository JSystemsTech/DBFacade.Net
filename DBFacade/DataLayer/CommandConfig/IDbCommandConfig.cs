using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using System.Data.Common;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.CommandConfig
{
    public interface IDbCommandConfig : ISafeDisposable
    {
        IValidationResult Validate(IDbParamsModel paramsModel);
        Task<IValidationResult> ValidateAsync(IDbParamsModel paramsModel);
    }
    internal interface IDbCommandConfigInternal: IDbCommandConfig
    {
        TDbCommand GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(IDbParamsModel TDbMethodManifestMethodParams, TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter;
        object GetReturnValue<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand;
        IDbConnectionConfigInternal DbConnectionConfig { get; }
        Task<IDbConnectionConfigInternal> GetDbConnectionConfigAsync();        
        IDbCommandText DbCommandText { get; }        
        bool IsTransaction { get; }
    }
}
