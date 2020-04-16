using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBFacade.DataLayer.Models.Validators.Rules;

namespace DBFacade.DataLayer.Models.Validators
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