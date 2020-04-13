using System.Collections.Generic;
using System.Data.Common;
using DBFacade.Utils;

namespace DBFacade.DataLayer.Models
{
    public enum MethodRunMode
    {
        Normal = 0,
        Test = 1
    }

    public class DbParamsModel : IInternalDbParamsModel
    {
        public DbParamsModel()
        {
            RunMode = MethodRunMode.Normal;
        }

        public MethodRunMode RunMode { get; private set; }
        public DbDataReader ResponseData { get; protected set; }
        public object ReturnValue { get; protected set; }

        public void RunAsTest(object returnValue)
        {
            SetRunAsTest<int?>(returnValue, null, null);
        }

        public void RunAsTest<T>(IEnumerable<T> responseData, object returnValue)
        {
            SetRunAsTest(returnValue, responseData, default(T));
        }

        public void RunAsTest<T>(T responseData, object returnValue)
        {
            SetRunAsTest(returnValue, null, responseData);
        }

        private void SetRunAsTest<T>(object returnValue, IEnumerable<T> responseData, T singleResponseValue)
        {
            RunMode = MethodRunMode.Test;
            ReturnValue = returnValue;
            var useEmptyTable = responseData == null && singleResponseValue == null;
            var useIEnumerableTable = responseData != null;
            var useSingleItemTable = singleResponseValue != null;

            ResponseData =
                useEmptyTable ? MockDbTable.EmptyTable :
                useIEnumerableTable ? new MockDbTable<T>(responseData).ToDataReader() :
                useSingleItemTable ? new MockDbTable<T>(singleResponseValue).ToDataReader() : MockDbTable.EmptyTable;
        }
    }

    internal interface IDbFunctionalTestParamsModel
    {
        IDbParamsModel ParamsModel { get; }
    }

    public sealed class MockParamsModel<DbParams> : DbParamsModel, IDbFunctionalTestParamsModel
        where DbParams : IDbParamsModel
    {
        public MockParamsModel(DbParams model, DbDataReader testResponseData)
        {
            ParamsModel = model;
            ResponseData = testResponseData;
        }

        public MockParamsModel(DbParams model, DbDataReader testResponseData, object returnValue)
        {
            ParamsModel = model;
            ResponseData = testResponseData;
            ReturnValue = returnValue;
        }

        public IDbParamsModel ParamsModel { get; }
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