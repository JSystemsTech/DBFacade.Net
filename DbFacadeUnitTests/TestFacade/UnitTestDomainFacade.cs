﻿using DbFacade.Facade.Core;
using DBFacade.DataLayer.Models;
using DBFacade.Facade;
using DbFacadeUnitTests.Models;

namespace DbFacadeUnitTests.TestFacade
{
    internal class UnitTestDomainFacadeBase: DomainFacade<UnitTestMethods>
    {
        
        protected IFetch<FetchData> TestFetchDataHandler
        {
            get=> BuildFetch<FetchData, UnitTestMethods.TestFetchData>();
        }
        protected IFetch<FetchDataWithNested> TestFetchDataWithNestedHandler
        {
            get => BuildFetch<FetchDataWithNested, UnitTestMethods.TestFetchData>();
        }
        protected IGenericTransaction<string> TestTransactionHandler
        {
            get => BuildGenericTransaction<UnitTestMethods.TestTransaction, string>();
        }


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
        public IDbResponse<FetchDataWithNested> TestFetchDataWithNested()
            => TestFetchDataWithNestedHandler.Mock(new ResponseData { MyString = "test string" });

        public IDbResponse TestTransaction(string value)
            => TestTransactionHandler.Mock(value, 10);

    }
    internal class UnitTestDomainFacadeWithManager : DomainFacade<UnitTestManager,UnitTestMethods>
    {
        protected IFetch<FetchData, UnitTestDbParamsForManager> TestFetchDataHandler
        {
            get => BuildFetch<FetchData, UnitTestMethods.TestFetchData, UnitTestDbParamsForManager>();
        }
        public IDbResponse<FetchData> TestFetchData(bool isValid)
        {
            return TestFetchDataHandler.Mock(
                new UnitTestDbParamsForManager { IsValidModel = isValid },
                new ResponseData { MyString = "test string" });
        }
    }

    internal class UnitTestDomainFacadeCustom : DomainFacade<UnitTestDbFacadeStep1, UnitTestMethods>
    {
        protected IFetch<FetchData, UnitTestDbParamsForManager> TestFetchDataHandler
        {
            get => BuildFetch<FetchData, UnitTestMethods.TestFetchData, UnitTestDbParamsForManager>();
        }
        public IDbResponse<FetchData> TestFetchData(bool stopAtStep1, bool stopAtStep2, bool stopAtStep3 )
        {
            return TestFetchDataHandler.Mock(
                new UnitTestDbParamsForManager { StopAtStep1 = stopAtStep1, StopAtStep2 = stopAtStep2, StopAtStep3 = stopAtStep3},
                new ResponseData { MyString = "test string" });
        }
    } 
}
