using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.Extensions;
using System;
using System.Data.Common;
using System.Linq;

namespace DbFacade.Exceptions
{
    internal class ErrorHelper
    {
        internal static void ThrowDbExecutionException(string message, Exception ex)
        {
            throw new DbExecutionException(message, ex);
        }
        internal static void ThrowFacadeException(string message, Exception ex)
        {
            throw new FacadeException(message, ex);
        }
        internal static void ThrowFacadeException(string message)
        {
            throw new FacadeException(message);
        }
        internal static void CheckThrowMockException(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                throw new MockDbException(message);
            }
        }
        
        internal static void ThrowInvalidParametersError(object data, params Type[] allowedTypes)
        => ThrowFacadeException($"Unable to add parameters: expected model of type(s) {allowedTypes.TypeNames()} but got {data.TypeName()}");
        internal static void ThrowOnRollbackTransactionError(Exception ex)
        => ThrowDbExecutionException("Unable to rollback transaction", ex);
        
        internal static void ThrowOnCreateTransactionError(Exception ex)
        => ThrowDbExecutionException("Unable to create transaction", ex);
        
        internal static void ThrowInvalidTransactionError()
        => ThrowFacadeException("Invalid Transaction Definition");
        
        internal static void ThrowOnExecuteQueryError(Exception ex)
        => ThrowDbExecutionException(ex is DbException ? "Unable to execute query" : "Unknown Error while attempting to execute query", ex);

        internal static void ThrowOnExecuteNonQueryError(Exception ex)
        => ThrowDbExecutionException(ex is DbException ? "Unable to execute non query" : "Unknown Error while attempting to execute non query", ex);

        internal static void ThrowOnExecuteScalarError(Exception ex)
        => ThrowDbExecutionException(ex is DbException ? "Unable to execute scalar" : "Unknown Error while attempting to execute scalar", ex);
        
        internal static void ThrowOnExecuteXmlError(Exception ex)
        => ThrowDbExecutionException(ex is DbException ? "Unable to execute xml query" : "Unknown Error while attempting to execute xml query", ex);
        
        internal static void ThrowInvalidConnectionError()
        => ThrowFacadeException("Invalid Connection Definition");
        
        internal static void ThrowUnableToOpenConnectionError(Exception ex)
        => ThrowDbExecutionException("Unable to open connection", ex);
        internal static void ThrowUnableToCreateConnectionError(Exception ex)
        => ThrowFacadeException("Unable to create connection", ex);
    }
}
