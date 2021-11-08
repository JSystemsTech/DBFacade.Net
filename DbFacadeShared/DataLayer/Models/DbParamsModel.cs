using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using DbFacade.Utils;

namespace DbFacade.DataLayer.Models
{
    public class DbParamsModel 
    {      

    }

    internal sealed class MockResponseData
    {
        internal DbDataReader ResponseData { get; set; }
        internal int ReturnValue { get; set; }
        internal IDictionary<string, object> OutputValues { get; set; }

        private static MockResponseData Create(DbDataReader ResponseData, Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        {
            var mockResponseData = new MockResponseData()
            {
                ResponseData = ResponseData,
                ReturnValue = returnValue is int value ? value : default(int),
                OutputValues = new Dictionary<string, object>()
            };
            if (outputValueHandler is Action<IDictionary<string, object>> handler)
            {
                handler(mockResponseData.OutputValues);
            }
            return mockResponseData;
        }        
        internal static MockResponseData Create(Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => Create(MockDbTable.EmptyTable, outputValueHandler, returnValue);
        internal static MockResponseData Create<T>(IEnumerable<T> responseData, Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => Create(new MockDbTable<T>(responseData).ToDataReader(), outputValueHandler, returnValue);

        private static async Task<MockResponseData> CreateAsync(DbDataReader ResponseData, Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        {
            var mockResponseData = Create(ResponseData, outputValueHandler, returnValue);
            await Task.CompletedTask;
            return mockResponseData;
        }
        internal static async Task<MockResponseData> CreateAsync(Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => await CreateAsync(MockDbTable.EmptyTable, outputValueHandler, returnValue);
        internal static async Task<MockResponseData> CreateAsync<T>(IEnumerable<T> responseData, Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => await CreateAsync(new MockDbTable<T>(responseData).ToDataReader(), outputValueHandler, returnValue);
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