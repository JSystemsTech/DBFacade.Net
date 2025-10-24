using DbFacade.DataLayer.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig
{
    public interface IDbCommandMethod
    {

        /// <summary>Executes the specified parameters.</summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        IDbResponse Execute(object parameters = null);

        /// <summary>Executes the asynchronous.</summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IDbResponse> ExecuteAsync(object parameters = null);

        /// <summary>Executes the asynchronous.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IDbResponse> ExecuteAsync(CancellationToken cancellationToken, object parameters = null);

        /// <summary>Enables the mock mode.</summary>
        /// <param name="setMockData"></param>
        void EnableMockMode(Action<IMockResponse> setMockData);

        /// <summary>Disables the mock mode.</summary>
        void DisableMockMode();
    }
}