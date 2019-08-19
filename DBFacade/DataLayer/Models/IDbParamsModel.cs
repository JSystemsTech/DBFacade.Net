using System.Collections.Generic;
using System.Data;

namespace DBFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbParamsModel
    {
        /// <summary>
        /// Runs as test.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        void RunAsTest(object returnValue);
        /// <summary>
        /// Runs as test.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        /// <param name="returnValue">The return value.</param>
        void RunAsTest<T>(IEnumerable<T> responseData, object returnValue);
        /// <summary>
        /// Runs as test.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        /// <param name="returnValue">The return value.</param>
        void RunAsTest<T>(T responseData, object returnValue);
        /// <summary>
        /// Gets the run mode.
        /// </summary>
        /// <returns></returns>
        MethodRunMode _GetRunMode();
        /// <summary>
        /// Gets the response data.
        /// </summary>
        /// <returns></returns>
        IDataReader _GetResponseData();
        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <returns></returns>
        object _GetReturnValue();
    }
}
