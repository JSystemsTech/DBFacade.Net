using System.Reflection;

namespace DomainFacade.DataLayer.Models.Validators.Rules
{
    public sealed class ValidationRuleResult
    {
        public PropertyInfo PropInfo { get; private set; }
        public enum ValidationStatus
        {
            PASS,
            FAIL
        }
        public ValidationStatus Status { get; private set; }
        public string ErrorMessage { get; private set; }
        public object Value { get; private set; }
        public ValidationRuleResult(IDbParamsModel model, PropertyInfo propInfo, string errorMessage, ValidationStatus status)
        {
            PropInfo = propInfo;
            ErrorMessage = errorMessage;
            Status = status;
            Value = propInfo.GetValue(model);
        }
    }
}
