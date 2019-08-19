using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;

namespace DBFacade.DataLayer.ConnectionService
{

    public abstract class DbConnectionConfig<TDbDataReader, TDbConnection, TDbCommand, TDbTransaction, TDbParameter, TDbConnectionConfig> : DbConnectionConfigCore 
        where TDbDataReader : DbDataReader
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbTransaction : DbTransaction
        where TDbParameter : DbParameter
        where TDbConnectionConfig : IDbConnectionConfig

    {
        public interface IDbCommandText : IDbCommandText<TDbConnectionConfig> { }
        private sealed class DbCommandText : DbCommandText<TDbConnectionConfig>, IDbCommandText
        {
            public DbCommandText(string commandText, string label) : base(commandText, label) { }
        }
        private sealed class ConnectionHandler<TDbManifest> : DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader, TDbManifest> where TDbManifest : DbManifest { }
        public sealed override IDbConnection GetDbConnection() => GetDbConnectionCore<TDbConnection>();

        public sealed override IDbResponse<TDbDataModel> ExecuteDbAction<TDbManifest, TDbDataModel, TDbParams, DbMethod>(DbMethod dbMethod, TDbParams parameters)
        {
            return ConnectionHandler<TDbManifest>.ExecuteDbAction<TDbDataModel, TDbParams, DbMethod>(dbMethod, parameters);
        }
        protected static IDbCommandText CreateCommandText(string commandText, string label) => new DbCommandText(commandText, label);
    }
    public abstract class SQLConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SqlDataReader, SqlConnection, SqlCommand, SqlTransaction, SqlParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class SQLiteConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SQLiteDataReader, SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class OleDbConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OleDbDataReader, OleDbConnection, OleDbCommand, OleDbTransaction, OleDbParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class OdbcConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OdbcDataReader, OdbcConnection, OdbcCommand, OdbcTransaction, OdbcParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class OracleConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OracleDataReader, OracleConnection, OracleCommand, OracleTransaction, OracleParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class DefaultConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<DbDataReader, DbConnection, DbCommand, DbTransaction, DbParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }
}
