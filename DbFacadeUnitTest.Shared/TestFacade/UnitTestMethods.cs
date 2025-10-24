using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.Extensions;
using DbFacadeUnitTests.Models;
using System;

namespace DbFacadeUnitTests.TestFacade
{

    internal class UnitTestMethods
    {
        internal readonly SqlDbConnectionConfig UnitTestConnection;

        internal readonly IDbCommandMethod TestBenchmark;
        internal readonly IDbCommandMethod TestFetchData;
        internal readonly IDbCommandMethod TestMultipleDataSets;
        internal readonly IDbCommandMethod TestFetchDataAlt;
        internal readonly IDbCommandMethod TestFetchDataWithBadDbColumn;
        internal readonly IDbCommandMethod TestFetchDataWithNested;
        internal readonly IDbCommandMethod TestFetchDataWithOutput;
        internal readonly IDbCommandMethod TestNoData;
        internal readonly IDbCommandMethod TestFetchDataWithModel;
        internal readonly IDbCommandMethod TestTransaction;
        internal readonly IDbCommandMethod TestTransactionWithOutput;
        internal UnitTestMethods(Action<Exception> errorHandler)
        {
            UnitTestConnection = new UnitTestConnection(new UnitTestConnectionProvider(errorHandler));

            TestBenchmark = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestBenchmark")
                .WithLabel("Test Benchmark");
            });
            TestFetchData = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestFetchData")
                .WithLabel("Test Fetch Data");
            });
            TestMultipleDataSets = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestTransaction")
                .WithLabel("Test Transaction");
            });
            TestFetchDataAlt = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestFetchData")
                .WithLabel("Test Fetch Data");
            });

            TestFetchDataWithBadDbColumn = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestFetchData")
                .WithLabel("Test Fetch Data");
            });
            TestFetchDataWithNested = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestFetchData")
                .WithLabel("Test Fetch Data");
            });
            TestFetchDataWithOutput = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestFetchData")
                .WithLabel("Test Fetch Data")
                .WithParameters(p => {
                    p.Add("MyStringOutputParam", p.Factory.OutputString(8000));
                });
            });

            TestNoData = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestTransaction")
                .WithLabel("Test Transaction")
                .WithParameters<Guid>(p => {
                    p.Add("Guid", p.Factory.Create(m => m));
                });
            });

            TestFetchDataWithModel = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestTransaction")
                .WithLabel("Test Transaction")
                //.WithParameters<UnitTestDbParamsForManager>(p => {
                //    p.Add("Guid", p.Factory.Create(m => m.));
                //})
                .AsTransaction();
            });

            TestTransaction = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestTransaction")
                .WithLabel("Test Transaction")
                .WithParameters<UnitTestDbParams>(p =>
                {
                    p.Add("MyStringParam", p.Factory.Create(model => model.CustomString));
                })
                .WithValidation<UnitTestDbParams>(v => {
                    v.Add(v.Rules.Required(model => model.CustomString));
                })
                .AsTransaction();
            });

            TestTransactionWithOutput = UnitTestConnection.Dbo.CreateMethod(o => {
                o.AsStoredProcedure("TestTransaction")
                .WithLabel("Test Transaction")
                .WithParameters<UnitTestDbParams>(p =>
                {
                    p.Add("MyStringParam", p.Factory.Create(model => model.CustomString));
                    p.Add("MyStringOutputParam", p.Factory.OutputString(8000));
                })
                .WithValidation<UnitTestDbParams>(v => {
                    v.Add(v.Rules.Required(model => model.CustomString));
                })
                .AsTransaction();
            });
        }
    }
}
