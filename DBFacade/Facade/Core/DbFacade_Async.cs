using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFacade.Facade.Core
{
    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
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
            => await ProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);

        private Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        {
            return Task.Run(() =>
            {
                return HandleProcess<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
            });
        }
    }
}
