using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DbFacade.Utils
{
    internal class Converter<T>
    {
        private static Type Type = typeof(T);
        private static Type StringType = typeof(string);
        private static TypeConverter TypeConverter = TypeDescriptor.GetConverter(Type);
        public static bool CanConvertFromString = TypeConverter.CanConvertFrom(StringType);
        public static T FromString(string value)
        => CanConvertFromString ? (T)TypeConverter.ConvertFrom(value) : default;
        public static async Task<T> FromStringAsync(string value)
        {
            T convertValue = default;
            if (CanConvertFromString)
            {
                convertValue = (T)TypeConverter.ConvertFrom(value);
            }
            await Task.CompletedTask;
            return convertValue;
        }

    }
    internal static class Converter
    {
        private static Type StringType = typeof(string);
        private static TypeConverter GetConverter(this Type t) => TypeDescriptor.GetConverter(t);
        private static bool CanConvertFromString(this Type t) => t.GetConverter().CanConvertFrom(StringType);
        private static object FromString(this Type t, string value)
            => t.CanConvertFromString() ? t.GetConverter().ConvertFrom(value) : GenericInstance.GetInstanceWithArgArray(t, new[] { value });

        private static async Task<object> FromStringAsync(this Type t, string value)
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(t);
            bool CanConvertFromString = typeConverter.CanConvertFrom(StringType);
            object convertedValue = CanConvertFromString ? typeConverter.ConvertFrom(value) : await GenericInstance.GetInstanceWithArgArrayAsync(t, new[] { value });
            await Task.CompletedTask;
            return convertedValue;
        }
        public static object ParseTo(this Type targetType, string input)
        {
            try
            {
                return targetType.FromString(input);
            }
            catch
            {
                return null;
            }
        }
        public static async Task<object> ParseToAsync(this Type targetType, string input)
        {
            try
            {
                return await targetType.FromStringAsync(input);
            }
            catch
            {
                await Task.CompletedTask;
                return null;
            }
        }
    }
}
