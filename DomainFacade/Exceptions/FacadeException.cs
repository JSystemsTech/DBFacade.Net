using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.DataLayer.Models.Validators.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainFacade.Exceptions
{
    public class FacadeException: Exception
    {
        public virtual string Name() { return "FacadeException"; }
        public FacadeException():base() { }
        public FacadeException(string errorMessage) : base(errorMessage) { }
        public FacadeException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    }
    public class MissingStoredProcedureException : FacadeException
    {
        public override string Name() { return "MissingStoredProcedureException"; }
        public MissingStoredProcedureException() : base() { }
        public MissingStoredProcedureException(string errorMessage) : base(errorMessage) { }
        public MissingStoredProcedureException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    }
    public class DataModelConstructionException : FacadeException
    {
        public override string Name() { return "DataModelConstructionException"; }
        public DataModelConstructionException() : base() { }
        public DataModelConstructionException(string errorMessage) : base(errorMessage) { }
        public DataModelConstructionException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    }
    public class SQLExecutionException : FacadeException
    {
        public override string Name() { return "SQLExecutionException"; }
        public SQLExecutionException() : base() { }
        public SQLExecutionException(string errorMessage) : base(errorMessage) { }
        public SQLExecutionException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    }
    public class ValidationException<DbParams> : FacadeException where DbParams: IDbParamsModel
    {
        public override string Name() { return "ValidationException"; }
        public IEnumerable<ValidationRuleResult> ValidationErrors { get; private set; }
        public int Count { get; private set; }
        public DbParams Parameters { get; private set; }
        public ValidationException(IValidationResult validationResult, DbParams parameters) : base() {
            Init(validationResult, parameters);
        }
        public ValidationException(IValidationResult validationResult, DbParams parameters, string errorMessage) : base(errorMessage) {
            Init(validationResult, parameters);
        }
        public ValidationException(IValidationResult validationResult, DbParams parameters, string errorMessage, Exception innerException) : base(errorMessage, innerException) {
            Init(validationResult, parameters);
        }
        private void Init(IValidationResult validationResult, DbParams parameters)
        {
            ValidationErrors = validationResult.Errors();
            Count = validationResult.Errors().Count();
            Parameters = parameters;
        }
    }
}
