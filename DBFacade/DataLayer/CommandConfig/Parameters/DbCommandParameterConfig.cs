using System;
using System.Data;
using System.Web.UI.WebControls;
using DBFacade.DataLayer.Models;
using DBFacade.Factories;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary></summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal class DbCommandParameterConfig<TDbParams> : IInternalDbCommandParameterConfig<TDbParams>
        where TDbParams : IDbParamsModel
    {
        internal DbCommandParameterConfig()
        {
        }

        protected DbCommandParameterConfig(DbType dbType, bool isNullable = true)
        {
            DbType = dbType;
            IsNullable = isNullable;
        }

        public DbType DbType { get; protected set; }
        public bool IsNullable { get; }

        public virtual object Value(TDbParams model)
        {
            return null;
        }

        /*params Functions*/
        internal IDbCommandParameterConfig<TDbParams> Byte(DelegateHandler<TDbParams, byte> returnFunction)
        {
            return new DbCommandParameterGenericConfig<byte>(DbType.Byte, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> SByte(DelegateHandler<TDbParams, sbyte> returnFunction)
        {
            return new DbCommandParameterGenericConfig<sbyte>(DbType.SByte, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Int16(DelegateHandler<TDbParams, short> returnFunction)
        {
            return new DbCommandParameterGenericConfig<short>(DbType.Int16, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Int32(DelegateHandler<TDbParams, int> returnFunction)
        {
            return new DbCommandParameterGenericConfig<int>(DbType.Int32, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Int64(DelegateHandler<TDbParams, long> returnFunction)
        {
            return new DbCommandParameterGenericConfig<long>(DbType.Int64, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt16(DelegateHandler<TDbParams, ushort> returnFunction)
        {
            return new DbCommandParameterGenericConfig<ushort>(DbType.UInt16, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt32(DelegateHandler<TDbParams, uint> returnFunction)
        {
            return new DbCommandParameterGenericConfig<uint>(DbType.UInt32, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt64(DelegateHandler<TDbParams, ulong> returnFunction)
        {
            return new DbCommandParameterGenericConfig<ulong>(DbType.UInt64, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Single(DelegateHandler<TDbParams, float> returnFunction)
        {
            return new DbCommandParameterGenericConfig<float>(DbType.Single, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Double(DelegateHandler<TDbParams, double> returnFunction)
        {
            return new DbCommandParameterGenericConfig<double>(DbType.Double, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Decimal(DelegateHandler<TDbParams, decimal> returnFunction)
        {
            return new DbCommandParameterGenericConfig<decimal>(DbType.Decimal, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Boolean(DelegateHandler<TDbParams, bool> returnFunction)
        {
            return new DbCommandParameterGenericConfig<bool>(DbType.Boolean, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Char(DelegateHandler<TDbParams, char> returnFunction)
        {
            return new DbCommandParameterGenericConfig<char>(DbType.StringFixedLength, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Guid(DelegateHandler<TDbParams, Guid> returnFunction)
        {
            return new DbCommandParameterGenericConfig<Guid>(DbType.Guid, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> TimeSpan(DelegateHandler<TDbParams, TimeSpan> returnFunction)
        {
            return new DbCommandParameterGenericConfig<TimeSpan>(DbType.Time, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTime(DelegateHandler<TDbParams, DateTime> returnFunction)
        {
            return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime> returnFunction)
        {
            return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime2, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTimeOffset(
            DelegateHandler<TDbParams, DateTimeOffset> returnFunction)
        {
            return new DbCommandParameterGenericConfig<DateTimeOffset>(DbType.DateTimeOffset, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Binary(DelegateHandler<TDbParams, byte[]> returnFunction)
        {
            return new DbCommandParameterGenericConfig<byte[]>(DbType.Binary, returnFunction, false);
        }

        internal IDbCommandParameterConfig<TDbParams> String(DelegateHandler<TDbParams, string> returnFunction,
            bool isNullable = true)
        {
            return new DbCommandParameterGenericConfig<string>(DbType.String, returnFunction, isNullable);
        }

        internal IDbCommandParameterConfig<TDbParams> CharArray(DelegateHandler<TDbParams, char[]> returnFunction,
            bool isNullable = true)
        {
            return new DbCommandParameterGenericConfig<char[]>(DbType.String, returnFunction, isNullable);
        }

        internal IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(
            DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        {
            return new DbCommandParameterGenericConfig<string>(DbType.AnsiStringFixedLength, returnFunction,
                isNullable);
        }

        internal IDbCommandParameterConfig<TDbParams> AnsiString(DelegateHandler<TDbParams, string> returnFunction,
            bool isNullable = true)
        {
            return new DbCommandParameterGenericConfig<string>(DbType.AnsiString, returnFunction, isNullable);
        }

        internal IDbCommandParameterConfig<TDbParams> Xml(DelegateHandler<TDbParams, Xml> returnFunction)
        {
            return new DbCommandParameterGenericConfig<Xml>(DbType.Xml, returnFunction, false);
        }

        /*Hard value set*/
        internal IDbCommandParameterConfig<TDbParams> Byte(byte value)
        {
            return new DbCommandParameterGenericConfig<byte>(DbType.Byte, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> SByte(sbyte value)
        {
            return new DbCommandParameterGenericConfig<sbyte>(DbType.SByte, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Int16(short value)
        {
            return new DbCommandParameterGenericConfig<short>(DbType.Int16, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Int32(int value)
        {
            return new DbCommandParameterGenericConfig<int>(DbType.Int32, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Int64(long value)
        {
            return new DbCommandParameterGenericConfig<long>(DbType.Int64, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt16(ushort value)
        {
            return new DbCommandParameterGenericConfig<ushort>(DbType.UInt16, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt32(uint value)
        {
            return new DbCommandParameterGenericConfig<uint>(DbType.UInt32, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt64(ulong value)
        {
            return new DbCommandParameterGenericConfig<ulong>(DbType.UInt64, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Single(float value)
        {
            return new DbCommandParameterGenericConfig<float>(DbType.Single, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Double(double value)
        {
            return new DbCommandParameterGenericConfig<double>(DbType.Double, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Decimal(decimal value)
        {
            return new DbCommandParameterGenericConfig<decimal>(DbType.Decimal, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Boolean(bool value)
        {
            return new DbCommandParameterGenericConfig<bool>(DbType.Boolean, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Guid(Guid value)
        {
            return new DbCommandParameterGenericConfig<Guid>(DbType.Guid, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> TimeSpan(TimeSpan value)
        {
            return new DbCommandParameterGenericConfig<TimeSpan>(DbType.Time, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTime(DateTime value)
        {
            return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTime2(DateTime value)
        {
            return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime2, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset value)
        {
            return new DbCommandParameterGenericConfig<DateTimeOffset>(DbType.DateTimeOffset, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Binary(byte[] value)
        {
            return new DbCommandParameterGenericConfig<byte[]>(DbType.Binary, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Char(char value)
        {
            return new DbCommandParameterGenericConfig<char>(DbType.StringFixedLength, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> String(string value)
        {
            return new DbCommandParameterGenericConfig<string>(DbType.String, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> CharArray(char[] value)
        {
            return new DbCommandParameterGenericConfig<char[]>(DbType.String, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(string value)
        {
            return new DbCommandParameterGenericConfig<string>(DbType.AnsiStringFixedLength, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> AnsiString(string value)
        {
            return new DbCommandParameterGenericConfig<string>(DbType.AnsiString, value, false);
        }

        internal IDbCommandParameterConfig<TDbParams> Xml(Xml value)
        {
            return new DbCommandParameterGenericConfig<Xml>(DbType.Xml, value, false);
        }


        /*Optional params Functions*/
        internal IDbCommandParameterConfig<TDbParams> Byte(DelegateHandler<TDbParams, byte?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<byte?>(DbType.Byte, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> SByte(DelegateHandler<TDbParams, sbyte?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<sbyte?>(DbType.SByte, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> Int16(DelegateHandler<TDbParams, short?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<short?>(DbType.Int16, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> Int32(DelegateHandler<TDbParams, int?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<int?>(DbType.Int32, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> Int64(DelegateHandler<TDbParams, long?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<long?>(DbType.Int64, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt16(DelegateHandler<TDbParams, ushort?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<ushort?>(DbType.UInt16, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt32(DelegateHandler<TDbParams, uint?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<uint?>(DbType.UInt32, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt64(DelegateHandler<TDbParams, ulong?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<ulong?>(DbType.UInt64, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> Single(DelegateHandler<TDbParams, float?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<float?>(DbType.Single, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> Double(DelegateHandler<TDbParams, double?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<double?>(DbType.Double, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> Decimal(DelegateHandler<TDbParams, decimal?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<decimal?>(DbType.Decimal, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> Boolean(DelegateHandler<TDbParams, bool?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<bool?>(DbType.Boolean, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> Char(DelegateHandler<TDbParams, char?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<char?>(DbType.StringFixedLength, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> Guid(DelegateHandler<TDbParams, Guid?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<Guid?>(DbType.Guid, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> TimeSpan(DelegateHandler<TDbParams, TimeSpan?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<TimeSpan?>(DbType.Time, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTime(DelegateHandler<TDbParams, DateTime?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<DateTime?>(DbType.DateTime, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<DateTime?>(DbType.DateTime2, returnFunction);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTimeOffset(
            DelegateHandler<TDbParams, DateTimeOffset?> returnFunction)
        {
            return new DbCommandParameterGenericConfig<DateTimeOffset?>(DbType.DateTimeOffset, returnFunction, false);
        }
        

        /*Optional Hard value set*/
        internal IDbCommandParameterConfig<TDbParams> Byte(byte? value)
        {
            return new DbCommandParameterGenericConfig<byte?>(DbType.Byte, value);
        }

        internal IDbCommandParameterConfig<TDbParams> SByte(sbyte? value)
        {
            return new DbCommandParameterGenericConfig<sbyte?>(DbType.SByte, value);
        }

        internal IDbCommandParameterConfig<TDbParams> Int16(short? value)
        {
            return new DbCommandParameterGenericConfig<short?>(DbType.Int16, value);
        }

        internal IDbCommandParameterConfig<TDbParams> Int32(int? value)
        {
            return new DbCommandParameterGenericConfig<int?>(DbType.Int32, value);
        }

        internal IDbCommandParameterConfig<TDbParams> Int64(long? value)
        {
            return new DbCommandParameterGenericConfig<long?>(DbType.Int64, value);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt16(ushort? value)
        {
            return new DbCommandParameterGenericConfig<ushort?>(DbType.UInt16, value);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt32(uint? value)
        {
            return new DbCommandParameterGenericConfig<uint?>(DbType.UInt32, value);
        }

        internal IDbCommandParameterConfig<TDbParams> UInt64(ulong? value)
        {
            return new DbCommandParameterGenericConfig<ulong?>(DbType.UInt64, value);
        }

        internal IDbCommandParameterConfig<TDbParams> Single(float? value)
        {
            return new DbCommandParameterGenericConfig<float?>(DbType.Single, value);
        }

        internal IDbCommandParameterConfig<TDbParams> Double(double? value)
        {
            return new DbCommandParameterGenericConfig<double?>(DbType.Double, value);
        }

        internal IDbCommandParameterConfig<TDbParams> Decimal(decimal? value)
        {
            return new DbCommandParameterGenericConfig<decimal?>(DbType.Decimal, value);
        }

        internal IDbCommandParameterConfig<TDbParams> Boolean(bool? value)
        {
            return new DbCommandParameterGenericConfig<bool?>(DbType.Boolean, value);
        }

        internal IDbCommandParameterConfig<TDbParams> StringFixedLength(char? value)
        {
            return new DbCommandParameterGenericConfig<char?>(DbType.StringFixedLength, value);
        }

        internal IDbCommandParameterConfig<TDbParams> Guid(Guid? value)
        {
            return new DbCommandParameterGenericConfig<Guid?>(DbType.Guid, value);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTime(DateTime? value)
        {
            return new DbCommandParameterGenericConfig<DateTime?>(DbType.DateTime, value);
        }

        internal IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset? value)
        {
            return new DbCommandParameterGenericConfig<DateTimeOffset?>(DbType.DateTimeOffset, value);
        }
        

        private class DbCommandParameterGenericConfig<N> : DbCommandParameterConfig<TDbParams>
        {
            public DbCommandParameterGenericConfig(DbType dbType, DelegateHandler<TDbParams, N> returnFunction,
                bool isNullable = true) : base(dbType, isNullable)
            {
                ReturnFunction = returnFunction;
            }

            public DbCommandParameterGenericConfig(DbType dbType, N value, bool isNullable = true) : this(dbType,
                model => value, isNullable)
            {
            }

            private DelegateHandler<TDbParams, N> ReturnFunction { get; }

            public sealed override object Value(TDbParams model)
            {
                return ReturnFunction(model);
            }
        }
    }
}