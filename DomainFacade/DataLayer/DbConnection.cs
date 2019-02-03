using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Configuration;
using System.Collections.Generic;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Facade;
using DomainFacade.DataLayer.Models;
using System.Data;
using DomainFacade.DataLayer.Models.Attributes;
using System;
using System.Dynamic;

namespace DomainFacade.DataLayer
{

    public interface IDbConnectionConfig<Con> where Con : DbConnection
    {
        string[] GetAvailableStoredProcs();
        Con GetDbConnection();
    }
    public abstract class DbConnectionCore
    {
        public string[] AvailableStoredProcs { get; private set; }
        public void SetAvailableStoredProcs(string[] storedProcs)
        {
            if(AvailableStoredProcs == null)
            {
                AvailableStoredProcs = storedProcs;
            }
        }
        public DbConnection GetDbConnection()
        {
            return GetDbConnectionCore<DbConnection>();
        }
        protected Con GetDbConnectionCore<Con>() where Con : DbConnection
        {
            string provider = GetDbConnectionProviderInvariantCore();
            string connectionString = GetDbConnectionStringCore();

            if (GetConnectionStringName() != string.Empty)
            {
                provider = GetConnectionStringSettings().ProviderName;
                connectionString = GetConnectionStringSettings().ConnectionString;
            }

            Con dbConnection = (Con)DbProviderFactories.GetFactory(provider).CreateConnection();
            dbConnection.ConnectionString = connectionString;
            return dbConnection;
        }
        protected abstract string GetDbConnectionStringCore();
        protected abstract string GetDbConnectionProviderInvariantCore();

        protected abstract string GetConnectionStringName();
        protected ConnectionStringSettings GetConnectionStringSettings()
        {
            return ConfigurationManager.ConnectionStrings[GetConnectionStringName()];
        }
        public virtual string GetAvailableStoredProcCommandText() {
                    return
                        "SELECT name[StoredProcedureName] " +
                        "FROM INFORMATION_SCHEMA.ROUTINES[routines] " +
                        "INNER JOIN sys.procedures[sys_procs] " +
                        "on sys_procs.name = routines.SPECIFIC_NAME " +
                        "WHERE ROUTINE_TYPE = 'PROCEDURE' AND sys_procs.is_ms_shipped = 0 " +
                        "ORDER BY routines.SPECIFIC_NAME";
        }
        public virtual CommandType GetAvailableStoredProcCommandType()
        {
            return CommandType.Text;
        }
        public virtual bool CheckStoredProcAvailability()
        {
            return true;
        }
        public abstract class Generic<C> : DbConnectionCore
            where C : DbConnection
        {
            public new C GetDbConnection()
            {
                return GetDbConnectionCore<C>();
            }
        }
        public abstract class SQL : Generic<SqlConnection> { }
        public abstract class SQLite : Generic<SQLiteConnection> { }
        public abstract class OleDb : Generic<OleDbConnection> { }
        public abstract class Odbc : Generic<OdbcConnection> { }
        public abstract class Oracle : Generic<OracleConnection> { }

    } 

}
