using System;
using System.Threading.Tasks;

namespace DBFacade.Utils
{
    internal sealed class GenericInstance<T>
    {
        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static T GetInstance()
        {
            return (T) Activator.CreateInstance(typeof(T));
        }

        public static Task<T> GetInstanceAsync()
        {
            return Task.Run(() => GetInstance());
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <param name="paramsArray">The parameters array.</param>
        /// <returns></returns>
        public static T GetInstance(params object[] paramsArray)
        {
            return (T) Activator.CreateInstance(typeof(T), paramsArray);
        }

        public static Task<T> GetInstanceAsync(params object[] paramsArray)
        {
            return Task.Run(() => GetInstance(paramsArray));
        }
    }

    /// <summary>
    /// </summary>
    internal sealed class GenericInstance
    {
        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        ///     Gets the instance with argument array.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="paramsArray">The parameters array.</param>
        /// <returns></returns>
        public static object GetInstanceWithArgArray(Type type, object[] paramsArray)
        {
            return Activator.CreateInstance(type, paramsArray);
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="paramsArray">The parameters array.</param>
        /// <returns></returns>
        public static object GetInstance(Type type, params object[] paramsArray)
        {
            return Activator.CreateInstance(type, paramsArray);
        }
    }
}