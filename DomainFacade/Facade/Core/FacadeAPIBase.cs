using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;

namespace DomainFacade.Facade.Core
{
      
    public abstract class FacadeAPIBase<DbMethodGroup> where DbMethodGroup : DbMethodsCore
    {

        protected abstract TDbResponse CallDbMethod<TDbResponse, DbMethod>() 
            where TDbResponse : DbResponse
            where DbMethod : DbMethodGroup;

        protected abstract TDbResponse CallDbMethod<DbParams, TDbResponse, DbMethod>(DbParams parameters) 
            where TDbResponse : DbResponse
            where DbParams : IDbParamsModel
            where DbMethod : DbMethodGroup;
        protected abstract TDbResponse CallFacadeAPIDbMethod<DbParams, F, TDbResponse, DbMethod>(DbParams parameters) 
            where TDbResponse : DbResponse
            where DbParams : IDbParamsModel
            where F : FacadeAPI<DbMethodGroup>
            where DbMethod : DbMethodGroup;

        internal sealed class DbMethodsService : InstanceResolver<DbMethodGroup>
        {

        }

    }
}
