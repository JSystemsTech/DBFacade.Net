using System.Collections.Generic;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DbFacade.Facade.Core;
using DBFacade.Facade.Core;
using System;

namespace DBFacade.Facade
{
    public abstract class
        DomainFacade<TDbMethodManifest> : DomainFacade<DefaultDomainManager<TDbMethodManifest>, TDbMethodManifest>
        where TDbMethodManifest : DbMethodManifest
    {
    }

    public abstract partial class
        DomainFacade<TDomainManager, TDbMethodManifest> : DbFacade<TDbMethodManifest, TDomainManager>
        where TDomainManager : DomainManager<TDbMethodManifest>
        where TDbMethodManifest : DbMethodManifest
    {
        #region Db Data Actions
        protected IFetch<TDbDataModel, TDbParams> BuildFetch<TDbDataModel, TDbMethodManifestMethod,TDbParams>()
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        => new Fetch<TDbDataModel, TDbParams>(
                ExecuteProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>);
        protected IGenericFetch<TDbDataModel, T1> BuildGenericFetch<TDbDataModel, TDbMethodManifestMethod, T1>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericFetch<TDbDataModel, T1>(
                ExecuteProcess<TDbDataModel, DbParamsModel<T1>, TDbMethodManifestMethod>);
        protected IGenericFetch<TDbDataModel, T1, T2> BuildGenericFetch<TDbDataModel, TDbMethodManifestMethod, T1, T2>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericFetch<TDbDataModel, T1, T2>(
                ExecuteProcess<TDbDataModel, DbParamsModel<T1, T2>, TDbMethodManifestMethod>);
        protected IGenericFetch<TDbDataModel, T1, T2, T3> BuildGenericFetch<TDbDataModel, TDbMethodManifestMethod, T1, T2, T3>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericFetch<TDbDataModel, T1, T2, T3>(
                ExecuteProcess<TDbDataModel, DbParamsModel<T1, T2, T3>, TDbMethodManifestMethod>);
        protected IGenericFetch<TDbDataModel, T1, T2, T3, T4> BuildGenericFetch<TDbDataModel, TDbMethodManifestMethod, T1, T2, T3, T4>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericFetch<TDbDataModel, T1, T2, T3, T4>(
                ExecuteProcess<TDbDataModel, DbParamsModel<T1, T2, T3, T4>, TDbMethodManifestMethod>);
        protected IGenericFetch<TDbDataModel, T1, T2, T3, T4, T5> BuildGenericFetch<TDbDataModel, TDbMethodManifestMethod, T1, T2, T3, T4, T5>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericFetch<TDbDataModel, T1, T2, T3, T4, T5>(
                ExecuteProcess<TDbDataModel, DbParamsModel<T1, T2, T3, T4, T5>, TDbMethodManifestMethod>);


        protected IFetch<TDbDataModel> BuildFetch<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new Fetch<TDbDataModel>(
                ExecuteProcess<TDbDataModel, TDbMethodManifestMethod>,
                ExecuteProcess<TDbDataModel, DbParamsModel, TDbMethodManifestMethod>);
        protected ITransaction<TDbParams> BuildTransaction<TDbMethodManifestMethod, TDbParams>()
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
            => new Transaction<TDbParams>(
                ExecuteProcess<TDbParams, TDbMethodManifestMethod>);
        protected IGenericTransaction<T1> BuildGenericTransaction<TDbMethodManifestMethod, T1>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericTransaction<T1>(
                ExecuteProcess<DbParamsModel<T1>, TDbMethodManifestMethod>);
        protected IGenericTransaction<T1, T2> BuildGenericTransaction<TDbMethodManifestMethod, T1, T2>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericTransaction<T1, T2>(
                ExecuteProcess<DbParamsModel<T1, T2>, TDbMethodManifestMethod>);
        protected IGenericTransaction<T1, T2, T3> BuildGenericTransaction<TDbMethodManifestMethod, T1, T2, T3>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericTransaction<T1, T2, T3>(
                ExecuteProcess<DbParamsModel<T1, T2, T3>, TDbMethodManifestMethod>);
        protected IGenericTransaction<T1, T2, T3, T4> BuildGenericTransaction<TDbMethodManifestMethod, T1, T2, T3, T4>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericTransaction<T1, T2, T3, T4>(
                ExecuteProcess<DbParamsModel<T1, T2, T3, T4>, TDbMethodManifestMethod>);
        protected IGenericTransaction<T1, T2, T3, T4, T5> BuildGenericTransaction<TDbMethodManifestMethod, T1, T2, T3, T4, T5>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new GenericTransaction<T1, T2, T3, T4, T5>(
                ExecuteProcess<DbParamsModel<T1, T2, T3, T4, T5>, TDbMethodManifestMethod>);

        protected ITransaction BuildTransaction<TDbMethodManifestMethod>()
            where TDbMethodManifestMethod : TDbMethodManifest
            => new Transaction(
                ExecuteProcess<TDbMethodManifestMethod>,
                ExecuteProcess<DbParamsModel, TDbMethodManifestMethod>);


        [Obsolete("This method will soon be deprecated. Use BuildFetch.Execute instead.")]
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return ExecuteProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildFetch.Execute instead.")]
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return ExecuteProcess<TDbDataModel, TDbMethodManifestMethod>();
        }
        [Obsolete("This method will soon be deprecated. Use BuildTransaction.Execute instead.")]
        protected IDbResponse Transaction<TDbParams, TDbMethodManifestMethod>(TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return ExecuteProcess<DbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildTransaction.Execute instead.")]
        protected IDbResponse Transaction<TDbMethodManifestMethod>()
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return ExecuteProcess<DbDataModel, TDbMethodManifestMethod>();
        }

        #endregion

        #region Mock Db Calls
        [Obsolete("This method will soon be deprecated. Use BuildFetch.Mock instead.")]
        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbParams, TDbMethodManifestMethod, T>(
            IEnumerable<T> responseData, TDbParams parameters, object returnValue = null)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildFetch.Mock instead.")]
        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbMethodManifestMethod, T>(
            IEnumerable<T> responseData, object returnValue = null)
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var parameters = new DbParamsModel();
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, DbParamsModel, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildFetch.Mock instead.")]
        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbParams, TDbMethodManifestMethod, T>(
            T responseData, TDbParams parameters, object returnValue = null)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildFetch.Mock instead.")]
        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbMethodManifestMethod, T>(T responseData,
            object returnValue = null)
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var parameters = new DbParamsModel();
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, DbParamsModel, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildTransaction.Mock instead.")]
        protected IDbResponse MockTransaction<TDbParams, TDbMethodManifestMethod>(TDbParams parameters,
            object returnValue = null)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            parameters.RunAsTest(returnValue);
            return Transaction<TDbParams, TDbMethodManifestMethod>(parameters);
        }
        [Obsolete("This method will soon be deprecated. Use BuildTransaction.Mock instead.")]
        protected IDbResponse MockTransaction<TDbMethodManifestMethod>(object returnValue = null)
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var parameters = new DbParamsModel();
            parameters.RunAsTest(returnValue);
            return Transaction<DbParamsModel, TDbMethodManifestMethod>(parameters);
        }

        #endregion
    }
}