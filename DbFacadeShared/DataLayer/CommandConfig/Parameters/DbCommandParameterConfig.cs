using DbFacade.Factories;
using System;
using System.Data;
using System.Data.SqlTypes;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    internal class DbCommandParameterConfig<TDbParams>: IDbCommandParameterConfig<TDbParams>
    {
        internal DbCommandParameterConfig() { }

        internal DbCommandParameterConfig(DbType dbType, bool isNullable = true)
        {
            DbType = dbType;
            IsNullable = isNullable;
            ParameterDirection = ParameterDirection.Input;
        }

        internal DbType DbType { get; set; }
        internal bool IsNullable { get; set; }
        internal bool IsOutput { get; private set; }
        internal Type OutputType { get; private set; }
        internal int? OutputSize { get; set; }
        internal ParameterDirection ParameterDirection { get; set; }
        internal virtual object Value(TDbParams model) => null;
        
        protected static DbType GetDbType<T>()
        {
            Type t = typeof(T);
            return t == typeof(byte) || t == typeof(byte?) ? DbType.Byte :
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
        }
        
        internal static DbCommandParameterConfig<TDbParams> CreateOutput<N>(DbType dbType, int? size = null)
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>(dbType, true);
            config.OutputSize = size;
            config.ParameterDirection = ParameterDirection.Output;
            config.OutputType = typeof(N);
            return config;
        }

        internal static DbCommandParameterConfig<TDbParams> CreateReturnValue()
        {
            DbCommandParameterConfig<TDbParams> config = new DbCommandParameterConfig<TDbParams>();
            config.ParameterDirection = ParameterDirection.ReturnValue;
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

            internal static DbCommandParameterGenericConfig<N> Create(DbType dbType, DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
                => new DbCommandParameterGenericConfig<N>(dbType, returnFunction, isNullable);
            
            internal static DbCommandParameterGenericConfig<N> Create(DbType dbType, N value, bool isNullable = true)
                => new DbCommandParameterGenericConfig<N>(dbType, value, isNullable);
            
            internal static DbCommandParameterGenericConfig<N> Create(DelegateHandler<TDbParams, N> returnFunction, bool isNullable = true)
                => Create(GetDbType<N>(), returnFunction, isNullable);
            
            internal static DbCommandParameterGenericConfig<N> Create(N value, bool isNullable = true)
                => Create(GetDbType<N>(), value, isNullable);

            private DelegateHandler<TDbParams, N> ReturnFunction { get; set; }

            internal sealed override object Value(TDbParams model)
            {
                return ReturnFunction(model);
            }
        }
    }

}