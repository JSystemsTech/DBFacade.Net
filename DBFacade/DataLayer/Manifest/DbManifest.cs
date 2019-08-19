using DBFacade.DataLayer.CommandConfig;

namespace DBFacade.DataLayer.Manifest
{
    public abstract class DbManifest : ITDbManifestMethod
    {
        private IDbCommandConfig Config { get; set; }
        public IDbCommandConfig GetConfig() => Config ?? GetConfigCore();
        protected abstract IDbCommandConfig GetConfigCore();
    }
}
