using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{DbParams}" />
    public abstract partial class ValidationRule<TDbParams>
    {
        
        internal class RequiredRule : ValidationRule<TDbParams>
        {
            private RequiredRule() { }
            public RequiredRule(Func<TDbParams, object> selector)
            {
                Init(selector, false);
            }
            public static async Task<RequiredRule> CreateAsync(Func<TDbParams, object> selector)
            {
                RequiredRule rule = new RequiredRule();
                await rule.InitAsync(selector, false);
                return rule;
            }

            protected override string GetErrorMessageCore()
            {
                return $"is required.";
            }
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"is required.";
            }
        }
    }
}