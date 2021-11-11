using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DbFacadeShared.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static class DataDictionaryExtensions
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        private static T Parse<T>(object value, T defaultValue = default)
        {
            try
            {
                if (value == null)
                {
                    return defaultValue;
                }
                Type returnType = typeof(T);
                Type underlyingType = Nullable.GetUnderlyingType(returnType);
                Type castType = underlyingType != null ? underlyingType : returnType;
                if (value is T tVal)
                {
                    return tVal;
                }
                if (castType == typeof(string))
                {
                    return (T)(object)value.ToString();
                }
                else if (castType == typeof(MailAddress))
                {
                    return (T)(object)new MailAddress(value.ToString());
                }
                return IsEnum(castType, value) && value is int intValue ?
                    (T)Enum.ToObject(castType, intValue) :
                    (T)Convert.ChangeType(value, castType);
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string GetName(this IDictionary<string,object> data, string key)
            => data.ContainsKey(key) ? key :
            data.Keys.FirstOrDefault(k => string.Equals(k, key, StringComparison.CurrentCultureIgnoreCase));
        /// <summary>
        /// Determines whether the specified enum type is enum.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified enum type is enum; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsEnum(Type enumType, object value)
        {
            try
            {
                return Enum.IsDefined(enumType, value);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="convert">The convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        internal static T GetValue<T>(this IDictionary<string, object> data, string key, Func<string, T> convert, T defaultValue = default)
            => data.GetValue<T, string>(key, convert, defaultValue);
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParse">The type of the parse.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="convert">The convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        internal static T GetValue<T, TParse>(this IDictionary<string, object> data, string key, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
        {
            TParse defaultParseValue = default(TParse);
            TParse parseValue = data.GetValue(key, defaultParseValue);
            return parseValue.CompareTo(defaultParseValue) == 0 ? defaultValue : convert(parseValue);
        }
        internal static T GetValue<T>(this IDictionary<string, object> data, string key, T defaultValue = default)
        {
            string name = data.GetName(key);
            if (name == null)
            {
                throw new KeyNotFoundException($"No data entry named '{key}' exists");
            }
            return name != null &&
                data.TryGetValue(name, out object value) &&
                value != null &&
                value != DBNull.Value ?
                Parse(value, defaultValue) : defaultValue;
        }

        /// <summary>
        /// Gets the date time value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        internal static DateTime? GetDateTimeValue(this IDictionary<string, object> data, string key, string format, DateTimeStyles style = DateTimeStyles.None)
            => data.GetDateTimeValue(key, format, CultureInfo.InvariantCulture, style);
        /// <summary>
        /// Gets the date time value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        internal static DateTime? GetDateTimeValue(this IDictionary<string, object> data, string key, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
        => data.GetValue(key, value =>
            !string.IsNullOrWhiteSpace(value) &&
            DateTime.TryParseExact(value, format, provider, style, out DateTime outValue) ? outValue : (DateTime?)null
            );
        /// <summary>
        /// Gets the formatted date time string value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        internal static string GetFormattedDateTimeStringValue(this IDictionary<string, object> data, string key, string format)
        => data.GetValue<DateTime?>(key) is DateTime convertedValue ? convertedValue.ToString(format) : null;
        /// <summary>
        /// Gets the enumerable value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        internal static IEnumerable<T> GetEnumerableValue<T>(this IDictionary<string, object> data, string key, string delimeter = ",")
        => data.GetValue(key, value => value.Split(delimeter.ToArray()).Select(v => Parse<T>(v)));
        /// <summary>
        /// Gets the flag value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="trueValue">The true value.</param>
        /// <returns></returns>
        internal static bool GetFlagValue<T>(this IDictionary<string, object> data, string key, T trueValue)
            where T : IComparable
        => data.GetValue<T>(key).CompareTo(trueValue) == 0;


        /// <summary>
        /// Gets the value asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="convert">The convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        internal static async Task<T> GetValueAsync<T>(this IDictionary<string, object> data, string key, Func<string, T> convert, T defaultValue = default)
            => await data.GetValueAsync<T, string>(key, convert, defaultValue);
        /// <summary>
        /// Gets the value asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParse">The type of the parse.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="convert">The convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        internal static async Task<T> GetValueAsync<T, TParse>(this IDictionary<string, object> data, string key, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
        {
            T result = data.GetValue(key, convert, defaultValue);
            await Task.CompletedTask;
            return result;
        }
        /// <summary>
        /// Gets the value asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        internal static async Task<T> GetValueAsync<T>(this IDictionary<string, object> data, string key, T defaultValue = default)
        {
            T result = data.GetValue(key, defaultValue);
            await Task.CompletedTask;
            return result;
        }
        /// <summary>
        /// Gets the enumerable value asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        internal static async Task<IEnumerable<T>> GetEnumerableValueAsync<T>(this IDictionary<string, object> data, string key, string delimeter = ",")
        {
            IEnumerable<T> result = data.GetEnumerableValue<T>(key, delimeter);
            await Task.CompletedTask;
            return result;
        }
        /// <summary>
        /// Gets the date time value asynchronous.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        internal static async Task<DateTime?> GetDateTimeValueAsync(this IDictionary<string, object> data, string key, string format, DateTimeStyles style = DateTimeStyles.None)
            => await data.GetDateTimeValueAsync(key, format, CultureInfo.InvariantCulture, style);
        /// <summary>
        /// Gets the date time value asynchronous.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        internal static async Task<DateTime?> GetDateTimeValueAsync(this IDictionary<string, object> data, string key, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
        {
            DateTime? result = data.GetDateTimeValue(key, format, provider, style);
            await Task.CompletedTask;
            return result;
        }
        /// <summary>
        /// Gets the formatted date time string value asynchronous.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        internal static async Task<string> GetFormattedDateTimeStringValueAsync(this IDictionary<string, object> data, string key, string format)
        {
            string result = data.GetFormattedDateTimeStringValue(key, format);
            await Task.CompletedTask;
            return result;
        }
        /// <summary>
        /// Gets the flag value asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="trueValue">The true value.</param>
        /// <returns></returns>
        internal static async Task<bool> GetFlagValueAsync<T>(this IDictionary<string, object> data, string key, T trueValue)
            where T : IComparable
        {
            bool result = data.GetFlagValue(key, trueValue);
            await Task.CompletedTask;
            return result;
        }
    }
}
