using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public abstract partial class ValidationRule<TDbParams>
    {
        

        public static IValidationRule<TDbParams> IsNumber(Func<TDbParams, string> selector,
            bool isNullable = false)
        {
            return new IsNumeric(selector, isNullable);
        }
        

        internal class IsNumeric : ValidationRule<TDbParams>
        {
            private static NumberStyles NumberStyles = 
                NumberStyles.Any |
                NumberStyles.AllowLeadingSign |
                NumberStyles.AllowExponent |
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowParentheses;
            
            private IsNumeric() {}
            public IsNumeric(Func<TDbParams, string> selector, bool isNullable = false)
            {
                Init(selector, isNullable);
            }

            public static async Task<IsNumeric> CreateAsync(Func<TDbParams, string> selector, bool isNullable = false)
            {
                IsNumeric rule = new IsNumeric();
                await rule.InitAsync(selector, isNullable);
                return rule;
            }
            protected override bool ValidateRule()
            {
                double result;
                return double.TryParse(ParamsValue.ToString(), NumberStyles, System.Globalization.CultureInfo.CurrentCulture, out result);
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                double result;
                bool isNumber = double.TryParse(ParamsValue.ToString(), NumberStyles, System.Globalization.CultureInfo.CurrentCulture, out result);
                await Task.CompletedTask;
                return isNumber;
            }
            protected override string GetErrorMessageCore()
            {
                return $"not a number.";
            }
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"not a number.";
            }
        }


    }
}