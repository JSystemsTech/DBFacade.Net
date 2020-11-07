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
    public abstract class DbConnectionConfigBase: IDbConnectionConfig, IAsyncInit
    {
        public async Task InitAsync()
        {
            await Task.CompletedTask;
        }
        internal abstract IDbConnection GetDbConnection(DbParamsModel parameters);
        internal abstract Task<IDbConnection> GetDbConnectionAsync(DbParamsModel parameters);
        internal abstract IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : DbParamsModel;

        internal abstract Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : DbParamsModel;
    }
    public abstract class DbConnectionConfig<TDbDataReader, TDbConnection, TDbCommand, TDbTransaction, TDbParameter,TDbConnectionConfig>: DbConnectionConfigBase
        where TDbDataReader : DbDataReader
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbTransaction : DbTransaction
        where TDbParameter : DbParameter
        where TDbConnectionConfig : DbConnectionConfigBase

    {
        
        internal override sealed IDbConnection GetDbConnection(DbParamsModel parameters) => ResolveDbConnection(parameters);
        internal override sealed async Task<IDbConnection> GetDbConnectionAsync(DbParamsModel parameters) => await ResolveDbConnectionAsync(parameters);
        internal override sealed IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig,TDbParams parameters)
        {
              if (commandConfig.DbConnectionConfig == this)
                {
                    commandConfig.OnBefore(parameters);
                    return parameters.RunMode == MethodRunMode.Test ?
                  DbConnectionHandler<MockDbConnection, MockDbCommand, MockDbParameter, MockDbTransaction, DbDataReader>
                      .ExecuteDbAction(commandConfig, parameters) :
                  DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader>
                      .ExecuteDbAction(commandConfig, parameters);
                }
                else
                {
                    throw new FacadeException($"'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'");
                }
            }       

        internal override sealed async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters)
        {
            if (commandConfig.DbConnectionConfig == this) {
            await commandConfig.OnBeforeAsync(parameters);
            return parameters.RunMode == MethodRunMode.Test ?
                 await DbConnectionHandler<MockDbConnection, MockDbCommand, MockDbParameter, MockDbTransaction, DbDataReader>
                      .ExecuteDbActionAsync(commandConfig, parameters) :
                 await DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader>
                      .ExecuteDbActionAsync(commandConfig, parameters);
            }
            else
            {
                throw new FacadeException($"'{GetType().Name}' does not support command '{commandConfig.DbCommandText.Label}'");
            }
        }        

        protected static IDbCommandConfig CreateFetchCommand(string commandText, string label, bool enforceValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, false, enforceValidation);
        protected static IDbCommandConfig CreateTransactionCommand(string commandText, string label, bool enforceValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, true, enforceValidation);
        protected static IDbCommandConfig CreateFetchCommand(string commandText, string label, CommandType commandType, bool enforceValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, false, enforceValidation);
        protected static IDbCommandConfig CreateTransactionCommand(string commandText, string label, CommandType commandType, bool enforceValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, true, enforceValidation);


        protected static async Task<IDbCommandConfig> CreateFetchCommandAsync(string commandText, string label, bool enforceValidation = false)
        => await DbCommand<TDbConnectionConfig>.CreateAsync(commandText, label, CommandType.StoredProcedure, false, enforceValidation);
        protected static async Task<IDbCommandConfig> CreateTransactionCommandAsync(string commandText, string label, bool enforceValidation = true)
        => await DbCommand<TDbConnectionConfig>.CreateAsync(commandText, label, CommandType.StoredProcedure, true, enforceValidation);
        protected static async Task<IDbCommandConfig> CreateFetchCommandAsync(string commandText, string label, CommandType commandType, bool enforceValidation = false)
        => await DbCommand<TDbConnectionConfig>.CreateAsync(commandText, label, commandType, false, enforceValidation);
        protected static async Task<IDbCommandConfig> CreateTransactionCommandAsync(string commandText, string label, CommandType commandType, bool enforceValidation = true)
        => await DbCommand<TDbConnectionConfig>.CreateAsync(commandText, label, commandType, true, enforceValidation);

        private IDbConnection ResolveDbConnection(DbParamsModel parameters)
        {
            IDbConnection resolvedDbConnection = parameters.RunMode == MethodRunMode.Test ? new MockDbConnection(parameters) as IDbConnection :
            DbProviderFactories.GetFactory(GetDbConnectionProvider()).CreateConnection() is TDbConnection dbConnection ? dbConnection : null;

            if (resolvedDbConnection != null)
            {
                resolvedDbConnection.ConnectionString = GetDbConnectionString();
                return resolvedDbConnection;
            }

            return null;
        }
        private async Task<IDbConnection> ResolveDbConnectionAsync(DbParamsModel parameters)
        {
            IDbConnection resolvedDbConnection = null;
            if (parameters.RunMode == MethodRunMode.Test)
            {
                resolvedDbConnection = new MockDbConnection(parameters);
            }
            else if (DbProviderFactories.GetFactory(await GetDbConnectionProviderAsync()).CreateConnection() is TDbConnection dbConnection)
            {
                resolvedDbConnection = dbConnection;
            }
            if (resolvedDbConnection != null)
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