using DomainFacade.DataLayer.Manifest;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DomainFacade.DataLayer.Models
{
    public abstract class DbResponse<TDbMethod> : DbResponse where TDbMethod : DbMethodCallType
    {
        public override Type GetDbMethodCallType() { return typeof(TDbMethod); }
        public void GetResponse() { }
        public DbResponse() { }
    }
    public abstract class DbResponse
    {
        public virtual Type GetDbMethodCallType() { return typeof(object); }
        public class TransactionModel: DbResponse<DbMethodCallType.Transaction>
        {
            public TransactionModel() {}
        }
        public abstract class FetchModel<T, DbMethod, TDbMethod> : DbResponse<TDbMethod> 
            where T : DbDataModel 
            where DbMethod : IDbMethod
            where TDbMethod : DbMethodCallType
        {
            internal IEnumerable<T> DataRecords { get; private set; }
            internal T Record { get; set; }
            private void AddFetchedData(DbDataReader dbReader)
            {
                List<T> dataRecords = new List<T>();
                while (dbReader.Read())
                {                   
                    T model = DbDataModel.ToDbDataModel<T, DbMethod>(dbReader);
                    dataRecords.Add(model);
                }
                DataRecords = dataRecords;
                if(DataRecords.Count() > 0)
                {
                    Record = DataRecords.First();
                }                
            }
            public static T DefaultModel = default(T);
            public FetchModel(DbDataReader dbReader) { AddFetchedData(dbReader); }
            public FetchModel() { }


            public new IEnumerable<T> GetResponse() { return DataRecords; }
            public List<T> GetResponseAsList()
            {
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
        
        public class FetchRecordModel<T, DbMethod> : FetchModel<T, DbMethod, DbMethodCallType.FetchRecord> where T : DbDataModel where DbMethod : IDbMethod
        {            
            public FetchRecordModel(DbDataReader dbReader) :base(dbReader) { }
            public FetchRecordModel() : base() { }
            public new T GetResponse() { return Record; }
        }
        public class FetchRecordsModel<T, DbMethod> : FetchModel<T, DbMethod, DbMethodCallType.FetchRecords> where T : DbDataModel where DbMethod : IDbMethod
        {
            public FetchRecordsModel(DbDataReader dbReader) : base(dbReader) { }
            public FetchRecordsModel() : base() {  }
            
        }
        protected class ReturnValueTransactionModel<U> : DbResponse<DbMethodCallType.TransactionWithReturn>
        {
            public U ReturnValue { get; private set; }
            public ReturnValueTransactionModel(object value)
            {                
                ReturnValue = (U)value;
            }
            public ReturnValueTransactionModel() : base() {}
        }
        protected class ReturnValueFetchRecordModel<T, DbMethod, U> : FetchModel <T, DbMethod, DbMethodCallType.FetchRecordWithReturn> where T : DbDataModel where DbMethod : IDbMethod
        {
            public U ReturnValue { get; private set; }
            public ReturnValueFetchRecordModel(object value, DbDataReader dbReader) : base(dbReader)
            {
                ReturnValue = (U)value;
            }
            public ReturnValueFetchRecordModel() : base() {}
            public new T GetResponse() { return Record; }
        }
        protected class ReturnValueFetchRecordsModel<T, DbMethod, U> : FetchModel<T, DbMethod, DbMethodCallType.FetchRecordsWithReturn> where T : DbDataModel where DbMethod : IDbMethod
        {
            public U ReturnValue { get; private set; }
            public ReturnValueFetchRecordsModel(object value, DbDataReader dbReader) : base(dbReader)
            {
                ReturnValue = (U)value;
            }
            public ReturnValueFetchRecordsModel() : base() { }
        }
    }
    
}
