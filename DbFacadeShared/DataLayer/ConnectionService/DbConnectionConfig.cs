using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Utils;
using Oracle.ManagedDataAccess.Client;

namespace DbFacade.DataLayer.ConnectionService
{
    public abstract class DbConnectionConfigBase : IDbConnectionConfig, IAsyncInit
    {
        public async Task InitAsync()
        {
            await Task.CompletedTask;
        }
        internal abstract IDbConnection GetDbConnection(object parameters, MockResponseData mockResponseData = null);
        internal abstract Task<IDbConnection> GetDbConnectionAsync(object parameters, MockResponseData mockResponseData = null);
        internal abstract IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, MockResponseData mockResponseData = null)
            where TDbDataModel : DbDataModel;

        internal abstract Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, MockResponseData mockResponseData = null)
            where TDbDataModel : DbDataModel;
    }
    public abstract class DbConnectionConfig<TDbDataReader, TDbConnection, TDbCommand, TDbTransaction, TDbParameter,TDbConnectionConfig>: DbConnectionConfigBase
        where TDbDataReader : DbDataReader
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbTransaction : DbTransaction
        where TDbParameter : DbParameter
        where TDbConnectionConfig : DbConnectionConfigBase

    {
        
        internal override sealed IDbConnection GetDbConnection(object parameters, MockResponseData mockResponseData = null) => ResolveDbConnection(mockResponseData);
        internal override sealed async Task<IDbConnection> GetDbConnectionAsync(object parameters, MockResponseData mockResponseData = null) => await ResolveDbConnectionAsync(mockResponseData);
        internal override sealed IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig,TDbParams parameters, MockResponseData mockResponseData = null)
        {
              if (commandConfig.DbConnectionConfig == this)
                {
                    commandConfig.OnBefore(parameters);
                    return mockResponseData != null ?
                  DbConnectionHandler<MockDbConnection, MockDbCommand, MockDbParameter, MockDbTransaction, DbDataReader>
                      .ExecuteDbAction(commandConfig, parameters, mockResponseData) :
                  DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader>
                      .ExecuteDbAction(commandConfig, parameters, mockResponseData);
                }
                else
                {
                    throw new FacadeException($"'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'");
                }
            }       

        internal override sealed async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, MockResponseData mockResponseData = null)
        {
            if (commandConfig.DbConnectionConfig == this) {
            await commandConfig.OnBeforeAsync(parameters);
            return mockResponseData != null ?
                 await DbConnectionHandler<MockDbConnection, MockDbCommand, MockDbParameter, MockDbTransaction, DbDataReader>
                      .ExecuteDbActionAsync(commandConfig, parameters, mockResponseData) :
                 await DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader>
                      .ExecuteDbActionAsync(commandConfig, parameters, mockResponseData);
            }
            else
            {
                throw new FacadeException($"'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'");
            }
        }        

        

        private IDbConnection ResolveDbConnection(MockResponseData mockResponseData = null)
        {
            IDbConnection resolvedDbConnection = mockResponseData != null ? new MockDbConnection(mockResponseData) :
            DbProviderFactories.GetFactory(GetDbConnectionProvider()).CreateConnection() is TDbConnection dbConnection ? dbConnection : default(IDbConnection);

            if (resolvedDbConnection != default(IDbConnection))
            {
                resolvedDbConnection.ConnectionString = GetDbConnectionString();
                return resolvedDbConnection;
            }

            return null;
        }
        private async Task<IDbConnection> ResolveDbConnectionAsync(MockResponseData mockResponseData = null)
        {
            IDbConnection resolvedDbConnection = mockResponseData != null ? new MockDbConnection(mockResponseData) :
                DbProviderFactories.GetFactory(await GetDbConnectionProviderAsync()).CreateConnection() is TDbConnection dbConnection ? dbConnection : 
                default(IDbConnection);
            
            if (resolvedDbConnection != default(IDbConnection))
            {
                resolvedDbConnection.ConnectionString = await GetDbConnectionStringAsync();
                return resolvedDbConnection;
            }
            await Task.CompletedTask;
            return null;
        }

        protected virtual string GetDbConnectionString()
        {
            throw new NotImplementedException("You must impliment the method GetDbConnectionString");
        }
        protected virtual string GetDbConnectionProvider()
        {
            throw new NotImplementedException("You must impliment the method GetDbConnectionProvider");
        }
        protected virtual async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException("You must impliment the method GetDbConnectionStringAsync");
        }
        protected virtual async Task<string> GetDbConnectionProviderAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException("You must impliment the method GetDbConnectionProviderAsync");
        }
    }
    public abstract class
        SqlConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SqlDataReader, SqlConnection, SqlCommand,
            SqlTransaction, SqlParameter, TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    public abstract class
        SqLiteConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<SQLiteDataReader, SQLiteConnection,
            SQLiteCommand, SQLiteTransaction, SQLiteParameter, TDbConnectionConfig>
        where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    public abstract class
        OleDbConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OleDbDataReader, OleDbConnection, OleDbCommand,
            OleDbTransaction, OleDbParameter, TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    public abstract class
        OdbcConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OdbcDataReader, OdbcConnection, OdbcCommand,
            OdbcTransaction, OdbcParameter, TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    public abstract class
        OracleConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<OracleDataReader, OracleConnection,
            OracleCommand, OracleTransaction, OracleParameter, TDbConnectionConfig>
        where TDbConnectionConfig : DbConnectionConfigBase
    {
    }

    public abstract class
        DefaultConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<DbDataReader, DbConnection, DbCommand,
            DbTransaction, DbParameter, TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
    }
}