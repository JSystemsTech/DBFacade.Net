using DBFacade.DataLayer.Models;
using System;
using System.Web.UI.WebControls;

namespace DBFacade.DataLayer.CommandConfig.Parameters
{
    public interface IDbCommandParameterConfigFactory<TDbParams> where TDbParams : IDbParamsModel
    {
        #region Non Nullable
        #region Lamda
        IDbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte> returnFunction);
        IDbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong> returnFunction);
        IDbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float> returnFunction);
        IDbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double> returnFunction);
        IDbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal> returnFunction);
        IDbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool> returnFunction);
        IDbCommandParameterConfig<TDbParams> Char(Func<TDbParams, char> returnFunction);
        IDbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid> returnFunction);
        IDbCommandParameterConfig<TDbParams> TimeSpan(Func<TDbParams, TimeSpan> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTime2(Func<TDbParams, DateTime> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset> returnFunction);
        IDbCommandParameterConfig<TDbParams> Binary(Func<TDbParams, byte[]> returnFunction);
        IDbCommandParameterConfig<TDbParams> Xml(Func<TDbParams, Xml> returnFunction);
        #endregion
        #region Hard Coded
        IDbCommandParameterConfig<TDbParams> Byte(byte value);
        IDbCommandParameterConfig<TDbParams> SByte(sbyte value);
        IDbCommandParameterConfig<TDbParams> Int16(short value);
        IDbCommandParameterConfig<TDbParams> Int32(int value);
        IDbCommandParameterConfig<TDbParams> Int64(long value);
        IDbCommandParameterConfig<TDbParams> UInt16(ushort value);
        IDbCommandParameterConfig<TDbParams> UInt32(uint value);
        IDbCommandParameterConfig<TDbParams> UInt64(ulong value);
        IDbCommandParameterConfig<TDbParams> Single(float value);
        IDbCommandParameterConfig<TDbParams> Double(double value);
        IDbCommandParameterConfig<TDbParams> Decimal(decimal value);
        IDbCommandParameterConfig<TDbParams> Boolean(bool value);
        IDbCommandParameterConfig<TDbParams> Guid(Guid value);
        IDbCommandParameterConfig<TDbParams> TimeSpan(TimeSpan value);
        IDbCommandParameterConfig<TDbParams> DateTime(DateTime value);
        IDbCommandParameterConfig<TDbParams> DateTime2(DateTime value);
        IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset value);
        IDbCommandParameterConfig<TDbParams> Binary(byte[] value);
        IDbCommandParameterConfig<TDbParams> Char(char value);
        IDbCommandParameterConfig<TDbParams> String(string value);
        IDbCommandParameterConfig<TDbParams> CharArray(char[] value);
        IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(string value);
        IDbCommandParameterConfig<TDbParams> AnsiString(string value);
        IDbCommandParameterConfig<TDbParams> Xml(Xml value);

        #endregion
        #endregion

        #region Nullable
        #region Lamda
        IDbCommandParameterConfig<TDbParams> String(Func<TDbParams, string> returnFunction, bool isNullable = true);
        IDbCommandParameterConfig<TDbParams> CharArray(Func<TDbParams, char[]> returnFunction, bool isNullable = true);
        IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(Func<TDbParams, string> returnFunction, bool isNullable = true);
        IDbCommandParameterConfig<TDbParams> AnsiString(Func<TDbParams, string> returnFunction, bool isNullable = true);


        IDbCommandParameterConfig<TDbParams> Byte(Func<TDbParams, byte?> returnFunction);
        IDbCommandParameterConfig<TDbParams> SByte(Func<TDbParams, sbyte?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int16(Func<TDbParams, short?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int32(Func<TDbParams, int?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int64(Func<TDbParams, long?> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt16(Func<TDbParams, ushort?> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt32(Func<TDbParams, uint?> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt64(Func<TDbParams, ulong?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Single(Func<TDbParams, float?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Double(Func<TDbParams, double?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Decimal(Func<TDbParams, decimal?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Boolean(Func<TDbParams, bool?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Char(Func<TDbParams, char?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Guid(Func<TDbParams, Guid?> returnFunction);
        IDbCommandParameterConfig<TDbParams> TimeSpan(Func<TDbParams, TimeSpan?> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTime(Func<TDbParams, DateTime?> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTime2(Func<TDbParams, DateTime?> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTimeOffset(Func<TDbParams, DateTimeOffset?> returnFunction);
        #endregion
        #region Hard Coded
        IDbCommandParameterConfig<TDbParams> Byte(byte? value);
        IDbCommandParameterConfig<TDbParams> SByte(sbyte? value);
        IDbCommandParameterConfig<TDbParams> Int16(short? value);
        IDbCommandParameterConfig<TDbParams> Int32(int? value);
        IDbCommandParameterConfig<TDbParams> Int64(long? value);
        IDbCommandParameterConfig<TDbParams> UInt16(ushort? value);
        IDbCommandParameterConfig<TDbParams> UInt32(uint? value);
        IDbCommandParameterConfig<TDbParams> UInt64(ulong? value);
        IDbCommandParameterConfig<TDbParams> Single(float? value);
        IDbCommandParameterConfig<TDbParams> Double(double? value);
        IDbCommandParameterConfig<TDbParams> Decimal(decimal? value);
        IDbCommandParameterConfig<TDbParams> Boolean(bool? value);
        IDbCommandParameterConfig<TDbParams> StringFixedLength(char? value);
        IDbCommandParameterConfig<TDbParams> Guid(Guid? value);
        IDbCommandParameterConfig<TDbParams> DateTime(DateTime? value);
        IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset? value);
        #endregion
        #endregion
    }
}
