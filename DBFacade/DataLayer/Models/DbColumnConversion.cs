using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;

namespace DBFacade.DataLayer.Models
{
    public sealed class DbColumnConversion
    {
        private delegate object Converter(IDataRecord data, int columnOrdinal);
        private delegate object IEnumerableConverter(IDataRecord data, int columnOrdinal, char delimeter = ',');
        private delegate object BufferedConverter(IDataRecord data, int columnOrdinal, int BufferSize);

        private static object AsEmail(IDataRecord data, int columnOrdinal) => new MailAddress(AsString(data, columnOrdinal).ToString());

        private static object AsBoolean(IDataRecord data, int columnOrdinal) => data.GetBoolean(columnOrdinal);
        private static object AsByte(IDataRecord data, int columnOrdinal) => data.GetByte(columnOrdinal);
        private static object AsChar(IDataRecord data, int columnOrdinal) => GetChar(data.GetString(columnOrdinal));        
        private static char GetChar(string str) 
            => string.IsNullOrEmpty(str) ? default(char) : str.FirstOrDefault();
        private static object AsDateTime(IDataRecord data, int columnOrdinal) => data.GetDateTime(columnOrdinal);
        private static object AsTimeSpan(IDataRecord data, int columnOrdinal) => data.GetDateTime(columnOrdinal).TimeOfDay;
        private static object AsDecimal(IDataRecord data, int columnOrdinal) => data.GetDecimal(columnOrdinal);
        private static object AsDouble(IDataRecord data, int columnOrdinal) => data.GetDouble(columnOrdinal);
        private static object AsFloat(IDataRecord data, int columnOrdinal) => data.GetFloat(columnOrdinal);
        private static object AsGuid(IDataRecord data, int columnOrdinal) => data.GetGuid(columnOrdinal);

        private static object AsShort(IDataRecord data, int columnOrdinal) => data.GetInt16(columnOrdinal);

        private static object AsInt(IDataRecord data, int columnOrdinal) => data.GetInt32(columnOrdinal);
        private static object AsLong(IDataRecord data, int columnOrdinal) => data.GetInt64(columnOrdinal);
        private static object AsString(IDataRecord data, int columnOrdinal) => data.GetString(columnOrdinal);
        private static object AsUShort(IDataRecord data, int columnOrdinal) => (ushort)AsShort(data, columnOrdinal);
        private static object AsUInt(IDataRecord data, int columnOrdinal) => (uint)AsInt(data, columnOrdinal);
        private static object AsULong(IDataRecord data, int columnOrdinal) => (ulong)AsLong(data, columnOrdinal);
        private static object AsObject(IDataRecord data, int columnOrdinal) => data.GetValue(columnOrdinal);

