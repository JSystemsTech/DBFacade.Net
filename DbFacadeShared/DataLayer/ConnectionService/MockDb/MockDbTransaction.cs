using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    /// <summary>
    /// 
    /// </summary>
    internal class MockDbTransaction : DbTransaction
    {
        /// <summary>
        /// Gets or sets the isolation level.
        /// </summary>
        /// <value>
        /// The isolation level.
        /// </value>
        private IsolationLevel _IsolationLevel { get; set; }
        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        private MockDbConnection _DbConnection { get; set; }
        /// <summary>
        /// Gets the isolation level.
        /// </summary>
        /// <value>
        /// The isolation level.
        /// </value>
        public override IsolationLevel IsolationLevel => _IsolationLevel;

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        protected override DbConnection DbConnection => _DbConnection;
        /// <summary>
        /// Initializes a new instance of the <see cref="MockDbTransaction"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="isolationLevel">The isolation level.</param>
        public MockDbTransaction(MockDbConnection dbConnection, IsolationLevel isolationLevel)
        {
            _DbConnection = dbConnection;
            _IsolationLevel = isolationLevel;
        }
        /// <summary>
        /// Commits this instance.
        /// </summary>
        public override void Commit() { }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public override void Rollback() { }
    }
}
