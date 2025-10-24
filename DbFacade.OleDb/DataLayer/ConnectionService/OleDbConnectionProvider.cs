using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.OleDb.DataLayer.ConnectionService
{
    public class OleDbConnectionProvider : IDbConnectionProvider
    {
        public DbConnectionConfig DbConnectionConfig { get; private set; }
        private Action<EndpointErrorInfo> ErrorHandler { get; set; }
        private Func<string, string> GetConnectionString { get; set; }
        public DbTypeCollection DbTypeCollection { get; private set; }
        public OleDbConnectionProvider()
        {
            GetConnectionString = id => throw new Exception("No Connection String provided");
            DbTypeCollection = DbTypeCollection.Create();
            ErrorHandler = i => { };
            DbConnectionConfig = DbConnectionConfig.Create(this);
        }

#if (NET6_0_OR_GREATER)
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public IDbConnection GetDbConnection(string connectionStringId)
        => new OleDbConnection(GetConnectionString(connectionStringId));
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public IDbDataAdapter GetDbDataAdapter()
        => new OleDbDataAdapter();
#else
        public IDbConnection GetDbConnection(string connectionStringId)
        => new OleDbConnection(GetConnectionString(connectionStringId));

        public IDbDataAdapter GetDbDataAdapter()
        => new OleDbDataAdapter();
#endif

        public void BindErrorHandler(Action<EndpointErrorInfo> errorHandler) => ErrorHandler = errorHandler;
        public void BindConnectionString(Func<string, string> getConnectionString) => GetConnectionString = getConnectionString;
        public void BindConnectionString(string connectionString) => GetConnectionString = id => connectionString;
        public void OnError(EndpointErrorInfo info) => ErrorHandler(info);

        public string ResolveParameterName(string name, bool useFullParameterName)
        {
            bool startsWithPrefix = name.StartsWith(LeadChar);
            return useFullParameterName && !startsWithPrefix ? $"{LeadChar}{name}" :
                !useFullParameterName && startsWithPrefix ? name.Substring(1) :
                name;
        }
#if NET8_0_OR_GREATER
        private static readonly char LeadChar = '@';

#else
        private static readonly string LeadChar = "@";
#endif
        public bool CanExecuteXmlReader(IDbCommand command)
            => false;
        public XmlReader ExecuteXmlReader(IDbCommand command)
            => null;

#if (NET6_0_OR_GREATER)
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public DbDataReader ExecuteReader(IDbCommand command)
            => command is OleDbCommand cmd ? cmd.ExecuteReader() : null;

        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public object ExecuteScalar(IDbCommand command)
            => command is OleDbCommand cmd ? cmd.ExecuteScalar() : null;

        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public int ExecuteNonQuery(IDbCommand command)
            => command is OleDbCommand cmd ? cmd.ExecuteNonQuery() : 0;
#else
        public DbDataReader ExecuteReader(IDbCommand command)
            => command is OleDbCommand cmd ? cmd.ExecuteReader() : null;

        public object ExecuteScalar(IDbCommand command)
            => command is OleDbCommand cmd ? cmd.ExecuteScalar() : null;

        public int ExecuteNonQuery(IDbCommand command)
            => command is OleDbCommand cmd ? cmd.ExecuteNonQuery() : 0;
#endif

        public async Task<XmlReader> ExecuteXmlReaderAsync(IDbCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;
        }
        public async Task<DbDataReader> ExecuteReaderAsync(IDbCommand command, CancellationToken cancellationToken)
        {
            if (command is OleDbCommand cmd)
            {
                DbDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);
                return reader;
            }
            return null;
        }
        public async Task<object> ExecuteScalarAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is OleDbCommand cmd ? await cmd.ExecuteScalarAsync(cancellationToken) : null;

        public async Task<int> ExecuteNonQueryAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is OleDbCommand cmd ? await cmd.ExecuteNonQueryAsync(cancellationToken) : 0;



#if NET6_0_OR_GREATER
        [ExcludeFromCodeCoverage]
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public IDbTransaction BeginTransaction(IDbConnection connection, IsolationLevel isolationLevel)
            => connection is OleDbConnection conn ? conn.BeginTransaction(isolationLevel) : null;
        [ExcludeFromCodeCoverage]
        public async Task<IDbTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken cancellationToken, IsolationLevel isolationLevel)
            => connection is OleDbConnection conn ? await conn.BeginTransactionAsync(isolationLevel, cancellationToken) : null;

#else
        [ExcludeFromCodeCoverage]
        public IDbTransaction BeginTransaction(IDbConnection connection, IsolationLevel isolationLevel)
            => connection is OleDbConnection conn ? conn.BeginTransaction(isolationLevel) : null;

        [ExcludeFromCodeCoverage]
        public async Task<IDbTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken cancellationToken, IsolationLevel isolationLevel)
        {
            if(connection is OleDbConnection conn)
            {
                Task<OleDbTransaction> task;
                if (cancellationToken.IsCancellationRequested)
                {
                    task = Task.FromCanceled<OleDbTransaction>(cancellationToken);
                }
                else
                {
                    try
                    {
                        task = new Task<OleDbTransaction>(() => conn.BeginTransaction(isolationLevel));
                    }
                    catch (Exception e)
                    {
                        task = Task.FromException<OleDbTransaction>(e);
                    }
                }
                return await task;
            }
            return null;
        }
#endif
    }
}
