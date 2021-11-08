using System;
using System.Data;
using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.Models;
using DbFacade.Utils;

namespace DbFacade.Factories
{
    public delegate T DelegateHandler<TDbParams, T>(TDbParams value);
    public sealed class DbCommandParameterConfigFactory<TDbParams>
    {
        internal static async Task<DbCommandParameterConfigFactory<TDbParams>> CreateAsync()
        {
            DbCommandParameterConfigFactory<TDbParams> factory = new DbCommandParameterConfigFactory<TDbParams>();
            await Task.CompletedTask;
            return factory;
        }
        #region ReturnValue
        public  IDbCommandParameterConfig<TDbParams> CreateReturnValue()
        => DbCommandParameterConfig<TDbParams>.CreateReturnValue();
        #endregion

        public  IDbCommandParameterConfig<TDbParams> Create<T>(DelegateHandler<TDbParams, T> returnFunction, DbType dbType, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(dbType, returnFunction, isNullable);

        public  IDbCommandParameterConfig<TDbParams> Create<T>(T value, DbType dbType, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(dbType, value, isNullable);

        public  IDbCommandParameterConfig<TDbParams> Create<T>(DelegateHandler<TDbParams, T> returnFunction, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(returnFunction, isNullable);

        public  IDbCommandParameterConfig<TDbParams> Create<T>(T value, bool isNullable)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.Create(value, isNullable);

        public  IDbCommandParameterConfig<TDbParams> Create<T>(DelegateHandler<TDbParams, T> returnFunction)
        => Create(returnFunction, GenericInstance.IsNullableType<T>());

        public  IDbCommandParameterConfig<TDbParams> Create<T>(T value)
        => Create(value, GenericInstance.IsNullableType<T>());



        public  IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime> returnFunction)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.Create(DbType.DateTime2, returnFunction, false);
        public  IDbCommandParameterConfig<TDbParams> DateTime2(DateTime value)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.Create(DbType.DateTime2, value, false);
        public  IDbCommandParameterConfig<TDbParams> DateTime2(DelegateHandler<TDbParams, DateTime?> returnFunction)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime?>.Create(DbType.DateTime2, returnFunction);

        public  IDbCommandParameterConfig<TDbParams> AnsiString(
            DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiString, returnFunction, isNullable);
        public  IDbCommandParameterConfig<TDbParams> AnsiString(string value, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiString, value, isNullable);

        public  IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiStringFixedLength, returnFunction, isNullable);
        public  IDbCommandParameterConfig<TDbParams> AnsiStringFixedLength(string value, bool isNullable = true)
        => DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.Create(DbType.AnsiStringFixedLength, value, isNullable);



        public  IDbCommandParameterConfig<TDbParams> OutputByte(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Byte, size);


        public  IDbCommandParameterConfig<TDbParams> OutputSByte(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.SByte, size);


        public  IDbCommandParameterConfig<TDbParams> OutputInt16(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Int16, size);


        public  IDbCommandParameterConfig<TDbParams> OutputInt32(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Int32, size);


        public  IDbCommandParameterConfig<TDbParams> OutputInt64(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Int64, size);


        public  IDbCommandParameterConfig<TDbParams> OutputUInt16(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.UInt16, size);


        public  IDbCommandParameterConfig<TDbParams> OutputUInt32(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.UInt32, size);


        public  IDbCommandParameterConfig<TDbParams> OutputUInt64(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.UInt64, size);


        public  IDbCommandParameterConfig<TDbParams> OutputSingle(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Single, size);


        public  IDbCommandParameterConfig<TDbParams> OutputDouble(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Double, size);


        public  IDbCommandParameterConfig<TDbParams> OutputDecimal(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Decimal, size);


        public  IDbCommandParameterConfig<TDbParams> OutputBoolean(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Boolean, size);


        public  IDbCommandParameterConfig<TDbParams> OutputGuid(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Guid, size);


        public  IDbCommandParameterConfig<TDbParams> OutputTimeSpan(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Time, size);


        public  IDbCommandParameterConfig<TDbParams> OutputDateTime(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.DateTime, size);


        public  IDbCommandParameterConfig<TDbParams> OutputDateTime2(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.DateTime2, size);


        public  IDbCommandParameterConfig<TDbParams> OutputDateTimeOffset(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.DateTimeOffset, size);


        public  IDbCommandParameterConfig<TDbParams> OutputBinary(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Binary, size);


        public  IDbCommandParameterConfig<TDbParams> OutputChar(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.StringFixedLength, size);


        public  IDbCommandParameterConfig<TDbParams> OutputString(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.String, size);


        public  IDbCommandParameterConfig<TDbParams> OutputCharArray(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.String, size);


        public  IDbCommandParameterConfig<TDbParams> OutputAnsiStringFixedLength(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.AnsiStringFixedLength, size);


        public  IDbCommandParameterConfig<TDbParams> OutputAnsiString(int? size = null)
        => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.AnsiString, size);


