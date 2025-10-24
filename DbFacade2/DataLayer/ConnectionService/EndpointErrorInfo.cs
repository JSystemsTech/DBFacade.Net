using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using System;

namespace DbFacade.DataLayer.ConnectionService
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class EndpointErrorInfo
    {
        /// <summary>Gets the error.</summary>
        /// <value>The error.</value>
        public Exception Error { get; private set; }
        /// <summary>Gets the error message.</summary>
        /// <value>The error message.</value>
        public string ErrorMessage => Error.Message;
        /// <summary>Gets the error details.</summary>
        /// <value>The error details.</value>
        public string ErrorDetails { get; private set; } 
        /// <summary>Gets the endpoint settings.</summary>
        /// <value>The endpoint settings.</value>
        public EndpointSettings EndpointSettings { get; private set; }
        /// <summary>Gets the parameters.</summary>
        /// <value>The parameters.</value>
        public object Parameters { get; private set; }
        /// <summary>The error data</summary>
        public readonly DataCollection ErrorData;
        /// <summary>Gets the type of the exception.</summary>
        /// <value>The type of the exception.</value>
        public DbExecutionExceptionType ExceptionType { get; private set; }
        internal EndpointErrorInfo(Exception error, EndpointSettings endpointSettings, object parameters)
        {
            Error = error;
            EndpointSettings = endpointSettings;
            Parameters = parameters;
            ExceptionType = error is ValidationException ? DbExecutionExceptionType.ValidationError :
                error is DbExecutionException ? DbExecutionExceptionType.DbExecutionError :
                error is FacadeException ? DbExecutionExceptionType.FacadeException :
                error is OperationCanceledException ? DbExecutionExceptionType.OperationCanceledException :
                DbExecutionExceptionType.Error;
            ErrorData = new DataCollection();
            ErrorDetails = Error is FacadeException ex ? ex.ErrorDetails :
            Error.InnerException is Exception innerException ? innerException.Message :
            "";
        }
    }
}
