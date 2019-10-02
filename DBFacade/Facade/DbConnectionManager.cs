using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Facade.Core;
using System.Threading.Tasks;

namespace DBFacade.Facade
{
    internal sealed class DbConnectionManager<TDbMethodManifest> : DbFacade<TDbMethodManifest>
    where TDbMethodManifest : DbMethodManifest
    {        
        protected sealed override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        => (method.Config as IDbCommandConfigInternal).DbConnectionConfig.ExecuteDbAction<TDbMethodManifest, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);            
        
        protected sealed override async Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IDbCommandConfigInternal commandConfig = await method.GetConfigAsync() as IDbCommandConfigInternal;
            IDbConnectionConfigInternal connectionConfig = await commandConfig.GetDbConnectionConfigAsync();
            return await connectionConfig.ExecuteDbActionAsync<TDbMethodManifest, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
    }
}
