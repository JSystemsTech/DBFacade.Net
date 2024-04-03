using DbFacade.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MockResponseData
    {
        /// <summary>
        /// Gets or sets the response data.
        /// </summary>
        /// <value>
        /// The response data.
        /// </value>
        internal Func<DbDataReader> GetResponseData { get; set; }
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
        /// <param name="getResponseData">The get response data.</param>
        /// <param name="outputValueHandler">The output value handler.</param>
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
        internal static MockResponseData Create(Func<DbDataReader> getResponseData, Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        {
            var mockResponseData = new MockResponseData()
            {
                GetResponseData = getResponseData,
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
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
        public static MockResponseData Create(int returnValue)
        => Create(builder => {
            builder.AddReturnValue(returnValue);
        });
        /// <summary>
        /// Creates the specified output value handler.
        /// </summary>
        /// <param name="outputValueHandler">The output value handler.</param>
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
        public static MockResponseData Create(Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => Create(builder => {
            builder.AddOutputValueHandler(outputValueHandler);
            builder.AddReturnValue(returnValue);
        });
        /// <summary>
        /// Creates the specified response data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        /// <param name="outputValueHandler">The output value handler.</param>
        /// <param name="returnValue">The return value.</param>
        /// <returns></returns>
        public static MockResponseData Create<T>(IEnumerable<T> responseData, Action<IDictionary<string, object>> outputValueHandler = null, int? returnValue = null)
        => Create(builder => {
            builder.AddOutputValueHandler(outputValueHandler);
            builder.AddReturnValue(returnValue);
            builder.AddResponseData(responseData);

        });

        /// <summary>
        /// Creates the specified builder handler.
        /// </summary>
        /// <param name="builderHandler">The builder handler.</param>
        /// <returns></returns>
        public static MockResponseData Create(Action<IMockResponseDataResponseBuilder> builderHandler) {
            MockResponseDataResponseBuilder builder = new MockResponseDataResponseBuilder();
            builderHandler(builder);
            return builder.Build();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IMockResponseDataResponseBuilder
    {
        /// <summary>
        /// Adds the response data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        void AddResponseData<T>(IEnumerable<T> responseData);
        /// <summary>
        /// Adds the output value handler.
        /// </summary>
        /// <param name="outputValueHandler">The output value handler.</param>
        void AddOutputValueHandler(Action<IDictionary<string, object>> outputValueHandler = null);
        /// <summary>
        /// Adds the return value.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        void AddReturnValue(int? returnValue = null);
    }
    /// <summary>
    /// 
    /// </summary>
    internal class MockResponseDataResponseBuilder: IMockResponseDataResponseBuilder
    {

        /// <summary>
        /// Gets or sets the return value.
        /// </summary>
        /// <value>
        /// The return value.
        /// </value>
        internal int? ReturnValue { get; private set; }
        /// <summary>
        /// Gets or sets the output values.
        /// </summary>
        /// <value>
        /// The output values.
        /// </value>
        internal Action<IDictionary<string, object>> OutputValueHandler { get; set; }

        /// <summary>
        /// Gets or sets the test data set.
        /// </summary>
        /// <value>
        /// The test data set.
        /// </value>
        private DataSet TestDataSet { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MockResponseDataResponseBuilder"/> class.
        /// </summary>
        public MockResponseDataResponseBuilder()
        {
            TestDataSet = new DataSet($"MockDataSetUID{Guid.NewGuid()}");

        }
        /// <summary>
        /// Adds the response data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        public void AddResponseData<T>(IEnumerable<T> responseData)
        {
            if (responseData.TryGetDataTable(out DataTable dt))
            {
                TestDataSet.Tables.Add(dt);
            }
        }
        /// <summary>
        /// Adds the output value handler.
        /// </summary>
        /// <param name="outputValueHandler">The output value handler.</param>
        public void AddOutputValueHandler(Action<IDictionary<string, object>> outputValueHandler = null)
        => OutputValueHandler = outputValueHandler;
        /// <summary>
        /// Adds the return value.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        public void AddReturnValue(int? returnValue = null)
        => ReturnValue = returnValue;
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public MockResponseData Build()
        {
            if (TestDataSet.Tables.Count == 0)
            {
                TestDataSet.Tables.Add(new DataTable("EmptyMockDbTable"));
            }
            return MockResponseData.Create(()=>TestDataSet.CreateDataReader(), OutputValueHandler, ReturnValue);
        }

    }

}