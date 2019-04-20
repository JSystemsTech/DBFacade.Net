using DomainFacade.DataLayer.CommandConfig;
using System;

namespace DomainFacade.DataLayer.Manifest
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbMethod
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        IDbCommandConfig GetConfig();
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <returns></returns>
        Type GetType();
    }
}
