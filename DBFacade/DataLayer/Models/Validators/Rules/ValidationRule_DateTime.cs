using DBFacade.Utils;
using System;
using System.Globalization;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        private DateTime ToDateTimeCore(string date, string dateFormat)
        {
            return string.IsNullOrEmpty(dateFormat) ?
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
        
        public IsDateTimeRule IsDateTime(Func<TDbParams, string> selector, string dateFormat) => new IsDateTimeRule(selector);

        public sealed class IsDateTimeRule : ValidationRule<TDbParams>
        {

            private string DateFormat { get; set; }

            public IsDateTimeRule(Func<TDbParams, string> selector, string dateFormat = null) : base(selector) { DateFormat = dateFormat; }

            protected sealed override string GetErrorMessageCore(string propertyName)
            {
                return $"{propertyName} is not a Valid DateTime.";
            }
            protected sealed override bool ValidateRule()
            {
                return ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat);
            }

        }

        public abstract class DateTimeLimit : ValidationRule<TDbParams>
        {
            private string DateFormat { get; set; }
            internal DateTime DateLimit { get; set; }
            protected string RuleText { get; set; }
            public DateTimeLimit(Func<TDbParams, DateTime> selector, DateTime dateLimit) : base(selector) { DateLimit = dateLimit;}
            public DateTimeLimit(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) : base(selector) { DateLimit = dateLimit; DateFormat = dateFormat; }

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

        public DateTimeEqualsRule DateTimeEquals(Func<TDbParams, DateTime> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeEqualsRule(selector, dateLimit);
        public DateTimeEqualsRule DateTimeEquals(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeEqualsRule(selector, dateLimit, dateFormat);
        public class DateTimeEqualsRule : DateTimeLimit
        {
            public DateTimeEqualsRule(Func<TDbParams, DateTime> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "equal to"; }
            public DateTimeEqualsRule(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) : base(selector, dateLimit, dateFormat) { RuleText = "equal to"; }

            protected override bool ValidateDateRule(DateTime ParamsValueAsDate)=> ParamsValueAsDate == DateLimit;           
        }

        public DateTimeIsBeforeRule DateTimeIsBefore(Func<TDbParams, DateTime> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsBeforeRule(selector, dateLimit);
        public DateTimeIsBeforeRule DateTimeIsBefore(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsBeforeRule(selector, dateLimit, dateFormat);
        public class DateTimeIsBeforeRule : DateTimeLimit
        {
            public DateTimeIsBeforeRule(Func<TDbParams, DateTime> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "before"; }
            public DateTimeIsBeforeRule(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) : base(selector, dateLimit, dateFormat) { RuleText = "before"; }
            
            protected override bool ValidateDateRule(DateTime ParamsValueAsDate)=> ParamsValueAsDate < DateLimit;
        }

        public DateTimeIsOnOrBeforeRule DateTimeIsOnOrBefore(Func<TDbParams, DateTime> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsOnOrBeforeRule(selector, dateLimit);
        public DateTimeIsOnOrBeforeRule DateTimeIsOnOrBefore(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsOnOrBeforeRule(selector, dateLimit, dateFormat);
        public class DateTimeIsOnOrBeforeRule : DateTimeLimit
        {
            public DateTimeIsOnOrBeforeRule(Func<TDbParams, DateTime> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "on or before"; }
            public DateTimeIsOnOrBeforeRule(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) : base(selector, dateLimit, dateFormat) { RuleText = "on or before"; }

            protected override bool ValidateDateRule(DateTime ParamsValueAsDate) => ParamsValueAsDate <= DateLimit;           
        }

        public DateTimeIsAfterRule DateTimeIsAfter(Func<TDbParams, DateTime> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsAfterRule(selector, dateLimit);
        public DateTimeIsAfterRule DateTimeIsAfter(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsAfterRule(selector, dateLimit, dateFormat);
        public class DateTimeIsAfterRule : DateTimeLimit
        {
            public DateTimeIsAfterRule(Func<TDbParams, DateTime> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "after"; }
            public DateTimeIsAfterRule(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) : base(selector, dateLimit, dateFormat) { RuleText = "after"; }

            protected override bool ValidateDateRule(DateTime ParamsValueAsDate) => ParamsValueAsDate > DateLimit;    
        }

        public DateTimeIsOnOrAfterRule DateTimeIsOnOrAfter(Func<TDbParams, DateTime> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsOnOrAfterRule(selector, dateLimit);
        public DateTimeIsOnOrAfterRule DateTimeIsOnOrAfter(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) => new DateTimeIsOnOrAfterRule(selector, dateLimit, dateFormat);
        public class DateTimeIsOnOrAfterRule : DateTimeLimit
        {
            public DateTimeIsOnOrAfterRule(Func<TDbParams, DateTime> selector, DateTime dateLimit) : base(selector, dateLimit) { RuleText = "on or after"; }
            public DateTimeIsOnOrAfterRule(Func<TDbParams, string> selector, DateTime dateLimit, string dateFormat = null) : base(selector, dateLimit, dateFormat) { RuleText = "on or after"; }

            protected override bool ValidateDateRule(DateTime ParamsValueAsDate)=> ParamsValueAsDate >= DateLimit;
        }
    }
}
