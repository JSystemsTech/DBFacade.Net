using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DbFacade.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncInit
    {
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task InitAsync();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal static class New<T>
    {
        /// <summary>
        /// The instance
        /// </summary>
        public static readonly Func<T> Instance = Creator();
        /// <summary>
        /// Determines whether [has default constructor] [the specified t].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        ///   <c>true</c> if [has default constructor] [the specified t]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasDefaultConstructor(Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }
        /// <summary>
        /// Creators this instance.
        /// </summary>
        /// <returns></returns>
        private static Func<T> Creator()
        {
            Type t = typeof(T);
            if (t == typeof(string))
                return Expression.Lambda<Func<T>>(Expression.Constant(string.Empty)).Compile();

            if (HasDefaultConstructor(t))
                return Expression.Lambda<Func<T>>(Expression.New(t)).Compile();

            return () => (T)FormatterServices.GetUninitializedObject(t);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    internal static class New
    {
        /// <summary>
        /// Instances the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        public static Func<object> Instance(Type t) => Creator(t);
        /// <summary>
        /// Determines whether [has default constructor] [the specified t].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        ///   <c>true</c> if [has default constructor] [the specified t]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasDefaultConstructor(Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }
        /// <summary>
        /// Creators the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static Func<object> Creator(Type t)
        {
            if (t == typeof(string))
                return Expression.Lambda<Func<object>>(Expression.Constant(string.Empty)).Compile();

            if (HasDefaultConstructor(t))
                return Expression.Lambda<Func<object>>(Expression.New(t)).Compile();

            return () => FormatterServices.GetUninitializedObject(t);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class InstanceCreatorService
    {
        /// <summary>
        /// The constructor map
        /// </summary>
        private static IDictionary<Type, Func<object>> constructorMap = new Dictionary<Type, Func<object>>();
        /// <summary>
        /// Instances this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static T Instance<T>()
            => constructorMap.ContainsKey(typeof(T)) ? (T)constructorMap[typeof(T)](): New<T>.Instance();
        /// <summary>
        /// Instances the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        internal static object Instance(Type type)
            => constructorMap.ContainsKey(type) ? constructorMap[type]() : New.Instance(type)();
        /// <summary>
        /// Adds the creator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creator">The creator.</param>
        public static void AddCreator<T>(Func<T> creator)
        {
            constructorMap.Add(typeof(T), () => creator());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    internal static class GenericInstance
    {


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>()
            => InstanceCreatorService.Instance<T>();

        /// <summary>
        /// Gets the instance asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> GetInstanceAsync<T>()
        {
            T obj = InstanceCreatorService.Instance<T>();
            await Task.CompletedTask;
            return obj;
        }
        /// <summary>
        /// Gets the instance asynchronous.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        public static async Task<object> GetInstanceAsync(Type t)
        {
            object obj = InstanceCreatorService.Instance(t);
            await Task.CompletedTask;
            return obj;
        }


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static object GetInstance(Type type)
            => InstanceCreatorService.Instance(type);


        /// <summary>
        /// Determines whether [is nullable type].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///   <c>true</c> if [is nullable type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullableType<T>()
            => Nullable.GetUnderlyingType(typeof(T)) != null;

        /// <summary>
        /// Determines whether [is nullable type asynchronous].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///   <c>true</c> if [is nullable type asynchronous]; otherwise, <c>false</c>.
        /// </returns>
        public static async Task<bool> IsNullableTypeAsync<T>()
        {
            bool isNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
            await Task.CompletedTask;
            return isNullable;
        }
    }
}