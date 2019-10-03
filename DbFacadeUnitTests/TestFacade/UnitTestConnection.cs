using DBFacade.DataLayer.ConnectionService;

namespace DbFacadeUnitTests.TestFacade
{
    internal class UnitTestConnection : SQLConnectionConfig<UnitTestConnection>
    {
        public UnitTestConnection(int test)
        {

        }
        protected override string GetDbConnectionString() => "MyUnitTestConnectionString";

        protected override string GetDbConnectionProvider() => "MyUnitTestConnectionProvider";

        public static IDbCommandText TestFetchData = CreateCommandText("TestFetchData", "Test Fetch Data");
        public static IDbCommandText TestTransaction = CreateCommandText("TestTransaction", "Test Transaction");
    }
}
