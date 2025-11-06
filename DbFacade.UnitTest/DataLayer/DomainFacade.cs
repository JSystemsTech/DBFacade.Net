using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
using DbFacade.UnitTest.DataLayer.EndpointLayer;
using DbFacade.UnitTest.DataLayer.Models.Data;
using DbFacade.UnitTest.DataLayer.Models.Parameters;
using DbFacade.UnitTest.Services;

namespace DbFacade.UnitTest.DataLayer
{
    internal class DomainFacade
    {
        private readonly Endpoints Endpoints;
        public DomainFacade(Endpoints endpoints)
        {
            Endpoints = endpoints;
        }

        public IDbResponse TestNullableParams(Guid? data)
        => Endpoints.TestNullableParams.Execute(data);
        public IDbResponse TestNullableStringParams(string data)
        => Endpoints.TestNullableStringParams.Execute(data);
        public IDbResponse TestFetchData(out IEnumerable<FetchData> data)
        => Endpoints.TestFetchData.ExecuteAndFetchFirst(out data);
        public IDbResponse TestFetchDataNoSchema(out IEnumerable<FetchData> data)
        => Endpoints.TestFetchDataNoSchema.ExecuteAndFetchFirst(out data);
        public IDbResponse TestMultipleDataSets(out IEnumerable<UserData> data)
        => Endpoints.TestMultipleDataSets.ExecuteAndFetchFirst(out data);
        public IDbResponse TestFetchDataAlt(out IEnumerable<FetchDataAlt> data)
        => Endpoints.TestFetchDataAlt.ExecuteAndFetchFirst((model, collection) => {
            model.MyEnum = collection.GetValue<FetchDataEnum>("MyEnumAlt");
            model.MyString = collection.GetValue<string>("MyStringAlt");
            model.MyChar = collection.GetValue<string>("MyCharAlt");
            model.Integer = collection.GetValue<int>("IntegerAlt");
            model.IntegerOptional = collection.GetValue<int?>("IntegerOptionalAlt");
            model.IntegerOptionalNull = collection.GetValue<int?>("IntegerOptionalAlt");
            model.PublicKey = collection.GetValue<Guid>("PublicKeyAlt");
            model.MyByte = collection.GetValue<byte>("MyByteAlt");
            model.MyByteAsInt = collection.GetValue<int>("MyByteAlt");
        },out data);
        public IDbResponse TestFetchDataAltWithParams(out IEnumerable<FetchDataAlt> data)
        => Endpoints.TestFetchDataAltWithParams.ExecuteAndFetchFirst(new UnitTestDbParams() { },(model, collection) => {
            model.MyEnum = collection.GetValue<FetchDataEnum>("MyEnumAlt");
            model.MyString = collection.GetValue<string>("MyStringAlt");
            model.MyChar = collection.GetValue<string>("MyCharAlt");
            model.Integer = collection.GetValue<int>("IntegerAlt");
            model.IntegerOptional = collection.GetValue<int?>("IntegerOptionalAlt");
            model.IntegerOptionalNull = collection.GetValue<int?>("IntegerOptionalAlt");
            model.PublicKey = collection.GetValue<Guid>("PublicKeyAlt");
            model.MyByte = collection.GetValue<byte>("MyByteAlt");
            model.MyByteAsInt = collection.GetValue<int>("MyByteAlt");
        }, out data);

        public IDbResponse TestMismatchParams()
        => Endpoints.TestFetchDataAltWithParams.Execute(Guid.NewGuid());
        public IDbResponse TestMismatchParams2()
        => Endpoints.TestParametersMismatch2.Execute(Guid.NewGuid());
        public IDbResponse TestMismatchValidationParams()
        => Endpoints.TestTransaction.Execute(Guid.NewGuid());


