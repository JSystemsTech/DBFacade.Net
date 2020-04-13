using System.Data;
using System.Threading.Tasks;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;

namespace DBFacade.DataLayer.ConnectionService
{
    public interface IDbConnectionConfig
    {
    }

    internal interface IDbConnectionConfigInternal : IDbConnectionConfig
    {
        IDbConnection DbConnection { get; }

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