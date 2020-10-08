using System;
using System.Threading.Tasks;

namespace DbFacade.Utils
{
    public interface IAsyncInit
    {
        Task InitAsync();
    }
    internal static class GenericInstance
    {
        
        public static T GetInstance<T>()
        => (T) Activator.CreateInstance(typeof(T));
        

        public static T GetInstance<T>(params object[] paramsArray)
        => (T) Activator.CreateInstance(typeof(T), paramsArray);
        

        public static async Task<T> GetInstanceAsync<T>(params object[] paramsArray)
            where T : IAsyncInit
        {
            T obj = (T)Activator.CreateInstance(typeof(T), paramsArray);
            await obj.InitAsync();
            return obj;
        }
        public static async Task<T> GetInstanceAsync<T>()
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            await Task.CompletedTask;
            return obj;
        }
        public static async Task<object> GetInstanceAsync(Type t)
        {
            object obj = Activator.CreateInstance(t);
            await Task.CompletedTask;
            return obj;
        }


        public static object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static object GetInstanceWithArgArray(Type type, object[] paramsArray)
        {
            return Activator.CreateInstance(type, paramsArray);
        }
        public static async Task<object> GetInstanceWithArgArrayAsync(Type type, object[] paramsArray)
        {
            object obj = Activator.CreateInstance(type, paramsArray);
            await Task.CompletedTask;
            return obj;
        }



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