using DbFacade.DataLayer.ConnectionService;
using DbFacade.Extensions;

namespace DbFacadeUnitTests.TestFacade
{
    internal class ConnectionStringIds
    {
        internal static string SQLUnitTest = "MyUnitTestConnectionString";
    }
    internal class ConnectionStringService
    {
        private readonly string AppId; 
        public ConnectionStringService(string appId)
        {
            AppId = appId;
        }
        internal string GetConnectionString(string connectionStringId)
        {
            return connectionStringId == ConnectionStringIds.SQLUnitTest ? "MyUnitTestConnectionString": null;
        }
    }
    internal class DbConnectionConfigService
    {
        private readonly ConnectionStringService ConnectionStringService;
        internal readonly DbConnectionConfig SQL;
        public DbConnectionConfigService()
        {
            ConnectionStringService = new ConnectionStringService("UnitTestAppId");
            SQL = CreateDbConnectionConfig(new SqlDbConnectionProvider());
        }
        private DbConnectionConfig CreateDbConnectionConfig(IDbConnectionProvider provider)
        {
            provider.BindConnectionString(ConnectionStringService.GetConnectionString);
            provider.BindErrorHandler(OnError);
            return provider.ToDbConnectionConfig();
        }
        private void OnError(EndpointErrorInfo errorInfo)
        {

        }
    }
}
