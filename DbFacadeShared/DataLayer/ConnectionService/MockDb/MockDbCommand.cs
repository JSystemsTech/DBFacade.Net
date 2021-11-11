using DbFacade.DataLayer.Models;
using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    /// <summary>
    /// 
    /// </summary>
    internal class MockDbCommand : DbCommand
    {
        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>
        /// The command text.
        /// </value>
        private string _CommandText { get; set; }
        /// <summary>
        /// Gets or sets the command timeout.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        private int _CommandTimeout { get; set; }
        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>
        /// The type of the command.
        /// </value>
        private CommandType _CommandType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [design time visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [design time visible]; otherwise, <c>false</c>.
        /// </value>
        private bool _DesignTimeVisible { get; set; }
        /// <summary>
        /// Gets or sets the updated row source.
        /// </summary>
        /// <value>
        /// The updated row source.
        /// </value>
        private UpdateRowSource _UpdatedRowSource { get; set; }
        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        private DbConnection _DbConnection { get; set; }
        /// <summary>
        /// Gets or sets the database parameter collection.
        /// </summary>
        /// <value>
        /// The database parameter collection.
        /// </value>
        private DbParameterCollection _DbParameterCollection { get; set; }
        /// <summary>
        /// Gets or sets the database transaction.
        /// </summary>
        /// <value>
        /// The database transaction.
        /// </value>
        private DbTransaction _DbTransaction { get; set; }
        /// <summary>
        /// Gets or sets the mock response data.
        /// </summary>
        /// <value>
        /// The mock response data.
        /// </value>
        private MockResponseData MockResponseData { get; set; }

        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        /// <value>
        /// The command text.
        /// </value>
        public override string CommandText { get => _CommandText; set => _CommandText = value; }
        /// <summary>
        /// Gets or sets the command timeout.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        public override int CommandTimeout { get => _CommandTimeout; set => _CommandTimeout = value; }
        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>
        /// The type of the command.
        /// </value>
        public override CommandType CommandType { get => _CommandType; set => _CommandType = value; }
        /// <summary>
        /// Gets or sets a value indicating whether [design time visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [design time visible]; otherwise, <c>false</c>.
        /// </value>
        public override bool DesignTimeVisible { get => _DesignTimeVisible; set => _DesignTimeVisible = value; }
        /// <summary>
        /// Gets or sets the updated row source.
        /// </summary>
        /// <value>
        /// The updated row source.
        /// </value>
        public override UpdateRowSource UpdatedRowSource { get => _UpdatedRowSource; set => _UpdatedRowSource = value; }
        /// <summary>
        /// Gets or sets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        protected override DbConnection DbConnection { get => _DbConnection; set => _DbConnection = value; }

        /// <summary>
        /// Gets the database parameter collection.
        /// </summary>
        /// <value>
        /// The database parameter collection.
        /// </value>
        protected override DbParameterCollection DbParameterCollection => _DbParameterCollection;

        /// <summary>
        /// Gets or sets the database transaction.
        /// </summary>
        /// <value>
        /// The database transaction.
        /// </value>
        protected override DbTransaction DbTransaction { get => _DbTransaction; set => _DbTransaction = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockDbCommand"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="mockResponseData">The mock response data.</param>
        public MockDbCommand(MockDbConnection dbConnection, MockResponseData mockResponseData)
        {
            MockResponseData = mockResponseData;
            _DbConnection = dbConnection;
            _DbParameterCollection = new MockDbParameterCollection();
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public override void Cancel() { }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <returns></returns>
        public override int ExecuteNonQuery() { SetResponse(); return 0; }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <returns></returns>
        public override object ExecuteScalar() { SetResponse(); return null; }

        /// <summary>
        /// Prepares this instance.
        /// </summary>
        public override void Prepare() { }

        /// <summary>
        /// Creates the database parameter.
        /// </summary>
        /// <returns></returns>
        protected override DbParameter CreateDbParameter()=> new MockDbParameter();
        /// <summary>
        /// Sets the response.
        /// </summary>
        private void SetResponse()
        {
            if(_DbParameterCollection is MockDbParameterCollection mockDbParameterCollection)
            {
                if(mockDbParameterCollection.GetReturnValueParam() is MockDbParameter param)
                {
                    param.Value = MockResponseData.ReturnValue;
                }
                if(MockResponseData.OutputValues != null)
                {                    
                    foreach (MockDbParameter outputParam in mockDbParameterCollection.GetOutputParams())
                    {
                        object value;
                        if (MockResponseData.OutputValues.TryGetValue(outputParam.ParameterNameAsKey(), out value))
                        {
                            outputParam.Value = value;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Executes the database data reader.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        /// <returns></returns>
        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            SetResponse();
            return MockResponseData.ResponseData;
        }
    }
}
