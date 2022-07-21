using DbFacade.DataLayer.Models;
using DbFacade.Services;
using DbFacadeUnitTests.Models;
using System;
using System.Collections.Generic;
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
            new UnitTestConnection(ex=> {
                CurrentErrorMessage = ex.Message;
            }).Register();
            EnableMockMode();
        }
        private void EnableMockMode()
        {
            IDictionary<Guid, MockResponseData> mockDataDictionary = new Dictionary<Guid, MockResponseData>()
            {
                { UnitTestConnection.TestFetchData.CommandId, MockResponseData.Create(new ResponseData[] { new ResponseData { MyString = "test string", MyEnum = 1 } }, null, 0)},
                { UnitTestConnection.TestFetchDataAlt.CommandId, MockResponseData.Create(new ResponseDataAlt[] { new ResponseDataAlt { MyStringAlt = "test string", MyEnumAlt = 1 } },null, 0)},
                { UnitTestConnection.TestFetchDataWithOutputParameters.CommandId, MockResponseData.Create(new ResponseData[] { new ResponseData { MyString = "test string", MyEnum = 1 } }, ov => { ov.Add("MyStringOutputParam", "output response"); }, 1)},
                { UnitTestConnection.TestTransaction.CommandId, MockResponseData.Create(ov => { ov.Add("MyStringOutputParam", "output response"); }, 10)},
                { UnitTestConnection.TestNoData.CommandId, MockResponseData.Create(0)}
            };
            DbConnectionService.EnableMockMode<UnitTestConnection>(mockDataDictionary);
        }
        public IDbResponse TestUnregisteredConnectionCall()=> UnregisteredConnectionUnitTestMethods.TestUnregisteredConnectionCall.Execute();
        public IDbResponse<FetchData> TestFetchData()
            => UnitTestMethods.TestFetchData.Execute();
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

        #region Async Calls
        public async Task<IDbResponse> TestUnregisteredConnectionCallAsync() => await UnregisteredConnectionUnitTestMethods.TestUnregisteredConnectionCall.ExecuteAsync();
        public async Task<IDbResponse<FetchData>> TestFetchDataAsync()
            => await UnitTestMethods.TestFetchData.ExecuteAsync();
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
        #endregion
    }
}
    
