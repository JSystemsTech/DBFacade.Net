using System;
using System.Data;
using DBFacade.DataLayer.Manifest;
using DBFacade.Exceptions;

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
        internal const char DefaultDelimiter = ',';
        private static readonly IgnorableDbColumnValue IgnorableDbColumnValueProp = new IgnorableDbColumnValue();
        private readonly object _defaultValue;

        private readonly string _name;

        private readonly Type _tDbMethodManifestMethodType;

        internal DbColumn()
        {
        }

        public DbColumn(string name, char delimiter = DefaultDelimiter)
            : this(null, name, null, null, delimiter)
        {
        }

        public DbColumn(Type tDbMethodManifestMethodType, string name, char delimiter = DefaultDelimiter)
            : this(tDbMethodManifestMethodType, name, null, delimiter)
        {
        }

        public DbColumn(string name, int bufferSize)
            : this(null, name, null, bufferSize)
        {
        }

        public DbColumn(Type tDbMethodManifestMethodType, string name, int bufferSize)
            : this(tDbMethodManifestMethodType, name, null, bufferSize)
        {
        }


        internal DbColumn(string name, object defaultValue, char delimiter = DefaultDelimiter)
            : this(null, name, defaultValue, null, delimiter)
        {
        }

        internal DbColumn(Type tDbMethodManifestMethodType, string name, object defaultValue, int? bufferSize,
            char delimiter = DefaultDelimiter)
        {
            Delimiter = delimiter;
            _name = name;
            _defaultValue = defaultValue;
            BufferSize = bufferSize ?? 0;
            if (tDbMethodManifestMethodType != null)
            {
                CheckTDbMethodManifestMethodType(tDbMethodManifestMethodType);
                _tDbMethodManifestMethodType = tDbMethodManifestMethodType;
                BoundToTDbMethodManifestMethodType = true;
            }
        }

        private char Delimiter { get; }
        private int BufferSize { get; }

        public virtual bool BoundToTDbMethodManifestMethodType { get; }

        public int GetOrdinal(IDataRecord data)
        {
            return data.GetOrdinal(_name);
        }

        public Type GetTDbMethodManifestMethodType()
        {
            return _tDbMethodManifestMethodType;
        }

        public object GetColumnValueCore(IDataRecord data, Type propType)
        {
            if (!HasColumnValue(data))
            {
                return null;
            }
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
            catch (Exception e)
            {
                throw new DataModelConstructionException($"Error finding column '{_name}'", e);
            }
        }

        protected bool HasColumnValue(IDataRecord data)
        {
            return HasColumn(data) && !data.IsDBNull(GetOrdinal(data));
        }
        

        private bool IsIgnorableValue(object value)
        {
            return value.GetType() == typeof(IgnorableDbColumnValue);
        }

        private object GetColumnValueBase(IDataRecord data, Type propType)
        {
            var hasData = HasColumn(data);
            var hasNullValue = hasData && data.IsDBNull(GetOrdinal(data));
            try
            {
                return !hasNullValue
                    ? DbColumnConversion.Convert(propType, data, GetOrdinal(data), BufferSize, Delimiter, _defaultValue)
                    : _defaultValue;
            }
            catch (Exception e)
            {
                throw new DataModelConstructionException($"Error converting Column {_name}: Expected type {propType.Name} Actual {data.GetFieldType(GetOrdinal(data)).Name}", e);
            }

            
        }

        protected override object GetColumnValue(IDataRecord data, Type propType = null)
        {
            return IgnorableDbColumnValueProp;
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