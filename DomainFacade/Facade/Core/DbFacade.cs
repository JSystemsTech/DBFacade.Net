using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;
using System.Data;

namespace DomainFacade.Facade.Core
{
    
    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {        
        internal DbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();
        private  IDbResponse<TDbDataModel> CallDbMethod<TDbDataModel, DbMethod>()
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<TDbDataModel, DbParamsModel, DbMethod>(DEFAULT_PARAMETERS);
        }

        private  IDbResponse<TDbDataModel> CallDbMethod<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(parameters);
        }

        protected override sealed IDbResponse<TDbDataModel> CallFacadeAPIDbMethod<TDbFacade, TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
        {
            OnBeforeForward<TDbParams, DbMethod>(parameters);
            return DbFacadeCache.GetInstance<TDbFacade>().CallDbMethod<TDbDataModel, TDbParams, DbMethod>(parameters);
        }
        protected virtual void OnBeforeForward<TDbParams, DbMethod>(TDbParams parameters)
        where TDbParams : IDbParamsModel
        where DbMethod : TDbManifest
        { }
        protected abstract IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod: TDbManifest;        

        private sealed class DbFacadeCache : InstanceResolver<DbFacade<TDbManifest>>{}
        internal sealed class DbMethodsCache : InstanceResolver<TDbManifest> { }

    }
    internal class Forwarder<TDbManifest,TDbFacade> : DbFacade<TDbManifest> 
        where TDbManifest : DbManifest
        where TDbFacade : DbFacade<TDbManifest>
    {
        protected sealed override IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
        {
            return CallFacadeAPIDbMethod<TDbFacade, TDbDataModel, TDbParams, DbMethod>(parameters);
        }
    }
}
