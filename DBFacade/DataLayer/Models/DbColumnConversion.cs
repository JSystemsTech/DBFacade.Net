using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbColumnConversion
    {
        /// <summary>
        /// As boolean
        /// </summary>
        private static Func<IDataRecord, int, bool> AsBoolean = (data, columnOrdinal) => data.GetBoolean(columnOrdinal);
        /// <summary>
        /// As byte
        /// </summary>
        private static Func<IDataRecord, int, byte> AsByte = (data, columnOrdinal) => data.GetByte(columnOrdinal);
        /// <summary>
        /// As byte array
        /// </summary>
        private static Func<IDataRecord, int, byte[]> AsByteArray = (data, columnOrdinal) => (byte[])AsObject(data, columnOrdinal);
        /// <summary>
        /// As character
        /// </summary>
        private static Func<IDataRecord, int, char> AsChar = (data, columnOrdinal) => GetChar(data.GetString(columnOrdinal));

        /*IDataRecord.GetChar not implemented by DataReader classes*/
        /// <summary>
        /// Gets the character.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        private static char GetChar(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return default(char);
            }
            return str.FirstOrDefault();
        }
        /// <summary>
        /// As date time
        /// </summary>
        private static Func<IDataRecord, int, DateTime> AsDateTime = (data, columnOrdinal) => data.GetDateTime(columnOrdinal);
        /// <summary>
        /// As decimal
        /// </summary>
        private static Func<IDataRecord, int, decimal> AsDecimal = (data, columnOrdinal) => data.GetDecimal(columnOrdinal);
        /// <summary>
        /// As double
        /// </summary>
        private static Func<IDataRecord, int, double> AsDouble = (data, columnOrdinal) => data.GetDouble(columnOrdinal);
        /// <summary>
        /// As float
        /// </summary>
        private static Func<IDataRecord, int, float> AsFloat = (data, columnOrdinal) => data.GetFloat(columnOrdinal);
        /// <summary>
        /// As unique identifier
        /// </summary>
        private static Func<IDataRecord, int, Guid> AsGuid = (data, columnOrdinal) => data.GetGuid(columnOrdinal);
        /// <summary>
        /// As short
        /// </summary>
        private static Func<IDataRecord, int, short> AsShort = (data, columnOrdinal) => data.GetInt16(columnOrdinal);
        /// <summary>
        /// As int
        /// </summary>
        private static Func<IDataRecord, int, int> AsInt = (data, columnOrdinal) => data.GetInt32(columnOrdinal);
        /// <summary>
        /// As long
        /// </summary>
        private static Func<IDataRecord, int, long> AsLong = (data, columnOrdinal) => data.GetInt64(columnOrdinal);
        /// <summary>
        /// As string
        /// </summary>
        private static Func<IDataRecord, int, string> AsString = (data, columnOrdinal) => data.GetString(columnOrdinal);
        /// <summary>
        /// As u short
        /// </summary>
        private static Func<IDataRecord, int, ushort> AsUShort = (data, columnOrdinal) => (ushort)AsShort(data, columnOrdinal);
        /// <summary>
        /// As u int
        /// </summary>
        private static Func<IDataRecord, int, uint> AsUInt = (data, columnOrdinal) => (uint)AsInt(data, columnOrdinal);
        /// <summary>
        /// As u long
        /// </summary>
        private static Func<IDataRecord, int, ulong> AsULong = (data, columnOrdinal) => (ulong)AsLong(data, columnOrdinal);
        /// <summary>
        /// As object
        /// </summary>
        private static Func<IDataRecord, int, object> AsObject = (data, columnOrdinal) => data.GetValue(columnOrdinal);


        /// <summary>
        /// As string array
        /// </summary>
        private static Func<IDataRecord, int, char, string[]> AsStringArray = (data, columnOrdinal, delimeter) => AsString(data, columnOrdinal).Split(delimeter);

        /// <summary>
        /// Determines whether [is delimited array] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [is delimited array] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool isDelimitedArray(Type type)
        {
            return type == typeof(string[]);
        }
        /// <summary>
        /// Determines whether the specified type has converter.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type has converter; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasConverter(Type type)
        {
            return Converters.ContainsKey(type);
        }
        /// <summary>
        /// The converters
        /// </summary>
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
