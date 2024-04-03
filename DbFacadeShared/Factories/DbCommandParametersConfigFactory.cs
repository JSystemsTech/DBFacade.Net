using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.Utils;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace DbFacade.Factories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public delegate T DelegateHandler<TDbParams, T>(TDbParams value);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public sealed class DbCommandParameterConfigFactory<TDbParams>
    {
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <returns></returns>
        internal static async Task<DbCommandParameterConfigFactory<TDbParams>> CreateAsync()
        {
            DbCommandParameterConfigFactory<TDbParams> factory = new DbCommandParameterConfigFactory<TDbParams>();
            await Task.CompletedTask;
            return factory;
        }
        #region ReturnValue
        /// <summary>
        /// Creates the return value.
        /// </summary>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> CreateReturnValue()
        => DbCommandParameterConfig<TDbParams>.CreateReturnValue();
        #endregion

        /// <summary>
        /// Creates the specified return function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnFunction">The return function.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> Create<T>(DelegateHandler<TDbParams, T> returnFunction, DbType dbType, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(dbType, returnFunction, isNullable);

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> Create<T>(T value, DbType dbType, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(dbType, value, isNullable);

        /// <summary>
        /// Creates the specified return function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnFunction">The return function.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> Create<T>(DelegateHandler<TDbParams, T> returnFunction, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(returnFunction, isNullable);

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> Create<T>(T value, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(value, isNullable);

        /// <summary>
        /// Creates the specified return function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> Create<T>(DelegateHandler<TDbParams, T> returnFunction)
        => Create(returnFunction, Nullable.GetUnderlyingType(typeof(T)) is Type);

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> Create<T>(T value)
        => Create(value, Nullable.GetUnderlyingType(typeof(T)) is Type);



        /// <summary>
        /// Dates the time2.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime> returnFunction)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.Create(DbType.DateTime2, returnFunction, false);
        /// <summary>
        /// Dates the time2.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> DateTime2(DateTime value)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.Create(DbType.DateTime2, value, false);
        /// <summary>
        /// Dates the time2.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime?> returnFunction)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime?>.Create(DbType.DateTime2, returnFunction);

        /// <summary>
        /// ANSIs the string.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> AnsiString(
            DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiString, returnFunction, isNullable);
        /// <summary>
        /// ANSIs the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> AnsiString(string value, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiString, value, isNullable);

        /// <summary>
        /// ANSIs the length of the string fixed.
        /// </summary>
        /// <param name="returnFunction">The return function.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiStringFixedLength, returnFunction, isNullable);
        /// <summary>
        /// ANSIs the length of the string fixed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(string value, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiStringFixedLength, value, isNullable);



        /// <summary>
        /// Outputs the byte.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputByte(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<byte>(DbType.Byte, size);


        /// <summary>
        /// Outputs the s byte.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputSByte(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<sbyte>(DbType.SByte, size);


        /// <summary>
        /// Outputs the int16.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputInt16(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<short>(DbType.Int16, size);


        /// <summary>
        /// Outputs the int32.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputInt32(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<int>(DbType.Int32, size);


        /// <summary>
        /// Outputs the int64.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputInt64(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<long>(DbType.Int64, size);


        /// <summary>
        /// Outputs the u int16.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputUInt16(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<ushort>(DbType.UInt16, size);


        /// <summary>
        /// Outputs the u int32.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputUInt32(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<uint>(DbType.UInt32, size);


        /// <summary>
        /// Outputs the u int64.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputUInt64(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<ulong>(DbType.UInt64, size);


        /// <summary>
        /// Outputs the single.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputSingle(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<float>(DbType.Single, size);


        /// <summary>
        /// Outputs the double.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDouble(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<double>(DbType.Double, size);


        /// <summary>
        /// Outputs the decimal.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDecimal(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<decimal>(DbType.Decimal, size);


        /// <summary>
        /// Outputs the boolean.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputBoolean(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<bool>(DbType.Boolean, size);


        /// <summary>
        /// Outputs the unique identifier.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputGuid(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<Guid>(DbType.Guid, size);


        /// <summary>
        /// Outputs the time span.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputTimeSpan(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<TimeSpan>(DbType.Time, size);


        /// <summary>
        /// Outputs the date time.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDateTime(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<DateTime>(DbType.DateTime, size);


        /// <summary>
        /// Outputs the date time2.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDateTime2(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<DateTime>(DbType.DateTime2, size);


        /// <summary>
        /// Outputs the date time offset.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDateTimeOffset(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<DateTimeOffset>(DbType.DateTimeOffset, size);


        /// <summary>
        /// Outputs the binary.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputBinary(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<byte[]>(DbType.Binary, size);


        /// <summary>
        /// Outputs the character.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputChar(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<char>(DbType.StringFixedLength, size);


        /// <summary>
        /// Outputs the string.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputString(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<string>(DbType.String, size);


        /// <summary>
        /// Outputs the character array.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputCharArray(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<char[]>(DbType.String, size);


        /// <summary>
        /// Outputs the length of the ANSI string fixed.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputAnsiStringFixedLength(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<char>(DbType.AnsiStringFixedLength, size);


        /// <summary>
        /// Outputs the ANSI string.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputAnsiString(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput<string>(DbType.AnsiString, size);


        /// <summary>
        /// Outputs the XML.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputXml(int? size = null)
            => DbCommandParameterConfig<TDbParams>.CreateOutput<SqlXml>(DbType.Xml, size);


    }
}