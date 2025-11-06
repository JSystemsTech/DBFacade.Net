using DbFacade.DataLayer.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig
{
    public interface IDbCommandMethod
    {

        /// <summary>Executes the specified parameters.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        IDbResponse Execute<T>(T parameters);
        /// <summary>Executes this instance.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        IDbResponse Execute();


        /// <summary>Executes the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IDbResponse> ExecuteAsync<T>(T parameters);
        /// <summary>Executes the asynchronous.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IDbResponse> ExecuteAsync();

        /// <summary>Executes the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IDbResponse> ExecuteAsync<T>(CancellationToken cancellationToken, T parameters);
        /// <summary>Executes the asynchronous.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IDbResponse> ExecuteAsync(CancellationToken cancellationToken);

        /// <summary>Enables the mock mode.</summary>
        /// <param name="setMockData"></param>
        void EnableMockMode(Action<IMockResponse> setMockData);

        /// <summary>Disables the mock mode.</summary>
        void DisableMockMode();
    }
}