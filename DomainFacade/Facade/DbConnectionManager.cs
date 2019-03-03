using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;

namespace DomainFacade.Facade
{
    internal sealed class DbConnectionManager<TDbManifest> : DbFacade<TDbManifest>
    where TDbManifest : DbManifest
    {
        protected override IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, DbParams, DbMethod>(DbParams parameters)
        {
            IDbConnectionConfig connectionConfig = DbMethodsCache.GetInstance<DbMethod>().GetConfig().GetDBConnectionConfig();

            if (connectionConfig is ISQL)
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.SQL, TDbDataModel, DbParams, DbMethod>(parameters);
            }
            if (connectionConfig is ISQLite)
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.SQLite, TDbDataModel, DbParams, DbMethod>(parameters);
            }
            else if (connectionConfig is IOleDb)
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.OleDb, TDbDataModel, DbParams, DbMethod>(parameters);
            }
            else if (connectionConfig is IOdbc)
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.Odbc, TDbDataModel, DbParams, DbMethod>(parameters);
            }
            else if (connectionConfig is IOracle)
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.Oracle, TDbDataModel, DbParams, DbMethod>(parameters);
            }
            else
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>, TDbDataModel, DbParams, DbMethod>(parameters);
            }
        }
    }
}
