using DBFacade.Utils;
using System;
using System.Reflection;
using static DBFacade.DataLayer.Models.Validators.Rules.ValidationRuleResult;

namespace DBFacade.DataLayer.Models.Validators.Rules
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{DbParams}" />
    public abstract partial class ValidationRule<DbParams> : IValidationRule<DbParams>
        where DbParams : IDbParamsModel
    {
        /// <summary>
        /// Gets the parameters value.
        /// </summary>
        /// <value>
        /// The parameters value.
        /// </value>
        protected object ParamsValue { get; private set; }

        /// <summary>
        /// Gets the get parameter function.
        /// </summary>
        /// <value>
        /// The get parameter function.
        /// </value>
        protected Func<DbParams, object> GetParamFunc { get; private set; }
        /// <summary>
        /// Gets the property information.
        /// </summary>
        /// <value>
        /// The property information.
        /// </value>
        protected PropertyInfo PropInfo { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        protected bool IsNullable { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule{DbParams}"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        public ValidationRule(Selector<DbParams> selector)
        {
            init(selector, false);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule{DbParams}"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        public ValidationRule(Selector<DbParams> selector, bool isNullable)
        {
            init(selector, isNullable);
        }
        /// <summary>
        /// Initializes the specified selector.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        private void init(Selector<DbParams> selector, bool isNullable)
        {
            GetParamFunc = PropertySelector<DbParams>.GetDelegate(selector.SelectorExpression);
            PropInfo = PropertySelector<DbParams>.GetPropertyInfo(selector.SelectorExpression);
            IsNullable = isNullable;
        }
        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public ValidationRuleResult Validate(DbParams paramsModel)
        {
            ParamsValue = GetParamFunc(paramsModel);
            if ((IsNullable && ParamsValue == null) || ValidateRule())
            {
                return new ValidationRuleResult(paramsModel, PropInfo, null, ValidationStatus.PASS);
            }
            return new ValidationRuleResult(paramsModel, PropInfo, GetErrorMessage(), ValidationStatus.FAIL);
        }
        /// <summary>
        /// Validates the rule.
        /// </summary>
        /// <returns></returns>
        protected abstract bool ValidateRule();

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <returns></returns>
        private string GetErrorMessage()
        {
            return GetErrorMessageCore(PropInfo.Name);
        }
        /// <summary>
        /// Gets the error message core.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected abstract string GetErrorMessageCore(string propertyName);
    }
}
