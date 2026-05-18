using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.PostgreSQL
    .DataLayer.ConnectionService
{
    public class PostgreSQLDbConnectionProvider : IDbConnectionProvider
    {
        public DbConnectionConfig DbConnectionConfig { get; private set; }
        private Action<EndpointErrorInfo> ErrorHandler { get; set; }
        private Func<string, string> GetConnectionString { get; set; }
        public DbTypeCollection DbTypeCollection { get; private set; }
        public PostgreSQLDbConnectionProvider()
        {
            GetConnectionString = id => throw new Exception("No Connection String provided");
            DbTypeCollection = DbTypeCollection.Create();
            ErrorHandler = i => { };
            DbConnectionConfig = DbConnectionConfig.Create(this);
        }

        public IDbConnection GetDbConnection(string connectionStringId)
        => new NpgsqlConnection(GetConnectionString(connectionStringId));

        public IDbDataAdapter GetDbDataAdapter()
        => new NpgsqlDataAdapter();

        public void BindErrorHandler(Action<EndpointErrorInfo> errorHandler) => ErrorHandler = errorHandler;
        public void BindConnectionString(Func<string, string> getConnectionString) => GetConnectionString = getConnectionString;
        public void BindConnectionString(string connectionString) => GetConnectionString = id => connectionString;
        public void OnError(EndpointErrorInfo info) => ErrorHandler(info);

        public string ResolveParameterName(string name, bool useFullParameterName)
        {

            bool startsWithPrefix = name.StartsWith(LeadChar1) || name.StartsWith(LeadChar2) || name.StartsWith(LeadChar3);
            return useFullParameterName && !startsWithPrefix ? $"{LeadChar1}{name}" :
                !useFullParameterName && startsWithPrefix ? name.Substring(1) :
                name;
        }
#if NET8_0_OR_GREATER
        private static readonly char LeadChar1 = '@';
        private static readonly char LeadChar2 = ':';
        private static readonly char LeadChar3 = '$';

#else
       private static readonly string LeadChar1 = "@";
       private static readonly string LeadChar2 = ":";
       private static readonly string LeadChar3 = "$";
#endif
        public bool CanExecuteXmlReader(IDbCommand command)
            => false;
        public XmlReader ExecuteXmlReader(IDbCommand command)
            => null;
        public DbDataReader ExecuteReader(IDbCommand command)
            => command is NpgsqlCommand cmd ? cmd.ExecuteReader() : null;

        public object ExecuteScalar(IDbCommand command)
            => command is NpgsqlCommand cmd ? cmd.ExecuteScalar() : null;

        public int ExecuteNonQuery(IDbCommand command)
            => command is NpgsqlCommand cmd ? cmd.ExecuteNonQuery() : 0;

        public async Task<XmlReader> ExecuteXmlReaderAsync(IDbCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return null;
        }
        public async Task<DbDataReader> ExecuteReaderAsync(IDbCommand command, CancellationToken cancellationToken)
        {
            if (command is NpgsqlCommand cmd)
            {
                DbDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);
                return reader;
            }
            return null;
        }
        public async Task<object> ExecuteScalarAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is NpgsqlCommand cmd ? await cmd.ExecuteScalarAsync(cancellationToken) : null;

        public async Task<int> ExecuteNonQueryAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is NpgsqlCommand cmd ? await cmd.ExecuteNonQueryAsync(cancellationToken) : 0;

        [ExcludeFromCodeCoverage]
        public IDbTransaction BeginTransaction(IDbConnection connection, IsolationLevel isolationLevel)
          => connection is NpgsqlConnection conn ? conn.BeginTransaction(isolationLevel) : null;

#if NET8_0_OR_GREATER
        [ExcludeFromCodeCoverage]
        public async Task<IDbTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken cancellationToken, IsolationLevel isolationLevel)
            => connection is NpgsqlConnection conn ? await conn.BeginTransactionAsync(isolationLevel, cancellationToken) : null;

#else
        [ExcludeFromCodeCoverage]
        public async Task<IDbTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken cancellationToken, IsolationLevel isolationLevel)
        {
            if(connection is NpgsqlConnection conn)
            {
                Task<NpgsqlTransaction> task;
                if (cancellationToken.IsCancellationRequested)
                {
                    task = Task.FromCanceled<NpgsqlTransaction>(cancellationToken);
                }
                else
                {
                    try
                    {
                        task = new Task<NpgsqlTransaction>(() => conn.BeginTransaction(isolationLevel));
                    }
                    catch (Exception e)
                    {
                        task = Task.FromException<NpgsqlTransaction>(e);
                    }
                }
                return await task;
            }
            return null;
        }
#endif
    }


}
