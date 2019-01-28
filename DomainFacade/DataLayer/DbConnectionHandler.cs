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


    public class DbConnectionHandler<Drd, Con, Cmd, Trn,Prm, E> : FacadeAPI<E>
        where Drd : DbDataReader
        where Con : DbConnection
        where Cmd : DbCommand
        where Trn : DbTransaction
        where Prm : DbParameter
        where E : DbMethodsCore
    {
        protected override R CallDbMethodCore<U, R>(U parameters, E dbMethod)
        {
            CheckResponseType<R>(dbMethod);
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
                    R response = BuildResponse<R>(dbCommand, dbMethod);
                    Done(transaction, dbConnection);
                    return response;
                }
                else
                {
                    Drd dbDataReader = (Drd)dbCommand.ExecuteReader();
                    R response = BuildResponse<R>(dbDataReader, dbCommand, dbMethod);
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
        private void CheckResponseType<R>(E dbMethod)
            where R : DbResponse
        {
            if (GenericInstance<R>.GetInstance().DBMethodType != dbMethod.GetConfig().DBMethodType)
            {
                Console.WriteLine(GenericInstance<R>.GetInstance().DBMethodType);
                Console.WriteLine(dbMethod.GetConfig().DBMethodType);
                throw new NotImplementedException();
            }
        }
        private R BuildResponse<R>(Drd dbReader, Cmd dbCommand, E dbMethod)
            where R : DbResponse
        {
            if (dbMethod.GetConfig().IsFetchRecord() || dbMethod.GetConfig().IsFetchRecords())
            {
                return GenericInstance<R>.GetInstance(dbReader, dbMethod);
            }
            else if (dbMethod.GetConfig().IsFetchRecordWithReturn() || dbMethod.GetConfig().IsFetchRecordsWithReturn())
            {
                return GenericInstance<R>.GetInstance(dbMethod.GetConfig().GetReturnValue(dbCommand), dbReader, dbMethod);
            }
            else
            {
                throw new Exception("Invalid Fetch Method");
            }
        }
        private R BuildResponse<R>(Cmd dbCommand, E dbMethod)
            where R : DbResponse
        {
            if (dbMethod.GetConfig().IsTransaction())
            {
                return GenericInstance<R>.GetInstance();
            }
            else if (dbMethod.GetConfig().IsTransactionWithReturn())
            {
                return GenericInstance<R>.GetInstance(dbMethod.GetConfig().GetReturnValue(dbCommand));
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
    public class DbConnectionHandler<E> : DbConnectionHandler<DbDataReader, DbConnection, DbCommand, DbTransaction,DbParameter, E> where E : DbMethodsCore {
        public class Sql : DbConnectionHandler<SqlDataReader, SqlConnection, SqlCommand, SqlTransaction, SqlParameter, E>{ }
        public class SQLite : DbConnectionHandler<SQLiteDataReader, SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteParameter, E> { }
        public class OleDb : DbConnectionHandler<OleDbDataReader, OleDbConnection, OleDbCommand, OleDbTransaction, OleDbParameter, E> { }
        public class Odbc : DbConnectionHandler<OdbcDataReader, OdbcConnection, OdbcCommand, OdbcTransaction, OdbcParameter, E> { }        
        public class Oracle : DbConnectionHandler<OracleDataReader, OracleConnection, OracleCommand, OracleTransaction, OracleParameter, E> { }
    }


}
