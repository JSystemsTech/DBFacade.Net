using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.DataLayer.Models.Validators.Rules;

namespace DbFacade.Exceptions
{
    public class FacadeException : Exception
    {
        public FacadeException()
        {
        }

        public FacadeException(string errorMessage) : base(errorMessage)
        {
        }

        public FacadeException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
        {
        }

        public virtual string Name()
        {
            return "FacadeException";
        }
    }

    public class SQLExecutionException : FacadeException
    {
        public SQLExecutionException(string errorMessage, IDbCommandText sqlMethod, SqlException innerException) : base(
            errorMessage, innerException)
        {
            SqlMethod = SqlMethod;
        }

        public IDbCommandText SqlMethod { get; set; }

        public override string Name()
        {
            return "SQLExecutionException";
        }
    }

    public class ValidationException<DbParams> : FacadeException where DbParams : IDbParamsModel
    {
        public ValidationException(IValidationResult validationResult, DbParams parameters)
        {
            Init(validationResult, parameters);
        }

        public ValidationException(IValidationResult validationResult, DbParams parameters, string errorMessage) : base(
            errorMessage)
        {
            Init(validationResult, parameters);
        }

        public ValidationException(IValidationResult validationResult, DbParams parameters, string errorMessage,
            Exception innerException) : base(errorMessage, innerException)
        {
            Init(validationResult, parameters);
        }

        public IEnumerable<IValidationRuleResult> ValidationErrors { get; private set; }
        public int Count { get; private set; }
        public DbParams Parameters { get; private set; }

        public override string Name()
        {
            return "ValidationException";
        }

        private void Init(IValidationResult validationResult, DbParams parameters)
        {
            ValidationErrors = validationResult.Errors;
            Count = validationResult.Errors.Count();
            Parameters = parameters;
        }
    }
}