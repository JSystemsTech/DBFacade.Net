using DbFacade.DataLayer.Models;
using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.DataLayer.ConnectionService
{
    public enum DbExecutionExceptionType
    {
        FacadeException,
        DbExecutionError,
        ValidationError,
        OperationCanceledException,
        Error
    }
    public interface IDbConnectionProvider
    {
        DbConnectionConfig DbConnectionConfig { get; }
        IDbDataAdapter GetDbDataAdapter();
        IDbConnection GetDbConnection(string conectionStringId);
        void OnError(EndpointErrorInfo info);
        string ResolveParameterName(string name, bool useFullParameterName);
        DbTypeCollection DbTypeCollection { get; }
        void BindErrorHandler(Action<EndpointErrorInfo> errorHandler);
        void BindConnectionString(Func<string, string> getConnectionString);
        void BindConnectionString(string connectionString);
        bool CanExecuteXmlReader(IDbCommand command);
        XmlReader ExecuteXmlReader(IDbCommand command);
        DbDataReader ExecuteReader(IDbCommand command);
        object ExecuteScalar(IDbCommand command);
        int ExecuteNonQuery(IDbCommand command);
        Task<XmlReader> ExecuteXmlReaderAsync(IDbCommand command, CancellationToken cancellationToken);
        Task<DbDataReader> ExecuteReaderAsync(IDbCommand command, CancellationToken cancellationToken);
        Task<object> ExecuteScalarAsync(IDbCommand command, CancellationToken cancellationToken);
        Task<int> ExecuteNonQueryAsync(IDbCommand command, CancellationToken cancellationToken);
        IDbTransaction BeginTransaction(IDbConnection connection, IsolationLevel isolationLevel);
        Task<IDbTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken cancellationToken, IsolationLevel isolationLevel);

    }
    
}
