using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Facade.Core;
using System;

namespace DomainFacade.Facade
{
    public class DataManager<E> : DataManagerCore<E> where E : DbMethodsCore {
        
    }

    public class DataManagerCore<E> : FacadeAPIMiddleMan<E, DbConnectionHandler<E>>
    where E : DbMethodsCore
    {
        protected override void OnBeforeForward<U>(U parameters, E dbMethod){}
        protected override R CallDbMethodCore<U, R>(U parameters, E dbMethod)
        {
            OnBeforeForward(parameters, dbMethod);
            Type dbConnectionType = dbMethod.GetConfig().GetDBConnectionType().BaseType;
            if (dbConnectionType == typeof(DbConnectionCore.SQL))
            {
                return CallFacadeAPIDbMethod<U, DbConnectionHandler<E>.Sql, R>(parameters, dbMethod);
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
