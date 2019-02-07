using System;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Odbc;
using Oracle.ManagedDataAccess.Client;
using DomainFacade.Facade.Core;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Utils;
using DomainFacade.DataLayer.Models;

namespace DomainFacade.DataLayer
{


    public class DbConnectionHandler<Drd, Con, Cmd, Trn,Prm, DbMethodGroup> : DbFacade<DbMethodGroup>
        where Drd : DbDataReader
        where Con : DbConnection
        where Cmd : DbCommand
        where Trn : DbTransaction
        where Prm : DbParameter
        where DbMethodGroup : DbMethodsCore
    {
        protected override TDbResponse CallDbMethodCore<DbParams, TDbResponse, DbMethod>(DbParams parameters)
        {
            DbMethod dbMethod = DbMethodsCache.GetInstance<DbMethod>();
            CheckResponseType<TDbResponse, DbMethod>();
            Con dbConnection = null;
            Cmd dbCommand = null;
            Trn transaction = null;
            try
            {
                dbConnection = dbMethod.GetConfig().GetDbConnection<Con>();
                dbCommand = dbMethod.GetConfig().GetDbCommand<Con, Cmd, Prm>(parameters, dbConnection);
                dbConnection.Open();

                if (dbMethod.GetConfig().IsTransaction() || dbMethod.GetConfig().IsTransactionWithReturn())
                {
                    transaction = (Trn)dbConnection.BeginTransaction();
                    dbCommand.Transaction = transaction;
                    dbCommand.ExecuteNonQuery();
                    transaction.Commit();
                    TDbResponse response = BuildResponse<TDbResponse, DbMethod>(dbCommand);
                    Done(transaction, dbConnection);
                    return response;
                }
                else
                {
                    Drd dbDataReader = (Drd)dbCommand.ExecuteReader();
                    TDbResponse response = BuildResponse<TDbResponse, DbMethod>(dbDataReader, dbCommand);
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
        private void CheckResponseType<TDbResponse, DbMethod>()
            where TDbResponse : DbResponse
            where DbMethod: DbMethodGroup
        {
            DbMethod dbMethod = DbMethodsCache.GetInstance<DbMethod>();
            if (GenericInstance<TDbResponse>.GetInstance().DBMethodType != dbMethod.GetConfig().DBMethodType)
            {
                Console.WriteLine(GenericInstance<TDbResponse>.GetInstance().DBMethodType);
                Console.WriteLine(dbMethod.GetConfig().DBMethodType);
                throw new NotImplementedException();
            }
        }
        private TDbResponse BuildResponse<TDbResponse, DbMethod>(Drd dbReader, Cmd dbCommand)
            where TDbResponse : DbResponse
            where DbMethod: DbMethodGroup
        {
            DbMethod dbMethod = DbMethodsCache.GetInstance<DbMethod>();
            if (dbMethod.GetConfig().IsFetchRecord() || dbMethod.GetConfig().IsFetchRecords())
            {
                return GenericInstance<TDbResponse>.GetInstance(dbReader);
            }
            else if (dbMethod.GetConfig().IsFetchRecordWithReturn() || dbMethod.GetConfig().IsFetchRecordsWithReturn())
            {
                return GenericInstance<TDbResponse>.GetInstance(dbMethod.GetConfig().GetReturnValue(dbCommand), dbReader);
            }
            else
            {
                throw new Exception("Invalid Fetch Method");
            }
        }
        private TDbResponse BuildResponse<TDbResponse, DbMethod>(Cmd dbCommand)
            where TDbResponse : DbResponse
            where DbMethod : DbMethodGroup
        {
            DbMethod dbMethod = DbMethodsCache.GetInstance<DbMethod>();
            if (dbMethod.GetConfig().IsTransaction())
            {
                return GenericInstance<TDbResponse>.GetInstance();
            }
            else if (dbMethod.GetConfig().IsTransactionWithReturn())
            {
                return GenericInstance<TDbResponse>.GetInstance(dbMethod.GetConfig().GetReturnValue(dbCommand));
            }
            else
            {
                throw new Exception("Invalid Transaction Method");
            }
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
    public sealed class DbConnectionHandler<DbMethodGroup> : DbConnectionHandler<DbDataReader, DbConnection, DbCommand, DbTransaction,DbParameter, DbMethodGroup> where DbMethodGroup : DbMethodsCore
    {
        public class SQL : DbConnectionHandler<SqlDataReader, SqlConnection, SqlCommand, SqlTransaction, SqlParameter, DbMethodGroup> { }
        public class SQLite : DbConnectionHandler<SQLiteDataReader, SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteParameter, DbMethodGroup> { }
        public class OleDb : DbConnectionHandler<OleDbDataReader, OleDbConnection, OleDbCommand, OleDbTransaction, OleDbParameter, DbMethodGroup> { }
        public class Odbc : DbConnectionHandler<OdbcDataReader, OdbcConnection, OdbcCommand, OdbcTransaction, OdbcParameter, DbMethodGroup> { }        
        public class Oracle : DbConnectionHandler<OracleDataReader, OracleConnection, OracleCommand, OracleTransaction, OracleParameter, DbMethodGroup> { }
    }
}
