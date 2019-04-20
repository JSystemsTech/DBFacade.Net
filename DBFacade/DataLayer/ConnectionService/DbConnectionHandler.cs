using System;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Odbc;
using Oracle.ManagedDataAccess.Client;
using DBFacade.Facade.Core;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Exceptions;
using DBFacade.DataLayer.CommandConfig;
using System.Text;

namespace DBFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Drd">The type of the rd.</typeparam>
    /// <typeparam name="Con">The type of the on.</typeparam>
    /// <typeparam name="Cmd">The type of the md.</typeparam>
    /// <typeparam name="Trn">The type of the rn.</typeparam>
    /// <typeparam name="Prm">The type of the rm.</typeparam>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <seealso cref="Facade.Core.DbFacade{TDbManifest}" />
    internal class DbConnectionHandler<Drd, Con, Cmd, Trn, Prm, TDbManifest> : DbFacade<TDbManifest>
        where Drd : DbDataReader
        where Con : DbConnection
        where Cmd : DbCommand
        where Trn : DbTransaction
        where Prm : DbParameter
        where TDbManifest : DbManifest
    {
        /// <summary>
        /// Calls the database method core.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="FacadeException">An Unknown Error Occured</exception>
        protected override IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
        {
            DbMethod dbMethod = DbMethodsCache.GetInstance<DbMethod>();
            Con dbConnection = null;
            Cmd dbCommand = null;
            Trn transaction = null;
            IDbCommandConfig config = DbMethodsCache.GetInstance<DbMethod>().GetConfig();
            string paramsType = parameters.GetType().Name;
            string MethodType = DbMethodsCache.GetInstance<DbMethod>().GetType().Name;
            StringBuilder builder = new StringBuilder();
            try
            {
                dbConnection = dbMethod.GetConfig().GetDbConnection<Con>();
                if (parameters is IDbFunctionalTestParamsModel)
                {
                    IDbFunctionalTestParamsModel TestParamsModel = (IDbFunctionalTestParamsModel)parameters;
                    dbCommand = dbMethod.GetConfig().GetDbCommand<Con, Cmd, Prm>(TestParamsModel.GetParamsModel(), dbConnection);
                    DbDataReader dbDataReader = (DbDataReader)TestParamsModel.GetTestResponse();
                    dbMethod.GetConfig().SetReturnValue(dbCommand, TestParamsModel.GetReturnValue());
                    return new DbResponse<DbMethod, TDbDataModel>(dbDataReader, dbMethod.GetConfig().GetReturnValue(dbCommand));
                }
                else
                {
                    dbCommand = dbMethod.GetConfig().GetDbCommand<Con, Cmd, Prm>(parameters, dbConnection);
                }

                dbConnection.Open();

                if (dbMethod.GetConfig().IsTransaction())
                {
                    transaction = (Trn)dbConnection.BeginTransaction();
                    dbCommand.Transaction = transaction;
                    try
                    {
                        dbCommand.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        builder.AppendFormat("Failed to Execute Transaction for method {0}: ", MethodType);
                        throw config.GetSQLExecutionException(builder.ToString(), e);
                    }

                    IDbResponse<TDbDataModel> response = new DbResponse<DbMethod, TDbDataModel>(dbMethod.GetConfig().GetReturnValue(dbCommand));
                    Done(transaction, dbConnection);
                    return response;
                }
                else
                {
                    Drd dbDataReader;
                    try
                    {
                        dbDataReader = (Drd)dbCommand.ExecuteReader();
                    }
                    catch (Exception e)
                    {
                        builder.AppendFormat("Failed to Execute Reader for method {0}: ", MethodType);
                        throw config.GetSQLExecutionException(builder.ToString(), e);
                    }
                    IDbResponse<TDbDataModel> response = new DbResponse<DbMethod, TDbDataModel>(dbDataReader, dbMethod.GetConfig().GetReturnValue(dbCommand));
                    Done(dbDataReader, dbConnection);
                    return response;
                }
            }
            catch (DataModelConstructionException e)
            {
                throw e;
            }
            catch (SQLExecutionException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    Done(transaction, dbConnection);
                }
                else
                {
                    Done(dbConnection);
                }
                throw new FacadeException("An Unknown Error Occured", e);
            }
        }
        /// <summary>
        /// Disposes the specified disposable item.
        /// </summary>
        /// <param name="disposableItem">The disposable item.</param>
        private void Dispose(IDisposable disposableItem)
        {
            if (disposableItem != null)
            {
                disposableItem.Dispose();
            }
        }
        /// <summary>
        /// Closes the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        private void Close(Con dbConnection)
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
        }
        /// <summary>
        /// Rollbacks the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        private void Rollback(Trn transaction)
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }
        }
        /// <summary>
        /// Closes the specified database data reader.
        /// </summary>
        /// <param name="dbDataReader">The database data reader.</param>
        private void Close(Drd dbDataReader)
        {
            if (dbDataReader != null)
            {
                dbDataReader.Close();
            }
        }
        /// <summary>
        /// Gets the reader.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        private Drd GetReader(Cmd dbCommand)
        {
            return (Drd)dbCommand.ExecuteReader();

        }

        /// <summary>
        /// Dones the specified database data reader.
        /// </summary>
        /// <param name="dbDataReader">The database data reader.</param>
        /// <param name="dbConnection">The database connection.</param>
        private void Done(Drd dbDataReader, Con dbConnection)
        {
            Close(dbDataReader);
            Dispose(dbDataReader);
            Done(dbConnection);
        }
        /// <summary>
        /// Dones the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="dbConnection">The database connection.</param>
        private void Done(Trn transaction, Con dbConnection)
        {
            Dispose(transaction);
            Done(dbConnection);
        }
        /// <summary>
        /// Dones the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        private void Done(Con dbConnection)
        {
            Close(dbConnection);
            Dispose(dbConnection);
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <seealso cref="Facade.Core.DbFacade{TDbManifest}" />
    internal sealed class DbConnectionHandler<TDbManifest> : DbConnectionHandler<DbDataReader, DbConnection, DbCommand, DbTransaction, DbParameter, TDbManifest> where TDbManifest : DbManifest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Facade.Core.DbFacade{TDbManifest}" />
        public sealed class SQL : DbConnectionHandler<SqlDataReader, SqlConnection, SqlCommand, SqlTransaction, SqlParameter, TDbManifest> { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Facade.Core.DbFacade{TDbManifest}" />
        public sealed class SQLite : DbConnectionHandler<SQLiteDataReader, SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteParameter, TDbManifest> { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Facade.Core.DbFacade{TDbManifest}" />
        public sealed class OleDb : DbConnectionHandler<OleDbDataReader, OleDbConnection, OleDbCommand, OleDbTransaction, OleDbParameter, TDbManifest> { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Facade.Core.DbFacade{TDbManifest}" />
        public sealed class Odbc : DbConnectionHandler<OdbcDataReader, OdbcConnection, OdbcCommand, OdbcTransaction, OdbcParameter, TDbManifest> { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Facade.Core.DbFacade{TDbManifest}" />
        public sealed class Oracle : DbConnectionHandler<OracleDataReader, OracleConnection, OracleCommand, OracleTransaction, OracleParameter, TDbManifest> { }
    }
}
