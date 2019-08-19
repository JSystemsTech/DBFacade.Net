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

    public abstract class DbConnectionConfig<TDbDataReader, TDbConnection, TDbCommand, TDbTransaction, TDbParameter,TConnection> : DbConnectionConfigCore 
        where TDbDataReader : DbDataReader
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbTransaction : DbTransaction
        where TDbParameter : DbParameter
        where TConnection : IDbConnectionConfig

    {
        public interface IDbCommandText : IDbCommandText<TConnection> { }
        private sealed class DbCommandText : DbCommandText<TConnection>, IDbCommandText
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
    public abstract class SQLConnectionConfig<TConnection> : DbConnectionConfig<SqlDataReader, SqlConnection, SqlCommand, SqlTransaction, SqlParameter, TConnection> where TConnection : IDbConnectionConfig { }

    public abstract class SQLiteConnectionConfig<TConnection> : DbConnectionConfig<SQLiteDataReader, SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteParameter, TConnection> where TConnection : IDbConnectionConfig { }

    public abstract class OleDbConnectionConfig<TConnection> : DbConnectionConfig<OleDbDataReader, OleDbConnection, OleDbCommand, OleDbTransaction, OleDbParameter, TConnection> where TConnection : IDbConnectionConfig { }

    public abstract class OdbcConnectionConfig<TConnection> : DbConnectionConfig<OdbcDataReader, OdbcConnection, OdbcCommand, OdbcTransaction, OdbcParameter, TConnection> where TConnection : IDbConnectionConfig { }

    public abstract class OracleConnectionConfig<TConnection> : DbConnectionConfig<OracleDataReader, OracleConnection, OracleCommand, OracleTransaction, OracleParameter, TConnection> where TConnection : IDbConnectionConfig { }

    public abstract class DefaultConnectionConfig<TConnection> : DbConnectionConfig<DbDataReader, DbConnection, DbCommand, DbTransaction, DbParameter, TConnection> where TConnection : IDbConnectionConfig { }
}
