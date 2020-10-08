using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models.Validators.Rules;

namespace DbFacade.DataLayer.Models.Validators
{
    /// <summary>
    /// </summary>
    public interface IValidationResult
    {
        bool IsValid { get; }
        IEnumerable<IValidationRuleResult> Errors { get; }
    }

    /// <summary>
    /// </summary>
    /// <seealso cref="IValidationResult" />
    internal class ValidationResult : IValidationResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationResult" /> class.
        /// </summary>
        /// <param name="errors">The errors.</param>
        public ValidationResult(IEnumerable<IValidationRuleResult> errors)
        {
            Errors = errors;
        }

        /// <summary>
        ///     Gets or sets the validation errors.
        /// </summary>
        /// <value>
        ///     The validation errors.
        /// </value>
        public IEnumerable<IValidationRuleResult> Errors { get; }

        /// <summary>
        ///     Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid => !Errors.Any();

        /// <summary>
        ///     Passes the validation.
        /// </summary>
        /// <returns></returns>
        public static ValidationResult PassingValidation()
        {
            return new ValidationResult(new List<ValidationRuleResult>());
        }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TDbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="System.Collections.Generic.List{IValidationRule&lt;TDbParams&gt;}" />
    public class Validator<TDbParams> : List<IValidationRule<TDbParams>>
        where TDbParams : IDbParamsModel
    {
        public static Validator<TDbParams> Create(params IValidationRule<TDbParams>[] rules)
        {
            Validator<TDbParams> validator = new Validator<TDbParams>();
            foreach (IValidationRule<TDbParams> rule in rules)
            {
                validator.Add(rule);
            }
            return validator;
        }
        public static async Task<Validator<TDbParams>> CreateAsync(params IValidationRule<TDbParams>[] rules)
        {
            Validator<TDbParams> validator = new Validator<TDbParams>();
            await validator.InitAsync(rules);
            return validator;
        }
        private async Task InitAsync(params IValidationRule<TDbParams>[] rules)
        {
            foreach (IValidationRule<TDbParams> rule in rules)
            {
                Add(rule);
            }
            await Task.CompletedTask;
        }

        public IValidationResult Validate(TDbParams paramsModel)
        {
            return ValidateCore(paramsModel);
        }

        public async Task<IValidationResult> ValidateAsync(TDbParams paramsModel)
        {
            return await ValidateCoreAsync(paramsModel);
        }


        private async Task<IValidationResult> ValidateCoreAsync(TDbParams paramsModel)
        {
            var validationTasks = this.Select(rule => rule.ValidateAsync(paramsModel)).ToArray();
            await Task.WhenAll(validationTasks);
            return new ValidationResult(validationTasks.Where(task => task.Result.Status == ValidationStatus.FAIL)
                .Select(task => task.Result));
        }

        private IValidationResult ValidateCore(TDbParams paramsModel)
        {
            return new ValidationResult(this.Select(rule => rule.Validate(paramsModel))
                .Where(result => result.Status == ValidationStatus.FAIL));
        }
    }
}