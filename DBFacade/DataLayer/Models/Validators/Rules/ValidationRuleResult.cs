namespace DBFacade.DataLayer.Models.Validators.Rules
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
    }

    internal sealed class ValidationRuleResult : IValidationRuleResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationRuleResult" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="status">The status.</param>
        public ValidationRuleResult(IDbParamsModel model, string errorMessage, ValidationStatus status)
        {
            ErrorMessage = errorMessage;
            Status = status;
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        public ValidationStatus Status { get; }

        /// <summary>
        ///     Gets the error message.
        /// </summary>
        /// <value>
        ///     The error message.
        /// </value>
        public string ErrorMessage { get; }
    }
}