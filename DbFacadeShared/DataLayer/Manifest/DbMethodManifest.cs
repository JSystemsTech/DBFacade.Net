using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Factories;
using DbFacade.Utils;

namespace DbFacade.DataLayer.Manifest
{
    public abstract class DbMethodManifest : SafeDisposableBase, IDbManifestMethod,IAsyncInit
    {
        private IDbCommandConfig InnerConfig { get; set; }
        public IDbCommandConfig GetConfig()
        {
            if (InnerConfig == null)
            {
                InnerConfig = BuildConfig();
            }
            return InnerConfig;
        }
        public async Task<IDbCommandConfig> GetConfigAsync()
        {
            if (InnerConfig == null)
            {
                InnerConfig = await BuildConfigAsync();
            }
            await Task.CompletedTask;
            return InnerConfig;
        }
        protected virtual async Task<IDbCommandConfig> BuildConfigAsync()
        {
            IDbCommandConfig config = GetConfig();
            await Task.CompletedTask;
            return config;
        }

        protected abstract IDbCommandConfig BuildConfig();

        public async Task InitAsync()
        {
            await Task.CompletedTask;
        }
        public virtual void OnDispose()
        {
        }

        #region SafeDisposable Support

        protected override void OnDispose(bool calledFromDispose)
        {
            OnDispose();
            if (InnerConfig != null) InnerConfig.Dispose(calledFromDispose);
        }

        protected override void OnDisposeComplete()
        {
            InnerConfig = null;
        }

        #endregion
    }
}