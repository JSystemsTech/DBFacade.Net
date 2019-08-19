using DBFacade.Utils;
using System;
using System.Collections.Generic;

namespace DBFacade.Services
{
    public interface IInstanceResolver: IDisposable
    {
        Type GetResolverType();
    }
    public interface IInstanceResolver<T> : IInstanceResolver
    {
        C Get<C>() where C : T;
    }
    internal class InstanceResolver<T> : Dictionary<Type, T>, IInstanceResolver<T>
    {
        private C Resolve<C>()
            where C : T
        {
            Type instanceType = typeof(C);
            if (!ContainsKey(instanceType))
            {
                Add(instanceType, GenericInstance<C>.GetInstance());
            }
            return (C)this[instanceType];
        }

        C IInstanceResolver<T>.Get<C>()
        {
            return Resolve<C>();
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
                    Remove(item.Key);
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
