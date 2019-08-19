using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System.Data;

namespace DBFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbConnectionConfig
    {
        IDbConnection GetDbConnection();
        IDbResponse<TDbDataModel> ExecuteDbAction<TDbManifest, TDbDataModel, TDbParams, DbMethod>(DbMethod dbMethod, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;        
    }
}
