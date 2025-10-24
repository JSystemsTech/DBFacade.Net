using DbFacade.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig
{
    public interface IDbCommandMethod
    {
        /// <summary>Sets the mock data.</summary>
        /// <param name="builderHandler">The builder handler.</param>
        void SetMockData(Action<IMockResponseDataResponseBuilder> builderHandler, bool clear = false);
        /// <summary>Clears the mock data.</summary>
        void ClearMockData();

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
    }
}