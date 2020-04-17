using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using DBFacade.Exceptions;

namespace DBFacade.DataLayer.Models
{
    public sealed class DbColumnConversion
    {
        private const char DefaultDelimiter = ',';


        private static readonly Dictionary<Type, Converter> Converters = new Dictionary<Type, Converter>
        {
            {typeof(bool), AsBoolean},
            {typeof(byte), AsByte},
            {typeof(char), AsChar},
            {typeof(DateTime), AsDateTime},
            {typeof(TimeSpan), AsTimeSpan},
            {typeof(decimal), AsDecimal},
            {typeof(double), AsDouble},
            {typeof(float), AsFloat},
            {typeof(Guid), AsGuid},
            {typeof(short), AsShort},
            {typeof(int), AsInt},
            {typeof(long), AsLong},
            {typeof(string), AsString},
            {typeof(ushort), AsUShort},
            {typeof(uint), AsUInt},
            {typeof(ulong), AsULong},
            {typeof(object), AsObject},
            {typeof(MailAddress), AsEmail},

            {typeof(bool?), AsBoolean},
            {typeof(byte?), AsByte},
            {typeof(char?), AsChar},
            {typeof(DateTime?), AsDateTime},
            {typeof(TimeSpan?), AsTimeSpan},
            {typeof(decimal?), AsDecimal},
            {typeof(double?), AsDouble},
            {typeof(float?), AsFloat},
            {typeof(Guid?), AsGuid},
            {typeof(short?), AsShort},
            {typeof(int?), AsInt},
            {typeof(long?), AsLong},
            {typeof(ushort?), AsUShort},
            {typeof(uint?), AsUInt},
            {typeof(ulong?), AsULong}
        };

        private static readonly Dictionary<Type, EnumerableConverter> EnumerableConverters =
            new Dictionary<Type, EnumerableConverter>
            {
                {typeof(IEnumerable<string>), AsIEnumerableString},
                {typeof(IEnumerable<short>), AsIEnumerableShort},
                {typeof(IEnumerable<int>), AsIEnumerableInt},
                {typeof(IEnumerable<long>), AsIEnumerableLong},
                {typeof(IEnumerable<float>), AsIEnumerableFloat},
                {typeof(IEnumerable<double>), AsIEnumerableDouble},
                {typeof(IEnumerable<decimal>), AsIEnumerableDecimal},
                {typeof(IEnumerable<MailAddress>), AsIEnumerableEmail},
                {typeof(IEnumerable<Guid>), AsIEnumerableGuid}
            };

        private static readonly Dictionary<Type, BufferedConverter> BufferedConverters =
            new Dictionary<Type, BufferedConverter>
            {
                {typeof(byte[]), AsByteArray},
                {typeof(char[]), AsCharArray}
            };

        private static object AsEmail(IDataRecord data, int columnOrdinal)
        {
            return new MailAddress(AsString(data, columnOrdinal).ToString());
        }

        private static object AsBoolean(IDataRecord data, int columnOrdinal)
        {
            return data.GetBoolean(columnOrdinal);
        }

        private static object AsByte(IDataRecord data, int columnOrdinal)
        {
            return data.GetByte(columnOrdinal);
        }

        private static object AsChar(IDataRecord data, int columnOrdinal)
        {
            return GetChar(data.GetString(columnOrdinal));
        }

        private static char GetChar(string str)
        {
            return string.IsNullOrEmpty(str) ? default(char) : str.FirstOrDefault();
        }

        private static object AsDateTime(IDataRecord data, int columnOrdinal)
        {
            return data.GetDateTime(columnOrdinal);
        }

        private static object AsTimeSpan(IDataRecord data, int columnOrdinal)
        {
            return data.GetDateTime(columnOrdinal).TimeOfDay;
        }

        private static object AsDecimal(IDataRecord data, int columnOrdinal)
        {
            return data.GetDecimal(columnOrdinal);
        }

        private static object AsDouble(IDataRecord data, int columnOrdinal)
        {
            return data.GetDouble(columnOrdinal);
        }

        private static object AsFloat(IDataRecord data, int columnOrdinal)
        {
            return data.GetFloat(columnOrdinal);
        }

        private static object AsGuid(IDataRecord data, int columnOrdinal)
        {
            return data.GetGuid(columnOrdinal);
        }

        private static object AsShort(IDataRecord data, int columnOrdinal)
        {
            return data.GetInt16(columnOrdinal);
        }

        private static object AsInt(IDataRecord data, int columnOrdinal)
        {
            return data.GetInt32(columnOrdinal);
        }

        private static object AsLong(IDataRecord data, int columnOrdinal)
        {
            return data.GetInt64(columnOrdinal);
        }

        private static object AsString(IDataRecord data, int columnOrdinal)
        {
            return data.GetString(columnOrdinal);
        }

        private static object AsUShort(IDataRecord data, int columnOrdinal)
        {
            return (ushort) AsShort(data, columnOrdinal);
        }

