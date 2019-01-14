using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DomainFacade.DataLayer.Models
{
    public abstract class DbColumnConversion
    {
        private static Func<IDataRecord, int, bool> AsBoolean = (data, columnOrdinal) => data.GetBoolean(columnOrdinal);
        private static Func<IDataRecord, int, byte> AsByte = (data, columnOrdinal) => data.GetByte(columnOrdinal);
        private static Func<IDataRecord, int, byte[]> AsByteArray = (data, columnOrdinal) => (byte[])AsObject(data, columnOrdinal);
        private static Func<IDataRecord, int, char> AsChar = (data, columnOrdinal) => data.GetChar(columnOrdinal);
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
        private static Func<IDataRecord, int, char, short[]> AsShortArray = (data, columnOrdinal, delimeter) => Array.ConvertAll(AsStringArray(data, columnOrdinal, delimeter), short.Parse);
        private static Func<IDataRecord, int, char, int[]> AsIntArray = (data, columnOrdinal, delimeter) => Array.ConvertAll(AsStringArray(data, columnOrdinal, delimeter), int.Parse);
        private static Func<IDataRecord, int, char, long[]> AsLongArray = (data, columnOrdinal, delimeter) => Array.ConvertAll(AsStringArray(data, columnOrdinal, delimeter), long.Parse);
        private static Func<IDataRecord, int, char, double[]> AsDoubleArray = (data, columnOrdinal, delimeter) => Array.ConvertAll(AsStringArray(data, columnOrdinal, delimeter), double.Parse);
        private static Func<IDataRecord, int, char, float[]> AsFloatArray = (data, columnOrdinal, delimeter) => Array.ConvertAll(AsStringArray(data, columnOrdinal, delimeter), float.Parse);
        private static Func<IDataRecord, int, char, decimal[]> AsDecimalArray = (data, columnOrdinal, delimeter) => Array.ConvertAll(AsStringArray(data, columnOrdinal, delimeter), decimal.Parse);

        private static Func<IDataRecord, int, char, List<string>> AsStringList = (data, columnOrdinal, delimeter) => AsStringArray(data, columnOrdinal, delimeter).ToList();


        public static bool isDelimitedArray(Type type)
        {
            return
                type == typeof(string[]) ||
                type == typeof(short[]) || 
                type == typeof(int[]) || 
                type == typeof(long[])|| 
                type == typeof(double[]) ||
                type == typeof(float[]) ||
                type == typeof(decimal[]);
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
            { typeof(object), AsObject },
            { typeof(short[]), AsShortArray },
            { typeof(int[]), AsIntArray },
            { typeof(long[]), AsLongArray },
            { typeof(double[]), AsDoubleArray },
            { typeof(float[]), AsFloatArray },
            { typeof(decimal[]), AsDecimalArray }
        };
        public static void RegisterConverter<T>(Func<IDataRecord, int, T> converter)
        {
            if (!Converters.ContainsKey(typeof(T)))
            {
                Converters.Add(typeof(T), converter);
            }
        }
    }
}
