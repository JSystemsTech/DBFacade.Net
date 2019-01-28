using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DomainFacade.DataLayer.Models
{
    public sealed class DbColumnConversion
    {
        private static Func<IDataRecord, int, bool> AsBoolean = (data, columnOrdinal) => data.GetBoolean(columnOrdinal);
        private static Func<IDataRecord, int, byte> AsByte = (data, columnOrdinal) => data.GetByte(columnOrdinal);
        private static Func<IDataRecord, int, byte[]> AsByteArray = (data, columnOrdinal) => (byte[])AsObject(data, columnOrdinal);
        private static Func<IDataRecord, int, char> AsChar = (data, columnOrdinal) => GetChar(data.GetString(columnOrdinal));
        
        /*IDataRecord.GetChar not implemented by DataReader classes*/
        private static char GetChar(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return default(char);
            }
            return str.FirstOrDefault();
        }
        private static Func<IDataRecord, int, DateTime> AsDateTime = (data, columnOrdinal) => data.GetDateTime(columnOrdinal);
        private static Func<IDataRecord, int, decimal> AsDecimal = (data, columnOrdinal) => data.GetDecimal(columnOrdinal);
        private static Func<IDataRecord, int, double> AsDouble = (data, columnOrdinal) => data.GetDouble(columnOrdinal);
        private static Func<IDataRecord, int, float> AsFloat = (data, columnOrdinal) => data.GetFloat(columnOrdinal);
        private static Func<IDataRecord, int, Guid> AsGuid = (data, columnOrdinal) => data.GetGuid(columnOrdinal);
        private static Func<IDataRecord, int, short> AsShort = (data, columnOrdinal) => data.GetInt16(columnOrdinal);
        private static Func<IDataRecord, int, int> AsInt = (data, columnOrdinal) => data.GetInt32(columnOrdinal);
        private static Func<IDataRecord, int, long> AsLong = (data, columnOrdinal) => data.GetInt64(columnOrdinal);
        private static Func<IDataRecord, int, string> AsString = (data, columnOrdinal) => data.GetString(columnOrdinal);        
        private static Func<IDataRecord, int, ushort> AsUShort = (data, columnOrdinal) => (ushort)AsShort(data, columnOrdinal);
        private static Func<IDataRecord, int, uint> AsUInt = (data, columnOrdinal) => (uint)AsInt(data, columnOrdinal);
        private static Func<IDataRecord, int, ulong> AsULong = (data, columnOrdinal) => (ulong)AsLong(data, columnOrdinal);
        private static Func<IDataRecord, int, object> AsObject = (data, columnOrdinal) => data.GetValue(columnOrdinal);


        private static Func<IDataRecord, int, char, string[]> AsStringArray = (data, columnOrdinal, delimeter) => AsString(data, columnOrdinal).Split(delimeter);
        
        public static bool isDelimitedArray(Type type)
        {
            return type == typeof(string[]);
        }
        public static bool HasConverter(Type type)
        {
            return Converters.ContainsKey(type);
        }
        public static Dictionary<Type, Delegate> Converters = new Dictionary<Type, Delegate>() {
            { typeof(bool), AsBoolean },
            { typeof(byte), AsByte },
            { typeof(byte[]), AsByteArray },
            { typeof(char), AsChar },
            { typeof(DateTime), AsDateTime },
            { typeof(decimal), AsDecimal },
            { typeof(double), AsDouble },
            { typeof(float), AsFloat },
            { typeof(Guid), AsGuid },
            { typeof(short), AsShort },
            { typeof(int), AsInt },
            { typeof(long), AsLong },
            { typeof(string), AsString },
            { typeof(string[]), AsStringArray },
            { typeof(ushort), AsUShort },
            { typeof(uint), AsUInt },
            { typeof(ulong), AsULong },
            { typeof(object), AsObject }
        };
    }
}