        public IDbResponse TestFetchDataWithBadDbColumn(out IEnumerable<FetchDataWithBadDbColumn> data)
        => Endpoints.TestFetchDataWithBadDbColumn.ExecuteAndFetchFirst(out data);
        public IDbResponse TestFetchDataWithNested(out IEnumerable<FetchDataWithNested> data)
        => Endpoints.TestFetchDataWithNested.ExecuteAndFetchFirst(out data);
        public IDbResponse TestTransaction(string value, out IEnumerable<UserData> data)
        => Endpoints.TestTransaction.ExecuteAndFetchFirst(new UnitTestDbParams() { CustomString = value }, out data);
        public IDbResponse TestFetchDataWithOutput(out IEnumerable<FetchData> data)
        => Endpoints.TestFetchDataWithOutput.ExecuteAndFetchFirst(out data);
        public IDbResponse TestNoData()
        => Endpoints.TestNoData.Execute(Guid.NewGuid());
        public IDbResponse TestBenchmark(out IEnumerable<TestClass2> data)
        => Endpoints.TestBenchmark.ExecuteAndFetchFirst(out data);
        public IDbResponse TestBenchmark2(out IEnumerable<TestClass2> data)
            => Endpoints.TestBenchmark.ExecuteAndFetchFirst((model, collection) => {
                model.String = collection.GetValue<string>("str");
            }, out data);
        public IDbResponse TestException(Exception ex)
        => Endpoints.TestException.Execute(ex);
        public IDbResponse TestParameters()
        => Endpoints.TestParameters.Execute();

        public IDbResponse TestReal()
        => Endpoints.TestReal.Execute();
        public IDbResponse TestBadConnStr()
        => Endpoints.TestBadConnStr.Execute();
        public IDbResponse TestValidation()
        => Endpoints.TestValidation.Execute(new UnitTestParamsForValidation());
        public IDbResponse TestValidationFail()
        => Endpoints.TestValidationFail.Execute(new UnitTestParamsForValidation());
        public IDbResponse TestTableDirect(out IEnumerable<FetchData> data)
        => Endpoints.TestTableDirect.ExecuteAndFetchFirst(out data);
        public IDbResponse TestSqlCredendials()
        => Endpoints.TestSqlCredendials.Execute();
        public IDbResponse TestXml(out IEnumerable<FetchData> data)
        => Endpoints.TestXml.ExecuteAndFetchFirst(out data);
        public IDbResponse TestScalar()
        => Endpoints.TestScalar.Execute();
        public IDbResponse TestMockException_ExecuteQuery()
        => Endpoints.TestMockException_ExecuteQuery.Execute();
        public IDbResponse TestMockException_ExecuteNonQuery()
        => Endpoints.TestMockException_ExecuteNonQuery.Execute();
        public IDbResponse TestMockException_ExecuteScalar()
        => Endpoints.TestMockException_ExecuteScalar.Execute();
        public IDbResponse TestMockException_ExecuteXml()
        => Endpoints.TestMockException_ExecuteXml.Execute();
        public IDbResponse TestMockException_Transaction()
        => Endpoints.TestMockException_Transaction.Execute();
        public IDbResponse TestQuery()
        => Endpoints.TestQuery.Execute();
        public IDbResponse TestNonQuery()
        => Endpoints.TestNonQuery.Execute();
        public IEnumerable<IDbResponse> TestExecuteGroup()
        {
            IDbCommandMethod[] methods = new IDbCommandMethod[] { Endpoints.TestFetchData, Endpoints.TestMultipleDataSets };
            return methods.ExecuteGroup();
        }
        public IEnumerable<IDbResponse> TestExecuteGroupWithConnectionError()
        {
            IDbCommandMethod[] methods = new IDbCommandMethod[] { Endpoints.TestBadConnStr, Endpoints.TestFetchData };
            return methods.ExecuteGroup();
        }

