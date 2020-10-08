using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using DbFacade.Utils;

namespace DbFacade.Services
{
    internal interface IInstanceResolver : ISafeDisposable,IAsyncInit
    {
        Type GetResolverType();
    }

    internal interface IInstanceResolver<T> : IInstanceResolver
        where T : IAsyncInit
    {
        C Get<C>() where C : T;
        Task<C> GetAsync<C>() where C : T;
        void Add<C>(C value) where C : T;
        Task AddAsync<C>(C value) where C : T;
    }

    internal class InstanceResolver<T> : ConcurrentDictionary<Type, T>, IInstanceResolver<T>
        where T : IAsyncInit
    {
        public async Task InitAsync()
        {
            await Task.CompletedTask;
        }
        C IInstanceResolver<T>.Get<C>()
        {
            var type = typeof(C);
            if (!ContainsKey(type)) GetOrAdd(type, GenericInstance.GetInstance<C>());
            TryGetValue(type, out T value);
            return (C) value;
        }

        void IInstanceResolver<T>.Add<C>(C value)
        {
            GetOrAdd(typeof(C), value);
        }

        
        async Task<C> IInstanceResolver<T>.GetAsync<C>()
        {
            var type = typeof(C);
            if (!ContainsKey(type))
            {
                C newValue = await GenericInstance.GetInstanceAsync<C>();
                GetOrAdd(type, newValue);
                return newValue;
            }
            TryGetValue(type, out T value);
            return (C)value;
        }

        async Task IInstanceResolver<T>.AddAsync<C>(C value)
        {
            GetOrAdd(typeof(C), value);
            await Task.CompletedTask;
        }

        public Type GetResolverType()
        {
            return typeof(T);
        }

        #region IDisposable Support

        private bool _disposed;
        private bool _disposing;

        public bool IsDisposing()
        {
            return _disposed || _disposing;
        }

        public void Dispose(bool calledFromDispose)
        {
            /* skip if already disposed or in the process of disposing*/
            if (!IsDisposing())
            {
                _disposing = true;
                foreach (var item in this)
                {
                    if (item.Value is ISafeDisposable valueAsISafeDisposable)
                        valueAsISafeDisposable.Dispose(calledFromDispose);
                    else if (item.Value is IDisposable valueAsIDisposable) valueAsIDisposable.Dispose();
                    TryRemove(item.Key, out var value);
                }

                _disposed = true;
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