using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{
    
    public abstract class DbFacade<DbMethodGroup> : DbFacadeBase<DbMethodGroup> where DbMethodGroup : DbMethodsCore
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
        protected override TDbResponse CallFacadeAPIDbMethod<DbParams, TDbFacade, TDbResponse, DbMethod>(DbParams parameters)
        {
            return DbFacadeCache.GetInstance<TDbFacade>().CallDbMethod<DbParams, TDbResponse, DbMethod>(parameters);
        }
        
        protected abstract TDbResponse CallDbMethodCore<DbParams, TDbResponse, DbMethod>(DbParams parameters)
            where TDbResponse : DbResponse
            where DbParams : IDbParamsModel
            where DbMethod: DbMethodGroup;


        public class Forwarder<TDbFacade> : DbFacade<DbMethodGroup>
        where TDbFacade : DbFacade<DbMethodGroup>
        {
            protected override TDbResponse CallDbMethodCore<DbParams, TDbResponse, DbMethod>(DbParams parameters)
            {
                OnBeforeForward<DbParams, DbMethod>(parameters);
                return CallFacadeAPIDbMethod<DbParams, TDbFacade, TDbResponse, DbMethod>(parameters);
            }
            protected virtual void OnBeforeForward<DbParams, DbMethod>(DbParams parameters) 
                where DbParams : IDbParamsModel
            where DbMethod : DbMethodGroup
            { }            
        }

        private sealed class DbFacadeCache : InstanceResolver<DbFacade<DbMethodGroup>>{}

        
    }    
}
