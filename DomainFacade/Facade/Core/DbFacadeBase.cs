using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{

    public abstract class DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {
        protected abstract IDbResponse<TDbDataModel> CallDbMethod<TDbDataModel, DbMethod>()
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest;

        protected abstract IDbResponse<TDbDataModel> CallDbMethod<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        protected abstract IDbResponse<TDbDataModel> CallFacadeAPIDbMethod<TDbFacade, TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbFacade : DbFacade<TDbManifest>
            where DbMethod : TDbManifest;

        internal sealed class DbMethodsCache : InstanceResolver<TDbManifest> { }
    }
}
