using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using DbFacade.Utils;

namespace DbFacade.DataLayer.Models
{
    public enum MethodRunMode
    {
        Normal = 0,
        Test = 1
    }

    public class DbParamsModel 
    {
        public DbParamsModel()
        {
            RunMode = MethodRunMode.Normal;
        }

        public MethodRunMode RunMode { get; private set; }
        public DbDataReader ResponseData { get; protected set; }
        public int ReturnValue { get; protected set; }
        internal IDictionary<string,object> OutputValues { get; set; }

        internal void RunAsTest(int returnValue, IDictionary<string, object> outputValues = null)
        {
            SetRunAsTest<int?>(returnValue, null, null, outputValues);
        }

        internal void RunAsTest<T>(IEnumerable<T> responseData, int returnValue, IDictionary<string, object> outputValues = null)
        {
            SetRunAsTest(returnValue, responseData, default, outputValues);
        }

        internal void RunAsTest<T>(T responseData, int returnValue, IDictionary<string, object> outputValues = null)
        {
            SetRunAsTest(returnValue, null, responseData, outputValues);
        }

        internal async Task RunAsTestAsync(int returnValue, IDictionary<string, object> outputValues = null)
        => await SetRunAsTestAsync<int?>(returnValue, null, null, outputValues);
        internal async Task RunAsTestAsync<T>(IEnumerable<T> responseData, int returnValue, IDictionary<string, object> outputValues = null)
        => await SetRunAsTestAsync(returnValue, responseData, default, outputValues);
        internal async Task RunAsTestAsync<T>(T responseData, int returnValue, IDictionary<string, object> outputValues = null)
        => await SetRunAsTestAsync(returnValue, null, responseData, outputValues);

        private void SetRunAsTest<T>(int returnValue, IEnumerable<T> responseData, T singleResponseValue, IDictionary<string, object> outputValues = null)
        {
            RunMode = MethodRunMode.Test;
            ReturnValue = returnValue;
            OutputValues = outputValues;
            var useEmptyTable = responseData == null && singleResponseValue == null;
            var useIEnumerableTable = responseData != null;

            ResponseData =
                useEmptyTable ? MockDbTable.EmptyTable :
                useIEnumerableTable ? new MockDbTable<T>(responseData).ToDataReader() :
                new MockDbTable<T>(singleResponseValue).ToDataReader();
        }
        private async Task SetRunAsTestAsync<T>(int returnValue, IEnumerable<T> responseData, T singleResponseValue, IDictionary<string, object> outputValues = null)
        {
            RunMode = MethodRunMode.Test;
            ReturnValue = returnValue;
            OutputValues = outputValues;
            var useEmptyTable = responseData == null && singleResponseValue == null;
            var useIEnumerableTable = responseData != null;

            ResponseData =
                useEmptyTable ? MockDbTable.EmptyTable :
                useIEnumerableTable ? new MockDbTable<T>(responseData).ToDataReader() :
                new MockDbTable<T>(singleResponseValue).ToDataReader();
            await Task.CompletedTask;
        }
    }

    public sealed class MockParamsModel<DbParams> : DbParamsModel
        where DbParams : DbParamsModel
    {
        public MockParamsModel(DbParams model, DbDataReader testResponseData,IDictionary<string, object> outputValues = null)
        :this(model, testResponseData, 0, outputValues){}

        public MockParamsModel(DbParams model, DbDataReader testResponseData, int returnValue = 0, IDictionary<string, object> outputValues = null)
        {
            ParamsModel = model;
            ResponseData = testResponseData;
            ReturnValue = returnValue;
            OutputValues = outputValues;
        }

        public DbParamsModel ParamsModel { get; }
    }

    public class DbParamsModel<T> : DbParamsModel
    {
        public DbParamsModel(T param1)
        {
            Param1 = param1;
        }

        public DbParamsModel()
        {
        }

        public T Param1 { get; }
    }

    public class DbParamsModel<T, U> : DbParamsModel<T>
    {
        public DbParamsModel(T param1, U param2) : base(param1)
        {
            Param2 = param2;
        }

        public DbParamsModel()
        {
        }

        public U Param2 { get; }
    }

    public class DbParamsModel<T, U, V> : DbParamsModel<T, U>
    {
        public DbParamsModel(T param1, U param2, V param3) : base(param1, param2)
        {
            Param3 = param3;
        }

        public DbParamsModel()
        {
        }

        public V Param3 { get; }
    }

    public class DbParamsModel<T, U, V, W> : DbParamsModel<T, U, V>
    {
        public DbParamsModel(T param1, U param2, V param3, W param4) : base(param1, param2, param3)
        {
            Param4 = param4;
        }

        public DbParamsModel()
        {
        }

        public W Param4 { get; }
    }

    public class DbParamsModel<T, U, V, W, X> : DbParamsModel<T, U, V, W>
    {
        public DbParamsModel(T param1, U param2, V param3, W param4, X param5) : base(param1, param2, param3, param4)
        {
            Param5 = param5;
        }

        public DbParamsModel()
        {
        }

        public X Param5 { get; }
    }
}