using DomainFacade.DataLayer.DbManifest;
using System;
using System.Data;
namespace DomainFacade.DataLayer.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor, Inherited = true, AllowMultiple = true)]
    public abstract class DbColumnCore : Attribute
    {
        protected abstract object GetColumnValue(IDataRecord data);
        protected abstract object GetColumnValue(IDataRecord data, Type propType);
    }
    public interface IDbColumn
    {
        object GetColumnValueCore(IDataRecord data, Type propType);
        int GetOrdinal(IDataRecord data);
        Type GetDbMethodType();
    }
    public class DbColumn : DbColumnCore, IDbColumn
    {

        private string name;
        private object defaultValue;
        private char delimeter = ',';
        private Type dbMethodType;
        private bool boundToDbMethod;
        internal DbColumn()
        {
        }
        public DbColumn(string name)
        {
            init(name, null);
        }

        public DbColumn(Type dBMethodType, string name)
        {
            init(dBMethodType, name, null);
        }       
        public DbColumn(string name, char delimeter)
        {
            init(name, null, delimeter);
        }
        public DbColumn(Type dBMethodType, string name, char delimeter)
        {
            init(dBMethodType, name, null, delimeter);
        }

        internal DbColumn(string name, object defaultValue)
        {
            init(name, defaultValue);
        }
        internal DbColumn(Type dBMethodType, string name, object defaultValue)
        {
            init(dBMethodType, name, defaultValue);
        }
        internal DbColumn(string name, object defaultValue, char delimeter)
        {
            init(name, defaultValue, delimeter);
        }
        internal DbColumn(Type dBMethodType, string name, object defaultValue, char delimeter)
        {
            init(dBMethodType, name, defaultValue, delimeter);
        }
        
        

        private void init(Type dbMethodType, string name, object defaultValue, char delimeter)
        {
            CheckDbMethodType(dbMethodType);
            this.dbMethodType = dbMethodType;
            boundToDbMethod = true;
            init(name, defaultValue, delimeter);
        }
        private void init(Type dbMethodType, string name, object defaultValue)
        {
            CheckDbMethodType(dbMethodType);
            this.dbMethodType = dbMethodType;
            boundToDbMethod = true;
            init(name, defaultValue);
        }
        private void init(string name, object defaultValue, char delimeter)
        {            
            this.delimeter = delimeter;
            init(name, defaultValue);
        }
        private void init(string name, object defaultValue)
        {
            this.name = name;
            this.defaultValue = defaultValue;
        }
        private void CheckDbMethodType(Type dbMethodType)
        {
            if (!dbMethodType.IsSubclassOf(typeof(DbManifest.DbManifest)))
            {
                throw new ArgumentException(dbMethodType.Name + " is not type of " + typeof(DbManifest.DbManifest).Name);
            }
        }
        public bool HasColumn(IDataRecord data)
        {
            try
            {
                return GetOrdinal(data) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
        public int GetOrdinal(IDataRecord data)
        {
            return data.GetOrdinal(name);
        }
        private void CheckIfIsValidDbMethod(Type dbMethodType)
        {
            if(!dbMethodType.IsSubclassOf(typeof(DbManifest.DbManifest))){
                throw new InvalidOperationException("type is not a DbMethod");
            }
        }
        public Type GetDbMethodType()
        {
            return dbMethodType;
        }
        public virtual bool BoundToDbMethodType
        {
            get { return boundToDbMethod; }
        }
        public object GetColumnValueCore(IDataRecord data, Type propType)
        {
            object value = GetColumnValue(data, propType);
            if (value.GetType() == typeof(IgnorableDbColumnValue))
            {
                value = GetColumnValue(data);
                if (value.GetType() == typeof(IgnorableDbColumnValue))
                {
                    value = GetColumnValueBase(data, propType);
                }
            }
            return value;
        }
        private object GetColumnValueBase(IDataRecord data, Type propType)
        {
            if (HasColumn(data) && !data.IsDBNull(GetOrdinal(data)) && DbColumnConversion.HasConverter(propType))
            {
                if (DbColumnConversion.isDelimitedArray(propType))
                {
                    return DbColumnConversion.Converters[propType].DynamicInvoke(data, GetOrdinal(data), delimeter);
                }
                return DbColumnConversion.Converters[propType].DynamicInvoke(data, GetOrdinal(data));
            }
            if (data.IsDBNull(GetOrdinal(data)) && defaultValue != null)
            {
                return defaultValue;
            }
            return null;
        }
        private class IgnorableDbColumnValue { }
        protected override object GetColumnValue(IDataRecord data)
        {
            return new IgnorableDbColumnValue();
        }
        protected override object GetColumnValue(IDataRecord data, Type propType)
        {
            return new IgnorableDbColumnValue();
        }
        internal OutVal GetValue<OutVal>(IDataRecord data)
        {
            object value = GetValue(data, typeof(OutVal));
            if (value == null)
            {
                value = default(OutVal);
            }
            return (OutVal) value;
        }
        internal object GetValue(IDataRecord data, Type outType)
        {
            return GetColumnValueBase(data, outType);
        }


        public sealed class Boolean : DbColumn
        {
            public Boolean(string name, bool defaultValue) : base(name, defaultValue) { }
            public Boolean(Type dbMethodType, string name, bool defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Byte : DbColumn
        {
            public Byte(string name, byte defaultValue) : base(name, defaultValue) { }
            public Byte(Type dbMethodType, string name, byte defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Char : DbColumn
        {
            public Char(string name, char defaultValue) : base(name, defaultValue) { }
            public Char(Type dbMethodType, string name, char defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class DateTime : DbColumn
        {
            public DateTime(string name, System.DateTime defaultValue) : base(name, defaultValue) { }
            public DateTime(Type dbMethodType, string name, System.DateTime defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Decimal : DbColumn
        {
            public Decimal(string name, decimal defaultValue) : base(name, defaultValue) { }
            public Decimal(Type dbMethodType, string name, decimal defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Double : DbColumn
        {
            public Double(string name, double defaultValue) : base(name, defaultValue) { }
            public Double(Type dbMethodType, string name, double defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Float : DbColumn
        {
            public Float(string name, float defaultValue) : base(name, defaultValue) { }
            public Float(Type dbMethodType, string name, float defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Guid : DbColumn
        {
            public Guid(string name, Guid defaultValue) : base(name, defaultValue) { }
            public Guid(Type dbMethodType, string name, Guid defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Short : DbColumn
        {
            public Short(string name, short defaultValue) : base(name, defaultValue) { }
            public Short(Type dbMethodType, string name, short defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Int : DbColumn
        {
            public Int(string name, int defaultValue) : base(name, defaultValue) { }
            public Int(Type dbMethodType, string name, int defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class Long : DbColumn
        {
            public Long(string name, long defaultValue) : base(name, defaultValue) { }
            public Long(Type dbMethodType, string name, long defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class UShort : DbColumn
        {
            public UShort(string name, ushort defaultValue) : base(name, defaultValue) { }
            public UShort(Type dbMethodType, string name, ushort defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class UInt : DbColumn
        {
            public UInt(string name, uint defaultValue) : base(name, defaultValue) { }
            public UInt(Type dbMethodType, string name, uint defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class ULong : DbColumn
        {
            public ULong(string name, ulong defaultValue) : base(name, defaultValue) { }
            public ULong(Type dbMethodType, string name, ulong defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
        public sealed class String : DbColumn
        {
            public String(string name, string defaultValue) : base(name, defaultValue) { }
            public String(Type dbMethodType, string name, string defaultValue) : base(dbMethodType, name, defaultValue) { }
        }
    }
   
}

