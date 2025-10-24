using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Data;

namespace DbFacade.DataLayer.ConnectionService
{
    public enum DbExecutionExceptionType
    {
        FacadeException,
        DbExecutionError,
        ValidationError,
        Error
    }
    public interface IDbConnectionProvider
    {
        IDbDataAdapter GetDbDataAdapter();
        IDbConnection GetDbConnection(string conectionStringId);
        void OnError(Exception ex, EndpointSettings dbCommandSettings, object parameters, DbExecutionExceptionType type);

    }
    
}
