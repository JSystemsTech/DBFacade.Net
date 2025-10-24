using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService
{
    /// <summary>
    ///   <br />
    /// </summary>
    internal partial class DbConnectionConfigBase
    {

        internal readonly IDbConnectionProvider DbConnectionProvider;
        private bool UseMockConnection { get; set; }
        internal DbConnectionConfigBase(IDbConnectionProvider dbConnectionProvider)
        {
            DbConnectionProvider = dbConnectionProvider;
        }

        internal void EnableMockMode() => UseMockConnection = true;
        internal void DisableMockMode() => UseMockConnection = false;
        internal IDbResponse ExecuteDbAction(DbCommandMethod commandConfig, object parameters)
        {
            try
            {
                commandConfig.Validate(parameters);
                return ExecuteDbAction(
                    commandConfig,
                    parameters,
                    () => UseMockConnection ? new MockDataAdapter() : DbConnectionProvider.GetDbDataAdapter()
                    );
            }
            catch (ValidationException valEx)
            {
                DbConnectionProvider.OnError(valEx, commandConfig.DbCommandSettings, parameters, DbExecutionExceptionType.ValidationError);
                return DbResponse.Create(valEx);
            }
            catch (DbExecutionException DbEx)
            {
                DbConnectionProvider.OnError(DbEx, commandConfig.DbCommandSettings, parameters, DbExecutionExceptionType.DbExecutionError);
                return DbResponse.Create(DbEx);
            }
            catch (FacadeException fEx)
            {
                DbConnectionProvider.OnError(fEx, commandConfig.DbCommandSettings, parameters, DbExecutionExceptionType.FacadeException);
                return DbResponse.Create(fEx);
            }
            catch (Exception ex)
            {
                DbConnectionProvider.OnError(ex, commandConfig.DbCommandSettings, parameters, DbExecutionExceptionType.Error);
                return DbResponse.Create(ex);
            }
        }
        private IDbResponse ExecuteTransaction(
            DbCommandMethod config,
            IDbConnection dbConnection,
            IDbCommand dbCommand)
        {
            IDbTransaction transaction;
            try
            {
                transaction = dbConnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new DbExecutionException($"Unable to create transaction", ex);
            }
            using (transaction)
            {
                if (transaction != null)
                {
                    try
                    {
                        dbCommand.Transaction = transaction;
                        dbCommand.ExecuteNonQuery();
                    }
                    catch (DbException sqlEx)
                    {
                        transaction.Rollback();
                        throw new DbExecutionException("Unable to execute transaction",sqlEx);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new DbExecutionException("Unknown Error while attempting to execute transaction", ex);
                    }
                    transaction.Commit();
                    return DbResponse.Create(dbCommand.GetReturnValue(), dbCommand.GetOutputValues(), null);
                }
                else
                {
                    throw new FacadeException("Invalid Transaction Definition");
                }


            }
        }

        private IDbResponse ExecuteQuery(
            DbCommandMethod config,
            IDbCommand dbCommand,
            Func<IDbDataAdapter> createDbDataAdapter)
        {
            try
            {
                DataSet ds = new DataSet($"{config.DbCommandSettings.Label} Result DataSet");
                IDbDataAdapter da = createDbDataAdapter();
                da.SelectCommand = dbCommand;
                da.Fill(ds);
                return DbResponse.Create(dbCommand.GetReturnValue(), dbCommand.GetOutputValues(), ds);
            }
            catch (DbException dbEx)
            {
                throw new DbExecutionException("Unable to execute query", dbEx);
            }
            catch (Exception ex)
            {

                throw new DbExecutionException("Unknown Error while attempting to execute query", ex);
            }
        }
        private IDbResponse ExecuteDbAction<TDbParams>(
            DbCommandMethod config,
            TDbParams parameters,
            Func<IDbDataAdapter> createDbDataAdapter)
        {
            IDbConnection dbConnection =
                    UseMockConnection ? new MockDbConnection(config) :
                    DbConnectionProvider.GetDbConnection(config.DbCommandSettings.ConnectionStringId);

            using (dbConnection)
            {
                if (dbConnection != null)
                {
                    try
                    {
                        dbConnection.Open();
                    }
                    catch (Exception ex)
                    {
                        throw new DbExecutionException("Unable to open Connection", ex);
                    }
                    using (var dbCommand = dbConnection.GetDbCommand(config, parameters))
                    {
                        return config.DbCommandSettings.IsTransaction ?
                            ExecuteTransaction(config, dbConnection, dbCommand) :
                            ExecuteQuery(config, dbCommand, createDbDataAdapter);
                    }
                }

                throw new FacadeException("Invalid Connection Definition");
            }
        }       
        
    }
}