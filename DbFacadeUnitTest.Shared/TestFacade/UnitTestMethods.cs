using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacadeUnitTests.Models;
using System;

namespace DbFacadeUnitTests.TestFacade
{
    internal class UnregisteredConnectionUnitTestMethods
    {
        public static readonly IDbCommandMethod TestUnregisteredConnectionCall
        = UnitTestUnregisteredConnection.TestCommand.CreateMethod();
    }

    internal class UnitTestMethods
    {
        public static readonly IParameterlessDbCommandMethod<TestClass2> TestBenchmark
            = UnitTestConnection.TestBenchmark.CreateParameterlessMethod<TestClass2>();

        public static readonly IParameterlessDbCommandMethod<FetchData> TestFetchData
            = UnitTestConnection.TestFetchData.CreateParameterlessMethod<FetchData>();

        public static readonly IParameterlessDbCommandMethod<UserData> TestMultipleDataSets
            = UnitTestConnection.TestMultipleDataSets.CreateParameterlessMethod<UserData>();

        public static readonly IParameterlessDbCommandMethod<FetchData> TestFetchDataAlt
            = UnitTestConnection.TestFetchDataAlt.CreateParameterlessMethod<FetchData>();

        public static readonly IParameterlessDbCommandMethod<FetchDataWithBadDbColumn> TestFetchDataWithBadDbColumn
            = UnitTestConnection.TestFetchData.CreateParameterlessMethod<FetchDataWithBadDbColumn>();

        public static readonly IParameterlessDbCommandMethod<FetchDataWithNested> TestFetchDataWithNested
            = UnitTestConnection.TestFetchData.CreateParameterlessMethod<FetchDataWithNested>();

        public static readonly IParameterlessDbCommandMethod<FetchData> TestFetchDataWithOutput
            = UnitTestConnection.TestFetchDataWithOutputParameters.CreateParameterlessMethod<FetchData>(
                p =>{
                    p.Add("MyStringOutputParam", p.Factory.OutputString(8000));
                });
        public static readonly IDbCommandMethod<Guid> TestNoData
            = UnitTestConnection.TestNoData.CreateMethod<Guid>(p =>{
                    p.Add("Guid", p.Factory.Create(m=>m));
                });
        public static readonly IDbCommandMethod<UnitTestDbParamsForManager, FetchData> TestFetchDataWithModel
            = UnitTestConnection.TestTransaction.CreateMethod<UnitTestDbParamsForManager, FetchData>();

        public static readonly IDbCommandMethod<string> TestTransaction
            = UnitTestConnection.TestTransaction.CreateMethod<string>(
                p =>{
                    p.Add("MyStringParam", p.Factory.Create(model => model));
                },
                v =>{
                    v.Add(v.Rules.Required(model => model));
                });
        public static readonly IDbCommandMethod<string> TestTransactionWithOutput
             = UnitTestConnection.TestTransaction.CreateMethod<string>(
                 p =>{
                     p.Add("MyStringParam", p.Factory.Create(model => model));
                     p.Add("MyStringOutputParam", p.Factory.OutputString(8000));
                 },
                 v =>{
                     v.Add(v.Rules.Required(model => model));
                 });
        public static readonly IDbCommandMethod<UnitTestDbParamsForManager> TestFetchDataWithModelProcessParams
             = UnitTestConnection.TestFetchData.CreateMethod<UnitTestDbParamsForManager>(
                 p => { },
                 v => { },
                 p => {
                    if (p.StopAtStep1)
                    {
                        throw new System.Exception("Stopping at step 1");
                    }
                },
                p => {
                    if (p.StopAtStep2)
                    {
                        throw new System.Exception("Stopping at step 2");
                    }
                },
                p => {
                    if (p.StopAtStep3)
                    {
                        throw new System.Exception("Stopping at step 3");
                    }
                });
        
    }
}
