using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;

namespace DomainFacade.Facade
{

    public abstract class DomainFacade<TDbManifest> : DomainFacade<DomainManager<TDbManifest>, TDbManifest>
    where TDbManifest : DbManifest
    { }
    
    public abstract class DomainFacade<M, TDbManifest> : DbFacade<TDbManifest>.Forwarder<DomainFacadeCore<M, TDbManifest>>
    where M : DomainManager<TDbManifest>
    where TDbManifest : DbManifest
    {
        protected virtual IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters)
            where TDbDataModel : DbDataModel
            where DbParams: IDbParamsModel
            where DbMethod: TDbManifest
        {
            return CallDbMethod<TDbDataModel, DbParams, DbMethod>(parameters);
        }
        protected virtual IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbMethod>() where TDbDataModel : DbDataModel where DbMethod : TDbManifest
        {
            return CallDbMethod<TDbDataModel, DbMethod>();
        }

        protected virtual IDbResponse Transaction<DbParams, DbMethod>(DbParams parameters) where DbParams : IDbParamsModel where DbMethod : TDbManifest
        {
            return CallDbMethod<DbDataModel, DbParams, DbMethod>(parameters);
        }
        protected virtual IDbResponse Transaction<DbMethod>() where DbMethod : TDbManifest
        {
            return CallDbMethod<DbDataModel, DbMethod>();
        }
    }

    public sealed class DomainFacadeCore<M, TDbManifest> : DbFacade<TDbManifest>.Forwarder<M>
    where M : DomainManager<TDbManifest>
    where TDbManifest : DbManifest
    {
    }
}
