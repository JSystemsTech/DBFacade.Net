﻿using DBFacade.DataLayer.Models.Validators.Rules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DBFacade.DataLayer.Models.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidationResult
    {
        bool IsValid();
        IEnumerable<ValidationRuleResult> Errors();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IValidationResult" />
    internal class ValidationResult : IValidationResult
    {
        /// <summary>
        /// Gets or sets the validation errors.
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        private IEnumerable<ValidationRuleResult> ValidationErrors { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public ValidationResult(IEnumerable<ValidationRuleResult> errors)
        {
            ValidationErrors = errors;
        }
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return ValidationErrors.Count() == 0;
        }
        /// <summary>
        /// Errorses this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidationRuleResult> Errors()
        {
            return ValidationErrors;
        }
        /// <summary>
        /// Passings the validation.
        /// </summary>
        /// <returns></returns>
        public static ValidationResult PassingValidation()
        {
            return new ValidationResult(new List<ValidationRuleResult>());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="System.Collections.Generic.List{IValidationRule{DbParams}}" />
    public class Validator<DbParams> : List<IValidationRule<DbParams>>
        where DbParams : IDbParamsModel
    {
        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public IValidationResult Validate(DbParams paramsModel)
        {
            return ValidateCore(paramsModel);
        }

        /// <summary>
        /// Validates the core.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="propName">Name of the property.</param>
        /// <returns></returns>
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        /// <summary>
        /// Gets the property information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo(string name)
        {
            return typeof(DbParams).GetProperty(name);
        }
    }

}