        private static object AsUInt(IDataRecord data, int columnOrdinal)
        {
            return (uint) AsInt(data, columnOrdinal);
        }

        private static object AsULong(IDataRecord data, int columnOrdinal)
        {
            return (ulong) AsLong(data, columnOrdinal);
        }

        private static object AsObject(IDataRecord data, int columnOrdinal)
        {
            return data.GetValue(columnOrdinal);
        }

        private static IEnumerable<string> ToIEnumerableString(IDataRecord data, int columnOrdinal,
            char delimiter = DefaultDelimiter)
        {
            return AsString(data, columnOrdinal).ToString().Split(delimiter).ToArray();
        }

        private static object AsIEnumerableString(IDataRecord data, int columnOrdinal,
            char delimiter = DefaultDelimiter)
        {
            return ToIEnumerableString(data, columnOrdinal, delimiter);
        }

        private static object AsIEnumerableShort(IDataRecord data, int columnOrdinal, char delimiter = DefaultDelimiter)
        {
            return ToIEnumerableString(data, columnOrdinal, delimiter).Select(value => short.Parse(value.Trim()))
                .ToArray();
        }

        private static object AsIEnumerableInt(IDataRecord data, int columnOrdinal, char delimiter = DefaultDelimiter)
        {
            return ToIEnumerableString(data, columnOrdinal, delimiter).Select(value => int.Parse(value.Trim()))
                .ToArray();
        }

        private static object AsIEnumerableLong(IDataRecord data, int columnOrdinal, char delimiter = DefaultDelimiter)
        {
            return ToIEnumerableString(data, columnOrdinal, delimiter).Select(value => long.Parse(value.Trim()))
                .ToArray();
        }

        private static object AsIEnumerableFloat(IDataRecord data, int columnOrdinal, char delimiter = DefaultDelimiter)
        {
            return ToIEnumerableString(data, columnOrdinal, delimiter).Select(value => float.Parse(value.Trim()))
                .ToArray();
        }

        private static object AsIEnumerableDouble(IDataRecord data, int columnOrdinal,
            char delimiter = DefaultDelimiter)
        {
            return ToIEnumerableString(data, columnOrdinal, delimiter).Select(value => double.Parse(value.Trim()))
                .ToArray();
        }

        private static object AsIEnumerableDecimal(IDataRecord data, int columnOrdinal,
            char delimiter = DefaultDelimiter)
        {
            return ToIEnumerableString(data, columnOrdinal, delimiter).Select(value => decimal.Parse(value.Trim()))
                .ToArray();
        }

        private static object AsIEnumerableGuid(IDataRecord data, int columnOrdinal, char delimiter = DefaultDelimiter)
        {
            return ToIEnumerableString(data, columnOrdinal, delimiter).Select(value => Guid.Parse(value.Trim()))
                .ToArray();
        }

        private static object AsByteArray(IDataRecord data, int columnOrdinal, int bufferSize)
        {
            var output = new byte[bufferSize];
            long startIndex = 0;

            var retVal = data.GetBytes(columnOrdinal, startIndex, output, 0, bufferSize);
            while (retVal == bufferSize)
            {
                startIndex += bufferSize;
                retVal = data.GetBytes(columnOrdinal, startIndex, output, 0, bufferSize);
            }

            return output;
        }

        private static object AsCharArray(IDataRecord data, int columnOrdinal, int bufferSize)
        {
            var output = new char[bufferSize];
            long startIndex = 0;

            var retVal = data.GetChars(columnOrdinal, startIndex, output, 0, bufferSize);
            while (retVal == bufferSize)
            {
                startIndex += bufferSize;
                retVal = data.GetChars(columnOrdinal, startIndex, output, 0, bufferSize);
            }

            return output;
        }

        private static object AsIEnumerableEmail(IDataRecord data, int columnOrdinal, char delimiter = DefaultDelimiter)
        {
            return ToIEnumerableString(data, columnOrdinal, delimiter).Select(value => new MailAddress(value.Trim()))
                .ToArray();
        }

        public static object Convert(Type type, IDataRecord data, int columnOrdinal, int bufferSize,
            char delimiter = DefaultDelimiter, object defaultValue = null)
        {
            
                return EnumerableConverters.ContainsKey(type)
                    ? EnumerableConverters[type](data, columnOrdinal, delimiter)
                    : BufferedConverters.ContainsKey(type)
                        ? BufferedConverters[type](data, columnOrdinal, bufferSize)
                        : Converters.ContainsKey(type)
                            ? Converters[type](data, columnOrdinal)
                            : defaultValue;
            
            
            
        }

        private delegate object Converter(IDataRecord data, int columnOrdinal);

        private delegate object EnumerableConverter(IDataRecord data, int columnOrdinal, char delimiter = ',');

        private delegate object BufferedConverter(IDataRecord data, int columnOrdinal, int bufferSize);
    }
}