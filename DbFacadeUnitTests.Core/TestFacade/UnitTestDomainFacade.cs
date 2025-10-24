using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
using DbFacadeUnitTests.Core.Models;
using DbFacadeUnitTests.Models;
using DbFacadeUnitTests.TestFacade;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
namespace DbFacadeUnitTests.Core.TestFacade
{

    internal class UnitTestDomainFacade
    {
        public string CurrentErrorMessage { get; private set; }
        internal readonly UnitTestMethods UnitTestMethods;
        internal DbConnectionConfig UnitTestConnection => UnitTestMethods.SQLConnection;

        
        public UnitTestDomainFacade()
        {
            var dbConnectionConfigService = new DbConnectionConfigService();
            UnitTestMethods = new UnitTestMethods(dbConnectionConfigService);
            Init();
        }
        private void Init()
        {            
            EnableMockMode();
        }
        private void EnableMockMode()
        {
            UnitTestMethods.TestFetchData.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1 });
                b.ReturnValue = 0;
            });

            UnitTestMethods.TestFetchDataAlt.EnableMockMode(b => {
                b.Add(new ResponseDataAlt { MyStringAlt = "test string", MyEnumAlt = 1 });
                b.ReturnValue = 0;
            });
            UnitTestMethods.TestFetchDataWithOutput.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1 });
                b.OutputValues["MyStringOutputParam"] = "output response";
                b.ReturnValue = 0;
            });

            UnitTestMethods.TestFetchDataWithBadDbColumn.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1 });
                b.ReturnValue = 0;
            });

            UnitTestMethods.TestFetchDataWithNested.EnableMockMode(b => {
                b.Add(new ResponseData { MyString = "test string", MyEnum = 1, Flag = "TRUE", FlagInt = 1, FlagFalse = "False", FlagIntFalse = 0 });
                b.ReturnValue = 0;
            });

            UnitTestMethods.TestTransaction.EnableMockMode(b => {
                b.ReturnValue = 10;
            });

            UnitTestMethods.TestTransactionWithOutput.EnableMockMode(b => {
                b.OutputValues["MyStringOutputParam"] = "output response";
                b.ReturnValue = 10;
            });

            UnitTestMethods.TestNoData.EnableMockMode(b => {
                b.ReturnValue = 0;
            });
            UnitTestMethods.TestMultipleDataSets.EnableMockMode(b => {
                b.Add(new ResponseDataMulti1 { Name = "Test User", Id = 1 }, new ResponseDataMulti1 { Name = "Other User", Id = 2 });
                b.Add(new ResponseDataMulti2 { Name = "Role One", Value = "R1" }, new ResponseDataMulti2 { Name = "Role Two", Value = "R2" });
                b.ReturnValue = 0;
            });
        }

        public IDbResponseCustom<FetchData> TestFetchData()
            => UnitTestMethods.TestFetchData.Execute().ToDbResponseCustom<FetchData>();
        public IDbResponseCustom<UserData> TestMultipleDataSets()
        => UnitTestMethods.TestMultipleDataSets.Execute().ToDbResponseCustom<UserData>();

        public IDbResponseCustom<FetchDataAlt> TestFetchDataAlt()
            => UnitTestMethods.TestFetchDataAlt.Execute().ToDbResponseCustom<FetchDataAlt>();
        public IDbResponseCustom<FetchDataWithBadDbColumn> TestFetchDataWithBadDbColumn()
            => UnitTestMethods.TestFetchDataWithBadDbColumn.Execute().ToDbResponseCustom<FetchDataWithBadDbColumn>();
        public IDbResponseCustom<FetchDataWithNested> TestFetchDataWithNested()
            => UnitTestMethods.TestFetchDataWithNested.Execute().ToDbResponseCustom<FetchDataWithNested>();
        public IDbResponseCustom TestTransaction(string value)
            => UnitTestMethods.TestTransaction.Execute(new UnitTestDbParams() { CustomString = value }).ToDbResponseCustom();
        public IDbResponseCustom<FetchData> TestFetchDataWithOutput()
            => UnitTestMethods.TestFetchDataWithOutput.Execute().ToDbResponseCustom<FetchData>();
        public IDbResponseCustom TestTransactionWithOutput(string value)
           => UnitTestMethods.TestTransactionWithOutput.Execute(new UnitTestDbParams() { CustomString = value }).ToDbResponseCustom();
        
        public IDbResponseCustom TestNoData()
            => UnitTestMethods.TestNoData.Execute(Guid.NewGuid()).ToDbResponseCustom();
        public IDbResponseCustom<TestClass2> TestBenchmark()
            => UnitTestMethods.TestBenchmark.Execute().ToDbResponseCustom<TestClass2>();

        public IDbResponse TestBenchmark2()
        {
            var response = UnitTestMethods.TestBenchmark.Execute();
            response.ToDbDataModelList<TestClass2>((model, collection) => {
                model.String = collection.GetValue<string>("str");
            });
            return response;
        }

        #region Async Calls
        public async Task<IDbResponseCustom<FetchData>> TestFetchDataAsync()
            => await UnitTestMethods.TestFetchData.ExecuteAsync().ToDbResponseCustomAsync<FetchData>();
        public async Task<IDbResponseCustom<UserData>> TestMultipleDataSetsAsync()
        => await UnitTestMethods.TestMultipleDataSets.ExecuteAsync().ToDbResponseCustomAsync<UserData>();
        public async Task<IDbResponseCustom<FetchDataAlt>> TestFetchDataAltAsync()
            => await UnitTestMethods.TestFetchDataAlt.ExecuteAsync().ToDbResponseCustomAsync<FetchDataAlt>();
        public async Task<IDbResponseCustom<FetchDataWithBadDbColumn>> TestFetchDataWithBadDbColumnAsync()
            => await UnitTestMethods.TestFetchDataWithBadDbColumn.ExecuteAsync().ToDbResponseCustomAsync<FetchDataWithBadDbColumn>();
        public async Task<IDbResponseCustom<FetchDataWithNested>> TestFetchDataWithNestedAsync()
            => await UnitTestMethods.TestFetchDataWithNested.ExecuteAsync().ToDbResponseCustomAsync<FetchDataWithNested>();
        public async Task<IDbResponseCustom> TestTransactionAsync(string value)
            => await UnitTestMethods.TestTransaction.ExecuteAsync(new UnitTestDbParams() { CustomString = value }).ToDbResponseCustomAsync();
        public async Task<IDbResponseCustom<FetchData>> TestFetchDataWithOutputAsync()
            => await UnitTestMethods.TestFetchDataWithOutput.ExecuteAsync().ToDbResponseCustomAsync<FetchData>();
        public async Task<IDbResponseCustom> TestTransactionWithOutputAsync(string value)
           => await UnitTestMethods.TestTransactionWithOutput.ExecuteAsync(new UnitTestDbParams() { CustomString = value }).ToDbResponseCustomAsync();        
        public async Task<IDbResponseCustom> TestNoDataAsync()
            => await UnitTestMethods.TestNoData.ExecuteAsync(Guid.NewGuid()).ToDbResponseCustomAsync();
        public async Task<IDbResponseCustom<TestClass2>> TestBenchmarkAsync()
            => await UnitTestMethods.TestBenchmark.ExecuteAsync().ToDbResponseCustomAsync<TestClass2>();
        #endregion
    }

    internal static class DbResponseCustomExtensions
    {

        public static IDbResponseCustom ToDbResponseCustom(this IDbResponse dbResponse)
            => new DbResponseCustom(dbResponse);
        public static IDbResponseCustom<T> ToDbResponseCustom<T>(this IDbResponse dbResponse)
            where T : class, IDbDataModel
            => new DbResponseCustom<T>(dbResponse);

        public static async Task<IDbResponseCustom> ToDbResponseCustomAsync(this Task<IDbResponse> dbResponse)
        {
            var dbResponseCustom = new DbResponseCustom(await dbResponse);
            await Task.CompletedTask;
            return dbResponseCustom;
        }
        public static async Task<IDbResponseCustom<T>> ToDbResponseCustomAsync<T>(this Task<IDbResponse> dbResponse)
            where T : class, IDbDataModel
        {
            var dbResponseCustom = new DbResponseCustom<T>(await dbResponse);
            await Task.CompletedTask;
            return dbResponseCustom;
        }
    }

    public interface IDbResponseCustom
    {
        int ReturnValue { get; }
        EndpointErrorInfo ErrorInfo { get; }
        bool HasError { get; }
        string ErrorMessage { get; }
        string ErrorDetails { get; }
        object GetOutputValue(string key);
        T GetOutputModel<T>(Action<T, IDataCollection> initialize) where T : class;
        T GetOutputModel<T>() where T : class, IDbDataModel;
        IEnumerable<IDbDataTable> DbDataTables { get; }
        DataSet DataSet { get; }
    }

    internal class DbResponseCustom : IDbResponseCustom
    {
        private readonly IDbResponse Response;
        internal DbResponseCustom(IDbResponse response)
        {
            Response = response;
        }
        public int ReturnValue => Response.ReturnValue;

        public EndpointErrorInfo ErrorInfo => Response.ErrorInfo;

        public bool HasError => Response.HasError;

        public string ErrorMessage => HasError ? Response.ErrorInfo.ErrorMessage : "";

        public string ErrorDetails => HasError ? Response.ErrorInfo.ErrorDetails : "";

        public IEnumerable<IDbDataTable> DbDataTables => Response.DbDataTables;

        public DataSet DataSet => Response.DataSet;

        public T GetOutputModel<T>(Action<T, IDataCollection> initialize) where T : class
        => Response.GetOutputModel(initialize);

        public object GetOutputValue(string key)
        => Response.GetOutputValue(key);
        T IDbResponseCustom.GetOutputModel<T>()
        => Response.GetOutputModel<T>();
    }

    public interface IDbResponseCustom<T>:IEnumerable<T>
    {
        int ReturnValue { get; }
        EndpointErrorInfo ErrorInfo { get; }
        bool HasError { get; }
        string ErrorMessage { get; }
        string ErrorDetails { get; }
        object GetOutputValue(string key);
        To GetOutputModel<To>(Action<To, IDataCollection> initialize) where To : class;
        To GetOutputModel<To>() where To : class, IDbDataModel;
        IEnumerable<IDbDataTable> DbDataTables { get; }
        DataSet DataSet { get; }
    }

    internal class DbResponseCustom<T> : IDbResponseCustom<T>
        where T : class, IDbDataModel
    {
        private readonly IDbResponse Response;
        private readonly IEnumerable<T> Data;
        internal DbResponseCustom(IDbResponse response)
        {
            Response = response;
            Data = response.ToDbDataModelList<T>();
        }
        public int ReturnValue => Response.ReturnValue;

        public EndpointErrorInfo ErrorInfo => Response.ErrorInfo;

        public bool HasError => Response.HasError;

        public string ErrorMessage => HasError ? Response.ErrorInfo.ErrorMessage : "";

        public string ErrorDetails => HasError ? Response.ErrorInfo.ErrorDetails : "";

        public IEnumerable<IDbDataTable> DbDataTables => Response.DbDataTables;

        public DataSet DataSet => Response.DataSet;

        public IEnumerator<T> GetEnumerator()
        => Data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        => Data.GetEnumerator();

        public To GetOutputModel<To>(Action<To, IDataCollection> initialize) where To : class
        => Response.GetOutputModel(initialize);

        public object GetOutputValue(string key)
        => Response.GetOutputValue(key);
        To IDbResponseCustom<T>.GetOutputModel<To>()
        => Response.GetOutputModel<To>();
    }
}
    
