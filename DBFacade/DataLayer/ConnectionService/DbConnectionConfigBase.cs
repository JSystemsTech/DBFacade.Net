using System.Data;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Manifest;

namespace DBFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    public class DbConnectionConfigBase
    {
        public virtual IDbConnection GetDbConnection() { return null; }
        public virtual IDbResponse<TDbDataModel> ExecuteDbAction<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        { return null; }
        
    }
}
