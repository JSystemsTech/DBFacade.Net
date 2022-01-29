using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public abstract partial class ValidationRule<TDbParams>
    {

        /// <summary>
        /// 
        /// </summary>
        internal sealed class IsDateTimeRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="IsDateTimeRule" /> class from being created.
            /// </summary>
            private IsDateTimeRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="IsDateTimeRule" /> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateFormat">The date format.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <param name="cultureInfo">The culture information.</param>
            /// <param name="dateTimeStyles">The date time styles.</param>
            public IsDateTimeRule(Func<TDbParams, string> selector, string dateFormat = null,
                bool isNullable = false, CultureInfo cultureInfo = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
            {
                DateFormat = dateFormat;
                CultureInfo = cultureInfo is CultureInfo ci ? ci : CultureInfo.InvariantCulture;
                DateTimeStyles = dateTimeStyles;
                Init(selector, isNullable);
            }

            /// <summary>
            /// Gets or sets the date format.
            /// </summary>
            /// <value>
            /// The date format.
            /// </value>
            private string DateFormat { get; set; }
            /// <summary>
            /// Gets or sets the culture information.
            /// </summary>
            /// <value>
            /// The culture information.
            /// </value>
            private CultureInfo CultureInfo { get; set; }
            /// <summary>
            /// Gets or sets the date time styles.
            /// </summary>
            /// <value>
            /// The date time styles.
            /// </value>
            private DateTimeStyles DateTimeStyles { get; set; }


            /// <summary>
            /// Gets the date time from string.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
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
            /// <summary>
            /// Gets the date time from string asynchronous.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
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
            /// <summary>
            /// Gets the date time.
            /// </summary>
            /// <returns></returns>
            private DateTime? GetDateTime()
            {
                return ParamsValue is DateTime date ? date : ParamsValue is string dateStr ? GetDateTimeFromString(dateStr) : null;
            }
            /// <summary>
            /// Gets the date time asynchronous.
            /// </summary>
            /// <returns></returns>
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
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(TDbParams paramsModel)
            => $"not a Valid DateTime.";
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync(TDbParams paramsModel)
            {
                await Task.CompletedTask;
                return $"not a Valid DateTime.";
            }

            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override bool ValidateRule(TDbParams paramsModel)
            => GetDateTime() is DateTime;

            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync(TDbParams paramsModel)
            => await GetDateTimeAsync() is DateTime;
        }

    }
}