using System;
using System.Data;
using System.Globalization;

namespace DBFacade.DataLayer.Models.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DbColumn" />
    public sealed class DbParseStringColumn : DbColumn
    {
        public DbParseStringColumn(string name) : base(name) { }
        public DbParseStringColumn(string name, object defaultValue) : base(name, defaultValue) { }
        
        public DbParseStringColumn(Type TDbManifestMethodType, string name) : base(TDbManifestMethodType, name) { }
        
        public DbParseStringColumn(Type TDbManifestMethodType, string name, object defaultValue) : base(TDbManifestMethodType, name, defaultValue) { }
        
        private string dateFormat;
        
        public DbParseStringColumn(string name, string dateFormat) : base(name)
        {
            this.dateFormat = dateFormat;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbParseStringColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="dateFormat">The date format.</param>
        public DbParseStringColumn(string name, System.DateTime defaultValue, string dateFormat) : base(name, defaultValue)
        {
            this.dateFormat = dateFormat;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbParseStringColumn"/> class.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="dateFormat">The date format.</param>
        public DbParseStringColumn(Type TDbManifestMethodType, string name, string dateFormat) : base(TDbManifestMethodType, name)
        {
            this.dateFormat = dateFormat;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbParseStringColumn"/> class.
        /// </summary>
        /// <param name="TDbManifestMethodType">Type of the database method.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="dateFormat">The date format.</param>
        public DbParseStringColumn(Type TDbManifestMethodType, string name, System.DateTime defaultValue, string dateFormat) : base(TDbManifestMethodType, name, defaultValue)
        {
            this.dateFormat = dateFormat;
        }

        /// <summary>
        /// Gets the column value.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="propType">Type of the property.</param>
        /// <returns></returns>
        protected override object GetColumnValue(IDataRecord data, Type propType)
        {
            string value = GetValue<string>(data);
            if (value != null)
            {
                return Transform(value, propType);
            }
            return null;
        }
        /// <summary>
        /// As boolean
        /// </summary>
        private static Func<string, bool> AsBoolean = (value) => bool.Parse(value);
        /// <summary>
        /// As decimal
        /// </summary>
        private static Func<string, decimal> AsDecimal = (value) => decimal.Parse(value);
        /// <summary>
        /// As double
        /// </summary>
        private static Func<string, double> AsDouble = (value) => double.Parse(value);
        /// <summary>
        /// As float
        /// </summary>
        private static Func<string, float> AsFloat = (value) => float.Parse(value);
        /// <summary>
        /// As short
        /// </summary>
        private static Func<string, short> AsShort = (value) => short.Parse(value);
        /// <summary>
        /// As int
        /// </summary>
        private static Func<string, int> AsInt = (value) => int.Parse(value);
        /// <summary>
        /// As long
        /// </summary>
        private static Func<string, long> AsLong = (value) => long.Parse(value);
        /// <summary>
        /// As date time
        /// </summary>
        private static Func<string, System.DateTime> AsDateTime = (value) => System.DateTime.Parse(value);
        /// <summary>
        /// As date time format
        /// </summary>
        private static Func<string, string, System.DateTime> AsDateTimeFormat = (value, dateFormat) => System.DateTime.ParseExact(value, dateFormat, CultureInfo.InvariantCulture);
        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="propType">Type of the property.</param>
        /// <returns></returns>
        private object Transform(string value, Type propType)
        {
            try
            {
                if (propType == typeof(bool))
                {
                    return AsBoolean(value);
                }
                if (propType == typeof(decimal))
                {
                    return AsDecimal(value);
                }
                else if (propType == typeof(decimal))
                {
                    return AsDouble(value);
                }
                else if (propType == typeof(double))
                {
                    return AsDecimal(value);
                }
                else if (propType == typeof(float))
                {
                    return AsFloat(value);
                }
                else if (propType == typeof(short))
                {
                    return AsShort(value);
                }
                else if (propType == typeof(int))
                {
                    return AsInt(value);
                }
                else if (propType == typeof(long))
                {
                    return AsLong(value);
                }
                else if (propType == typeof(System.DateTime))
                {
                    if (dateFormat != null)
                    {
                        return AsDateTimeFormat(value, dateFormat);
                    }
                    return AsDateTime(value);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }

    }
}
