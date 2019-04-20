using System.Reflection;

namespace DBFacade.DataLayer.Models.Validators.Rules
{
    public sealed class ValidationRuleResult
    {
        /// <summary>
        /// Gets the property information.
        /// </summary>
        /// <value>
        /// The property information.
        /// </value>
        public PropertyInfo PropInfo { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public enum ValidationStatus
        {
            PASS,
            FAIL
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
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRuleResult"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="propInfo">The property information.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="status">The status.</param>
        public ValidationRuleResult(IDbParamsModel model, PropertyInfo propInfo, string errorMessage, ValidationStatus status)
        {
            PropInfo = propInfo;
            ErrorMessage = errorMessage;
            Status = status;
            Value = propInfo.GetValue(model);
        }
    }
}
