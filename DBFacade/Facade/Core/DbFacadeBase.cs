using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;

namespace DBFacade.Facade.Core
{

    public abstract class DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {
        internal abstract IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbManifestMethod : TDbManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, TDbManifestMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;
        

        internal abstract IDbResponse<TDbDataModel> ExecuteNext<TDbFacade, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteNext<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;

        internal abstract void OnBeforeNextInner<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;

        protected abstract void OnBeforeNext<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;
    }
}
