using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.ConnectionService
{

    public abstract class DbConnectionConfig<TDbDataReader, TDbConnection, TDbCommand, TDbTransaction, TDbParameter, TDbConnectionConfig> : IDbConnectionConfigInternal
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
        public IDbConnection DbConnection => ResolveDbConnection();

        public IDbResponse<TDbDataModel> ExecuteDbAction<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        => DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader, TDbManifest>.ExecuteDbAction<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        
        public async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        => await DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader, TDbManifest>.ExecuteDbActionAsync<TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        
        protected static IDbCommandText CreateCommandText(string commandText, string label) => new DbCommandText(commandText, label);

        private TDbConnection ResolveDbConnection()
        {
            TDbConnection dbConnection = DbProviderFactories.GetFactory(GetDbConnectionProvider()).CreateConnection() as TDbConnection;
            dbConnection.ConnectionString = GetDbConnectionString();
            return dbConnection;
        }
        protected abstract string GetDbConnectionString();
        protected abstract string GetDbConnectionProvider();
    }
    public abstract class SQLConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SqlDataReader, SqlConnection, SqlCommand, SqlTransaction, SqlParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class SQLiteConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SQLiteDataReader, SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class OleDbConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OleDbDataReader, OleDbConnection, OleDbCommand, OleDbTransaction, OleDbParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class OdbcConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OdbcDataReader, OdbcConnection, OdbcCommand, OdbcTransaction, OdbcParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class OracleConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OracleDataReader, OracleConnection, OracleCommand, OracleTransaction, OracleParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }

    public abstract class DefaultConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<DbDataReader, DbConnection, DbCommand, DbTransaction, DbParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig { }
}
