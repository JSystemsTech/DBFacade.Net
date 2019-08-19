using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Facade.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBFacade.Facade
{

    public abstract partial class DomainFacade<TDomainManager, TDbManifest> : DbFacade<TDbManifest, TDomainManager>
    where TDomainManager : DomainManager<TDbManifest>
    where TDbManifest : DbManifest
    {
        #region Db Data Actions Async
        protected async Task<IDbResponse<TDbDataModel>> FetchAsync<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, TDbParams, DbMethod>(parameters);
        }
        protected async Task<IDbResponse<TDbDataModel>> FetchAsync<TDbDataModel, DbMethod>()
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, DbMethod>();
        }
        protected async Task<IDbResponse> TransactionAsync<TDbParams, DbMethod>(TDbParams parameters)
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return await ExecuteProcessAsync<DbDataModel, TDbParams, DbMethod>(parameters);
        }
        protected async Task<IDbResponse> TransactionAsync<DbMethod>()
            where DbMethod : TDbManifest
        {
            return await ExecuteProcessAsync<DbDataModel, DbMethod>();
        }
        #endregion
        #region Mock Db Calls Async
        protected async Task<IDbResponse<TDbDataModel>> MockFetchAsync<TDbDataModel, TDbParams, DbMethod, T>(IEnumerable<T> responseData, object returnValue, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            parameters.RunAsTest(responseData, returnValue);
            return await FetchAsync<TDbDataModel, TDbParams, DbMethod>(parameters);
        }
        protected async Task<IDbResponse<TDbDataModel>> MockFetchAsync<TDbDataModel, DbMethod, T>(IEnumerable<T> responseData, object returnValue)
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest
        {
            DbParamsModel parameters = new DbParamsModel();
            parameters.RunAsTest(responseData, returnValue);
            return await FetchAsync<TDbDataModel, DbParamsModel, DbMethod>(parameters);
        }
        protected async Task<IDbResponse> MockTransactionAsync<TDbParams, DbMethod>(object returnValue, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            parameters.RunAsTest(returnValue);
            return await TransactionAsync<TDbParams, DbMethod>(parameters);
        }
        protected async Task<IDbResponse> MockTransactionAsync<DbMethod>(object returnValue)
            where DbMethod : TDbManifest
        {
            DbParamsModel parameters = new DbParamsModel();
            parameters.RunAsTest(returnValue);
            return await TransactionAsync<DbParamsModel, DbMethod>(parameters);
        }
        #endregion

    }
}
