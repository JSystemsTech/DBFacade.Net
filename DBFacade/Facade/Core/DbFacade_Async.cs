using DbFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace DBFacade.Facade.Core
{
    public abstract partial class DbFacade<TDbMethodManifest> : DbFacadeBase<TDbMethodManifest>, IDisposable where TDbMethodManifest : DbMethodManifest
    {
        protected async Task InitConnectionConfigAsync<TDbConnectionConfig>(TDbConnectionConfig value)
            where TDbConnectionConfig : IDbConnectionConfig
        => await DbConnectionConfigManager.AddAsync(value);

        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await ExecuteProcessAsync<TDbDataModel, IDbParamsModel, TDbMethodManifestMethod>(GetMethod<TDbMethodManifestMethod>(), DEFAULT_PARAMETERS);

        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(GetMethod<TDbMethodManifestMethod>(), parameters);

        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await HandleProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);


        internal virtual async Task OnBeforeNextInnerAsync<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.Run(() => { return; });
        }
        protected virtual async Task OnBeforeNextAsync<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.Run(() => { return; });
        }
        internal async Task<IDbResponse<TDbDataModel>> ExecuteNextAsync<TDbFacade, TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbMethodManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            TDbFacade next = await DbFacadeCache.GetAsync<TDbFacade>();
            return await next.ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        protected async Task<IDbResponse<TDbDataModel>> NextAsync<TDbFacade, TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbMethodManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        => await ExecuteNextAsync<TDbFacade, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);

        internal async Task<IDbResponse<TDbDataModel>> ExecuteNextAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.WhenAll(OnBeforeNextInnerAsync(method, parameters), OnBeforeNextAsync(method, parameters));
            return await ExecuteNextCoreAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        internal async virtual Task<IDbResponse<TDbDataModel>> ExecuteNextCoreAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        => await GetDefaultAsyncReturn<TDbDataModel, TDbMethodManifestMethod>();


        private async Task<IDbResponse<TDbDataModel>> HandleProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            IDbResponse<TDbDataModel> response =  await ProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);            
            return !response.IsNull ? response : await ExecuteNextAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        private async Task<IDbResponse<TDbDataModel>> GetDefaultAsyncReturn<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await Task.Run(() => new DbResponse<TDbMethodManifestMethod,TDbDataModel>());
        }
        
        protected virtual async Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        => await GetDefaultAsyncReturn<TDbDataModel, TDbMethodManifestMethod>();
    }
}
