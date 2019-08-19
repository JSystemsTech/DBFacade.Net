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
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return ExecuteProcess<TDbDataModel, TDbParams, DbMethod>(parameters);
        }
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbMethod>()
            where TDbDataModel : DbDataModel 
            where DbMethod : TDbManifest
        {
            return ExecuteProcess<TDbDataModel, DbMethod>();
        }
        protected IDbResponse Transaction<TDbParams, DbMethod>(TDbParams parameters)
            where TDbParams : IDbParamsModel 
            where DbMethod : TDbManifest
        {
            return ExecuteProcess<DbDataModel, TDbParams, DbMethod>(parameters);
        }
        protected IDbResponse Transaction<DbMethod>()
            where DbMethod : TDbManifest
        {
            return ExecuteProcess<DbDataModel, DbMethod>();
        }
        #endregion

        #region Mock Db Calls
        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbParams, DbMethod, T>(IEnumerable<T> responseData, object returnValue, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, TDbParams, DbMethod>(parameters);
        }
        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbMethod, T>(IEnumerable<T> responseData, object returnValue)
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest
        {
            DbParamsModel parameters = new DbParamsModel();
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, DbParamsModel, DbMethod>(parameters);
        }
        protected IDbResponse MockTransaction<TDbParams, DbMethod>(object returnValue, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            parameters.RunAsTest(returnValue);
            return Transaction<TDbParams, DbMethod>(parameters);
        }
        protected IDbResponse MockTransaction<DbMethod>(object returnValue)
            where DbMethod : TDbManifest
        {
            DbParamsModel parameters = new DbParamsModel();
            parameters.RunAsTest(returnValue);
            return Transaction<DbParamsModel, DbMethod>(parameters);
        }
        #endregion

    }
}