        public async Task<Tuple<IDbResponse, IEnumerable<FetchData>>> TestFetchDataWithOnBeforeAsync(CancellationToken cancellationToken, Func<Task> cb)
        => await Endpoints.TestFetchDataWithOnBeforeAsync.ExecuteAndFetchFirstAsync<FetchData, Func<Task>>(cancellationToken, cb);
        public async Task<Tuple<IDbResponse, IEnumerable<FetchData>>> TestFetchDataAsync()
        => await Endpoints.TestFetchData.ExecuteAndFetchFirstAsync<FetchData>();
        public async Task<Tuple<IDbResponse, IEnumerable<FetchData>>> TestFetchDataNoSchemaAsync()
        => await Endpoints.TestFetchDataNoSchema.ExecuteAndFetchFirstAsync<FetchData>();
        public async Task<Tuple<IDbResponse, IEnumerable<UserData>>> TestMultipleDataSetsAsync()
        => await Endpoints.TestMultipleDataSets.ExecuteAndFetchFirstAsync<UserData>();
        public async Task<Tuple<IDbResponse, IEnumerable<FetchDataAlt>>> TestFetchDataAltAsync()
        => await Endpoints.TestFetchDataAlt.ExecuteAndFetchFirstAsync<FetchDataAlt>((model, collection) => {
            model.MyEnum = collection.GetValue<FetchDataEnum>("MyEnumAlt");
            model.MyString = collection.GetValue<string>("MyStringAlt");
            model.MyChar = collection.GetValue<string>("MyCharAlt");
            model.Integer = collection.GetValue<int>("IntegerAlt");
            model.IntegerOptional = collection.GetValue<int?>("IntegerOptionalAlt");
            model.IntegerOptionalNull = collection.GetValue<int?>("IntegerOptionalAlt");
            model.PublicKey = collection.GetValue<Guid>("PublicKeyAlt");
            model.MyByte = collection.GetValue<byte>("MyByteAlt");
            model.MyByteAsInt = collection.GetValue<int>("MyByteAlt");
        });
        public async Task<Tuple<IDbResponse, IEnumerable<FetchDataAlt>>> TestFetchDataAltWithParamsAsync()
        => await Endpoints.TestFetchDataAltWithParams.ExecuteAndFetchFirstAsync<FetchDataAlt, UnitTestDbParams>(new UnitTestDbParams() { }, (model, collection) => {
            model.MyEnum = collection.GetValue<FetchDataEnum>("MyEnumAlt");
            model.MyString = collection.GetValue<string>("MyStringAlt");
            model.MyChar = collection.GetValue<string>("MyCharAlt");
            model.Integer = collection.GetValue<int>("IntegerAlt");
            model.IntegerOptional = collection.GetValue<int?>("IntegerOptionalAlt");
            model.IntegerOptionalNull = collection.GetValue<int?>("IntegerOptionalAlt");
            model.PublicKey = collection.GetValue<Guid>("PublicKeyAlt");
            model.MyByte = collection.GetValue<byte>("MyByteAlt");
            model.MyByteAsInt = collection.GetValue<int>("MyByteAlt");
        });
        public async Task<IDbResponse> TestMismatchParamsAsync()
        => await Endpoints.TestFetchDataAltWithParams.ExecuteAsync(Guid.NewGuid());
        public async Task<IDbResponse> TestMismatchParams2Async()
        => await Endpoints.TestParametersMismatch2.ExecuteAsync(Guid.NewGuid());
        public async Task<IDbResponse> TestMismatchValidationParamsAsync()
        => await Endpoints.TestTransaction.ExecuteAsync(Guid.NewGuid());
        public async Task<Tuple<IDbResponse, IEnumerable<FetchDataWithBadDbColumn>>> TestFetchDataWithBadDbColumnAsync()
        => await Endpoints.TestFetchDataWithBadDbColumn.ExecuteAndFetchFirstAsync<FetchDataWithBadDbColumn>();
        public async Task<Tuple<IDbResponse, IEnumerable<FetchDataWithNested>>> TestFetchDataWithNestedAsync()
        => await Endpoints.TestFetchDataWithNested.ExecuteAndFetchFirstAsync<FetchDataWithNested>();
        public async Task<Tuple<IDbResponse, IEnumerable<UserData>>> TestTransactionAsync(string value)
        => await Endpoints.TestTransaction.ExecuteAndFetchFirstAsync<UserData, UnitTestDbParams>(new UnitTestDbParams() { CustomString = value });
        public async Task<Tuple<IDbResponse, IEnumerable<FetchData>>> TestFetchDataWithOutputAsync()
        => await Endpoints.TestFetchDataWithOutput.ExecuteAndFetchFirstAsync<FetchData>();

