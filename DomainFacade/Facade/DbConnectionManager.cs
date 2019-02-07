using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using System;
using System.Collections.Generic;

namespace DomainFacade.Facade
{
    public class DbConnectionManager<DbMethodGroup> : FacadeAPI<DbMethodGroup>.Forwarder<DbConnectionManagerCore<DbMethodGroup>>where DbMethodGroup : DbMethodsCore{}

    public sealed class DbConnectionManagerCore<DbMethodGroup> : FacadeAPI<DbMethodGroup>
    where DbMethodGroup : DbMethodsCore
    {
        protected override R CallDbMethodCore<U, R, DbMethod>(U parameters)
        {
            Type dbConnectionType = DbMethodsService.GetInstance<DbMethod>().GetConfig().GetDBConnectionType().BaseType;
            if (dbConnectionType == typeof(DbConnectionCore.SQL))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<DbMethodGroup>.SQL, R, DbMethod>(parameters);
            }
            if (dbConnectionType == typeof(DbConnectionCore.SQLite))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<DbMethodGroup>.SQLite, R, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.OleDb))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<DbMethodGroup>.OleDb, R, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Odbc))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<DbMethodGroup>.Odbc, R, DbMethod>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Oracle))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<DbMethodGroup>.Oracle, R, DbMethod>(parameters);
            }
            else
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>, R, Em>(parameters);
            }
        }
    }
}
