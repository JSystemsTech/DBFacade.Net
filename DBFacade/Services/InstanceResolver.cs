using DBFacade.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBFacade.Services
{
    public interface IInstanceResolver: IDisposable
    {
        Type GetResolverType();
    }
    public interface IInstanceResolver<T> : IInstanceResolver
    {
        C Get<C>() where C : T;
        Task<C> GetAsync<C>() where C : T;
    }
    internal class InstanceResolver<T> : ConcurrentDictionary<Type, T>, IInstanceResolver<T>
    {
        C IInstanceResolver<T>.Get<C>()
        {
            return (C)GetOrAdd(typeof(C), GenericInstance<C>.GetInstance());
        }
        async Task<C> IInstanceResolver<T>.GetAsync<C>()
        {
            return await Task.Run(async()=> (C)GetOrAdd(typeof(C), await GenericInstance<C>.GetInstanceAsync()));
        }
        private bool Disposed { get; set; } 
        void IDisposable.Dispose()
        {
            if (!Disposed)
            {
                foreach (KeyValuePair<Type, T> item in this)
                {
                    if (item.Value is IDisposable)
                    {
                        (item.Value as IDisposable).Dispose();
                    }
                    TryRemove(item.Key, out T value);
                }
                Disposed = true;
            }
        }

        public Type GetResolverType()
        {
            return typeof(T);
        }
    }
}
