
using DBFacade.DataLayer.Models;
using System;
using System.Web.UI.WebControls;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    internal class DbCommandParameterConfigFactory<TDbParams> : IDbCommandParameterConfigFactory<TDbParams>
        where TDbParams : IDbParamsModel
    {
        private DbCommandParameterConfig<TDbParams> Base = new DbCommandParameterConfig<TDbParams>();
        #region Non Nullable
        #region Lamda
        public IDbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte> returnFunction) => Base.Byte(returnFunction);
        public IDbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte> returnFunction) => Base.SByte(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short> returnFunction) => Base.Int16(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int> returnFunction) => Base.Int32(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long> returnFunction) => Base.Int64(returnFunction);
        public IDbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort> returnFunction) => Base.UInt16(returnFunction);
        public IDbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint> returnFunction) => Base.UInt32(returnFunction);
        public IDbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong> returnFunction) => Base.UInt64(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float> returnFunction) => Base.Single(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double> returnFunction) => Base.Double(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal> returnFunction) => Base.Decimal(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool> returnFunction) => Base.Boolean(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Char(Func<TDbParams, char> returnFunction) => Base.Char(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid> returnFunction) => Base.Guid(returnFunction);
        public IDbCommandParameterConfig<TDbParams> TimeSpan(Func<TDbParams, TimeSpan> returnFunction) => Base.TimeSpan(returnFunction);
        public IDbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime> returnFunction) => Base.DateTime(returnFunction);
        public IDbCommandParameterConfig<TDbParams> DateTime2(Func<TDbParams, DateTime> returnFunction) => Base.DateTime2(returnFunction);
        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset> returnFunction) => Base.DateTimeOffset(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte[]> returnFunction) => Base.Binary(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Xml(Func<TDbParams, Xml> returnFunction) => Base.Xml(returnFunction);
        #endregion
        #region Hard Coded
        public IDbCommandParameterConfig<TDbParams> Byte(byte value) => Base.Byte(value);
        public IDbCommandParameterConfig<TDbParams> SByte(sbyte value) => Base.SByte(value);
        public IDbCommandParameterConfig<TDbParams> Int16(short value) => Base.Int16(value);
        public IDbCommandParameterConfig<TDbParams> Int32(int value) => Base.Int32(value);
        public IDbCommandParameterConfig<TDbParams> Int64(long value) => Base.Int64(value);
        public IDbCommandParameterConfig<TDbParams> UInt16(ushort value) => Base.UInt16(value);
        public IDbCommandParameterConfig<TDbParams> UInt32(uint value) => Base.UInt32(value);
        public IDbCommandParameterConfig<TDbParams> UInt64(ulong value) => Base.UInt64(value);
        public IDbCommandParameterConfig<TDbParams> Single(float value) => Base.Single(value);
        public IDbCommandParameterConfig<TDbParams> Double(double value) => Base.Double(value);
        public IDbCommandParameterConfig<TDbParams> Decimal(decimal value) => Base.Decimal(value);
        public IDbCommandParameterConfig<TDbParams> Boolean(bool value) => Base.Boolean(value);
        public IDbCommandParameterConfig<TDbParams> Guid(Guid value) => Base.Guid(value);
        public IDbCommandParameterConfig<TDbParams> TimeSpan(TimeSpan value) => Base.TimeSpan(value);
        public IDbCommandParameterConfig<TDbParams> DateTime(DateTime value) => Base.DateTime(value);
        public IDbCommandParameterConfig<TDbParams> DateTime2(DateTime value) => Base.DateTime2(value);
        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset value) => Base.DateTimeOffset(value);
        public IDbCommandParameterConfig<TDbParams> Binary(byte[] value) => Base.Binary(value);
        public IDbCommandParameterConfig<TDbParams> Char(char value) => Base.Char(value);
        public IDbCommandParameterConfig<TDbParams> String(string value) => Base.String(value);
        public IDbCommandParameterConfig<TDbParams> CharArray(char[] value) => Base.CharArray(value);
        public IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(string value) => Base.AnsiStringFixedLength(value);
        public IDbCommandParameterConfig<TDbParams> AnsiString(string value) => Base.AnsiString(value);
        public IDbCommandParameterConfig<TDbParams> Xml(Xml value) => Base.Xml(value);

        #endregion
        #endregion

        #region Nullable
        #region Lamda
        public IDbCommandParameterConfig<TDbParams> String(Func<TDbParams, string> returnFunction, bool isNullable = true) => Base.String(returnFunction, isNullable);
        public IDbCommandParameterConfig<TDbParams> CharArray(Func<TDbParams, char[]> returnFunction, bool isNullable = true) => Base.CharArray(returnFunction, isNullable);
        public IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(Func<TDbParams, string> returnFunction, bool isNullable = true) => Base.AnsiStringFixedLength(returnFunction, isNullable);
        public IDbCommandParameterConfig<TDbParams> AnsiString(Func<TDbParams, string> returnFunction, bool isNullable = true) => Base.AnsiString(returnFunction, isNullable);

        public IDbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte?> returnFunction) => Base.Byte(returnFunction);
        public IDbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte?> returnFunction) => Base.SByte(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short?> returnFunction) => Base.Int16(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int?> returnFunction) => Base.Int32(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long?> returnFunction) => Base.Int64(returnFunction);
        public IDbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort?> returnFunction) => Base.UInt16(returnFunction);
        public IDbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint?> returnFunction) => Base.UInt32(returnFunction);
        public IDbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong?> returnFunction) => Base.UInt64(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float?> returnFunction) => Base.Single(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double?> returnFunction) => Base.Double(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal?> returnFunction) => Base.Decimal(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool?> returnFunction) => Base.Boolean(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Char(Func<TDbParams, char?> returnFunction) => Base.Char(returnFunction);
        public IDbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid?> returnFunction) => Base.Guid(returnFunction);
        public IDbCommandParameterConfig<TDbParams> TimeSpan(Func<TDbParams, TimeSpan?> returnFunction) => Base.TimeSpan(returnFunction);
        public IDbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime?> returnFunction) => Base.DateTime(returnFunction);
        public IDbCommandParameterConfig<TDbParams> DateTime2(Func<TDbParams, DateTime?> returnFunction) => Base.DateTime2(returnFunction);
        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset?> returnFunction) => Base.DateTimeOffset(returnFunction);

        #endregion
        #region Hard Coded
        public IDbCommandParameterConfig<TDbParams> Byte(byte? value) => Base.Byte(value);
        public IDbCommandParameterConfig<TDbParams> SByte(sbyte? value) => Base.SByte(value);
        public IDbCommandParameterConfig<TDbParams> Int16(short? value) => Base.Int16(value);
        public IDbCommandParameterConfig<TDbParams> Int32(int? value) => Base.Int32(value);
        public IDbCommandParameterConfig<TDbParams> Int64(long? value) => Base.Int64(value);
        public IDbCommandParameterConfig<TDbParams> UInt16(ushort? value) => Base.UInt16(value);
        public IDbCommandParameterConfig<TDbParams> UInt32(uint? value) => Base.UInt32(value);
        public IDbCommandParameterConfig<TDbParams> UInt64(ulong? value) => Base.UInt64(value);
        public IDbCommandParameterConfig<TDbParams> Single(float? value) => Base.Single(value);
        public IDbCommandParameterConfig<TDbParams> Double(double? value) => Base.Double(value);
        public IDbCommandParameterConfig<TDbParams> Decimal(decimal? value) => Base.Decimal(value);
        public IDbCommandParameterConfig<TDbParams> Boolean(bool? value) => Base.Boolean(value);
        public IDbCommandParameterConfig<TDbParams> StringFixedLength(char? value) => Base.StringFixedLength(value);
        public IDbCommandParameterConfig<TDbParams> Guid(Guid? value) => Base.Guid(value);
        public IDbCommandParameterConfig<TDbParams> DateTime(DateTime? value) => Base.DateTime(value);
        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset? value) => Base.DateTimeOffset(value);
        #endregion
        #endregion
    }
}
