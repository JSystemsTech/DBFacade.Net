using DbFacade.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;

namespace DbFacade.Extensions
{
    internal static partial class DbDataReaderExtensions
    {
        private const char DefaultDelimiter = ',';
        private delegate object BasicDataConverter(int ordinal);

        private static Type IEnumerableType = typeof(IEnumerable<>);
        private static bool IsIEnumerable(this Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == IEnumerableType;
        private static Type GetIEnumerableElementType(this Type type)
        {
            if (type.IsArray)
                return type.GetElementType();

            // type is IEnumerable<T>;
            if (type.IsIEnumerable())
            {
                return type.GetGenericArguments().FirstOrDefault();
            }

            // type implements/extends IEnumerable<T>;
            var enumType = type.GetInterfaces()
                                    .Where(t => t.IsGenericType &&
                                           t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                                    .Select(t => t.GenericTypeArguments.FirstOrDefault()).FirstOrDefault();
            return enumType ?? type;
        }

        private static object GetEnum(this IDataRecord data, int ordinal, Type t) 
            => data.GetInt64(ordinal) is long enumValue
               && Enum.IsDefined(t, enumValue) ? Enum.ToObject(t, enumValue): GenericInstance.GetInstance(t);
        private static object GetMailAddress(this IDataRecord data, int ordinal)
            => new MailAddress(data.GetString(ordinal));
        private static object GetTimeSpan(this IDataRecord data, int ordinal)
            => data.GetDateTime(ordinal).TimeOfDay;


        private static List<T> ConvertEnumerable<T>(this List<string> data)
        => data.ConvertAll(i => Converter<T>.FromString(i));
        private static object ConvertEnumerable(this List<string> data, Type t)
        {
            if (t == typeof(byte))
            {
                return data.ConvertEnumerable<byte>();
            }
            else if (t == typeof(byte?))
            {
                return data.ConvertEnumerable<byte?>();
            }
            if (t == typeof(sbyte))
            {
                return data.ConvertEnumerable<sbyte>();
            }
            else if (t == typeof(sbyte?))
            {
                return data.ConvertEnumerable<sbyte?>();
            }
            if (t == typeof(short))
            {
                return data.ConvertEnumerable<short>();
            }
            else if (t == typeof(short?))
            {
                return data.ConvertEnumerable<short?>();
            }
            else if (t == typeof(int))
            {
                return data.ConvertEnumerable<int>();
            }
            else if (t == typeof(int?))
            {
                return data.ConvertEnumerable<int?>();
            }
            else if (t == typeof(long))
            {
                return data.ConvertEnumerable<long>();
            }
            else if (t == typeof(long?))
            {
                return data.ConvertEnumerable<long?>();
            }
            else if (t == typeof(ushort))
            {
                return data.ConvertEnumerable<ushort>();
            }
            else if (t == typeof(ushort?))
            {
                return data.ConvertEnumerable<ushort?>();
            }
            else if (t == typeof(uint))
            {
                return data.ConvertEnumerable<uint>();
            }
            else if (t == typeof(uint?))
            {
                return data.ConvertEnumerable<uint?>();
            }
            else if (t == typeof(ulong))
            {
                return data.ConvertEnumerable<ulong>();
            }
            else if (t == typeof(ulong?))
            {
                return data.ConvertEnumerable<ulong?>();
            }
            else if (t == typeof(float))
            {
                return data.ConvertEnumerable<float>();
            }
            else if (t == typeof(float?))
            {
                return data.ConvertEnumerable<float?>();
            }
            else if (t == typeof(double))
            {
                return data.ConvertEnumerable<double>();
            }
            else if (t == typeof(double?))
            {
                return data.ConvertEnumerable<double?>();
            }
            else if (t == typeof(decimal))
            {
                return data.ConvertEnumerable<decimal>();
            }
            else if (t == typeof(decimal?))
            {
                return data.ConvertEnumerable<decimal?>();
            }
            else if (t == typeof(bool))
            {
                return data.ConvertEnumerable<bool>();
            }
            else if (t == typeof(bool?))
            {
                return data.ConvertEnumerable<bool?>();
            }
            else if (t == typeof(char))
            {
                return data.ConvertEnumerable<char>();
            }
            else if (t == typeof(char?))
            {
                return data.ConvertEnumerable<char?>();
            }
            else if (t == typeof(Guid))
            {
                return data.ConvertEnumerable<Guid>();
            }
            else if (t == typeof(Guid?))
            {
                return data.ConvertEnumerable<Guid?>();
            }
            else if (t == typeof(TimeSpan))
            {
                return data.ConvertEnumerable<TimeSpan>();
            }
            else if (t == typeof(TimeSpan?))
            {
                return data.ConvertEnumerable<TimeSpan?>();
            }
            else if (t == typeof(DateTime))
            {
                return data.ConvertEnumerable<DateTime>();
            }
            else if (t == typeof(DateTime?))
            {
                return data.ConvertEnumerable<DateTime?>();
            }
            else if (t == typeof(MailAddress))
            {
                return data.ConvertEnumerable<MailAddress>();
            }
            else
            {
                return data;
            }
        }
        private static object GetIEnumerable(this IDataRecord data, int ordinal, Type t, char delimiter = DefaultDelimiter)
        {
                List<string> dataAsIEnumerable = data.GetString(ordinal).Split(delimiter).ToList();
                Type elementType = t.GetIEnumerableElementType();
                if (elementType == typeof(string))               {
                   
                    return dataAsIEnumerable;
                }
                return dataAsIEnumerable.ConvertEnumerable(elementType);
        }
        private static object GetByteArray(this IDataRecord data, int ordinal, int bufferSize)
        {
            var output = new byte[bufferSize];
            long startIndex = 0;

            var retVal = data.GetBytes(ordinal, startIndex, output, 0, bufferSize);
            while (retVal == bufferSize)
            {
                startIndex += bufferSize;
                retVal = data.GetBytes(ordinal, startIndex, output, 0, bufferSize);
            }

            return output;
        }

        private static object GetCharArray(this IDataRecord data, int ordinal, int bufferSize)
        {
            var output = new char[bufferSize];
            long startIndex = 0;

            var retVal = data.GetChars(ordinal, startIndex, output, 0, bufferSize);
            while (retVal == bufferSize)
            {
                startIndex += bufferSize;
                retVal = data.GetChars(ordinal, startIndex, output, 0, bufferSize);
            }

            return output;
        }
        private static object GetGenericValue(this IDataRecord data, int ordinal, Type t)
        => t.ParseTo(data.GetString(ordinal));


        private static object GetColumnValue(this IDataRecord data, int ordinal, Type t, int bufferSize,
            char delimiter = DefaultDelimiter)
        {
            return t.IsEnum ? data.GetEnum(ordinal, t) :
                   t == typeof(byte[]) ? data.GetByteArray(ordinal, bufferSize) :
                   t == typeof(char[]) ? data.GetCharArray(ordinal, bufferSize) :                   

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
                   t == typeof(TimeSpan) || t == typeof(TimeSpan?) ? data.GetTimeSpan(ordinal) :
                   t == typeof(DateTime) || t == typeof(DateTime?) ? data.GetDateTime(ordinal) :
                   t == typeof(MailAddress) ? data.GetMailAddress(ordinal):

                   t.IsIEnumerable() ? data.GetIEnumerable(ordinal, t, delimiter) :
                   data.GetGenericValue(ordinal, t);
        }
        

        public static object GetColumn(this IDataRecord data, int ordinal, Type t, int bufferSize,
            char delimiter = DefaultDelimiter, object defaultValue = null)
        => ordinal >= 0 && data.IsDBNull(ordinal) ? (defaultValue != null ? defaultValue: GenericInstance.GetInstance(t)) : 
            data.GetColumnValue(ordinal,t, bufferSize, delimiter);
        public static object GetColumn(this IDataRecord data, string columnName, Type t, int bufferSize,
            char delimiter = DefaultDelimiter, object defaultValue = null)
        => data.GetColumn(data.GetOrdinal(columnName), t, bufferSize, delimiter, defaultValue);


    }
}