        public async Task<IDbResponse> TestNoDataAsync()
        => await Endpoints.TestNoData.ExecuteAsync(Guid.NewGuid());
        public async Task<Tuple<IDbResponse, IEnumerable<TestClass2>>> TestBenchmarkAsync()
        => await Endpoints.TestBenchmark.ExecuteAndFetchFirstAsync<TestClass2>();

        public async Task<Tuple<IDbResponse, IEnumerable<TestClass2>>> TestBenchmark2Async()
        => await Endpoints.TestBenchmark.ExecuteAndFetchFirstAsync<TestClass2>((model, collection) => {
            model.String = collection.GetValue<string>("str");
        });
        public async Task<IDbResponse> TestExceptionAsync(Exception ex)
        => await Endpoints.TestException.ExecuteAsync(ex);
        public async Task<IDbResponse> TestParametersAsync()
        => await Endpoints.TestParameters.ExecuteAsync();

        public async Task<IDbResponse> TestRealAsync()
        => await Endpoints.TestReal.ExecuteAsync();

        public async Task<IDbResponse> TestBadConnStrAsync()
        => await Endpoints.TestBadConnStr.ExecuteAsync();
        public async Task<IDbResponse> TestValidationAsync()
        => await Endpoints.TestValidation.ExecuteAsync(new UnitTestParamsForValidation());
        public async Task<IDbResponse> TestValidationFailAsync()
        => await Endpoints.TestValidationFail.ExecuteAsync(new UnitTestParamsForValidation());
        public async Task<Tuple<IDbResponse, IEnumerable<FetchData>>> TestTableDirectAsync()
        => await Endpoints.TestTableDirect.ExecuteAndFetchFirstAsync<FetchData>();
        public async Task<IDbResponse> TestSqlCredendialsAsync()
        => await Endpoints.TestSqlCredendials.ExecuteAsync();
        public async Task<Tuple<IDbResponse, IEnumerable<FetchData>>> TestXmlAsync()
        => await Endpoints.TestXml.ExecuteAndFetchFirstAsync<FetchData>();

        public async Task<IDbResponse> TestScalarAsync()
        => await Endpoints.TestScalar.ExecuteAsync();

        public async Task<IDbResponse> TestMockException_ExecuteQueryAsync()
        => await Endpoints.TestMockException_ExecuteQuery.ExecuteAsync();
        public async Task<IDbResponse> TestMockException_ExecuteNonQueryAsync()
        => await Endpoints.TestMockException_ExecuteNonQuery.ExecuteAsync();
        public async Task<IDbResponse> TestMockException_ExecuteScalarAsync()
        => await Endpoints.TestMockException_ExecuteScalar.ExecuteAsync();
        public async Task<IDbResponse> TestMockException_ExecuteXmlAsync()
        => await Endpoints.TestMockException_ExecuteXml.ExecuteAsync();
        public async Task<IDbResponse> TestMockException_TransactionAsync()
        => await Endpoints.TestMockException_Transaction.ExecuteAsync();
        public async Task<IDbResponse> TestQueryAsync()
        => await Endpoints.TestQuery.ExecuteAsync();
        public async Task<IDbResponse> TestNonQueryAsync()
        => await Endpoints.TestNonQuery.ExecuteAsync();

        public async Task<IEnumerable<IDbResponse>> TestExecuteGroupAsync()
        {
            IDbCommandMethod[] methods = new IDbCommandMethod[] { Endpoints.TestFetchData, Endpoints.TestMultipleDataSets };
            return await methods.ExecuteGroupAsync();
        }
        public async Task<IEnumerable<IDbResponse>> TestExecuteGroupWithConnectionErrorAsync()
        {
            IDbCommandMethod[] methods = new IDbCommandMethod[] { Endpoints.TestBadConnStr, Endpoints.TestFetchData };
            return await methods.ExecuteGroupAsync();
        }

    }
}
