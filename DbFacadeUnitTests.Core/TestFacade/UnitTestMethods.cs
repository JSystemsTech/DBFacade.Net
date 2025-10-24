using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.Extensions;
using DbFacadeUnitTests.Models;
using DbFacadeUnitTests.TestFacade;
using System;

namespace DbFacadeUnitTests.Core.TestFacade
{

    internal class UnitTestMethods
    {
        internal readonly DbConnectionConfig SQLConnection;

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
        internal UnitTestMethods(DbConnectionConfigService dbConnectionConfigService)
        {
            SQLConnection = dbConnectionConfigService.SQL;

            TestBenchmark = SQLConnection.Dbo.DefineEndpoint("TestBenchmark", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestBenchmark");
            });
            TestFetchData = SQLConnection.Dbo.DefineEndpoint("TestFetchData", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData");
            });
            TestMultipleDataSets = SQLConnection.Dbo.DefineEndpoint("TestMultipleDataSets", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestTransaction");
            });
            TestFetchDataAlt = SQLConnection.Dbo.DefineEndpoint("TestFetchDataAlt", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData");
            });

            TestFetchDataWithBadDbColumn = SQLConnection.Dbo.DefineEndpoint("TestFetchDataWithBadDbColumn", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData");
            });
            TestFetchDataWithNested = SQLConnection.Dbo.DefineEndpoint("TestFetchDataWithNested", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData");
            });
            TestFetchDataWithOutput = SQLConnection.Dbo.DefineEndpoint("TestFetchDataWithOutput", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestFetchData")
                .WithParameters(p => {
                    p.AddOutput<string>("MyStringOutputParam", 8000);
                });
            });

            TestNoData = SQLConnection.Dbo.DefineEndpoint("TestNoData", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestTransaction")
                .WithParameters<Guid>((m,p) => {
                    p.AddInput("Guid", m);
                });
            });

            TestFetchDataWithModel = SQLConnection.Dbo.DefineEndpoint("TestFetchDataWithModel", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestTransaction")
                .AsTransaction();
            });

            TestTransaction = SQLConnection.Dbo.DefineEndpoint("TestTransaction", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestTransaction")
                .WithParameters<UnitTestDbParams>(p =>
                {
                    p.AddInput("MyStringParam", model => model.CustomString);
                })
                .WithValidation<UnitTestDbParams>(v => {
                    v.AddIsNotNullOrWhiteSpace(m => m.CustomString, "CustomString is required.");
                })
                .AsTransaction();
            });

            TestTransactionWithOutput = SQLConnection.Dbo.DefineEndpoint("TestTransactionWithOutput", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestTransaction")
                .WithParameters<UnitTestDbParams>(p =>
                {
                    p.AddInput("MyStringParam", model => model.CustomString);
                    p.AddOutput<string>("MyStringOutputParam", 8000);
                })
                .WithValidation<UnitTestDbParams>(v => {
                    v.AddIsNotNullOrWhiteSpace(m => m.CustomString, "CustomString is required.");
                })
                .AsTransaction()
                .BindErrorHandler(e =>
                {
                    bool logError = e.ExceptionType != DbExecutionExceptionType.ValidationError;
                    e.ErrorData["LogError"] = logError;

                });
            });
        }
    }
}
