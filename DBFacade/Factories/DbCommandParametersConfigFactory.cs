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

        public static IDbCommandParameterConfig<DbParamsModel> OutputByte(int size) => ConfigBuilder.OutputByte(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputSByte(int size) => ConfigBuilder.OutputSByte(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputInt16(int size) => ConfigBuilder.OutputInt16(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputInt32(int size) => ConfigBuilder.OutputInt32(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputInt64(int size) => ConfigBuilder.OutputInt64(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputUInt16(int size) => ConfigBuilder.OutputUInt16(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputUInt32(int size) => ConfigBuilder.OutputUInt32(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputUInt64(int size) => ConfigBuilder.OutputUInt64(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputSingle(int size) => ConfigBuilder.OutputSingle(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDouble(int size) => ConfigBuilder.OutputDouble(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDecimal(int size) => ConfigBuilder.OutputDecimal(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputBoolean(int size) => ConfigBuilder.OutputBoolean(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputGuid(int size) => ConfigBuilder.OutputGuid(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputTimeSpan(int size) => ConfigBuilder.OutputTimeSpan(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDateTime(int size) => ConfigBuilder.OutputDateTime(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDateTime2(int size) => ConfigBuilder.OutputDateTime2(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDateTimeOffset(int size) => ConfigBuilder.OutputDateTimeOffset(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputBinary(int size) => ConfigBuilder.OutputBinary(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputChar(int size) => ConfigBuilder.OutputChar(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputString(int size) => ConfigBuilder.OutputString(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputCharArray(int size) => ConfigBuilder.OutputCharArray(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputAnsiStringFixedLength(int size) => ConfigBuilder.OutputAnsiStringFixedLength(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputAnsiString(int size) => ConfigBuilder.OutputAnsiString(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputXml(int size) => ConfigBuilder.OutputXml(size);


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

        public IDbCommandParameterConfig<TDbParams> OutputByte(int size) => ConfigBuilder.OutputByte(size);
        public IDbCommandParameterConfig<TDbParams> OutputSByte(int size) => ConfigBuilder.OutputSByte(size);
        public IDbCommandParameterConfig<TDbParams> OutputInt16(int size) => ConfigBuilder.OutputInt16(size);
        public IDbCommandParameterConfig<TDbParams> OutputInt32(int size) => ConfigBuilder.OutputInt32(size);
        public IDbCommandParameterConfig<TDbParams> OutputInt64(int size) => ConfigBuilder.OutputInt64(size);
        public IDbCommandParameterConfig<TDbParams> OutputUInt16(int size) => ConfigBuilder.OutputUInt16(size);
        public IDbCommandParameterConfig<TDbParams> OutputUInt32(int size) => ConfigBuilder.OutputUInt32(size);
        public IDbCommandParameterConfig<TDbParams> OutputUInt64(int size) => ConfigBuilder.OutputUInt64(size);
        public IDbCommandParameterConfig<TDbParams> OutputSingle(int size) => ConfigBuilder.OutputSingle(size);
        public IDbCommandParameterConfig<TDbParams> OutputDouble(int size) => ConfigBuilder.OutputDouble(size);
        public IDbCommandParameterConfig<TDbParams> OutputDecimal(int size) => ConfigBuilder.OutputDecimal(size);
        public IDbCommandParameterConfig<TDbParams> OutputBoolean(int size) => ConfigBuilder.OutputBoolean(size);
        public IDbCommandParameterConfig<TDbParams> OutputGuid(int size)  => ConfigBuilder.OutputGuid(size);
        public IDbCommandParameterConfig<TDbParams> OutputTimeSpan(int size) =>ConfigBuilder.OutputTimeSpan(size);
        public IDbCommandParameterConfig<TDbParams> OutputDateTime(int size) => ConfigBuilder.OutputDateTime(size);
        public IDbCommandParameterConfig<TDbParams> OutputDateTime2(int size) => ConfigBuilder.OutputDateTime2(size);
        public IDbCommandParameterConfig<TDbParams> OutputDateTimeOffset(int size) => ConfigBuilder.OutputDateTimeOffset(size);
        public IDbCommandParameterConfig<TDbParams> OutputBinary(int size) => ConfigBuilder.OutputBinary(size);
        public IDbCommandParameterConfig<TDbParams> OutputChar(int size)   => ConfigBuilder.OutputChar(size);
        public IDbCommandParameterConfig<TDbParams> OutputString(int size) => ConfigBuilder.OutputString(size);
        public IDbCommandParameterConfig<TDbParams> OutputCharArray(int size)  => ConfigBuilder.OutputCharArray(size);
        public IDbCommandParameterConfig<TDbParams> OutputAnsiStringFixedLength(int size) => ConfigBuilder.OutputAnsiStringFixedLength(size);
        public IDbCommandParameterConfig<TDbParams> OutputAnsiString(int size) => ConfigBuilder.OutputAnsiString(size);
        public IDbCommandParameterConfig<TDbParams> OutputXml(int size) => ConfigBuilder.OutputXml(size);


        #endregion
    }
}