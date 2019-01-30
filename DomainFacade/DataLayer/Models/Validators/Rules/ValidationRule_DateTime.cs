using System;
using System.Globalization;
using System.Reflection;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<U>
        where U : IDbParamsModel
    {
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
        public class IsDateTime : ValidationRule<U>
        {

            private string DateFormat { get; set; }
            public IsDateTime(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo) { }
            public IsDateTime(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo) { DateFormat = dateFormat; }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a Valid DateTime.";
            }
                    
            protected override bool ValidateRule()
            {
                return ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat);
            }
            
        }
        public abstract class DateTimeLimit : ValidationRule<U>
        {

            private string DateFormat { get; set; }
            internal DateTime DateLimit { get; set; }
            public DateTimeLimit(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit) : base(getPropInfo) { DateLimit = dateLimit; }
            public DateTimeLimit(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit, string dateFormat) : base(getPropInfo) { DateLimit = dateLimit; DateFormat = dateFormat; }
            
            protected override bool ValidateRule()
            {
                return (ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat)) && ValidateExtraRule(ToDateTime(ParamsValue, DateFormat));
            }
            protected abstract bool ValidateExtraRule(DateTime ParamsValueAsDate);
            
        }
        public class DateTimeEquals : DateTimeLimit
        {

            private string DateFormat { get; set; }
            public DateTimeEquals(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit) : base(getPropInfo, dateLimit) { }
            public DateTimeEquals(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit, string dateFormat) : base(getPropInfo, dateLimit, dateFormat) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date equal to " + DateLimit.ToString();
            }
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate == DateLimit;
            }
            
            public class Today : DateTimeEquals
            {
                public Today(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.Today) { }
                public Today(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.Today, dateFormat) { }
                protected override bool ValidateRule()
                {
                    return (ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat)) && ValidateExtraRule(ToDateTime(ParamsValue, DateFormat).Date);
                }
            }
        }
        public class DateTimeIsBefore : DateTimeLimit
        {

            private string DateFormat { get; set; }
            public DateTimeIsBefore(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit) : base(getPropInfo, dateLimit) { }
            public DateTimeIsBefore(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit, string dateFormat) : base(getPropInfo, dateLimit, dateFormat) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date before " + DateLimit.ToString();
            }
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate < DateLimit;
            }
            public class Today : DateTimeIsBefore
            {
                public Today(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.Today) { }
                public Today(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.Today, dateFormat) { }
            }
            public class Now : DateTimeIsBefore
            {
                public Now(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.Now) { }
                public Now(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.Now, dateFormat) { }
            }
            public class UtcNow : DateTimeIsBefore
            {
                public UtcNow(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.UtcNow) { }
                public UtcNow(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.UtcNow, dateFormat) { }
            }
        }
        public class DateTimeIsOnOrBefore : DateTimeLimit
        {

            public DateTimeIsOnOrBefore(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit) : base(getPropInfo, dateLimit) { }
            public DateTimeIsOnOrBefore(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit, string dateFormat) : base(getPropInfo, dateLimit, dateFormat) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date on or before " + DateLimit.ToString();
            }
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate <= DateLimit;
            }
            public class Today : DateTimeIsOnOrBefore
            {
                public Today(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.Today) { }
                public Today(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.Today, dateFormat) { }
            }
            public class Now : DateTimeIsOnOrBefore
            {
                public Now(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.Now) { }
                public Now(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.Now, dateFormat) { }
            }
            public class UtcNow : DateTimeIsOnOrBefore
            {
                public UtcNow(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.UtcNow) { }
                public UtcNow(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.UtcNow, dateFormat) { }
            }
        }
        public class DateTimeIsAfter : DateTimeLimit
        {

            public DateTimeIsAfter(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit) : base(getPropInfo, dateLimit) { }
            public DateTimeIsAfter(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit, string dateFormat) : base(getPropInfo, dateLimit, dateFormat) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date after " + DateLimit.ToString();
            }
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate > DateLimit;
            }
            public class Today : DateTimeIsAfter
            {
                public Today(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.Today) { }
                public Today(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.Today, dateFormat) { }
            }
            public class Now : DateTimeIsAfter
            {
                public Now(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.Now) { }
                public Now(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.Now, dateFormat) { }
            }
            public class UtcNow : DateTimeIsAfter
            {
                public UtcNow(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.UtcNow) { }
                public UtcNow(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.UtcNow, dateFormat) { }
            }
        }
        public class DateTimeIsOnOrAfter : DateTimeLimit
        {
            public DateTimeIsOnOrAfter(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit) : base(getPropInfo, dateLimit) { }
            public DateTimeIsOnOrAfter(Func<dynamic, PropertyInfo> getPropInfo, DateTime dateLimit, string dateFormat) : base(getPropInfo, dateLimit, dateFormat) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a date on or after " + DateLimit.ToString();
            }
            protected override bool ValidateExtraRule(DateTime ParamsValueAsDate)
            {
                return ParamsValueAsDate >= DateLimit;
            }
            public class Today : DateTimeIsOnOrAfter
            {
                public Today(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.Today) { }
                public Today(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.Today, dateFormat) { }
            }
            public class Now : DateTimeIsOnOrAfter
            {
                public Now(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.Now) { }
                public Now(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.Now, dateFormat) { }
            }
            public class UtcNow : DateTimeIsOnOrAfter
            {
                public UtcNow(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo, DateTime.UtcNow) { }
                public UtcNow(Func<dynamic, PropertyInfo> getPropInfo, string dateFormat) : base(getPropInfo, DateTime.UtcNow, dateFormat) { }
            }
        }
    }
}
