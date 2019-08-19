using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System.Threading.Tasks;

namespace DBFacade.Facade.Core
{
    
    public abstract class DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {
        internal abstract IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, DbMethod>()
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteProcess<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        

        internal abstract IDbResponse<TDbDataModel> ExecuteNext<TDbFacade, TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteNext<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;

        internal abstract IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;

        internal abstract void OnBeforeNextInner<TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;

        protected abstract void OnBeforeNext<TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;
    }
}
