﻿using DomainFacade.Utils;
using System;
using System.Globalization;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    public abstract partial class ValidationRule<DbParams>
        where DbParams : IDbParamsModel
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
        public class IsDateTime : ValidationRule<DbParams>
        {

            private string DateFormat { get; set; }
            public IsDateTime(Selector<DbParams> selector) : base(selector) { }
            public IsDateTime(Selector<DbParams> selector, string dateFormat) : base(selector) { DateFormat = dateFormat; }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a Valid DateTime.";
            }
                    
            protected override bool ValidateRule()
            {
                return ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat);
            }
            
        }
        public abstract class DateTimeLimit : ValidationRule<DbParams>
        {

            private string DateFormat { get; set; }
            internal DateTime DateLimit { get; set; }
            public DateTimeLimit(Selector<DbParams> selector, DateTime dateLimit) : base(selector) { DateLimit = dateLimit; }
            public DateTimeLimit(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector) { DateLimit = dateLimit; DateFormat = dateFormat; }
            
            protected override bool ValidateRule()
            {
                return (ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat)) && ValidateExtraRule(ToDateTime(ParamsValue, DateFormat));
            }
            protected abstract bool ValidateExtraRule(DateTime ParamsValueAsDate);
            
        }
        public class DateTimeEquals : DateTimeLimit
        {

            private string DateFormat { get; set; }
            public DateTimeEquals(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            public DateTimeEquals(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
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
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
                protected override bool ValidateRule()
                {
                    return (ParamsValue.GetType() == typeof(DateTime) || IsDateTimeString(ParamsValue.ToString(), DateFormat)) && ValidateExtraRule(ToDateTime(ParamsValue, DateFormat).Date);
                }
            }
        }
        public class DateTimeIsBefore : DateTimeLimit
        {

            private string DateFormat { get; set; }
            public DateTimeIsBefore(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            public DateTimeIsBefore(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
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
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
            }
            public class Now : DateTimeIsBefore
            {
                public Now(Selector<DbParams> selector) : base(selector, DateTime.Now) { }
                public Now(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Now, dateFormat) { }
            }
            public class UtcNow : DateTimeIsBefore
            {
                public UtcNow(Selector<DbParams> selector) : base(selector, DateTime.UtcNow) { }
                public UtcNow(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.UtcNow, dateFormat) { }
            }
        }
        public class DateTimeIsOnOrBefore : DateTimeLimit
        {

            public DateTimeIsOnOrBefore(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            public DateTimeIsOnOrBefore(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
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
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
            }
            public class Now : DateTimeIsOnOrBefore
            {
                public Now(Selector<DbParams> selector) : base(selector, DateTime.Now) { }
                public Now(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Now, dateFormat) { }
            }
            public class UtcNow : DateTimeIsOnOrBefore
            {
                public UtcNow(Selector<DbParams> selector) : base(selector, DateTime.UtcNow) { }
                public UtcNow(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.UtcNow, dateFormat) { }
            }
        }
        public class DateTimeIsAfter : DateTimeLimit
        {

            public DateTimeIsAfter(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            public DateTimeIsAfter(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
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
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
            }
            public class Now : DateTimeIsAfter
            {
                public Now(Selector<DbParams> selector) : base(selector, DateTime.Now) { }
                public Now(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Now, dateFormat) { }
            }
            public class UtcNow : DateTimeIsAfter
            {
                public UtcNow(Selector<DbParams> selector) : base(selector, DateTime.UtcNow) { }
                public UtcNow(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.UtcNow, dateFormat) { }
            }
        }
        public class DateTimeIsOnOrAfter : DateTimeLimit
        {
            public DateTimeIsOnOrAfter(Selector<DbParams> selector, DateTime dateLimit) : base(selector, dateLimit) { }
            public DateTimeIsOnOrAfter(Selector<DbParams> selector, DateTime dateLimit, string dateFormat) : base(selector, dateLimit, dateFormat) { }
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
                public Today(Selector<DbParams> selector) : base(selector, DateTime.Today) { }
                public Today(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Today, dateFormat) { }
            }
            public class Now : DateTimeIsOnOrAfter
            {
                public Now(Selector<DbParams> selector) : base(selector, DateTime.Now) { }
                public Now(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.Now, dateFormat) { }
            }
            public class UtcNow : DateTimeIsOnOrAfter
            {
                public UtcNow(Selector<DbParams> selector) : base(selector, DateTime.UtcNow) { }
                public UtcNow(Selector<DbParams> selector, string dateFormat) : base(selector, DateTime.UtcNow, dateFormat) { }
            }
        }
    }
}
