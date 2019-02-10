using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.Facade.Core;
using System;

namespace DomainFacade.Facade
{
    public class DbConnectionManager<TDbManifest> : DbFacade<TDbManifest>.Forwarder<DbConnectionManagerCore<TDbManifest>>where TDbManifest : DbManifest{}

    public sealed class DbConnectionManagerCore<TDbManifest> : DbFacade<TDbManifest>
    where TDbManifest : DbManifest
    {
        protected override TDbResponse CallDbMethodCore<TDbResponse, DbParams, DbMethod>(DbParams parameters)
        {
            Type dbConnectionType = DbMethodsCache.GetInstance<DbMethod>().GetConfig().GetDBConnectionType().BaseType;
            if (dbConnectionType == typeof(DbConnectionCore.SQL))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.SQL, TDbResponse, DbParams, DbMethod>(parameters);
            }
            if (dbConnectionType == typeof(DbConnectionCore.SQLite))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.SQLite, TDbResponse, DbParams, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.OleDb))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.OleDb, TDbResponse, DbParams, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Odbc))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.Odbc, TDbResponse, DbParams, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Oracle))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.Oracle, TDbResponse, DbParams, DbMethod>(parameters);
            }
            else
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>, TDbResponse, DbParams, DbMethod>(parameters);
            }
        }
    }
}
