using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using System.Data;

namespace DomainFacade.Facade.Core
{
    public abstract class DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {

        protected abstract IDbResponse<TDbDataModel> CallFacadeAPIDbMethod<TDbFacade, TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbFacade : DbFacade<TDbManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        protected abstract IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters)
            where TDbDataModel : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        protected abstract IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbMethod>() where TDbDataModel : DbDataModel where DbMethod : TDbManifest;
        protected abstract IDbResponse Transaction<DbParams, DbMethod>(DbParams parameters) where DbParams : IDbParamsModel where DbMethod : TDbManifest;
        protected abstract IDbResponse Transaction<DbMethod>() where DbMethod : TDbManifest;


        protected abstract IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData)
            where TDbDataModel : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        protected abstract IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
            where TDbDataModel : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        protected abstract IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbMethod>(IDataReader testResponseData)
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest;
        protected abstract IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbMethod>(IDataReader testResponseData, object ReturnValue)
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest;
        protected abstract IDbResponse MockTransaction<DbParams, DbMethod>(DbParams parameters)
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        protected abstract IDbResponse MockTransaction<DbParams, DbMethod>(DbParams parameters, object ReturnValue)
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        protected abstract IDbResponse MockTransaction<DbMethod>()
            where DbMethod : TDbManifest;
        protected abstract IDbResponse MockTransaction<DbMethod>(IDataReader testResponseData, object ReturnValue)
            where DbMethod : TDbManifest;
    }
}
