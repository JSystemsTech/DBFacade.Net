using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    internal class MockDbTransaction : DbTransaction
    {
        private IsolationLevel _IsolationLevel { get; set; }
        private MockDbConnection _DbConnection { get; set; }
        public override IsolationLevel IsolationLevel => _IsolationLevel;

        protected override DbConnection DbConnection => _DbConnection;
        public MockDbTransaction(MockDbConnection dbConnection, IsolationLevel isolationLevel)
        {
            _DbConnection = dbConnection;
            _IsolationLevel = isolationLevel;
        }
        public override void Commit() { }

        public override void Rollback() { }
    }
}
