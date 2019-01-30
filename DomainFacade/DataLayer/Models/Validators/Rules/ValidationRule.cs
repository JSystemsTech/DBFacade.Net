using DomainFacade.Utils;
using System;
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
        private  static dynamic ParamsProperties = GenericInstance<U>.GetInstance().GetModelProperties();

        public ValidationRule(Func<dynamic, PropertyInfo> getPropInfo )
        {
            PropInfo = getPropInfo(ParamsProperties);
            IsNullable = false;
        }
        public ValidationRule(Func<dynamic, PropertyInfo> getPropInfo, bool isNullable)
        {
            PropInfo = getPropInfo(ParamsProperties);
            IsNullable = isNullable;
        }
        public ValidationRuleResult Validate(U paramsModel)
        {
            ParamsValue = PropInfo.GetValue(paramsModel);
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
