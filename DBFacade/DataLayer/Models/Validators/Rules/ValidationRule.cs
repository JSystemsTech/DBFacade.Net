using DBFacade.Utils;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using static DBFacade.DataLayer.Models.Validators.Rules.ValidationRuleResult;

namespace DBFacade.DataLayer.Models.Validators.Rules
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public partial class ValidationRule<TDbParams> : IValidationRule<TDbParams>
        where TDbParams : IDbParamsModel
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
        protected Func<TDbParams, object> GetParamFunc { get; private set; }
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

        public static ValidationRule<TDbParams> GetRules() { return new ValidationRuleInstance(); }
        private class ValidationRuleInstance : ValidationRule<TDbParams>
        {
            protected override string GetErrorMessageCore(string propertyName)=> "";

            protected override bool ValidateRule() => true;
        }
        public ValidationRule() { }

        public ValidationRule(Func<TDbParams, object> selector, bool isNullable = false)=> init(selector, isNullable);
        public ValidationRule(Func<TDbParams, short> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, int> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, long> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, ushort> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, uint> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, ulong> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, double> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, float> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, decimal> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, string> selector, bool isNullable = false) => init(selector, isNullable);
        public ValidationRule(Func<TDbParams, char> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, TimeSpan> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, DateTime> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, DateTimeOffset> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, bool> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, byte> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, sbyte> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, byte[]> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, char[]> selector) => init(selector, false);
        public ValidationRule(Func<TDbParams, Xml> selector) => init(selector, false);

        public ValidationRule(Func<TDbParams, short?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, int?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, long?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, ushort?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, uint?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, ulong?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, double?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, float?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, decimal?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, char?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, TimeSpan?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, DateTime?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, DateTimeOffset?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, bool?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, byte?> selector, bool isNullable = false) => init(selector, true);
        public ValidationRule(Func<TDbParams, sbyte?> selector, bool isNullable = false) => init(selector, true);


        private void init<T>(Func<TDbParams, T> selector, bool isNullable)
        {
            GetParamFunc = model => selector(model);
            PropInfo = PropertySelector<TDbParams>.GetPropertyInfo(selector);
            IsNullable = isNullable;
        }
        
        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public async Task<ValidationRuleResult> ValidateAsync(TDbParams paramsModel)
        {
            return await Task.Run(() => Validate(paramsModel));
        }
        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public ValidationRuleResult Validate(TDbParams paramsModel)
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
        protected virtual bool ValidateRule() => true;

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
        protected virtual string GetErrorMessageCore(string propertyName) => string.Empty;
    }
}
