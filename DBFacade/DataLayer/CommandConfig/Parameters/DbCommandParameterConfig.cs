using DBFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary></summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public abstract class DbCommandParameterConfig<TDbParams> : DbCommandParameterConfigBase<TDbParams> where TDbParams : IDbParamsModel
    {
        
        protected DbCommandParameterConfig(DbType dbType) : base(dbType) { }
        
        public DbCommandParameterConfig<TDbParams> Required() { isNullable = false; return this; }
        
        private class DbCommandParameterGenericConfig<N> : DbCommandParameterConfig<TDbParams>
        {
            
            public Func<TDbParams, N> ReturnFunction { get; private set; }
            
            public DbCommandParameterGenericConfig(DbType dbType, Func<TDbParams, N> returnFunction) : base(dbType)
            {
                this.ReturnFunction = returnFunction;
            }
            
            public DbCommandParameterGenericConfig(DbType dbType, N value) : base(dbType)
            {
                Func<TDbParams, N> ret = model => value;
                ReturnFunction = ret;
            }
            
            public override object GetParam(TDbParams model)
            {
                return ReturnFunction(model);
            }
            
            private bool IsNullableType() => Nullable.GetUnderlyingType(typeof(N)) != null;
            
            public override bool IsNullable()
            {
                if (!isNullable)
                {
                    return isNullable;
                }
                return IsNullableType();
            }
        }

        public static DbCommandParameterConfig<TDbParams> GetConfig() => new DbCommandParameterGenericConfig<byte>(DbType.Byte, default(byte));

        /*params Functions*/
        public DbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte> returnFunction) { return new DbCommandParameterGenericConfig<byte>(DbType.Byte, returnFunction); }
        public DbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte> returnFunction) { return new DbCommandParameterGenericConfig<sbyte>(DbType.SByte, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short> returnFunction) { return new DbCommandParameterGenericConfig<short>(DbType.Int16, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int> returnFunction) { return new DbCommandParameterGenericConfig<int>(DbType.Int32, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long> returnFunction) { return new DbCommandParameterGenericConfig<long>(DbType.Int64, returnFunction); }
        public DbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort> returnFunction) { return new DbCommandParameterGenericConfig<ushort>(DbType.UInt16, returnFunction); }
        public DbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint> returnFunction) { return new DbCommandParameterGenericConfig<uint>(DbType.UInt32, returnFunction); }
        public DbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong> returnFunction) { return new DbCommandParameterGenericConfig<ulong>(DbType.UInt64, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float> returnFunction) { return new DbCommandParameterGenericConfig<float>(DbType.Single, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double> returnFunction) { return new DbCommandParameterGenericConfig<double>(DbType.Double, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal> returnFunction) { return new DbCommandParameterGenericConfig<decimal>(DbType.Decimal, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool> returnFunction) { return new DbCommandParameterGenericConfig<bool>(DbType.Boolean, returnFunction); }
        public DbCommandParameterConfig<TDbParams> StringFixedLength(Func<TDbParams, char> returnFunction) { return new DbCommandParameterGenericConfig<char>(DbType.StringFixedLength, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid> returnFunction) { return new DbCommandParameterGenericConfig<Guid>(DbType.Guid, returnFunction); }
        public DbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime> returnFunction) { return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime, returnFunction); }
        public DbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset> returnFunction) { return new DbCommandParameterGenericConfig<DateTimeOffset>(DbType.DateTimeOffset, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte[]> returnFunction) { return new DbCommandParameterGenericConfig<byte[]>(DbType.Binary, returnFunction); }
        public DbCommandParameterConfig<TDbParams> String(Func<TDbParams, string> returnFunction) { return new DbCommandParameterGenericConfig<string>(DbType.String, returnFunction); }

        /*Hard value set*/
        public DbCommandParameterConfig<TDbParams> Byte(byte value) { return new DbCommandParameterGenericConfig<byte>(DbType.Byte, value); }
        public DbCommandParameterConfig<TDbParams> SByte(sbyte value) { return new DbCommandParameterGenericConfig<sbyte>(DbType.SByte, value); }
        public DbCommandParameterConfig<TDbParams> Int16(short value) { return new DbCommandParameterGenericConfig<short>(DbType.Int16, value); }
        public DbCommandParameterConfig<TDbParams> Int32(int value) { return new DbCommandParameterGenericConfig<int>(DbType.Int32, value); }
        public DbCommandParameterConfig<TDbParams> Int64(long value) { return new DbCommandParameterGenericConfig<long>(DbType.Int64, value); }
        public DbCommandParameterConfig<TDbParams> UInt16(ushort value) { return new DbCommandParameterGenericConfig<ushort>(DbType.UInt16, value); }
        public DbCommandParameterConfig<TDbParams> UInt32(uint value) { return new DbCommandParameterGenericConfig<uint>(DbType.UInt32, value); }
        public DbCommandParameterConfig<TDbParams> UInt64(ulong value) { return new DbCommandParameterGenericConfig<ulong>(DbType.UInt64, value); }
        public DbCommandParameterConfig<TDbParams> Single(float value) { return new DbCommandParameterGenericConfig<float>(DbType.Single, value); }
        public DbCommandParameterConfig<TDbParams> Double(double value) { return new DbCommandParameterGenericConfig<double>(DbType.Double, value); }
        public DbCommandParameterConfig<TDbParams> Decimal(decimal value) { return new DbCommandParameterGenericConfig<decimal>(DbType.Decimal, value); }
        public DbCommandParameterConfig<TDbParams> Boolean(bool value) { return new DbCommandParameterGenericConfig<bool>(DbType.Boolean, value); }
        public DbCommandParameterConfig<TDbParams> StringFixedLength(char value) { return new DbCommandParameterGenericConfig<char>(DbType.StringFixedLength, value); }
        public DbCommandParameterConfig<TDbParams> Guid(Guid value) { return new DbCommandParameterGenericConfig<Guid>(DbType.Guid, value); }
        public DbCommandParameterConfig<TDbParams> DateTime(DateTime value) { return new DbCommandParameterGenericConfig<DateTime>(DbType.DateTime, value); }
        public DbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset value) { return new DbCommandParameterGenericConfig<DateTimeOffset>(DbType.DateTimeOffset, value); }
        public DbCommandParameterConfig<TDbParams> Binary(byte[] value) { return new DbCommandParameterGenericConfig<byte[]>(DbType.Binary, value); }
        public DbCommandParameterConfig<TDbParams> String(string value) { return new DbCommandParameterGenericConfig<string>(DbType.String, value); }



        /*Optional params Functions*/
        public DbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte?> returnFunction) { return new DbCommandParameterGenericConfig<byte?>(DbType.Byte, returnFunction); }
        public DbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte?> returnFunction) { return new DbCommandParameterGenericConfig<sbyte?>(DbType.SByte, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short?> returnFunction) { return new DbCommandParameterGenericConfig<short?>(DbType.Int16, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int?> returnFunction) { return new DbCommandParameterGenericConfig<int?>(DbType.Int32, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long?> returnFunction) { return new DbCommandParameterGenericConfig<long?>(DbType.Int64, returnFunction); }
        public DbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort?> returnFunction) { return new DbCommandParameterGenericConfig<ushort?>(DbType.UInt16, returnFunction); }
        public DbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint?> returnFunction) { return new DbCommandParameterGenericConfig<uint?>(DbType.UInt32, returnFunction); }
        public DbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong?> returnFunction) { return new DbCommandParameterGenericConfig<ulong?>(DbType.UInt64, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float?> returnFunction) { return new DbCommandParameterGenericConfig<float?>(DbType.Single, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double?> returnFunction) { return new DbCommandParameterGenericConfig<double?>(DbType.Double, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal?> returnFunction) { return new DbCommandParameterGenericConfig<decimal?>(DbType.Decimal, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool?> returnFunction) { return new DbCommandParameterGenericConfig<bool?>(DbType.Boolean, returnFunction); }
        public DbCommandParameterConfig<TDbParams> StringFixedLength(Func<TDbParams, char?> returnFunction) { return new DbCommandParameterGenericConfig<char?>(DbType.StringFixedLength, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid?> returnFunction) { return new DbCommandParameterGenericConfig<Guid?>(DbType.Guid, returnFunction); }
        public DbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime?> returnFunction) { return new DbCommandParameterGenericConfig<DateTime?>(DbType.DateTime, returnFunction); }
        public DbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset?> returnFunction) { return new DbCommandParameterGenericConfig<DateTimeOffset?>(DbType.DateTimeOffset, returnFunction); }
        public DbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte?[]> returnFunction) { return new DbCommandParameterGenericConfig<byte?[]>(DbType.Binary, returnFunction); }

        
        /*Optional Hard value set*/
        public DbCommandParameterConfig<TDbParams> Byte(byte? value) { return new DbCommandParameterGenericConfig<byte?>(DbType.Byte,value); }        
        public DbCommandParameterConfig<TDbParams> SByte(sbyte? value) { return new DbCommandParameterGenericConfig<sbyte?>(DbType.SByte, value); }        
        public DbCommandParameterConfig<TDbParams> Int16(short? value) { return new DbCommandParameterGenericConfig<short?>(DbType.Int16, value); }        
        public DbCommandParameterConfig<TDbParams> Int32(int? value) { return new DbCommandParameterGenericConfig<int?>(DbType.Int32, value); }        
        public DbCommandParameterConfig<TDbParams> Int64(long? value) { return new DbCommandParameterGenericConfig<long?>(DbType.Int64, value); }        
        public DbCommandParameterConfig<TDbParams> UInt16(ushort? value) { return new DbCommandParameterGenericConfig<ushort?>(DbType.UInt16, value); }        
        public DbCommandParameterConfig<TDbParams> UInt32(uint? value) { return new DbCommandParameterGenericConfig<uint?>(DbType.UInt32, value); }        
        public DbCommandParameterConfig<TDbParams> UInt64(ulong? value) { return new DbCommandParameterGenericConfig<ulong?>(DbType.UInt64, value); }        
        public DbCommandParameterConfig<TDbParams> Single(float? value) { return new DbCommandParameterGenericConfig<float?>(DbType.Single, value); }        
        public DbCommandParameterConfig<TDbParams> Double(double? value) { return new DbCommandParameterGenericConfig<double?>(DbType.Double, value); }        
        public DbCommandParameterConfig<TDbParams> Decimal(decimal? value) { return new DbCommandParameterGenericConfig<decimal?>(DbType.Decimal, value); }        
        public DbCommandParameterConfig<TDbParams> Boolean(bool? value) { return new DbCommandParameterGenericConfig<bool?>(DbType.Boolean, value); }        
        public DbCommandParameterConfig<TDbParams> StringFixedLength(char? value) { return new DbCommandParameterGenericConfig<char?>(DbType.StringFixedLength, value); }        
        public DbCommandParameterConfig<TDbParams> Guid(Guid? value) { return new DbCommandParameterGenericConfig<Guid?>(DbType.Guid, value); }        
        public DbCommandParameterConfig<TDbParams> DateTime(DateTime? value) { return new DbCommandParameterGenericConfig<DateTime?>(DbType.DateTime, value); }        
        public DbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset? value) { return new DbCommandParameterGenericConfig<DateTimeOffset?>(DbType.DateTimeOffset, value); }        
        public DbCommandParameterConfig<TDbParams> Binary(byte?[] value) { return new DbCommandParameterGenericConfig<byte?[]>(DbType.Binary, value); }

        
    }
}
