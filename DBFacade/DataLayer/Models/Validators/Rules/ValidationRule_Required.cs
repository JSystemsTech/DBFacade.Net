using DBFacade.Utils;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{DbParams}" />
    public abstract partial class ValidationRule<DbParams>
        where DbParams : IDbParamsModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Rules.IValidationRule{DbParams}" />
        public class Required : ValidationRule<DbParams>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Required"/> class.
            /// </summary>
            /// <param name="selector">The selector.</param>
            public Required(Selector<DbParams> selector) : base(selector) { }
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(string propertyName)
            {
                return propertyName + " is required.";
            }

            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <returns></returns>
            protected override bool ValidateRule()
            {
                return ParamsValue != null;
            }
        }
    }
}
