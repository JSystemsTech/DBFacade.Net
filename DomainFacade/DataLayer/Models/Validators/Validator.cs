using DomainFacade.DataLayer.DbManifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomainFacade.DataLayer.Models.Validators
{
    public abstract class ValidatorFunc
    {
        protected Func<int, bool> validatorFunc;

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

        private static bool IsInRange(short number, short min, short max)
        {
            return number >= min && number <= max;
        }
        private static bool IsInRange(int number, int min, int max)
        {
            return number >= min && number <= max;
        }
        private static bool IsInRange(long number, long min, long max)
        {
            return number >= min && number <= max;
        }
        private static bool IsInRange(ushort number, ushort min, ushort max)
        {
            return number >= min && number <= max;
        }
        private static bool IsInRange(uint number, uint min, uint max)
        {
            return number >= min && number <= max;
        }
        private static bool IsInRange(ulong number, ulong min, ulong max)
        {
            return number >= min && number <= max;
        }
        private static bool IsInRange(double number, double min, double max)
        {
            return number >= min && number <= max;
        }
        private static bool IsInRange(float number, float min, float max)
        {
            return number >= min && number <= max;
        }
        private static bool IsInRange(decimal number, decimal min, decimal max)
        {
            return number >= min && number <= max;
        }
        private static bool IsInRange(string number, double min, double max)
        {
            if (IsNumeric(number))
            {
                double n;
                double.TryParse(number, out n);
                return IsInRange(number, min, max);
            }
            return false;
        }

        public static bool IsNumeric(string number)
        {
            double n;
            return double.TryParse(number, out n);
        }
        Func<object, bool> IsNumericFunc = (number) => IsNumeric(number);
        Func<int, int, int, bool> IsInRangeFunc = (number, min, max) => IsInRange(number, min, max);

    }

    
    public abstract class ValidatorRule<U>
        where U : IDbParamsModel
    {
        protected object ParamsValue { get; private set; }

        protected Func<U, object> GetParamFunc { get; private set; }
        protected PropertyInfo PropInfo { get; private set; }
        protected bool IsNullable { get; private set; }
        
        public ValidatorRule(PropertyInfo propInfo)
        {
            PropInfo = propInfo;
            IsNullable = false;
        }
        public ValidatorRule(PropertyInfo propInfo, bool isNullable)
        {
            PropInfo = propInfo;
            IsNullable = isNullable;
        }
        public string Validate(U paramsModel)
        {
            ParamsValue = PropInfo.GetValue(paramsModel);
            if ((IsNullable && ParamsValue == null) || ValidateRule())
            {
                return string.Empty;
            }            
            return GetErrorMessage();
        }
        protected abstract bool ValidateRule();

        private string GetErrorMessage()
        {
            return GetErrorMessageCore(PropInfo.Name);
        }
        protected abstract string GetErrorMessageCore(string propertyName);

        public class Required : ValidatorRule<U>
        {
            public Required(PropertyInfo propInfo) : base(propInfo) { }

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
        public class Match : ValidatorRule<U>
        {
            internal string MatchStr{ get; private set; }
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
        public class MinLength : ValidatorRule<U>
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
            public MaxLength(PropertyInfo propInfo, int limit) : base(propInfo, limit) {}
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
        public class IsNummeric : ValidatorRule<U>
        {
            public IsNummeric(PropertyInfo propInfo) : base(propInfo) {}
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
    


   //public class Validator<Par>: List<ValidatorRule<Par>>
   //     where Par : IDbParamsModel
   // {
   //     public bool Validate(Par paramsModel)
   //     {
   //         return ValidateCore(paramsModel);
   //     }        

   //     protected bool ValidateCore(Par paramsModel)
   //     {
   //         //bool result =  this.All(rule => rule.Validate(paramsModel));
   //         bool result = true;
   //         foreach(ValidatorRule<Par> rule in this)
   //         {
   //             result = rule.Validate(paramsModel) && result;
   //         }
            
   //         return result;
   //     }
         
   // }

    public class Validator<Par> : List<ValidatorRule<Par>>
        where Par : IDbParamsModel
    {
        public bool Validate(Par paramsModel)
        {
            return ValidateCore(paramsModel);
        }

        protected bool ValidateCore(Par paramsModel)
        {
            List<string> errors = new List<string>();
            foreach ( ValidatorRule<Par> rule in this)
            {
                string resultStr = rule.Validate(paramsModel);
                if(resultStr != string.Empty)
                {
                    errors.Add(resultStr);
                }
            }

            return errors.Count == 0;
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static PropertyInfo GetPropertyInfo(string name)
        {
            return typeof(Par).GetProperty(name);
        }
    }

}
