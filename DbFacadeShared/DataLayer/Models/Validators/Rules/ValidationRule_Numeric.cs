using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public abstract partial class ValidationRule<TDbParams>
    {


        /// <summary>
        /// Determines whether the specified selector is number.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        /// <returns></returns>
        public static IValidationRule<TDbParams> IsNumber(Func<TDbParams, string> selector,
            bool isNullable = false)
        {
            return new IsNumeric(selector, isNullable);
        }


        /// <summary>
        /// 
        /// </summary>
        internal class IsNumeric : ValidationRule<TDbParams>
        {
            /// <summary>
            /// The number styles
            /// </summary>
            private static NumberStyles NumberStyles = 
                NumberStyles.Any |
                NumberStyles.AllowLeadingSign |
                NumberStyles.AllowExponent |
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowParentheses;

            /// <summary>
            /// Prevents a default instance of the <see cref="IsNumeric"/> class from being created.
            /// </summary>
            private IsNumeric() {}
            /// <summary>
            /// Initializes a new instance of the <see cref="IsNumeric"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            public IsNumeric(Func<TDbParams, string> selector, bool isNullable = false)
            {
                Init(selector, isNullable);
            }

            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
            /// <returns></returns>
            public static async Task<IsNumeric> CreateAsync(Func<TDbParams, string> selector, bool isNullable = false)
            {
                IsNumeric rule = new IsNumeric();
                await rule.InitAsync(selector, isNullable);
                return rule;
            }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                double result;
                return double.TryParse(ParamsValue.ToString(), NumberStyles, System.Globalization.CultureInfo.CurrentCulture, out result);
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync()
            {
                double result;
                bool isNumber = double.TryParse(ParamsValue.ToString(), NumberStyles, System.Globalization.CultureInfo.CurrentCulture, out result);
                await Task.CompletedTask;
                return isNumber;
            }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"not a number.";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"not a number.";
            }
        }


    }
}