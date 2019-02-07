using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{
    
    public abstract class FacadeAPI<DbMethodGroup> : FacadeAPIBase<DbMethodGroup> where DbMethodGroup : DbMethodsCore
    {        
        private DbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();
        
        protected override TDbResponse CallDbMethod<TDbResponse, DbMethod>()
        {
            return CallDbMethod<DbParamsModel, TDbResponse, DbMethod>(DEFAULT_PARAMETERS);
        }
        protected override TDbResponse CallDbMethod<DbParams, TDbResponse, DbMethod>(DbParams parameters)
        {
            return CallDbMethodCore<DbParams, TDbResponse, DbMethod>(parameters);
        }         
        protected override TDbResponse CallFacadeAPIDbMethod<DbParams, F, TDbResponse, DbMethod>(DbParams parameters)
        {
            return FacadeAPIService.GetInstance<F>().CallDbMethod<DbParams, TDbResponse, DbMethod>(parameters);
        }
        
        protected abstract TDbResponse CallDbMethodCore<DbParams, TDbResponse, DbMethod>(DbParams parameters)
            where TDbResponse : DbResponse
            where DbParams : IDbParamsModel
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
