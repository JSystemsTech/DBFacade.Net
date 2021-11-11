using DbFacade.Factories;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal class DbCommandParameterConfig<TDbParams> : IInternalDbCommandParameterConfig<TDbParams>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandParameterConfig{TDbParams}"/> class.
        /// </summary>
        internal DbCommandParameterConfig() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandParameterConfig{TDbParams}"/> class.
        /// </summary>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        protected DbCommandParameterConfig(DbType dbType, bool isNullable = true)
        {
            DbType = dbType;
            IsNullable = isNullable;
            ParameterDirection = ParameterDirection.Input;
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        protected async Task InitAsync(DbType dbType, bool isNullable = true)
        {
            DbType = dbType;
            IsNullable = isNullable;
            ParameterDirection = ParameterDirection.Input;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Gets or sets the type of the database.
        /// </summary>
        /// <value>
        /// The type of the database.
        /// </value>
        public DbType DbType { get; protected set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNullable { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is output.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is output; otherwise, <c>false</c>.
        /// </value>
        public bool IsOutput { get; private set; }
        /// <summary>
        /// Gets or sets the size of the output.
        /// </summary>
        /// <value>
        /// The size of the output.
        /// </value>
        public int? OutputSize { get; protected set; }
        /// <summary>
        /// Gets or sets the parameter direction.
        /// </summary>
        /// <value>
        /// The parameter direction.
        /// </value>
        public ParameterDirection ParameterDirection { get; protected set; }


        /// <summary>
        /// Values the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public virtual object Value(TDbParams model) => null;
        /// <summary>
        /// Values the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public virtual async Task<object> ValueAsync(TDbParams model) {
            await Task.CompletedTask;
            return null;
        }
        /// <summary>
        /// Gets the type of the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the database type asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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
        /// <summary>
        /// Creates the output.
        /// </summary>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        internal static DbCommandParameterConfig<TDbParams> CreateOutput(DbType dbType, int? size = null)
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>(dbType, true);
            config.OutputSize = size;
            config.ParameterDirection = ParameterDirection.Output;
            return config;
        }
        /// <summary>
        /// Creates the output asynchronous.
        /// </summary>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        internal static async Task<DbCommandParameterConfig<TDbParams>> CreateOutputAsync(DbType dbType, int? size = null)
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>();
            await config.InitAsync(dbType, true);
            config.OutputSize = size;
            config.ParameterDirection = ParameterDirection.Output;
            await Task.CompletedTask;
            return config;
        }
        /// <summary>
        /// Creates the return value.
        /// </summary>
        /// <returns></returns>
        internal static DbCommandParameterConfig<TDbParams> CreateReturnValue()
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>();
            config.ParameterDirection = ParameterDirection.ReturnValue;
            return config;
        }
        /// <summary>
        /// Creates the return value asynchronous.
        /// </summary>
        /// <returns></returns>
        internal static async Task<DbCommandParameterConfig<TDbParams>> CreateReturnValueAsync()
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>();
            config.ParameterDirection = ParameterDirection.ReturnValue;
            await Task.CompletedTask;
            return config;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="N"></typeparam>
        internal class DbCommandParameterGenericConfig<N> : DbCommandParameterConfig<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="DbCommandParameterGenericConfig`1"/> class from being created.
            /// </summary>
            private DbCommandParameterGenericConfig() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DbCommandParameterGenericConfig`1"/> class.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="returnFunction">The return function.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            private DbCommandParameterGenericConfig(DbType dbType, DelegateHandler<TDbParams, N> returnFunction,
                bool isNullable = true) : base(dbType, isNullable)
            {
                ReturnFunction = returnFunction;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DbCommandParameterGenericConfig`1"/> class.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="value">The value.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            private DbCommandParameterGenericConfig(DbType dbType, N value, bool isNullable = true) : this(dbType,
                model => value, isNullable)
            { }

            /// <summary>
            /// Creates the specified database type.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="returnFunction">The return function.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static DbCommandParameterGenericConfig<N> Create(DbType dbType, DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
                => new DbCommandParameterGenericConfig<N>(dbType, returnFunction, isNullable);
            /// <summary>
            /// Creates the specified database type.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="value">The value.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static DbCommandParameterGenericConfig<N> Create(DbType dbType, N value, bool isNullable = true)
                => new DbCommandParameterGenericConfig<N>(dbType, value, isNullable);
            /// <summary>
            /// Creates the specified return function.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static DbCommandParameterGenericConfig<N> Create(DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
                => Create(GetDbType<N>(), returnFunction, isNullable);
            /// <summary>
            /// Creates the specified value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static DbCommandParameterGenericConfig<N> Create(N value, bool isNullable = true)
                => Create(GetDbType<N>(), value, isNullable);


            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="returnFunction">The return function.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            private async Task InitAsync(DbType dbType, DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
            {
                ReturnFunction = returnFunction;
                await InitAsync(dbType, isNullable);                
            }
            /// <summary>
            /// Initializes the asynchronous.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="value">The value.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            private async Task InitAsync(DbType dbType, N value, bool isNullable = true)
            {
                await InitAsync(dbType, model => value, isNullable);
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="returnFunction">The return function.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            internal static async Task<DbCommandParameterGenericConfig<N>> CreateAsync(DbType dbType, DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
            {
                DbCommandParameterGenericConfig<N> config = new DbCommandParameterGenericConfig<N>();
                await config.InitAsync(dbType, returnFunction, isNullable);
                return config;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="dbType">Type of the database.</param>
            /// <param name="value">The value.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            internal static async Task<DbCommandParameterGenericConfig<N>> CreateAsync(DbType dbType, N value, bool isNullable = true)
            {
                DbCommandParameterGenericConfig<N> config = new DbCommandParameterGenericConfig<N>();
                await config.InitAsync(dbType, value, isNullable);
                return config;
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="returnFunction">The return function.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<DbCommandParameterGenericConfig<N>> CreateAsync(DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
                => await CreateAsync(await GetDbTypeAsync<N>(), returnFunction, isNullable);
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<DbCommandParameterGenericConfig<N>> CreateAsync(N value, bool isNullable = true)
                => await CreateAsync(await GetDbTypeAsync<N>(), value, isNullable);



            /// <summary>
            /// Gets or sets the return function.
            /// </summary>
            /// <value>
            /// The return function.
            /// </value>
            private DelegateHandler<TDbParams, N> ReturnFunction { get; set; }

            /// <summary>
            /// Values the specified model.
            /// </summary>
            /// <param name="model">The model.</param>
            /// <returns></returns>
            public sealed override object Value(TDbParams model)
            {
                return ReturnFunction(model);
            }
            /// <summary>
            /// Values the asynchronous.
            /// </summary>
            /// <param name="model">The model.</param>
            /// <returns></returns>
            public sealed override async Task<object> ValueAsync(TDbParams model)
            {
                N value = ReturnFunction(model);
                await Task.CompletedTask;
                return value;
            }
        }
    }
    
}