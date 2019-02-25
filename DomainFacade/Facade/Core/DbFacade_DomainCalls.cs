using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;
using System.Data;

namespace DomainFacade.Facade.Core
{
    
    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {        
        
        protected sealed override IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters)
        {
            return CallDbMethod<TDbDataModel, DbParams, DbMethod>(parameters);
        }
        protected sealed override IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbMethod>()
        {
            return CallDbMethod<TDbDataModel, DbMethod>();
        }
        protected sealed override IDbResponse Transaction<DbParams, DbMethod>(DbParams parameters) 
        {
            return CallDbMethod<DbDataModel, DbParams, DbMethod>(parameters);
        }
        protected sealed override IDbResponse Transaction<DbMethod>()
        {
            return CallDbMethod<DbDataModel, DbMethod>();
        }

    }
}
