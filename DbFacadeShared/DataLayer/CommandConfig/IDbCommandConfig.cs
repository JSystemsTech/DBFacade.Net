using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;

namespace DbFacade.DataLayer.CommandConfig
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

        TDbCommand GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(IDbParamsModel tDbMethodManifestMethodParams,
            TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter;
        Task<TDbCommand> GetDbCommandAsync<TDbConnection, TDbCommand, TDbParameter>(IDbParamsModel tDbMethodManifestMethodParams,
            TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter;

        int GetReturnValue<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand;
        Task<int> GetReturnValueAsync<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand;
        IDictionary<string, object> GetOutputValues<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand;
        Task<IDictionary<string, object>> GetOutputValuesAsync<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand;

        Task<IDbConnectionConfigInternal> GetDbConnectionConfigAsync();
    }
}