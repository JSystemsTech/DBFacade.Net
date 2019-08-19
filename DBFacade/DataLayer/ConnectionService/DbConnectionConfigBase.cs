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
        public virtual IDbResponse<TDbDataModel> ExecuteDbAction<TDbManifest, TDbDataModel, TDbParams, DbMethod>(DbMethod dbMethod, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        { return null; }
        
    }
}
