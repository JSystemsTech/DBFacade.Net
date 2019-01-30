using System;

namespace DomainFacade.Utils
{
    public sealed class GenericInstance<T>
    {
        public static T GetInstance()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        public static T GetInstance(params object[] paramsArray)
        {
            return (T)Activator.CreateInstance(typeof(T), args: paramsArray);
        }
    }
}
