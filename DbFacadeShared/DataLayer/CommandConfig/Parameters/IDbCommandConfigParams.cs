using DbFacade.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public interface IDbCommandConfigParams<TDbParams> : IEnumerable, IDictionary<string, IDbCommandParameterConfig<TDbParams>>
    {
        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        DbCommandParameterConfigFactory<TDbParams> Factory { get; }
        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        Task AddAsync(string key, IDbCommandParameterConfig<TDbParams> value);
    }
}