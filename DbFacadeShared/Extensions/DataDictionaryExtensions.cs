using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DbFacadeShared.Extensions
{
    internal static class Types
    {
        public static Type Byte = typeof(byte);
        public static Type ByteOptional = typeof(byte?);
        public static Type Sbyte = typeof(sbyte);
        public static Type SbyteOptional = typeof(sbyte?);
        public static Type Short = typeof(short);
        public static Type ShortOptional = typeof(short?);
        public static Type Ushort = typeof(ushort);
        public static Type UshortOptional = typeof(ushort?);
        public static Type Int = typeof(int);
        public static Type IntOptional = typeof(int?);
        public static Type Uint = typeof(uint);
        public static Type UintOptional = typeof(uint?);
        public static Type Long = typeof(long);
        public static Type LongOptional = typeof(long?);
        public static Type Ulong = typeof(ulong);
        public static Type UlongOptional = typeof(ulong?);
        public static Type Float = typeof(float);
        public static Type FloatOptional = typeof(float?);
        public static Type Double = typeof(double);
        public static Type DoubleOptional = typeof(double?);
        public static Type Decimal = typeof(decimal);
        public static Type DecimalOptional = typeof(decimal?);
        public static Type DateTime = typeof(DateTime);
        public static Type DateTimeOptional = typeof(DateTime?);
        public static Type TimeSpan = typeof(TimeSpan);
        public static Type TimeSpanOptional = typeof(TimeSpan?);
        public static Type Guid = typeof(Guid);
        public static Type GuidOptional = typeof(Guid?);
        public static Type Char = typeof(char);
        public static Type CharOptional = typeof(char?);
        public static Type Bool = typeof(bool);
        public static Type BoolOptional = typeof(bool?);
        public static Type String = typeof(string);


    }
    internal static class DataDictionaryExtensions
    {

        /// <summary>The converters</summary>
        private static readonly ConcurrentDictionary<Type, TypeConverter> Converters
        = new ConcurrentDictionary<Type, TypeConverter>();
        private static readonly IDictionary<Type, Func<string, object>> StringConverters = new Dictionary<Type, Func<string, object>>() {
            { Types.Guid, str=>  Guid.TryParse(str, out Guid value) ? value: default(Guid) },
            { Types.GuidOptional, str=>  Guid.TryParse(str, out Guid value) ? value: default(Guid?) },
            { Types.DateTime, str=>  DateTime.TryParse(str, out DateTime value) ? value: default(DateTime) },
            { Types.DateTimeOptional, str=>  DateTime.TryParse(str, out DateTime value) ? value: default(DateTime?) },
            { Types.TimeSpan, str=>  TimeSpan.TryParse(str, out TimeSpan value) ? value: default(TimeSpan) },
            { Types.TimeSpanOptional, str=>  TimeSpan.TryParse(str, out TimeSpan value) ? value: default(TimeSpan?) },
            { Types.Byte, str=>  byte.TryParse(str, out byte value) ? value: default(byte) },
            { Types.ByteOptional, str=>  byte.TryParse(str, out byte value) ? value: default(byte?) },
            { Types.Sbyte, str=>  sbyte.TryParse(str, out sbyte value) ? value: default(sbyte) },
            { Types.SbyteOptional, str=>  sbyte.TryParse(str, out sbyte value) ? value: default(sbyte?) },
            { Types.Short, str=>  short.TryParse(str, out short value) ? value: default(short) },
            { Types.ShortOptional, str=>  short.TryParse(str, out short value) ? value: default(short?) },
            { Types.Ushort, str=>  ushort.TryParse(str, out ushort value) ? value: default(ushort) },
            { Types.UshortOptional, str=>  ushort.TryParse(str, out ushort value) ? value: default(ushort?) },
            { Types.Int, str=>  int.TryParse(str, out int value) ? value: default(int) },
            { Types.IntOptional, str=>  int.TryParse(str, out int value) ? value: default(int?) },
            { Types.Uint, str=>  uint.TryParse(str, out uint value) ? value: default(uint) },
            { Types.UintOptional, str=>  uint.TryParse(str, out uint value) ? value: default(uint?) },
            { Types.Long, str=>  long.TryParse(str, out long value) ? value: default(long) },
            { Types.LongOptional, str=>  long.TryParse(str, out long value) ? value: default(long?) },
            { Types.Ulong, str=>  ulong.TryParse(str, out ulong value) ? value: default(ulong) },
            { Types.UlongOptional, str=>  ulong.TryParse(str, out ulong value) ? value: default(ulong?) },
            { Types.Float, str=>  float.TryParse(str, out float value) ? value: default(float) },
            { Types.FloatOptional, str=>  float.TryParse(str, out float value) ? value: default(float?) },
            { Types.Double, str=>  double.TryParse(str, out double value) ? value: default(double) },
            { Types.DoubleOptional, str=>  double.TryParse(str, out double value) ? value: default(double?) },
            { Types.Decimal, str=>  decimal.TryParse(str, out decimal value) ? value: default(decimal) },
            { Types.DecimalOptional, str=>  decimal.TryParse(str, out decimal value) ? value: default(decimal?) },
            { Types.Char, str=>  char.TryParse(str, out char value) ? value: default(char) },
            { Types.CharOptional, str=>  char.TryParse(str, out char value) ? value: default(char?) },
            { Types.Bool, str=>  bool.TryParse(str, out bool value) ? value: default(bool) },
            { Types.BoolOptional, str=>  bool.TryParse(str, out bool value) ? value: default(bool?) }
        };

        /// <summary>Changes the type.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private static T ChangeType<T>(object value)
        {
            if (typeof(T).IsPrimitive)
            {
                TypeConverter newTC = ResolveTypeConverter<T>();
                object convertedValue = newTC.ConvertFrom(value);
                return (T)convertedValue;
            }
            else if (value is string strValue)
            {
                try
                {
                    object convertedValue = StringConverters.TryGetValue(typeof(T), out Func<string, object> converter) ? converter(strValue) :
                    Convert.ChangeType(value, typeof(T));
                    return (T)convertedValue;
                }
                catch
                {
                    return default(T);
                }

            }

            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        /// <summary>Resolves the type converter.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///   <br />
        /// </returns>
        private static TypeConverter ResolveTypeConverter<T>()
        {
            if (Converters.TryGetValue(typeof(T), out TypeConverter tc))
            {
                return tc;
            }
            TypeConverter newTC = TypeDescriptor.GetConverter(typeof(T));
            Converters.TryAdd(typeof(T), newTC);
            return newTC;
        }
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
                else if (value is string strVal && int.TryParse(strVal, out int intVal) && IsEnum(castType, intVal))
                {
                    return (T)Enum.ToObject(castType, intVal);
                }
                return IsEnum(castType, value) && value is int intValue ?
                    (T)Enum.ToObject(castType, intValue) :
                    ChangeType<T>(value);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static IDictionary<string, object> ToDictionary(this DataRow dataRow)
        {
            ConcurrentDictionary<string, object> result = new ConcurrentDictionary<string, object>();
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                var value = dataRow[column.ColumnName] == DBNull.Value ? null : dataRow[column.ColumnName];
                result.TryAdd(column.ColumnName, value);
            }
            return result;
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string GetName(this IDictionary<string, object> data, string key)
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
        internal static (T value, string error) GetValue<T>(this IDictionary<string, object> data, string key, Func<string, T> convert, T defaultValue = default)
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
        internal static (T value, string error) GetValue<T, TParse>(this IDictionary<string, object> data, string key, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
        {
            TParse defaultParseValue = default(TParse);
            var result = data.GetValue(key, defaultParseValue);
            return result.value.CompareTo(defaultParseValue) == 0 ? (defaultValue, result.error) : (convert(result.value), result.error);
        }
        internal static (T value, string error) GetValue<T>(this IDictionary<string, object> data, string key, T defaultValue = default)
        {
            string name = data.GetName(key);
            if (name == null)
            {
                return (defaultValue, $"No data entry named '{key}' exists");
            }
            return (data.TryGetValue(name, out object value) &&
                value != null &&
                value != DBNull.Value ?
                Parse(value, defaultValue) : defaultValue, null);
        }

        /// <summary>
        /// Gets the date time value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        internal static (DateTime? value, string error) GetDateTimeValue(this IDictionary<string, object> data, string key, string format, DateTimeStyles style = DateTimeStyles.None)
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
        internal static (DateTime? value, string error) GetDateTimeValue(this IDictionary<string, object> data, string key, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
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
        internal static (string value, string error) GetFormattedDateTimeStringValue(this IDictionary<string, object> data, string key, string format)
        {
            var result = data.GetValue<DateTime?>(key);
            return result.value is DateTime convertedValue ? (convertedValue.ToString(format), result.error) : (null, result.error);
        }

        /// <summary>
        /// Gets the enumerable value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        internal static (IEnumerable<T> value, string error) GetEnumerableValue<T>(this IDictionary<string, object> data, string key, string delimeter = ",")
        => data.GetValue(key, value => value.Split(delimeter.ToArray()).Select(v => Parse<T>(v)));
        /// <summary>
        /// Gets the flag value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="trueValue">The true value.</param>
        /// <returns></returns>
        internal static (bool value, string error) GetFlagValue<T>(this IDictionary<string, object> data, string key, T trueValue)
            where T : IComparable
        {
            var result = data.GetValue<T>(key);
            return (result.value.CompareTo(trueValue) == 0, result.error);
        }


        /// <summary>
        /// Gets the value asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="convert">The convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        internal static async Task<(T value, string error)> GetValueAsync<T>(this IDictionary<string, object> data, string key, Func<string, T> convert, T defaultValue = default)
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
        internal static async Task<(T value, string error)> GetValueAsync<T, TParse>(this IDictionary<string, object> data, string key, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
        {
            (T value, string error) result = data.GetValue(key, convert, defaultValue);
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
        internal static async Task<(T value, string error)> GetValueAsync<T>(this IDictionary<string, object> data, string key, T defaultValue = default)
        {
            (T value, string error) result = data.GetValue(key, defaultValue);
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
        internal static async Task<(IEnumerable<T> value, string error)> GetEnumerableValueAsync<T>(this IDictionary<string, object> data, string key, string delimeter = ",")
        {
            (IEnumerable<T> value, string error) result = data.GetEnumerableValue<T>(key, delimeter);
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
        internal static async Task<(DateTime? value, string error)> GetDateTimeValueAsync(this IDictionary<string, object> data, string key, string format, DateTimeStyles style = DateTimeStyles.None)
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
        internal static async Task<(DateTime? value, string error)> GetDateTimeValueAsync(this IDictionary<string, object> data, string key, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
        {
            (DateTime? value, string error) result = data.GetDateTimeValue(key, format, provider, style);
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
        internal static async Task<(string value, string error)> GetFormattedDateTimeStringValueAsync(this IDictionary<string, object> data, string key, string format)
        {
            (string value, string error) result = data.GetFormattedDateTimeStringValue(key, format);
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
        internal static async Task<(bool value, string error)> GetFlagValueAsync<T>(this IDictionary<string, object> data, string key, T trueValue)
            where T : IComparable
        {
            (bool value, string error) result = data.GetFlagValue(key, trueValue);
            await Task.CompletedTask;
            return result;
        }
    }
}
