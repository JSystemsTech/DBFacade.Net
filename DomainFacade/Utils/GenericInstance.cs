using System;
using System.Collections.Generic;

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
    public sealed class GenericInstance
    {
        public static object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
        public static object GetInstanceWithArgArray(Type type, object[] paramsArray)
        {
            return Activator.CreateInstance(type, args: paramsArray);
        }
        public static object GetInstance(Type type, params object[] paramsArray)
        {
            return Activator.CreateInstance(type, args: paramsArray);
        }
    }
}
