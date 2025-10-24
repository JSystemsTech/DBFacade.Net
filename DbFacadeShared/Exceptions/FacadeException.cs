using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.DataLayer.Models.Validators.Rules;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DbFacade.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class FacadeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacadeException"/> class.
        /// </summary>
        public FacadeException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacadeException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public FacadeException(string errorMessage) : base(errorMessage) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacadeException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public FacadeException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }

        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public virtual string Name() => "FacadeException";

        public virtual string ErrorDetails => InnerException is Exception innerException ? $"{innerException.Message}" : "";

    }
    /// <summary>
    /// 
    /// </summary>
    public class DbConnectionConfigNotRegisteredException : FacadeException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionConfigNotRegisteredException"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        internal DbConnectionConfigNotRegisteredException() : base($"Connection has not been registered yet") { }

        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name() => "DbConnectionConfigNotRegisteredException";

    }
    /// <summary>
    /// 
    /// </summary>
    public class DbExecutionException : FacadeException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLExecutionException"/> class.
        /// </summary>
        /// <param name="dbMethod">The SQL method.</param>
        /// <param name="innerException">The inner exception.</param>
        internal DbExecutionException(string message, Exception innerException) : base(
            message, innerException) { }

        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name() => "DbExecutionException";

    }

    /// <summary>
    ///   <br />
    /// </summary>
    public class ValidationException : FacadeException
    {

        /// <summary>Initializes a new instance of the <see cref="ValidationException" /> class.</summary>
        /// <param name="validationResult">The validation result.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorMessage">The error message.</param>
        internal ValidationException(IValidationResult validationResult, object parameters, string errorMessage) : base(
            errorMessage)
        {
            Init(validationResult, parameters);
        }
        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        public IEnumerable<IValidationRuleResult> ValidationErrors => ValidationResult.Errors;
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

        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name() => "ValidationException";

        /// <summary>Gets or sets the validation result.</summary>
        /// <value>The validation result.</value>
        private IValidationResult ValidationResult { get; set; }


        /// <summary>
        /// Initializes the specified validation result.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <param name="parameters">The parameters.</param>
        private void Init(IValidationResult validationResult, object parameters)
        {
            ValidationResult = validationResult;
            Parameters = parameters;
        }



        public override string ErrorDetails => $"{ValidationResult.ErrorSummary}";
    }
}