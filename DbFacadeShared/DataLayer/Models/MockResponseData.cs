using DbFacade.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class MockResponseData
    {
        /// <summary>
        /// Gets or sets the response data.
        /// </summary>
        /// <value>
        /// The response data.
        /// </value>
        internal DbDataReader ResponseData { get; set; }
        /// <summary>
        /// Gets or sets the return value.
        /// </summary>
        /// <value>
        /// The return value.
        /// </value>
        internal int ReturnValue { get; set; }
        /// <summary>
        /// Gets or sets the output values.
        /// </summary>
        /// <value>
        /// The output values.
        /// </value>
        internal IDictionary<string, object> OutputValues { get; set; }

        /// <summary>
        /// Creates the specified response data.
        /// </summary>
        /// <param name="ResponseData">The response data.</param>
        /// <param name="outputValueHandler">The output value handler.</param>
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates the specified output value handler.
        /// </summary>
        /// <param name="outputValueHandler">The output value handler.</param>
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
        internal static MockResponseData Create(Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => Create(MockDbTable.EmptyTable, outputValueHandler, returnValue);
        /// <summary>
        /// Creates the specified response data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        /// <param name="outputValueHandler">The output value handler.</param>
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
        internal static MockResponseData Create<T>(IEnumerable<T> responseData, Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => Create(new MockDbTable<T>(responseData).ToDataReader(), outputValueHandler, returnValue);

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="ResponseData">The response data.</param>
        /// <param name="outputValueHandler">The output value handler.</param>
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
        private static async Task<MockResponseData> CreateAsync(DbDataReader ResponseData, Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        {
            var mockResponseData = Create(ResponseData, outputValueHandler, returnValue);
            await Task.CompletedTask;
            return mockResponseData;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="outputValueHandler">The output value handler.</param>
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
        internal static async Task<MockResponseData> CreateAsync(Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => await CreateAsync(MockDbTable.EmptyTable, outputValueHandler, returnValue);
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        /// <param name="outputValueHandler">The output value handler.</param>
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
        internal static async Task<MockResponseData> CreateAsync<T>(IEnumerable<T> responseData, Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => await CreateAsync(new MockDbTable<T>(responseData).ToDataReader(), outputValueHandler, returnValue);
    }

}