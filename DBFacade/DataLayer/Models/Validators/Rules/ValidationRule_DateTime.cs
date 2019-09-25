using System;
using System.Globalization;
using System.Linq.Expressions;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        private DateTime ToDateTimeCore(string date, string dateFormat)
        {
            return !string.IsNullOrEmpty(dateFormat) ?
                    DateTime.ParseExact(date.ToString(), dateFormat, CultureInfo.InvariantCulture) :
                    DateTime.Parse(date.ToString());
        }
        internal bool IsDateTimeString(string date, string dateFormat)
        {
            try
            {
                ToDateTimeCore(date, dateFormat);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private DateTime ToDateTime(object date, string dateFormat)
        {
            return date.GetType() == typeof(DateTime) ?
                (DateTime)date : ToDateTimeCore(date.ToString(), dateFormat);
        }
        
        public static IValidationRule<TDbParams> IsDateTime(Expression<Func<TDbParams, string>> selector, string dateFormat = null, bool isNullable = false) => new IsDateTimeRule(selector, dateFormat, isNullable);

        private sealed class IsDateTimeRule : ValidationRule<TDbParams>
        {

            private string DateFormat { get; set; }

            public IsDateTimeRule(Expression<Func<TDbParams, string>> selector, string dateFormat = null, bool isNullable=false) : base(selector,isNullable) { DateFormat = dateFormat; }

            protected sealed override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} is not a Valid DateTime.";
            }
            protected sealed override bool ValidateRule()
            {
                return ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat);
            }
        }

        private abstract class DateTimeLimit : ValidationRule<TDbParams>
        {
            private string DateFormat { get; set; }
            internal DateTime DateLimit { get; set; }
            protected string RuleText { get; set; }
            public DateTimeLimit(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit) : base(selector) { DateLimit = dateLimit;}
            public DateTimeLimit(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit) : base(selector) { DateLimit = dateLimit; }
            public DateTimeLimit(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) : base(selector, isNullable) { DateLimit = dateLimit; DateFormat = dateFormat; }

            protected override bool ValidateRule()
            {
                return (ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat)) && ValidateDateRule(ToDateTime(ParamsValue, DateFormat));
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} is not a date {RuleText} {DateLimit.ToString()}";
            }
            protected abstract bool ValidateDateRule(DateTime ParamsValueAsDate);
        }

        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit) => new DateTimeEqualsRule(selector, dateLimit);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit) => new DateTimeEqualsRule(selector, dateLimit);
        public static IValidationRule<TDbParams> Equals(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) => new DateTimeEqualsRule(selector, dateLimit, dateFormat, isNullable);
        private class DateTimeEqualsRule : DateTimeLimit
        {
            public DateTimeEqualsRule(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "equal to"; }
            public DateTimeEqualsRule(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "equal to"; }
            public DateTimeEqualsRule(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) : base(selector, dateLimit, dateFormat, isNullable) { RuleText = "equal to"; }

            protected override bool ValidateDateRule(DateTime ParamsValueAsDate)=> ParamsValueAsDate == DateLimit;           
        }

        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsBeforeRule(selector, dateLimit);
        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsBeforeRule(selector, dateLimit);
        public static IValidationRule<TDbParams> IsBefore(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) => new DateTimeIsBeforeRule(selector, dateLimit, dateFormat,isNullable);
        private class DateTimeIsBeforeRule : DateTimeLimit
        {
            public DateTimeIsBeforeRule(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "before"; }
            public DateTimeIsBeforeRule(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "before"; }
            public DateTimeIsBeforeRule(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) : base(selector, dateLimit, dateFormat, isNullable) { RuleText = "before"; }
            
            protected override bool ValidateDateRule(DateTime ParamsValueAsDate)=> ParamsValueAsDate < DateLimit;
        }

        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsOnOrBeforeRule(selector, dateLimit);
        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsOnOrBeforeRule(selector, dateLimit);
        public static IValidationRule<TDbParams> IsOnOrBefore(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) => new DateTimeIsOnOrBeforeRule(selector, dateLimit, dateFormat,isNullable);
        private class DateTimeIsOnOrBeforeRule : DateTimeLimit
        {
            public DateTimeIsOnOrBeforeRule(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "on or before"; }
            public DateTimeIsOnOrBeforeRule(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "on or before"; }
            public DateTimeIsOnOrBeforeRule(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) : base(selector, dateLimit, dateFormat,isNullable) { RuleText = "on or before"; }

            protected override bool ValidateDateRule(DateTime ParamsValueAsDate) => ParamsValueAsDate <= DateLimit;           
        }

        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsAfterRule(selector, dateLimit);
        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsAfterRule(selector, dateLimit);
        public static IValidationRule<TDbParams> IsAfter(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) => new DateTimeIsAfterRule(selector, dateLimit, dateFormat,isNullable);
        private class DateTimeIsAfterRule : DateTimeLimit
        {
            public DateTimeIsAfterRule(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "after"; }
            public DateTimeIsAfterRule(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "after"; }
            public DateTimeIsAfterRule(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) : base(selector, dateLimit, dateFormat, isNullable) { RuleText = "after"; }

            protected override bool ValidateDateRule(DateTime ParamsValueAsDate) => ParamsValueAsDate > DateLimit;    
        }

        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsOnOrAfterRule(selector, dateLimit);
        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsOnOrAfterRule(selector, dateLimit);
        public static IValidationRule<TDbParams> IsOnOrAfter(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) => new DateTimeIsOnOrAfterRule(selector, dateLimit, dateFormat,isNullable);
        private class DateTimeIsOnOrAfterRule : DateTimeLimit
        {
            public DateTimeIsOnOrAfterRule(Expression<Func<TDbParams, DateTime>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "on or after"; }
            public DateTimeIsOnOrAfterRule(Expression<Func<TDbParams, DateTime?>> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "on or after"; }
            public DateTimeIsOnOrAfterRule(Expression<Func<TDbParams, string>> selector, DateTime dateLimit, string dateFormat = null, bool isNullable = false) : base(selector, dateLimit, dateFormat, isNullable) { RuleText = "on or after"; }

            protected override bool ValidateDateRule(DateTime ParamsValueAsDate)=> ParamsValueAsDate >= DateLimit;
        }
    }
}
