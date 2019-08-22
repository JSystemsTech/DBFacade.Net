using DBFacade.DataLayer.CommandConfig;
using System;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.Manifest
{
    public abstract class DbManifest : IDbManifestMethod, IDisposable
    {
        private IDbCommandConfig Config { get; set; }
        public IDbCommandConfig GetConfig() => Config ?? BuildConfig();
        public Task<IDbCommandConfig> GetConfigAsync()
        {
            return Task.Run(() => Config ?? BuildConfig());
        } 
        protected abstract IDbCommandConfig BuildConfig();
        private bool Disposed = false;
        public void Dispose()
        {
            if (!Disposed)
            {
                if(Config != null)
                {
                    Config.Dispose();
                    Config = null;
                }                
                OnDispose();
                Disposed = true;
            }
        }
        public virtual void OnDispose() { }
    }
}
