using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{

    public abstract class DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {
        protected abstract TDbResponse CallDbMethod<TDbResponse, DbMethod>() 
            where TDbResponse : DbResponse
            where DbMethod : TDbManifest;

        protected abstract TDbResponse CallDbMethod<TDbResponse, DbParams, DbMethod>(DbParams parameters) 
            where TDbResponse : DbResponse
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        protected abstract TDbResponse CallFacadeAPIDbMethod<TDbFacade, TDbResponse, DbParams, DbMethod>(DbParams parameters) 
            where TDbResponse : DbResponse
            where DbParams : IDbParamsModel
            where TDbFacade : DbFacade<TDbManifest>
            where DbMethod : TDbManifest;

        internal sealed class DbMethodsCache : InstanceResolver<TDbManifest> { }
    }
}
