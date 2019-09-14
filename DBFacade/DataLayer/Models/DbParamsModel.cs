
using DBFacade.Utils;
using System.Collections.Generic;
using System.Data.Common;

namespace DBFacade.DataLayer.Models
{
    public enum MethodRunMode
    {
        Normal = 0,
        Test = 1
    }
    public class DbParamsModel : IInternalDbParamsModel
    {
        public MethodRunMode RunMode  { get; private set; }
        public DbDataReader ResponseData { get; protected set; }
        public object ReturnValue { get; protected set; }
        public DbParamsModel()
        {
            RunMode = MethodRunMode.Normal;
        }
        private void SetRunAsTest<T>(object returnValue, IEnumerable<T> responseData, T singleResponseValue)
        {
            RunMode = MethodRunMode.Test;
            ReturnValue = returnValue;
            bool useEmptyTable = responseData == null && singleResponseValue == null;
            bool useIEnumerableTable = responseData != null;
            bool useSingleItemTable = singleResponseValue != null;

            ResponseData = 
                useEmptyTable ? MockDbTable.EmptyTable :
                useIEnumerableTable ? new MockDbTable<T>(responseData).ToDataReader() :
                useSingleItemTable ? new MockDbTable<T>(singleResponseValue).ToDataReader() : MockDbTable.EmptyTable;            
        }
        public void RunAsTest(object returnValue)=> SetRunAsTest<int?>(returnValue, null, null);
        public void RunAsTest<T>(IEnumerable<T> responseData, object returnValue) => SetRunAsTest(returnValue, responseData, default(T));        
        public void RunAsTest<T>(T responseData, object returnValue) => SetRunAsTest(returnValue, null, responseData);
        
    }
    interface IDbFunctionalTestParamsModel
    {
        IDbParamsModel ParamsModel { get; }
    }
    public sealed class MockParamsModel<DbParams> : DbParamsModel, IDbFunctionalTestParamsModel where DbParams : IDbParamsModel
    {
        public IDbParamsModel ParamsModel { get; private set; }
        public MockParamsModel(DbParams model, DbDataReader testResponseData) { ParamsModel = model; ResponseData = testResponseData; }
        public MockParamsModel(DbParams model, DbDataReader testResponseData, object returnValue) { ParamsModel = model; ResponseData = testResponseData; ReturnValue = returnValue; }
           
    }
    public class DbParamsModel<T> : DbParamsModel
    {
        public T Param1 { get; private set; }
        public DbParamsModel(T param1) : base() { Param1 = param1; }
        public DbParamsModel() : base() { }
    }
    public class DbParamsModel<T, U> : DbParamsModel<T>
    {
        public U Param2 { get; private set; }
        public DbParamsModel(T param1, U param2) : base(param1) { Param2 = param2; }
        public DbParamsModel() : base() { }
    }
    public class DbParamsModel<T, U, V> : DbParamsModel<T, U>
    {
        public V Param3 { get; private set; }
        public DbParamsModel(T param1, U param2, V param3) : base(param1, param2) { Param3 = param3; }
        public DbParamsModel() : base() { }
    }
    public class DbParamsModel<T, U, V, W> : DbParamsModel<T, U, V>
    {
        public W Param4 { get; private set; }
        public DbParamsModel(T param1, U param2, V param3, W param4) : base(param1, param2, param3) { Param4 = param4; }
        public DbParamsModel() : base() { }
    }
    public class DbParamsModel<T, U, V, W, X> : DbParamsModel<T, U, V, W>
    {
        public X Param5 { get; private set; }
        public DbParamsModel(T param1, U param2, V param3, W param4, X param5) : base(param1, param2, param3, param4) { Param5 = param5; }
        public DbParamsModel() : base() { }
    }
}
