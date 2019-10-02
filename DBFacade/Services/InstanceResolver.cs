using DBFacade.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBFacade.Services
{
    internal interface IInstanceResolver: ISafeDisposable
    {
        Type GetResolverType();
    }
    internal interface IInstanceResolver<T> : IInstanceResolver
    {
        C Get<C>() where C : T;
        Task<C> GetAsync<C>() where C : T;
        void Add<C>(C value) where C : T;
        Task AddAsync<C>(C value) where C : T;
    }
    internal class InstanceResolver<T> : ConcurrentDictionary<Type, T>, IInstanceResolver<T>
    {
        C IInstanceResolver<T>.Get<C>()
        {
            return (C)GetOrAdd(typeof(C), GenericInstance<C>.GetInstance());
        }
        void IInstanceResolver<T>.Add<C>(C value)
        {
            GetOrAdd(typeof(C), value);
        }
        async Task<C> IInstanceResolver<T>.GetAsync<C>()
        {
            return await Task.Run(async()=> (C)GetOrAdd(typeof(C), await GenericInstance<C>.GetInstanceAsync()));
        }
        async Task IInstanceResolver<T>.AddAsync<C>(C value)
        {
            await Task.Run(() => GetOrAdd(typeof(C), value));
        }
        public Type GetResolverType()
        {
            return typeof(T);
        }
 
        #region IDisposable Support
        private bool Disposed = false;
        private bool Disposing = false;
        public bool IsDisposing() { return Disposed || Disposing; }
        public void Dispose(bool calledFromDispose)
        {
            /* skip if already disposed or in the process of disposing*/
            if (!IsDisposing())
            {
                Disposing = true;
                foreach (KeyValuePair<Type, T> item in this)
                {
                    if (item.Value is ISafeDisposable)
                    {
                        (item.Value as ISafeDisposable).Dispose(calledFromDispose);
                    }
                    else if (item.Value is IDisposable)
                    {
                        (item.Value as IDisposable).Dispose();
                    }
                    TryRemove(item.Key, out T value);
                }
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
