using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Facade.Core;

namespace DBFacade.Facade
{
    internal sealed class DbConnectionManager<TDbManifest> : DbFacade<TDbManifest>
    where TDbManifest : DbManifest
    {        
        protected sealed override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            IDbConnectionConfig connectionConfig = method.GetConfig().GetDBConnectionConfig();
            return connectionConfig.ExecuteDbAction<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);            
        }
    }
}
