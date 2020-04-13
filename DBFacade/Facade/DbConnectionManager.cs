using System.Threading.Tasks;
using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Facade.Core;

namespace DBFacade.Facade
{
    internal sealed class DbConnectionManager<TDbMethodManifest> : DbFacade<TDbMethodManifest>
        where TDbMethodManifest : DbMethodManifest
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbMethodManifestMethod method, TDbParams parameters)
        {
            return method.Config is IDbCommandConfigInternal config
                ? config.DbConnectionConfig
                    .ExecuteDbAction<TDbMethodManifest, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method,
                        parameters)
                : null;
        }

        protected override async Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            if (await method.GetConfigAsync() is IDbCommandConfigInternal commandConfig)
                if (await commandConfig.GetDbConnectionConfigAsync() is IDbConnectionConfigInternal connectionConfig)
                    return await connectionConfig
                        .ExecuteDbActionAsync<TDbMethodManifest, TDbDataModel, TDbParams, TDbMethodManifestMethod>(
                            method, parameters);

            return null;
        }
    }
}