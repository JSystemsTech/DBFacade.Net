using System;

namespace DbFacade
{
    /// <summary>
    ///   <br />
    /// </summary>
    internal abstract class SafeDisposableBase : IDisposable
    {
        #region IDisposable Support

        /// <summary>
        /// The disposed
        /// </summary>
        private bool Disposed;
        /// <summary>
        /// The disposing
        /// </summary>
        private bool Disposing;

        /// <summary>
        /// Determines whether this instance is disposing.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is disposing; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDisposing()
        {
            return Disposed || Disposing;
        }

        /// <summary>
        /// Called when [dispose].
        /// </summary>
        /// <param name="calledFromDispose">if set to <c>true</c> [called from dispose].</param>
        protected abstract void OnDispose(bool calledFromDispose);
        /// <summary>
        /// Called when [dispose complete].
        /// </summary>
        protected abstract void OnDisposeComplete();

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="calledFromDispose"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}