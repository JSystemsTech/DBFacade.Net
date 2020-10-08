using System;
using System.Data;
using System.Threading.Tasks;
using DbFacade.DataLayer.Manifest;
using DbFacade.Exceptions;
using DbFacadeShared.DataLayer.Models;
using DbFacadeShared.Extensions;

namespace DbFacade.DataLayer.Models.Attributes
{

    internal interface IDbColumn
    {
        object GetValue(IDataRecord data, Type propType);
        Task<object> GetValueAsync(IDataRecord data, Type propType);
        Type TDbMethodManifestMethodType { get; }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = true)]
    public class DbColumn : Attribute, IDbColumn
    {
        internal const char DefaultDelimiter = ',';
        private readonly object _defaultValue;

        private readonly string _name;

        private readonly Type _tDbMethodManifestMethodType;

        internal DbColumn() { }

        public DbColumn(string name, char delimiter = DefaultDelimiter)
            : this(null, name, null, null, delimiter) { }

        public DbColumn(Type tDbMethodManifestMethodType, string name, char delimiter = DefaultDelimiter)
            : this(tDbMethodManifestMethodType, name, null, delimiter) { }

        public DbColumn(string name, int bufferSize)
            : this(null, name, null, bufferSize) { }

        public DbColumn(Type tDbMethodManifestMethodType, string name, int bufferSize)
            : this(tDbMethodManifestMethodType, name, null, bufferSize) { }


        internal DbColumn(string name, object defaultValue, char delimiter = DefaultDelimiter)
            : this(null, name, defaultValue, null, delimiter) { }

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


        public Type TDbMethodManifestMethodType { get => _tDbMethodManifestMethodType; }     
        

        private void CheckTDbMethodManifestMethodType(Type tDbMethodManifestMethodType)
        {
            if (!tDbMethodManifestMethodType.IsSubclassOf(typeof(DbMethodManifest)))
                throw new ArgumentException(
                    $"{tDbMethodManifestMethodType.Name} is not type of {typeof(DbMethodManifest).Name}");
        }       

            

        protected object TryGetValue(IDataRecord data, Type propType)
        {
            try
            {
                return data.GetColumn(_name, propType, BufferSize, Delimiter, _defaultValue);
            }
            catch (Exception e)
            {
                return DbDataModelBindingError.Create(e, propType, data.GetFieldType(data.GetOrdinal(_name)), _name);
            }
        }
        protected async Task<object> TryGetValueAsync (IDataRecord data, Type propType)
        {
            try
            {
                return await data.GetColumnAsync(_name, propType, BufferSize, Delimiter, _defaultValue);                
            }
            catch (Exception e)
            {
                 return await DbDataModelBindingError.CreateAsync(e, propType, data.GetFieldType(data.GetOrdinal(_name)), _name);
            }
        }

        public virtual object GetValue(IDataRecord data, Type propType)
        => TryGetValue(data, propType);
        public virtual async Task<object> GetValueAsync(IDataRecord data, Type propType)
        => await TryGetValueAsync(data, propType);
    }
}