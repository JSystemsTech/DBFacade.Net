using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Attributes
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
            DateTimeStyles dateTimeStyles = DateTimeStyles.None) : base(name)
        {
            _dateFormat = dateFormat;
            _cultureInfo = CultureInfoMap[cultureInfo];
            _dateTimeStyles = dateTimeStyles;
        }

        public override object GetValue(IDataRecord data, Type propType, object currentValue)
        {
            if (propType.Equals(typeof(DateTime)))
                return GetDateTimeValue(data, currentValue);
            if (propType.Equals(typeof(string))) return GetStringValue(data, currentValue);
            return null;
        }
        public override async Task<object> GetValueAsync(IDataRecord data, Type propType, object currentValue)
        {
            if (propType.Equals(typeof(DateTime)))
                return await GetDateTimeValueAsync(data, currentValue);
            if (propType.Equals(typeof(string))) return await  GetStringValueAsync(data, currentValue);
            await Task.CompletedTask;
            return null;
        }

        private string GetStringValue(IDataRecord data, object currentValue)
        {
            var value = TryGetValue(data, typeof(DateTime?), currentValue);
            if (value is DateTime convertedValue)
            {
                return convertedValue.ToString(_dateFormat);
            }
            return null;
        }

        private DateTime? GetDateTimeValue(IDataRecord data, object currentValue)
        {
            var value = TryGetValue(data, typeof(string), currentValue);
            var outValue = new DateTime();
            var hasDate = value is string valueStr && !string.IsNullOrWhiteSpace(valueStr)
                          && DateTime.TryParseExact(valueStr, _dateFormat, _cultureInfo, _dateTimeStyles, out outValue);
            if (hasDate) return outValue;
            return null;
        }

        private async Task<string> GetStringValueAsync(IDataRecord data, object currentValue)
        {
            var value = await TryGetValueAsync(data, typeof(DateTime?), currentValue);
            if (value is DateTime convertedValue)
            {
                await Task.CompletedTask;
                return convertedValue.ToString(_dateFormat);
            }
            return null;
        }

        private async Task<DateTime?> GetDateTimeValueAsync(IDataRecord data, object currentValue)
        {
            var value = await TryGetValueAsync(data, typeof(string), currentValue);
            var outValue = new DateTime();
            var hasDate = value is string valueStr && !string.IsNullOrWhiteSpace(valueStr)
                          && DateTime.TryParseExact(valueStr, _dateFormat, _cultureInfo, _dateTimeStyles, out outValue);
            if (hasDate)
            {
                await Task.CompletedTask;
                return outValue;
            }
            await Task.CompletedTask;
            return null;
        }
    }
}