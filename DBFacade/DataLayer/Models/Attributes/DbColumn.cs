using DBFacade.DataLayer.Manifest;
using System;
using System.Data;
namespace DBFacade.DataLayer.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor, Inherited = true, AllowMultiple = true)]
    public abstract class DbColumnCore : Attribute
    {
        protected abstract object GetColumnValue(IDataRecord data, Type propType = null);
    }
    interface IDbColumn
    {
        object GetColumnValueCore(IDataRecord data, Type propType = null);
        int GetOrdinal(IDataRecord data);
        Type GetTDbManifestMethodType();
    }
    public class DbColumn : DbColumnCore, IDbColumn
    {
        internal const char DefaultDelimeter = ',';
        private char delimeter { get; set; }
        private int BufferSize { get; set; }

        private string name;
        private object defaultValue;
        
        private Type TDbManifestMethodType;
        private bool boundToTDbManifestMethod;
        internal DbColumn() { }

        public DbColumn(string name, char delimeter = DefaultDelimeter)
            : this(null, name, null,null, delimeter) { }
        public DbColumn(Type TDbManifestMethodType, string name, char delimeter = DefaultDelimeter)
            : this(TDbManifestMethodType, name, null, delimeter) { }

        public DbColumn(string name, int bufferSize)
            : this(null, name, null, bufferSize, DefaultDelimeter) { }
        public DbColumn(Type TDbManifestMethodType, string name, int bufferSize)
            : this(TDbManifestMethodType, name, null, bufferSize, DefaultDelimeter) { }


        internal DbColumn(string name, object defaultValue, char delimeter = DefaultDelimeter)
            :this(null, name, defaultValue,null, delimeter){}
        internal DbColumn(Type TDbManifestMethodType, string name, object defaultValue, int? bufferSize, char delimeter = DefaultDelimeter)            
        {                        
            this.delimeter = delimeter;
            this.name = name;
            this.defaultValue = defaultValue;
            if(TDbManifestMethodType != null)
            {
                CheckTDbManifestMethodType(TDbManifestMethodType);
                this.TDbManifestMethodType = TDbManifestMethodType;
                boundToTDbManifestMethod = true;
            }            
        }        
        
        private void CheckTDbManifestMethodType(Type TDbManifestMethodType)
        {
            if (!TDbManifestMethodType.IsSubclassOf(typeof(DbManifest)))
            {
                throw new ArgumentException($"{TDbManifestMethodType.Name} is not type of {typeof(DbManifest).Name}");
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
        protected bool HasColumnValue(IDataRecord data) => HasColumn(data) && !data.IsDBNull(GetOrdinal(data));
        
        public int GetOrdinal(IDataRecord data)
        {
            return data.GetOrdinal(name);
        }
        private void CheckIfIsValidTDbManifestMethod(Type TDbManifestMethodType)
        {
            if (!TDbManifestMethodType.IsSubclassOf(typeof(DbManifest)))
            {
                throw new InvalidOperationException("type is not a TDbManifestMethod");
            }
        }
        public Type GetTDbManifestMethodType()
        {
            return TDbManifestMethodType;
        }
        public virtual bool BoundToTDbManifestMethodType
        {
            get { return boundToTDbManifestMethod; }
        }
        private bool IsIgnorableValue(object value) => value.GetType() == typeof(IgnorableDbColumnValue);
        public object GetColumnValueCore(IDataRecord data, Type propType)
        {
            object value = GetColumnValue(data, propType);
            return IsIgnorableValue(value) ? GetColumnValueBase(data, propType) : value;
        }
        private object GetColumnValueBase(IDataRecord data, Type propType)
        {
            bool hasData = HasColumn(data);
            bool hasNullValue = hasData && data.IsDBNull(GetOrdinal(data));
            return !hasNullValue ? DbColumnConversion.Convert(propType, data, GetOrdinal(data),BufferSize, delimeter, defaultValue) :
                defaultValue != null ? defaultValue : 
                null;
        }
        private class IgnorableDbColumnValue { }
        private static IgnorableDbColumnValue ignorableDbColumnValue = new IgnorableDbColumnValue();
        protected override object GetColumnValue(IDataRecord data, Type propType = null) => ignorableDbColumnValue;

        protected OutVal GetValue<OutVal>(IDataRecord data)
        {
            object value = GetValue(data, typeof(OutVal));
            return value == null ? default(OutVal) : (OutVal)value;
        }
        protected object GetValue(IDataRecord data, Type outType) => GetColumnValueBase(data, outType);
    }

}

