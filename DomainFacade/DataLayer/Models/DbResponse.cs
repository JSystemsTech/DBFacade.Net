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
            internal List<T> DataRecords { get; set; }
            private void AddFetchedData(DbDataReader dbReader)
            {
                if(DataRecords == null)
                {
                    DataRecords = new List<T>();
                }
                while (dbReader.Read())
                {                   
                    T model = GenericInstance<T>.GetInstance(new object[] { dbReader });
                    DataRecords.Add(model);
                }                
            }
            public static T DefaultModel = default(T);
            public FetchModel(DbDataReader dbReader) { AddFetchedData(dbReader); }
            public FetchModel() { }
        }
        public class FetchRecordModel<T> : FetchModel<T> where T : DbDataModel
        {
            private T Record { get; set; }
            
            public FetchRecordModel(DbDataReader dbReader):base(dbReader) { DBMethodType = DbMethodType.FetchRecord; }
            public FetchRecordModel() : base() { DBMethodType = DbMethodType.FetchRecord; }
            public new T GetResponse() { return Record; }
        }
        public class FetchRecordsModel<T> : FetchModel<T> where T : DbDataModel
        {
            public FetchRecordsModel(DbDataReader dbReader) : base(dbReader) { DBMethodType = DbMethodType.FetchRecords; }
            public FetchRecordsModel() : base() { DBMethodType = DbMethodType.FetchRecords; }
            public new List<T> GetResponse() { return DataRecords; }
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
            public ReturnValueFetchRecordModel(object value, DbDataReader dbReader) : base(dbReader)
            {
                ReturnValue = (U)value;
                DBMethodType = DbMethodType.FetchRecordWithReturn;
            }
            public ReturnValueFetchRecordModel() : base() { DBMethodType = DbMethodType.FetchRecordWithReturn; }
        }
        protected class ReturnValueFetchRecordsModel<T, U> : FetchRecordsModel<T> where T : DbDataModel
        {
            public U ReturnValue { get; private set; }
            public ReturnValueFetchRecordsModel(object value, DbDataReader dbReader) : base(dbReader)
            {
                ReturnValue = (U)value;
                DBMethodType = DbMethodType.FetchRecordsWithReturn;
            }
            public ReturnValueFetchRecordsModel() : base() { DBMethodType = DbMethodType.FetchRecordsWithReturn; }
        }
    }
    
}
