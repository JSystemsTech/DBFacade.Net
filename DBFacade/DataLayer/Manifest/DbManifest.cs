using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.DataLayer.Models.Validators.Rules;
using System;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.Manifest
{
    public abstract class DbManifest : SafeDisposableBase, IDbManifestMethod
    {
        private IDbCommandConfig Config { get; set; }
        public IDbCommandConfig GetConfig() => Config ?? BuildConfig();
        public Task<IDbCommandConfig> GetConfigAsync()
        {
            return Task.Run(() => Config ?? BuildConfig());
        } 
        protected abstract IDbCommandConfig BuildConfig();
        
        public virtual void OnDispose() { }

        #region SafeDisposable Support        
        protected override void OnDispose(bool calledFromDispose) {
            if(Config != null)
            {
                Config.Dispose(calledFromDispose);
            }            
        }

        protected override void OnDisposeComplete() {
            Config = null;
        }
        #endregion
        
    }
}
