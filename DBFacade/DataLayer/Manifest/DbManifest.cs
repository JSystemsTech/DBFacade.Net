using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Models;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.Manifest
{
    
    public abstract class DbManifest : SafeDisposableBase, IDbManifestMethod
    {
        private IDbCommandConfig config { get; set; }
        public IDbCommandConfig Config { get => config ?? BuildConfig(); }

        public Task<IDbCommandConfig> GetConfigAsync() => Task.Run(() => Config);
        protected abstract IDbCommandConfig BuildConfig();
        
        protected IDbCommandParameterConfigFactory<TDbParams> GetCommandParameterConfigFactory<TDbParams>()
            where TDbParams : IDbParamsModel
        => new DbCommandParameterConfigFactory<TDbParams>();

        public virtual void OnDispose() { }

        #region SafeDisposable Support        
        protected override void OnDispose(bool calledFromDispose) {
            if(config != null)
            {
                config.Dispose(calledFromDispose);
            }            
        }

        protected override void OnDisposeComplete() {
            config = null;
        }
        #endregion
        
    }
}
