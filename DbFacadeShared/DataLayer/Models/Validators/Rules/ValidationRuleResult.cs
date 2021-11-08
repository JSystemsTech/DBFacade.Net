using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    public enum ValidationStatus
    {
        PASS,
        FAIL
    }

    public interface IValidationRuleResult
    {
        ValidationStatus Status { get; }
        string ErrorMessage { get; }
        object Model { get; }
    }

    internal sealed class ValidationRuleResult : IValidationRuleResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationRuleResult" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="status">The status.</param>
        public ValidationRuleResult(object model, string errorMessage, ValidationStatus status)
        {
            Model = model;
            ErrorMessage = errorMessage;
            Status = status;
        }
        public ValidationRuleResult() { }
        public static async Task<ValidationRuleResult> CreateAsync(object model, string errorMessage, ValidationStatus status)
        {
            ValidationRuleResult result = new ValidationRuleResult();
            await result.InitializeAsync(model, errorMessage, status);
            return result;
        }
        private async Task InitializeAsync(object model, string errorMessage, ValidationStatus status)
        {
            Model = model;
            ErrorMessage = errorMessage;
            Status = status;
            await Task.CompletedTask;
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        public ValidationStatus Status { get; private set; }

        /// <summary>
        ///     Gets the error message.
        /// </summary>
        /// <value>
        ///     The error message.
        /// </value>
        public string ErrorMessage { get; private set; }


        public object Model { get; private set; }
    }
}