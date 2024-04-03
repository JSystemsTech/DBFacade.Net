using DbFacade.DataLayer.ConnectionService;
using DbFacade.Exceptions;
using DbFacade.Factories;
using DbFacade.Utils;
using System;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.TestFacade
{
    internal class UnitTestConnection : SqlConnectionConfig<UnitTestConnection>
    {
        private static Action<Exception> ErrorHandler { get; set; }

        public static void Configure(Action<Exception> errorHandler)
        {
            ErrorHandler = errorHandler;
            Configure(GetDbConnectionString, o => {
                o.SetOnErrorHandler(OnError); 
                o.SetOnErrorHandlerAsync(OnErrorAsync);
            });
        }
        private static string GetDbConnectionString() => "MyUnitTestConnectionString";


        private static void OnError(Exception ex, IDbCommandSettings dbCommandSettings)
        => ErrorHandler(ex);
        private static async Task OnErrorAsync(Exception ex, IDbCommandSettings dbCommandSettings)
        {
            ErrorHandler(ex);
            await Task.CompletedTask;
        }


        public static IDbCommandConfig TestFetchData = Dbo.CreateFetchCommand("TestFetchData", "Test Fetch Data");
        public static IDbCommandConfig TestFetchDataAlt = Dbo.CreateFetchCommand("TestFetchData", "Test Fetch Data");
        public static IDbCommandConfig TestFetchDataWithOutputParameters = Dbo.CreateFetchCommand("TestFetchDataWithOutputParameters", "Test Fetch Data with output parameters");
        public static IDbCommandConfig TestTransaction = Dbo.CreateTransactionCommand("TestTransaction", "Test Transaction");
        public static IDbCommandConfig TestNoData = Dbo.CreateFetchCommand("TestTransaction", "Test Transaction");
        public static IDbCommandConfig TestMultipleDataSets = Dbo.CreateFetchCommand("TestTransaction", "Test Transaction");
        public static IDbCommandConfig TestBenchmark = Dbo.CreateFetchCommand("TestBenchmark", "Test Benchmark");
    }

    internal class UnitTestUnregisteredConnection : SqlConnectionConfig<UnitTestUnregisteredConnection>
    {
        public static void Configure()
        {
            Configure(GetDbConnectionString, o => {});
        }
        protected static string GetDbConnectionString() => "MyUnitTestConnectionString";


        public static IDbCommandConfig TestCommand = Dbo.CreateTransactionCommand("TestCommand", "Test Command", false);
    }
}
