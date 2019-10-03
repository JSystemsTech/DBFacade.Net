using DBFacade.DataLayer.Models;
using DBFacade.Facade;
using DbFacadeUnitTests.Models;

namespace DbFacadeUnitTests.TestFacade
{
    internal class UnitTestDomainFacade: DomainFacade<UnitTestMethods>
    {
        public UnitTestDomainFacade()
        {
            UnitTestConnection connection = new UnitTestConnection(1);
            InitConnectionConfig(connection);
        }
        public IDbResponse<FetchData> TestFetchData()
        {            
            return MockFetch<FetchData,UnitTestMethods.TestFetchData, ResponseData>(new ResponseData { MyString = "test string" });
        }
        public IDbResponse<FetchDataWithNested> TestFetchDataWithNsted()
        {
            return MockFetch<FetchDataWithNested, UnitTestMethods.TestFetchData, ResponseData>(new ResponseData { MyString = "test string" });
        }
        public IDbResponse TestTransaction(string value)
        {
            return MockTransaction<DbParamsModel<string>, UnitTestMethods.TestTransaction>(new DbParamsModel<string>(value), 10);
        }
    }
    internal class UnitTestDomainFacadeWithManager : DomainFacade<UnitTestManager,UnitTestMethods>
    {
        public IDbResponse<FetchData> TestFetchData(bool isValid)
        {
            return MockFetch<FetchData,UnitTestDbParamsForManager, UnitTestMethods.TestFetchData, ResponseData>(new ResponseData { MyString = "test string" }, new UnitTestDbParamsForManager { IsValidModel = isValid });
        }
    }

    internal class UnitTestDomainFacadeCustom : DomainFacade<UnitTestDbFacadeStep1, UnitTestMethods>
    {
        public IDbResponse<FetchData> TestFetchData(bool stopAtStep1, bool stopAtStep2, bool stopAtStep3 )
        {
            return MockFetch<FetchData, UnitTestDbParamsForManager, UnitTestMethods.TestFetchData, ResponseData>(new ResponseData { MyString = "test string" }, new UnitTestDbParamsForManager { StopAtStep1 = stopAtStep1, StopAtStep2 = stopAtStep2, StopAtStep3 = stopAtStep3});
        }
    } 
}
