using DbFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IDbCommandMethod<TDbParams,TDbDataModel> : ISafeDisposable
        where TDbDataModel : DbDataModel
    {

        /// <summary>
        /// Executes the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Execute(TDbParams parameters);

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(TDbParams parameters);


        /// <summary>
        /// Mocks the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Mock(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        /// <summary>
        /// Mocks the specified parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="responseData">The response data.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);

        /// <summary>
        /// Mocks the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> MockAsync(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        /// <summary>
        /// Mocks the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="responseData">The response data.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IParameterlessDbCommandMethod<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Execute();
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> ExecuteAsync();
        /// <summary>
        /// Mocks the specified return value.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Mock(int returnValue, Action<IDictionary<string, object>> outputValues = null);
        /// <summary>
        /// Mocks the specified response data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Mock<T>(IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);

        /// <summary>
        /// Mocks the asynchronous.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> MockAsync(int returnValue, Action<IDictionary<string, object>> outputValues = null);
        /// <summary>
        /// Mocks the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseData">The response data.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public interface IDbCommandMethod<TDbParams>
    {
        /// <summary>
        /// Executes the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IDbResponse Execute(TDbParams parameters);
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IDbResponse> ExecuteAsync(TDbParams parameters);
        /// <summary>
        /// Mocks the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        IDbResponse Mock(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        /// <summary>
        /// Mocks the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        Task<IDbResponse> MockAsync(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null);
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDbCommandMethod
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        IDbResponse Execute();
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IDbResponse> ExecuteAsync();
        /// <summary>
        /// Mocks the specified return value.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        IDbResponse Mock(int returnValue, Action<IDictionary<string, object>> outputValues = null);
        /// <summary>
        /// Mocks the asynchronous.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        /// <param name="outputValues">The output values.</param>
        /// <returns></returns>
        Task<IDbResponse> MockAsync(int returnValue, Action<IDictionary<string, object>> outputValues = null);
    }

    
}