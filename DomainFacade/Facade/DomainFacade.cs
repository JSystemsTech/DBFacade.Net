using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using System.Data;

namespace DomainFacade.Facade
{

    public abstract class DomainFacade<TDbManifest> : DomainFacade<DomainManager<TDbManifest>, TDbManifest>
    where TDbManifest : DbManifest
    { }
    
    public abstract class DomainFacade<M, TDbManifest> : DbFacade<TDbManifest>
    where M : DomainManager<TDbManifest>
    where TDbManifest : DbManifest
    {

        protected sealed override IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
        {
            return CallFacadeAPIDbMethod<M, TDbDataModel, TDbParams, DbMethod>(parameters);
        }
        
    }
}
