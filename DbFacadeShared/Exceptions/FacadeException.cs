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
        public FacadeException() { }

        public FacadeException(string errorMessage) : base(errorMessage) { }

        public FacadeException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }

        public virtual string Name()=> "FacadeException";
        
    }
    public class DbConnectionConfigNotRegisteredException : FacadeException
    {
        internal DbConnectionConfigNotRegisteredException(Type type): base($"{type.Name} has not been registered yet") { }

        public override string Name() => "DbConnectionConfigNotRegisteredException";

    }
    public class SQLExecutionException : FacadeException
    {
        internal SQLExecutionException(string errorMessage, DbCommandSettingsBase sqlMethod, SqlException innerException) : base(
            errorMessage, innerException)
        {
            CommandText = sqlMethod.CommandText;
        }

        public string CommandText { get; private set; }

        public override string Name()=> "SQLExecutionException";
        
    }

    public class ValidationException<DbParams> : FacadeException
    {
        internal ValidationException(IValidationResult validationResult, DbParams parameters, string errorMessage) : base(
            errorMessage)
        {
            Init(validationResult, parameters);
        }
        public IEnumerable<IValidationRuleResult> ValidationErrors { get; private set; }
        public int Count { get; private set; }
        public DbParams Parameters { get; private set; }

        public override string Name()=> "ValidationException";
        

        private void Init(IValidationResult validationResult, DbParams parameters)
        {
            ValidationErrors = validationResult.Errors;
            Count = validationResult.Errors.Count();
            Parameters = parameters;
        }
    }
}