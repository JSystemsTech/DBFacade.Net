using DBFacade.DataLayer.Manifest;
using System;
using System.Data;
namespace DBFacade.DataLayer.Models.Attributes
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
        Type GetTDbManifestMethodType();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DbColumnCore" />
    /// <seealso cref="IDbColumn" />
    public class DbColumn : DbColumnCore, IDbColumn
    {
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// The default value
        /// </summary>
        private object defaultValue;
        /// <summary>
        /// The delimeter
        /// </summary>
        private char delimeter = ',';
        /// <summary>
        /// The database method type
        /// </summary>
        private Type TDbManifestMethodType;
        /// <summary>
        /// The bound to database method
        /// </summary>
        private bool boundToTDbManifestMethod;
        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        internal DbColumn()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DbColumn(string name)
        {
            init(name, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        public DbColumn(Type TDbManifestMethodType, string name)
        {
            init(TDbManifestMethodType, name, null);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="delimeter">The delimeter.</param>
        internal DbColumn(string name, char delimeter)
        {
            init(name, null, delimeter);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="delimeter">The delimeter.</param>
        internal DbColumn(Type TDbManifestMethodType, string name, char delimeter)
        {
            init(TDbManifestMethodType, name, null, delimeter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        internal DbColumn(string name, object defaultValue)
        {
            init(name, defaultValue);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        internal DbColumn(Type TDbManifestMethodType, string name, object defaultValue)
        {
            init(TDbManifestMethodType, name, defaultValue);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delimeter">The delimeter.</param>
        internal DbColumn(string name, object defaultValue, char delimeter)
        {
            init(name, defaultValue, delimeter);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumn"/> class.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delimeter">The delimeter.</param>
        internal DbColumn(Type TDbManifestMethodType, string name, object defaultValue, char delimeter)
        {
            init(TDbManifestMethodType, name, defaultValue, delimeter);
        }


        /// <summary>
        /// Initializes the specified database method type.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delimeter">The delimeter.</param>
        private void init(Type TDbManifestMethodType, string name, object defaultValue, char delimeter)
        {
            CheckTDbManifestMethodType(TDbManifestMethodType);
            this.TDbManifestMethodType = TDbManifestMethodType;
            boundToTDbManifestMethod = true;
            init(name, defaultValue, delimeter);
        }
        /// <summary>
        /// Initializes the specified database method type.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        private void init(Type TDbManifestMethodType, string name, object defaultValue)
        {
            CheckTDbManifestMethodType(TDbManifestMethodType);
            this.TDbManifestMethodType = TDbManifestMethodType;
            boundToTDbManifestMethod = true;
            init(name, defaultValue);
        }
        /// <summary>
        /// Initializes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delimeter">The delimeter.</param>
        private void init(string name, object defaultValue, char delimeter)
        {
            this.delimeter = delimeter;
            init(name, defaultValue);
        }
        /// <summary>
        /// Initializes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        private void init(string name, object defaultValue)
        {
            this.name = name;
            this.defaultValue = defaultValue;
        }
        /// <summary>
        /// Checks the type of the database method.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the database method.</param>
        /// <exception cref="ArgumentException"></exception>
        private void CheckTDbManifestMethodType(Type TDbManifestMethodType)
        {
            if (!TDbManifestMethodType.IsSubclassOf(typeof(DbManifest)))
            {
                throw new ArgumentException(TDbManifestMethodType.Name + " is not type of " + typeof(DbManifest).Name);
            }
        }
        /// <summary>
        /// Determines whether the specified data has column.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <c>true</c> if the specified data has column; otherwise, <c>false</c>.
        /// </returns>
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
        /// <summary>
        /// Gets the ordinal.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public int GetOrdinal(IDataRecord data)
        {
            return data.GetOrdinal(name);
        }
        /// <summary>
        /// Checks if is valid database method.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the database method.</param>
        /// <exception cref="InvalidOperationException">type is not a TDbManifestMethod</exception>
        private void CheckIfIsValidTDbManifestMethod(Type TDbManifestMethodType)
        {
            if (!TDbManifestMethodType.IsSubclassOf(typeof(DbManifest)))
            {
                throw new InvalidOperationException("type is not a TDbManifestMethod");
            }
        }
        /// <summary>
        /// Gets the type of the database method.
        /// </summary>
        /// <returns></returns>
        public Type GetTDbManifestMethodType()
        {
            return TDbManifestMethodType;
        }
        /// <summary>
        /// Gets a value indicating whether [bound to database method type].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [bound to database method type]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool BoundToTDbManifestMethodType
        {
            get { return boundToTDbManifestMethod; }
        }
        /// <summary>
        /// Gets the column value core.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="propType">Type of the property.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the column value base.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="propType">Type of the property.</param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        private class IgnorableDbColumnValue { }
        /// <summary>
        /// Gets the column value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        protected override object GetColumnValue(IDataRecord data)
        {
            return new IgnorableDbColumnValue();
        }
        /// <summary>
        /// Gets the column value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="propType">Type of the property.</param>
        /// <returns></returns>
        protected override object GetColumnValue(IDataRecord data, Type propType)
        {
            return new IgnorableDbColumnValue();
        }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="OutVal">The type of the ut value.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        internal OutVal GetValue<OutVal>(IDataRecord data)
        {
            object value = GetValue(data, typeof(OutVal));
            if (value == null)
            {
                value = default(OutVal);
            }
            return (OutVal)value;
        }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="outType">Type of the out.</param>
        /// <returns></returns>
        internal object GetValue(IDataRecord data, Type outType)
        {
            return GetColumnValueBase(data, outType);
        }
        
    }

}

