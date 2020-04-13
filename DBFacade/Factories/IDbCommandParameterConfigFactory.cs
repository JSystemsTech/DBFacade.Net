using System;
using System.Web.UI.WebControls;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Models;

namespace DBFacade.Factories
{
    public delegate T DelegateHandler<TDbParams, T>(TDbParams value) where TDbParams : IDbParamsModel;

    public interface IDbCommandParameterConfigFactory<TDbParams> where TDbParams : IDbParamsModel
    {
        #region Non Nullable

        #region Lamda

        IDbCommandParameterConfig<TDbParams> Byte(DelegateHandler<TDbParams, byte> returnFunction);
        IDbCommandParameterConfig<TDbParams> SByte(DelegateHandler<TDbParams, sbyte> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int16(DelegateHandler<TDbParams, short> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int32(DelegateHandler<TDbParams, int> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int64(DelegateHandler<TDbParams, long> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt16(DelegateHandler<TDbParams, ushort> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt32(DelegateHandler<TDbParams, uint> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt64(DelegateHandler<TDbParams, ulong> returnFunction);
        IDbCommandParameterConfig<TDbParams> Single(DelegateHandler<TDbParams, float> returnFunction);
        IDbCommandParameterConfig<TDbParams> Double(DelegateHandler<TDbParams, double> returnFunction);
        IDbCommandParameterConfig<TDbParams> Decimal(DelegateHandler<TDbParams, decimal> returnFunction);
        IDbCommandParameterConfig<TDbParams> Boolean(DelegateHandler<TDbParams, bool> returnFunction);
        IDbCommandParameterConfig<TDbParams> Char(DelegateHandler<TDbParams, char> returnFunction);
        IDbCommandParameterConfig<TDbParams> Guid(DelegateHandler<TDbParams, Guid> returnFunction);
        IDbCommandParameterConfig<TDbParams> TimeSpan(DelegateHandler<TDbParams, TimeSpan> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTime(DelegateHandler<TDbParams, DateTime> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTimeOffset(DelegateHandler<TDbParams, DateTimeOffset> returnFunction);
        IDbCommandParameterConfig<TDbParams> Binary(DelegateHandler<TDbParams, byte[]> returnFunction);
        IDbCommandParameterConfig<TDbParams> Xml(DelegateHandler<TDbParams, Xml> returnFunction);

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

        IDbCommandParameterConfig<TDbParams> String(DelegateHandler<TDbParams, string> returnFunction,
            bool isNullable = true);

        IDbCommandParameterConfig<TDbParams> CharArray(DelegateHandler<TDbParams, char[]> returnFunction,
            bool isNullable = true);

        IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(DelegateHandler<TDbParams, string> returnFunction,
            bool isNullable = true);

        IDbCommandParameterConfig<TDbParams> AnsiString(DelegateHandler<TDbParams, string> returnFunction,
            bool isNullable = true);


        IDbCommandParameterConfig<TDbParams> Byte(DelegateHandler<TDbParams, byte?> returnFunction);
        IDbCommandParameterConfig<TDbParams> SByte(DelegateHandler<TDbParams, sbyte?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int16(DelegateHandler<TDbParams, short?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int32(DelegateHandler<TDbParams, int?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Int64(DelegateHandler<TDbParams, long?> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt16(DelegateHandler<TDbParams, ushort?> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt32(DelegateHandler<TDbParams, uint?> returnFunction);
        IDbCommandParameterConfig<TDbParams> UInt64(DelegateHandler<TDbParams, ulong?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Single(DelegateHandler<TDbParams, float?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Double(DelegateHandler<TDbParams, double?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Decimal(DelegateHandler<TDbParams, decimal?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Boolean(DelegateHandler<TDbParams, bool?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Char(DelegateHandler<TDbParams, char?> returnFunction);
        IDbCommandParameterConfig<TDbParams> Guid(DelegateHandler<TDbParams, Guid?> returnFunction);
        IDbCommandParameterConfig<TDbParams> TimeSpan(DelegateHandler<TDbParams, TimeSpan?> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTime(DelegateHandler<TDbParams, DateTime?> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime?> returnFunction);
        IDbCommandParameterConfig<TDbParams> DateTimeOffset(DelegateHandler<TDbParams, DateTimeOffset?> returnFunction);

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