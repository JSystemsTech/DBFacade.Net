using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using System;
using System.Collections.Generic;

namespace DomainFacade.Facade
{
    public class DbConnectionManager<E> : FacadeAPI<E>.Forwarder<DbConnectionManagerCore<E>>where E : DbMethodsCore{}

    public sealed class DbConnectionManagerCore<E> : FacadeAPI<E>
    where E : DbMethodsCore
    {
        protected override R CallDbMethodCore<U, R, Em>(U parameters)
        {
            Type dbConnectionType = DbMethodsService.GetInstance<Em>().GetConfig().GetDBConnectionType().BaseType;
            if (dbConnectionType == typeof(DbConnectionCore.SQL))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.SQL, R, Em>(parameters);
            }
            if (dbConnectionType == typeof(DbConnectionCore.SQLite))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.SQLite, R, Em>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.OleDb))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.OleDb, R, Em>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Odbc))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.Odbc, R, Em>(parameters);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Oracle))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.Oracle, R, Em>(parameters);
            }
            else
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>, R, Em>(parameters);
            }
        }
    }
}
