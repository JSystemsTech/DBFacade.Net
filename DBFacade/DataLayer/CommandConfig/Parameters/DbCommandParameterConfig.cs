using DBFacade.DataLayer.Models;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary></summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal class DbCommandParameterConfig<TDbParams> : IDbCommandParameterConfig<TDbParams> where TDbParams : IDbParamsModel
    {

        public DbType DbType { get; protected set; }
        public bool IsNullable { get; private set;}
        internal DbCommandParameterConfig() { }
        protected DbCommandParameterConfig(DbType dbType, bool isNullable = true)
        {
            DbType = dbType;
            IsNullable = isNullable;
        }
        public virtual object Value(TDbParams model) => null;        
        
        private class DbCommandParameterGenericConfig<N> : DbCommandParameterConfig<TDbParams>
        {
            private Func<TDbParams, N> ReturnFunction { get; set; }            
            public DbCommandParameterGenericConfig(DbType dbType, Func<TDbParams, N> returnFunction, bool isNullable = true) : base(dbType, isNullable)
            {
                ReturnFunction = returnFunction;
            }
            public DbCommandParameterGenericConfig(DbType dbType, N value, bool isNullable = true) : this(dbType, model => value, isNullable) { }       
            public override sealed object Value(TDbParams model) => ReturnFunction(model);
        }

        /*params Functions*/
        internal IDbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte> returnFunction) { return new DbCommandParameterGenericConfig<byte>(DbType.Byte, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte> returnFunction) { return new DbCommandParameterGenericConfig<sbyte>(DbType.SByte, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short> returnFunction) { return new DbCommandParameterGenericConfig<short>(DbType.Int16, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int> returnFunction) { return new DbCommandParameterGenericConfig<int>(DbType.Int32, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long> returnFunction) { return new DbCommandParameterGenericConfig<long>(DbType.Int64, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort> returnFunction) { return new DbCommandParameterGenericConfig<ushort>(DbType.UInt16, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint> returnFunction) { return new DbCommandParameterGenericConfig<uint>(DbType.UInt32, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong> returnFunction) { return new DbCommandParameterGenericConfig<ulong>(DbType.UInt64, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float> returnFunction) { return new DbCommandParameterGenericConfig<float>(DbType.Single, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double> returnFunction) { return new DbCommandParameterGenericConfig<double>(DbType.Double, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal> returnFunction) { return new DbCommandParameterGenericConfig<decimal>(DbType.Decimal, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool> returnFunction) { return new DbCommandParameterGenericConfig<bool>(DbType.Boolean, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Char(Func<TDbParams, char> returnFunction) { return new DbCommandParameterGenericConfig<char>(DbType.StringFixedLength, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid> returnFunction) { return new DbCommandParameterGenericConfig<Guid>(DbType.Guid, returnFunction, false); }        
        internal IDbCommandParameterConfig<TDbParams> TimeSpan(Func<TDbParams, TimeSpan> returnFunction) { return new DbCommandParameterGenericConfig<TimeSpan>(DbType.Time, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime> returnFunction) { return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> DateTime2(Func<TDbParams, DateTime> returnFunction) { return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime2, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset> returnFunction) { return new DbCommandParameterGenericConfig<DateTimeOffset>(DbType.DateTimeOffset, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte[]> returnFunction) { return new DbCommandParameterGenericConfig<byte[]>(DbType.Binary, returnFunction, false); }

        internal IDbCommandParameterConfig<TDbParams> String(Func<TDbParams, string> returnFunction, bool isNullable = true) { return new DbCommandParameterGenericConfig<string>(DbType.String, returnFunction, isNullable); }
        internal IDbCommandParameterConfig<TDbParams> CharArray(Func<TDbParams, char[]> returnFunction, bool isNullable = true) { return new DbCommandParameterGenericConfig<char[]>(DbType.String, returnFunction, isNullable); }
        internal IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(Func<TDbParams, string> returnFunction, bool isNullable = true) { return new DbCommandParameterGenericConfig<string>(DbType.AnsiStringFixedLength, returnFunction, isNullable); }
        internal IDbCommandParameterConfig<TDbParams> AnsiString(Func<TDbParams, string> returnFunction, bool isNullable = true) { return new DbCommandParameterGenericConfig<string>(DbType.AnsiString, returnFunction, isNullable); }
        internal IDbCommandParameterConfig<TDbParams> Xml(Func<TDbParams, Xml> returnFunction) { return new DbCommandParameterGenericConfig<Xml>(DbType.Xml, returnFunction, false); }
        /*Hard value set*/
        internal IDbCommandParameterConfig<TDbParams> Byte(byte value) { return new DbCommandParameterGenericConfig<byte>(DbType.Byte, value, false); }
        internal IDbCommandParameterConfig<TDbParams> SByte(sbyte value) { return new DbCommandParameterGenericConfig<sbyte>(DbType.SByte, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Int16(short value) { return new DbCommandParameterGenericConfig<short>(DbType.Int16, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Int32(int value) { return new DbCommandParameterGenericConfig<int>(DbType.Int32, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Int64(long value) { return new DbCommandParameterGenericConfig<long>(DbType.Int64, value, false); }
        internal IDbCommandParameterConfig<TDbParams> UInt16(ushort value) { return new DbCommandParameterGenericConfig<ushort>(DbType.UInt16, value, false); }
        internal IDbCommandParameterConfig<TDbParams> UInt32(uint value) { return new DbCommandParameterGenericConfig<uint>(DbType.UInt32, value, false); }
        internal IDbCommandParameterConfig<TDbParams> UInt64(ulong value) { return new DbCommandParameterGenericConfig<ulong>(DbType.UInt64, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Single(float value) { return new DbCommandParameterGenericConfig<float>(DbType.Single, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Double(double value) { return new DbCommandParameterGenericConfig<double>(DbType.Double, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Decimal(decimal value) { return new DbCommandParameterGenericConfig<decimal>(DbType.Decimal, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Boolean(bool value) { return new DbCommandParameterGenericConfig<bool>(DbType.Boolean, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Guid(Guid value) { return new DbCommandParameterGenericConfig<Guid>(DbType.Guid, value, false); }
        internal IDbCommandParameterConfig<TDbParams> TimeSpan(TimeSpan value) { return new DbCommandParameterGenericConfig<TimeSpan>(DbType.Time, value, false); }
        internal IDbCommandParameterConfig<TDbParams> DateTime(DateTime value) { return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime, value, false); }
        internal IDbCommandParameterConfig<TDbParams> DateTime2(DateTime value) { return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime2, value, false); }
        internal IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset value) { return new DbCommandParameterGenericConfig<DateTimeOffset>(DbType.DateTimeOffset, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Binary(byte[] value) { return new DbCommandParameterGenericConfig<byte[]>(DbType.Binary, value, false); }

        internal IDbCommandParameterConfig<TDbParams> Char(char value) { return new DbCommandParameterGenericConfig<char>(DbType.StringFixedLength, value, false); }
        internal IDbCommandParameterConfig<TDbParams> String(string value) { return new DbCommandParameterGenericConfig<string>(DbType.String, value, false); }
        internal IDbCommandParameterConfig<TDbParams> CharArray(char[] value) { return new DbCommandParameterGenericConfig<char[]>(DbType.String, value, false); }
        internal IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(string value) { return new DbCommandParameterGenericConfig<string>(DbType.AnsiStringFixedLength, value, false); }
        internal IDbCommandParameterConfig<TDbParams> AnsiString(string value) { return new DbCommandParameterGenericConfig<string>(DbType.AnsiString, value, false); }
        internal IDbCommandParameterConfig<TDbParams> Xml(Xml value) { return new DbCommandParameterGenericConfig<Xml>(DbType.Xml, value, false); }



        /*Optional params Functions*/
        internal IDbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte?> returnFunction) { return new DbCommandParameterGenericConfig<byte?>(DbType.Byte, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte?> returnFunction) { return new DbCommandParameterGenericConfig<sbyte?>(DbType.SByte, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short?> returnFunction) { return new DbCommandParameterGenericConfig<short?>(DbType.Int16, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int?> returnFunction) { return new DbCommandParameterGenericConfig<int?>(DbType.Int32, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long?> returnFunction) { return new DbCommandParameterGenericConfig<long?>(DbType.Int64, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort?> returnFunction) { return new DbCommandParameterGenericConfig<ushort?>(DbType.UInt16, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint?> returnFunction) { return new DbCommandParameterGenericConfig<uint?>(DbType.UInt32, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong?> returnFunction) { return new DbCommandParameterGenericConfig<ulong?>(DbType.UInt64, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float?> returnFunction) { return new DbCommandParameterGenericConfig<float?>(DbType.Single, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double?> returnFunction) { return new DbCommandParameterGenericConfig<double?>(DbType.Double, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal?> returnFunction) { return new DbCommandParameterGenericConfig<decimal?>(DbType.Decimal, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool?> returnFunction) { return new DbCommandParameterGenericConfig<bool?>(DbType.Boolean, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> Char(Func<TDbParams, char?> returnFunction) { return new DbCommandParameterGenericConfig<char?>(DbType.StringFixedLength, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid?> returnFunction) { return new DbCommandParameterGenericConfig<Guid?>(DbType.Guid, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> TimeSpan(Func<TDbParams, TimeSpan?> returnFunction) { return new DbCommandParameterGenericConfig<TimeSpan?>(DbType.Time, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime?> returnFunction) { return new DbCommandParameterGenericConfig<DateTime?>(DbType.DateTime, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> DateTime2(Func<TDbParams, DateTime?> returnFunction) { return new DbCommandParameterGenericConfig<DateTime?>(DbType.DateTime2, returnFunction); }
        internal IDbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset?> returnFunction) { return new DbCommandParameterGenericConfig<DateTimeOffset?>(DbType.DateTimeOffset, returnFunction, false); }
        internal IDbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte?[]> returnFunction) { return new DbCommandParameterGenericConfig<byte?[]>(DbType.Binary, returnFunction); }

        /*Optional Hard value set*/
        internal IDbCommandParameterConfig<TDbParams> Byte(byte? value) { return new DbCommandParameterGenericConfig<byte?>(DbType.Byte,value); }        
        internal IDbCommandParameterConfig<TDbParams> SByte(sbyte? value) { return new DbCommandParameterGenericConfig<sbyte?>(DbType.SByte, value); }        
        internal IDbCommandParameterConfig<TDbParams> Int16(short? value) { return new DbCommandParameterGenericConfig<short?>(DbType.Int16, value); }        
        internal IDbCommandParameterConfig<TDbParams> Int32(int? value) { return new DbCommandParameterGenericConfig<int?>(DbType.Int32, value); }        
        internal IDbCommandParameterConfig<TDbParams> Int64(long? value) { return new DbCommandParameterGenericConfig<long?>(DbType.Int64, value); }        
        internal IDbCommandParameterConfig<TDbParams> UInt16(ushort? value) { return new DbCommandParameterGenericConfig<ushort?>(DbType.UInt16, value); }        
        internal IDbCommandParameterConfig<TDbParams> UInt32(uint? value) { return new DbCommandParameterGenericConfig<uint?>(DbType.UInt32, value); }        
        internal IDbCommandParameterConfig<TDbParams> UInt64(ulong? value) { return new DbCommandParameterGenericConfig<ulong?>(DbType.UInt64, value); }        
        internal IDbCommandParameterConfig<TDbParams> Single(float? value) { return new DbCommandParameterGenericConfig<float?>(DbType.Single, value); }        
        internal IDbCommandParameterConfig<TDbParams> Double(double? value) { return new DbCommandParameterGenericConfig<double?>(DbType.Double, value); }        
        internal IDbCommandParameterConfig<TDbParams> Decimal(decimal? value) { return new DbCommandParameterGenericConfig<decimal?>(DbType.Decimal, value); }        
        internal IDbCommandParameterConfig<TDbParams> Boolean(bool? value) { return new DbCommandParameterGenericConfig<bool?>(DbType.Boolean, value); }        
        internal IDbCommandParameterConfig<TDbParams> StringFixedLength(char? value) { return new DbCommandParameterGenericConfig<char?>(DbType.StringFixedLength, value); }        
        internal IDbCommandParameterConfig<TDbParams> Guid(Guid? value) { return new DbCommandParameterGenericConfig<Guid?>(DbType.Guid, value); }        
        internal IDbCommandParameterConfig<TDbParams> DateTime(DateTime? value) { return new DbCommandParameterGenericConfig<DateTime?>(DbType.DateTime, value); }        
        internal IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset? value) { return new DbCommandParameterGenericConfig<DateTimeOffset?>(DbType.DateTimeOffset, value); }        
        internal IDbCommandParameterConfig<TDbParams> Binary(byte?[] value) { return new DbCommandParameterGenericConfig<byte?[]>(DbType.Binary, value); } 
    }
}
