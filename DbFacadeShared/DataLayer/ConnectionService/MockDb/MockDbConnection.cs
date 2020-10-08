using DbFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    internal class MockDbConnection: DbConnection
    {
        private string _ConnectionString { get; set; }
        public override string ConnectionString { get => _ConnectionString; set => _ConnectionString = value; }

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
            if(DbCommand != null)
            {
                DbCommand.Transaction = DbTransaction;
            }
            return DbTransaction;
        }
        private IInternalDbParamsModel ParamsModel { get; set; }
        protected override DbCommand CreateDbCommand() {
            DbCommand = new MockDbCommand(this, ParamsModel);
            return DbCommand;
        }
        public MockDbConnection (IInternalDbParamsModel paramsModel)
        {
            ParamsModel = paramsModel;
        }
    }
}
