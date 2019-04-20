using DBFacade.DataLayer.CommandConfig;

namespace DBFacade.DataLayer.Manifest
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IDbMethod" />
    public abstract class DbManifest : IDbMethod
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        protected IDbCommandConfig Config { get; private set; }
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        public IDbCommandConfig GetConfig()
        {
            if (Config == null)
            {
                Config = GetConfigCore();
            }
            return Config;
        }
        /// <summary>
        /// Gets the configuration core.
        /// </summary>
        /// <returns></returns>
        protected abstract IDbCommandConfig GetConfigCore();
    }
}
