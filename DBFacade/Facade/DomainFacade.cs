using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Facade.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBFacade.Facade
{
    
    public abstract class DomainFacade<TDbManifest> : DomainFacade<DomainManager<TDbManifest>, TDbManifest>
    where TDbManifest : DbManifest
    { }
    
    public abstract partial class DomainFacade<TDomainManager, TDbManifest> : DbFacade<TDbManifest, TDomainManager>
    where TDomainManager : DomainManager<TDbManifest>
    where TDbManifest : DbManifest
    {   
        #region Db Data Actions
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, TDbParams, TDbManifestMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            return ExecuteProcess<TDbDataModel, TDbParams, TDbManifestMethod>(parameters);
        }
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, TDbManifestMethod>()
            where TDbDataModel : DbDataModel 
            where TDbManifestMethod : TDbManifest
        {
            return ExecuteProcess<TDbDataModel, TDbManifestMethod>();
        }
        protected IDbResponse Transaction<TDbParams, TDbManifestMethod>(TDbParams parameters)
            where TDbParams : IDbParamsModel 
            where TDbManifestMethod : TDbManifest
        {
            return ExecuteProcess<DbDataModel, TDbParams, TDbManifestMethod>(parameters);
        }
        protected IDbResponse Transaction<TDbManifestMethod>()
            where TDbManifestMethod : TDbManifest
        {
            return ExecuteProcess<DbDataModel, TDbManifestMethod>();
        }
        #endregion

        #region Mock Db Calls
        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbParams, TDbManifestMethod, T>(IEnumerable<T> responseData, object returnValue, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, TDbParams, TDbManifestMethod>(parameters);
        }
        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbManifestMethod, T>(IEnumerable<T> responseData, object returnValue)
            where TDbDataModel : DbDataModel
            where TDbManifestMethod : TDbManifest
        {
            DbParamsModel parameters = new DbParamsModel();
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, DbParamsModel, TDbManifestMethod>(parameters);
        }
        protected IDbResponse MockTransaction<TDbParams, TDbManifestMethod>(object returnValue, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            parameters.RunAsTest(returnValue);
            return Transaction<TDbParams, TDbManifestMethod>(parameters);
        }
        protected IDbResponse MockTransaction<TDbManifestMethod>(object returnValue)
            where TDbManifestMethod : TDbManifest
        {
            DbParamsModel parameters = new DbParamsModel();
            parameters.RunAsTest(returnValue);
            return Transaction<DbParamsModel, TDbManifestMethod>(parameters);
        }
        #endregion

    }
}
