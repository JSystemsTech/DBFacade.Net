using System.Data.Common;
using System.Threading.Tasks;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;

namespace DBFacade.DataLayer.CommandConfig
{
    public interface IDbCommandConfig : ISafeDisposable
    {
        IValidationResult Validate(IDbParamsModel paramsModel);
        Task<IValidationResult> ValidateAsync(IDbParamsModel paramsModel);
    }

    internal interface IDbCommandConfigInternal : IDbCommandConfig
    {
        IDbConnectionConfigInternal DbConnectionConfig { get; }
        IDbCommandText DbCommandText { get; }
        bool IsTransaction { get; }

        TDbCommand GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(IDbParamsModel TDbMethodManifestMethodParams,
            TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter;

        object GetReturnValue<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand;

        Task<IDbConnectionConfigInternal> GetDbConnectionConfigAsync();
    }
}