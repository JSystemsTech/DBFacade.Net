using DomainFacade.Utils;
using System;
using System.Linq.Expressions;
using System.Reflection;
using static DomainFacade.DataLayer.Models.Validators.Rules.ValidationRuleResult;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    
    public abstract partial class ValidationRule<U>
        where U : IDbParamsModel
    {
        protected object ParamsValue { get; private set; }

        protected Func<U, object> GetParamFunc { get; private set; }
        protected PropertyInfo PropInfo { get; private set; }
        protected bool IsNullable { get; private set; }

        public ValidationRule(Expression<Func<U, object>> selector)
        {
            init(selector, false);
        }
        public ValidationRule(Expression<Func<U, object>> selector, bool isNullable)
        {
            init(selector, isNullable);
        }
        private void init(Expression<Func<U, object>> selector, bool isNullable)
        {
            GetParamFunc = PropertySelector<U>.GetDelegate(selector);
            PropInfo = PropertySelector<U>.GetPropertyInfo(selector);
            IsNullable = isNullable;
        }
        public ValidationRuleResult Validate(U paramsModel)
        {
            ParamsValue = GetParamFunc(paramsModel);
            if ((IsNullable && ParamsValue == null) || ValidateRule())
            {
                return new ValidationRuleResult(paramsModel, PropInfo, null, ValidationStatus.PASS);
            }
            return new ValidationRuleResult(paramsModel, PropInfo, GetErrorMessage(), ValidationStatus.FAIL);
        }
        protected abstract bool ValidateRule();

        private string GetErrorMessage()
        {
            return GetErrorMessageCore(PropInfo.Name);
        }
        protected abstract string GetErrorMessageCore(string propertyName);               
    }
}
