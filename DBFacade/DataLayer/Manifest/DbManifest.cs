using DBFacade.DataLayer.CommandConfig;
using System;

namespace DBFacade.DataLayer.Manifest
{
    public abstract class DbManifest : IDbManifestMethod, IDisposable
    {
        private IDbCommandConfig Config { get; set; }
        public IDbCommandConfig GetConfig() => Config ?? BuildConfig();
        protected abstract IDbCommandConfig BuildConfig();
        private bool Disposed = false;
        public void Dispose()
        {
            if (!Disposed)
            {
                Config.Dispose();
                Config = null;
                OnDispose();
                Disposed = true;
            }
        }
        public virtual void OnDispose() { }
    }
}
