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
        private static readonly Dictionary<CultureInfoType, CultureInfo> CultureInfoMap =
            new Dictionary<CultureInfoType, CultureInfo>
            {
                {CultureInfoType.InvariantCulture, CultureInfo.InvariantCulture},
                {CultureInfoType.DefaultThreadCurrentCulture, CultureInfo.DefaultThreadCurrentCulture},
                {CultureInfoType.InstalledUICulture, CultureInfo.InstalledUICulture},
                {CultureInfoType.CurrentUICulture, CultureInfo.CurrentUICulture},
                {CultureInfoType.CurrentCulture, CultureInfo.CurrentCulture},
                {CultureInfoType.DefaultThreadCurrentUICulture, CultureInfo.DefaultThreadCurrentUICulture}
            };

        private readonly CultureInfo _cultureInfo;
        private readonly string _dateFormat;
        private readonly DateTimeStyles _dateTimeStyles;

        public DbDateStringColumn(string name, string dateFormat,
            CultureInfoType cultureInfo = CultureInfoType.InvariantCulture,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None) : this(null, name, dateFormat, cultureInfo,
            dateTimeStyles)
        {
        }

        public DbDateStringColumn(Type tDbMethodManifestMethodType, string name, string dateFormat,
            CultureInfoType cultureInfo = CultureInfoType.InvariantCulture,
            DateTimeStyles dateTimeStyles = DateTimeStyles.None) : base(tDbMethodManifestMethodType, name)
        {
            _dateFormat = dateFormat;
            _cultureInfo = CultureInfoMap[cultureInfo];
            _dateTimeStyles = dateTimeStyles;
        }

        protected override object GetColumnValue(IDataRecord data, Type propType = null)
        {
            if (propType.Equals(typeof(DateTime)))
                return GetDateTimeValue(data);
            if (propType.Equals(typeof(string))) return GetStringValue(data);
            return null;
        }

        private string GetStringValue(IDataRecord data)
        {
            var value = GetValue<DateTime?>(data);
            if (value is DateTime convertedValue)
            {
                return convertedValue.ToString(_dateFormat);
            }
            return null;
        }

        private DateTime? GetDateTimeValue(IDataRecord data)
        {
            var valueStr = GetValue<string>(data);
            var outValue = new DateTime();
            var hasDate = !string.IsNullOrWhiteSpace(valueStr)
                          && DateTime.TryParseExact(valueStr, _dateFormat, _cultureInfo, _dateTimeStyles, out outValue);
            if (hasDate) return outValue;
            return null;
        }
    }
}