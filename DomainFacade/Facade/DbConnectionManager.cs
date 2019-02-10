using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Facade.Core;
using System;

namespace DomainFacade.Facade
{
    public class DbConnectionManager<DbMethodGroup> : DbFacade<DbMethodGroup>.Forwarder<DbConnectionManagerCore<DbMethodGroup>>where DbMethodGroup : DbMethodsCore{}

    public sealed class DbConnectionManagerCore<DbMethodGroup> : DbFacade<DbMethodGroup>
    where DbMethodGroup : DbMethodsCore
    {
        protected override TDbResponse CallDbMethodCore<TDbResponse, DbParams, DbMethod>(DbParams parameters)
        {
            Type dbConnectionType = DbMethodsCache.GetInstance<DbMethod>().GetConfig().GetDBConnectionType().BaseType;
            if (dbConnectionType == typeof(DbConnectionCore.SQL))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<DbMethodGroup>.SQL, TDbResponse, DbParams, DbMethod>(parameters);
            }
            if (dbConnectionType == typeof(DbConnectionCore.SQLite))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<DbMethodGroup>.SQLite, TDbResponse, DbParams, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.OleDb))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<DbMethodGroup>.OleDb, TDbResponse, DbParams, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Odbc))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<DbMethodGroup>.Odbc, TDbResponse, DbParams, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Oracle))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<DbMethodGroup>.Oracle, TDbResponse, DbParams, DbMethod>(parameters);
            }
            else
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<DbMethodGroup>, TDbResponse, DbParams, DbMethod>(parameters);
            }
        }
    }
}
