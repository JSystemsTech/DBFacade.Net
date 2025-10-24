using DbFacade.DataLayer.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Data;

namespace DbFacade.DataLayer.Models
{
    public sealed class DbTypeCollection
    {
        private readonly Dictionary<Type, DbTypeCollectionItem> Items;
        internal DbTypeCollectionItem this[Type type]
        {
            get
            {
                Type valueType = GetValueType(type);
                return GetRecord(valueType);
            }
            private set
            {
                Type valueType = GetValueType(type);
#if NET8_0_OR_GREATER
            Items.TryAdd(valueType, value);

#else
                if (!Items.ContainsKey(valueType))
                {
                    Items.Add(valueType, value);
                }
#endif
            }
        }
        private DbTypeCollection()
        {
            Items = new Dictionary<Type, DbTypeCollectionItem>();
            InitDefaultValues();
        }
        public static DbTypeCollection Create() => new DbTypeCollection();
        public void Add<T>(DbType dbType) => this[typeof(T)] = new DbTypeCollectionItem(typeof(T), dbType);
        public void Add<T>(DbType dbType, Func<T, object> valueResolver)
            => this[typeof(T)] = new DbTypeCollectionItem(typeof(T), dbType, m => m is T obj ? valueResolver(obj) : (object)null);

        private static Type GetValueType(Type type)
        => Nullable.GetUnderlyingType(type) is Type nullableType && nullableType != null ? nullableType : type;
        private DbTypeCollectionItem GetRecord(Type valueType)
        => Items.TryGetValue(valueType, out DbTypeCollectionItem item) ? item : DbTypeCollectionItem.Default;
        private void InitDefaultValues()
        {
            Add<byte>(DbType.Byte);
            Add<sbyte>(DbType.SByte);
            Add<short>(DbType.Int16);
            Add<int>(DbType.Int32);
            Add<long>(DbType.Int64);
            Add<ushort>(DbType.UInt16);
            Add<uint>(DbType.UInt32);
            Add<ulong>(DbType.UInt64);
            Add<float>(DbType.Single);
            Add<double>(DbType.Double);
            Add<decimal>(DbType.Decimal);
            Add<bool>(DbType.Boolean);
            Add<char>(DbType.StringFixedLength);
            Add<Guid>(DbType.Guid);
            Add<TimeSpan>(DbType.Time);
            Add<DateTime>(DbType.DateTime);
            Add<DateTimeOffset>(DbType.DateTimeOffset);
            Add<string>(DbType.String);
            Add<char[]>(DbType.String, arr => new string(arr));
            Add<byte[]>(DbType.Binary);
            Add<Currency>(DbType.Currency, m => m.Value);
            Add<AnsiString>(DbType.AnsiString, m => m.Value);
            Add<AnsiStringFixedLength>(DbType.AnsiStringFixedLength, m => m.Value);
            Add<StringFixedLength>(DbType.StringFixedLength, m => m.Value);
        }
    }
}
