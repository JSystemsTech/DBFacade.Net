using DbFacade.DataLayer.Manifest;
using DbFacade.DataLayer.Models;
using System.Threading.Tasks;

namespace DbFacade.Facade
{
    public abstract partial class
        DomainFacade<TDomainManager, TDbMethodManifest> : DbFacade<TDbMethodManifest, TDomainManager>
        where TDomainManager : DomainManager<TDbMethodManifest>
        where TDbMethodManifest : DbMethodManifest
    {
        #region Db Data Actions Async
        protected async Task<IAsyncFetch<TDbDataModel, TDbParams>> BuildFetchAsync<TDbDataModel, TDbMethodManifestMethod, TDbParams>()
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        => await AsyncFetch<TDbDataModel, TDbParams>.CreateAsync(
                ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>);
        
            
        protected async Task<IAsyncFetch<TDbDataModel>> BuildFetchAsync<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await AsyncFetch<TDbDataModel>.CreateAsync(
                ExecuteProcessAsync<TDbDataModel, TDbMethodManifestMethod>,
                ExecuteProcessAsync<TDbDataModel, DbParamsModel, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericFetch<TDbDataModel, T1>> BuildGenericFetchAsync<TDbDataModel, TDbMethodManifestMethod, T1>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await GenericAsyncFetch<TDbDataModel, T1>.CreateAsync(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1>, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericFetch<TDbDataModel, T1, T2>> BuildGenericFetchAsync<TDbDataModel, TDbMethodManifestMethod, T1, T2>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await GenericAsyncFetch<TDbDataModel, T1, T2>.CreateAsync(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1, T2>, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericFetch<TDbDataModel, T1, T2, T3>> BuildGenericFetchAsync<TDbDataModel, TDbMethodManifestMethod, T1, T2, T3>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await  GenericAsyncFetch<TDbDataModel, T1, T2, T3>.CreateAsync(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1, T2, T3>, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4>> BuildGenericFetchAsync<TDbDataModel, TDbMethodManifestMethod, T1, T2, T3, T4>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4>.CreateAsync(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1, T2, T3, T4>, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4, T5>> BuildGenericFetchAsync<TDbDataModel, TDbMethodManifestMethod, T1, T2, T3, T4, T5>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4, T5>.CreateAsync(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1, T2, T3, T4, T5>, TDbMethodManifestMethod>);



        protected async Task<IAsyncTransaction<TDbParams>> BuildTransactionAsync<TDbMethodManifestMethod, TDbParams>()
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => await AsyncTransaction<TDbParams>.CreateAsync(
                ExecuteProcessAsync<TDbParams, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericTransaction<T1>> BuildGenericTransactionAsync<TDbMethodManifestMethod, T1>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => await GenericAsyncTransaction<T1>.CreateAsync(
                ExecuteProcessAsync<DbParamsModel<T1>, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericTransaction<T1, T2>> BuildGenericTransactionAsync<TDbMethodManifestMethod, T1, T2>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => await GenericAsyncTransaction<T1, T2>.CreateAsync(
                ExecuteProcessAsync<DbParamsModel<T1, T2>, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericTransaction<T1, T2, T3>> BuildGenericTransactionAsync<TDbMethodManifestMethod, T1, T2, T3>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => await GenericAsyncTransaction<T1, T2, T3>.CreateAsync(
                ExecuteProcessAsync<DbParamsModel<T1, T2, T3>, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericTransaction<T1, T2, T3, T4>> BuildGenericTransactionAsync<TDbMethodManifestMethod, T1, T2, T3, T4>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => await GenericAsyncTransaction<T1, T2, T3, T4>.CreateAsync(
                ExecuteProcessAsync<DbParamsModel<T1, T2, T3, T4>, TDbMethodManifestMethod>);
        protected async Task<IAsyncGenericTransaction<T1, T2, T3, T4, T5>> BuildGenericTransactionAsync<TDbMethodManifestMethod, T1, T2, T3, T4, T5>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => await GenericAsyncTransaction<T1, T2, T3, T4, T5>.CreateAsync(
                ExecuteProcessAsync<DbParamsModel<T1, T2, T3, T4, T5>, TDbMethodManifestMethod>);

        protected async Task<IAsyncTransaction> BuildTransactionAsync<TDbMethodManifestMethod>()
            where TDbMethodManifestMethod :TDbMethodManifest
            => await AsyncTransaction.CreateAsync(
                ExecuteProcessAsync<TDbMethodManifestMethod>,
                ExecuteProcessAsync<DbParamsModel, TDbMethodManifestMethod>);
        
        #endregion

    }
}