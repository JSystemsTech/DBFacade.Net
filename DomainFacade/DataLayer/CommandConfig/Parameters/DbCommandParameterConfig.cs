using DomainFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace DomainFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary></summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public abstract class DbCommandParameterConfig<TDbParams> : DbCommandParameterConfigBase<TDbParams> where TDbParams : IDbParamsModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandParameterConfig{TDbParams}"/> class.
        /// </summary>
        /// <param name="dbType">Type of the database.</param>
        protected DbCommandParameterConfig(DbType dbType) : base(dbType){}
        /// <summary>
        /// Requireds this instance.
        /// </summary>
        /// <returns></returns>
        public DbCommandParameterConfig<TDbParams> Required() { isNullable = false; return this; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected class DbCommandParameterGenericConfig<N> : DbCommandParameterConfig<TDbParams>
        {
            /// <summary>
            /// Gets the return function.
            /// </summary>
            /// <value>
            /// The return function.
            /// </value>
            public Func<TDbParams, N> ReturnFunction { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="DbCommandParameterGenericConfig`1"/> class.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="returnFunction">The return function.</param>
            public DbCommandParameterGenericConfig(DbType dbType, Func<TDbParams, N> returnFunction) : base(dbType)
            {
                this.ReturnFunction = returnFunction;
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="DbCommandParameterGenericConfig`1"/> class.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="value">The value.</param>
            public DbCommandParameterGenericConfig(DbType dbType, N value) : base(dbType)
            {
               Func<TDbParams, N> ret = model => value;
               ReturnFunction = ret;
            }
            /// <summary>
            /// Gets the parameter.
            /// </summary>
            /// <param name="model">The model.</param>
            /// <returns></returns>
            public override object GetParam(TDbParams model)
            {
                return ReturnFunction(model);
            }
            /// <summary>
            /// Determines whether [is nullable type].
            /// </summary>
            /// <returns>
            ///   <c>true</c> if [is nullable type]; otherwise, <c>false</c>.
            /// </returns>
            private bool IsNullableType() => Nullable.GetUnderlyingType(typeof(N)) != null;
            /// <summary>
            /// Determines whether this instance is nullable.
            /// </summary>
            /// <returns>
            ///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
            /// </returns>
            public override bool IsNullable()
            {
                if (!isNullable)
                {
                    return isNullable;
                }
                return IsNullableType();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected class StringWithMaxParameterConfig : DbCommandParameterGenericConfig<string>
        {
            /// <summary>
            /// Determines the maximum of the parameters.
            /// </summary>
            private int Max;
            /// <summary>
            /// Initializes a new instance of the <see cref="StringWithMaxParameterConfig"/> class.
            /// </summary>
            /// <param name="max">The maximum.</param>
            /// <param name="returnFunction">The return function.</param>
            public StringWithMaxParameterConfig(int max, Func<TDbParams, string> returnFunction) : base(DbType.String, returnFunction)
            {
                Max = max;
            }
            /// <summary>
            /// Gets the parameter.
            /// </summary>
            /// <param name="model">The model.</param>
            /// <returns></returns>
            /// <exception cref="Exception">Length of paramerter value (" + ret.Length + ") is greater than " + Max</exception>
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

        /// <summary>
        /// Bytes the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte> returnFunction) { return new ByteParameterConfig(returnFunction); }
        /// <summary>
        /// ses the byte.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte> returnFunction) { return new SByteParameterConfig(returnFunction); }
        /// <summary>
        /// Strings the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> String(Func<TDbParams, string> returnFunction) { return new StringParameterConfig(returnFunction); }
        /// <summary>
        /// Strings the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> String(Func<TDbParams, IEnumerable<string>> returnFunction) { return new EnumerableStringParameterConfig(returnFunction); }
        /// <summary>
        /// Strings the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> String(Func<TDbParams, IEnumerable<string>> returnFunction, string delimeter) { return new EnumerableStringParameterConfig(returnFunction, delimeter); }
        /// <summary>
        /// Int16s the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short> returnFunction) { return new Int16ParameterConfig(returnFunction); }
        /// <summary>
        /// Int32s the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int> returnFunction) { return new Int32ParameterConfig(returnFunction); }
        /// <summary>
        /// Int64s the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long> returnFunction) { return new Int64ParameterConfig(returnFunction); }
        /// <summary>
        /// us the int16.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort> returnFunction) { return new UInt16ParameterConfig(returnFunction); }
        /// <summary>
        /// us the int32.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint> returnFunction) { return new UInt32ParameterConfig(returnFunction); }
        /// <summary>
        /// us the int64.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong> returnFunction) { return new UInt64ParameterConfig(returnFunction); }
        /// <summary>
        /// Singles the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float> returnFunction) { return new SingleParameterConfig(returnFunction); }
        /// <summary>
        /// Doubles the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double> returnFunction) { return new DoubleParameterConfig(returnFunction); }
        /// <summary>
        /// Decimals the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal> returnFunction) { return new DecimalParameterConfig(returnFunction); }
        /// <summary>
        /// Booleans the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool> returnFunction) { return new BooleanParameterConfig(returnFunction); }
        /// <summary>
        /// Strings the length of the fixed.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> StringFixedLength(Func<TDbParams, char> returnFunction) { return new StringFixedLengthParameterConfig(returnFunction); }
        /// <summary>
        /// Unique identifiers the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid> returnFunction) { return new GuidParameterConfig(returnFunction); }
        /// <summary>
        /// Dates the time.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime> returnFunction) { return new DateTimeParameterConfig(returnFunction); }
        /// <summary>
        /// Dates the time offset.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset> returnFunction) { return new DateTimeOffsetParameterConfig(returnFunction); }
        /// <summary>
        /// Binaries the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte[]> returnFunction) { return new BinaryParameterConfig(returnFunction); }

        /*Hard value set*/
        /// <summary>
        /// Bytes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Byte(byte value) { return new ByteParameterConfig(value); }
        /// <summary>
        /// ses the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> SByte(sbyte value) { return new SByteParameterConfig(value); }
        /// <summary>
        /// Strings the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> String(string value) { return new StringParameterConfig(value); }
        /// <summary>
        /// Strings the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> String(IEnumerable<string> value) { return new EnumerableStringParameterConfig(value); }
        /// <summary>
        /// Strings the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> String(IEnumerable<string> value, string delimeter) { return new EnumerableStringParameterConfig(value, delimeter); }
        /// <summary>
        /// Int16s the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int16(short value) { return new Int16ParameterConfig(value); }
        /// <summary>
        /// Int32s the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int32(int value) { return new Int32ParameterConfig(value); }
        /// <summary>
        /// Int64s the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int64(long value) { return new Int64ParameterConfig(value); }
        /// <summary>
        /// us the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt16(ushort value) { return new UInt16ParameterConfig(value); }
        /// <summary>
        /// us the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt32(uint value) { return new UInt32ParameterConfig(value); }
        /// <summary>
        /// us the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt64(ulong value) { return new UInt64ParameterConfig(value); }
        /// <summary>
        /// Singles the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Single(float value) { return new SingleParameterConfig(value); }
        /// <summary>
        /// Doubles the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Double(double value) { return new DoubleParameterConfig(value); }
        /// <summary>
        /// Decimals the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Decimal(decimal value) { return new DecimalParameterConfig(value); }
        /// <summary>
        /// Booleans the specified value.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Boolean(bool value) { return new BooleanParameterConfig(value); }
        /// <summary>
        /// Strings the length of the fixed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> StringFixedLength(char value) { return new StringFixedLengthParameterConfig(value); }
        /// <summary>
        /// Unique identifiers the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Guid(Guid value) { return new GuidParameterConfig(value); }
        /// <summary>
        /// Dates the time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> DateTime(DateTime value) { return new DateTimeParameterConfig(value); }
        /// <summary>
        /// Dates the time offset.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset value) { return new DateTimeOffsetParameterConfig(value); }
        /// <summary>
        /// Binaries the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Binary(byte[] value) { return new BinaryParameterConfig(value); }



        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class ByteParameterConfig : DbCommandParameterGenericConfig<byte>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ByteParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public ByteParameterConfig(Func<TDbParams, byte> returnFunction) : base(DbType.Byte, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="ByteParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public ByteParameterConfig(byte value) : base(DbType.Byte, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class SByteParameterConfig : DbCommandParameterGenericConfig<sbyte>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SByteParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public SByteParameterConfig(Func<TDbParams, sbyte> returnFunction) : base(DbType.SByte, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="SByteParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public SByteParameterConfig(sbyte value) : base(DbType.SByte, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class StringParameterConfig : DbCommandParameterGenericConfig<string>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="StringParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public StringParameterConfig(Func<TDbParams, string> returnFunction) : base(DbType.String, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="StringParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public StringParameterConfig(string value) : base(DbType.String, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class Int16ParameterConfig : DbCommandParameterGenericConfig<short>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Int16ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public Int16ParameterConfig(Func<TDbParams, short> returnFunction) : base(DbType.Int16, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Int16ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public Int16ParameterConfig(short value) : base(DbType.Int16, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class Int32ParameterConfig : DbCommandParameterGenericConfig<int>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Int32ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public Int32ParameterConfig(Func<TDbParams, int> returnFunction) : base(DbType.Int32, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Int32ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public Int32ParameterConfig(int value) : base(DbType.Int32, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class Int64ParameterConfig : DbCommandParameterGenericConfig<long>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Int64ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public Int64ParameterConfig(Func<TDbParams, long> returnFunction) : base(DbType.Int64, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="Int64ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public Int64ParameterConfig(long value) : base(DbType.Int64, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class UInt16ParameterConfig : DbCommandParameterGenericConfig<ushort>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="UInt16ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public UInt16ParameterConfig(Func<TDbParams, ushort> returnFunction) : base(DbType.UInt16, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="UInt16ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public UInt16ParameterConfig(ushort value) : base(DbType.Int16, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class UInt32ParameterConfig : DbCommandParameterGenericConfig<uint>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="UInt32ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public UInt32ParameterConfig(Func<TDbParams, uint> returnFunction) : base(DbType.UInt32, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="UInt32ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public UInt32ParameterConfig(uint value) : base(DbType.Int32, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class UInt64ParameterConfig : DbCommandParameterGenericConfig<ulong>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="UInt64ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public UInt64ParameterConfig(Func<TDbParams, ulong> returnFunction) : base(DbType.UInt64, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="UInt64ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public UInt64ParameterConfig(ulong value) : base(DbType.Int64, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class SingleParameterConfig : DbCommandParameterGenericConfig<float>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SingleParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public SingleParameterConfig(Func<TDbParams, float> returnFunction) : base(DbType.Single, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="SingleParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public SingleParameterConfig(float value) : base(DbType.Single, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class DoubleParameterConfig : DbCommandParameterGenericConfig<double>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DoubleParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public DoubleParameterConfig(Func<TDbParams, double> returnFunction) : base(DbType.Double, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DoubleParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public DoubleParameterConfig(double value) : base(DbType.Double, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class DecimalParameterConfig : DbCommandParameterGenericConfig<decimal>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DecimalParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public DecimalParameterConfig(Func<TDbParams, decimal> returnFunction) : base(DbType.Decimal, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DecimalParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public DecimalParameterConfig(decimal value) : base(DbType.Decimal, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class BooleanParameterConfig : DbCommandParameterGenericConfig<bool>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="BooleanParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public BooleanParameterConfig(Func<TDbParams, bool> returnFunction) : base(DbType.Boolean, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="BooleanParameterConfig"/> class.
            /// </summary>
            /// <param name="value">if set to <c>true</c> [value].</param>
            public BooleanParameterConfig(bool value) : base(DbType.Boolean, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class StringFixedLengthParameterConfig : DbCommandParameterGenericConfig<char>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="StringFixedLengthParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public StringFixedLengthParameterConfig(Func<TDbParams, char> returnFunction) : base(DbType.StringFixedLength, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="StringFixedLengthParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public StringFixedLengthParameterConfig(char value) : base(DbType.StringFixedLength, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class GuidParameterConfig : DbCommandParameterGenericConfig<Guid>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GuidParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public GuidParameterConfig(Func<TDbParams, Guid> returnFunction) : base(DbType.Guid, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="GuidParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public GuidParameterConfig(Guid value) : base(DbType.Guid, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class DateTimeParameterConfig : DbCommandParameterGenericConfig<DateTime>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public DateTimeParameterConfig(Func<TDbParams, DateTime> returnFunction) : base(DbType.DateTime, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public DateTimeParameterConfig(DateTime value) : base(DbType.DateTime, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class DateTimeOffsetParameterConfig : DbCommandParameterGenericConfig<DateTimeOffset>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeOffsetParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public DateTimeOffsetParameterConfig(Func<TDbParams, DateTimeOffset> returnFunction) : base(DbType.DateTimeOffset, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeOffsetParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public DateTimeOffsetParameterConfig(DateTimeOffset value) : base(DbType.DateTimeOffset, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class BinaryParameterConfig : DbCommandParameterGenericConfig<byte[]>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="BinaryParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public BinaryParameterConfig(Func<TDbParams, byte[]> returnFunction) : base(DbType.Binary, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="BinaryParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public BinaryParameterConfig(byte[] value) : base(DbType.Binary, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class EnumerableStringParameterConfig : DbCommandParameterGenericConfig<IEnumerable<string>>
        {
            private string Delimeter = ",";
            /// <summary>
            /// Initializes a new instance of the <see cref="EnumerableStringParameterConfig" /> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public EnumerableStringParameterConfig(Func<TDbParams, IEnumerable<string>> returnFunction) : base(DbType.String, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="EnumerableStringParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public EnumerableStringParameterConfig(IEnumerable<string> value) : base(DbType.String, value) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="EnumerableStringParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            /// <param name="delimeter">The delimeter.</param>
            public EnumerableStringParameterConfig(Func<TDbParams, IEnumerable<string>> returnFunction, string delimeter) : base(DbType.String, returnFunction) { Delimeter = delimeter; }
            /// <summary>
            /// Initializes a new instance of the <see cref="EnumerableStringParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="delimeter">The delimeter.</param>
            public EnumerableStringParameterConfig(IEnumerable<string> value, string delimeter) : base(DbType.String, value) { Delimeter = delimeter; }
            /// <summary>
            /// Gets the parameter.
            /// </summary>
            /// <param name="model">The model.</param>
            /// <returns></returns>
            public override object GetParam(TDbParams model)
            {
                IEnumerable<string> value = ReturnFunction(model);
                return value == null ? string.Empty : string.Join(Delimeter, value);
            }
        }

        /*Optional params Functions*/

        /// <summary>
        /// Bytes the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte?> returnFunction) { return new OptionalByteParameterConfig(returnFunction); }
        /// <summary>
        /// ses the byte.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte?> returnFunction) { return new OptionalSByteParameterConfig(returnFunction); }
        /// <summary>
        /// Int16s the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short?> returnFunction) { return new OptionalInt16ParameterConfig(returnFunction); }
        /// <summary>
        /// Int32s the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int?> returnFunction) { return new OptionalInt32ParameterConfig(returnFunction); }
        /// <summary>
        /// Int64s the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long?> returnFunction) { return new OptionalInt64ParameterConfig(returnFunction); }
        /// <summary>
        /// us the int16.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort?> returnFunction) { return new OptionalUInt16ParameterConfig(returnFunction); }
        /// <summary>
        /// us the int32.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint?> returnFunction) { return new OptionalUInt32ParameterConfig(returnFunction); }
        /// <summary>
        /// us the int64.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong?> returnFunction) { return new OptionalUInt64ParameterConfig(returnFunction); }
        /// <summary>
        /// Singles the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float?> returnFunction) { return new OptionalSingleParameterConfig(returnFunction); }
        /// <summary>
        /// Doubles the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double?> returnFunction) { return new OptionalDoubleParameterConfig(returnFunction); }
        /// <summary>
        /// Decimals the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal?> returnFunction) { return new OptionalDecimalParameterConfig(returnFunction); }
        /// <summary>
        /// Booleans the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool?> returnFunction) { return new OptionalBooleanParameterConfig(returnFunction); }
        /// <summary>
        /// Strings the length of the fixed.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> StringFixedLength(Func<TDbParams, char?> returnFunction) { return new OptionalStringFixedLengthParameterConfig(returnFunction); }
        /// <summary>
        /// Unique identifiers the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid?> returnFunction) { return new OptionalGuidParameterConfig(returnFunction); }
        /// <summary>
        /// Dates the time.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime?> returnFunction) { return new OptionalDateTimeParameterConfig(returnFunction); }
        /// <summary>
        /// Dates the time offset.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset?> returnFunction) { return new OptionalDateTimeOffsetParameterConfig(returnFunction); }
        /// <summary>
        /// Binaries the specified return function.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte?[]> returnFunction) { return new OptionalBinaryParameterConfig(returnFunction); }

        /*Optional Hard value set*/
        /// <summary>
        /// Bytes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Byte(byte? value) { return new OptionalByteParameterConfig(value); }
        /// <summary>
        /// ses the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> SByte(sbyte? value) { return new OptionalSByteParameterConfig(value); }
        /// <summary>
        /// Int16s the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int16(short? value) { return new OptionalInt16ParameterConfig(value); }
        /// <summary>
        /// Int32s the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int32(int? value) { return new OptionalInt32ParameterConfig(value); }
        /// <summary>
        /// Int64s the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Int64(long? value) { return new OptionalInt64ParameterConfig(value); }
        /// <summary>
        /// us the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt16(ushort? value) { return new OptionalUInt16ParameterConfig(value); }
        /// <summary>
        /// us the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt32(uint? value) { return new OptionalUInt32ParameterConfig(value); }
        /// <summary>
        /// us the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> UInt64(ulong? value) { return new OptionalUInt64ParameterConfig(value); }
        /// <summary>
        /// Singles the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Single(float? value) { return new OptionalSingleParameterConfig(value); }
        /// <summary>
        /// Doubles the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Double(double? value) { return new OptionalDoubleParameterConfig(value); }
        /// <summary>
        /// Decimals the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Decimal(decimal? value) { return new OptionalDecimalParameterConfig(value); }
        /// <summary>
        /// Booleans the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Boolean(bool? value) { return new OptionalBooleanParameterConfig(value); }
        /// <summary>
        /// Strings the length of the fixed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> StringFixedLength(char? value) { return new OptionalStringFixedLengthParameterConfig(value); }
        /// <summary>
        /// Unique identifiers the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Guid(Guid? value) { return new OptionalGuidParameterConfig(value); }
        /// <summary>
        /// Dates the time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> DateTime(DateTime? value) { return new OptionalDateTimeParameterConfig(value); }
        /// <summary>
        /// Dates the time offset.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset? value) { return new OptionalDateTimeOffsetParameterConfig(value); }
        /// <summary>
        /// Binaries the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DbCommandParameterConfig<TDbParams> Binary(byte?[] value) { return new OptionalBinaryParameterConfig(value); }


        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalByteParameterConfig : DbCommandParameterGenericConfig<byte?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalByteParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalByteParameterConfig(Func<TDbParams, byte?> returnFunction) : base(DbType.Byte, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalByteParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalByteParameterConfig(byte? value) : base(DbType.Byte, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalSByteParameterConfig : DbCommandParameterGenericConfig<sbyte?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalSByteParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalSByteParameterConfig(Func<TDbParams, sbyte?> returnFunction) : base(DbType.SByte, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalSByteParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalSByteParameterConfig(sbyte? value) : base(DbType.SByte, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalInt16ParameterConfig : DbCommandParameterGenericConfig<short?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalInt16ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalInt16ParameterConfig(Func<TDbParams, short?> returnFunction) : base(DbType.Int16, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalInt16ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalInt16ParameterConfig(short? value) : base(DbType.Int16, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalInt32ParameterConfig : DbCommandParameterGenericConfig<int?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalInt32ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalInt32ParameterConfig(Func<TDbParams, int?> returnFunction) : base(DbType.Int32, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalInt32ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalInt32ParameterConfig(int? value) : base(DbType.Int32, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalInt64ParameterConfig : DbCommandParameterGenericConfig<long?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalInt64ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalInt64ParameterConfig(Func<TDbParams, long?> returnFunction) : base(DbType.Int64, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalInt64ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalInt64ParameterConfig(long? value) : base(DbType.Int64, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalUInt16ParameterConfig : DbCommandParameterGenericConfig<ushort?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalUInt16ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalUInt16ParameterConfig(Func<TDbParams, ushort?> returnFunction) : base(DbType.UInt16, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalUInt16ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalUInt16ParameterConfig(ushort? value) : base(DbType.Int16, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalUInt32ParameterConfig : DbCommandParameterGenericConfig<uint?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalUInt32ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalUInt32ParameterConfig(Func<TDbParams, uint?> returnFunction) : base(DbType.UInt32, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalUInt32ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalUInt32ParameterConfig(uint? value) : base(DbType.Int32, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalUInt64ParameterConfig : DbCommandParameterGenericConfig<ulong?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalUInt64ParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalUInt64ParameterConfig(Func<TDbParams, ulong?> returnFunction) : base(DbType.UInt64, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalUInt64ParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalUInt64ParameterConfig(ulong? value) : base(DbType.Int64, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalSingleParameterConfig : DbCommandParameterGenericConfig<float?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalSingleParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalSingleParameterConfig(Func<TDbParams, float?> returnFunction) : base(DbType.Single, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalSingleParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalSingleParameterConfig(float? value) : base(DbType.Single, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalDoubleParameterConfig : DbCommandParameterGenericConfig<double?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalDoubleParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalDoubleParameterConfig(Func<TDbParams, double?> returnFunction) : base(DbType.Double, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalDoubleParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalDoubleParameterConfig(double? value) : base(DbType.Double, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalDecimalParameterConfig : DbCommandParameterGenericConfig<decimal?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalDecimalParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalDecimalParameterConfig(Func<TDbParams, decimal?> returnFunction) : base(DbType.Decimal, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalDecimalParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalDecimalParameterConfig(decimal? value) : base(DbType.Decimal, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalBooleanParameterConfig : DbCommandParameterGenericConfig<bool?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalBooleanParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalBooleanParameterConfig(Func<TDbParams, bool?> returnFunction) : base(DbType.Boolean, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalBooleanParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalBooleanParameterConfig(bool? value) : base(DbType.Boolean, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalStringFixedLengthParameterConfig : DbCommandParameterGenericConfig<char?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalStringFixedLengthParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalStringFixedLengthParameterConfig(Func<TDbParams, char?> returnFunction) : base(DbType.StringFixedLength, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalStringFixedLengthParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalStringFixedLengthParameterConfig(char? value) : base(DbType.StringFixedLength, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalGuidParameterConfig : DbCommandParameterGenericConfig<Guid?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalGuidParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalGuidParameterConfig(Func<TDbParams, Guid?> returnFunction) : base(DbType.Guid, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalGuidParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalGuidParameterConfig(Guid? value) : base(DbType.Guid, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalDateTimeParameterConfig : DbCommandParameterGenericConfig<DateTime?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalDateTimeParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalDateTimeParameterConfig(Func<TDbParams, DateTime?> returnFunction) : base(DbType.DateTime, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalDateTimeParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalDateTimeParameterConfig(DateTime? value) : base(DbType.DateTime, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalDateTimeOffsetParameterConfig : DbCommandParameterGenericConfig<DateTimeOffset?>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalDateTimeOffsetParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalDateTimeOffsetParameterConfig(Func<TDbParams, DateTimeOffset?> returnFunction) : base(DbType.DateTimeOffset, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalDateTimeOffsetParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalDateTimeOffsetParameterConfig(DateTimeOffset? value) : base(DbType.DateTimeOffset, value) { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.CommandConfig.Parameters.DbCommandParameterConfigBase{TDbParams}" />
        protected sealed class OptionalBinaryParameterConfig : DbCommandParameterGenericConfig<byte?[]>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalBinaryParameterConfig"/> class.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            public OptionalBinaryParameterConfig(Func<TDbParams, byte?[]> returnFunction) : base(DbType.Binary, returnFunction) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="OptionalBinaryParameterConfig"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public OptionalBinaryParameterConfig(byte?[] value) : base(DbType.Binary, value) { }
        }

    }
}
