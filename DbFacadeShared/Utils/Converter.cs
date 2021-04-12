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
}
