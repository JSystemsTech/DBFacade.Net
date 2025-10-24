using System;

namespace DbFacade.Factories
{
    internal static class TypeFactory
    {
        internal static T CreateInstance<T>()
        {
#if NET8_0_OR_GREATER
            return Activator.CreateInstance<T>();

#else
            return (T)Activator.CreateInstance(typeof(T));
#endif
        }
    }
}
