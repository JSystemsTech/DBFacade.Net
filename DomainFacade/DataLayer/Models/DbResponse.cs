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
        public class FetchModel<T, DbMethod> : DbResponse where T : DbDataModel where DbMethod : IDbMethod
        {
            internal IEnumerable<T> DataRecords { get; private set; }
            private void AddFetchedData(DbDataReader dbReader)
            {
                List<T> dataRecords = new List<T>();
                while (dbReader.Read())
                {                   
                    T model = DbDataModel.ToDbDataModel<T, DbMethod>(dbReader);
                    dataRecords.Add(model);
                }
                DataRecords = dataRecords;
            }
            public static T DefaultModel = default(T);
            public FetchModel(DbDataReader dbReader) { AddFetchedData(dbReader); }
            public FetchModel() { }
        }
        public class FetchRecordModel<T, DbMethod> : FetchModel<T, DbMethod> where T : DbDataModel where DbMethod : IDbMethod
        {
            private T Record { get; set; }
            
            public FetchRecordModel(DbDataReader dbReader) :base(dbReader) { DBMethodType = DbMethodType.FetchRecord; }
            public FetchRecordModel() : base() { DBMethodType = DbMethodType.FetchRecord; }
            public new T GetResponse() { return Record; }
        }
        public class FetchRecordsModel<T, DbMethod> : FetchModel<T, DbMethod> where T : DbDataModel where DbMethod : IDbMethod
        {
            public FetchRecordsModel(DbDataReader dbReader) : base(dbReader) { DBMethodType = DbMethodType.FetchRecords; }
            public FetchRecordsModel() : base() { DBMethodType = DbMethodType.FetchRecords; }
            public new IEnumerable<T> GetResponse() { return DataRecords; }
            public List<T> GetResponseAsList() {
                List<T> dataList = new List<T>();
                foreach (T item in DataRecords)
                {
                    dataList.Add(item);
                }
                return dataList;
            }
            public T[] GetResponseAsArray()
            {
                return GetResponseAsList().ToArray();
            }
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
        protected class ReturnValueFetchRecordModel<T, DbMethod, U> : FetchRecordModel <T, DbMethod> where T : DbDataModel where DbMethod : IDbMethod
        {
            public U ReturnValue { get; private set; }
            public ReturnValueFetchRecordModel(object value, DbDataReader dbReader) : base(dbReader)
            {
                ReturnValue = (U)value;
                DBMethodType = DbMethodType.FetchRecordWithReturn;
            }
            public ReturnValueFetchRecordModel() : base() { DBMethodType = DbMethodType.FetchRecordWithReturn; }
        }
        protected class ReturnValueFetchRecordsModel<T, DbMethod, U> : FetchRecordsModel<T, DbMethod> where T : DbDataModel where DbMethod : IDbMethod
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
