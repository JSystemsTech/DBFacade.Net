using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{
    
    public abstract class FacadeAPI<DbMethodGroup> : FacadeAPIBase<DbMethodGroup> where DbMethodGroup : DbMethodsCore
    {        
        private DbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();
        
        protected override R CallDbMethod<R, DbMethod>()
        {
            return CallDbMethod<DbParamsModel, R, DbMethod>(DEFAULT_PARAMETERS);
        }
        protected override R CallDbMethod<U,R, DbMethod>(U parameters)
        {
            return CallDbMethodCore<U,R, DbMethod>(parameters);
        }         
        protected override R CallFacadeAPIDbMethod<U, F, R, DbMethod>(U parameters)
        {
            return FacadeAPIService.GetInstance<F>().CallDbMethod<U, R, DbMethod>(parameters);
        }
        
        protected abstract R CallDbMethodCore<U, R, DbMethod>(U parameters)
            where R : DbResponse
            where U : IDbParamsModel
            where DbMethod: DbMethodGroup;


        public class Forwarder<T> : FacadeAPI<DbMethodGroup>
        where T : FacadeAPI<DbMethodGroup>
        {
            protected override R CallDbMethodCore<U, R, DbMethod>(U parameters)
            {
                OnBeforeForward<U, DbMethod>(parameters);
                return CallFacadeAPIDbMethod<U, T, R, DbMethod>(parameters);
            }
            protected virtual void OnBeforeForward<U, DbMethod>(U parameters) 
                where U : IDbParamsModel
            where DbMethod : DbMethodGroup
            { }            
        }

        private sealed class FacadeAPIService : InstanceResolver<FacadeAPI<DbMethodGroup>>{}

        
    }    
}
