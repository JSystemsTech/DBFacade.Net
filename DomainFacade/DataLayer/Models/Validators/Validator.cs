using DomainFacade.DataLayer.Models.Validators.Rules;
using System.Collections.Generic;
using System.Reflection;

namespace DomainFacade.DataLayer.Models.Validators
{
   
    public class Validator<DbParams> : List<IValidationRule<DbParams>>
        where DbParams : IDbParamsModel
    {
        public bool Validate(DbParams paramsModel)
        {
            return ValidateCore(paramsModel);
        }

        private bool ValidateCore(DbParams paramsModel)
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

            return errors.Count == 0;
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
