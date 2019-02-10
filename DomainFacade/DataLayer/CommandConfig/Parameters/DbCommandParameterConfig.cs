using DomainFacade.DataLayer.Models;
using System;
using System.Data;

namespace DomainFacade.DataLayer.CommandConfig.Parameters
{       
    public abstract class DbCommandParameterConfig<TDbParams> : DbCommandParameterConfigBase<TDbParams> where TDbParams : IDbParamsModel
    {
        protected DbCommandParameterConfig(DbType dbType) : base(dbType){}
        public DbCommandParameterConfig<TDbParams> Required() { isNullable = false; return this; }
        protected class DbCommandParameterGenericConfig<N> : DbCommandParameterConfig<TDbParams>
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
        protected class StringWithMaxParameterConfig : DbCommandParameterGenericConfig<string>
        {
            private int Max;
            public StringWithMaxParameterConfig(int max, Func<TDbParams, string> returnFunction) : base(DbType.String, returnFunction)
            {
                Max = max;
            }
            public override object GetParam(TDbParams model)
            {
                string ret = ReturnFunction(model);
                if (ret.Length > Max)
                {
                    throw new Exception("Length of paramerter value (" + ret.Length + ") is greater than " + Max);
                }
                else
                {
                    return ret;
                }
            }
        }
        
