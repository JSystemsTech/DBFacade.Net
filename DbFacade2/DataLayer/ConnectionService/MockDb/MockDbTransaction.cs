using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    [ExcludeFromCodeCoverage]
    internal class MockDbTransaction : DbTransaction
    {
        private readonly IsolationLevel IsolationLevelCore;
        private readonly MockDbConnection DbConnectionCore;
        public override IsolationLevel IsolationLevel => IsolationLevelCore;
        protected override DbConnection DbConnection => DbConnectionCore;
        public MockDbTransaction(MockDbConnection dbConnection, IsolationLevel isolationLevel)
        {
            DbConnectionCore = dbConnection;
            IsolationLevelCore = isolationLevel;

            ErrorHelper.CheckThrowMockException(DbConnectionCore.MockResponse.Settings.BeginTransactionError);
        }
        public override void Commit() { }
        public override void Rollback() {
            ErrorHelper.CheckThrowMockException(DbConnectionCore.MockResponse.Settings.TransactionRollbackError);
        }
    }
}
