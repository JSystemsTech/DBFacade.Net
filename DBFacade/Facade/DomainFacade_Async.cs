using System.Collections.Generic;
using System.Threading.Tasks;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Facade.Core;

namespace DBFacade.Facade
{
    public abstract partial class
        DomainFacade<TDomainManager, TDbMethodManifest> : DbFacade<TDbMethodManifest, TDomainManager>
        where TDomainManager : DomainManager<TDbMethodManifest>
        where TDbMethodManifest : DbMethodManifest
    {
        #region Db Data Actions Async

        protected async Task<IDbResponse<TDbDataModel>> FetchAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }

        protected async Task<IDbResponse<TDbDataModel>> FetchAsync<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, TDbMethodManifestMethod>();
        }

        protected async Task<IDbResponse> TransactionAsync<TDbParams, TDbMethodManifestMethod>(TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<DbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }

        protected async Task<IDbResponse> TransactionAsync<TDbMethodManifestMethod>()
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<DbDataModel, TDbMethodManifestMethod>();
        }

        #endregion

        #region Mock Db Calls Async

        protected async Task<IDbResponse<TDbDataModel>> MockFetchAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod,
            T>(IEnumerable<T> responseData, object returnValue, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await FetchAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }

        protected async Task<IDbResponse<TDbDataModel>> MockFetchAsync<TDbDataModel, TDbMethodManifestMethod, T>(
            IEnumerable<T> responseData, object returnValue)
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var parameters = new DbParamsModel();
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await FetchAsync<TDbDataModel, DbParamsModel, TDbMethodManifestMethod>(parameters);
        }

        protected async Task<IDbResponse> MockTransactionAsync<TDbParams, TDbMethodManifestMethod>(object returnValue,
            TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.Run(() => parameters.RunAsTest(returnValue));
            return await TransactionAsync<TDbParams, TDbMethodManifestMethod>(parameters);
        }

        protected async Task<IDbResponse> MockTransactionAsync<TDbMethodManifestMethod>(object returnValue)
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var parameters = new DbParamsModel();
            await Task.Run(() => parameters.RunAsTest(returnValue));
            return await TransactionAsync<DbParamsModel, TDbMethodManifestMethod>(parameters);
        }

        #endregion
    }
}