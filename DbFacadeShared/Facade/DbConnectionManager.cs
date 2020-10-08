using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Manifest;
using DbFacade.DataLayer.Models;

namespace DbFacade.Facade
{
    internal sealed class DbConnectionManager<TDbMethodManifest> : DbFacade<TDbMethodManifest>
        where TDbMethodManifest : DbMethodManifest
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbMethodManifestMethod method, TDbParams parameters)
        {
            return method.GetConfig() is IDbCommandConfigInternal config
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