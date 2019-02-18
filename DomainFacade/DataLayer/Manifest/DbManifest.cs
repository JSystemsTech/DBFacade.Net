using DomainFacade.DataLayer.CommandConfig;

namespace DomainFacade.DataLayer.Manifest
{
    public abstract class DbManifest : IDbMethod
    {
        protected IDbCommandConfig Config { get; private set; } 
        public IDbCommandConfig GetConfig()
        {
            if(Config == null)
            {
                Config = GetConfigCore();
            }
            return Config;
        }
        protected abstract IDbCommandConfig GetConfigCore();
    }
}
