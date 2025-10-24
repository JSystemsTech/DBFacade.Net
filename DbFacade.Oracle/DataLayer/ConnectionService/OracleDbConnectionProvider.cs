using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.Oracle.DataLayer.ConnectionService
{
    public class OracleDbConnectionProvider : IDbConnectionProvider
    {
        public DbConnectionConfig DbConnectionConfig { get; private set; }
        private Action<EndpointErrorInfo> ErrorHandler { get; set; }
        private Func<string, string> GetConnectionString { get; set; }
        private OracleCredential Credential { get; set; }
        public DbTypeCollection DbTypeCollection { get; private set; }
        public OracleDbConnectionProvider()
        {
            Credential = null;
            GetConnectionString = id => throw new Exception("No Connection String provided");
            DbTypeCollection = DbTypeCollection.Create();
            InitDbTypeCollection();
            ErrorHandler = i => { };
            DbConnectionConfig = DbConnectionConfig.Create(this);
        }
        private void InitDbTypeCollection()
        {
            DbTypeCollection.Add<OracleXmlType>(DbType.Xml, m => m.Value);
            DbTypeCollection.Add<OracleBlob>(DbType.Binary, m => m.Value);
            DbTypeCollection.Add<OracleBinary>(DbType.Binary, m => m.Value);
            DbTypeCollection.Add<OracleBFile>(DbType.Binary, m => m.Value);
            DbTypeCollection.Add<OracleDate>(DbType.DateTime, m => m.Value);
            DbTypeCollection.Add<OracleTimeStampLTZ>(DbType.DateTime, m => m.Value);
            DbTypeCollection.Add<OracleTimeStampTZ>(DbType.DateTime, m => m.Value);
            DbTypeCollection.Add<OracleDecimal>(DbType.Decimal, m => m.Value);
            DbTypeCollection.Add<OracleIntervalYM>(DbType.Int64, m => m.Value);
            DbTypeCollection.Add<OracleString>(DbType.String, m => m.Value);
            DbTypeCollection.Add<OracleClob>(DbType.String, m => m.Value);
            DbTypeCollection.Add<OracleRef>(DbType.String, m => m.Value);
            DbTypeCollection.Add<OracleTimeStamp>(DbType.Time, m => m.Value);
            DbTypeCollection.Add<OracleIntervalDS>(DbType.Time, m => m.Value);
            DbTypeCollection.Add<OracleBoolean>(DbType.Boolean, m => m.Value);
        }
        public void BindCredentials(OracleCredential credential)
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
            BindCredentials(new OracleCredential(username, securePassword));
        }

        public IDbConnection GetDbConnection(string connectionStringId)
        => Credential != null ?
            new OracleConnection(GetConnectionString(connectionStringId), Credential) :
            new OracleConnection(GetConnectionString(connectionStringId));

        public IDbDataAdapter GetDbDataAdapter()
        => new OracleDataAdapter();

        public void BindErrorHandler(Action<EndpointErrorInfo> errorHandler) => ErrorHandler = errorHandler;
        public void BindConnectionString(Func<string, string> getConnectionString) => GetConnectionString = getConnectionString;
        public void BindConnectionString(string connectionString) => GetConnectionString = id => connectionString;
        public void OnError(EndpointErrorInfo info) => ErrorHandler(info);

        public string ResolveParameterName(string name, bool useFullParameterName)
        {
            return name;
        }
        public bool CanExecuteXmlReader(IDbCommand command)
            => command is OracleCommand;
        public XmlReader ExecuteXmlReader(IDbCommand command)
            => command is OracleCommand cmd ? cmd.ExecuteXmlReader() : null;
        public DbDataReader ExecuteReader(IDbCommand command)
            => command is OracleCommand cmd ? cmd.ExecuteReader() : null;

        public object ExecuteScalar(IDbCommand command)
            => command is OracleCommand cmd ? cmd.ExecuteScalar() : null;

        public int ExecuteNonQuery(IDbCommand command)
            => command is OracleCommand cmd ? cmd.ExecuteNonQuery() : 0;

        public async Task<XmlReader> ExecuteXmlReaderAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is OracleCommand cmd ? await cmd.ExecuteXmlReaderAsync(cancellationToken) : null;

        public async Task<DbDataReader> ExecuteReaderAsync(IDbCommand command, CancellationToken cancellationToken)
        {
            if (command is OracleCommand cmd)
            {
                DbDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);
                return reader;
            }
            return null;
        }
        public async Task<object> ExecuteScalarAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is OracleCommand cmd ? await cmd.ExecuteScalarAsync(cancellationToken) : null;

        public async Task<int> ExecuteNonQueryAsync(IDbCommand command, CancellationToken cancellationToken)
            => command is OracleCommand cmd ? await cmd.ExecuteNonQueryAsync(cancellationToken) : 0;
        
        [ExcludeFromCodeCoverage]
        public IDbTransaction BeginTransaction(IDbConnection connection, IsolationLevel isolationLevel)
            => connection is OracleConnection conn ? conn.BeginTransaction(isolationLevel) : null;
#if NET6_0_OR_GREATER
        [ExcludeFromCodeCoverage]
        public async Task<IDbTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken cancellationToken, IsolationLevel isolationLevel)
            => connection is OracleConnection conn ? await conn.BeginTransactionAsync(isolationLevel, cancellationToken) : null;

#else
        [ExcludeFromCodeCoverage]
        public async Task<IDbTransaction> BeginTransactionAsync(IDbConnection connection, CancellationToken cancellationToken, IsolationLevel isolationLevel)
        {
            if (connection is OracleConnection conn)
            {
                Task<OracleTransaction> task;
                if (cancellationToken.IsCancellationRequested)
                {
                    task = Task.FromCanceled<OracleTransaction>(cancellationToken);
                }
                else
                {
                    try
                    {
                        task = new Task<OracleTransaction>(() => conn.BeginTransaction(isolationLevel));
                    }
                    catch (Exception e)
                    {
                        task = Task.FromException<OracleTransaction>(e);
                    }
                }
                return await task;
            }
            return null;
        }
#endif
    }
}
