using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace DBFacade.Facade.Core
{
    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest>, IDisposable where TDbManifest : DbManifest
    {
        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbManifestMethod : TDbManifest
            => await ExecuteProcessAsync<TDbDataModel, IDbParamsModel, TDbManifestMethod>(GetMethod<TDbManifestMethod>(), DEFAULT_PARAMETERS);

        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
            => await ExecuteProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(GetMethod<TDbManifestMethod>(), parameters);

        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
            => await HandleProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);


        internal virtual async Task OnBeforeNextInnerAsync<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            await Task.Run(() => { return; });
        }
        protected virtual async Task OnBeforeNextAsync<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            await Task.Run(() => { return; });
        }
        internal async Task<IDbResponse<TDbDataModel>> ExecuteNextAsync<TDbFacade, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            TDbFacade next = await DbFacadeCache.GetAsync<TDbFacade>();
            return await next.ExecuteProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
        protected async Task<IDbResponse<TDbDataModel>> NextAsync<TDbFacade, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        => await ExecuteNextAsync<TDbFacade, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);

        internal async Task<IDbResponse<TDbDataModel>> ExecuteNextAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            await Task.WhenAll(OnBeforeNextInnerAsync(method, parameters), OnBeforeNextAsync(method, parameters));
            return await ExecuteNextCoreAsync<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
        internal async virtual Task<IDbResponse<TDbDataModel>> ExecuteNextCoreAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        => await GetDefaultAsyncReturn<TDbDataModel, TDbManifestMethod>();


        private async Task<IDbResponse<TDbDataModel>> HandleProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            IDbResponse<TDbDataModel> response =  await ProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);            
            return !response.IsNull ? response : await ExecuteNextAsync<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
        private async Task<IDbResponse<TDbDataModel>> GetDefaultAsyncReturn<TDbDataModel, TDbManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbManifestMethod : TDbManifest
        {
            return await Task.Run(() => new DbResponse<TDbManifestMethod,TDbDataModel>());
        }
        
        protected virtual async Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        => await GetDefaultAsyncReturn<TDbDataModel, TDbManifestMethod>();
    }
}
