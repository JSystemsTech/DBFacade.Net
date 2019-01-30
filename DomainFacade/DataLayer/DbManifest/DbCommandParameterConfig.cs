using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;
using System;
using System.Data;

namespace DomainFacade.DataLayer.DbManifest
{
    public abstract class DbCommandParameterConfigBase<T> where T : IDbParamsModel
    {
        
        public DbType DBType { get; private set; }
        
        
        protected DbCommandParameterConfigBase(DbType dbType)
        {
            DBType = dbType;
        }

        public virtual object GetParam(T model)
        {
            return default(object);
        }

    }    
    
    public class DbCommandParameterConfig<T> : DbCommandParameterConfigBase<T> where T : IDbParamsModel
    {     

        protected DbCommandParameterConfig(DbType dbType) : base(dbType){}
        public bool IsNullable { get; private set; }
        public DbCommandParameterConfig<T> SetIsNullable() { IsNullable = true; return this; }
        protected class DbCommandParameterGenericConfig<N> : DbCommandParameterConfig<T>
        {
            public Func<T, N> ReturnFunction { get; private set; }

            public DbCommandParameterGenericConfig(DbType dbType, Func<T, N> returnFunction) : base(dbType)
            {
                this.ReturnFunction = returnFunction;
            }
            public override object GetParam(T model)
            {
                return ReturnFunction(model);
            }
        }
        protected class StringWithMaxParameterConfig : DbCommandParameterGenericConfig<string>
        {
            private int Max;
            public StringWithMaxParameterConfig(int max, Func<T, string> returnFunction) : base(DbType.String, returnFunction)
            {
                Max = max;
            }
            public override object GetParam(T model)
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
        
        public static DbCommandParameterConfig<T> VarChar(int max, Func<T, string> returnFunction)
        {
            return new VarCharParameterConfig(max, returnFunction);
        }
        
        public static Func<Func<T, byte>, DbCommandParameterConfig<T>> Byte = returnFunction => new ByteParameterConfig(returnFunction);
        public static Func<Func<T, sbyte>, DbCommandParameterConfig<T>> SByte = returnFunction => new SByteParameterConfig(returnFunction);
        public static Func<Func<T, string>, DbCommandParameterConfig<T>> String = returnFunction => new StringParameterConfig(returnFunction);
        public static Func<Func<T, short>, DbCommandParameterConfig<T>> Int16 = returnFunction => new Int16ParameterConfig(returnFunction);
        public static Func<Func<T, int>, DbCommandParameterConfig<T>>  Int32  = returnFunction => new Int32ParameterConfig(returnFunction);
        public static Func<Func<T, long>, DbCommandParameterConfig<T>> Int64 = returnFunction => new Int64ParameterConfig(returnFunction);
        public static Func<Func<T, ushort>, DbCommandParameterConfig<T>> UInt16 = returnFunction => new UInt16ParameterConfig(returnFunction);
        public static Func<Func<T, uint>, DbCommandParameterConfig<T>> UInt32 = returnFunction => new UInt32ParameterConfig(returnFunction);
        public static Func<Func<T, ulong>, DbCommandParameterConfig<T>> UInt64 = returnFunction => new UInt64ParameterConfig(returnFunction);
        public static Func<Func<T, float>, DbCommandParameterConfig<T>> Single = returnFunction => new SingleParameterConfig(returnFunction);
        public static Func<Func<T, double>, DbCommandParameterConfig<T>> Double = returnFunction => new DoubleParameterConfig(returnFunction);
        public static Func<Func<T, decimal>, DbCommandParameterConfig<T>> Decimal = returnFunction => new DecimalParameterConfig(returnFunction);
        public static Func<Func<T, bool>, DbCommandParameterConfig<T>> Boolean = returnFunction => new BooleanParameterConfig(returnFunction);
        public static Func<Func<T, char>, DbCommandParameterConfig<T>> StringFixedLength = returnFunction => new StringFixedLengthParameterConfig(returnFunction);
        public static Func<Func<T, Guid>, DbCommandParameterConfig<T>> Guid = returnFunction => new GuidParameterConfig(returnFunction);
        public static Func<Func<T, DateTime>, DbCommandParameterConfig<T>> DateTime = returnFunction => new DateTimeParameterConfig(returnFunction);
        public static Func<Func<T, DateTimeOffset>, DbCommandParameterConfig<T>> DateTimeOffset = returnFunction => new DateTimeOffsetParameterConfig(returnFunction);
        public static Func<Func<T, byte[]>, DbCommandParameterConfig<T>> Binary = returnFunction => new BinaryParameterConfig(returnFunction);


