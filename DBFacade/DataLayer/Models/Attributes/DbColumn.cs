using System;
using System.Data;
using DBFacade.DataLayer.Manifest;

namespace DBFacade.DataLayer.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = true)]
    public abstract class DbColumnCore : Attribute
    {
        protected abstract object GetColumnValue(IDataRecord data, Type propType = null);
    }

    internal interface IDbColumn
    {
        object GetColumnValueCore(IDataRecord data, Type propType = null);
        int GetOrdinal(IDataRecord data);
        Type GetTDbMethodManifestMethodType();
    }

    public class DbColumn : DbColumnCore, IDbColumn
    {
        internal const char DefaultDelimeter = ',';
        private static readonly IgnorableDbColumnValue ignorableDbColumnValue = new IgnorableDbColumnValue();
        private readonly object defaultValue;

        private readonly string name;

        private readonly Type TDbMethodManifestMethodType;

        internal DbColumn()
        {
        }

        public DbColumn(string name, char delimeter = DefaultDelimeter)
            : this(null, name, null, null, delimeter)
        {
        }

        public DbColumn(Type TDbMethodManifestMethodType, string name, char delimeter = DefaultDelimeter)
            : this(TDbMethodManifestMethodType, name, null, delimeter)
        {
        }

        public DbColumn(string name, int bufferSize)
            : this(null, name, null, bufferSize)
        {
        }

        public DbColumn(Type TDbMethodManifestMethodType, string name, int bufferSize)
            : this(TDbMethodManifestMethodType, name, null, bufferSize)
        {
        }


        internal DbColumn(string name, object defaultValue, char delimeter = DefaultDelimeter)
            : this(null, name, defaultValue, null, delimeter)
        {
        }

        internal DbColumn(Type TDbMethodManifestMethodType, string name, object defaultValue, int? bufferSize,
            char delimeter = DefaultDelimeter)
        {
            this.delimeter = delimeter;
            this.name = name;
            this.defaultValue = defaultValue;
            if (TDbMethodManifestMethodType != null)
            {
                CheckTDbMethodManifestMethodType(TDbMethodManifestMethodType);
                this.TDbMethodManifestMethodType = TDbMethodManifestMethodType;
                BoundToTDbMethodManifestMethodType = true;
            }
        }

        private char delimeter { get; }
        private int BufferSize { get; set; }

        public virtual bool BoundToTDbMethodManifestMethodType { get; }

        public int GetOrdinal(IDataRecord data)
        {
            return data.GetOrdinal(name);
        }

        public Type GetTDbMethodManifestMethodType()
        {
            return TDbMethodManifestMethodType;
        }

        public object GetColumnValueCore(IDataRecord data, Type propType)
        {
            var value = GetColumnValue(data, propType);
            return IsIgnorableValue(value) ? GetColumnValueBase(data, propType) : value;
        }

        private void CheckTDbMethodManifestMethodType(Type tDbMethodManifestMethodType)
        {
            if (!tDbMethodManifestMethodType.IsSubclassOf(typeof(DbMethodManifest)))
                throw new ArgumentException(
                    $"{tDbMethodManifestMethodType.Name} is not type of {typeof(DbMethodManifest).Name}");
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

        protected bool HasColumnValue(IDataRecord data)
        {
            return HasColumn(data) && !data.IsDBNull(GetOrdinal(data));
        }

        private void CheckIfIsValidTDbMethodManifestMethod(Type tDbMethodManifestMethodType)
        {
            if (!tDbMethodManifestMethodType.IsSubclassOf(typeof(DbMethodManifest)))
                throw new InvalidOperationException("type is not a TDbMethodManifestMethod");
        }

        private bool IsIgnorableValue(object value)
        {
            return value.GetType() == typeof(IgnorableDbColumnValue);
        }

        private object GetColumnValueBase(IDataRecord data, Type propType)
        {
            var hasData = HasColumn(data);
            var hasNullValue = hasData && data.IsDBNull(GetOrdinal(data));
            return !hasNullValue
                ? DbColumnConversion.Convert(propType, data, GetOrdinal(data), BufferSize, delimeter, defaultValue)
                : defaultValue;
        }

        protected override object GetColumnValue(IDataRecord data, Type propType = null)
        {
            return ignorableDbColumnValue;
        }

        protected OutVal GetValue<OutVal>(IDataRecord data)
        {
            var value = GetValue(data, typeof(OutVal));
            return value == null ? default(OutVal) : (OutVal) value;
        }

        protected object GetValue(IDataRecord data, Type outType)
        {
            return GetColumnValueBase(data, outType);
        }

        private class IgnorableDbColumnValue
        {
        }
    }
}