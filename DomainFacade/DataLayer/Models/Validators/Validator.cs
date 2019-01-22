using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

     

    public class Validator<Par> : List<ValidationRule<Par>>
        where Par : IDbParamsModel
    {
        public bool Validate(Par paramsModel)
        {
            return ValidateCore(paramsModel);
        }

        protected bool ValidateCore(Par paramsModel)
        {
            List<ValidationRuleResult> errors = new List<ValidationRuleResult>();
            foreach (ValidationRule<Par> rule in this)
            {
                ValidationRuleResult validationResult = rule.Validate(paramsModel);
                if(validationResult.Status == ValidationRuleResult.ValidationStatus.FAIL)
                {
                    errors.Add(validationResult);
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