        public  IDbCommandParameterConfig<TDbParams> OutputXml(int? size = null)
            => DbCommandParameterConfig<TDbParams>.CreateOutput(DbType.Xml, size);




        #region Async Methods
        #region ReturnValue
        public  async Task<IDbCommandParameterConfig<TDbParams>> CreateReturnValueAsync()
        => await DbCommandParameterConfig<TDbParams>.CreateReturnValueAsync();
        #endregion
        public  async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(DelegateHandler<TDbParams, T> returnFunction, DbType dbType, bool isNullable)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.CreateAsync(dbType, returnFunction, isNullable);

        public  async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(T value, DbType dbType, bool isNullable)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.CreateAsync(dbType, value, isNullable);

        public  async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(DelegateHandler<TDbParams, T> returnFunction, bool isNullable)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.CreateAsync(returnFunction, isNullable);

        public  async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(T value, bool isNullable)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<T>.CreateAsync(value, isNullable);

        public  async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(DelegateHandler<TDbParams, T> returnFunction)
        => await CreateAsync(returnFunction, GenericInstance.IsNullableType<T>());

        public  async Task<IDbCommandParameterConfig<TDbParams>> CreateAsync<T>(T value)
        => await CreateAsync(value, GenericInstance.IsNullableType<T>());



        public  async Task<IDbCommandParameterConfig<TDbParams>> DateTime2Async(DelegateHandler<TDbParams, DateTime> returnFunction)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.CreateAsync(DbType.DateTime2, returnFunction, false);
        public  async Task<IDbCommandParameterConfig<TDbParams>> DateTime2Async(DateTime value)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime>.CreateAsync(DbType.DateTime2, value, false);
        public  async Task<IDbCommandParameterConfig<TDbParams>> DateTime2Async(DelegateHandler<TDbParams, DateTime?> returnFunction)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<DateTime?>.CreateAsync(DbType.DateTime2, returnFunction);

        public  async Task<IDbCommandParameterConfig<TDbParams>> AnsiStringAsync(
            DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiString, returnFunction, isNullable);
        public  async Task<IDbCommandParameterConfig<TDbParams>> AnsiStringAsync(string value, bool isNullable = true)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiString, value, isNullable);

        public  async Task<IDbCommandParameterConfig<TDbParams>> AnsiStringFixedLengthAsync(DelegateHandler<TDbParams, string> returnFunction, bool isNullable = true)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiStringFixedLength, returnFunction, isNullable);
        public  async Task<IDbCommandParameterConfig<TDbParams>> AnsiStringFixedLengthAsync(string value, bool isNullable = true)
        => await DbCommandParameterConfig<TDbParams>.DbCommandParameterGenericConfig<string>.CreateAsync(DbType.AnsiStringFixedLength, value, isNullable);



        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputByteAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Byte, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputSByteAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.SByte, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputInt16Async(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Int16, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputInt32Async(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Int32, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputInt64Async(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Int64, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputUInt16Async(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.UInt16, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputUInt32Async(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.UInt32, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputUInt64Async(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.UInt64, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputSingleAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Single, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputDoubleAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Double, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputDecimalAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Decimal, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputBooleanAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Boolean, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputGuidAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Guid, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputTimeSpanAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Time, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputDateTimeAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.DateTime, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputDateTime2Async(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.DateTime2, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputDateTimeOffsetAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.DateTimeOffset, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputBinaryAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Binary, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputCharAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.StringFixedLength, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputStringAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.String, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputCharArrayAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.String, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputAnsiStringFixedLengthAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.AnsiStringFixedLength, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputAnsiStringAsync(int? size = null)
        => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.AnsiString, size);


        public  async Task<IDbCommandParameterConfig<TDbParams>> OutputXmlAsync(int? size = null)
            => await DbCommandParameterConfig<TDbParams>.CreateOutputAsync(DbType.Xml, size);

        #endregion
    }
}