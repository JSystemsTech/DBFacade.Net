using DbFacade.DataLayer.Models;
using DbFacade.Services;
using DbFacadeUnitTests.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.TestFacade
{

    internal class UnitTestDomainFacade
    {
        public UnitTestDomainFacade()
        {
            UnitTestConnection connection = new UnitTestConnection(1);
            DbConnectionService.Register(connection);
        }
        public IDbResponse TestUnregisteredConnectionCall()=> UnregisteredConnectionUnitTestMethods.TestUnregisteredConnectionCall.Mock(1);
        public IDbResponse<FetchData> TestFetchData()
            => UnitTestMethods.TestFetchData.Mock(new ResponseData { MyString = "test string" }, 0);
        public IDbResponse<FetchDataWithBadDbColumn> TestFetchDataWithBadDbColumn()
            => UnitTestMethods.TestFetchDataWithBadDbColumn.Mock(new ResponseData { MyString = "test string" }, 0);
        public IDbResponse<FetchDataWithNested> TestFetchDataWithNested()
            => UnitTestMethods.TestFetchDataWithNested.Mock(new ResponseData { MyString = "test string" }, 0);
        public IDbResponse TestTransaction(string value)
            => UnitTestMethods.TestTransaction.Mock(new DbParamsModel<string>(value), 10);
        public IDbResponse<FetchData> TestFetchDataWithOutput()
            => UnitTestMethods.TestFetchDataWithOutput.Mock(new ResponseData { MyString = "test string" }, 1, ov=> { ov.Add("MyStringOutputParam", "output response"); });
        public IDbResponse TestTransactionWithOutput(string value)
           => UnitTestMethods.TestTransactionWithOutput.Mock(new DbParamsModel<string>(value), 10, ov => { ov.Add("MyStringOutputParam", "output response"); });
        public IDbResponse TestFetchDataWithModelProcessParams(bool stopAtStep1, bool stopAtStep2, bool stopAtStep3)
           => UnitTestMethods.TestFetchDataWithModelProcessParams.Mock(new UnitTestDbParamsForManager() { StopAtStep1 = stopAtStep1, StopAtStep2 = stopAtStep2, StopAtStep3 = stopAtStep3 }, 0);

        #region Async Calls
        public async Task<IDbResponse> TestUnregisteredConnectionCallAsync() => await UnregisteredConnectionUnitTestMethods.TestUnregisteredConnectionCall.MockAsync(1);
        public async Task<IDbResponse<FetchData>> TestFetchDataAsync()
            => await UnitTestMethods.TestFetchData.MockAsync(new ResponseData { MyString = "test string" }, 0);
        public async Task<IDbResponse<FetchDataWithBadDbColumn>> TestFetchDataWithBadDbColumnAsync()
            => await UnitTestMethods.TestFetchDataWithBadDbColumn.MockAsync(new ResponseData { MyString = "test string" }, 0);
        public async Task<IDbResponse<FetchDataWithNested>> TestFetchDataWithNestedAsync()
            => await UnitTestMethods.TestFetchDataWithNested.MockAsync(new ResponseData { MyString = "test string" }, 0);
        public async Task<IDbResponse> TestTransactionAsync(string value)
            => await UnitTestMethods.TestTransaction.MockAsync(new DbParamsModel<string>(value), 10);
        public async Task<IDbResponse<FetchData>> TestFetchDataWithOutputAsync()
            => await UnitTestMethods.TestFetchDataWithOutput.MockAsync(new ResponseData { MyString = "test string" }, 1, ov => { ov.Add("MyStringOutputParam", "output response"); });
        public async Task<IDbResponse> TestTransactionWithOutputAsync(string value)
           => await UnitTestMethods.TestTransactionWithOutput.MockAsync(new DbParamsModel<string>(value), 10, ov => { ov.Add("MyStringOutputParam", "output response"); });
        public async Task<IDbResponse> TestFetchDataWithModelProcessParamsAsync(bool stopAtStep1, bool stopAtStep2, bool stopAtStep3)
           => await UnitTestMethods.TestFetchDataWithModelProcessParams.MockAsync(new UnitTestDbParamsForManager() { StopAtStep1 = stopAtStep1, StopAtStep2 = stopAtStep2, StopAtStep3 = stopAtStep3 }, 0);
        #endregion
    }
}
    
