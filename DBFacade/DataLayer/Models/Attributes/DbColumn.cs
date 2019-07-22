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
        Type GetDbMethodType();
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
        private Type dbMethodType;
        /// <summary>
        /// The bound to database method
        /// </summary>
        private bool boundToDbMethod;
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
        /// <param name="dBMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        public DbColumn(Type dBMethodType, string name)
        {
            init(dBMethodType, name, null);
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
        /// <param name="dBMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="delimeter">The delimeter.</param>
        internal DbColumn(Type dBMethodType, string name, char delimeter)
        {
            init(dBMethodType, name, null, delimeter);
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
        /// <param name="dBMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        internal DbColumn(Type dBMethodType, string name, object defaultValue)
        {
            init(dBMethodType, name, defaultValue);
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
        /// <param name="dBMethodType">Type of the d b method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delimeter">The delimeter.</param>
        internal DbColumn(Type dBMethodType, string name, object defaultValue, char delimeter)
        {
            init(dBMethodType, name, defaultValue, delimeter);
        }


        /// <summary>
        /// Initializes the specified database method type.
        /// </summary>
        /// <param name="dbMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delimeter">The delimeter.</param>
        private void init(Type dbMethodType, string name, object defaultValue, char delimeter)
        {
            CheckDbMethodType(dbMethodType);
            this.dbMethodType = dbMethodType;
            boundToDbMethod = true;
            init(name, defaultValue, delimeter);
        }
        /// <summary>
        /// Initializes the specified database method type.
        /// </summary>
        /// <param name="dbMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        private void init(Type dbMethodType, string name, object defaultValue)
        {
            CheckDbMethodType(dbMethodType);
            this.dbMethodType = dbMethodType;
            boundToDbMethod = true;
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
        /// <param name="dbMethodType">Type of the database method.</param>
        /// <exception cref="ArgumentException"></exception>
        private void CheckDbMethodType(Type dbMethodType)
        {
            if (!dbMethodType.IsSubclassOf(typeof(DbManifest)))
            {
                throw new ArgumentException(dbMethodType.Name + " is not type of " + typeof(DbManifest).Name);
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
        /// <param name="dbMethodType">Type of the database method.</param>
        /// <exception cref="InvalidOperationException">type is not a DbMethod</exception>
        private void CheckIfIsValidDbMethod(Type dbMethodType)
        {
            if (!dbMethodType.IsSubclassOf(typeof(DbManifest)))
            {
                throw new InvalidOperationException("type is not a DbMethod");
            }
        }
        /// <summary>
        /// Gets the type of the database method.
        /// </summary>
        /// <returns></returns>
        public Type GetDbMethodType()
        {
            return dbMethodType;
        }
        /// <summary>
        /// Gets a value indicating whether [bound to database method type].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [bound to database method type]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool BoundToDbMethodType
        {
            get { return boundToDbMethod; }
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

