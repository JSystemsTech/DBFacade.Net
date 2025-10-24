using DbFacade.DataLayer.Models;

#if NET8_0_OR_GREATER
using Microsoft.Data.SqlClient;
#else
using System.Data.SqlClient;
#endif
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.DataLayer.ConnectionService
{
    public class SqlDbConnectionProvider : IDbConnectionProvider
    {
        public DbConnectionConfig DbConnectionConfig { get; private set; }
        private Action<EndpointErrorInfo> ErrorHandler { get; set; }
        private Func<string, string> GetConnectionString { get; set; }
        public DbTypeCollection DbTypeCollection { get; private set; }
        private SqlCredential Credential { get; set; }
        public SqlDbConnectionProvider()
        {            
            DbTypeCollection = DbTypeCollection.Create();
            InitDbTypeCollection();
            DbConnectionConfig = DbConnectionConfig.Create(this);
        }
        private void InitDbTypeCollection()
        {
            DbTypeCollection.Add<SqlXml>(DbType.Xml);
            DbTypeCollection.Add<SqlBytes>(DbType.Binary, m => m.Value);
            DbTypeCollection.Add<SqlChars>(DbType.String, m => new string(m.Value));
            DbTypeCollection.Add<SqlDateTime>(DbType.DateTime, m => m.Value);
            DbTypeCollection.Add<SqlDecimal>(DbType.Decimal, m => m.Value);
            DbTypeCollection.Add<SqlDouble>(DbType.Double, m => m.Value);
            DbTypeCollection.Add<SqlGuid>(DbType.Guid, m => m.Value);
            DbTypeCollection.Add<SqlInt16>(DbType.Int16, m => m.Value);
            DbTypeCollection.Add<SqlInt32>(DbType.Int32, m => m.Value);
            DbTypeCollection.Add<SqlInt64>(DbType.Int64, m => m.Value);
            DbTypeCollection.Add<SqlMoney>(DbType.Currency, m => m.Value);
            DbTypeCollection.Add<SqlSingle>(DbType.Single, m => m.Value);
            DbTypeCollection.Add<SqlString>(DbType.String, m => m.Value);
        }
        public void BindCredentials(SqlCredential credential)
        {
            Credential = credential;
        }
        public void BindCredentials(string username, string password)
        {
            var securePassword = new System.Security.SecureString();
            foreach (char character in password)
            {
                securePassword.AppendChar(character);
            }
            securePassword.MakeReadOnly();
            BindCredentials(new SqlCredential(username, securePassword));
        }
        public IDbConnection GetDbConnection(string connectionStringId)
        {
            if (GetConnectionString == null)
            {
                throw new Exception("No Connection String provided");
            }
            string connectionString = GetConnectionString(connectionStringId);
            return Credential != null ?
            new SqlConnection(connectionString, Credential) :
            new SqlConnection(connectionString);
        }

        [ExcludeFromCodeCoverage]
        public IDbDataAdapter GetDbDataAdapter()
        => new SqlDataAdapter();
        public void BindConnectionString(Func<string, string> getConnectionString) => GetConnectionString = getConnectionString;
        public void BindConnectionString(string connectionString) => GetConnectionString = id => connectionString;
        public void BindErrorHandler(Action<EndpointErrorInfo> errorHandler) => ErrorHandler = errorHandler;
        public void OnError(EndpointErrorInfo info) {
            if (ErrorHandler != null)
            {
                ErrorHandler(info);
            }
        }

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
        [ExcludeFromCodeCoverage]
        public bool CanExecuteXmlReader(IDbCommand command)
            => command is SqlCommand; 
        [ExcludeFromCodeCoverage]
        public XmlReader ExecuteXmlReader(IDbCommand command)
            => command is SqlCommand cmd ? cmd.ExecuteXmlReader() : null;
        [ExcludeFromCodeCoverage]
        public DbDataReader ExecuteReader(IDbCommand command)
            => command is SqlCommand cmd ? cmd.ExecuteReader() : null;
        [ExcludeFromCodeCoverage]
        public object ExecuteScalar(IDbCommand command)
            => command is SqlCommand cmd ? cmd.ExecuteScalar() : null;
        [ExcludeFromCodeCoverage]
        public int ExecuteNonQuery(IDbCommand command)
            => command is SqlCommand cmd ? cmd.ExecuteNonQuery() : 0;

        [ExcludeFromCodeCoverage]
        public async Task<XmlReader> ExecuteXmlReaderAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is SqlCommand cmd ? await cmd.ExecuteXmlReaderAsync(cancellationToken) : null;
        [ExcludeFromCodeCoverage]
        public async Task<DbDataReader> ExecuteReaderAsync(IDbCommand command, CancellationToken cancellationToken)
        {
            if (command is SqlCommand cmd)
            {
                DbDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);
                return reader;
            }
            return null;
        }
        [ExcludeFromCodeCoverage]
        public async Task<object> ExecuteScalarAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is SqlCommand cmd ? await cmd.ExecuteScalarAsync(cancellationToken) : null;
        [ExcludeFromCodeCoverage]
        public async Task<int> ExecuteNonQueryAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is SqlCommand cmd ? await cmd.ExecuteNonQueryAsync(cancellationToken) : 0;

        [ExcludeFromCodeCoverage]
        public IDbTransaction BeginTransaction(IDbConnection connection, IsolationLevel isolationLevel)
            => connection is SqlConnection conn ? conn.BeginTransaction(isolationLevel) : null;

#if NET8_0_OR_GREATER
        [ExcludeFromCodeCoverage]
        public async Task<IDbTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken cancellationToken, IsolationLevel isolationLevel)
            => connection is SqlConnection conn ? await conn.BeginTransactionAsync(isolationLevel, cancellationToken) : null;

#else
        [ExcludeFromCodeCoverage]
        public async Task<IDbTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken cancellationToken, IsolationLevel isolationLevel)
        {
            if(connection is SqlConnection conn)
            {
                Task<SqlTransaction> task;
                if (cancellationToken.IsCancellationRequested)
                {
                    task = Task.FromCanceled<SqlTransaction>(cancellationToken);
                }
                else
                {
                    try
                    {
                        task = new Task<SqlTransaction>(() => conn.BeginTransaction(isolationLevel));
                    }
                    catch (Exception e)
                    {
                        task = Task.FromException<SqlTransaction>(e);
                    }
                }
                return await task;
            }
            return null;
        }
#endif

    }
}
