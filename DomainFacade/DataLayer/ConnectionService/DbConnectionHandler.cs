using System;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Odbc;
using Oracle.ManagedDataAccess.Client;
using DomainFacade.Facade.Core;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;

namespace DomainFacade.DataLayer.ConnectionService
{
    internal class DbConnectionHandler<Drd, Con, Cmd, Trn,Prm, TDbManifest> : DbFacade<TDbManifest>
        where Drd : DbDataReader
        where Con : DbConnection
        where Cmd : DbCommand
        where Trn : DbTransaction
        where Prm : DbParameter
        where TDbManifest : DbManifest
    {
        protected override IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
        {
            DbMethod dbMethod = DbMethodsCache.GetInstance<DbMethod>();
            Con dbConnection = null;
            Cmd dbCommand = null;
            Trn transaction = null;
            try
            {
                dbConnection = dbMethod.GetConfig().GetDbConnection<Con>();
                if(parameters is IDbFunctionalTestParamsModel)
                {
                    IDbFunctionalTestParamsModel TestParamsModel = (IDbFunctionalTestParamsModel)parameters;
                    dbCommand = dbMethod.GetConfig().GetDbCommand<Con, Cmd, Prm>(TestParamsModel.GetParamsModel(), dbConnection);
                    Drd dbDataReader = (Drd)TestParamsModel.GetTestResponse();
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
                    dbCommand.ExecuteNonQuery();
                    transaction.Commit();
                    IDbResponse<TDbDataModel> response = new DbResponse<DbMethod, TDbDataModel>(dbMethod.GetConfig().GetReturnValue(dbCommand));
                    Done(transaction, dbConnection);
                    return response;
                }
                else
                {
                    Drd dbDataReader = (Drd)dbCommand.ExecuteReader();
                    IDbResponse<TDbDataModel> response = new DbResponse<DbMethod, TDbDataModel>(dbDataReader, dbMethod.GetConfig().GetReturnValue(dbCommand));
                    Done(dbDataReader, dbConnection);
                    return response;
                }
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
                throw e;
            }
        }
        private void Dispose(IDisposable disposableItem)
        {
            if (disposableItem != null)
            {
                disposableItem.Dispose();
            }
        }
        private void Close(Con dbConnection)
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
        }
        private void Rollback(Trn transaction)
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }
        }
        private void Close(Drd dbDataReader)
        {
            if (dbDataReader != null)
            {
                dbDataReader.Close();
            }
        }
        private Drd GetReader(Cmd dbCommand)
        {
            return (Drd)dbCommand.ExecuteReader();

        }
        
        private void Done(Drd dbDataReader, Con dbConnection)
        {
            Close(dbDataReader);
            Dispose(dbDataReader);
            Done(dbConnection);
        }
        private void Done(Trn transaction, Con dbConnection)
        {
            Dispose(transaction);
            Done(dbConnection);
        }
        private void Done(Con dbConnection)
        {
            Close(dbConnection);
            Dispose(dbConnection);
        }

    }
    internal sealed class DbConnectionHandler<TDbManifest> : DbConnectionHandler<DbDataReader, DbConnection, DbCommand, DbTransaction,DbParameter, TDbManifest> where TDbManifest : DbManifest
    {
        public sealed class SQL : DbConnectionHandler<SqlDataReader, SqlConnection, SqlCommand, SqlTransaction, SqlParameter, TDbManifest> { }
        public sealed class SQLite : DbConnectionHandler<SQLiteDataReader, SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteParameter, TDbManifest> { }
        public sealed class OleDb : DbConnectionHandler<OleDbDataReader, OleDbConnection, OleDbCommand, OleDbTransaction, OleDbParameter, TDbManifest> { }
        public sealed class Odbc : DbConnectionHandler<OdbcDataReader, OdbcConnection, OdbcCommand, OdbcTransaction, OdbcParameter, TDbManifest> { }        
        public sealed class Oracle : DbConnectionHandler<OracleDataReader, OracleConnection, OracleCommand, OracleTransaction, OracleParameter, TDbManifest> { }
    }
}
