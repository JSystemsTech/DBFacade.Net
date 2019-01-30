using DomainFacade.DataLayer.Models.Validators.Rules;
using DomainFacade.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DomainFacade.DataLayer.Models.Validators
{
   public interface IValidator<Par> where Par : IDbParamsModel
    {
        Validator<Par> GetValidator();
    }
    public sealed class Validator<Par> : List<ValidationRule<Par>>
        where Par : IDbParamsModel
    {
        public bool Validate(Par paramsModel)
        {
            return ValidateCore(paramsModel);
        }
        
        private bool ValidateCore(Par paramsModel)
        {
            List<ValidationRuleResult> errors = new List<ValidationRuleResult>();
            foreach (ValidationRule<Par> rule in this)
            {
                ValidationRuleResult validationResult = rule.Validate(paramsModel);
                if(validationResult.Status == ValidationRuleResult.ValidationStatus.FAIL)
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
            return typeof(Par).GetProperty(name);
        }
        public static dynamic ParamsProperties = GenericInstance<Par>.GetInstance().GetModelProperties();
    }

}
