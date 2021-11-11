using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models.Validators.Rules
{
    /// <summary>
    /// 
    /// </summary>
    public enum ValidationStatus
    {
        /// <summary>
        /// The pass
        /// </summary>
        PASS,
        /// <summary>
        /// The fail
        /// </summary>
        FAIL
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IValidationRuleResult
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        ValidationStatus Status { get; }
        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        string ErrorMessage { get; }
        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        object Model { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    internal sealed class ValidationRuleResult : IValidationRuleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRuleResult" /> class.
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
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRuleResult"/> class.
        /// </summary>
        public ValidationRuleResult() { }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public static async Task<ValidationRuleResult> CreateAsync(object model, string errorMessage, ValidationStatus status)
        {
            ValidationRuleResult result = new ValidationRuleResult();
            await result.InitializeAsync(model, errorMessage, status);
            return result;
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="status">The status.</param>
        private async Task InitializeAsync(object model, string errorMessage, ValidationStatus status)
        {
            Model = model;
            ErrorMessage = errorMessage;
            Status = status;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public ValidationStatus Status { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; private set; }


        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public object Model { get; private set; }
    }
}