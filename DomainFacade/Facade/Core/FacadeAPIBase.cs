using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;

namespace DomainFacade.Facade.Core
{
      
    public abstract class FacadeAPIBase<DbMethodGroup> where DbMethodGroup : DbMethodsCore
    {

        protected abstract R CallDbMethod<R, DbMethod>() 
            where R : DbResponse
            where DbMethod : DbMethodGroup;

        protected abstract R CallDbMethod<U, R, DbMethod>(U parameters) 
            where R : DbResponse
            where U : IDbParamsModel
            where DbMethod : DbMethodGroup;
        protected abstract R CallFacadeAPIDbMethod<U, F, R, DbMethod>(U parameters) 
            where R: DbResponse
            where U : IDbParamsModel
            where F : FacadeAPI<DbMethodGroup>
            where DbMethod : DbMethodGroup;

        internal sealed class DbMethodsService : InstanceResolver<DbMethodGroup>
        {

        }

    }
}
