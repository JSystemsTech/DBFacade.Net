using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Threading.Tasks;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using Oracle.ManagedDataAccess.Client;

namespace DBFacade.DataLayer.ConnectionService
{
    public abstract class DbConnectionConfig<TDbDataReader, TDbConnection, TDbCommand, TDbTransaction, TDbParameter,
        TDbConnectionConfig> : IDbConnectionConfigInternal
        where TDbDataReader : DbDataReader
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbTransaction : DbTransaction
        where TDbParameter : DbParameter
        where TDbConnectionConfig : IDbConnectionConfig

    {
        public IDbConnection GetDbConnection(IDbParamsModel parameters) =>ResolveDbConnection(parameters);
           
        
        private bool IsRunAsTest(IDbParamsModel parameters)
        => parameters is IInternalDbParamsModel paramsModel && paramsModel.RunMode == MethodRunMode.Test;
        public IDbResponse<TDbDataModel> ExecuteDbAction<TDbMethodManifest, TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbMethodManifest : DbMethodManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        => IsRunAsTest(parameters) ?
            DbConnectionHandler<MockDbConnection, MockDbCommand, MockDbParameter, MockDbTransaction, DbDataReader,
                    TDbMethodManifest>
                .ExecuteDbAction<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters):
            DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader,
                    TDbMethodManifest>
                .ExecuteDbAction<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        

        public async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbMethodManifest, TDbDataModel, TDbParams,
            TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
            where TDbMethodManifest : DbMethodManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbMethodManifestMethod : TDbMethodManifest
        
            => IsRunAsTest(parameters) ?
            await DbConnectionHandler<MockDbConnection, MockDbCommand, MockDbParameter, MockDbTransaction, DbDataReader,
                    TDbMethodManifest>
                .ExecuteDbActionAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters):            
            await
                DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader,
                        TDbMethodManifest>
                    .ExecuteDbActionAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
                      
        

        protected static IDbCommandText CreateCommandText(string commandText, string label)
        {
            return new DbCommandText(commandText, label);
        }

        private IDbConnection ResolveDbConnection(IDbParamsModel parameters)
        {
            IDbConnection resolvedDbConnection = IsRunAsTest(parameters) ? new MockDbConnection(parameters as IInternalDbParamsModel) as IDbConnection :
            DbProviderFactories.GetFactory(GetDbConnectionProvider()).CreateConnection() is TDbConnection dbConnection ? dbConnection : null;

            if (resolvedDbConnection != null)
            {
                resolvedDbConnection.ConnectionString = GetDbConnectionString();
                return resolvedDbConnection;
            }

            return null;
        }

        protected abstract string GetDbConnectionString();
        protected abstract string GetDbConnectionProvider();

        public interface IDbCommandText : IDbCommandText<TDbConnectionConfig>
        {
        }

        private sealed class DbCommandText : DbCommandText<TDbConnectionConfig>, IDbCommandText
        {
            public DbCommandText(string commandText, string label) : base(commandText, label)
            {
            }
        }
    }
    
    public abstract class
        SqlConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SqlDataReader, SqlConnection, SqlCommand,
            SqlTransaction, SqlParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig
    {
    }

    public abstract class
        SqLiteConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SQLiteDataReader, SQLiteConnection,
            SQLiteCommand, SQLiteTransaction, SQLiteParameter, TDbConnectionConfig>
        where TDbConnectionConfig : IDbConnectionConfig
    {
    }

    public abstract class
        OleDbConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OleDbDataReader, OleDbConnection, OleDbCommand,
            OleDbTransaction, OleDbParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig
    {
    }

    public abstract class
        OdbcConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OdbcDataReader, OdbcConnection, OdbcCommand,
            OdbcTransaction, OdbcParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig
    {
    }

    public abstract class
        OracleConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OracleDataReader, OracleConnection,
            OracleCommand, OracleTransaction, OracleParameter, TDbConnectionConfig>
        where TDbConnectionConfig : IDbConnectionConfig
    {
    }

    public abstract class
        DefaultConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<DbDataReader, DbConnection, DbCommand,
            DbTransaction, DbParameter, TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig
    {
    }
}