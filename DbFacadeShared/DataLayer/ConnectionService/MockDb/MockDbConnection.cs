using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    internal class MockDbConnection : DbConnection
    {
        private string ConnectionStringRoot { get; set; }
        public override string ConnectionString { get => ConnectionStringRoot; set => ConnectionStringRoot = value; }
        public override string Database => "MockDb";
        public override string DataSource => "Class Library";
        public override string ServerVersion => "1.0.0";
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
        }
        public MockDbTransaction DbTransaction { get; private set; }
        public MockDbCommand DbCommand { get; private set; }
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            DbTransaction = new MockDbTransaction(this, isolationLevel);
            if (DbCommand != null)
            {
                DbCommand.Transaction = DbTransaction;
            }
            return DbTransaction;
        }
        private readonly MockResponseData MockResponseData;

        protected override DbCommand CreateDbCommand()
        {
            DbCommand = new MockDbCommand(this, MockResponseData);
            return DbCommand;
        }
        internal MockDbConnection(DbCommandMethod dbCommandMethod)
        {
            MockResponseData = dbCommandMethod.MockData is MockResponseData data && data != null ? data : MockResponseData.Empty;
        }
    }
}