        protected sealed class ByteParameterConfig : DbCommandParameterGenericConfig<byte>
        {
            public ByteParameterConfig(Func<T, byte> returnFunction) : base(DbType.Byte, returnFunction) { }
        }
        protected sealed class SByteParameterConfig : DbCommandParameterGenericConfig<sbyte>
        {
            public SByteParameterConfig(Func<T, sbyte> returnFunction) : base(DbType.SByte, returnFunction) { }
        }
        protected sealed class StringParameterConfig : DbCommandParameterGenericConfig<string>
        {
            public StringParameterConfig(Func<T, string> returnFunction) : base(DbType.String, returnFunction) { }           
        }        
        protected sealed class VarCharParameterConfig : StringWithMaxParameterConfig
        {
            public VarCharParameterConfig(int max, Func<T, string> returnFunction) : base(max, returnFunction) { }
        }
        protected sealed class Int16ParameterConfig : DbCommandParameterGenericConfig<short>
        {
            public Int16ParameterConfig(Func<T, short> returnFunction) : base(DbType.Int16, returnFunction) { }
        }
        protected sealed class Int32ParameterConfig : DbCommandParameterGenericConfig<int>
        {
            public Int32ParameterConfig(Func<T, int> returnFunction) : base(DbType.Int32, returnFunction) { }
        }
        protected sealed class Int64ParameterConfig : DbCommandParameterGenericConfig<long>
        {
            public Int64ParameterConfig(Func<T, long> returnFunction) : base(DbType.Int64, returnFunction) { }
        }
        protected sealed class UInt16ParameterConfig : DbCommandParameterGenericConfig<ushort>
        {
            public UInt16ParameterConfig(Func<T, ushort> returnFunction) : base(DbType.UInt16, returnFunction) { }
        }
        protected sealed class UInt32ParameterConfig : DbCommandParameterGenericConfig<uint>
        {
            public UInt32ParameterConfig(Func<T, uint> returnFunction) : base(DbType.UInt32, returnFunction) { }
        }
        protected sealed class UInt64ParameterConfig : DbCommandParameterGenericConfig<ulong>
        {
            public UInt64ParameterConfig(Func<T, ulong> returnFunction) : base(DbType.UInt64, returnFunction) { }
        }
        protected sealed class SingleParameterConfig : DbCommandParameterGenericConfig<float>
        {
            public SingleParameterConfig(Func<T, float> returnFunction) : base(DbType.Single, returnFunction) { }
        }
        protected sealed class DoubleParameterConfig : DbCommandParameterGenericConfig<double>
        {
            public DoubleParameterConfig(Func<T, double> returnFunction) : base(DbType.Double, returnFunction) { }
        }
        protected sealed class DecimalParameterConfig : DbCommandParameterGenericConfig<decimal>
        {
            public DecimalParameterConfig(Func<T, decimal> returnFunction) : base(DbType.Decimal, returnFunction) { }
        }
        protected sealed class BooleanParameterConfig : DbCommandParameterGenericConfig<bool>
        {
            public BooleanParameterConfig(Func<T, bool> returnFunction) : base(DbType.Boolean, returnFunction) { }
        }
        protected sealed class StringFixedLengthParameterConfig : DbCommandParameterGenericConfig<char>
        {
            public StringFixedLengthParameterConfig(Func<T, char> returnFunction) : base(DbType.StringFixedLength, returnFunction) { }
        }
        protected sealed class GuidParameterConfig : DbCommandParameterGenericConfig<Guid>
        {
            public GuidParameterConfig(Func<T, Guid> returnFunction) : base(DbType.Guid, returnFunction) { }
        }
        protected sealed class DateTimeParameterConfig : DbCommandParameterGenericConfig<DateTime>
        {
            public DateTimeParameterConfig(Func<T, DateTime> returnFunction) : base(DbType.DateTime, returnFunction) { }
        }
        protected sealed class DateTimeOffsetParameterConfig : DbCommandParameterGenericConfig<DateTimeOffset>
        {
            public DateTimeOffsetParameterConfig(Func<T, DateTimeOffset> returnFunction) : base(DbType.DateTimeOffset, returnFunction) { }
        }
        protected sealed class BinaryParameterConfig : DbCommandParameterGenericConfig<byte[]>
        {
            public BinaryParameterConfig(Func<T, byte[]> returnFunction) : base(DbType.Binary, returnFunction) { }
        }

    }
    
    
    
}
