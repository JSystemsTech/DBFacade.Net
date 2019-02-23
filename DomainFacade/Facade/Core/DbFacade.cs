using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{

    public abstract class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {        
        internal DbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();
        
        protected override IDbResponse<TDbDataModel> CallDbMethod<TDbDataModel, DbMethod>()
        {
            return CallDbMethod<TDbDataModel, DbParamsModel, DbMethod>(DEFAULT_PARAMETERS);
        }
        protected override IDbResponse<TDbDataModel> CallDbMethod<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
        {
            return CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(parameters);
        }         
        protected override IDbResponse<TDbDataModel> CallFacadeAPIDbMethod<TDbFacade, TDbDataModel, TDbParams,  DbMethod>(TDbParams parameters)
        {
            return DbFacadeCache.GetInstance<TDbFacade>().CallDbMethod<TDbDataModel, TDbParams, DbMethod>(parameters);
        }
        
        protected abstract IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod: TDbManifest;


        public class Forwarder<TDbFacade> : DbFacade<TDbManifest>
        where TDbFacade : DbFacade<TDbManifest>
        {
            protected override IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            {
                OnBeforeForward<TDbParams, DbMethod>(parameters);
                return CallFacadeAPIDbMethod<TDbFacade, TDbDataModel, TDbParams, DbMethod>(parameters);
            }
            protected virtual void OnBeforeForward<TDbParams, DbMethod>(TDbParams parameters) 
                where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
            { }            
        }

        private sealed class DbFacadeCache : InstanceResolver<DbFacade<TDbManifest>>{}

        
    }    
}
