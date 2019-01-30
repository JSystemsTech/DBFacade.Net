using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Utils;
using System.Collections.Generic;
using System.Data.Common;

namespace DomainFacade.DataLayer.Models
{
    public class DbResponse
    {
        public enum DbMethodType
        {
            FetchRecord,
            FetchRecords,
            Transaction,
            FetchRecordWithReturn,
            FetchRecordsWithReturn,
            TransactionWithReturn
        }
        public DbMethodType DBMethodType { get; private set; }
        public void GetResponse() { }
        public DbResponse() { }

        public class TransactionModel: DbResponse{
            public TransactionModel() { DBMethodType = DbMethodType.Transaction; }
        }
        public class FetchModel<T> : DbResponse where T : DbDataModel
        {
            internal IEnumerable<T> DataRecords { get; private set; }
            private void AddFetchedData(DbDataReader dbReader, IDbMethod dbMethod)
            {
                List<T> dataRecords = new List<T>();
                while (dbReader.Read())
                {                   
                    T model = GenericInstance<T>.GetInstance(new object[] { dbReader, dbMethod });
                    dataRecords.Add(model);
                }
                DataRecords = dataRecords;
            }
            public static T DefaultModel = default(T);
            public FetchModel(DbDataReader dbReader, IDbMethod dbMethod) { AddFetchedData(dbReader, dbMethod); }
            public FetchModel() { }
        }
        public class FetchRecordModel<T> : FetchModel<T> where T : DbDataModel
        {
            private T Record { get; set; }
            
            public FetchRecordModel(DbDataReader dbReader, IDbMethod dbMethod) :base(dbReader, dbMethod) { DBMethodType = DbMethodType.FetchRecord; }
            public FetchRecordModel() : base() { DBMethodType = DbMethodType.FetchRecord; }
            public new T GetResponse() { return Record; }
        }
        public class FetchRecordsModel<T> : FetchModel<T> where T : DbDataModel
        {
            public FetchRecordsModel(DbDataReader dbReader, IDbMethod dbMethod) : base(dbReader, dbMethod) { DBMethodType = DbMethodType.FetchRecords; }
            public FetchRecordsModel() : base() { DBMethodType = DbMethodType.FetchRecords; }
            public new IEnumerable<T> GetResponse() { return DataRecords; }
        }
        protected class ReturnValueTransactionModel<U> : TransactionModel
        {
            public U ReturnValue { get; private set; }
            public ReturnValueTransactionModel(object value)
            {                
                ReturnValue = (U)value;
                DBMethodType = DbMethodType.TransactionWithReturn;
            }
            public ReturnValueTransactionModel() : base() { DBMethodType = DbMethodType.TransactionWithReturn; }
        }
        protected class ReturnValueFetchRecordModel<T, U> : FetchRecordModel <T> where T : DbDataModel
        {
            public U ReturnValue { get; private set; }
            public ReturnValueFetchRecordModel(object value, DbDataReader dbReader, IDbMethod dbMethod) : base(dbReader, dbMethod)
            {
                ReturnValue = (U)value;
                DBMethodType = DbMethodType.FetchRecordWithReturn;
            }
            public ReturnValueFetchRecordModel() : base() { DBMethodType = DbMethodType.FetchRecordWithReturn; }
        }
        protected class ReturnValueFetchRecordsModel<T, U> : FetchRecordsModel<T> where T : DbDataModel
        {
            public U ReturnValue { get; private set; }
            public ReturnValueFetchRecordsModel(object value, DbDataReader dbReader, IDbMethod dbMethod) : base(dbReader, dbMethod)
            {
                ReturnValue = (U)value;
                DBMethodType = DbMethodType.FetchRecordsWithReturn;
            }
            public ReturnValueFetchRecordsModel() : base() { DBMethodType = DbMethodType.FetchRecordsWithReturn; }
        }
    }
    
}
