using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System.Data;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.ConnectionService
{
    public interface IDbConnectionConfig { }
    internal interface IDbConnectionConfigInternal: IDbConnectionConfig
    {
        IDbConnection DbConnection { get; }
        
        IDbResponse<TDbDataModel> ExecuteDbAction<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;
        
        Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;
    }
}
