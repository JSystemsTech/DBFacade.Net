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
        protected override R CallDbMethodCore<U, R>(U parameters, E dbMethod)
        {
            Type dbConnectionType = dbMethod.GetConfig().GetDBConnectionType().BaseType;
            if (dbConnectionType == typeof(DbConnectionCore.SQL))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.SQL, R>(parameters, dbMethod);
            }
            if (dbConnectionType == typeof(DbConnectionCore.SQLite))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.SQLite, R>(parameters, dbMethod);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.OleDb))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.OleDb, R>(parameters, dbMethod);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Odbc))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.Odbc, R>(parameters, dbMethod);
            }
            else if (dbConnectionType == typeof(DbConnectionCore.Oracle))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.Oracle, R>(parameters, dbMethod);
            }
            else
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>, R>(parameters, dbMethod);
            }
        }
    }
}
