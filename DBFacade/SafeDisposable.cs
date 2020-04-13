using System;

namespace DBFacade
{
    public interface ISafeDisposable : IDisposable
    {
        void Dispose(bool calledFromDispose);
    }

    public abstract class SafeDisposableBase : ISafeDisposable
    {
        #region IDisposable Support

        private bool Disposed;
        private bool Disposing;

        public bool IsDisposing()
        {
            return Disposed || Disposing;
        }

        protected abstract void OnDispose(bool calledFromDispose);
        protected abstract void OnDisposeComplete();

        public void Dispose(bool calledFromDispose)
        {
            /* skip if already disposed or in the process of disposing*/
            if (!IsDisposing())
            {
                Disposing = true;
                OnDispose(calledFromDispose);
                OnDisposeComplete();
                Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}