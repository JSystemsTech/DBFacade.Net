using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace DBFacade.DataLayer.Models.Attributes
{
    public enum CultureInfoType
    {
        InvariantCulture,
        DefaultThreadCurrentCulture,
        InstalledUICulture,
        CurrentUICulture,
        CurrentCulture,
        DefaultThreadCurrentUICulture
    }
    public sealed class DbDateStringColumn : DbColumn
    {
        private static Dictionary<CultureInfoType, CultureInfo> CultureInfoMap = new Dictionary<CultureInfoType, CultureInfo>()
        {
            {CultureInfoType.InvariantCulture, CultureInfo.InvariantCulture},
            {CultureInfoType.DefaultThreadCurrentCulture, CultureInfo.DefaultThreadCurrentCulture},
            {CultureInfoType.InstalledUICulture, CultureInfo.InstalledUICulture},
            {CultureInfoType.CurrentUICulture, CultureInfo.CurrentUICulture},
            {CultureInfoType.CurrentCulture, CultureInfo.CurrentCulture},
            {CultureInfoType.DefaultThreadCurrentUICulture, CultureInfo.DefaultThreadCurrentUICulture}
        };
        private CultureInfo CultureInfo;
        private string DateFormat;
        private DateTimeStyles DateTimeStyles;

        public DbDateStringColumn(string name, string dateFormat, CultureInfoType cultureInfo = CultureInfoType.InvariantCulture, DateTimeStyles dateTimeStyles = DateTimeStyles.None) : this(null, name, dateFormat, cultureInfo, dateTimeStyles) { }
        public DbDateStringColumn(Type TDbManifestMethodType, string name, string dateFormat, CultureInfoType cultureInfo = CultureInfoType.InvariantCulture, DateTimeStyles dateTimeStyles = DateTimeStyles.None) : base(TDbManifestMethodType, name)
        {
            DateFormat = dateFormat;
            CultureInfo = CultureInfoMap[cultureInfo];
            DateTimeStyles = dateTimeStyles;
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
            DateTime? value = GetValue<DateTime>(data);
            return value != null ? ((DateTime)value).ToString(DateFormat) : null;
        }
        private DateTime? GetDateTimeValue(IDataRecord data)
        {
            string valueStr = GetValue<string>(data);
            DateTime outValue = new DateTime();
            var hasDate = !string.IsNullOrWhiteSpace(valueStr)
                && DateTime.TryParseExact(valueStr, DateFormat, CultureInfo, DateTimeStyles, out outValue);
            if (hasDate)
            {
                return outValue;
            }
            return null;
        }
    }
}