        private const char DefaultDelimeter = ',';
        private static IEnumerable<string> ToIEnumerableString(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => AsString(data, columnOrdinal).ToString().Split(delimeter).ToArray();

        private static object AsIEnumerableString(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => ToIEnumerableString(data, columnOrdinal, delimeter);
        private static object AsIEnumerableShort(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => ToIEnumerableString(data, columnOrdinal, delimeter).Select(value => short.Parse(value.Trim())).ToArray();
        private static object AsIEnumerableInt(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => ToIEnumerableString(data, columnOrdinal, delimeter).Select(value => int.Parse(value.Trim())).ToArray();
        private static object AsIEnumerableLong(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => ToIEnumerableString(data, columnOrdinal, delimeter).Select(value => long.Parse(value.Trim())).ToArray();
        private static object AsIEnumerableFloat(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => ToIEnumerableString(data, columnOrdinal, delimeter).Select(value => float.Parse(value.Trim())).ToArray();
        private static object AsIEnumerableDouble(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => ToIEnumerableString(data, columnOrdinal, delimeter).Select(value => double.Parse(value.Trim())).ToArray();
        private static object AsIEnumerableDecimal(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => ToIEnumerableString(data, columnOrdinal, delimeter).Select(value => decimal.Parse(value.Trim())).ToArray();
        private static object AsIEnumerableGuid(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => ToIEnumerableString(data, columnOrdinal, delimeter).Select(value => Guid.Parse(value.Trim())).ToArray();

        private static object AsByteArray(IDataRecord data, int columnOrdinal, int BufferSize)
        {
            byte[] output = new byte[BufferSize];
            long startIndex = 0;

            long retval = data.GetBytes(columnOrdinal, startIndex, output, 0, BufferSize);
            while (retval == BufferSize)
            {
                startIndex += BufferSize;
                retval = data.GetBytes(columnOrdinal, startIndex, output, 0, BufferSize);
            }
            return output;
        }
        private static object AsCharArray(IDataRecord data, int columnOrdinal, int BufferSize)
        {
            char[] output = new char[BufferSize];
            long startIndex = 0;

            long retval = data.GetChars(columnOrdinal, startIndex, output, 0, BufferSize);
            while (retval == BufferSize)
            {
                startIndex += BufferSize;
                retval = data.GetChars(columnOrdinal, startIndex, output, 0, BufferSize);
            }
            return output;
        }

        private static object AsIEnumerableEmail(IDataRecord data, int columnOrdinal, char delimeter = DefaultDelimeter) => ToIEnumerableString(data, columnOrdinal, delimeter).Select(value => new MailAddress(value.Trim())).ToArray();

        public static object Convert(Type type, IDataRecord data, int columnOrdinal, int BufferSize, char delimeter = DefaultDelimeter, object defaultValue = null)
        {
            return IEnumerableConverters.ContainsKey(type) ? 
                   IEnumerableConverters[type](data, columnOrdinal, delimeter) :
                   BufferedConverters.ContainsKey(type) ?
                   BufferedConverters[type](data, columnOrdinal, BufferSize) :
                    Converters.ContainsKey(type) ? 
                    Converters[type](data, columnOrdinal) : 
                        defaultValue;            
        }

        
        private static Dictionary<Type, Converter> Converters = new Dictionary<Type, Converter>() {
            { typeof(bool), AsBoolean },
            { typeof(byte), AsByte },
            { typeof(char), AsChar },
            { typeof(DateTime), AsDateTime },
            { typeof(TimeSpan), AsTimeSpan },
            { typeof(decimal), AsDecimal },
            { typeof(double), AsDouble },
            { typeof(float), AsFloat },
            { typeof(Guid), AsGuid },
            { typeof(short), AsShort },
            { typeof(int), AsInt },
            { typeof(long), AsLong },
            { typeof(string), AsString },
            { typeof(ushort), AsUShort },
            { typeof(uint), AsUInt },
            { typeof(ulong), AsULong },
            { typeof(object), AsObject },
            {typeof(MailAddress), AsEmail},

            { typeof(bool?), AsBoolean },
            { typeof(byte?), AsByte },
            { typeof(char?), AsChar },
            { typeof(DateTime?), AsDateTime },
            { typeof(TimeSpan?), AsTimeSpan },
            { typeof(decimal?), AsDecimal },
            { typeof(double?), AsDouble },
            { typeof(float?), AsFloat },
            { typeof(Guid?), AsGuid },
            { typeof(short?), AsShort },
            { typeof(int?), AsInt },
            { typeof(long?), AsLong },
            { typeof(ushort?), AsUShort },
            { typeof(uint?), AsUInt },
            { typeof(ulong?), AsULong }
        };
        private static Dictionary<Type, IEnumerableConverter> IEnumerableConverters = new Dictionary<Type, IEnumerableConverter>() {
            { typeof(IEnumerable<string>), AsIEnumerableString },
            { typeof(IEnumerable<short>), AsIEnumerableShort },
            { typeof(IEnumerable<int>), AsIEnumerableInt },
            { typeof(IEnumerable<long>), AsIEnumerableLong },
            { typeof(IEnumerable<float>), AsIEnumerableFloat },
            { typeof(IEnumerable<double>), AsIEnumerableDouble },
            { typeof(IEnumerable<decimal>), AsIEnumerableDecimal },
            { typeof(IEnumerable<MailAddress>), AsIEnumerableEmail },
            { typeof(IEnumerable<Guid>), AsIEnumerableGuid }
        };
        private static Dictionary<Type, BufferedConverter> BufferedConverters = new Dictionary<Type, BufferedConverter>() {
            { typeof(byte[]), AsByteArray },
            { typeof(char[]), AsCharArray }
        };
    }
}
