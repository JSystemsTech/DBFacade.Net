using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.DataLayer.Models.Validators.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainFacade.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class FacadeException: Exception
    {
        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public virtual string Name() { return "FacadeException"; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FacadeException"/> class.
        /// </summary>
        public FacadeException():base() { }
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
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DomainFacade.Exceptions.FacadeException" />
    public class MissingStoredProcedureException : FacadeException
    {
        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name() { return "MissingStoredProcedureException"; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingStoredProcedureException"/> class.
        /// </summary>
        public MissingStoredProcedureException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingStoredProcedureException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public MissingStoredProcedureException(string errorMessage) : base(errorMessage) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingStoredProcedureException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public MissingStoredProcedureException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DomainFacade.Exceptions.FacadeException" />
    public class DataModelConstructionException : FacadeException
    {
        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name() { return "DataModelConstructionException"; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataModelConstructionException"/> class.
        /// </summary>
        public DataModelConstructionException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataModelConstructionException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public DataModelConstructionException(string errorMessage) : base(errorMessage) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataModelConstructionException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DataModelConstructionException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DomainFacade.Exceptions.FacadeException" />
    public class SQLExecutionException : FacadeException
    {
        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name() { return "SQLExecutionException"; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLExecutionException"/> class.
        /// </summary>
        public SQLExecutionException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLExecutionException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public SQLExecutionException(string errorMessage) : base(errorMessage) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLExecutionException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SQLExecutionException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    /// <seealso cref="DomainFacade.Exceptions.FacadeException" />
    public class ValidationException<DbParams> : FacadeException where DbParams: IDbParamsModel
    {
        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name() { return "ValidationException"; }
        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        public IEnumerable<ValidationRuleResult> ValidationErrors { get; private set; }
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; private set; }
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public DbParams Parameters { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException{DbParams}"/> class.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <param name="parameters">The parameters.</param>
        public ValidationException(IValidationResult validationResult, DbParams parameters) : base() {
            Init(validationResult, parameters);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException{DbParams}"/> class.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorMessage">The error message.</param>
        public ValidationException(IValidationResult validationResult, DbParams parameters, string errorMessage) : base(errorMessage) {
            Init(validationResult, parameters);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException{DbParams}"/> class.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ValidationException(IValidationResult validationResult, DbParams parameters, string errorMessage, Exception innerException) : base(errorMessage, innerException) {
            Init(validationResult, parameters);
        }
        /// <summary>
        /// Initializes the specified validation result.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <param name="parameters">The parameters.</param>
        private void Init(IValidationResult validationResult, DbParams parameters)
        {
            ValidationErrors = validationResult.Errors();
            Count = validationResult.Errors().Count();
            Parameters = parameters;
        }
    }
}
