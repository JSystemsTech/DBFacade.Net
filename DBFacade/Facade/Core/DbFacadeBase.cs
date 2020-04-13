using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;

namespace DBFacade.Facade.Core
{
    public abstract class DbFacadeBase<TDbMethodManifest> : SafeDisposableBase
        where TDbMethodManifest : DbMethodManifest
    {
        internal abstract IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest;


        internal abstract IDbResponse<TDbDataModel> ExecuteNext<TDbFacade, TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbMethodManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteNext<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest;

        internal abstract void OnBeforeNextInner<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method,
            TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest;

        protected abstract void OnBeforeNext<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method,
            TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest;
    }
}