        public static DbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte> returnFunction) { return new ByteParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte> returnFunction) { return new SByteParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> String(Func<TDbParams, string> returnFunction) { return new StringParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short> returnFunction) { return new Int16ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int> returnFunction) { return new Int32ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long> returnFunction) { return new Int64ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort> returnFunction) { return new UInt16ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint> returnFunction) { return new UInt32ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong> returnFunction) { return new UInt64ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float> returnFunction) { return new SingleParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double> returnFunction) { return new DoubleParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal> returnFunction) { return new DecimalParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool> returnFunction) { return new BooleanParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> StringFixedLength(Func<TDbParams, char> returnFunction) { return new StringFixedLengthParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid> returnFunction) { return new GuidParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime> returnFunction) { return new DateTimeParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset> returnFunction) { return new DateTimeOffsetParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte[]> returnFunction) { return new BinaryParameterConfig(returnFunction); }

        /*Hard value set*/
        public static DbCommandParameterConfig<TDbParams> Byte(byte value) { return new ByteParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> SByte(sbyte value) { return new SByteParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> String(string value) { return new StringParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Int16(short value) { return new Int16ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Int32(int value) { return new Int32ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Int64(long value) { return new Int64ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> UInt16(ushort value) { return new UInt16ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> UInt32(uint value) { return new UInt32ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> UInt64(ulong value) { return new UInt64ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Single(float value) { return new SingleParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Double(double value) { return new DoubleParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Decimal(decimal value) { return new DecimalParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Boolean(bool value) { return new BooleanParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> StringFixedLength(char value) { return new StringFixedLengthParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Guid(Guid value) { return new GuidParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> DateTime(DateTime value) { return new DateTimeParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset value) { return new DateTimeOffsetParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Binary(byte[] value) { return new BinaryParameterConfig(value); }



        protected sealed class ByteParameterConfig : DbCommandParameterGenericConfig<byte>
        {
            public ByteParameterConfig(Func<TDbParams, byte> returnFunction) : base(DbType.Byte, returnFunction) { }
            public ByteParameterConfig(byte value) : base(DbType.Byte, value) { }
        }
        protected sealed class SByteParameterConfig : DbCommandParameterGenericConfig<sbyte>
        {
            public SByteParameterConfig(Func<TDbParams, sbyte> returnFunction) : base(DbType.SByte, returnFunction) { }
            public SByteParameterConfig(sbyte value) : base(DbType.SByte, value) { }
        }
        protected sealed class StringParameterConfig : DbCommandParameterGenericConfig<string>
        {
            public StringParameterConfig(Func<TDbParams, string> returnFunction) : base(DbType.String, returnFunction) { }
            public StringParameterConfig(string value) : base(DbType.String, value) { }
        }
        protected sealed class Int16ParameterConfig : DbCommandParameterGenericConfig<short>
        {
            public Int16ParameterConfig(Func<TDbParams, short> returnFunction) : base(DbType.Int16, returnFunction) { }
            public Int16ParameterConfig(short value) : base(DbType.Int16, value) { }
        }
        protected sealed class Int32ParameterConfig : DbCommandParameterGenericConfig<int>
        {
            public Int32ParameterConfig(Func<TDbParams, int> returnFunction) : base(DbType.Int32, returnFunction) { }
            public Int32ParameterConfig(int value) : base(DbType.Int32, value) { }
        }
        protected sealed class Int64ParameterConfig : DbCommandParameterGenericConfig<long>
        {
            public Int64ParameterConfig(Func<TDbParams, long> returnFunction) : base(DbType.Int64, returnFunction) { }
            public Int64ParameterConfig(long value) : base(DbType.Int64, value) { }
        }
        protected sealed class UInt16ParameterConfig : DbCommandParameterGenericConfig<ushort>
        {
            public UInt16ParameterConfig(Func<TDbParams, ushort> returnFunction) : base(DbType.UInt16, returnFunction) { }
            public UInt16ParameterConfig(ushort value) : base(DbType.Int16, value) { }
        }
        protected sealed class UInt32ParameterConfig : DbCommandParameterGenericConfig<uint>
        {
            public UInt32ParameterConfig(Func<TDbParams, uint> returnFunction) : base(DbType.UInt32, returnFunction) { }
            public UInt32ParameterConfig(uint value) : base(DbType.Int32, value) { }
        }
        protected sealed class UInt64ParameterConfig : DbCommandParameterGenericConfig<ulong>
        {
            public UInt64ParameterConfig(Func<TDbParams, ulong> returnFunction) : base(DbType.UInt64, returnFunction) { }
            public UInt64ParameterConfig(ulong value) : base(DbType.Int64, value) { }
        }
        protected sealed class SingleParameterConfig : DbCommandParameterGenericConfig<float>
        {
            public SingleParameterConfig(Func<TDbParams, float> returnFunction) : base(DbType.Single, returnFunction) { }
            public SingleParameterConfig(float value) : base(DbType.Single, value) { }
        }
        protected sealed class DoubleParameterConfig : DbCommandParameterGenericConfig<double>
        {
            public DoubleParameterConfig(Func<TDbParams, double> returnFunction) : base(DbType.Double, returnFunction) { }
            public DoubleParameterConfig(double value) : base(DbType.Double, value) { }
        }
        protected sealed class DecimalParameterConfig : DbCommandParameterGenericConfig<decimal>
        {
            public DecimalParameterConfig(Func<TDbParams, decimal> returnFunction) : base(DbType.Decimal, returnFunction) { }
            public DecimalParameterConfig(decimal value) : base(DbType.Decimal, value) { }
        }
        protected sealed class BooleanParameterConfig : DbCommandParameterGenericConfig<bool>
        {
            public BooleanParameterConfig(Func<TDbParams, bool> returnFunction) : base(DbType.Boolean, returnFunction) { }
            public BooleanParameterConfig(bool value) : base(DbType.Boolean, value) { }
        }
        protected sealed class StringFixedLengthParameterConfig : DbCommandParameterGenericConfig<char>
        {
            public StringFixedLengthParameterConfig(Func<TDbParams, char> returnFunction) : base(DbType.StringFixedLength, returnFunction) { }
            public StringFixedLengthParameterConfig(char value) : base(DbType.StringFixedLength, value) { }
        }
        protected sealed class GuidParameterConfig : DbCommandParameterGenericConfig<Guid>
        {
            public GuidParameterConfig(Func<TDbParams, Guid> returnFunction) : base(DbType.Guid, returnFunction) { }
            public GuidParameterConfig(Guid value) : base(DbType.Guid, value) { }
        }
        protected sealed class DateTimeParameterConfig : DbCommandParameterGenericConfig<DateTime>
        {
            public DateTimeParameterConfig(Func<TDbParams, DateTime> returnFunction) : base(DbType.DateTime, returnFunction) { }
            public DateTimeParameterConfig(DateTime value) : base(DbType.DateTime, value) { }
        }
        protected sealed class DateTimeOffsetParameterConfig : DbCommandParameterGenericConfig<DateTimeOffset>
        {
            public DateTimeOffsetParameterConfig(Func<TDbParams, DateTimeOffset> returnFunction) : base(DbType.DateTimeOffset, returnFunction) { }
            public DateTimeOffsetParameterConfig(DateTimeOffset value) : base(DbType.DateTimeOffset, value) { }
        }
        protected sealed class BinaryParameterConfig : DbCommandParameterGenericConfig<byte[]>
        {
            public BinaryParameterConfig(Func<TDbParams, byte[]> returnFunction) : base(DbType.Binary, returnFunction) { }
            public BinaryParameterConfig(byte[] value) : base(DbType.Binary, value) { }
        }

        /*Optional params Functions*/

        public static DbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte?> returnFunction) { return new OptionalByteParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte?> returnFunction) { return new OptionalSByteParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short?> returnFunction) { return new OptionalInt16ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int?> returnFunction) { return new OptionalInt32ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long?> returnFunction) { return new OptionalInt64ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort?> returnFunction) { return new OptionalUInt16ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint?> returnFunction) { return new OptionalUInt32ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong?> returnFunction) { return new OptionalUInt64ParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float?> returnFunction) { return new OptionalSingleParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double?> returnFunction) { return new OptionalDoubleParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal?> returnFunction) { return new OptionalDecimalParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool?> returnFunction) { return new OptionalBooleanParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> StringFixedLength(Func<TDbParams, char?> returnFunction) { return new OptionalStringFixedLengthParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid?> returnFunction) { return new OptionalGuidParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime?> returnFunction) { return new OptionalDateTimeParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset?> returnFunction) { return new OptionalDateTimeOffsetParameterConfig(returnFunction); }
        public static DbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte?[]> returnFunction) { return new OptionalBinaryParameterConfig(returnFunction); }

        /*Optional Hard value set*/
        public static DbCommandParameterConfig<TDbParams> Byte(byte? value) { return new OptionalByteParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> SByte(sbyte? value) { return new OptionalSByteParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Int16(short? value) { return new OptionalInt16ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Int32(int? value) { return new OptionalInt32ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Int64(long? value) { return new OptionalInt64ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> UInt16(ushort? value) { return new OptionalUInt16ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> UInt32(uint? value) { return new OptionalUInt32ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> UInt64(ulong? value) { return new OptionalUInt64ParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Single(float? value) { return new OptionalSingleParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Double(double? value) { return new OptionalDoubleParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Decimal(decimal? value) { return new OptionalDecimalParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Boolean(bool? value) { return new OptionalBooleanParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> StringFixedLength(char? value) { return new OptionalStringFixedLengthParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Guid(Guid? value) { return new OptionalGuidParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> DateTime(DateTime? value) { return new OptionalDateTimeParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset? value) { return new OptionalDateTimeOffsetParameterConfig(value); }
        public static DbCommandParameterConfig<TDbParams> Binary(byte?[] value) { return new OptionalBinaryParameterConfig(value); }


        protected sealed class OptionalByteParameterConfig : DbCommandParameterGenericConfig<byte?>
        {
            public OptionalByteParameterConfig(Func<TDbParams, byte?> returnFunction) : base(DbType.Byte, returnFunction) { }
            public OptionalByteParameterConfig(byte? value) : base(DbType.Byte, value) { }
        }
        protected sealed class OptionalSByteParameterConfig : DbCommandParameterGenericConfig<sbyte?>
        {
            public OptionalSByteParameterConfig(Func<TDbParams, sbyte?> returnFunction) : base(DbType.SByte, returnFunction) { }
            public OptionalSByteParameterConfig(sbyte? value) : base(DbType.SByte, value) { }
        }
        protected sealed class OptionalInt16ParameterConfig : DbCommandParameterGenericConfig<short?>
        {
            public OptionalInt16ParameterConfig(Func<TDbParams, short?> returnFunction) : base(DbType.Int16, returnFunction) { }
            public OptionalInt16ParameterConfig(short? value) : base(DbType.Int16, value) { }
        }
        protected sealed class OptionalInt32ParameterConfig : DbCommandParameterGenericConfig<int?>
        {
            public OptionalInt32ParameterConfig(Func<TDbParams, int?> returnFunction) : base(DbType.Int32, returnFunction) { }
            public OptionalInt32ParameterConfig(int? value) : base(DbType.Int32, value) { }
        }
        protected sealed class OptionalInt64ParameterConfig : DbCommandParameterGenericConfig<long?>
        {
            public OptionalInt64ParameterConfig(Func<TDbParams, long?> returnFunction) : base(DbType.Int64, returnFunction) { }
            public OptionalInt64ParameterConfig(long? value) : base(DbType.Int64, value) { }
        }
        protected sealed class OptionalUInt16ParameterConfig : DbCommandParameterGenericConfig<ushort?>
        {
            public OptionalUInt16ParameterConfig(Func<TDbParams, ushort?> returnFunction) : base(DbType.UInt16, returnFunction) { }
            public OptionalUInt16ParameterConfig(ushort? value) : base(DbType.Int16, value) { }
        }
        protected sealed class OptionalUInt32ParameterConfig : DbCommandParameterGenericConfig<uint?>
        {
            public OptionalUInt32ParameterConfig(Func<TDbParams, uint?> returnFunction) : base(DbType.UInt32, returnFunction) { }
            public OptionalUInt32ParameterConfig(uint? value) : base(DbType.Int32, value) { }
        }
        protected sealed class OptionalUInt64ParameterConfig : DbCommandParameterGenericConfig<ulong?>
        {
            public OptionalUInt64ParameterConfig(Func<TDbParams, ulong?> returnFunction) : base(DbType.UInt64, returnFunction) { }
            public OptionalUInt64ParameterConfig(ulong? value) : base(DbType.Int64, value) { }
        }
        protected sealed class OptionalSingleParameterConfig : DbCommandParameterGenericConfig<float?>
        {
            public OptionalSingleParameterConfig(Func<TDbParams, float?> returnFunction) : base(DbType.Single, returnFunction) { }
            public OptionalSingleParameterConfig(float? value) : base(DbType.Single, value) { }
        }
        protected sealed class OptionalDoubleParameterConfig : DbCommandParameterGenericConfig<double?>
        {
            public OptionalDoubleParameterConfig(Func<TDbParams, double?> returnFunction) : base(DbType.Double, returnFunction) { }
            public OptionalDoubleParameterConfig(double? value) : base(DbType.Double, value) { }
        }
        protected sealed class OptionalDecimalParameterConfig : DbCommandParameterGenericConfig<decimal?>
        {
            public OptionalDecimalParameterConfig(Func<TDbParams, decimal?> returnFunction) : base(DbType.Decimal, returnFunction) { }
            public OptionalDecimalParameterConfig(decimal? value) : base(DbType.Decimal, value) { }
        }
        protected sealed class OptionalBooleanParameterConfig : DbCommandParameterGenericConfig<bool?>
        {
            public OptionalBooleanParameterConfig(Func<TDbParams, bool?> returnFunction) : base(DbType.Boolean, returnFunction) { }
            public OptionalBooleanParameterConfig(bool? value) : base(DbType.Boolean, value) { }
        }
        protected sealed class OptionalStringFixedLengthParameterConfig : DbCommandParameterGenericConfig<char?>
        {
            public OptionalStringFixedLengthParameterConfig(Func<TDbParams, char?> returnFunction) : base(DbType.StringFixedLength, returnFunction) { }
            public OptionalStringFixedLengthParameterConfig(char? value) : base(DbType.StringFixedLength, value) { }
        }
        protected sealed class OptionalGuidParameterConfig : DbCommandParameterGenericConfig<Guid?>
        {
            public OptionalGuidParameterConfig(Func<TDbParams, Guid?> returnFunction) : base(DbType.Guid, returnFunction) { }
            public OptionalGuidParameterConfig(Guid? value) : base(DbType.Guid, value) { }
        }
        protected sealed class OptionalDateTimeParameterConfig : DbCommandParameterGenericConfig<DateTime?>
        {
            public OptionalDateTimeParameterConfig(Func<TDbParams, DateTime?> returnFunction) : base(DbType.DateTime, returnFunction) { }
            public OptionalDateTimeParameterConfig(DateTime? value) : base(DbType.DateTime, value) { }
        }
        protected sealed class OptionalDateTimeOffsetParameterConfig : DbCommandParameterGenericConfig<DateTimeOffset?>
        {
            public OptionalDateTimeOffsetParameterConfig(Func<TDbParams, DateTimeOffset?> returnFunction) : base(DbType.DateTimeOffset, returnFunction) { }
            public OptionalDateTimeOffsetParameterConfig(DateTimeOffset? value) : base(DbType.DateTimeOffset, value) { }
        }
        protected sealed class OptionalBinaryParameterConfig : DbCommandParameterGenericConfig<byte?[]>
        {
            public OptionalBinaryParameterConfig(Func<TDbParams, byte?[]> returnFunction) : base(DbType.Binary, returnFunction) { }
            public OptionalBinaryParameterConfig(byte?[] value) : base(DbType.Binary, value) { }
        }

    }
}
