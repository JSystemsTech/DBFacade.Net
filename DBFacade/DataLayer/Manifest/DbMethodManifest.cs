using System.Threading.Tasks;
using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.Models;
using DBFacade.Factories;

namespace DBFacade.DataLayer.Manifest
{
    public abstract class DbMethodManifest : SafeDisposableBase, IDbManifestMethod
    {
        private IDbCommandConfig InnerConfig { get; set; }
        public IDbCommandConfig Config => InnerConfig ?? BuildConfig();

        public Task<IDbCommandConfig> GetConfigAsync()
        {
            return Task.Run(() => Config);
        }

        protected abstract IDbCommandConfig BuildConfig();

        protected IDbCommandParameterConfigFactory<TDbParams> GetCommandParameterConfigFactory<TDbParams>()
            where TDbParams : IDbParamsModel
        {
            return new DbCommandParameterConfigFactory<TDbParams>();
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