using System;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Odbc;
using Oracle.ManagedDataAccess.Client;
using DomainFacade.Facade.Core;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.Utils;
using DomainFacade.DataLayer.Models;

namespace DomainFacade.DataLayer.ConnectionService
{
    public class DbConnectionHandler<Drd, Con, Cmd, Trn,Prm, TDbManifest> : DbFacade<TDbManifest>
        where Drd : DbDataReader
        where Con : DbConnection
        where Cmd : DbCommand
        where Trn : DbTransaction
        where Prm : DbParameter
        where TDbManifest : DbManifest
    {
        protected override TDbResponse CallDbMethodCore<TDbResponse, TDbParams, DbMethod>(TDbParams parameters)
        {
            DbMethod dbMethod = DbMethodsCache.GetInstance<DbMethod>();
            CheckResponseType<TDbResponse, DbMethod>();
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
                    if (dbMethod.GetConfig().IsFetchRecordWithReturn() ||
                        dbMethod.GetConfig().IsFetchRecordsWithReturn() ||
                        dbMethod.GetConfig().IsTransactionWithReturn())
                    {
                        dbMethod.GetConfig().SetReturnValue(dbCommand, TestParamsModel.GetReturnValue());
                    }
                    TDbResponse response = BuildResponse<TDbResponse, DbMethod>(dbDataReader, dbCommand);
                    return response;
                }
                else
                {
                    dbCommand = dbMethod.GetConfig().GetDbCommand<Con, Cmd, Prm>(parameters, dbConnection);
                }
                
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
        private void CheckResponseType<TDbResponse, TDbParams>()
            where TDbResponse : DbResponse
            where TDbParams : TDbManifest
        {
            TDbParams dbMethod = DbMethodsCache.GetInstance<TDbParams>();
            if (GenericInstance<TDbResponse>.GetInstance().GetDbMethodCallType() != dbMethod.GetConfig().GetDbMethodCallType())
            {
                Console.WriteLine(GenericInstance<TDbResponse>.GetInstance().GetDbMethodCallType());
                Console.WriteLine(dbMethod.GetConfig().GetDbMethodCallType());
                throw new NotImplementedException();
            }
        }
        private TDbResponse BuildResponse<TDbResponse, TDbParams>(Drd dbReader, Cmd dbCommand)
            where TDbResponse : DbResponse
            where TDbParams : TDbManifest
        {
            TDbParams dbMethod = DbMethodsCache.GetInstance<TDbParams>();
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
        private TDbResponse BuildResponse<TDbResponse, TDbParams>(Cmd dbCommand)
            where TDbResponse : DbResponse
            where TDbParams : TDbManifest
        {
            TDbParams dbMethod = DbMethodsCache.GetInstance<TDbParams>();
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
    public sealed class DbConnectionHandler<TDbManifest> : DbConnectionHandler<DbDataReader, DbConnection, DbCommand, DbTransaction,DbParameter, TDbManifest> where TDbManifest : DbManifest
    {
        public class SQL : DbConnectionHandler<SqlDataReader, SqlConnection, SqlCommand, SqlTransaction, SqlParameter, TDbManifest> { }
        public class SQLite : DbConnectionHandler<SQLiteDataReader, SQLiteConnection, SQLiteCommand, SQLiteTransaction, SQLiteParameter, TDbManifest> { }
        public class OleDb : DbConnectionHandler<OleDbDataReader, OleDbConnection, OleDbCommand, OleDbTransaction, OleDbParameter, TDbManifest> { }
        public class Odbc : DbConnectionHandler<OdbcDataReader, OdbcConnection, OdbcCommand, OdbcTransaction, OdbcParameter, TDbManifest> { }        
        public class Oracle : DbConnectionHandler<OracleDataReader, OracleConnection, OracleCommand, OracleTransaction, OracleParameter, TDbManifest> { }
    }
}
