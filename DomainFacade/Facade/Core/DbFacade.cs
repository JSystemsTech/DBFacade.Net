using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{

    public abstract class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {        
        internal DbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();
        
        protected override TDbResponse CallDbMethod<TDbResponse, DbMethod>()
        {
            return CallDbMethod<TDbResponse,DbParamsModel, DbMethod>(DEFAULT_PARAMETERS);
        }
        protected override TDbResponse CallDbMethod<TDbResponse, TDbParams, DbMethod>(TDbParams parameters)
        {
            return CallDbMethodCore<TDbResponse, TDbParams, DbMethod>(parameters);
        }         
        protected override TDbResponse CallFacadeAPIDbMethod<TDbFacade, TDbResponse, TDbParams,  DbMethod>(TDbParams parameters)
        {
            return DbFacadeCache.GetInstance<TDbFacade>().CallDbMethod< TDbResponse, TDbParams, DbMethod>(parameters);
        }
        
        protected abstract TDbResponse CallDbMethodCore<TDbResponse, TDbParams, DbMethod>(TDbParams parameters)
            where TDbResponse : DbResponse
            where TDbParams : IDbParamsModel
            where DbMethod: TDbManifest;


        public class Forwarder<TDbFacade> : DbFacade<TDbManifest>
        where TDbFacade : DbFacade<TDbManifest>
        {
            protected override TDbResponse CallDbMethodCore<TDbResponse, TDbParams, DbMethod>(TDbParams parameters)
            {
                OnBeforeForward<TDbParams, DbMethod>(parameters);
                return CallFacadeAPIDbMethod<TDbFacade, TDbResponse, TDbParams, DbMethod>(parameters);
            }
            protected virtual void OnBeforeForward<TDbParams, DbMethod>(TDbParams parameters) 
                where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
            { }            
        }

        private sealed class DbFacadeCache : InstanceResolver<DbFacade<TDbManifest>>{}

        
    }    
}
