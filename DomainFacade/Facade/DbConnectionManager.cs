using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using System;

namespace DomainFacade.Facade
{
   internal sealed class DbConnectionManager<TDbManifest> : DbFacade<TDbManifest>
    where TDbManifest : DbManifest
    {
        protected override IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, DbParams, DbMethod>(DbParams parameters)
        {
            Type dbConnectionType = DbMethodsCache.GetInstance<DbMethod>().GetConfig().GetDBConnectionType().BaseType;
            if (dbConnectionType == typeof(DbConnectionCore.SQL))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.SQL, TDbDataModel, DbParams, DbMethod>(parameters);
            }
            if (dbConnectionType == typeof(DbConnectionCore.SQLite))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.SQLite, TDbDataModel, DbParams, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.OleDb))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.OleDb, TDbDataModel, DbParams, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Odbc))
            {
                return CallFacadeAPIDbMethod<DbConnectionHandler<TDbManifest>.Odbc, TDbDataModel, DbParams, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Oracle))
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
