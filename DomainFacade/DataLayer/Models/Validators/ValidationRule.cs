﻿using DomainFacade.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static DomainFacade.DataLayer.Models.Validators.ValidationRuleResult;

namespace DomainFacade.DataLayer.Models.Validators
{
    public class ValidationRuleResult
    {
        public PropertyInfo PropInfo { get; private set; }
        public enum ValidationStatus
        {
            PASS,
            FAIL
        }
        public ValidationStatus Status { get; private set; }
        public string ErrorMessage { get; private set; }
        public object Value { get; private set; }
        public ValidationRuleResult(IDbParamsModel model, PropertyInfo propInfo, string errorMessage, ValidationStatus status)
        {
            PropInfo = propInfo;
            ErrorMessage = errorMessage;
            Status = status;
            Value = propInfo.GetValue(model);
        }
    }
    public abstract class ValidationRule<U>
        where U : IDbParamsModel
    {
        protected object ParamsValue { get; private set; }

        protected Func<U, object> GetParamFunc { get; private set; }
        protected PropertyInfo PropInfo { get; private set; }
        protected bool IsNullable { get; private set; }
        private  static dynamic ParamsProperties = GenericInstance<U>.GetInstance().GetModelProperties();

        public ValidationRule(PropertyInfo propInfo)
        {
            PropInfo = propInfo;
            IsNullable = false;
        }
        public ValidationRule(Func<dynamic, PropertyInfo> getPropInfo )
        {
            PropInfo = getPropInfo(ParamsProperties);
            IsNullable = false;
        }
        public ValidationRule(PropertyInfo propInfo, bool isNullable)
        {
            PropInfo = propInfo;
            IsNullable = isNullable;
        }
        public ValidationRuleResult Validate(U paramsModel)
        {
            ParamsValue = PropInfo.GetValue(paramsModel);
            if ((IsNullable && ParamsValue == null) || ValidateRule())
            {
                return new ValidationRuleResult(paramsModel, PropInfo, null, ValidationStatus.PASS);
            }
            return new ValidationRuleResult(paramsModel, PropInfo, GetErrorMessage(), ValidationStatus.FAIL);
        }
        protected abstract bool ValidateRule();

        private string GetErrorMessage()
        {
            return GetErrorMessageCore(PropInfo.Name);
        }
        protected abstract string GetErrorMessageCore(string propertyName);

        public class Required : ValidationRule<U>
        {
            public Required(PropertyInfo propInfo) : base(propInfo) { }
            public Required(Func<dynamic, PropertyInfo> getPropInfo) : base(getPropInfo) { }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is required.";
            }

            protected override bool ValidateRule()
            {
                return ParamsValue != null;
            }
        }


        private static Type[] numberTypes = new Type[9] {
        typeof(short),
        typeof(int),
        typeof(long),
        typeof(double),
        typeof(float),
        typeof(decimal),
        typeof(ushort),
        typeof(uint),
        typeof(ulong)
    };
        public static bool IsNumericType(object value)
        {
            return numberTypes.Contains(value.GetType());
        }
        public static bool IsNumeric(object value)
        {
            if (IsNumericType(value))
            {
                return true;
            }
            else if (value.GetType() == typeof(string))
            {
                return IsNumeric(value.ToString());
            }
            else
            {
                return false;
            }
        }
        public class Match : ValidationRule<U>
        {
            internal string MatchStr { get; private set; }
            internal RegexOptions RegexOptions { get; private set; }
            public Match(U paramsModel, PropertyInfo propInfo, string regexMatchStr) : base(propInfo)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            public Match(U paramsModel, PropertyInfo propInfo, bool isNullable, string regexMatchStr) : base(propInfo, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = RegexOptions.IgnoreCase;
            }
            public Match(U paramsModel, PropertyInfo propInfo, string regexMatchStr, RegexOptions options) : base(propInfo)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            public Match(U paramsModel, PropertyInfo propInfo, bool isNullable, string regexMatchStr, RegexOptions options) : base(propInfo, isNullable)
            {
                MatchStr = regexMatchStr;
                RegexOptions = options;
            }
            protected override bool ValidateRule()
            {
                System.Text.RegularExpressions.Match MatchRegex = Regex.Match(ParamsValue.ToString(), MatchStr, RegexOptions);
                return MatchRegex.Success;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " does not match the expression.";
            }

        }
        public class MinLength : ValidationRule<U>
        {
            internal int LimitValue { get; set; }
            public MinLength(PropertyInfo propInfo, int limit) : base(propInfo) { LimitValue = limit; }
            public MinLength(PropertyInfo propInfo, bool isNullable, int limit) : base(propInfo, isNullable) { LimitValue = limit; }
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length >= LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be >= " + LimitValue;
            }

        }
        public class MaxLength : MinLength
        {
            public MaxLength(PropertyInfo propInfo, int limit) : base(propInfo, limit) { }
            public MaxLength(PropertyInfo propInfo, bool isNullable, int limit) : base(propInfo, isNullable, limit) { }
            protected override bool ValidateRule()
            {
                return ParamsValue.ToString().Length <= LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be <= " + LimitValue;
            }

        }
        public class IsNummeric : ValidationRule<U>
        {
            public IsNummeric(PropertyInfo propInfo) : base(propInfo) { }
            protected override bool ValidateRule()
            {
                if (IsNumeric(ParamsValue))
                {
                    double num;
                    if (ParamsValue.GetType() == typeof(string))
                    {
                        double.TryParse(ParamsValue.ToString(), out num);
                    }
                    else
                    {
                        num = Convert.ToDouble(ParamsValue);
                    }
                    return Compare(num);
                }
                return false;
            }
            internal virtual bool Compare(double value)
            {
                return true;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a number.";
            }

        }
        public class NumericCompare : IsNummeric
        {
            internal double LimitValue { get; set; }
            public NumericCompare(PropertyInfo propInfo, double limit) : base(propInfo) { LimitValue = limit; }

            internal override bool Compare(double value)
            {
                return true;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is invalid ";
            }
        }
        public class EqualTo : NumericCompare
        {
            public EqualTo(PropertyInfo propInfo, double min) : base(propInfo, min) { }

            internal override bool Compare(double value)
            {
                return value == LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to = " + LimitValue;
            }
        }
        public class NotEqualTo : NumericCompare
        {
            public NotEqualTo(PropertyInfo propInfo, double min) : base(propInfo, min) { }

            internal override bool Compare(double value)
            {
                return value != LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to not = " + LimitValue;
            }
        }
        public class GreatorThan : NumericCompare
        {
            public GreatorThan(PropertyInfo propInfo, double min) : base(propInfo, min) { }

            internal override bool Compare(double value)
            {
                return value > LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be > " + LimitValue;
            }
        }
        public class GreatorThanOrEqual : NumericCompare
        {
            public GreatorThanOrEqual(PropertyInfo propInfo, double min) : base(propInfo, min) { }

            internal override bool Compare(double value)
            {
                return value >= LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be >= " + LimitValue;
            }
        }
        public class LessThan : NumericCompare
        {
            public LessThan(PropertyInfo propInfo, double max) : base(propInfo, max) { }

            internal override bool Compare(double value)
            {
                return value < LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be < " + LimitValue;
            }
        }
        public class LessThanOrEqual : NumericCompare
        {
            public LessThanOrEqual(PropertyInfo propInfo, double max) : base(propInfo, max) { }

            internal override bool Compare(double value)
            {
                return value <= LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be <= " + LimitValue;
            }

        }
    }
}
