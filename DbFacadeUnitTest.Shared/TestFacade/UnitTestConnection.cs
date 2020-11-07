using DbFacade.DataLayer.ConnectionService;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.TestFacade
{
    internal class UnitTestConnection : SqlConnectionConfig<UnitTestConnection>
    {
        public UnitTestConnection(int test) { }
        protected override string GetDbConnectionString() => "MyUnitTestConnectionString";

        protected override string GetDbConnectionProvider() => "MyUnitTestConnectionProvider";

        protected override async Task<string> GetDbConnectionStringAsync() {
            await Task.CompletedTask;
            return "MyUnitTestConnectionString";
        }

        protected override async Task<string> GetDbConnectionProviderAsync()
        {
            await Task.CompletedTask;
            return "MyUnitTestConnectionProvider";
        }

        public static IDbCommandConfig TestFetchData = CreateFetchCommand("TestFetchData", "Test Fetch Data");
        public static IDbCommandConfig TestFetchDataWithOutputParameters = CreateFetchCommand("TestFetchDataWithOutputParameters", "Test Fetch Data with output parameters");
        public static IDbCommandConfig TestTransaction = CreateTransactionCommand("TestTransaction", "Test Transaction");
    }

    internal class UnitTestUnregisteredConnection : SqlConnectionConfig<UnitTestUnregisteredConnection>
    {
        public UnitTestUnregisteredConnection(int test) { }
        protected override string GetDbConnectionString() => "MyUnitTestConnectionString";

        protected override string GetDbConnectionProvider() => "MyUnitTestConnectionProvider";

        protected override async Task<string> GetDbConnectionStringAsync()
        {
            await Task.CompletedTask;
            return "MyUnitTestConnectionString";
        }

        protected override async Task<string> GetDbConnectionProviderAsync()
        {
            await Task.CompletedTask;
            return "MyUnitTestConnectionProvider";
        }

        public static IDbCommandConfig TestCommand = CreateTransactionCommand("TestCommand", "Test Command");
    }
}
