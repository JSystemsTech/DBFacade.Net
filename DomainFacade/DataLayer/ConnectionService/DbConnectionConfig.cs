using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;

namespace DomainFacade.DataLayer.ConnectionService
{
    internal interface ISQL { }
    internal interface ISQLite { }
    internal interface IOleDb { }
    internal interface IOdbc { }
    internal interface IOracle { }

    public abstract class DbConnectionConfig<TConnection> : DbConnectionConfigCore where TConnection : IDbConnectionConfig
    {
        public interface IDbCommandText : IDbCommandText<TConnection> { }
        private class DbCommandText : DbCommandText<TConnection>, IDbCommandText
        {
            public DbCommandText(string commandText, string label) : base(commandText, label) { }
        }

        public abstract class GenericBase<TDbConnection> : DbConnectionConfig<TConnection>
            where TDbConnection : DbConnection
        {
            public override IDbConnection GetDbConnection()
            {
                return GetDbConnectionCore<TDbConnection>();
            }
        }
        public static IDbCommandText CreateCommandText(string commandText, string label) { return new DbCommandText(commandText, label); }
        public abstract class SQL : GenericBase<SqlConnection>, ISQL { }
        public abstract class SQLite : GenericBase<SQLiteConnection>, ISQLite { }
        public abstract class OleDb : GenericBase<OleDbConnection>, IOleDb { }
        public abstract class Odbc : GenericBase<OdbcConnection>, IOdbc { }
        public abstract class Oracle : GenericBase<OracleConnection>, IOracle { }
        public abstract class Generic : GenericBase<DbConnection> { }
    }

}
