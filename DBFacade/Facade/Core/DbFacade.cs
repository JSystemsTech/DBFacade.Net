using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Services;
using System;
using System.Threading.Tasks;

namespace DBFacade.Facade.Core
{

    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {
        #region SafeDisposable Support        
        protected override void OnDispose(bool calledFromDispose)
        {
            InstanceResolvers.Get<IDbParamsModel>().Dispose(calledFromDispose);
            DbFacadeCache.Dispose(calledFromDispose);
            TDbManifestMethodsCache.Dispose(calledFromDispose);            
        }

        protected override void OnDisposeComplete()
        {
            defaultParams = null;
            dbFacadeCache = null;
            tDbManifestMethodsCache = null;
        }
        #endregion

        private IDbParamsModel defaultParams { get; set; }
        private IDbParamsModel DEFAULT_PARAMETERS { get { return defaultParams ?? InstanceResolvers.Get<IDbParamsModel>().Get<DbParamsModel>(); } }

        private IInstanceResolver<DbFacade<TDbManifest>> dbFacadeCache { get; set; }
        private IInstanceResolver<DbFacade<TDbManifest>> DbFacadeCache { get { return dbFacadeCache ?? InstanceResolvers.Get<DbFacade<TDbManifest>>(); } }

        private IInstanceResolver<TDbManifest> tDbManifestMethodsCache { get; set; }
        private IInstanceResolver<TDbManifest> TDbManifestMethodsCache { get { return tDbManifestMethodsCache?? InstanceResolvers.Get<TDbManifest>(); } }

        private TDbManifestMethod GetMethod<TDbManifestMethod>() where TDbManifestMethod: TDbManifest => TDbManifestMethodsCache.Get<TDbManifestMethod>();
        internal TDbFacade GetDbFacade<TDbFacade>() where TDbFacade : DbFacade<TDbManifest> => DbFacadeCache.Get<TDbFacade>();
        
        internal override sealed IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbManifestMethod>()
            => ExecuteProcess<TDbDataModel, IDbParamsModel, TDbManifestMethod>(GetMethod<TDbManifestMethod>(),DEFAULT_PARAMETERS);
        internal override sealed IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, TDbManifestMethod>(TDbParams parameters)
            => ExecuteProcess<TDbDataModel, TDbParams, TDbManifestMethod>(GetMethod<TDbManifestMethod>(), parameters);
        internal override sealed IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            => HandleProcess<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);

        


        internal override void OnBeforeNextInner<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters) { }
        protected override void OnBeforeNext<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters) { }

        internal sealed override IDbResponse<TDbDataModel> ExecuteNext<TDbFacade, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            return DbFacadeCache.Get<TDbFacade>().ExecuteProcess<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
        internal sealed override IDbResponse<TDbDataModel> ExecuteNext<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            OnBeforeNextInner(method, parameters);
            OnBeforeNext(method, parameters);
            return ExecuteNextCore<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }

        internal override IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters) => null;
        
        
        private IDbResponse<TDbDataModel> HandleProcess<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            IDbResponse<TDbDataModel> response = Process<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
            return response != null ? response : ExecuteNext<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
        protected virtual IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            return null;
        }       
    }
    public abstract class DbFacade<TDbManifest, TDbFacade> : DbFacade<TDbManifest>
        where TDbManifest : DbManifest
        where TDbFacade : DbFacade<TDbManifest>
    {
        protected override void OnDispose(bool calledFromDispose)
        {
            GetDbFacade<TDbFacade>().Dispose(calledFromDispose);
            base.OnDispose(calledFromDispose);
        }        
        internal override sealed IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            return ExecuteNext<TDbFacade, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
        internal override sealed async Task<IDbResponse<TDbDataModel>> ExecuteNextCoreAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            return await ExecuteNextAsync<TDbFacade, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
    }
}
