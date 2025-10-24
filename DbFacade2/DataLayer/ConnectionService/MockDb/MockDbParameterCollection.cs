using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// 
    /// </summary>
    internal class MockDbParameterCollection : DbParameterCollection
    {
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        private List<MockDbParameter> Parameters { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MockDbParameterCollection"/> class.
        /// </summary>
        public MockDbParameterCollection()
        {
            Parameters = new List<MockDbParameter>();
        }
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public override int Count => Parameters.Count;

        public override object SyncRoot => throw new NotImplementedException();

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override int Add(object value)
        {
            if (value is MockDbParameter parameter && !Contains(parameter))
            {
                Parameters.Add(parameter);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="values">The values.</param>
        public override void AddRange(Array values)
        {
            if (values is IEnumerable<MockDbParameter> range)
            {
                Parameters.AddRange(range);
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public override void Clear() => Parameters.Clear();

        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public override bool Contains(object value)
        {
            if (value is MockDbParameter parameter)
            {
                return Contains(parameter.ParameterName);
            }
            return false;
        }

        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public override bool Contains(string value) => Parameters.Any(p => p.ParameterName == value);

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        public override void CopyTo(Array array, int index)
        {
            if (array is MockDbParameter[] values)
            {
                Parameters.CopyTo(values, index);
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator GetEnumerator() => Parameters.GetEnumerator();

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override int IndexOf(object value)
        {
            if (value is MockDbParameter parameter)
            {
                return Parameters.IndexOf(parameter);
            }
            return -1;
        }
        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public override int IndexOf(string parameterName) => IndexOf(Parameters.FirstOrDefault(p => p.ParameterName == parameterName));


        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public override void Insert(int index, object value)
        {
            if (value is MockDbParameter parameter)
            {
                Parameters.Insert(index, parameter);
            }
        }

        /// <summary>
        /// Removes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public override void Remove(object value)
        {
            if (value is MockDbParameter parameter)
            {
                Parameters.Remove(parameter);
            }
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public override void RemoveAt(int index) => Parameters.RemoveAt(index);

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        public override void RemoveAt(string parameterName) => RemoveAt(IndexOf(parameterName));

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        protected override DbParameter GetParameter(int index) => Parameters.ElementAtOrDefault(index);
        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        protected override DbParameter GetParameter(string parameterName) => GetParameter(IndexOf(parameterName));

        /// <summary>
        /// Sets the parameter.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        protected override void SetParameter(int index, DbParameter value)
        {
            if (value is MockDbParameter parameter && index >= 0 && index < Count)
            {
                bool hasExistingRecord =
                    Contains(parameter) ||
                    Parameters.ElementAtOrDefault(index) is MockDbParameter elParam && elParam.ParameterName == parameter.ParameterName;
                if (!hasExistingRecord)
                {
                    Parameters[index] = parameter;
                }
            }
        }

        /// <summary>
        /// Sets the parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        protected override void SetParameter(string parameterName, DbParameter value) => SetParameter(IndexOf(parameterName), value);
        /// <summary>
        /// Gets the return value parameter.
        /// </summary>
        /// <returns></returns>
        public MockDbParameter GetReturnValueParam()
        => Parameters.Where(entry => entry.Direction == System.Data.ParameterDirection.ReturnValue).FirstOrDefault();

        /// <summary>
        /// Gets the output parameters.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MockDbParameter> GetOutputParams()
        => Parameters.Where(entry => entry.Direction == System.Data.ParameterDirection.Output);
    }
}
