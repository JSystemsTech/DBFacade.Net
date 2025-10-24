using DbFacade.DataLayer.ConnectionService;

namespace Docs2._0.docfx_project.CodeExamples
{
    public class ConnectionProvider
    {
        public void BasicExample()
        {
            #region InitProvider
            SqlDbConnectionProvider connectionProvider = new SqlDbConnectionProvider();
            #endregion
            #region BindConnectionString
            connectionProvider.BindConnectionString("Some Safely Stored Configured Connection String");
            #endregion

            #region BindErrorHandler
            connectionProvider.BindErrorHandler(OnError);
            #endregion

            #region BindConnectionStringHandler
            connectionProvider.BindConnectionString(GetConnectionString);
            #endregion
        }
        #region OnError
        private void OnError(EndpointErrorInfo endpointErrorInfo)
        {
            //handle error here
        }
        #endregion
        #region GetConnectionString
        private string GetConnectionString(string connectionId)
        {
            return connectionId == "ConnationId1" ?  "Some Safely Stored Configured Connection String" : 
                "Some Other Safely Stored Configured Connection String";
        }
        #endregion
    }
}