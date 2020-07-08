using System;
using System.Web.UI.WebControls;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Models;

namespace DBFacade.Factories
{
    public static class DbCommandParameterConfigFactory
    {
        private static DbCommandParameterConfigFactory<DbParamsModel> ConfigBuilder = new DbCommandParameterConfigFactory<DbParamsModel>();
        #region Non Nullable
        

        public static IDbCommandParameterConfig<DbParamsModel> Byte(byte value)
        {
            return ConfigBuilder.Byte(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> SByte(sbyte value)
        {
            return ConfigBuilder.SByte(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Int16(short value)
        {
            return ConfigBuilder.Int16(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Int32(int value)
        {
            return ConfigBuilder.Int32(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Int64(long value)
        {
            return ConfigBuilder.Int64(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> UInt16(ushort value)
        {
            return ConfigBuilder.UInt16(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> UInt32(uint value)
        {
            return ConfigBuilder.UInt32(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> UInt64(ulong value)
        {
            return ConfigBuilder.UInt64(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Single(float value)
        {
            return ConfigBuilder.Single(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Double(double value)
        {
            return ConfigBuilder.Double(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Decimal(decimal value)
        {
            return ConfigBuilder.Decimal(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Boolean(bool value)
        {
            return ConfigBuilder.Boolean(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Guid(Guid value)
        {
            return ConfigBuilder.Guid(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> TimeSpan(TimeSpan value)
        {
            return ConfigBuilder.TimeSpan(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> DateTime(DateTime value)
        {
            return ConfigBuilder.DateTime(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> DateTime2(DateTime value)
        {
            return ConfigBuilder.DateTime2(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> DateTimeOffset(DateTimeOffset value)
        {
            return ConfigBuilder.DateTimeOffset(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Binary(byte[] value)
        {
            return ConfigBuilder.Binary(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Char(char value)
        {
            return ConfigBuilder.Char(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> String(string value)
        {
            return ConfigBuilder.String(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> CharArray(char[] value)
        {
            return ConfigBuilder.CharArray(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> AnsiStringFixedLength(string value)
        {
            return ConfigBuilder.AnsiStringFixedLength(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> AnsiString(string value)
        {
            return ConfigBuilder.AnsiString(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Xml(Xml value)
        {
            return ConfigBuilder.Xml(value);
        }

        #endregion

        #region Nullable

        public static IDbCommandParameterConfig<DbParamsModel> Byte(byte? value)
        {
            return ConfigBuilder.Byte(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> SByte(sbyte? value)
        {
            return ConfigBuilder.SByte(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Int16(short? value)
        {
            return ConfigBuilder.Int16(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Int32(int? value)
        {
            return ConfigBuilder.Int32(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Int64(long? value)
        {
            return ConfigBuilder.Int64(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> UInt16(ushort? value)
        {
            return ConfigBuilder.UInt16(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> UInt32(uint? value)
        {
            return ConfigBuilder.UInt32(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> UInt64(ulong? value)
        {
            return ConfigBuilder.UInt64(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Single(float? value)
        {
            return ConfigBuilder.Single(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Double(double? value)
        {
            return ConfigBuilder.Double(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Decimal(decimal? value)
        {
            return ConfigBuilder.Decimal(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Boolean(bool? value)
        {
            return ConfigBuilder.Boolean(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> StringFixedLength(char? value)
        {
            return ConfigBuilder.StringFixedLength(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> Guid(Guid? value)
        {
            return ConfigBuilder.Guid(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> DateTime(DateTime? value)
        {
            return ConfigBuilder.DateTime(value);
        }

        public static IDbCommandParameterConfig<DbParamsModel> DateTimeOffset(DateTimeOffset? value)
        {
            return ConfigBuilder.DateTimeOffset(value);
        }

        #endregion

        #region Output

        public static IDbCommandParameterConfig<DbParamsModel> OutputByte => ConfigBuilder.OutputByte();
        public static IDbCommandParameterConfig<DbParamsModel> OutputSByte => ConfigBuilder.OutputSByte();
        public static IDbCommandParameterConfig<DbParamsModel> OutputInt16 => ConfigBuilder.OutputInt16();
        public static IDbCommandParameterConfig<DbParamsModel> OutputInt32 => ConfigBuilder.OutputInt32();
        public static IDbCommandParameterConfig<DbParamsModel> OutputInt64 => ConfigBuilder.OutputInt64();
        public static IDbCommandParameterConfig<DbParamsModel> OutputUInt16 => ConfigBuilder.OutputUInt16();
        public static IDbCommandParameterConfig<DbParamsModel> OutputUInt32 => ConfigBuilder.OutputUInt32();
        public static IDbCommandParameterConfig<DbParamsModel> OutputUInt64 => ConfigBuilder.OutputUInt64();
        public static IDbCommandParameterConfig<DbParamsModel> OutputSingle => ConfigBuilder.OutputSingle();
        public static IDbCommandParameterConfig<DbParamsModel> OutputDouble => ConfigBuilder.OutputDouble();
        public static IDbCommandParameterConfig<DbParamsModel> OutputDecimal => ConfigBuilder.OutputDecimal();
        public static IDbCommandParameterConfig<DbParamsModel> OutputBoolean => ConfigBuilder.OutputBoolean();
        public static IDbCommandParameterConfig<DbParamsModel> OutputGuid => ConfigBuilder.OutputGuid();
        public static IDbCommandParameterConfig<DbParamsModel> OutputTimeSpan => ConfigBuilder.OutputTimeSpan();
        public static IDbCommandParameterConfig<DbParamsModel> OutputDateTime => ConfigBuilder.OutputDateTime();
        public static IDbCommandParameterConfig<DbParamsModel> OutputDateTime2 => ConfigBuilder.OutputDateTime2();
        public static IDbCommandParameterConfig<DbParamsModel> OutputDateTimeOffset => ConfigBuilder.OutputDateTimeOffset();
        public static IDbCommandParameterConfig<DbParamsModel> OutputBinary => ConfigBuilder.OutputBinary();
        public static IDbCommandParameterConfig<DbParamsModel> OutputChar => ConfigBuilder.OutputChar();
        public static IDbCommandParameterConfig<DbParamsModel> OutputString => ConfigBuilder.OutputString();
        public static IDbCommandParameterConfig<DbParamsModel> OutputCharArray => ConfigBuilder.OutputCharArray();
        public static IDbCommandParameterConfig<DbParamsModel> OutputAnsiStringFixedLength => ConfigBuilder.OutputAnsiStringFixedLength();
        public static IDbCommandParameterConfig<DbParamsModel> OutputAnsiString => ConfigBuilder.OutputAnsiString();
        public static IDbCommandParameterConfig<DbParamsModel> OutputXml => ConfigBuilder.OutputXml();


        #endregion

    }
    internal class DbCommandParameterConfigFactory<TDbParams> : IDbCommandParameterConfigFactory<TDbParams>
        where TDbParams : IDbParamsModel
    {
        private readonly DbCommandParameterConfig<TDbParams> ConfigBuilder = new DbCommandParameterConfig<TDbParams>();

        #region Non Nullable

        #region Lamda

        public IDbCommandParameterConfig<TDbParams> Byte(DelegateHandler<TDbParams, byte> returnFunction)
        {
            return ConfigBuilder.Byte(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> SByte(DelegateHandler<TDbParams, sbyte> returnFunction)
        {
            return ConfigBuilder.SByte(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int16(DelegateHandler<TDbParams, short> returnFunction)
        {
            return ConfigBuilder.Int16(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int32(DelegateHandler<TDbParams, int> returnFunction)
        {
            return ConfigBuilder.Int32(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int64(DelegateHandler<TDbParams, long> returnFunction)
        {
            return ConfigBuilder.Int64(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt16(DelegateHandler<TDbParams, ushort> returnFunction)
        {
            return ConfigBuilder.UInt16(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt32(DelegateHandler<TDbParams, uint> returnFunction)
        {
            return ConfigBuilder.UInt32(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt64(DelegateHandler<TDbParams, ulong> returnFunction)
        {
            return ConfigBuilder.UInt64(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Single(DelegateHandler<TDbParams, float> returnFunction)
        {
            return ConfigBuilder.Single(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Double(DelegateHandler<TDbParams, double> returnFunction)
        {
            return ConfigBuilder.Double(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Decimal(DelegateHandler<TDbParams, decimal> returnFunction)
        {
            return ConfigBuilder.Decimal(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Boolean(DelegateHandler<TDbParams, bool> returnFunction)
        {
            return ConfigBuilder.Boolean(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Char(DelegateHandler<TDbParams, char> returnFunction)
        {
            return ConfigBuilder.Char(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Guid(DelegateHandler<TDbParams, Guid> returnFunction)
        {
            return ConfigBuilder.Guid(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> TimeSpan(DelegateHandler<TDbParams, TimeSpan> returnFunction)
        {
            return ConfigBuilder.TimeSpan(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime(DelegateHandler<TDbParams, DateTime> returnFunction)
        {
            return ConfigBuilder.DateTime(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime> returnFunction)
        {
            return ConfigBuilder.DateTime2(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(
            DelegateHandler<TDbParams, DateTimeOffset> returnFunction)
        {
            return ConfigBuilder.DateTimeOffset(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Binary(DelegateHandler<TDbParams, byte[]> returnFunction)
        {
            return ConfigBuilder.Binary(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Xml(DelegateHandler<TDbParams, Xml> returnFunction)
        {
            return ConfigBuilder.Xml(returnFunction);
        }

        #endregion

        #region Hard Coded

        public IDbCommandParameterConfig<TDbParams> Byte(byte value)
        {
            return ConfigBuilder.Byte(value);
        }

        public IDbCommandParameterConfig<TDbParams> SByte(sbyte value)
        {
            return ConfigBuilder.SByte(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int16(short value)
        {
            return ConfigBuilder.Int16(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int32(int value)
        {
            return ConfigBuilder.Int32(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int64(long value)
        {
            return ConfigBuilder.Int64(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt16(ushort value)
        {
            return ConfigBuilder.UInt16(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt32(uint value)
        {
            return ConfigBuilder.UInt32(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt64(ulong value)
        {
            return ConfigBuilder.UInt64(value);
        }

        public IDbCommandParameterConfig<TDbParams> Single(float value)
        {
            return ConfigBuilder.Single(value);
        }

        public IDbCommandParameterConfig<TDbParams> Double(double value)
        {
            return ConfigBuilder.Double(value);
        }

        public IDbCommandParameterConfig<TDbParams> Decimal(decimal value)
        {
            return ConfigBuilder.Decimal(value);
        }

        public IDbCommandParameterConfig<TDbParams> Boolean(bool value)
        {
            return ConfigBuilder.Boolean(value);
        }

        public IDbCommandParameterConfig<TDbParams> Guid(Guid value)
        {
            return ConfigBuilder.Guid(value);
        }

        public IDbCommandParameterConfig<TDbParams> TimeSpan(TimeSpan value)
        {
            return ConfigBuilder.TimeSpan(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime(DateTime value)
        {
            return ConfigBuilder.DateTime(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime2(DateTime value)
        {
            return ConfigBuilder.DateTime2(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset value)
        {
            return ConfigBuilder.DateTimeOffset(value);
        }

        public IDbCommandParameterConfig<TDbParams> Binary(byte[] value)
        {
            return ConfigBuilder.Binary(value);
        }

        public IDbCommandParameterConfig<TDbParams> Char(char value)
        {
            return ConfigBuilder.Char(value);
        }

        public IDbCommandParameterConfig<TDbParams> String(string value)
        {
            return ConfigBuilder.String(value);
        }

        public IDbCommandParameterConfig<TDbParams> CharArray(char[] value)
        {
            return ConfigBuilder.CharArray(value);
        }

        public IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(string value)
        {
            return ConfigBuilder.AnsiStringFixedLength(value);
        }

        public IDbCommandParameterConfig<TDbParams> AnsiString(string value)
        {
            return ConfigBuilder.AnsiString(value);
        }

        public IDbCommandParameterConfig<TDbParams> Xml(Xml value)
        {
            return ConfigBuilder.Xml(value);
        }

        #endregion

        #endregion

        #region Nullable

        #region Lamda

        public IDbCommandParameterConfig<TDbParams> String(DelegateHandler<TDbParams, string> returnFunction,
            bool isNullable = true)
        {
            return ConfigBuilder.String(returnFunction, isNullable);
        }

        public IDbCommandParameterConfig<TDbParams> CharArray(DelegateHandler<TDbParams, char[]> returnFunction,
            bool isNullable = true)
        {
            return ConfigBuilder.CharArray(returnFunction, isNullable);
        }

        public IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(
            DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        {
            return ConfigBuilder.AnsiStringFixedLength(returnFunction, isNullable);
        }

        public IDbCommandParameterConfig<TDbParams> AnsiString(DelegateHandler<TDbParams, string> returnFunction,
            bool isNullable = true)
        {
            return ConfigBuilder.AnsiString(returnFunction, isNullable);
        }

        public IDbCommandParameterConfig<TDbParams> Byte(DelegateHandler<TDbParams, byte?> returnFunction)
        {
            return ConfigBuilder.Byte(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> SByte(DelegateHandler<TDbParams, sbyte?> returnFunction)
        {
            return ConfigBuilder.SByte(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int16(DelegateHandler<TDbParams, short?> returnFunction)
        {
            return ConfigBuilder.Int16(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int32(DelegateHandler<TDbParams, int?> returnFunction)
        {
            return ConfigBuilder.Int32(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int64(DelegateHandler<TDbParams, long?> returnFunction)
        {
            return ConfigBuilder.Int64(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt16(DelegateHandler<TDbParams, ushort?> returnFunction)
        {
            return ConfigBuilder.UInt16(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt32(DelegateHandler<TDbParams, uint?> returnFunction)
        {
            return ConfigBuilder.UInt32(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt64(DelegateHandler<TDbParams, ulong?> returnFunction)
        {
            return ConfigBuilder.UInt64(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Single(DelegateHandler<TDbParams, float?> returnFunction)
        {
            return ConfigBuilder.Single(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Double(DelegateHandler<TDbParams, double?> returnFunction)
        {
            return ConfigBuilder.Double(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Decimal(DelegateHandler<TDbParams, decimal?> returnFunction)
        {
            return ConfigBuilder.Decimal(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Boolean(DelegateHandler<TDbParams, bool?> returnFunction)
        {
            return ConfigBuilder.Boolean(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Char(DelegateHandler<TDbParams, char?> returnFunction)
        {
            return ConfigBuilder.Char(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Guid(DelegateHandler<TDbParams, Guid?> returnFunction)
        {
            return ConfigBuilder.Guid(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> TimeSpan(DelegateHandler<TDbParams, TimeSpan?> returnFunction)
        {
            return ConfigBuilder.TimeSpan(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime(DelegateHandler<TDbParams, DateTime?> returnFunction)
        {
            return ConfigBuilder.DateTime(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime?> returnFunction)
        {
            return ConfigBuilder.DateTime2(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(
            DelegateHandler<TDbParams, DateTimeOffset?> returnFunction)
        {
            return ConfigBuilder.DateTimeOffset(returnFunction);
        }

        #endregion

        #region Hard Coded

        public IDbCommandParameterConfig<TDbParams> Byte(byte? value)
        {
            return ConfigBuilder.Byte(value);
        }

        public IDbCommandParameterConfig<TDbParams> SByte(sbyte? value)
        {
            return ConfigBuilder.SByte(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int16(short? value)
        {
            return ConfigBuilder.Int16(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int32(int? value)
        {
            return ConfigBuilder.Int32(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int64(long? value)
        {
            return ConfigBuilder.Int64(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt16(ushort? value)
        {
            return ConfigBuilder.UInt16(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt32(uint? value)
        {
            return ConfigBuilder.UInt32(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt64(ulong? value)
        {
            return ConfigBuilder.UInt64(value);
        }

        public IDbCommandParameterConfig<TDbParams> Single(float? value)
        {
            return ConfigBuilder.Single(value);
        }

        public IDbCommandParameterConfig<TDbParams> Double(double? value)
        {
            return ConfigBuilder.Double(value);
        }

        public IDbCommandParameterConfig<TDbParams> Decimal(decimal? value)
        {
            return ConfigBuilder.Decimal(value);
        }

        public IDbCommandParameterConfig<TDbParams> Boolean(bool? value)
        {
            return ConfigBuilder.Boolean(value);
        }

        public IDbCommandParameterConfig<TDbParams> StringFixedLength(char? value)
        {
            return ConfigBuilder.StringFixedLength(value);
        }

        public IDbCommandParameterConfig<TDbParams> Guid(Guid? value)
        {
            return ConfigBuilder.Guid(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime(DateTime? value)
        {
            return ConfigBuilder.DateTime(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset? value)
        {
            return ConfigBuilder.DateTimeOffset(value);
        }

        #endregion

        #endregion

        #region Output

        public IDbCommandParameterConfig<TDbParams> OutputByte() => ConfigBuilder.OutputByte();
        public IDbCommandParameterConfig<TDbParams> OutputSByte() => ConfigBuilder.OutputSByte();
        public IDbCommandParameterConfig<TDbParams> OutputInt16() => ConfigBuilder.OutputInt16();
        public IDbCommandParameterConfig<TDbParams> OutputInt32() => ConfigBuilder.OutputInt32();
        public IDbCommandParameterConfig<TDbParams> OutputInt64() => ConfigBuilder.OutputInt64();
        public IDbCommandParameterConfig<TDbParams> OutputUInt16()=> ConfigBuilder.OutputUInt16();
        public IDbCommandParameterConfig<TDbParams> OutputUInt32()=> ConfigBuilder.OutputUInt32();
        public IDbCommandParameterConfig<TDbParams> OutputUInt64()=> ConfigBuilder.OutputUInt64();
        public IDbCommandParameterConfig<TDbParams> OutputSingle()=> ConfigBuilder.OutputSingle();
        public IDbCommandParameterConfig<TDbParams> OutputDouble()=> ConfigBuilder.OutputDouble();
        public IDbCommandParameterConfig<TDbParams> OutputDecimal()=> ConfigBuilder.OutputDecimal();
        public IDbCommandParameterConfig<TDbParams> OutputBoolean()=> ConfigBuilder.OutputBoolean();
        public IDbCommandParameterConfig<TDbParams> OutputGuid()  => ConfigBuilder.OutputGuid();
        public IDbCommandParameterConfig<TDbParams> OutputTimeSpan() =>ConfigBuilder.OutputTimeSpan();
        public IDbCommandParameterConfig<TDbParams> OutputDateTime()=> ConfigBuilder.OutputDateTime();
        public IDbCommandParameterConfig<TDbParams> OutputDateTime2()=> ConfigBuilder.OutputDateTime2();
        public IDbCommandParameterConfig<TDbParams> OutputDateTimeOffset()=> ConfigBuilder.OutputDateTimeOffset();
        public IDbCommandParameterConfig<TDbParams> OutputBinary() => ConfigBuilder.OutputBinary();
        public IDbCommandParameterConfig<TDbParams> OutputChar()   => ConfigBuilder.OutputChar();
        public IDbCommandParameterConfig<TDbParams> OutputString() => ConfigBuilder.OutputString();
        public IDbCommandParameterConfig<TDbParams> OutputCharArray()  => ConfigBuilder.OutputCharArray();
        public IDbCommandParameterConfig<TDbParams> OutputAnsiStringFixedLength() => ConfigBuilder.OutputAnsiStringFixedLength();
        public IDbCommandParameterConfig<TDbParams> OutputAnsiString() => ConfigBuilder.OutputAnsiString();
        public IDbCommandParameterConfig<TDbParams> OutputXml() => ConfigBuilder.OutputXml();


        #endregion
    }
}