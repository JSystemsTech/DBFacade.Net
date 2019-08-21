using System;

namespace DBFacade.Utils
{
    internal sealed class GenericInstance<T>
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static T GetInstance() => (T)Activator.CreateInstance(typeof(T));
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="paramsArray">The parameters array.</param>
        /// <returns></returns>
        public static T GetInstance(params object[] paramsArray)=> (T) Activator.CreateInstance(typeof(T), args: paramsArray);       
    }
    /// <summary>
    /// 
    /// </summary>
    internal sealed class GenericInstance
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object GetInstance(Type type) => Activator.CreateInstance(type);
        
        /// <summary>
        /// Gets the instance with argument array.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="paramsArray">The parameters array.</param>
        /// <returns></returns>
        public static object GetInstanceWithArgArray(Type type, object[] paramsArray) => Activator.CreateInstance(type, args: paramsArray);
        
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="paramsArray">The parameters array.</param>
        /// <returns></returns>
        public static object GetInstance(Type type, params object[] paramsArray) => Activator.CreateInstance(type, args: paramsArray);
        
    }
}
