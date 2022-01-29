using System;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public partial class ValidationRule<TDbParams> : IValidationRule<TDbParams>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule{TDbParams}" /> class.
        /// </summary>
        protected ValidationRule() { }

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
        /// Gets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is nullable; otherwise, <c>false</c>.
        /// </value>
        protected bool IsNullable { get; private set; }
        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public IValidationRuleResult Validate(TDbParams paramsModel)
        {
            ParamsValue = GetParamFunc(paramsModel);
            if (ParamsValue == null)
                return IsNullable
                    ? new ValidationRuleResult(paramsModel, null, ValidationStatus.PASS)
                    : new ValidationRuleResult(paramsModel, GetErrorMessage(paramsModel), ValidationStatus.FAIL);
            return ValidateRule(paramsModel)
                ? new ValidationRuleResult(paramsModel, null, ValidationStatus.PASS)
                : new ValidationRuleResult(paramsModel, GetErrorMessage(paramsModel), ValidationStatus.FAIL);
        }
        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public async Task<IValidationRuleResult> ValidateAsync(TDbParams paramsModel)
        {
            ParamsValue = GetParamFunc(paramsModel);
            if (ParamsValue == null)
                return IsNullable
                    ? await ValidationRuleResult.CreateAsync(paramsModel, null, ValidationStatus.PASS)
                    : await ValidationRuleResult.CreateAsync(paramsModel, await GetErrorMessageAsync(paramsModel), ValidationStatus.FAIL);
            return await ValidateRuleAsync(paramsModel)
                ? await ValidationRuleResult.CreateAsync(paramsModel, null, ValidationStatus.PASS)
                : await ValidationRuleResult.CreateAsync(paramsModel, await GetErrorMessageAsync(paramsModel), ValidationStatus.FAIL);
        }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <returns></returns>
        public static ValidationRule<TDbParams> GetRules()
        {
            return new ValidationRuleInstance();
        }



        /// <summary>
        /// Initializes the specified selector.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <param name="isNullable">if set to <c>true</c> [is nullable].</param>
        private void Init<T>(Func<TDbParams, T> selector, bool isNullable)
        {
            GetParamFunc = model => selector(model);
            IsNullable = isNullable;
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Init()
        {
            GetParamFunc = model => model;
            IsNullable = true;
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        private async Task InitAsync()
        {
            Init();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Validates the rule.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        protected virtual bool ValidateRule(TDbParams paramsModel)
        {
            return true;
        }
        /// <summary>
        /// Validates the rule asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        protected virtual async Task<bool> ValidateRuleAsync(TDbParams paramsModel)
        {
            await Task.CompletedTask;
            return true;
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        private string GetErrorMessage(TDbParams paramsModel)
        => GetErrorMessageCore(paramsModel);
        /// <summary>
        /// Gets the error message asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        private async Task<string> GetErrorMessageAsync(TDbParams paramsModel)
        => await GetErrorMessageCoreAsync(paramsModel);

        /// <summary>
        /// Gets the error message core.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        protected virtual string GetErrorMessageCore(TDbParams paramsModel)
        => string.Empty;
        /// <summary>
        /// Gets the error message core asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        protected virtual async Task<string> GetErrorMessageCoreAsync(TDbParams paramsModel)
        {
            await Task.CompletedTask;
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        private class ValidationRuleInstance : ValidationRule<TDbParams>
        {
            /// <summary>
            /// Gets the error message core.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override string GetErrorMessageCore(TDbParams paramsModel)
            {
                return string.Empty;
            }
            /// <summary>
            /// Gets the error message core asynchronous.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override async Task<string> GetErrorMessageCoreAsync(TDbParams paramsModel)
            {
                await Task.CompletedTask;
                return string.Empty;
            }
            /// <summary>
            /// Validates the rule.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override bool ValidateRule(TDbParams paramsModel)
            {
                return true;
            }
            /// <summary>
            /// Validates the rule asynchronous.
            /// </summary>
            /// <param name="paramsModel">The parameters model.</param>
            /// <returns></returns>
            protected override async Task<bool> ValidateRuleAsync(TDbParams paramsModel)
            {
                await Task.CompletedTask;
                return true;
            }
        }
    }
}