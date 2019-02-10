using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{

    public abstract partial class ValidationRule<DbParams>
        where DbParams : IDbParamsModel
    {
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
        public class IsNummeric : ValidationRule<DbParams>
        {
            public IsNummeric(Selector<DbParams> selector) : base(selector) { }
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
            public NumericCompare(Selector<DbParams> selector, double limit) : base(selector) { LimitValue = limit; }

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
            public EqualTo(Selector<DbParams> selector, double min) : base(selector, min) { }

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
            public NotEqualTo(Selector<DbParams> selector, double min) : base(selector, min) { }

            internal override bool Compare(double value)
            {
                return value != LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to not = " + LimitValue;
            }
        }
        public class GreaterThan : NumericCompare
        {
            public GreaterThan(Selector<DbParams> selector, double min) : base(selector, min) { }

            internal override bool Compare(double value)
            {
                return value > LimitValue;
            }
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be > " + LimitValue;
            }
        }
        public class GreaterThanOrEqual : NumericCompare
        {
            public GreaterThanOrEqual(Selector<DbParams> selector, double min) : base(selector, min) { }

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
            public LessThan(Selector<DbParams> selector, double max) : base(selector, max) { }

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
            public LessThanOrEqual(Selector<DbParams> selector, double max) : base(selector, max) { }

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