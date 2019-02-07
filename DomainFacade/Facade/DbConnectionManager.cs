using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using System;
using System.Collections.Generic;

namespace DomainFacade.Facade
{
    public class DbConnectionManager<DbMethodGroup> : DbFacade<DbMethodGroup>.Forwarder<DbConnectionManagerCore<DbMethodGroup>>where DbMethodGroup : DbMethodsCore{}

    public sealed class DbConnectionManagerCore<DbMethodGroup> : DbFacade<DbMethodGroup>
    where DbMethodGroup : DbMethodsCore
    {
        protected override TDbResponse CallDbMethodCore<DbParams, TDbResponse, DbMethod>(DbParams parameters)
        {
            Type dbConnectionType = DbMethodsCache.GetInstance<DbMethod>().GetConfig().GetDBConnectionType().BaseType;
            if (dbConnectionType == typeof(DbConnectionCore.SQL))
            {
                return CallFacadeAPIDbMethod<DbParams, DbConnectionHandler<DbMethodGroup>.SQL, TDbResponse, DbMethod>(parameters);
            }
            if (dbConnectionType == typeof(DbConnectionCore.SQLite))
            {
                return CallFacadeAPIDbMethod<DbParams, DbConnectionHandler<DbMethodGroup>.SQLite, TDbResponse, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.OleDb))
            {
                return CallFacadeAPIDbMethod<DbParams, DbConnectionHandler<DbMethodGroup>.OleDb, TDbResponse, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Odbc))
            {
                return CallFacadeAPIDbMethod<DbParams, DbConnectionHandler<DbMethodGroup>.Odbc, TDbResponse, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Oracle))
            {
                return CallFacadeAPIDbMethod<DbParams, DbConnectionHandler<DbMethodGroup>.Oracle, TDbResponse, DbMethod>(parameters);
            }
            else
            {
                return CallFacadeAPIDbMethod<DbParams, DbConnectionHandler<DbMethodGroup>, TDbResponse, DbMethod>(parameters);
            }
        }
    }
}
