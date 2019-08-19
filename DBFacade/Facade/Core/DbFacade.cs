using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Services;
using System;
using System.Threading.Tasks;

namespace DBFacade.Facade.Core
{   
    
    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {       
        internal IDbParamsModel DEFAULT_PARAMETERS { get { return InstanceResolvers.Get<IDbParamsModel>().Get<DbParamsModel>(); } }
        private IInstanceResolver<DbFacade<TDbManifest>> DbFacadeCache { get { return InstanceResolvers.Get<DbFacade<TDbManifest>>(); } }
        private IInstanceResolver<TDbManifest> DbMethodsCache { get { return InstanceResolvers.Get<TDbManifest>(); } }
        private DbMethod GetMethod<DbMethod>() where DbMethod: TDbManifest => DbMethodsCache.Get<DbMethod>();
        internal TDbFacade GetDbFacade<TDbFacade>() where TDbFacade : DbFacade<TDbManifest> => DbFacadeCache.Get<TDbFacade>();

        protected bool Disposed { get; set; }
        public void Dispose()
        {
            if (Disposed)
            {
                InstanceResolvers.Get<IDbParamsModel>().Dispose();
                DbFacadeCache.Dispose();
                DbMethodsCache.Dispose();
                OnDispose();
                Disposed = true;
            }
        }
        internal virtual void HandleInnerDispose() {}
        protected virtual void OnDispose() { }
        

        internal override sealed IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, DbMethod>()
            => ExecuteProcess<TDbDataModel, IDbParamsModel, DbMethod>(GetMethod<DbMethod>(),DEFAULT_PARAMETERS);
        internal override sealed IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            => ExecuteProcess<TDbDataModel, TDbParams, DbMethod>(GetMethod<DbMethod>(), parameters);
        internal override sealed IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            => HandleProcess<TDbDataModel, TDbParams, DbMethod>(method, parameters);

        


        internal override void OnBeforeNextInner<TDbParams, DbMethod>(DbMethod method, TDbParams parameters) { }

        protected override void OnBeforeNext<TDbParams, DbMethod>(DbMethod method, TDbParams parameters) { }
        internal sealed override IDbResponse<TDbDataModel> ExecuteNext<TDbFacade, TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
        {
            return DbFacadeCache.Get<TDbFacade>().ExecuteProcess<TDbDataModel, TDbParams, DbMethod>(method, parameters);
        }

        internal sealed override IDbResponse<TDbDataModel> ExecuteNext<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
        {
            OnBeforeNextInner(method, parameters);
            OnBeforeNext(method, parameters);
            return ExecuteNextCore<TDbDataModel, TDbParams, DbMethod>(method, parameters);
        }
        internal override IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters) => null;
        
        
        private IDbResponse<TDbDataModel> HandleProcess<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            IDbResponse<TDbDataModel> response = Process<TDbDataModel, TDbParams, DbMethod>(method, parameters);
            return response != null ? response : ExecuteNext<TDbDataModel, TDbParams, DbMethod>(method, parameters);
        }
        protected virtual IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return null;
        }       
    }
    public abstract class DbFacade<TDbManifest, TDbFacade> : DbFacade<TDbManifest>
        where TDbManifest : DbManifest
        where TDbFacade : DbFacade<TDbManifest>
    {
        internal override sealed void HandleInnerDispose() => GetDbFacade<TDbFacade>().Dispose();
        internal override sealed IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
        {
            return ExecuteNext<TDbFacade, TDbDataModel, TDbParams, DbMethod>(method, parameters);
        }
    }
}
