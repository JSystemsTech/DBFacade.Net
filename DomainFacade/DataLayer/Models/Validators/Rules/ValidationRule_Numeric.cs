using DomainFacade.Utils;
using System;
using System.Linq;

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
        /// The number types
        /// </summary>
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
        /// <summary>
        /// Determines whether [is numeric type] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is numeric type] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumericType(object value)
        {
            return numberTypes.Contains(value.GetType());
        }
        /// <summary>
        /// Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is numeric; otherwise, <c>false</c>.
        /// </returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class IsNummeric : ValidationRule<DbParams>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="IsNummeric"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public IsNummeric(Selector<DbParams> selector) : base(selector) { }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
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
            /// <summary>
            /// Compares the specified value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            internal virtual bool Compare(double value)
            {
                return true;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is not a number.";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
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
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
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
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class NotEqualTo : NumericCompare
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NotEqualTo"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="min">The minimum.</param>
            public NotEqualTo(Selector<DbParams> selector, double min) : base(selector, min) { }

            /// <summary>
            /// Compares the specified value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            internal override bool Compare(double value)
            {
                return value != LimitValue;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to not = " + LimitValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class GreaterThan : NumericCompare
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GreaterThan"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="min">The minimum.</param>
            public GreaterThan(Selector<DbParams> selector, double min) : base(selector, min) { }

            /// <summary>
            /// Compares the specified value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            internal override bool Compare(double value)
            {
                return value > LimitValue;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be > " + LimitValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class GreaterThanOrEqual : NumericCompare
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GreaterThanOrEqual"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="min">The minimum.</param>
            public GreaterThanOrEqual(Selector<DbParams> selector, double min) : base(selector, min) { }

            /// <summary>
            /// Compares the specified value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            internal override bool Compare(double value)
            {
                return value >= LimitValue;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be >= " + LimitValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class LessThan : NumericCompare
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LessThan"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="max">The maximum.</param>
            public LessThan(Selector<DbParams> selector, double max) : base(selector, max) { }

            /// <summary>
            /// Compares the specified value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            internal override bool Compare(double value)
            {
                return value < LimitValue;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be < " + LimitValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.DataLayer.Models.Validators.Rules.IValidationRule{DbParams}" />
        public class LessThanOrEqual : NumericCompare
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LessThanOrEqual"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="max">The maximum.</param>
            public LessThanOrEqual(Selector<DbParams> selector, double max) : base(selector, max) { }

            /// <summary>
            /// Compares the specified value.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <returns></returns>
            internal override bool Compare(double value)
            {
                return value <= LimitValue;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " expecting value length to be <= " + LimitValue;
            }

        }
    }
}