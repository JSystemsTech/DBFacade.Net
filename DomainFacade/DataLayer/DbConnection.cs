using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Configuration;

namespace DomainFacade.DataLayer
{
    
    public abstract class DbConnectionCore
    {

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

        public abstract class SQL: DbConnectionCore{
            public new SqlConnection GetDbConnection()
            {
                return GetDbConnectionCore<SqlConnection>();
            }
        }
        public abstract class SQLite : DbConnectionCore{
            public new SQLiteConnection GetDbConnection()
            {
                return GetDbConnectionCore<SQLiteConnection>();
            }
        }
        public abstract class OleDb : DbConnectionCore{
            public new OleDbConnection GetDbConnection()
            {
                return GetDbConnectionCore<OleDbConnection>();
            }
        }
        public abstract class Odbc : DbConnectionCore{
            public new OdbcConnection GetDbConnection()
            {
                return GetDbConnectionCore<OdbcConnection>();
            }
        }
        public abstract class Oracle : DbConnectionCore{
            public new OracleConnection GetDbConnection()
            {
                return GetDbConnectionCore<OracleConnection>();
            }
        }

    } 

}
