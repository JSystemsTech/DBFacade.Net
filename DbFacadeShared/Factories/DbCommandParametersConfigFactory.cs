using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.Utils;
using System;
using System.Data;
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
        => Create(returnFunction, GenericInstance.IsNullableType<T>());

        /// <summary>
        /// Creates the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> Create<T>(T value)
        => Create(value, GenericInstance.IsNullableType<T>());



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
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Byte, size);


        /// <summary>
        /// Outputs the s byte.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputSByte(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.SByte, size);


        /// <summary>
        /// Outputs the int16.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputInt16(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Int16, size);


        /// <summary>
        /// Outputs the int32.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputInt32(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Int32, size);


        /// <summary>
        /// Outputs the int64.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputInt64(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Int64, size);


        /// <summary>
        /// Outputs the u int16.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputUInt16(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.UInt16, size);


        /// <summary>
        /// Outputs the u int32.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputUInt32(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.UInt32, size);


        /// <summary>
        /// Outputs the u int64.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputUInt64(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.UInt64, size);


        /// <summary>
        /// Outputs the single.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputSingle(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Single, size);


        /// <summary>
        /// Outputs the double.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDouble(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Double, size);


        /// <summary>
        /// Outputs the decimal.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDecimal(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Decimal, size);


        /// <summary>
        /// Outputs the boolean.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputBoolean(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Boolean, size);


        /// <summary>
        /// Outputs the unique identifier.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputGuid(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Guid, size);


        /// <summary>
        /// Outputs the time span.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputTimeSpan(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Time, size);


        /// <summary>
        /// Outputs the date time.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDateTime(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.DateTime, size);


        /// <summary>
        /// Outputs the date time2.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDateTime2(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.DateTime2, size);


        /// <summary>
        /// Outputs the date time offset.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputDateTimeOffset(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.DateTimeOffset, size);


        /// <summary>
        /// Outputs the binary.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputBinary(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Binary, size);


        /// <summary>
        /// Outputs the character.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputChar(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.StringFixedLength, size);


        /// <summary>
        /// Outputs the string.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputString(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.String, size);


        /// <summary>
        /// Outputs the character array.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputCharArray(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.String, size);


        /// <summary>
        /// Outputs the length of the ANSI string fixed.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputAnsiStringFixedLength(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.AnsiStringFixedLength, size);


        /// <summary>
        /// Outputs the ANSI string.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputAnsiString(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.AnsiString, size);


        /// <summary>
        /// Outputs the XML.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public IDbCommandParameterConfig<TDbParams> OutputXml(int? size = null)
            => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Xml, size);


    }
}