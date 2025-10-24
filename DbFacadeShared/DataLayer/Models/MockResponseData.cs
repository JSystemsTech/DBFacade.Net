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

        private static readonly DataSet EmptyDataSet = MockResponseDataResponseBuilder.ResolveDataSet(MockResponseDataResponseBuilder.CreateDataSet());
        internal static readonly MockResponseData Empty = new MockResponseData()
        {
            GetResponseData = EmptyDataSet.CreateDataReader,
            ReturnValue = default(int),
            OutputValues = new Dictionary<string, object>()
        };

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

        /// <summary>Adds the response data.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        void AddResponseData<T>(params T[] responseData) where T : class;

        /// <summary>Adds the output value.</summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        void AddOutputValue(string name, object value);
        
        /// <summary>
        /// Adds the return value.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        void AddReturnValue(int returnValue);

        /// <summary>Clears this instance.</summary>
        void Clear();
    }
    
    /// <summary>
    /// 
    /// </summary>
    internal class MockResponseDataResponseBuilder: IMockResponseDataResponseBuilder
    {

        private int ReturnValue { get; set; }
        private readonly Dictionary<string, object> OutputValues;
        private readonly DataSet TestDataSet;
        
        internal MockResponseDataResponseBuilder()
        {
            TestDataSet = CreateDataSet();
            ReturnValue = default(int);
            OutputValues = new Dictionary<string, object>();
        }
        internal static DataSet CreateDataSet()
        => new DataSet($"MockDataSetUID{Guid.NewGuid()}");
        internal static DataSet ResolveDataSet(DataSet dataSet)
        {
            if (dataSet.Tables.Count == 0)
            {
                dataSet.Tables.Add(new DataTable("EmptyMockDbTable"));
            }
            return dataSet;
        }

        /// <summary>
        /// Adds the response data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        public void AddResponseData<T>(IEnumerable<T> responseData)
        => AddResponseDataCore(responseData);
        /// <summary>Adds the response data.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        public void AddResponseData<T>(params T[] responseData)
            where T : class
        => AddResponseDataCore(responseData);
        private void AddResponseDataCore<T>(IEnumerable<T> responseData)
        {
            if (responseData.TryGetDataTable(out DataTable dt))
            {
                TestDataSet.Tables.Add(dt);
            }
        }

        /// <summary>Adds the output value.</summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void AddOutputValue(string name, object value)
        {
//#if NET8_0_OR_GREATER
//            if (OutputValues.ContainsKey(name)) 
//            {
//                OutputValues[name] = value;
//            }
//            else
//            {
//                OutputValues.Add(name, value);
//            }
//#else
            if (OutputValues.ContainsKey(name)) 
            {
                OutputValues[name] = value;
            }
            else
            {
                OutputValues.Add(name, value);
            }
//#endif
        }
        /// <summary>
        /// Adds the return value.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        public void AddReturnValue(int returnValue)
        => ReturnValue = returnValue;

        /// <summary>Clears this instance.</summary>
        public void Clear()
        {
            OutputValues.Clear();
            TestDataSet.Tables.Clear();
            ReturnValue = default(int);
        }
        
        private DataTableReader GetResponseData()
        => ResolveDataSet(TestDataSet).CreateDataReader();
        internal MockResponseData Build(Action<IMockResponseDataResponseBuilder> handler)
        {
            handler(this);
            return new MockResponseData()
            {
                GetResponseData = GetResponseData,
                ReturnValue = ReturnValue,
                OutputValues = OutputValues
            };
        }

    }

}