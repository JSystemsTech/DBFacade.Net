﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DbFacade.Facade.Core;
using DBFacade.Facade.Core;
using System;

namespace DBFacade.Facade
{
    public abstract partial class
        DomainFacade<TDomainManager, TDbMethodManifest> : DbFacade<TDbMethodManifest, TDomainManager>
        where TDomainManager : DomainManager<TDbMethodManifest>
        where TDbMethodManifest : DbMethodManifest
    {
        #region Db Data Actions Async
        protected IAsyncFetch<TDbDataModel, TDbParams> BuildAsyncFetch<TDbDataModel, TDbMethodManifestMethod, TDbParams>()
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new AsyncFetch<TDbDataModel, TDbParams>(
                ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>);
        protected IAsyncFetch<TDbDataModel> BuildAsyncFetch<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new AsyncFetch<TDbDataModel>(
                ExecuteProcessAsync<TDbDataModel, TDbMethodManifestMethod>,
                ExecuteProcessAsync<TDbDataModel, DbParamsModel, TDbMethodManifestMethod>);
        protected IAsyncGenericFetch<TDbDataModel, T1> BuildGenericAsyncFetch<TDbDataModel, TDbMethodManifestMethod, T1>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncFetch<TDbDataModel, T1>(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1>, TDbMethodManifestMethod>);
        protected IAsyncGenericFetch<TDbDataModel, T1, T2> BuildGenericAsyncFetch<TDbDataModel, TDbMethodManifestMethod, T1, T2>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncFetch<TDbDataModel, T1, T2>(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1, T2>, TDbMethodManifestMethod>);
        protected IAsyncGenericFetch<TDbDataModel, T1, T2, T3> BuildGenericAsyncFetch<TDbDataModel, TDbMethodManifestMethod, T1, T2, T3>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncFetch<TDbDataModel, T1, T2, T3>(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1, T2, T3>, TDbMethodManifestMethod>);
        protected IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4> BuildGenericAsyncFetch<TDbDataModel, TDbMethodManifestMethod, T1, T2, T3, T4>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4>(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1, T2, T3, T4>, TDbMethodManifestMethod>);
        protected IAsyncGenericFetch<TDbDataModel, T1, T2, T3, T4, T5> BuildGenericAsyncFetch<TDbDataModel, TDbMethodManifestMethod, T1, T2, T3, T4, T5>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncFetch<TDbDataModel, T1, T2, T3, T4, T5>(
                ExecuteProcessAsync<TDbDataModel, DbParamsModel<T1, T2, T3, T4, T5>, TDbMethodManifestMethod>);



        protected IAsyncTransaction<TDbParams> BuildAsyncTransaction<TDbMethodManifestMethod, TDbParams>()
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new AsyncTransaction<TDbParams>(
                ExecuteProcessAsync<TDbParams, TDbMethodManifestMethod>);
        protected IAsyncGenericTransaction<T1> BuildGenericAsyncTransaction<TDbMethodManifestMethod, T1>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncTransaction<T1>(
                ExecuteProcessAsync<DbParamsModel<T1>, TDbMethodManifestMethod>);
        protected IAsyncGenericTransaction<T1, T2> BuildGenericAsyncTransaction<TDbMethodManifestMethod, T1, T2>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncTransaction<T1, T2>(
                ExecuteProcessAsync<DbParamsModel<T1, T2>, TDbMethodManifestMethod>);
        protected IAsyncGenericTransaction<T1, T2, T3> BuildGenericAsyncTransaction<TDbMethodManifestMethod, T1, T2, T3>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncTransaction<T1, T2, T3>(
                ExecuteProcessAsync<DbParamsModel<T1, T2, T3>, TDbMethodManifestMethod>);
        protected IAsyncGenericTransaction<T1, T2, T3, T4> BuildGenericAsyncTransaction<TDbMethodManifestMethod, T1, T2, T3, T4>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncTransaction<T1, T2, T3, T4>(
                ExecuteProcessAsync<DbParamsModel<T1, T2, T3, T4>, TDbMethodManifestMethod>);
        protected IAsyncGenericTransaction<T1, T2, T3, T4, T5> BuildGenericAsyncTransaction<TDbMethodManifestMethod, T1, T2, T3, T4, T5>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericAsyncTransaction<T1, T2, T3, T4, T5>(
                ExecuteProcessAsync<DbParamsModel<T1, T2, T3, T4, T5>, TDbMethodManifestMethod>);

        protected IAsyncTransaction BuildAsyncTransaction<TDbMethodManifestMethod>()
            where TDbMethodManifestMethod :TDbMethodManifest
            => new AsyncTransaction(
                ExecuteProcessAsync<TDbMethodManifestMethod>,
                ExecuteProcessAsync<DbParamsModel, TDbMethodManifestMethod>);



        [Obsolete("This method will soon be deprecated. Use BuildAsyncFetch.Execute instead.")]
        protected async Task<IDbResponse<TDbDataModel>> FetchAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildAsyncFetch.Execute instead.")]
        protected async Task<IDbResponse<TDbDataModel>> FetchAsync<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<TDbDataModel, TDbMethodManifestMethod>();
        }
        [Obsolete("This method will soon be deprecated. Use BuildAsyncTransaction.Execute instead.")]
        protected async Task<IDbResponse> TransactionAsync<TDbParams, TDbMethodManifestMethod>(TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<DbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildAsyncTransaction.Execute instead.")]
        protected async Task<IDbResponse> TransactionAsync<TDbMethodManifestMethod>()
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return await ExecuteProcessAsync<DbDataModel, TDbMethodManifestMethod>();
        }

        #endregion

        #region Mock Db Calls Async
        [Obsolete("This method will soon be deprecated. Use BuildAsyncFetch.Mock instead.")]
        protected async Task<IDbResponse<TDbDataModel>> MockFetchAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod,
            T>(IEnumerable<T> responseData, object returnValue, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await FetchAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildAsyncFetch.Mock instead.")]
        protected async Task<IDbResponse<TDbDataModel>> MockFetchAsync<TDbDataModel, TDbMethodManifestMethod, T>(
            IEnumerable<T> responseData, object returnValue)
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var parameters = new DbParamsModel();
            await Task.Run(() => parameters.RunAsTest(responseData, returnValue));
            return await FetchAsync<TDbDataModel, DbParamsModel, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildAsyncTransaction.Mock instead.")]
        protected async Task<IDbResponse> MockTransactionAsync<TDbParams, TDbMethodManifestMethod>(object returnValue,
            TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            await Task.Run(() => parameters.RunAsTest(returnValue));
            return await TransactionAsync<TDbParams, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildAsyncTransaction.Mock instead.")]
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