using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    [ExcludeFromCodeCoverage]
    internal class MockDbConnection : DbConnection
    {
        private string ConnectionStringRoot { get; set; }
        public override string ConnectionString { get => ConnectionStringRoot; set => ConnectionStringRoot = value; }
        public override string Database => "MockDb";
        public override string DataSource => "Class Library";
        public override string ServerVersion => "2.0.0";
        private ConnectionState ConnectionState { get; set; }
        public override ConnectionState State => ConnectionState;
        public override void ChangeDatabase(string databaseName) { }
        public override void Close()
        {
            ConnectionState = ConnectionState.Closed;
        }
        public override void Open()
        {
            ConnectionState = ConnectionState.Open;
            ErrorHelper.CheckThrowMockException(MockResponse.Settings.ConnectionOpenError);
        }
        public MockDbTransaction DbTransaction { get; private set; }
        
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        => MockResponse.Settings.UseNullTransaction ? null : new MockDbTransaction(this, isolationLevel);
#if NET472_OR_GREATER
        public async Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
            => await BeginDbTransactionAsync(isolationLevel, cancellationToken);
        private Task<DbTransaction> BeginDbTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<DbTransaction>(cancellationToken);
            }
            try
            {
                return new Task<DbTransaction>(() => BeginDbTransaction(isolationLevel));
            }
            catch (Exception e)
            {
                return Task.FromException<DbTransaction>(e);
            }
        }
#endif
        internal MockResponse MockResponse { get; private set; }

        protected override DbCommand CreateDbCommand()
        {
            return new MockDbCommand(this, MockResponse);
        }
        internal DbCommand CreateDbCommand(DbCommandMethod dbCommandMethod)
        {
            MockResponse = dbCommandMethod.MockResponse;
            return CreateDbCommand();
        }
        private MockDbConnection(DbCommandMethod dbCommandMethod)
        {
            MockResponse = dbCommandMethod.MockResponse;
            ErrorHelper.CheckThrowMockException(MockResponse.Settings.ConnectionCreationError);
        }
        internal static MockDbConnection Create(DbCommandMethod dbCommandMethod)
        => dbCommandMethod.MockResponse.Settings.UseNullConnection ? null : new MockDbConnection(dbCommandMethod);
    }
}
