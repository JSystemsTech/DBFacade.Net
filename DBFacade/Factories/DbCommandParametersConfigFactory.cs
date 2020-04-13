using System;
using System.Web.UI.WebControls;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.Models;

namespace DBFacade.Factories
{
    internal class DbCommandParameterConfigFactory<TDbParams> : IDbCommandParameterConfigFactory<TDbParams>
        where TDbParams : IDbParamsModel
    {
        private readonly DbCommandParameterConfig<TDbParams> Base = new DbCommandParameterConfig<TDbParams>();

        #region Non Nullable

        #region Lamda

        public IDbCommandParameterConfig<TDbParams> Byte(DelegateHandler<TDbParams, byte> returnFunction)
        {
            return Base.Byte(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> SByte(DelegateHandler<TDbParams, sbyte> returnFunction)
        {
            return Base.SByte(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int16(DelegateHandler<TDbParams, short> returnFunction)
        {
            return Base.Int16(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int32(DelegateHandler<TDbParams, int> returnFunction)
        {
            return Base.Int32(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int64(DelegateHandler<TDbParams, long> returnFunction)
        {
            return Base.Int64(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt16(DelegateHandler<TDbParams, ushort> returnFunction)
        {
            return Base.UInt16(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt32(DelegateHandler<TDbParams, uint> returnFunction)
        {
            return Base.UInt32(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt64(DelegateHandler<TDbParams, ulong> returnFunction)
        {
            return Base.UInt64(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Single(DelegateHandler<TDbParams, float> returnFunction)
        {
            return Base.Single(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Double(DelegateHandler<TDbParams, double> returnFunction)
        {
            return Base.Double(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Decimal(DelegateHandler<TDbParams, decimal> returnFunction)
        {
            return Base.Decimal(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Boolean(DelegateHandler<TDbParams, bool> returnFunction)
        {
            return Base.Boolean(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Char(DelegateHandler<TDbParams, char> returnFunction)
        {
            return Base.Char(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Guid(DelegateHandler<TDbParams, Guid> returnFunction)
        {
            return Base.Guid(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> TimeSpan(DelegateHandler<TDbParams, TimeSpan> returnFunction)
        {
            return Base.TimeSpan(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime(DelegateHandler<TDbParams, DateTime> returnFunction)
        {
            return Base.DateTime(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime> returnFunction)
        {
            return Base.DateTime2(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(
            DelegateHandler<TDbParams, DateTimeOffset> returnFunction)
        {
            return Base.DateTimeOffset(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Binary(DelegateHandler<TDbParams, byte[]> returnFunction)
        {
            return Base.Binary(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Xml(DelegateHandler<TDbParams, Xml> returnFunction)
        {
            return Base.Xml(returnFunction);
        }

        #endregion

        #region Hard Coded

        public IDbCommandParameterConfig<TDbParams> Byte(byte value)
        {
            return Base.Byte(value);
        }

        public IDbCommandParameterConfig<TDbParams> SByte(sbyte value)
        {
            return Base.SByte(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int16(short value)
        {
            return Base.Int16(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int32(int value)
        {
            return Base.Int32(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int64(long value)
        {
            return Base.Int64(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt16(ushort value)
        {
            return Base.UInt16(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt32(uint value)
        {
            return Base.UInt32(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt64(ulong value)
        {
            return Base.UInt64(value);
        }

        public IDbCommandParameterConfig<TDbParams> Single(float value)
        {
            return Base.Single(value);
        }

        public IDbCommandParameterConfig<TDbParams> Double(double value)
        {
            return Base.Double(value);
        }

        public IDbCommandParameterConfig<TDbParams> Decimal(decimal value)
        {
            return Base.Decimal(value);
        }

        public IDbCommandParameterConfig<TDbParams> Boolean(bool value)
        {
            return Base.Boolean(value);
        }

        public IDbCommandParameterConfig<TDbParams> Guid(Guid value)
        {
            return Base.Guid(value);
        }

        public IDbCommandParameterConfig<TDbParams> TimeSpan(TimeSpan value)
        {
            return Base.TimeSpan(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime(DateTime value)
        {
            return Base.DateTime(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime2(DateTime value)
        {
            return Base.DateTime2(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset value)
        {
            return Base.DateTimeOffset(value);
        }

        public IDbCommandParameterConfig<TDbParams> Binary(byte[] value)
        {
            return Base.Binary(value);
        }

        public IDbCommandParameterConfig<TDbParams> Char(char value)
        {
            return Base.Char(value);
        }

        public IDbCommandParameterConfig<TDbParams> String(string value)
        {
            return Base.String(value);
        }

        public IDbCommandParameterConfig<TDbParams> CharArray(char[] value)
        {
            return Base.CharArray(value);
        }

        public IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(string value)
        {
            return Base.AnsiStringFixedLength(value);
        }

        public IDbCommandParameterConfig<TDbParams> AnsiString(string value)
        {
            return Base.AnsiString(value);
        }

        public IDbCommandParameterConfig<TDbParams> Xml(Xml value)
        {
            return Base.Xml(value);
        }

        #endregion

        #endregion

        #region Nullable

        #region Lamda

        public IDbCommandParameterConfig<TDbParams> String(DelegateHandler<TDbParams, string> returnFunction,
            bool isNullable = true)
        {
            return Base.String(returnFunction, isNullable);
        }

        public IDbCommandParameterConfig<TDbParams> CharArray(DelegateHandler<TDbParams, char[]> returnFunction,
            bool isNullable = true)
        {
            return Base.CharArray(returnFunction, isNullable);
        }

        public IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(
            DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        {
            return Base.AnsiStringFixedLength(returnFunction, isNullable);
        }

        public IDbCommandParameterConfig<TDbParams> AnsiString(DelegateHandler<TDbParams, string> returnFunction,
            bool isNullable = true)
        {
            return Base.AnsiString(returnFunction, isNullable);
        }

        public IDbCommandParameterConfig<TDbParams> Byte(DelegateHandler<TDbParams, byte?> returnFunction)
        {
            return Base.Byte(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> SByte(DelegateHandler<TDbParams, sbyte?> returnFunction)
        {
            return Base.SByte(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int16(DelegateHandler<TDbParams, short?> returnFunction)
        {
            return Base.Int16(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int32(DelegateHandler<TDbParams, int?> returnFunction)
        {
            return Base.Int32(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Int64(DelegateHandler<TDbParams, long?> returnFunction)
        {
            return Base.Int64(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt16(DelegateHandler<TDbParams, ushort?> returnFunction)
        {
            return Base.UInt16(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt32(DelegateHandler<TDbParams, uint?> returnFunction)
        {
            return Base.UInt32(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> UInt64(DelegateHandler<TDbParams, ulong?> returnFunction)
        {
            return Base.UInt64(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Single(DelegateHandler<TDbParams, float?> returnFunction)
        {
            return Base.Single(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Double(DelegateHandler<TDbParams, double?> returnFunction)
        {
            return Base.Double(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Decimal(DelegateHandler<TDbParams, decimal?> returnFunction)
        {
            return Base.Decimal(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Boolean(DelegateHandler<TDbParams, bool?> returnFunction)
        {
            return Base.Boolean(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Char(DelegateHandler<TDbParams, char?> returnFunction)
        {
            return Base.Char(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> Guid(DelegateHandler<TDbParams, Guid?> returnFunction)
        {
            return Base.Guid(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> TimeSpan(DelegateHandler<TDbParams, TimeSpan?> returnFunction)
        {
            return Base.TimeSpan(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime(DelegateHandler<TDbParams, DateTime?> returnFunction)
        {
            return Base.DateTime(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime?> returnFunction)
        {
            return Base.DateTime2(returnFunction);
        }

        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(
            DelegateHandler<TDbParams, DateTimeOffset?> returnFunction)
        {
            return Base.DateTimeOffset(returnFunction);
        }

        #endregion

        #region Hard Coded

        public IDbCommandParameterConfig<TDbParams> Byte(byte? value)
        {
            return Base.Byte(value);
        }

        public IDbCommandParameterConfig<TDbParams> SByte(sbyte? value)
        {
            return Base.SByte(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int16(short? value)
        {
            return Base.Int16(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int32(int? value)
        {
            return Base.Int32(value);
        }

        public IDbCommandParameterConfig<TDbParams> Int64(long? value)
        {
            return Base.Int64(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt16(ushort? value)
        {
            return Base.UInt16(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt32(uint? value)
        {
            return Base.UInt32(value);
        }

        public IDbCommandParameterConfig<TDbParams> UInt64(ulong? value)
        {
            return Base.UInt64(value);
        }

        public IDbCommandParameterConfig<TDbParams> Single(float? value)
        {
            return Base.Single(value);
        }

        public IDbCommandParameterConfig<TDbParams> Double(double? value)
        {
            return Base.Double(value);
        }

        public IDbCommandParameterConfig<TDbParams> Decimal(decimal? value)
        {
            return Base.Decimal(value);
        }

        public IDbCommandParameterConfig<TDbParams> Boolean(bool? value)
        {
            return Base.Boolean(value);
        }

        public IDbCommandParameterConfig<TDbParams> StringFixedLength(char? value)
        {
            return Base.StringFixedLength(value);
        }

        public IDbCommandParameterConfig<TDbParams> Guid(Guid? value)
        {
            return Base.Guid(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTime(DateTime? value)
        {
            return Base.DateTime(value);
        }

        public IDbCommandParameterConfig<TDbParams> DateTimeOffset(DateTimeOffset? value)
        {
            return Base.DateTimeOffset(value);
        }

        #endregion

        #endregion
    }
}