using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DbFacade.Utils
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Converter<T>
    {
        /// <summary>
        /// The type
        /// </summary>
        private static Type Type = typeof(T);
        /// <summary>
        /// The string type
        /// </summary>
        private static Type StringType = typeof(string);
        /// <summary>
        /// The type converter
        /// </summary>
        private static TypeConverter TypeConverter = TypeDescriptor.GetConverter(Type);
        /// <summary>
        /// The can convert from string
        /// </summary>
        public static bool CanConvertFromString = TypeConverter.CanConvertFrom(StringType);
        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T FromString(string value)
        => CanConvertFromString ? (T)TypeConverter.ConvertFrom(value) : default;
        /// <summary>
        /// Froms the string asynchronous.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
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
}
