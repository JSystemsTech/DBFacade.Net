using System;
using System.Collections.Generic;
using System.Data;

namespace DbFacade.DataLayer.Models
{

    /// <summary>
    /// 
    /// </summary>
    public interface IDbResponse  {
        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <value>
        /// The return value.
        /// </value>
        int ReturnValue { get; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        Exception Error { get; }
        /// <summary>
        /// Gets a value indicating whether this instance has error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        bool HasError { get; }
        /// <summary>Gets the error message.</summary>
        /// <value>The error message.</value>
        string ErrorMessage { get; }
        /// <summary>Gets the error details.</summary>
        /// <value>The error details.</value>
        string ErrorDetails { get; }
        /// <summary>
        /// Gets the output value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object GetOutputValue(string key);

        /// <summary>Gets the output model.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        T GetOutputModel<T>(Action<T, IDbDataCollection> initialize) where T : class;
        /// <summary>Gets the output model.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///   <br />
        /// </returns>
        T GetOutputModel<T>() where T : class, IDbDataModel;

        /// <summary>Gets the data sets.</summary>
        /// <value>The data sets.</value>
        IEnumerable<IDbDataSet> DataSets { get; }

        /// <summary>Gets the data set.</summary>
        /// <value>The data set.</value>
        DataSet DataSet { get; }
    }

}