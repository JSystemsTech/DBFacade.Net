using DbFacade;
using DbFacade.DataLayer.Models.Validators.Rules;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using DbFacade.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(DbFacadeConstants.UnitTestAssembly)]
namespace DbFacade.DataLayer.Models.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool IsValid { get; }
        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        IEnumerable<IValidationRuleResult> Errors { get; }

        /// <summary>Gets the error summary.</summary>
        /// <value>The error summary.</value>
        string ErrorSummary { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class ValidationResult : IValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public ValidationResult(IEnumerable<IValidationRuleResult> errors)
        {
            Errors = errors;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IEnumerable<IValidationRuleResult> Errors { get; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid => Errors.Count() == 0;

        /// <summary>Gets the error summary.</summary>
        /// <value>The error summary.</value>
        public string ErrorSummary => string.Join(", ", Errors.Where(m => m.Status == ValidationStatus.FAIL).Select(x => x.ErrorMessage).ToArray());

        /// <summary>
        /// The passing validation
        /// </summary>
        public static ValidationResult PassingValidation = new ValidationResult(new List<ValidationRuleResult>());
    }    

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public interface IValidator<TDbParams>
    {
        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        ValidationRuleFactory<TDbParams> Rules { get; }
        /// <summary>
        /// Adds the specified rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// 
        void Add(IValidationRule<TDbParams> rule, string parameterName = "Unspecified Parameter");
    }
    
    internal interface IValidator
    {
        IValidationResult Validate(object paramsModel);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal class Validator<TDbParams> : IEnumerable<IValidationRule<TDbParams>>, IValidator<TDbParams>, IValidator
    {
        private readonly List<IValidationRule<TDbParams>> ValidationRules;

        public IEnumerator<IValidationRule<TDbParams>> GetEnumerator() 
            => ValidationRules.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() 
            => ValidationRules.GetEnumerator();

        internal Validator()
        {
            ValidationRules = new List<IValidationRule<TDbParams>>();
        }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        public ValidationRuleFactory<TDbParams> Rules => ValidationRuleFactory<TDbParams>.Instance;

        /// <summary>Adds the specified rule.</summary>
        /// <param name="rule">The rule.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public void Add(IValidationRule<TDbParams> rule, string parameterName = "Unspecified Parameter")
        {
            if (rule is ValidationRule<TDbParams> internalRule)
            {
                internalRule.SetParameterName(parameterName);
            }
            ValidationRules.Add(rule);
        }        

        public IValidationResult Validate(object paramsModel)
        {
            if (paramsModel is TDbParams model)
            {
                var results = this.Select(rule => rule.Validate(model));
                var errors = results.Where(result => result.Status == ValidationStatus.FAIL);
                return new ValidationResult(errors);
            }
            else
            {
                throw new FacadeException($"Unable to validate parameters: expected model of type {typeof(TDbParams).TypeName()} but got {paramsModel.TypeName()}");
            }
        }

    }
}