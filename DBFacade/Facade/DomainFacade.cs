using System.Collections.Generic;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Facade.Core;

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

        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return ExecuteProcess<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }

        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, TDbMethodManifestMethod>()
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return ExecuteProcess<TDbDataModel, TDbMethodManifestMethod>();
        }

        protected IDbResponse Transaction<TDbParams, TDbMethodManifestMethod>(TDbParams parameters)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return ExecuteProcess<DbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }

        protected IDbResponse Transaction<TDbMethodManifestMethod>()
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            return ExecuteProcess<DbDataModel, TDbMethodManifestMethod>();
        }

        #endregion

        #region Mock Db Calls

        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbParams, TDbMethodManifestMethod, T>(
            IEnumerable<T> responseData, TDbParams parameters, object returnValue = null)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }

        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbMethodManifestMethod, T>(
            IEnumerable<T> responseData, object returnValue = null)
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var parameters = new DbParamsModel();
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, DbParamsModel, TDbMethodManifestMethod>(parameters);
        }

        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbParams, TDbMethodManifestMethod, T>(
            T responseData, TDbParams parameters, object returnValue = null)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, TDbParams, TDbMethodManifestMethod>(parameters);
        }

        protected IDbResponse<TDbDataModel> MockFetch<TDbDataModel, TDbMethodManifestMethod, T>(T responseData,
            object returnValue = null)
            where TDbDataModel : DbDataModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            var parameters = new DbParamsModel();
            parameters.RunAsTest(responseData, returnValue);
            return Fetch<TDbDataModel, DbParamsModel, TDbMethodManifestMethod>(parameters);
        }

        protected IDbResponse MockTransaction<TDbParams, TDbMethodManifestMethod>(TDbParams parameters,
            object returnValue = null)
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        {
            parameters.RunAsTest(returnValue);
            return Transaction<TDbParams, TDbMethodManifestMethod>(parameters);
        }

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