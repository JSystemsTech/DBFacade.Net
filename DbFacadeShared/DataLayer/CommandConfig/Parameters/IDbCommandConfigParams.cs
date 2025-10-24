using DbFacade.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public interface IDbCommandConfigParams<TDbParams>
    {
        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        DbCommandParameterConfigFactory<TDbParams> Factory { get; }

        /// <summary>Adds the specified name.</summary>
        /// <param name="name">The name.</param>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        void Add(string name, IDbCommandParameterConfig<TDbParams> value);
    }
}