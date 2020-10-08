using DbFacade.DataLayer.Models;
using DbFacade.Facade;
using DbFacadeUnitTests.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.TestFacade
{
    internal class UnitTestDomainFacadeBase: DomainFacade<UnitTestMethods>
    {
        
        protected IFetch<FetchData> TestFetchDataHandler
        {
            get=> BuildFetch<FetchData, UnitTestMethods.TestFetchData>();
        }
        protected IFetch<FetchDataWithBadDbColumn> TestFetchDataWithBadDbColumnHandler
        {
            get => BuildFetch<FetchDataWithBadDbColumn, UnitTestMethods.TestFetchData>();
        }
        protected IFetch<FetchDataWithNested> TestFetchDataWithNestedHandler
        {
            get => BuildFetch<FetchDataWithNested, UnitTestMethods.TestFetchData>();
        }
        protected IGenericTransaction<string> TestTransactionHandler
        {
            get => BuildGenericTransaction<UnitTestMethods.TestTransaction, string>();
        }
        protected IFetch<FetchData> TestFetchDataHandlerWithOutput
        {
            get => BuildFetch<FetchData, UnitTestMethods.TestFetchDataWithOutput>();
        }
        protected IGenericTransaction<string> TestTransactionHandlerWithOutput
        {
            get => BuildGenericTransaction<UnitTestMethods.TestTransactionWithOutput, string>();
        }


        #region Async Handlers
        protected async Task<IAsyncFetch<FetchData>> TestFetchDataHandlerAsync()
            => await BuildFetchAsync<FetchData, UnitTestMethods.TestFetchData>();        
        protected async Task<IAsyncFetch<FetchDataWithBadDbColumn>> TestFetchDataWithBadDbColumnHandlerAsync()
            => await BuildFetchAsync<FetchDataWithBadDbColumn, UnitTestMethods.TestFetchData>();
        
        protected async Task<IAsyncFetch<FetchDataWithNested>> TestFetchDataWithNestedHandlerAsync()
            => await BuildFetchAsync<FetchDataWithNested, UnitTestMethods.TestFetchData>();
        
        protected async Task<IAsyncGenericTransaction<string>> TestTransactionHandlerAsync()
            => await BuildGenericTransactionAsync<UnitTestMethods.TestTransaction, string>();
        
        protected async Task<IAsyncFetch<FetchData>> TestFetchDataHandlerWithOutputAsync()
            => await BuildFetchAsync<FetchData, UnitTestMethods.TestFetchDataWithOutput>();
        
        protected async Task<IAsyncGenericTransaction<string>> TestTransactionHandlerWithOutputAsync()
            => await BuildGenericTransactionAsync<UnitTestMethods.TestTransactionWithOutput, string>();        

        #endregion
    }
    internal class UnitTestDomainFacade : UnitTestDomainFacadeBase
    {
        public UnitTestDomainFacade()
        {
            UnitTestConnection connection = new UnitTestConnection(1);
            InitConnectionConfig(connection);
        }
        public IDbResponse<FetchData> TestFetchData()
            => TestFetchDataHandler.Mock(new ResponseData { MyString = "test string" });
        public IDbResponse<FetchDataWithBadDbColumn> TestFetchDataWithBadDbColumn()
            => TestFetchDataWithBadDbColumnHandler.Mock(new ResponseData { MyString = "test string" });
        public IDbResponse<FetchDataWithNested> TestFetchDataWithNested()
            => TestFetchDataWithNestedHandler.Mock(new ResponseData { MyString = "test string" });

        public IDbResponse TestTransaction(string value)
            => TestTransactionHandler.Mock(value, 10);
        public IDbResponse<FetchData> TestFetchDataWithOutput()
            => TestFetchDataHandlerWithOutput.Mock(new ResponseData { MyString = "test string" }, 1, 
                new Dictionary<string, object> {
                    {"MyStringOutputParam", "output response" }
                });
        public IDbResponse TestTransactionWithOutput(string value)
           => TestTransactionHandlerWithOutput.Mock(value, 10,
               new Dictionary<string, object> {
                    {"MyStringOutputParam", "output response" }
               });
        #region Async Calls
        public async Task<IDbResponse<FetchData>> TestFetchDataAsync()
            => await TestFetchDataHandlerAsync().ContinueWith(t => t.Result.MockAsync(new ResponseData { MyString = "test string" }).Result);
        public async Task<IDbResponse<FetchDataWithBadDbColumn>> TestFetchDataWithBadDbColumnAsync()
            => await TestFetchDataWithBadDbColumnHandlerAsync().ContinueWith(t => t.Result.MockAsync(new ResponseData { MyString = "test string" }).Result);
        public async Task<IDbResponse<FetchDataWithNested>> TestFetchDataWithNestedAsync()
            => await TestFetchDataWithNestedHandlerAsync().ContinueWith(t=> t.Result.MockAsync(new ResponseData { MyString = "test string" }).Result);

        public async Task<IDbResponse> TestTransactionAsync(string value)
            => await TestTransactionHandlerAsync().ContinueWith(t => t.Result.MockAsync(value, 10).Result);
        public async Task<IDbResponse<FetchData>> TestFetchDataWithOutputAsync()
            => await TestFetchDataHandlerWithOutputAsync().ContinueWith(t => t.Result.MockAsync(new ResponseData { MyString = "test string" }, 1,
                new Dictionary<string, object> {
                    {"MyStringOutputParam", "output response" }
                }).Result);
        public async Task<IDbResponse> TestTransactionWithOutputAsync(string value)
           => await TestTransactionHandlerWithOutputAsync().ContinueWith(t => t.Result.MockAsync(value, 10,
               new Dictionary<string, object> {
                    {"MyStringOutputParam", "output response" }
               }).Result);


        #endregion
    }
    internal class UnitTestDomainFacadeWithManager : DomainFacade<UnitTestManager,UnitTestMethods>
    {
        protected IFetch<FetchData, UnitTestDbParamsForManager> TestFetchDataHandler
        {
            get => BuildFetch<FetchData, UnitTestMethods.TestFetchData, UnitTestDbParamsForManager>();
        }
        protected async Task<IAsyncFetch<FetchData, UnitTestDbParamsForManager>> TestFetchDataHandlerAsync()
            => await BuildFetchAsync<FetchData, UnitTestMethods.TestFetchData, UnitTestDbParamsForManager>();
        public IDbResponse<FetchData> TestFetchData(bool isValid)
        {
            return TestFetchDataHandler.Mock(
                new UnitTestDbParamsForManager { IsValidModel = isValid },
                new ResponseData { MyString = "test string" });
        }
        public async Task<IDbResponse<FetchData>> TestFetchDataAsync(bool isValid)
            => await TestFetchDataHandlerAsync().ContinueWith(t => t.Result.MockAsync(
                new UnitTestDbParamsForManager { IsValidModel = isValid }, 
                new ResponseData { MyString = "test string" }
                ).Result);
    }

    internal class UnitTestDomainFacadeCustom : DomainFacade<UnitTestDbFacadeStep1, UnitTestMethods>
    {
        protected IFetch<FetchData, UnitTestDbParamsForManager> TestFetchDataHandler
        {
            get => BuildFetch<FetchData, UnitTestMethods.TestFetchData, UnitTestDbParamsForManager>();
        }
        protected async Task<IAsyncFetch<FetchData, UnitTestDbParamsForManager>> TestFetchDataHandlerAsync()
            => await BuildFetchAsync<FetchData, UnitTestMethods.TestFetchData, UnitTestDbParamsForManager>();
        public IDbResponse<FetchData> TestFetchData(bool stopAtStep1, bool stopAtStep2, bool stopAtStep3 )
        {
            return TestFetchDataHandler.Mock(
                new UnitTestDbParamsForManager { StopAtStep1 = stopAtStep1, StopAtStep2 = stopAtStep2, StopAtStep3 = stopAtStep3},
                new ResponseData { MyString = "test string" });
        }
        public async Task<IDbResponse<FetchData>> TestFetchDataAsync(bool stopAtStep1, bool stopAtStep2, bool stopAtStep3)
           => await TestFetchDataHandlerAsync().ContinueWith(t => t.Result.MockAsync(
               new UnitTestDbParamsForManager { StopAtStep1 = stopAtStep1, StopAtStep2 = stopAtStep2, StopAtStep3 = stopAtStep3 },
               new ResponseData { MyString = "test string" }
               ).Result);
    } 
}
