using DbFacade.DataLayer.Models.Validators.Rules;
using DbFacade.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public bool IsValid => !Errors.Any();

        /// <summary>Gets the error summary.</summary>
        /// <value>The error summary.</value>
        public string ErrorSummary => string.Join(", ", Errors.Where(m=> m.Status == ValidationStatus.FAIL).Select(x => x.ErrorMessage).ToArray());

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
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; }
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
        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <returns></returns>
        Task AddAsync(IValidationRule<TDbParams> rule, string parameterName = "Unspecified Parameter");
        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        IValidationResult Validate(TDbParams paramsModel);
        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        Task<IValidationResult> ValidateAsync(TDbParams paramsModel);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal class Validator<TDbParams> : List<IValidationRule<TDbParams>>, IValidator<TDbParams>
    {
        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        public ValidationRuleFactory<TDbParams> Rules { get; private set; }
        /// <summary>Adds the asynchronous.</summary>
        /// <param name="rule">The rule.</param>
        /// <param name="parameterName"></param>
        public async Task AddAsync(IValidationRule<TDbParams> rule, string parameterName = "Unspecified Parameter")
        {
            if (rule is IValidationRuleInternal<TDbParams> internalRule)
            {
                internalRule.SetParameterName(parameterName);
            }
            base.Add(rule);
            await Task.CompletedTask;
        }

        /// <summary>Adds the specified rule.</summary>
        /// <param name="rule">The rule.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public void Add(IValidationRule<TDbParams> rule, string parameterName = "Unspecified Parameter")
        {
            if (rule is IValidationRuleInternal<TDbParams> internalRule)
            {
                internalRule.SetParameterName(parameterName);
            }
            base.Add(rule);
        }
        /// <summary>
        /// Creates the specified validator initializer.
        /// </summary>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        public static Validator<TDbParams> Create(Action<IValidator<TDbParams>> validatorInitializer = null)
        {
            Validator<TDbParams> validator = new Validator<TDbParams>();            
            validator.Rules = new ValidationRuleFactory<TDbParams>();
            Action<IValidator<TDbParams>> _validatorInitializer =
                validatorInitializer != null ? validatorInitializer : v => { };
            _validatorInitializer(validator);
            return validator;
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="validatorInitializer">The validator initializer.</param>
        /// <returns></returns>
        public static async Task<Validator<TDbParams>> CreateAsync(Func<IValidator<TDbParams>, Task> validatorInitializer = null)
        {
            Validator<TDbParams> validator = new Validator<TDbParams>();
            validator.Rules = await ValidationRuleFactory<TDbParams>.CreateFactoryAsync();
            Func<IValidator<TDbParams>, Task> _validatorInitializer =
                validatorInitializer != null ? validatorInitializer : async v => { await Task.CompletedTask; };
            await _validatorInitializer(validator);           
            return validator;
        }

        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public IValidationResult Validate(TDbParams paramsModel)
        => new ValidationResult(this.Select(rule => rule.Validate(paramsModel))
                .Where(result => result.Status == ValidationStatus.FAIL));


        /// <summary>
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public async Task<IValidationResult> ValidateAsync(TDbParams paramsModel)
        {
            var validationTasks = this.Select(rule => rule.ValidateAsync(paramsModel)).ToArray();
            await Task.WhenAll(validationTasks);
            return new ValidationResult(validationTasks.Where(task => task.Result.Status == ValidationStatus.FAIL)
                .Select(task => task.Result));
        }
    }
}