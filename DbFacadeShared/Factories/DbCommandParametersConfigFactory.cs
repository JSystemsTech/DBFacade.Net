using System;
using System.Data;
using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.Models;
using DbFacade.Utils;

namespace DbFacade.Factories
{
    public delegate T DelegateHandler<TDbParams, T>(TDbParams value) where TDbParams : IDbParamsModel;
    public static class DbCommandParameterConfigFactory
    {
        public static IDbCommandParameterConfig<DbParamsModel> Create<T>(T value, bool isNullable)
        => DbCommandParameterConfigFactory<DbParamsModel>.Create(value, isNullable);


        public static IDbCommandParameterConfig<DbParamsModel> Create<T>(T value)
        => DbCommandParameterConfigFactory<DbParamsModel>.Create(value);


        public static IDbCommandParameterConfig<DbParamsModel> DateTime2(DateTime value)
        => DbCommandParameterConfigFactory<DbParamsModel>.DateTime2(value);

        public static IDbCommandParameterConfig<DbParamsModel> AnsiString(string value, bool isNullable = true)
        => DbCommandParameterConfigFactory<DbParamsModel>.AnsiString(value, isNullable);

        public static IDbCommandParameterConfig<DbParamsModel> AnsiStringFixedLength(string value, bool isNullable = true)
        => DbCommandParameterConfigFactory<DbParamsModel>.AnsiStringFixedLength(value, isNullable);


        #region Output

