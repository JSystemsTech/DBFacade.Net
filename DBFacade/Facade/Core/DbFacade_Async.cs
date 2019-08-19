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
        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, DbMethod>()
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest
            => await ExecuteProcessAsync<TDbDataModel, IDbParamsModel, DbMethod>(GetMethod<DbMethod>(), DEFAULT_PARAMETERS);

        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
            => await ExecuteProcessAsync<TDbDataModel, TDbParams, DbMethod>(GetMethod<DbMethod>(), parameters);

        internal async Task<IDbResponse<TDbDataModel>> ExecuteProcessAsync<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
            => await ProcessAsync<TDbDataModel, TDbParams, DbMethod>(method, parameters);

        private Task<IDbResponse<TDbDataModel>> ProcessAsync<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return Task.Run(() =>
            {
                return HandleProcess<TDbDataModel, TDbParams, DbMethod>(method, parameters);
            });
        }
    }
}
