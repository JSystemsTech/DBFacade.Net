using DbFacade.Utils;
using DbFacadeShared.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DbFacadeShared.Extensions
{
    internal static partial class DbDataReaderExtensions
    {
        private static async Task<bool> IsIEnumerableAsync(this Type type)
        {
            bool value = type.IsGenericType && type.GetGenericTypeDefinition() == IEnumerableType;
            await Task.CompletedTask;
            return value;
        }
        private static async Task<Type> GetIEnumerableElementTypeAsync(this Type type)
        {
            Type t;
            if (type.IsArray)
            {
                t = type.GetElementType();
                await Task.CompletedTask;
                return t;
            }

            // type is IEnumerable<T>;
            if (type.IsIEnumerable())
            {
                t = type.GetGenericArguments().FirstOrDefault();
                await Task.CompletedTask;
                return t;
            }

            // type implements/extends IEnumerable<T>;
            var enumType = type.GetInterfaces()
                                    .Where(ty => ty.IsGenericType &&
                                           ty.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                    .Select(ty => ty.GenericTypeArguments.FirstOrDefault()).FirstOrDefault();
            t =  enumType ?? type;
            await Task.CompletedTask;
            return t;
        }

        private static async Task<object> GetEnumAsync(this IDataRecord data, int ordinal, Type t)
        {
            if(data.GetInt64(ordinal) is long enumValue && Enum.IsDefined(t, enumValue))
            {
                var value = Enum.ToObject(t, enumValue);
                await Task.CompletedTask;
                return value;
            }
            return await GenericInstance.GetInstanceAsync(t);
        }
        private static async Task<object> GetMailAddressAsync(this IDataRecord data, int ordinal)
        {
            string email = data.GetString(ordinal);
            MailAddress mailAddress = new MailAddress(email);
            await Task.CompletedTask;
            return mailAddress;
        }
        private static async Task<object> GetTimeSpanAsync(this IDataRecord data, int ordinal)
        {
            TimeSpan value = data.GetDateTime(ordinal).TimeOfDay;
            await Task.CompletedTask;
            return value;
        }


        private static async Task<List<T>> ConvertEnumerableAsync<T>(this List<string> data)
        {
            List<T> value = data.ConvertAll(i => Converter<T>.FromString(i));
            await Task.CompletedTask;
            return value;
        }
        private static async Task<object> ConvertEnumerableAsync(this List<string> data, Type t)
        {
            if (t == typeof(byte))
            {
                return await data.ConvertEnumerableAsync<byte>();
            }
            else if (t == typeof(byte?))
            {
                return await data.ConvertEnumerableAsync<byte?>();
            }
            if (t == typeof(sbyte))
            {
                return await data.ConvertEnumerableAsync<sbyte>();
            }
            else if (t == typeof(sbyte?))
            {
                return await data.ConvertEnumerableAsync<sbyte?>();
            }
            if (t == typeof(short))
            {
                return await data.ConvertEnumerableAsync<short>();
            }
            else if (t == typeof(short?))
            {
                return await data.ConvertEnumerableAsync<short?>();
            }
            else if (t == typeof(int))
            {
                return await data.ConvertEnumerableAsync<int>();
            }
            else if (t == typeof(int?))
            {
                return await data.ConvertEnumerableAsync<int?>();
            }
            else if (t == typeof(long))
            {
                return await data.ConvertEnumerableAsync<long>();
            }
            else if (t == typeof(long?))
            {
                return await data.ConvertEnumerableAsync<long?>();
            }
            else if (t == typeof(ushort))
            {
                return await data.ConvertEnumerableAsync<ushort>();
            }
            else if (t == typeof(ushort?))
            {
                return await data.ConvertEnumerableAsync<ushort?>();
            }
            else if (t == typeof(uint))
            {
                return await data.ConvertEnumerableAsync<uint>();
            }
            else if (t == typeof(uint?))
            {
                return await data.ConvertEnumerableAsync<uint?>();
            }
            else if (t == typeof(ulong))
            {
                return await data.ConvertEnumerableAsync<ulong>();
            }
            else if (t == typeof(ulong?))
            {
                return await data.ConvertEnumerableAsync<ulong?>();
            }
            else if (t == typeof(float))
            {
                return await data.ConvertEnumerableAsync<float>();
            }
            else if (t == typeof(float?))
            {
                return await data.ConvertEnumerableAsync<float?>();
            }
            else if (t == typeof(double))
            {
                return await data.ConvertEnumerableAsync<double>();
            }
            else if (t == typeof(double?))
            {
                return await data.ConvertEnumerableAsync<double?>();
            }
            else if (t == typeof(decimal))
            {
                return await data.ConvertEnumerableAsync<decimal>();
            }
            else if (t == typeof(decimal?))
            {
                return await data.ConvertEnumerableAsync<decimal?>();
            }
            else if (t == typeof(bool))
            {
                return await data.ConvertEnumerableAsync<bool>();
            }
            else if (t == typeof(bool?))
            {
                return await data.ConvertEnumerableAsync<bool?>();
            }
            else if (t == typeof(char))
            {
                return await data.ConvertEnumerableAsync<char>();
            }
            else if (t == typeof(char?))
            {
                return await data.ConvertEnumerableAsync<char?>();
            }
            else if (t == typeof(Guid))
            {
                return await data.ConvertEnumerableAsync<Guid>();
            }
            else if (t == typeof(Guid?))
            {
                return await data.ConvertEnumerableAsync<Guid?>();
            }
            else if (t == typeof(TimeSpan))
            {
                return await data.ConvertEnumerableAsync<TimeSpan>();
            }
            else if (t == typeof(TimeSpan?))
            {
                return await data.ConvertEnumerableAsync<TimeSpan?>();
            }
            else if (t == typeof(DateTime))
            {
                return await data.ConvertEnumerableAsync<DateTime>();
            }
            else if (t == typeof(DateTime?))
            {
                return await data.ConvertEnumerableAsync<DateTime?>();
            }
            else if (t == typeof(MailAddress))
            {
                return await data.ConvertEnumerableAsync<MailAddress>();
            }
            else
            {
                await Task.CompletedTask;
                return data;
            }
        }
        private static async Task<object> GetIEnumerableAsync(this IDataRecord data, int ordinal, Type t, char delimiter = DefaultDelimiter)
        {
            
                List<string> dataAsIEnumerable = data.GetString(ordinal).Split(delimiter).ToList();
                Type elementType = t.GetIEnumerableElementType();
                if (elementType == typeof(string))               
                {
                    await Task.CompletedTask;
                    return dataAsIEnumerable;
                }
            return await dataAsIEnumerable.ConvertEnumerableAsync(elementType);
            
        }
        private static async Task<object> GetByteArrayAsync(this IDataRecord data, int ordinal, int bufferSize)
        {
            var output = new byte[bufferSize];
            long startIndex = 0;

            var retVal = data.GetBytes(ordinal, startIndex, output, 0, bufferSize);
            while (retVal == bufferSize)
            {
                startIndex += bufferSize;
                retVal = data.GetBytes(ordinal, startIndex, output, 0, bufferSize);
            }
            await Task.CompletedTask;
            return output;
        }

        private static async Task<object> GetCharArrayAsync(this IDataRecord data, int ordinal, int bufferSize)
        {
            var output = new char[bufferSize];
            long startIndex = 0;

            var retVal = data.GetChars(ordinal, startIndex, output, 0, bufferSize);
            while (retVal == bufferSize)
            {
                startIndex += bufferSize;
                retVal = data.GetChars(ordinal, startIndex, output, 0, bufferSize);
            }
            await Task.CompletedTask;
            return output;
        }
        private static async Task<object> GetGenericValueAsync(this IDataRecord data, int ordinal, Type t)
        => await t.ParseToAsync(data.GetString(ordinal));


        private static async Task<object> GetColumnValueAsync(this IDataRecord data, int ordinal, Type t, int bufferSize,
            char delimiter = DefaultDelimiter)
        {
            object value = t.IsEnum ? await data.GetEnumAsync(ordinal, t) :
                   t == typeof(byte[]) ? await data.GetByteArrayAsync(ordinal, bufferSize) :
                   t == typeof(char[]) ? await data.GetCharArrayAsync(ordinal, bufferSize) :                   

                   t == typeof(byte) || t == typeof(byte?) ? data.GetByte(ordinal) :
                   t == typeof(sbyte) || t == typeof(sbyte?) ? data.GetByte(ordinal) :
                   t == typeof(short) || t == typeof(short?) ? data.GetInt16(ordinal) :
                   t == typeof(int) || t == typeof(int?) ? data.GetInt32(ordinal) :
                   t == typeof(long) || t == typeof(long?) ? data.GetInt64(ordinal) :
                   t == typeof(ushort) || t == typeof(ushort?) ? data.GetInt16(ordinal) :
                   t == typeof(uint) || t == typeof(uint?) ? data.GetInt32(ordinal) :
                   t == typeof(ulong) || t == typeof(ulong?) ? data.GetInt64(ordinal) :
                   t == typeof(float) || t == typeof(float?) ? data.GetFloat(ordinal) :
                   t == typeof(double) || t == typeof(double?) ? data.GetDouble(ordinal) :
                   t == typeof(decimal) || t == typeof(decimal?) ? data.GetDecimal(ordinal) :
                   t == typeof(string) ? data.GetString(ordinal) :
                   t == typeof(bool) || t == typeof(bool?) ? data.GetBoolean(ordinal) :
                   t == typeof(char) || t == typeof(char?) ? data.GetChar(ordinal) :
                   t == typeof(Guid) || t == typeof(Guid?) ? data.GetGuid(ordinal) :
                   t == typeof(TimeSpan) || t == typeof(TimeSpan?) ? await data.GetTimeSpanAsync(ordinal) :
                   t == typeof(DateTime) || t == typeof(DateTime?) ? data.GetDateTime(ordinal) :
                   t == typeof(MailAddress) ? await data.GetMailAddressAsync(ordinal):

                   t.IsIEnumerable() ? await data.GetIEnumerableAsync(ordinal, t, delimiter) :
                   await data.GetGenericValueAsync(ordinal, t);

            await Task.CompletedTask;
            return value;
        }
        

        public static async Task<object> GetColumnAsync(this IDataRecord data, int ordinal, Type t, int bufferSize,
            char delimiter = DefaultDelimiter, object defaultValue = null)
        {
            if(ordinal >= 0 && data.IsDBNull(ordinal))
            {
                await Task.CompletedTask;
                return defaultValue != null ? defaultValue : await GenericInstance.GetInstanceAsync(t);
            }
            return await data.GetColumnValueAsync(ordinal, t, bufferSize, delimiter);
        }
        public static async Task<object> GetColumnAsync(this IDataRecord data, string columnName, Type t, int bufferSize,
            char delimiter = DefaultDelimiter, object defaultValue = null)
        {
            int ordinal = data.GetOrdinal(columnName);
            return await data.GetColumnAsync(ordinal, t, bufferSize, delimiter, defaultValue);
        }

    }
}
