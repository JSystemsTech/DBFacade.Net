using System;
using System.Data;
using System.Globalization;

namespace DBFacade.DataLayer.Models.Attributes
{
    public sealed class DbDateStringColumn : DbColumn
    {
        private string DateFormat;
        public DbDateStringColumn(string name, string dateFormat) : base(name)
        {
            DateFormat = dateFormat;
        }
        
        public DbDateStringColumn(Type TDbManifestMethodType, string name, string dateFormat) : base(TDbManifestMethodType, name)
        {
            DateFormat = dateFormat;
        }        
        
        protected override object GetColumnValue(IDataRecord data, Type propType)
        {           
            if (propType.Equals(typeof(DateTime)))
            {
                return GetDateTimeValue(data);
            }
            else if (propType.Equals(typeof(string)))
            {
                return GetStringValue(data);
            }
            return null;
        }
        private string GetStringValue(IDataRecord data)
        {
            DateTime value = GetValue<DateTime>(data);
            if (value != null)
            {
                return value.ToString(DateFormat);
            }
            return null;
        }
        private DateTime? GetDateTimeValue(IDataRecord data)
        {
            string value = GetValue<string>(data);
            if (value != null)
            {
                return DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
            }
            return null;
        }
    }
}
