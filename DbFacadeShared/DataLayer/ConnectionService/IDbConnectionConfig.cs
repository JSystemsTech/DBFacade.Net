using System.Data;
using System.Threading.Tasks;
using DbFacade.DataLayer.Manifest;
using DbFacade.DataLayer.Models;
using DbFacade.Utils;

namespace DbFacade.DataLayer.ConnectionService
{
    public interface IDbConnectionConfig: IAsyncInit
    {
    }

    internal interface IDbConnectionConfigInternal : IDbConnectionConfig
    {
        IDbConnection GetDbConnection(IDbParamsModel parameters);
        IDbResponse<TDbDataModel> ExecuteDbAction<TDbMethodManifest, TDbDataModel, TDbParams, TDbMethodManifestMethod>(
            TDbMethodManifestMethod method, TDbParams parameters)
            where TDbMethodManifest : DbMethodManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest;

        Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbMethodManifest, TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbMethodManifest : DbMethodManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest;
    }
}