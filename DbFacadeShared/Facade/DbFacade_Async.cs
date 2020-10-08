using System.Threading.Tasks;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.Utils;

namespace DbFacade.Facade
{
    public abstract partial class DbFacade<TDbMethodManifest> 
    {
        protected async Task InitConnectionConfigAsync<TDbConnectionConfig>(TDbConnectionConfig value)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            await DbConnectionConfigManager.AddAsync(value);
        }
        private async Task<TDbMethodManifestMethod> GetMethodAsync<TDbMethodManifestMethod>()
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await Task<TDbMethodManifestMethod>.Factory.StartNew(() => TDbManifestMethodsCache.Get<TDbMethodManifestMethod>());
        }

        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, IDbParamsModel, TDbMethodManifestMethod>(
                await GetMethodAsync<TDbMethodManifestMethod>(), _defaultParameters);
        }

        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
                await GetMethodAsync<TDbMethodManifestMethod>(), parameters);
        }
        internal async Task<IDbResponse> ExecuteProcessAsync<TDbMethodManifestMethod>()
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<DbDataModel, IDbParamsModel, TDbMethodManifestMethod>(
                await GetMethodAsync<TDbMethodManifestMethod>(), _defaultParameters);
        }

        internal async Task<IDbResponse> ExecuteProcessAsync<TDbParams,
            TDbMethodManifestMethod>(TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<DbDataModel, TDbParams, TDbMethodManifestMethod>(
                await GetMethodAsync<TDbMethodManifestMethod>(), parameters);
        }


        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await HandleProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }


        internal virtual async Task OnBeforeNextInnerAsync<TDbParams, TDbMethodManifestMethod>(
            TDbMethodManifestMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.CompletedTask;
        }

        protected virtual async Task OnBeforeNextAsync<TDbParams, TDbMethodManifestMethod>(
            TDbMethodManifestMethod method, TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.CompletedTask;
        }

        internal async Task<IDbResponse<TDbDataModel>> ExecuteNextAsync<TDbFacade, TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbMethodManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var next = await DbFacadeCache.GetAsync<TDbFacade>();
            return await next.ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }

        protected async Task<IDbResponse<TDbDataModel>> NextAsync<TDbFacade, TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbFacade : DbFacade<TDbMethodManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteNextAsync<TDbFacade, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method,
                parameters);
        }

        internal async Task<IDbResponse<TDbDataModel>> ExecuteNextAsync<TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.WhenAll(OnBeforeNextInnerAsync(method, parameters), OnBeforeNextAsync(method, parameters));
            return await ExecuteNextCoreAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }

        internal virtual async Task<IDbResponse<TDbDataModel>> ExecuteNextCoreAsync<TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await GetDefaultAsyncReturn<TDbDataModel, TDbMethodManifestMethod>();
        }


        private async Task<IDbResponse<TDbDataModel>> HandleProcessAsync<TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var response = await ProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
            return !response.IsNull
                ? response
                : await ExecuteNextAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }

        private async Task<IDbResponse<TDbDataModel>> GetDefaultAsyncReturn<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        => await DbResponse<TDbMethodManifestMethod, TDbDataModel>.CreateAsync();
        

        protected virtual async Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        => await GetDefaultAsyncReturn<TDbDataModel, TDbMethodManifestMethod>();
        
    }
}