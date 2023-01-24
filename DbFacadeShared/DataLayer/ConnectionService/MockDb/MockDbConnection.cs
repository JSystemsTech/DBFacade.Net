using DbFacade.DataLayer.Models;
using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    /// <summary>
    /// 
    /// </summary>
    internal class MockDbConnection : DbConnection
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        private string _ConnectionString { get; set; }
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public override string ConnectionString { get => _ConnectionString; set => _ConnectionString = value; }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public override string Database => "MockDb";

        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <value>
        /// The data source.
        /// </value>
        public override string DataSource => "Class Library";

        /// <summary>
        /// Gets the server version.
        /// </summary>
        /// <value>
        /// The server version.
        /// </value>
        public override string ServerVersion => "1.0.0";

        /// <summary>
        /// Gets or sets the state of the connection.
        /// </summary>
        /// <value>
        /// The state of the connection.
        /// </value>
        private ConnectionState ConnectionState { get; set; }
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public override ConnectionState State => ConnectionState;

        /// <summary>
        /// Changes the database.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        public override void ChangeDatabase(string databaseName) { }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            ConnectionState = ConnectionState.Closed;
        }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public override void Open()
        {
            ConnectionState = ConnectionState.Open;
        }
        /// <summary>
        /// Gets the database transaction.
        /// </summary>
        /// <value>
        /// The database transaction.
        /// </value>
        public MockDbTransaction DbTransaction { get; private set; }
        /// <summary>
        /// Gets the database command.
        /// </summary>
        /// <value>
        /// The database command.
        /// </value>
        public MockDbCommand DbCommand { get; private set; }
        /// <summary>
        /// Begins the database transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns></returns>
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            DbTransaction = new MockDbTransaction(this, isolationLevel);
            if (DbCommand != null)
            {
                DbCommand.Transaction = DbTransaction;
            }
            return DbTransaction;
        }
        /// <summary>
        /// Gets or sets the mock response data.
        /// </summary>
        /// <value>
        /// The mock response data.
        /// </value>
        private MockResponseData MockResponseData { get; set; }
        /// <summary>
        /// Creates the database command.
        /// </summary>
        /// <returns></returns>
        protected override DbCommand CreateDbCommand()
        {
            DbCommand = new MockDbCommand(this, MockResponseData);
            return DbCommand;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MockDbConnection"/> class.
        /// </summary>
        /// <param name="mockResponseData">The mock response data.</param>
        public MockDbConnection(MockResponseData mockResponseData)
        {
            MockResponseData = mockResponseData;
        }
    }
}
