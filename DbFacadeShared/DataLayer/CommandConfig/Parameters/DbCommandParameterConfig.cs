using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models;
using DbFacade.Factories;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary></summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal class DbCommandParameterConfig<TDbParams> : IInternalDbCommandParameterConfig<TDbParams>
    {
        internal DbCommandParameterConfig() { }

        protected DbCommandParameterConfig(DbType dbType, bool isNullable = true)
        {
            DbType = dbType;
            IsNullable = isNullable;
            ParameterDirection = ParameterDirection.Input;
        }
        protected async Task InitAsync(DbType dbType, bool isNullable = true)
        {
            DbType = dbType;
            IsNullable = isNullable;
            ParameterDirection = ParameterDirection.Input;
            await Task.CompletedTask;
        }

        public DbType DbType { get; protected set; }
        public bool IsNullable { get; set; }
        public bool IsOutput { get; private set; }
        public int? OutputSize { get; protected set; }
        public ParameterDirection ParameterDirection { get; protected set; }


        public virtual object Value(TDbParams model) => null;
        public virtual async Task<object> ValueAsync(TDbParams model) {
            await Task.CompletedTask;
            return null;
        }
        protected static DbType GetDbType<T>()
        {
            Type t = typeof(T);
            return t == typeof(byte)        || t == typeof(byte?)       ? DbType.Byte :
                   t == typeof(sbyte)       || t == typeof(sbyte?)      ? DbType.SByte :
                   t == typeof(short)       || t == typeof(short?)      ? DbType.Int16 :
                   t == typeof(int)         || t == typeof(int?)        ? DbType.Int32 :
                   t == typeof(long)        || t == typeof(long?)       ? DbType.Int64 :
                   t == typeof(ushort)      || t == typeof(ushort?)     ? DbType.UInt16 :
                   t == typeof(uint)        || t == typeof(uint?)       ? DbType.UInt32 :
                   t == typeof(ulong)       || t == typeof(ulong?)      ? DbType.UInt64 :
                   t == typeof(float)       || t == typeof(float?)      ? DbType.Single :
                   t == typeof(double)      || t == typeof(double?)     ? DbType.Double :
                   t == typeof(decimal)     || t == typeof(decimal?)    ? DbType.Decimal :
                   t == typeof(bool)        || t == typeof(bool?)       ? DbType.Boolean :
                   t == typeof(char)        || t == typeof(char?)       ? DbType.StringFixedLength :
                   t == typeof(Guid)        || t == typeof(Guid?)       ? DbType.Guid :
                   t == typeof(TimeSpan)    || t == typeof(TimeSpan?)   ? DbType.Time :
                   t == typeof(DateTime)    || t == typeof(DateTime?)   ? DbType.DateTime :
                   t == typeof(string)      || t == typeof(char[])      ? DbType.String :
                   t == typeof(byte[])                                  ? DbType.Binary :                   
                   t == typeof(SqlXml)                                  ? DbType.Xml : 
                   DbType.Object;
        }
        protected static async Task<DbType> GetDbTypeAsync<T>()
        {
            Type t = typeof(T);
            DbType dbType =  t == typeof(byte) || t == typeof(byte?) ? DbType.Byte :
                             t == typeof(sbyte) || t == typeof(sbyte?) ? DbType.SByte :
                             t == typeof(short) || t == typeof(short?) ? DbType.Int16 :
                             t == typeof(int) || t == typeof(int?) ? DbType.Int32 :
                             t == typeof(long) || t == typeof(long?) ? DbType.Int64 :
                             t == typeof(ushort) || t == typeof(ushort?) ? DbType.UInt16 :
                             t == typeof(uint) || t == typeof(uint?) ? DbType.UInt32 :
                             t == typeof(ulong) || t == typeof(ulong?) ? DbType.UInt64 :
                             t == typeof(float) || t == typeof(float?) ? DbType.Single :
                             t == typeof(double) || t == typeof(double?) ? DbType.Double :
                             t == typeof(decimal) || t == typeof(decimal?) ? DbType.Decimal :
                             t == typeof(bool) || t == typeof(bool?) ? DbType.Boolean :
                             t == typeof(char) || t == typeof(char?) ? DbType.StringFixedLength :
                             t == typeof(Guid) || t == typeof(Guid?) ? DbType.Guid :
                             t == typeof(TimeSpan) || t == typeof(TimeSpan?) ? DbType.Time :
                             t == typeof(DateTime) || t == typeof(DateTime?) ? DbType.DateTime :
                             t == typeof(string) || t == typeof(char[]) ? DbType.String :
                             t == typeof(byte[]) ? DbType.Binary :
                             t == typeof(SqlXml) ? DbType.Xml :
                             DbType.Object;
            await Task.CompletedTask;
            return dbType;
        }
        internal static DbCommandParameterConfig<TDbParams> CreateOutput(DbType dbType, int? size = null)
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>(dbType, true);
            config.OutputSize = size;
            config.ParameterDirection = ParameterDirection.Output;
            return config;
        }
        internal static async Task<DbCommandParameterConfig<TDbParams>> CreateOutputAsync(DbType dbType, int? size = null)
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>();
            await config.InitAsync(dbType, true);
            config.OutputSize = size;
            config.ParameterDirection = ParameterDirection.Output;
            await Task.CompletedTask;
            return config;
        }
        internal static DbCommandParameterConfig<TDbParams> CreateReturnValue()
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>();
            config.ParameterDirection = ParameterDirection.ReturnValue;
            return config;
        }
        internal static async Task<DbCommandParameterConfig<TDbParams>> CreateReturnValueAsync()
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>();
            config.ParameterDirection = ParameterDirection.ReturnValue;
            await Task.CompletedTask;
            return config;
        }
        internal class DbCommandParameterGenericConfig<N> : DbCommandParameterConfig<TDbParams>
        {
            private DbCommandParameterGenericConfig() { }
            private DbCommandParameterGenericConfig(DbType dbType, DelegateHandler<TDbParams, N> returnFunction,
                bool isNullable = true) : base(dbType, isNullable)
            {
                ReturnFunction = returnFunction;
            }

            private DbCommandParameterGenericConfig(DbType dbType, N value, bool isNullable = true) : this(dbType,
                model => value, isNullable)
            { }

            public static DbCommandParameterGenericConfig<N> Create(DbType dbType, DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
                => new DbCommandParameterGenericConfig<N>(dbType, returnFunction, isNullable);
            public static DbCommandParameterGenericConfig<N> Create(DbType dbType, N value, bool isNullable = true)
                => new DbCommandParameterGenericConfig<N>(dbType, value, isNullable);
            public static DbCommandParameterGenericConfig<N> Create(DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
                => Create(GetDbType<N>(), returnFunction, isNullable);
            public static DbCommandParameterGenericConfig<N> Create(N value, bool isNullable = true)
                => Create(GetDbType<N>(), value, isNullable);


            private async Task InitAsync(DbType dbType, DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
            {
                ReturnFunction = returnFunction;
                await InitAsync(dbType, isNullable);                
            }
            private async Task InitAsync(DbType dbType, N value, bool isNullable = true)
            {
                await InitAsync(dbType, model => value, isNullable);
            }
            internal static async Task<DbCommandParameterGenericConfig<N>> CreateAsync(DbType dbType, DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
            {
                DbCommandParameterGenericConfig<N> config = new DbCommandParameterGenericConfig<N>();
                await config.InitAsync(dbType, returnFunction, isNullable);
                return config;
            }
            internal static async Task<DbCommandParameterGenericConfig<N>> CreateAsync(DbType dbType, N value, bool isNullable = true)
            {
                DbCommandParameterGenericConfig<N> config = new DbCommandParameterGenericConfig<N>();
                await config.InitAsync(dbType, value, isNullable);
                return config;
            }
            public static async Task<DbCommandParameterGenericConfig<N>> CreateAsync(DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
                => await CreateAsync(await GetDbTypeAsync<N>(), returnFunction, isNullable);
            public static async Task<DbCommandParameterGenericConfig<N>> CreateAsync(N value, bool isNullable = true)
                => await CreateAsync(await GetDbTypeAsync<N>(), value, isNullable);



            private DelegateHandler<TDbParams, N> ReturnFunction { get; set; }

            public sealed override object Value(TDbParams model)
            {
                return ReturnFunction(model);
            }
            public sealed override async Task<object> ValueAsync(TDbParams model)
            {
                N value = ReturnFunction(model);
                await Task.CompletedTask;
                return value;
            }
        }
    }
    
}