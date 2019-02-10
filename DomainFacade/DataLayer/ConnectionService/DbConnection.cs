using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Configuration;
using System.Data;
using static DomainFacade.DataLayer.ConnectionService.DbConnectionService;

namespace DomainFacade.DataLayer.ConnectionService
{

    public interface IDbConnectionConfig<Con> where Con : DbConnection
    {
        string[] GetAvailableStoredProcs();
        Con GetDbConnection();
    }
    public abstract class DbConnectionCore
    {
        public DbConnectionStoredProcedure[] AvailableStoredProcs { get; private set; }
        public void SetAvailableStoredProcs(DbConnectionStoredProcedure[] storedProcs)
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
        public virtual string GetAvailableStoredProcAdditionalMetaCommandText(string spName)
        {
            return
            "SELECT RIGHT(name, LEN(name) - 1) [name], type, is_nullable ^ 1 [isRequired], MaxLength " +
            "FROM sys.parameters [sp] " +
            "inner join( " +
                "SELECT name [type], system_type_id, max_length [MaxLength] " +
                "FROM sys.types[types] " +
            ") [t] " +
            "on t.system_type_id = [sp].system_type_id " +
            "where object_id = object_id('"+spName+"') " +
            "order by parameter_id";
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
