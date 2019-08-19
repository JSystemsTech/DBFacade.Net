using DBFacade.DataLayer.CommandConfig;
using System;

namespace DBFacade.DataLayer.Manifest
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITDbManifestMethod
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