        public static IDbCommandParameterConfig<DbParamsModel> OutputByte(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputByte(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputSByte(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputSByte(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputInt16(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputInt16(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputInt32(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputInt32(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputInt64(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputInt64(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputUInt16(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputUInt16(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputUInt32(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputUInt32(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputUInt64(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputUInt64(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputSingle(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputSingle(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDouble(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputDouble(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDecimal(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputDecimal(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputBoolean(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputBoolean(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputGuid(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputGuid(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputTimeSpan(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputTimeSpan(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDateTime(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputDateTime(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDateTime2(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputDateTime2(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputDateTimeOffset(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputDateTimeOffset(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputBinary(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputBinary(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputChar(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputChar(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputString(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputString(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputCharArray(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputCharArray(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputAnsiStringFixedLength(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputAnsiStringFixedLength(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputAnsiString(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputAnsiString(size);
        public static IDbCommandParameterConfig<DbParamsModel> OutputXml(int size) => DbCommandParameterConfigFactory<DbParamsModel>.OutputXml(size);


        #endregion


        #region Async Methods
        public static async Task<IDbCommandParameterConfig<DbParamsModel>> CreateAsync<T>(T value, bool isNullable)
        => await DbCommandParameterConfig<DbParamsModel>.DbCommandParameterGenericConfig<T>.CreateAsync(value, isNullable);

        public static async Task<IDbCommandParameterConfig<DbParamsModel>> CreateAsync<T>(T value)
        => await CreateAsync(value, GenericInstance.IsNullableType<T>());


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> DateTime2Async(DateTime value)
        => await DbCommandParameterConfig<DbParamsModel>.DbCommandParameterGenericConfig<DateTime>.CreateAsync(DbType.DateTime2, value, false);

        public static async Task<IDbCommandParameterConfig<DbParamsModel>> AnsiStringAsync(string value, bool isNullable = true)
        => await DbCommandParameterConfig<DbParamsModel>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiString, value, isNullable);

        public static async Task<IDbCommandParameterConfig<DbParamsModel>> AnsiStringFixedLengthAsync(string value, bool isNullable = true)
        => await DbCommandParameterConfig<DbParamsModel>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiStringFixedLength, value, isNullable);



        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputByteAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Byte, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputSByteAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.SByte, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputInt16Async(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Int16, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputInt32Async(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Int32, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputInt64Async(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Int64, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputUInt16Async(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.UInt16, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputUInt32Async(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.UInt32, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputUInt64Async(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.UInt64, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputSingleAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Single, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputDoubleAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Double, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputDecimalAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Decimal, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputBooleanAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Boolean, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputGuidAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Guid, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputTimeSpanAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Time, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputDateTimeAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.DateTime, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputDateTime2Async(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.DateTime2, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputDateTimeOffsetAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.DateTimeOffset, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputBinaryAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Binary, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputCharAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.StringFixedLength, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputStringAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.String, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputCharArrayAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.String, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputAnsiStringFixedLengthAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.AnsiStringFixedLength, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputAnsiStringAsync(int size)
        => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.AnsiString, size);


        public static async Task<IDbCommandParameterConfig<DbParamsModel>> OutputXmlAsync(int size)
            => await DbCommandParameterConfig<DbParamsModel>.CreateOutputAsync(DbType.Xml, size);

        #endregion
    }


    public class DbCommandParameterConfigFactory<TDbParams>
        where TDbParams : IDbParamsModel
    {
        public static IDbCommandParameterConfig<TDbParams> Create<T>(DelegateHandler<TDbParams, T> returnFunction, DbType dbType, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(dbType, returnFunction, isNullable);

        public static IDbCommandParameterConfig<TDbParams> Create<T>(T value, DbType dbType, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(dbType, value, isNullable);

        public static IDbCommandParameterConfig<TDbParams> Create<T>(DelegateHandler<TDbParams, T> returnFunction, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(returnFunction, isNullable);

        public static IDbCommandParameterConfig<TDbParams> Create<T>(T value, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(value, isNullable);

        public static IDbCommandParameterConfig<TDbParams> Create<T>(DelegateHandler<TDbParams, T> returnFunction)
        => Create(returnFunction, GenericInstance.IsNullableType<T>());

        public static IDbCommandParameterConfig<TDbParams> Create<T>(T value)
        => Create(value, GenericInstance.IsNullableType<T>());



        public static IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime> returnFunction)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.Create(DbType.DateTime2, returnFunction, false);
        public static IDbCommandParameterConfig<TDbParams> DateTime2(DateTime value)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.Create(DbType.DateTime2, value, false);
        public static IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime?> returnFunction)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime?>.Create(DbType.DateTime2, returnFunction);

        public static IDbCommandParameterConfig<TDbParams> AnsiString(
            DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiString, returnFunction, isNullable);
        public static IDbCommandParameterConfig<TDbParams> AnsiString(string value, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiString, value, isNullable);

        public static IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiStringFixedLength, returnFunction, isNullable);
        public static IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(string value, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiStringFixedLength, value, isNullable);



        public static IDbCommandParameterConfig<TDbParams> OutputByte(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Byte, size);


        public static IDbCommandParameterConfig<TDbParams> OutputSByte(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.SByte, size);


        public static IDbCommandParameterConfig<TDbParams> OutputInt16(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Int16, size);


        public static IDbCommandParameterConfig<TDbParams> OutputInt32(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Int32, size);


        public static IDbCommandParameterConfig<TDbParams> OutputInt64(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Int64, size);


        public static IDbCommandParameterConfig<TDbParams> OutputUInt16(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.UInt16, size);


        public static IDbCommandParameterConfig<TDbParams> OutputUInt32(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.UInt32, size);


        public static IDbCommandParameterConfig<TDbParams> OutputUInt64(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.UInt64, size);


        public static IDbCommandParameterConfig<TDbParams> OutputSingle(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Single, size);


        public static IDbCommandParameterConfig<TDbParams> OutputDouble(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Double, size);


        public static IDbCommandParameterConfig<TDbParams> OutputDecimal(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Decimal, size);


        public static IDbCommandParameterConfig<TDbParams> OutputBoolean(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Boolean, size);


        public static IDbCommandParameterConfig<TDbParams> OutputGuid(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Guid, size);


        public static IDbCommandParameterConfig<TDbParams> OutputTimeSpan(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Time, size);


        public static IDbCommandParameterConfig<TDbParams> OutputDateTime(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.DateTime, size);


        public static IDbCommandParameterConfig<TDbParams> OutputDateTime2(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.DateTime2, size);


        public static IDbCommandParameterConfig<TDbParams> OutputDateTimeOffset(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.DateTimeOffset, size);


        public static IDbCommandParameterConfig<TDbParams> OutputBinary(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Binary, size);


        public static IDbCommandParameterConfig<TDbParams> OutputChar(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.StringFixedLength, size);


        public static IDbCommandParameterConfig<TDbParams> OutputString(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.String, size);


        public static IDbCommandParameterConfig<TDbParams> OutputCharArray(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.String, size);


        public static IDbCommandParameterConfig<TDbParams> OutputAnsiStringFixedLength(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.AnsiStringFixedLength, size);


        public static IDbCommandParameterConfig<TDbParams> OutputAnsiString(int size)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.AnsiString, size);


        public static IDbCommandParameterConfig<TDbParams> OutputXml(int size)
            => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Xml, size);




        #region Async Methods
        public static async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(DelegateHandler<TDbParams, T> returnFunction, DbType dbType, bool isNullable)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.CreateAsync(dbType, returnFunction, isNullable);

        public static async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(T value, DbType dbType, bool isNullable)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.CreateAsync(dbType, value, isNullable);

        public static async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(DelegateHandler<TDbParams, T> returnFunction, bool isNullable)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.CreateAsync(returnFunction, isNullable);

        public static async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(T value, bool isNullable)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.CreateAsync(value, isNullable);

        public static async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(DelegateHandler<TDbParams, T> returnFunction)
        => await CreateAsync(returnFunction, GenericInstance.IsNullableType<T>());

        public static async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(T value)
        => await CreateAsync(value, GenericInstance.IsNullableType<T>());



        public static async Task<IDbCommandParameterConfig<TDbParams>> DateTime2Async(DelegateHandler<TDbParams, DateTime> returnFunction)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.CreateAsync(DbType.DateTime2, returnFunction, false);
        public static async Task<IDbCommandParameterConfig<TDbParams>> DateTime2Async(DateTime value)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.CreateAsync(DbType.DateTime2, value, false);
        public static async Task<IDbCommandParameterConfig<TDbParams>> DateTime2Async(DelegateHandler<TDbParams, DateTime?> returnFunction)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime?>.CreateAsync(DbType.DateTime2, returnFunction);

        public static async Task<IDbCommandParameterConfig<TDbParams>> AnsiStringAsync(
            DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiString, returnFunction, isNullable);
        public static async Task<IDbCommandParameterConfig<TDbParams>> AnsiStringAsync(string value, bool isNullable = true)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiString, value, isNullable);

        public static async Task<IDbCommandParameterConfig<TDbParams>> AnsiStringFixedLengthAsync(DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiStringFixedLength, returnFunction, isNullable);
        public static async Task<IDbCommandParameterConfig<TDbParams>> AnsiStringFixedLengthAsync(string value, bool isNullable = true)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiStringFixedLength, value, isNullable);



        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputByteAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Byte, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputSByteAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.SByte, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputInt16Async(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Int16, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputInt32Async(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Int32, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputInt64Async(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Int64, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputUInt16Async(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.UInt16, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputUInt32Async(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.UInt32, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputUInt64Async(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.UInt64, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputSingleAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Single, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputDoubleAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Double, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputDecimalAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Decimal, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputBooleanAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Boolean, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputGuidAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Guid, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputTimeSpanAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Time, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputDateTimeAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.DateTime, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputDateTime2Async(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.DateTime2, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputDateTimeOffsetAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.DateTimeOffset, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputBinaryAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Binary, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputCharAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.StringFixedLength, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputStringAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.String, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputCharArrayAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.String, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputAnsiStringFixedLengthAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.AnsiStringFixedLength, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputAnsiStringAsync(int size)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.AnsiString, size);


        public static async Task<IDbCommandParameterConfig<TDbParams>> OutputXmlAsync(int size)
            => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Xml, size);

        #endregion
    }
}