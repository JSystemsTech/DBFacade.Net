using DbFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Factories;
using DBFacade.Services;
using System.Threading.Tasks;

namespace DBFacade.Facade.Core
{

    public abstract partial class DbFacade<TDbMethodManifest> : DbFacadeBase<TDbMethodManifest> where TDbMethodManifest : DbMethodManifest
    {
        #region SafeDisposable Support        
        protected override void OnDispose(bool calledFromDispose)
        {
            DbFacadeCache.Dispose(calledFromDispose);
            TDbManifestMethodsCache.Dispose(calledFromDispose);            
        }

        protected override void OnDisposeComplete() { }
        #endregion

        private IDbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();

        private IInstanceResolver<DbFacade<TDbMethodManifest>> DbFacadeCache => InstanceResolverFactory.Get<DbFacade<TDbMethodManifest>>();


        private IInstanceResolver<TDbMethodManifest> TDbManifestMethodsCache => InstanceResolverFactory.Get<TDbMethodManifest>();

        private TDbMethodManifestMethod GetMethod<TDbMethodManifestMethod>() where TDbMethodManifestMethod: TDbMethodManifest => TDbManifestMethodsCache.Get<TDbMethodManifestMethod>();
        internal TDbFacade GetDbFacade<TDbFacade>() where TDbFacade : DbFacade<TDbMethodManifest> => DbFacadeCache.Get<TDbFacade>();
        protected void InitConnectionConfig<TDbConnectionConfig>(TDbConnectionConfig value)
        where TDbConnectionConfig : IDbConnectionConfig
        => DbConnectionConfigManager.Add(value);

        internal override sealed IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbMethodManifestMethod>()
            => ExecuteProcess<TDbDataModel, IDbParamsModel, TDbMethodManifestMethod>(GetMethod<TDbMethodManifestMethod>(),DEFAULT_PARAMETERS);
        internal override sealed IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbParams parameters)
            => ExecuteProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(GetMethod<TDbMethodManifestMethod>(), parameters);
        internal override sealed IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            => HandleProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);


              


        internal override void OnBeforeNextInner<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters) { }
        protected override void OnBeforeNext<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters) { }

        protected IDbResponse<TDbDataModel> Next<TDbFacade, TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbMethodManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        => ExecuteNext<TDbFacade, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        
        internal sealed override IDbResponse<TDbDataModel> ExecuteNext<TDbFacade, TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            return DbFacadeCache.Get<TDbFacade>().ExecuteProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        internal sealed override IDbResponse<TDbDataModel> ExecuteNext<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            OnBeforeNextInner(method, parameters);
            OnBeforeNext(method, parameters);
            return ExecuteNextCore<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }

        internal override IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters) => null;
        
        
        private IDbResponse<TDbDataModel> HandleProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            IDbResponse<TDbDataModel> response = Process<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
            return response != null ? response : ExecuteNext<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        protected virtual IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return null;
        }       
    }
    public abstract class DbFacade<TDbMethodManifest, TDbFacade> : DbFacade<TDbMethodManifest>
        where TDbMethodManifest : DbMethodManifest
        where TDbFacade : DbFacade<TDbMethodManifest>
    {
        protected override void OnDispose(bool calledFromDispose)
        {
            GetDbFacade<TDbFacade>().Dispose(calledFromDispose);
            base.OnDispose(calledFromDispose);
        }        
        internal override sealed IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            return ExecuteNext<TDbFacade, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        internal override sealed async Task<IDbResponse<TDbDataModel>> ExecuteNextCoreAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            return await ExecuteNextAsync<TDbFacade, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
    }
}
