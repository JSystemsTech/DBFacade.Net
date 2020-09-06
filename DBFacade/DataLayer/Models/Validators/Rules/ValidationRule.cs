using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DBFacade.Utils;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public partial class ValidationRule<TDbParams> : IValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
    {
        protected ValidationRule()
        {
        }

        protected ValidationRule(Expression<Func<TDbParams, object>> selector, bool isNullable = false)
        {
            Init(selector, isNullable);
        }

        protected ValidationRule(Expression<Func<TDbParams, short>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, int>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, long>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, ushort>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, uint>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, ulong>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, double>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, float>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, decimal>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, string>> selector, bool isNullable = false)
        {
            Init(selector, isNullable);
        }

        protected ValidationRule(Expression<Func<TDbParams, char>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, TimeSpan>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, DateTime>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, DateTimeOffset>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, bool>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, byte>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, sbyte>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, byte[]>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, char[]>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, Xml>> selector)
        {
            Init(selector, false);
        }

        protected ValidationRule(Expression<Func<TDbParams, short?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, int?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, long?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, ushort?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, uint?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, ulong?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, double?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, float?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, decimal?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, char?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, TimeSpan?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, DateTime?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, DateTimeOffset?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, bool?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, byte?>> selector)
        {
            Init(selector, true);
        }

        protected ValidationRule(Expression<Func<TDbParams, sbyte?>> selector)
        {
            Init(selector, true);
        }

        /// <summary>
        ///     Gets the parameters value.
        /// </summary>
        /// <value>
        ///     The parameters value.
        /// </value>
        protected object ParamsValue { get; private set; }

        /// <summary>
        ///     Gets the get parameter function.
        /// </summary>
        /// <value>
        ///     The get parameter function.
        /// </value>
        protected Func<TDbParams, object> GetParamFunc { get; private set; }

        private string PropertyName { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        protected bool IsNullable { get; private set; }

        /// <summary>
        ///     Validates the asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public async Task<IValidationRuleResult> ValidateAsync(TDbParams paramsModel)
        {
            return await Task.Run(() => Validate(paramsModel));
        }

        /// <summary>
        ///     Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public IValidationRuleResult Validate(TDbParams paramsModel)
        {
            ParamsValue = GetParamFunc(paramsModel);
            if (ParamsValue == null)
                return IsNullable
                    ? new ValidationRuleResult(paramsModel, null, ValidationStatus.PASS)
                    : new ValidationRuleResult(paramsModel, GetErrorMessage(), ValidationStatus.FAIL);
            return ValidateRule()
                ? new ValidationRuleResult(paramsModel, null, ValidationStatus.PASS)
                : new ValidationRuleResult(paramsModel, GetErrorMessage(), ValidationStatus.FAIL);
        }

        public static ValidationRule<TDbParams> GetRules()
        {
            return new ValidationRuleInstance();
        }


        private void Init<T>(Expression<Func<TDbParams, T>> selector, bool isNullable)
        {
            GetParamFunc = model => selector.Compile()(model);
            PropertyName = PropertySelector<TDbParams>.GetPropertyName(selector);
            IsNullable = isNullable;
        }

        /// <summary>
        ///     Validates the rule.
        /// </summary>
        /// <returns></returns>
        protected virtual bool ValidateRule()
        {
            return true;
        }

        /// <summary>
        ///     Gets the error message.
        /// </summary>
        /// <returns></returns>
        private string GetErrorMessage()
        {
            return GetErrorMessageCore(PropertyName);
        }

        /// <summary>
        ///     Gets the error message core.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected virtual string GetErrorMessageCore(string propertyName)
        {
            return string.Empty;
        }

        private class ValidationRuleInstance : ValidationRule<TDbParams>
        {
            protected override string GetErrorMessageCore(string propertyName)
            {
                return "";
            }

            protected override bool ValidateRule()
            {
                return true;
            }
        }
    }
}