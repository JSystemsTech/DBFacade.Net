using DomainFacade.DataLayer.Models.Validators.Rules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DomainFacade.DataLayer.Models.Validators
{
    public interface IValidationResult
    {
        bool IsValid();
        IEnumerable<ValidationRuleResult> Errors();
    }
    internal class ValidationResult:IValidationResult
    {
        private IEnumerable<ValidationRuleResult> ValidationErrors { get; set; }

        public ValidationResult(IEnumerable<ValidationRuleResult> errors)
        {
            ValidationErrors = errors;
        }
        public bool IsValid()
        {
            return ValidationErrors.Count() == 0;
        }
        public IEnumerable<ValidationRuleResult> Errors()
        {
            return ValidationErrors;
        }
        public static ValidationResult PassingValidation()
        {
            return new ValidationResult(new List<ValidationRuleResult>());
        }                
    }
    public class Validator<DbParams> : List<IValidationRule<DbParams>>
        where DbParams : IDbParamsModel
    {
        public IValidationResult Validate(DbParams paramsModel)
        {
            return ValidateCore(paramsModel);
        }

        private IValidationResult ValidateCore(DbParams paramsModel)
        {
            List<ValidationRuleResult> errors = new List<ValidationRuleResult>();
            foreach (IValidationRule<DbParams> rule in this)
            {
                ValidationRuleResult validationResult = rule.Validate(paramsModel);
                if (validationResult.Status == ValidationRuleResult.ValidationStatus.FAIL)
                {
                    errors.Add(validationResult);
                }
            }
            return new ValidationResult(errors);
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static PropertyInfo GetPropertyInfo(string name)
        {
            return typeof(DbParams).GetProperty(name);
        }
    }

}
