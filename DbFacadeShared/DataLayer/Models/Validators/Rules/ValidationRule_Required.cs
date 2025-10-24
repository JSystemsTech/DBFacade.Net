using System;

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
            /// Prevents a default instance of the <see cref="RequiredRule" /> class from being created.
            /// </summary>
            private RequiredRule() { }
            /// <summary>
            /// Initializes a new instance of the <see cref="RequiredRule" /> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public RequiredRule(Func<TDbParams, object> selector)
            {
                Init(selector, false);
            }

            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(TDbParams paramsModel)
            {
                return $"is required.";
            }
        }
    }
}