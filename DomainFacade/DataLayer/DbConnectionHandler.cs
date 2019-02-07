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


    public class DbConnectionHandler<Drd, Con, Cmd, Trn,Prm, DbMethodGroup> : FacadeAPI<DbMethodGroup>
        where Drd : DbDataReader
        where Con : DbConnection
        where Cmd : DbCommand
        where Trn : DbTransaction
        where Prm : DbParameter
        where DbMethodGroup : DbMethodsCore
    {
        protected override R CallDbMethodCore<U, R, DbMethod>(U parameters)
        {
            DbMethod dbMethod = DbMethodsService.GetInstance<DbMethod>();
            CheckResponseType<R, DbMethod>();
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
                    R response = BuildResponse<R, DbMethod>(dbCommand);
                    Done(transaction, dbConnection);
                    return response;
                }
                else
                {
                    Drd dbDataReader = (Drd)dbCommand.ExecuteReader();
                    R response = BuildResponse<R,DbMethod>(dbDataReader, dbCommand);
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
        private void CheckResponseType<R, DbMethod>()
            where R : DbResponse
            where DbMethod: DbMethodGroup
        {
            DbMethod dbMethod = DbMethodsService.GetInstance<DbMethod>();
            if (GenericInstance<R>.GetInstance().DBMethodType != dbMethod.GetConfig().DBMethodType)
            {
                Console.WriteLine(GenericInstance<R>.GetInstance().DBMethodType);
                Console.WriteLine(dbMethod.GetConfig().DBMethodType);
                throw new NotImplementedException();
            }
        }
        private R BuildResponse<R, DbMethod>(Drd dbReader, Cmd dbCommand)
            where R : DbResponse
            where DbMethod: DbMethodGroup
        {
            DbMethod dbMethod = DbMethodsService.GetInstance<DbMethod>();
            if (dbMethod.GetConfig().IsFetchRecord() || dbMethod.GetConfig().IsFetchRecords())
            {
                return GenericInstance<R>.GetInstance(dbReader);
            }
            else if (dbMethod.GetConfig().IsFetchRecordWithReturn() || dbMethod.GetConfig().IsFetchRecordsWithReturn())
            {
                return GenericInstance<R>.GetInstance(dbMethod.GetConfig().GetReturnValue(dbCommand), dbReader);
            }
            else
            {
                throw new Exception("Invalid Fetch Method");
            }
        }
        private R BuildResponse<R, DbMethod>(Cmd dbCommand)
            where R : DbResponse
            where DbMethod : DbMethodGroup
        {
            DbMethod dbMethod = DbMethodsService.GetInstance<DbMethod>();
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
    public sealed class DbConnectionHandler<DbMethodGroup> : DbConnectionHandler<DbDataReader, DbConnection, DbCommand, DbTransaction,DbParameter, DbMethodGroup> where DbMethodGroup : DbMethodsCore
    {
        public class SQL : DbConnectionHandler<SqlDataReader, SqlConnection, SqlCommand, SqlTransaction, SqlParameter, DbMethodGroup> { }
        public class SQLite : DbConnectionHandler<SQLiteDataReader, SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteParameter, DbMethodGroup> { }
        public class OleDb : DbConnectionHandler<OleDbDataReader, OleDbConnection, OleDbCommand, OleDbTransaction, OleDbParameter, DbMethodGroup> { }
        public class Odbc : DbConnectionHandler<OdbcDataReader, OdbcConnection, OdbcCommand, OdbcTransaction, OdbcParameter, DbMethodGroup> { }        
        public class Oracle : DbConnectionHandler<OracleDataReader, OracleConnection, OracleCommand, OracleTransaction, OracleParameter, DbMethodGroup> { }
    }
}
