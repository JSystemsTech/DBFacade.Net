using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : DbParamsModel
    {
        
        internal sealed class IsDateTimeRule : ValidationRule<TDbParams>
        {
            private IsDateTimeRule() { }
            public IsDateTimeRule(Expression<Func<TDbParams, string>> selector, string dateFormat = null,
                bool isNullable = false, CultureInfo cultureInfo = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
            {
                DateFormat = dateFormat;
                CultureInfo = cultureInfo is CultureInfo ci ? ci : CultureInfo.InvariantCulture;
                DateTimeStyles = dateTimeStyles;
                Init(selector, isNullable);
            }
            public static async Task<IsDateTimeRule>CreateAsync(
                Expression<Func<TDbParams, string>> selector,
                string dateFormat = null, 
                bool isNullable = false,                 
                CultureInfo cultureInfo = null, 
                DateTimeStyles dateTimeStyles = DateTimeStyles.None)
            {
                IsDateTimeRule rule = new IsDateTimeRule();
                rule.DateFormat = dateFormat;
                rule.CultureInfo = cultureInfo is CultureInfo ci ? ci : CultureInfo.InvariantCulture;
                rule.DateTimeStyles = dateTimeStyles;
                await rule.InitAsync(selector, isNullable);
                return rule;
            }

            private string DateFormat { get; set; }
            private CultureInfo CultureInfo { get; set; }
            private DateTimeStyles DateTimeStyles { get; set; }


            private DateTime? GetDateTimeFromString(string value)
            {
                DateTime? dateTime = null;
                DateTime dateTimeParsed;
                bool isDate = !string.IsNullOrEmpty(DateFormat)
                ? DateTime.TryParseExact(value, DateFormat, CultureInfo, DateTimeStyles, out dateTimeParsed)
                : DateTime.TryParse(value, CultureInfo, DateTimeStyles, out dateTimeParsed);
                if (isDate)
                {
                    dateTime = dateTimeParsed;
                }
                return dateTime;
            }
            private async Task<DateTime?> GetDateTimeFromStringAsync(string value)
            {
                DateTime? dateTime = null;
                DateTime dateTimeParsed;
                bool isDate = !string.IsNullOrEmpty(DateFormat)
                ? DateTime.TryParseExact(value, DateFormat, CultureInfo, DateTimeStyles, out dateTimeParsed)
                : DateTime.TryParse(value, CultureInfo, DateTimeStyles, out dateTimeParsed);
                if (isDate)
                {
                    dateTime = dateTimeParsed;
                }
                await Task.CompletedTask;
                return dateTime;
            }
            private DateTime? GetDateTime()
            {
                return ParamsValue is DateTime date ? date : ParamsValue is string dateStr ? GetDateTimeFromString(dateStr) : null;
            }
            private async Task<DateTime?> GetDateTimeAsync()
            {
                if (ParamsValue is string dateStr)
                {
                    return await GetDateTimeFromStringAsync(dateStr);
                }
                DateTime? dateTime = null;
                if (ParamsValue is DateTime date)
                {
                    dateTime = date;
                }
                await Task.CompletedTask;
                return dateTime;
            }
            protected override string GetErrorMessageCore(string propertyName)
            => $"{propertyName} is not a Valid DateTime.";
            protected override async Task<string> GetErrorMessageCoreAsync(string propertyName)
            {
                await Task.CompletedTask;
                return $"{propertyName} is not a Valid DateTime.";
            }

            protected override bool ValidateRule()
            => GetDateTime() is DateTime;

            protected override async Task<bool> ValidateRuleAsync()
            => await GetDateTimeAsync() is DateTime;
        }

    }
}