using System;
using System.Data;

namespace DbFacade.DataLayer.Models
{
    internal readonly struct DbTypeCollectionItem
    {
        internal static DbTypeCollectionItem Default = new DbTypeCollectionItem(typeof(object), DbType.Object);
        public readonly Type ValueType;
        public readonly DbType DbType;
        public readonly Func<object, object> ValueResolver;
        public DbTypeCollectionItem(Type valueType, DbType dbType)
        {
            ValueType = valueType;
            DbType = dbType;
            ValueResolver = m => m;
        }
        public DbTypeCollectionItem(Type valueType, DbType dbType, Func<object, object> valueResolver)
        {
            ValueType = valueType;
            DbType = dbType;
            ValueResolver = valueResolver;
        }
    }
}
