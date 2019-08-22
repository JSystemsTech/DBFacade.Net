
using DBFacade.Utils;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DBFacade.DataLayer.Models
{
    public enum MethodRunMode
    {
        Normal = 0,
        Test = 1
    }
    public class DbParamsModel : IDbParamsModel
    {
        private MethodRunMode _RunMode = MethodRunMode.Normal;
        private DbDataReader _ResponseData { get; set; }
        private object _ReturnValue { get; set; }
        private void SetRunAsTest<T>(object returnValue, IEnumerable<T> responseData, T singleResponseValue)
        {
            _RunMode = MethodRunMode.Test;
            _ReturnValue = returnValue;
            bool useEmptyTable = responseData == null && singleResponseValue == null;
            bool useIEnumerableTable = responseData != null;
            bool useSingleItemTable = singleResponseValue != null;

            _ResponseData = 
                useEmptyTable ? MockDbTable.EmptyTable :
                useIEnumerableTable ? new MockDbTable<T>(responseData).ToDataReader() :
                useSingleItemTable ? new MockDbTable<T>(singleResponseValue).ToDataReader() : MockDbTable.EmptyTable;            
        }
        public void RunAsTest(object returnValue)=> SetRunAsTest<int?>(returnValue, null, null);
        public void RunAsTest<T>(IEnumerable<T> responseData, object returnValue) => SetRunAsTest(returnValue, responseData, default(T));        
        public void RunAsTest<T>(T responseData, object returnValue) => SetRunAsTest(returnValue, null, responseData);
        
        public MethodRunMode _GetRunMode() => _RunMode;
        public DbDataReader _GetResponseData() => _ResponseData;
        public object _GetReturnValue() => _ReturnValue;

        public DbParamsModel() { }
    }
    public interface IDbFunctionalTestParamsModel
    {
        IDbParamsModel GetParamsModel();
        DbDataReader GetTestResponse();
        object GetReturnValue();
    }
    internal sealed class MockParamsModel<DbParams> : DbParamsModel, IDbFunctionalTestParamsModel where DbParams : IDbParamsModel
    {
        private DbParams Model { get; set; }
        private DbDataReader TestResponseData { get; set; }
        private object ReturnValue { get; set; }
        public MockParamsModel(DbParams model, DbDataReader testResponseData) { Model = model; TestResponseData = testResponseData; }
        public MockParamsModel(DbParams model, DbDataReader testResponseData, object returnValue) { Model = model; TestResponseData = testResponseData; ReturnValue = returnValue; }
       
        public IDbParamsModel GetParamsModel()
        {
            return Model;
        }
       
        public DbDataReader GetTestResponse()
        {
            return TestResponseData;
        }
        public object GetReturnValue()
        {
            return ReturnValue;
        }
    }
    public class SimpleDbParamsModel<T> : DbParamsModel
    {
        public T Param1 { get; private set; }
        public SimpleDbParamsModel(T param1) : base() { Param1 = param1; }
        public SimpleDbParamsModel() : base() { }
    }
    public class SimpleDbParamsModel<T, U> : SimpleDbParamsModel<T>
    {
        public U Param2 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2) : base(param1) { Param2 = param2; }
        public SimpleDbParamsModel() : base() { }
    }
    public class SimpleDbParamsModel<T, U, V> : SimpleDbParamsModel<T, U>
    {
        public V Param3 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3) : base(param1, param2) { Param3 = param3; }
        public SimpleDbParamsModel() : base() { }
    }
    public class SimpleDbParamsModel<T, U, V, W> : SimpleDbParamsModel<T, U, V>
    {
        public W Param4 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3, W param4) : base(param1, param2, param3) { Param4 = param4; }
        public SimpleDbParamsModel() : base() { }
    }
    public class SimpleDbParamsModel<T, U, V, W, X> : SimpleDbParamsModel<T, U, V, W>
    {
        public X Param5 { get; private set; }
        public SimpleDbParamsModel(T param1, U param2, V param3, W param4, X param5) : base(param1, param2, param3, param4) { Param5 = param5; }
        public SimpleDbParamsModel() : base() { }
    }
}
