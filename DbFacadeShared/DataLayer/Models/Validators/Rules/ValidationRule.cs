﻿using System;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbFacade.Utils;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="Rules.IValidationRule{TDbParams}" />
    public partial class ValidationRule<TDbParams> : IValidationRule<TDbParams>
    {
        protected ValidationRule() { }

        protected object ParamsValue { get; private set; }
        protected Func<TDbParams, object> GetParamFunc { get; private set; }

        protected bool IsNullable { get; private set; }
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
        public async Task<IValidationRuleResult> ValidateAsync(TDbParams paramsModel)
        {
            ParamsValue = GetParamFunc(paramsModel);
            if (ParamsValue == null)
                return IsNullable
                    ? await ValidationRuleResult.CreateAsync(paramsModel, null, ValidationStatus.PASS)
                    : await ValidationRuleResult.CreateAsync(paramsModel, await GetErrorMessageAsync(), ValidationStatus.FAIL);
            return await ValidateRuleAsync()
                ? await ValidationRuleResult.CreateAsync(paramsModel, null, ValidationStatus.PASS)
                : await ValidationRuleResult.CreateAsync(paramsModel, await GetErrorMessageAsync(), ValidationStatus.FAIL);
        }

        public static ValidationRule<TDbParams> GetRules()
        {
            return new ValidationRuleInstance();
        }
        
        

        private void Init<T>(Func<TDbParams, T> selector, bool isNullable)
        {
            GetParamFunc = model => selector(model);
            IsNullable = isNullable;
        }
        private async Task InitAsync<T>(Func<TDbParams, T> selector, bool isNullable)
        {
            GetParamFunc = model => selector(model);
            IsNullable = isNullable;
            await Task.CompletedTask;
        }
        private void Init()
        {
            GetParamFunc = model => model;
            IsNullable = true;
        }
        private async Task InitAsync()
        {
            Init();
            await Task.CompletedTask;
        }

        /// <summary>
        ///     Validates the rule.
        /// </summary>
        /// <returns></returns>
        protected virtual bool ValidateRule()
        {
            return true;
        }
        protected virtual async Task<bool> ValidateRuleAsync()
        {
            await Task.CompletedTask;
            return true;
        }

        /// <summary>
        ///     Gets the error message.
        /// </summary>
        /// <returns></returns>
        private string GetErrorMessage()
        => GetErrorMessageCore();
        private async Task<string> GetErrorMessageAsync()
        => await GetErrorMessageCoreAsync();

        /// <summary>
        ///     Gets the error message core.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected virtual string GetErrorMessageCore()
        => string.Empty;
        protected virtual async Task<string> GetErrorMessageCoreAsync()
        {
            await Task.CompletedTask;
            return string.Empty;
        }

        private class ValidationRuleInstance : ValidationRule<TDbParams>
        {
            protected override string GetErrorMessageCore()
            {
                return string.Empty;
            }
            protected override async Task<string> GetErrorMessageCoreAsync()
            {
                await Task.CompletedTask;
                return string.Empty;
            }
            protected override bool ValidateRule()
            {
                return true;
            }
            protected override async Task<bool> ValidateRuleAsync()
            {
                await Task.CompletedTask;
                return true;
            }
        }
    }
}