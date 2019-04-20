using DomainFacade.Utils;
using System;
using System.Globalization;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
    public abstract partial class ValidationRule<DbParams>
        where DbParams : IDbParamsModel
    {
        /// <summary>
        /// Determines whether [is date time string] [the specified date].
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <returns>
        ///   <c>true</c> if [is date time string] [the specified date]; otherwise, <c>false</c>.
        /// </returns>
        internal bool IsDateTimeString(string date, string dateFormat)
        {
            try
            {
                if (dateFormat != null)
                {
                    DateTime.ParseExact(date, dateFormat, CultureInfo.InvariantCulture);
                }
                else
                {
                    DateTime.Parse(date);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private DateTime ToDateTime(object date, string dateFormat)
        {
            if (date.GetType() == typeof(DateTime))
            {
                return (DateTime) date;
            }
            else if (IsDateTimeString(date.ToString(), dateFormat))
            {
                if (dateFormat != null)
                {
                    return DateTime.ParseExact(date.ToString(), dateFormat, CultureInfo.InvariantCulture);
                }
                else
                {
                    return DateTime.Parse(date.ToString());
                }
            }
            else
            {
                throw new Exception(date + " is not a valid date");
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class IsDateTime : ValidationRule<DbParams>
        {

            /// <summary>
            /// Gets or sets the date format.
            /// </summary>
            /// <value>
            /// The date format.
            /// </value>
            private string DateFormat { get; set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="IsDateTime"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public IsDateTime(Selector<DbParams> selector) : base(selector) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="IsDateTime"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateFormat">The date format.</param>
            public IsDateTime(Selector<DbParams> selector, string dateFormat) : base(selector) { DateFormat = dateFormat; }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a Valid DateTime.";
            }

            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat);
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public abstract class DateTimeLimit : ValidationRule<DbParams>
        {

            /// <summary>
            /// Gets or sets the date format.
            /// </summary>
            /// <value>
            /// The date format.
            /// </value>
            private string DateFormat { get; set; }
            /// <summary>
            /// Gets or sets the date limit.
            /// </summary>
            /// <value>
            /// The date limit.
            /// </value>
            internal DateTime DateLimit { get; set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeLimit"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            public DateTimeLimit(Selector<DbParams> selector, DateTime dateLimit) : base(selector) { DateLimit = dateLimit; }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeLimit"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            /// <param name="dateFormat">The date format.</param>
            public DateTimeLimit(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector) { DateLimit = dateLimit; DateFormat = dateFormat; }

            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return (ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat)) && ValidateExtraRule(ToDateTime(ParamsValue, DateFormat));
            }
            /// <summary>
            /// Validates the extra rule.
            /// </summary>
            /// <param name="ParamsValueAsDate">The parameters value as date.</param>
            /// <returns></returns>
            protected abstract bool ValidateExtraRule(DateTime ParamsValueAsDate);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class DateTimeEquals : DateTimeLimit
        {

            /// <summary>
            /// Gets or sets the date format.
            /// </summary>
            /// <value>
            /// The date format.
            /// </value>
            private string DateFormat { get; set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeEquals"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            public DateTimeEquals(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeEquals"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            /// <param name="dateFormat">The date format.</param>
            public DateTimeEquals(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date equal to " + DateLimit.ToString();
            }
            /// <summary>
            /// Validates the extra rule.
            /// </summary>
            /// <param name="ParamsValueAsDate">The parameters value as date.</param>
            /// <returns></returns>
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate == DateLimit;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class Today : DateTimeEquals
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="Today"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="Today"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
                /// <summary>
                /// Validates the rule.
                /// </summary>
                /// <returns></returns>
                protected override bool ValidateRule()
                {
                    return (ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat)) && ValidateExtraRule(ToDateTime(ParamsValue, DateFormat).Date);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class DateTimeIsBefore : DateTimeLimit
        {

            /// <summary>
            /// Gets or sets the date format.
            /// </summary>
            /// <value>
            /// The date format.
            /// </value>
            private string DateFormat { get; set; }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeIsBefore"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            public DateTimeIsBefore(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeIsBefore"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            /// <param name="dateFormat">The date format.</param>
            public DateTimeIsBefore(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date before " + DateLimit.ToString();
            }
            /// <summary>
            /// Validates the extra rule.
            /// </summary>
            /// <param name="ParamsValueAsDate">The parameters value as date.</param>
            /// <returns></returns>
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate < DateLimit;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class Today : DateTimeIsBefore
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="Today"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="Today"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class Now : DateTimeIsBefore
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="Now"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public Now(Selector<DbParams> selector) : base(selector, DateTime.Now) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="Now"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public Now(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Now, dateFormat) { }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class UtcNow : DateTimeIsBefore
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="UtcNow"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public UtcNow(Selector<DbParams> selector) : base(selector, DateTime.UtcNow) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="UtcNow"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public UtcNow(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.UtcNow, dateFormat) { }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class DateTimeIsOnOrBefore : DateTimeLimit
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeIsOnOrBefore"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            public DateTimeIsOnOrBefore(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeIsOnOrBefore"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            /// <param name="dateFormat">The date format.</param>
            public DateTimeIsOnOrBefore(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date on or before " + DateLimit.ToString();
            }
            /// <summary>
            /// Validates the extra rule.
            /// </summary>
            /// <param name="ParamsValueAsDate">The parameters value as date.</param>
            /// <returns></returns>
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate <= DateLimit;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class Today : DateTimeIsOnOrBefore
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="Today"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="Today"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class Now : DateTimeIsOnOrBefore
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="Now"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public Now(Selector<DbParams> selector) : base(selector, DateTime.Now) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="Now"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public Now(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Now, dateFormat) { }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class UtcNow : DateTimeIsOnOrBefore
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="UtcNow"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public UtcNow(Selector<DbParams> selector) : base(selector, DateTime.UtcNow) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="UtcNow"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public UtcNow(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.UtcNow, dateFormat) { }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class DateTimeIsAfter : DateTimeLimit
        {

            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeIsAfter"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            public DateTimeIsAfter(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeIsAfter"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            /// <param name="dateFormat">The date format.</param>
            public DateTimeIsAfter(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date after " + DateLimit.ToString();
            }
            /// <summary>
            /// Validates the extra rule.
            /// </summary>
            /// <param name="ParamsValueAsDate">The parameters value as date.</param>
            /// <returns></returns>
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate > DateLimit;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class Today : DateTimeIsAfter
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="Today"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="Today"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class Now : DateTimeIsAfter
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="Now"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public Now(Selector<DbParams> selector) : base(selector, DateTime.Now) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="Now"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public Now(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Now, dateFormat) { }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class UtcNow : DateTimeIsAfter
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="UtcNow"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public UtcNow(Selector<DbParams> selector) : base(selector, DateTime.UtcNow) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="UtcNow"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public UtcNow(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.UtcNow, dateFormat) { }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class DateTimeIsOnOrAfter : DateTimeLimit
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeIsOnOrAfter"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            public DateTimeIsOnOrAfter(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            /// <summary>
            /// Initializes a new instance of the <see cref="DateTimeIsOnOrAfter"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="dateLimit">The date limit.</param>
            /// <param name="dateFormat">The date format.</param>
            public DateTimeIsOnOrAfter(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date on or after " + DateLimit.ToString();
            }
            /// <summary>
            /// Validates the extra rule.
            /// </summary>
            /// <param name="ParamsValueAsDate">The parameters value as date.</param>
            /// <returns></returns>
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate >= DateLimit;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class Today : DateTimeIsOnOrAfter
            {
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class Now : DateTimeIsOnOrAfter
            {
                public Now(Selector<DbParams> selector) : base(selector, DateTime.Now) { }
                public Now(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Now, dateFormat) { }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
            public class UtcNow : DateTimeIsOnOrAfter
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="UtcNow"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                public UtcNow(Selector<DbParams> selector) : base(selector, DateTime.UtcNow) { }
                /// <summary>
                /// Initializes a new instance of the <see cref="UtcNow"/> class.
                /// </summary>
                /// <param name="selector">The selector.</param>
                /// <param name="dateFormat">The date format.</param>
                public UtcNow(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.UtcNow, dateFormat) { }
            }
        }
    }
}
