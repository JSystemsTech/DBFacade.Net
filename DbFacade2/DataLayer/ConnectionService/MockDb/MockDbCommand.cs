using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Xml;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// 
    /// </summary>
    internal class MockDbCommand : DbCommand
    {
        
        private string CommandTextCore { get; set; }
     
        private int CommandTimeoutCore { get; set; }
       
        private CommandType CommandTypeCore { get; set; }
       
        private bool DesignTimeVisibleCore { get; set; }
      
        private UpdateRowSource UpdatedRowSourceCore { get; set; }
       
        private DbConnection DbConnectionCore { get; set; }
        
        private DbParameterCollection DbParameterCollectionCore { get; set; }
       
        private DbTransaction DbTransactionCore { get; set; }
        
        private MockResponse MockResponse { get; set; }

        
        public override string CommandText { get => CommandTextCore; set => CommandTextCore = value; }
        
        public override int CommandTimeout { get => CommandTimeoutCore; set => CommandTimeoutCore = value; }
       
        public override CommandType CommandType { get => CommandTypeCore; set => CommandTypeCore = value; }
        
        public override bool DesignTimeVisible { get => DesignTimeVisibleCore; set => DesignTimeVisibleCore = value; }
        
        public override UpdateRowSource UpdatedRowSource { get => UpdatedRowSourceCore; set => UpdatedRowSourceCore = value; }
        
        protected override DbConnection DbConnection { get => DbConnectionCore; set => DbConnectionCore = value; }

        
        protected override DbParameterCollection DbParameterCollection => DbParameterCollectionCore;

        
        protected override DbTransaction DbTransaction { get => DbTransactionCore; set => DbTransactionCore = value; }

        
        public MockDbCommand(MockDbConnection dbConnection, MockResponse mockResponse)
        {
            MockResponse = mockResponse;
            DbConnectionCore = dbConnection;
            DbParameterCollectionCore = new MockDbParameterCollection();
        }

        public override void Cancel() { }

        public override int ExecuteNonQuery() {
            ErrorHelper.CheckThrowMockException(MockResponse.Settings.ExecuteNonQueryError);
            SetResponse();
            return MockResponse.ReturnValue; 
        }

        public override object ExecuteScalar()
        {
            ErrorHelper.CheckThrowMockException(MockResponse.Settings.ExecuteScalarError);
            return MockResponse.ScalarReturnValue;
        }


        public override void Prepare() { }
        private void SetResponse()
        {
            if (DbParameterCollectionCore is MockDbParameterCollection mockDbParameterCollection)
            {
                if (mockDbParameterCollection.GetReturnValueParam() is MockDbParameter param)
                {
                    param.Value = MockResponse.ReturnValue;
                }
                if (MockResponse.OutputValues.Keys.Length > 0)
                {
                    foreach (MockDbParameter outputParam in mockDbParameterCollection.GetOutputParams())
                    {
                        outputParam.Value = MockResponse.OutputValues[outputParam.ParameterNameAsKey()];
                    }
                }
            }
        }

        protected override DbParameter CreateDbParameter() => new MockDbParameter();
   
        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            SetResponse();
            return MockResponse.GetResponseData();
        }
        public XmlReader ExecuteXmlReader()
        {
            ErrorHelper.CheckThrowMockException(MockResponse.Settings.ExecuteXmlError);
            SetResponse();
            return MockResponse.GetResponseDataXml();
        }
        public async Task<XmlReader> ExecuteXmlReaderAsync()
        {
            var reader = ExecuteXmlReader(); 
            await Task.CompletedTask;
            return reader;
        }
    }
}
