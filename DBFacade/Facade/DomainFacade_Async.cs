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
        protected async Task<IDbResponse<TDbDataModel>> FetchAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(parameters);
        }
        protected async Task<IDbResponse<TDbDataModel>> FetchAsync<TDbDataModel, TDbManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbManifestMethod : TDbManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, TDbManifestMethod>();
        }
        protected async Task<IDbResponse> TransactionAsync<TDbParams, TDbManifestMethod>(TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            return await ExecuteProcessAsync<DbDataModel, TDbParams, TDbManifestMethod>(parameters);
        }
        protected async Task<IDbResponse> TransactionAsync<TDbManifestMethod>()
            where TDbManifestMethod : TDbManifest
        {
            return await ExecuteProcessAsync<DbDataModel, TDbManifestMethod>();
        }
        #endregion
        #region Mock Db Calls Async
        protected async Task<IDbResponse<TDbDataModel>> MockFetchAsync<TDbDataModel, TDbParams, TDbManifestMethod, T>(IEnumerable<T> responseData, object returnValue, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await FetchAsync<TDbDataModel, TDbParams, TDbManifestMethod>(parameters);
        }
        protected async Task<IDbResponse<TDbDataModel>> MockFetchAsync<TDbDataModel, TDbManifestMethod, T>(IEnumerable<T> responseData, object returnValue)
            where TDbDataModel : DbDataModel
            where TDbManifestMethod : TDbManifest
        {
            DbParamsModel parameters = new DbParamsModel();
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await FetchAsync<TDbDataModel, DbParamsModel, TDbManifestMethod>(parameters);
        }
        protected async Task<IDbResponse> MockTransactionAsync<TDbParams, TDbManifestMethod>(object returnValue, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            await Task.Run(() => parameters.RunAsTest(returnValue));
            return await TransactionAsync<TDbParams, TDbManifestMethod>(parameters);
        }
        protected async Task<IDbResponse> MockTransactionAsync<TDbManifestMethod>(object returnValue)
            where TDbManifestMethod : TDbManifest
        {
            DbParamsModel parameters = new DbParamsModel();
            await Task.Run(() => parameters.RunAsTest(returnValue));
            return await TransactionAsync<DbParamsModel, TDbManifestMethod>(parameters);
        }
        #endregion

    }
}
