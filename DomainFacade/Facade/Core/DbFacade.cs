using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{

    public abstract class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {        
        private DbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();
        
        protected override TDbResponse CallDbMethod<TDbResponse, DbMethod>()
        {
            return CallDbMethod<TDbResponse,DbParamsModel, DbMethod>(DEFAULT_PARAMETERS);
        }
        protected override TDbResponse CallDbMethod<TDbResponse, DbParams, DbMethod>(DbParams parameters)
        {
            return CallDbMethodCore<TDbResponse, DbParams, DbMethod>(parameters);
        }         
        protected override TDbResponse CallFacadeAPIDbMethod<TDbFacade, TDbResponse,DbParams,  DbMethod>(DbParams parameters)
        {
            return DbFacadeCache.GetInstance<TDbFacade>().CallDbMethod< TDbResponse,DbParams, DbMethod>(parameters);
        }
        
        protected abstract TDbResponse CallDbMethodCore<TDbResponse, DbParams, DbMethod>(DbParams parameters)
            where TDbResponse : DbResponse
            where DbParams : IDbParamsModel
            where DbMethod: TDbManifest;


        public class Forwarder<TDbFacade> : DbFacade<TDbManifest>
        where TDbFacade : DbFacade<TDbManifest>
        {
            protected override TDbResponse CallDbMethodCore<TDbResponse, DbParams, DbMethod>(DbParams parameters)
            {
                OnBeforeForward<DbParams, DbMethod>(parameters);
                return CallFacadeAPIDbMethod<TDbFacade, TDbResponse, DbParams, DbMethod>(parameters);
            }
            protected virtual void OnBeforeForward<DbParams, DbMethod>(DbParams parameters) 
                where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
            { }            
        }

        private sealed class DbFacadeCache : InstanceResolver<DbFacade<TDbManifest>>{}

        
    }    
}
