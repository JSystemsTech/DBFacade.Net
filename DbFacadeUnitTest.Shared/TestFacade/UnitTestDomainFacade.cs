using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacadeUnitTests.Models;
using System;
using System.Threading.Tasks;
namespace DbFacadeUnitTests.TestFacade
{

    internal class UnitTestDomainFacade
    {
        public string CurrentErrorMessage { get; private set; }
        public UnitTestDomainFacade()
        {
            Init();
        }
        private void Init()
        {
            UnitTestConnection.Configure(ex =>
            {
                CurrentErrorMessage = ex.Message;
            });
            EnableMockMode();
        }
        private void EnableMockMode()
        {
            
            UnitTestConnection.TestFetchData.AddMockResponseData(MockResponseData.Create(new ResponseData[] { 
                new ResponseData { MyString = "test string", MyEnum = 1 } 
            }, null, 0));
            UnitTestConnection.TestFetchDataAlt.AddMockResponseData(MockResponseData.Create(new ResponseDataAlt[] { 
                new ResponseDataAlt { MyStringAlt = "test string", MyEnumAlt = 1 } 
            }, null, 0));
            UnitTestConnection.TestFetchDataWithOutputParameters.AddMockResponseData(MockResponseData.Create(new ResponseData[] { 
                new ResponseData { MyString = "test string", MyEnum = 1 } 
            }, ov => {
                ov.Add("MyStringOutputParam", "output response");
            }, 0));
            UnitTestConnection.TestFetchData.AddMockResponseData(MockResponseData.Create(new ResponseData[] { 
                new ResponseData { MyString = "test string", MyEnum = 1 } 
            }, 
            null, 1));
            UnitTestConnection.TestTransaction.AddMockResponseData(MockResponseData.Create(ov => { 
                ov.Add("MyStringOutputParam", "output response"); 
            }, 10));
            UnitTestConnection.TestNoData.AddMockResponseData(MockResponseData.Create(0));
            UnitTestConnection.TestMultipleDataSets.AddMockResponseData(MockResponseData.Create(builder => {
                builder.AddResponseData(new[] { new { Name = "Test User", Id = 1 }, new { Name = "Other User", Id = 2 } });
                builder.AddResponseData(new[] { new { Name = "Role One", Value = "R1" }, new { Name = "Role Two", Value = "R2" } });
            }));

            UnitTestConnection.EnableMockMode();
        }
        public IDbResponse TestUnregisteredConnectionCall()=> UnregisteredConnectionUnitTestMethods.TestUnregisteredConnectionCall.Execute();
        public IDbResponse<FetchData> TestFetchData()
            => UnitTestMethods.TestFetchData.Execute();
        public IDbResponse<UserData> TestMultipleDataSets()
        => UnitTestMethods.TestMultipleDataSets.Execute();

        public IDbResponse<FetchData> TestFetchDataAlt()
            => UnitTestMethods.TestFetchDataAlt.Execute();
        public IDbResponse<FetchDataWithBadDbColumn> TestFetchDataWithBadDbColumn()
            => UnitTestMethods.TestFetchDataWithBadDbColumn.Execute();
        public IDbResponse<FetchDataWithNested> TestFetchDataWithNested()
            => UnitTestMethods.TestFetchDataWithNested.Execute();
        public IDbResponse TestTransaction(string value)
            => UnitTestMethods.TestTransaction.Execute(value);
        public IDbResponse<FetchData> TestFetchDataWithOutput()
            => UnitTestMethods.TestFetchDataWithOutput.Execute();
        public IDbResponse TestTransactionWithOutput(string value)
           => UnitTestMethods.TestTransactionWithOutput.Execute(value);
        public IDbResponse TestFetchDataWithModelProcessParams(bool stopAtStep1, bool stopAtStep2, bool stopAtStep3)
           => UnitTestMethods.TestFetchDataWithModelProcessParams.Execute(new UnitTestDbParamsForManager() { StopAtStep1 = stopAtStep1, StopAtStep2 = stopAtStep2, StopAtStep3 = stopAtStep3 });
        public IDbResponse TestNoData()
            => UnitTestMethods.TestNoData.Execute(Guid.NewGuid());
        public IDbResponse<TestClass2> TestBenchmark()
            => UnitTestMethods.TestBenchmark.Execute();

        #region Async Calls
        public async Task<IDbResponse> TestUnregisteredConnectionCallAsync() => await UnregisteredConnectionUnitTestMethods.TestUnregisteredConnectionCall.ExecuteAsync();
        public async Task<IDbResponse<FetchData>> TestFetchDataAsync()
            => await UnitTestMethods.TestFetchData.ExecuteAsync();
        public async Task<IDbResponse<UserData>> TestMultipleDataSetsAsync()
        => await UnitTestMethods.TestMultipleDataSets.ExecuteAsync();
        public async Task<IDbResponse<FetchData>> TestFetchDataAltAsync()
            => await UnitTestMethods.TestFetchDataAlt.ExecuteAsync();
        public async Task<IDbResponse<FetchDataWithBadDbColumn>> TestFetchDataWithBadDbColumnAsync()
            => await UnitTestMethods.TestFetchDataWithBadDbColumn.ExecuteAsync();
        public async Task<IDbResponse<FetchDataWithNested>> TestFetchDataWithNestedAsync()
            => await UnitTestMethods.TestFetchDataWithNested.ExecuteAsync();
        public async Task<IDbResponse> TestTransactionAsync(string value)
            => await UnitTestMethods.TestTransaction.ExecuteAsync(value);
        public async Task<IDbResponse<FetchData>> TestFetchDataWithOutputAsync()
            => await UnitTestMethods.TestFetchDataWithOutput.ExecuteAsync();
        public async Task<IDbResponse> TestTransactionWithOutputAsync(string value)
           => await UnitTestMethods.TestTransactionWithOutput.ExecuteAsync(value);
        public async Task<IDbResponse> TestFetchDataWithModelProcessParamsAsync(bool stopAtStep1, bool stopAtStep2, bool stopAtStep3)
           => await UnitTestMethods.TestFetchDataWithModelProcessParams.ExecuteAsync(new UnitTestDbParamsForManager() { StopAtStep1 = stopAtStep1, StopAtStep2 = stopAtStep2, StopAtStep3 = stopAtStep3 });

        public async Task<IDbResponse> TestNoDataAsync()
            => await UnitTestMethods.TestNoData.ExecuteAsync(Guid.NewGuid());
        public async Task<IDbResponse<TestClass2>> TestBenchmarkAsync()
            => await UnitTestMethods.TestBenchmark.ExecuteAsync();
        #endregion
    }
}
    
