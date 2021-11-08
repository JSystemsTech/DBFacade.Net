using DbFacade.DataLayer.ConnectionService;
using DbFacade.Exceptions;
using DbFacade.Factories;
using System;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.TestFacade
{
    internal class UnitTestConnection : SqlConnectionConfig<UnitTestConnection>
    {
        private Action<Exception> ErrorHandler { get; set; }
        public UnitTestConnection(Action<Exception> errorHandler) { ErrorHandler = errorHandler;  }
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

        protected override void OnError(Exception ex)
        => ErrorHandler(ex);
        protected override async Task OnErrorAsync(Exception ex)
        {
            ErrorHandler(ex);
            await Task.CompletedTask;
        }


        public static IDbCommandConfig TestFetchData = DbCommandConfigFactory<UnitTestConnection>.CreateFetchCommand("TestFetchData", "Test Fetch Data");
        public static IDbCommandConfig TestFetchDataAlt = DbCommandConfigFactory<UnitTestConnection>.CreateFetchCommand("TestFetchData", "Test Fetch Data");
        public static IDbCommandConfig TestFetchDataWithOutputParameters = DbCommandConfigFactory<UnitTestConnection>.CreateFetchCommand("TestFetchDataWithOutputParameters", "Test Fetch Data with output parameters");
        public static IDbCommandConfig TestTransaction = DbCommandConfigFactory<UnitTestConnection>.CreateTransactionCommand("TestTransaction", "Test Transaction");
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

        public static IDbCommandConfig TestCommand = DbCommandConfigFactory<UnitTestUnregisteredConnection>.CreateTransactionCommand("TestCommand", "Test Command", false);
    }
}
