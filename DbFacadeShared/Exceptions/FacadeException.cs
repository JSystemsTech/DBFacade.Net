using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.DataLayer.Models.Validators.Rules;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public virtual string Name()=> "FacadeException";
        
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
        internal DbConnectionConfigNotRegisteredException(Type type): base($"{type.Name} has not been registered yet") { }

        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name() => "DbConnectionConfigNotRegisteredException";

    }
    /// <summary>
    /// 
    /// </summary>
    public class SQLExecutionException : FacadeException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLExecutionException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="sqlMethod">The SQL method.</param>
        /// <param name="innerException">The inner exception.</param>
        internal SQLExecutionException(string errorMessage, DbCommandSettingsBase sqlMethod, SqlException innerException) : base(
            errorMessage, innerException)
        {
            CommandText = sqlMethod.CommandText;
        }

        /// <summary>
        /// Gets the command text.
        /// </summary>
        /// <value>
        /// The command text.
        /// </value>
        public string CommandText { get; private set; }

        /// <summary>
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name()=> "SQLExecutionException";
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
    public class ValidationException<DbParams> : FacadeException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException{DbParams}"/> class.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="errorMessage">The error message.</param>
        internal ValidationException(IValidationResult validationResult, DbParams parameters, string errorMessage) : base(
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
        public IEnumerable<IValidationRuleResult> ValidationErrors { get; private set; }
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
        /// Names this instance.
        /// </summary>
        /// <returns></returns>
        public override string Name()=> "ValidationException";


        /// <summary>
        /// Initializes the specified validation result.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <param name="parameters">The parameters.</param>
        private void Init(IValidationResult validationResult, DbParams parameters)
        {
            ValidationErrors = validationResult.Errors;
            Count = validationResult.Errors.Count();
            Parameters = parameters;
        }
    }
}