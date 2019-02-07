using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;

namespace DomainFacade.Facade.Core
{
      
    public abstract class DbFacadeBase<DbMethodGroup> where DbMethodGroup : DbMethodsCore
    {
        protected abstract TDbResponse CallDbMethod<TDbResponse, DbMethod>() 
            where TDbResponse : DbResponse
            where DbMethod : DbMethodGroup;

        protected abstract TDbResponse CallDbMethod<TDbResponse, DbParams, DbMethod>(DbParams parameters) 
            where TDbResponse : DbResponse
            where DbParams : IDbParamsModel
            where DbMethod : DbMethodGroup;
        protected abstract TDbResponse CallFacadeAPIDbMethod<TDbFacade, TDbResponse, DbParams, DbMethod>(DbParams parameters) 
            where TDbResponse : DbResponse
            where DbParams : IDbParamsModel
            where TDbFacade : DbFacade<DbMethodGroup>
            where DbMethod : DbMethodGroup;

        internal sealed class DbMethodsCache : InstanceResolver<DbMethodGroup> { }
    }
}
