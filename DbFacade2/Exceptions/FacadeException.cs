using System;
using System.Linq;

namespace DbFacade.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class FacadeException : Exception
    {
        internal FacadeException(string errorMessage) : base(errorMessage) { }
        internal FacadeException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }

        /// <summary>Gets the error details.</summary>
        /// <value>The error details.</value>
        public virtual string ErrorDetails => InnerException is Exception innerException ? innerException.Message : "";

    }
    /// <summary>
    /// 
    /// </summary>
    public class DbExecutionException : FacadeException
    {
        internal DbExecutionException(string message, Exception innerException) : base(
            message, innerException) { }
    }

    /// <summary>
    ///   <br />
    /// </summary>
    public class ValidationException : FacadeException
    {
        internal ValidationException(string[] errors, object parameters, string errorMessage) : base(
            errorMessage)
        {
            ValidationErrors = errors;
            Parameters = parameters;
        }
        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        public string[] ValidationErrors { get; private set; }
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count => ValidationErrors.Count();
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public object Parameters { get; private set; }

        /// <summary>Gets the error details.</summary>
        /// <value>The error details.</value>
        public override string ErrorDetails => string.Join(" ", ValidationErrors);
    }
}