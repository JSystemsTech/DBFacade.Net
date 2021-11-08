using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using DbFacade.Utils;

namespace DbFacade.DataLayer.Models
{
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

}