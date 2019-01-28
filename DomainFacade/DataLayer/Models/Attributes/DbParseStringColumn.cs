using System;
using System.Data;
using System.Globalization;

namespace DomainFacade.DataLayer.Models.Attributes
{
    public sealed class DbParseStringColumn : DbColumn
    {
        public DbParseStringColumn(string name) : base(name) { }
        public DbParseStringColumn(string name, object defaultValue) : base(name, defaultValue) { }
        public DbParseStringColumn(Type dbMethodType, string name) : base(dbMethodType, name) { }
        public DbParseStringColumn(Type dbMethodType, string name, object defaultValue) : base(dbMethodType, name, defaultValue) { }
        private string dateFormat;
        public DbParseStringColumn(string name, string dateFormat) : base(name)
        {
            this.dateFormat = dateFormat;
        }
        public DbParseStringColumn(string name, System.DateTime defaultValue, string dateFormat) : base(name, defaultValue)
        {
            this.dateFormat = dateFormat;
        }
        public DbParseStringColumn(Type dbMethodType, string name, string dateFormat) : base(dbMethodType, name)
        {
            this.dateFormat = dateFormat;
        }
        public DbParseStringColumn(Type dbMethodType, string name, System.DateTime defaultValue, string dateFormat) : base(dbMethodType, name, defaultValue)
        {
            this.dateFormat = dateFormat;
        }

        protected override object GetColumnValue(IDataRecord data, Type propType)
        {
            string value = GetValue<string>(data);
            if (value != null)
            {
                return Transform(value, propType);
            }
            return null;
        }
        private static Func<string, bool> AsBoolean = (value) => bool.Parse(value);
        private static Func<string, decimal> AsDecimal = (value) => decimal.Parse(value);
        private static Func<string, double> AsDouble = (value) => double.Parse(value);
        private static Func<string, float> AsFloat = (value) => float.Parse(value);
        private static Func<string, short> AsShort = (value) => short.Parse(value);
        private static Func<string, int> AsInt = (value) => int.Parse(value);
        private static Func<string, long> AsLong = (value) => long.Parse(value);
        private static Func<string, System.DateTime> AsDateTime = (value) => System.DateTime.Parse(value);
        private static Func<string, string, System.DateTime> AsDateTimeFormat = (value, dateFormat) => System.DateTime.ParseExact(value, dateFormat, CultureInfo.InvariantCulture);
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
