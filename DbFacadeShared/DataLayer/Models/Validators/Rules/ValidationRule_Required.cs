using System;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{DbParams}" />
    public abstract partial class ValidationRule<TDbParams>
    {

        /// <summary>
        /// 
        /// </summary>
        internal class RequiredRule : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Prevents a default instance of the <see cref="RequiredRule"/> class from being created.
            /// </summary>
            private RequiredRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="RequiredRule"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public RequiredRule(Func<TDbParams, object> selector)
            {
                Init(selector, false);
            }
            /// <summary>
            /// Creates the asynchronous.
            /// </summary>
            /// <param name="selector">The selector.</param>
            /// <returns></returns>
            public static async Task<RequiredRule> CreateAsync(Func<TDbParams, object> selector)
            {
                RequiredRule rule = new RequiredRule();
                await rule.InitAsync(selector, false);
                return rule;
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <returns></returns>
            protected override string GetErrorMessageCore()
            {
                return $"is required.";
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return $"is required.";
            }
        }
    }
}