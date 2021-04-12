using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DbFacade.Utils
{
    public interface IAsyncInit
    {
        Task InitAsync();
    }
    internal static class New<T>
    {
        public static readonly Func<T> Instance = Creator();
        public static bool HasDefaultConstructor(Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }
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
    internal static class New
    {
        public static Func<object> Instance(Type t) => Creator(t);
        public static bool HasDefaultConstructor(Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }
        private static Func<object> Creator(Type t)
        {
            if (t == typeof(string))
                return Expression.Lambda<Func<object>>(Expression.Constant(string.Empty)).Compile();

            if (HasDefaultConstructor(t))
                return Expression.Lambda<Func<object>>(Expression.New(t)).Compile();

            return () => FormatterServices.GetUninitializedObject(t);
        }
    }
    public class InstanceCreatorService
    {
        private static IDictionary<Type, Func<object>> constructorMap = new Dictionary<Type, Func<object>>();
        internal static T Instance<T>()
            => constructorMap.ContainsKey(typeof(T)) ? (T)constructorMap[typeof(T)](): New<T>.Instance();
        internal static object Instance(Type type)
            => constructorMap.ContainsKey(type) ? constructorMap[type]() : New.Instance(type)();
        public static void AddCreator<T>(Func<T> creator)
        {
            constructorMap.Add(typeof(T), () => creator());
        }
    }
    internal static class GenericInstance
    {


        public static T GetInstance<T>()
            => InstanceCreatorService.Instance<T>();
        
        public static async Task<T> GetInstanceAsync<T>()
        {
            T obj = InstanceCreatorService.Instance<T>();
            await Task.CompletedTask;
            return obj;
        }
        public static async Task<object> GetInstanceAsync(Type t)
        {
            object obj = InstanceCreatorService.Instance(t);
            await Task.CompletedTask;
            return obj;
        }


        public static object GetInstance(Type type)
            => InstanceCreatorService.Instance(type);


        public static bool IsNullableType<T>()
            => Nullable.GetUnderlyingType(typeof(T)) != null;

        public static async Task<bool> IsNullableTypeAsync<T>()
        {
            bool isNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
            await Task.CompletedTask;
            return isNullable;
        }
    }
}