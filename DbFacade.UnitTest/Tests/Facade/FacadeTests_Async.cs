using DbFacade.DataLayer.ConnectionService;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using DbFacade.UnitTest.DataLayer.Models.Data;
using DbFacade.UnitTest.DataLayer.Models.Parameters;
using System.Net.Mail;

namespace DbFacade.UnitTest.Tests.Facade
{
    public partial class FacadeTests : UnitTestBase
    {
        protected static async Task CheckEnumerableAsync<T>(IEnumerable<T> expectedData, IEnumerable<T> data)
        {
            Assert.NotNull(data);
            Assert.Equal(expectedData.Count(), data.Count());
            Assert.True(expectedData.SequenceEqual(data));
            await Task.CompletedTask;

        }
        protected static async Task ValidateEmailAsync(MailAddress expectedEmail, MailAddress email)
        {
            Assert.NotNull(email);
            Assert.Equal(expectedEmail.Address, email.Address);
            Assert.Equal(expectedEmail.Host, email.Host);
            Assert.Equal(expectedEmail.User, email.User);

            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyFetchesDataWithCancealationTokenAsync()
        {
            Services.EndpointMockHelper.TestFetchDataWithOnBeforeAsync_EnableMockMode();
            CancellationTokenSource cts = new();
            Func<Task> testCancel = () => {
                cts.Cancel();
                cts.Token.ThrowIfCancellationRequested();
                return Task.CompletedTask;
            };
            var tupple = await Services.DomainFacade.TestFetchDataWithOnBeforeAsync(cts.Token, testCancel);
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.True(response.HasError);
            Assert.Empty(data);
            Assert.True(response.ErrorInfo.Error is OperationCanceledException);
            Assert.Equal("The operation was canceled.", response.ErrorInfo.ErrorMessage);
            Assert.Equal("", response.ErrorInfo.ErrorDetails);

            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyFetchesDataAsync()
        {
            Services.EndpointMockHelper.TestFetchData_EnableMockMode();
            
            var tupple = await Services.DomainFacade.TestFetchDataAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.Equal(11, response.ReturnValue);
            Assert.NotEmpty(data);
            FetchData model = data.First();
            Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
            Assert.Equal("test string", model.MyString);
            Assert.NotEqual(default, model.PublicKey);
            Assert.NotNull(model.MyIndexRefValue);
            Assert.Null(model.OutOfRangeHighIndexRefValue);
            Assert.Null(model.OutOfRangeLowIndexRefValue);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyFetchesDataNoSchemaAsync()
        {
            Services.EndpointMockHelper.TestFetchDataNoSchema_EnableMockMode();
            var tupple = await Services.DomainFacade.TestFetchDataNoSchemaAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.Equal(11, response.ReturnValue);
            Assert.NotEmpty(data);
            FetchData model = data.First();
            Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
            Assert.Equal("test string", model.MyString);
            Assert.NotEqual(default, model.PublicKey);
            Assert.NotNull(model.MyIndexRefValue);
            Assert.Null(model.OutOfRangeHighIndexRefValue);
            Assert.Null(model.OutOfRangeLowIndexRefValue);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyFetchesTestDataConsistantlyAsync()
        {
            Services.EndpointMockHelper.TestFetchData_EnableMockMode();
            int runForNTimes = 100;
            for (int i = 0; i < runForNTimes; i++)
            {
                var tupple = await Services.DomainFacade.TestFetchDataAsync();
                var response = tupple.Item1;
                var data = tupple.Item2;
                Assert.False(response.HasError);
                Assert.Equal(11, response.ReturnValue);
                Assert.NotEmpty(data);
                FetchData model = data.First();
                Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
                Assert.Equal("test string", model.MyString);
                Assert.NotEqual(default, model.PublicKey);
                Assert.NotNull(model.MyIndexRefValue);
                Assert.Null(model.OutOfRangeHighIndexRefValue);
                Assert.Null(model.OutOfRangeLowIndexRefValue);
            }
            await Task.CompletedTask;
        }

        [Fact]
        public async Task SuccessfullyFetchesMultipleDataSetsAsync()
        {
            Services.EndpointMockHelper.TestMultipleDataSets_EnableMockMode();
            var tupple = await Services.DomainFacade.TestMultipleDataSetsAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            Assert.Equal(2, data.Count());
            var user1 = data.ElementAt(0);
            var user2 = data.ElementAt(1);
            Assert.Equal(1, user1.Id);
            Assert.Equal("Test User", user1.Name);
            Assert.Equal(2, user2.Id);
            Assert.Equal("Other User", user2.Name);

            var roles = response.ToDbDataModelList<UserRole>(1);
            Assert.NotEmpty(roles);
            Assert.Equal(2, roles.Count());
            var role1 = roles.ElementAt(0);
            var role2 = roles.ElementAt(1);
            Assert.Equal("R1", role1.Value);
            Assert.Equal("Role One", role1.Name);
            Assert.Equal("R2", role2.Value);
            Assert.Equal("Role Two", role2.Name);

            var indexOutOfRangeData = response.ToDbDataModelList<UserRole>(-1);
            Assert.Empty(indexOutOfRangeData);
            var indexOutOfRangeData2 = response.ToDbDataModelList<UserRole>(100);
            Assert.Empty(indexOutOfRangeData2);
            var indexOutOfRangeData3 = response.ToDbDataModelList<UserRole>((m, c) => { }, -1);
            Assert.Empty(indexOutOfRangeData3);
            var indexOutOfRangeData4 = response.ToDbDataModelList<UserRole>((m, c) => { }, 100);
            Assert.Empty(indexOutOfRangeData4);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyFetchesDataAltAsync()
        {
            Services.EndpointMockHelper.TestFetchDataAlt_EnableMockMode();
            var tupple = await Services.DomainFacade.TestFetchDataAltAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchDataAlt model = data.First();
            Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
            Assert.Equal("test string", model.MyString);
            Assert.NotEqual(default, model.PublicKey);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyFetchesDataAltWithParamsAsync()
        {
            Services.EndpointMockHelper.TestFetchDataAltWithParams_EnableMockMode();
            var tupple = await Services.DomainFacade.TestFetchDataAltWithParamsAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchDataAlt model = data.First();
            Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
            Assert.Equal("test string", model.MyString);
            Assert.NotEqual(default, model.PublicKey);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyCatchesMismatchParamsAsync()
        {
            Services.EndpointMockHelper.TestFetchDataAltWithParams_EnableMockMode();
            var response = await Services.DomainFacade.TestMismatchParamsAsync();
            Type[] expectedTypes = new Type[] { typeof(UnitTestDbParams) };
            Assert.True(response.HasError);
            Assert.Equal(DbExecutionExceptionType.FacadeException, response.ErrorInfo.ExceptionType);
            Assert.Equal($"Unable to add parameters: expected model of type(s) {expectedTypes.TypeNames()} but got {typeof(Guid).TypeName()}", response.ErrorInfo.ErrorMessage);
            Assert.Equal("", response.ErrorInfo.ErrorDetails);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyCatchesMismatchParams2Async()
        {
            Services.EndpointMockHelper.TestParametersMismatch_EnableMockMode();
            var response = await Services.DomainFacade.TestMismatchParams2Async();
            Type[] expectedTypes = new Type[] { typeof(UnitTestDbParams) };
            Assert.True(response.HasError);
            Assert.Equal(DbExecutionExceptionType.FacadeException, response.ErrorInfo.ExceptionType);
            Assert.Equal($"Unable to add parameters: expected model of type(s) {expectedTypes.TypeNames()} but got {typeof(Guid).TypeName()}", response.ErrorInfo.ErrorMessage);
            Assert.Equal("", response.ErrorInfo.ErrorDetails);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyCatchesMismatchValidationParamsAsync()
        {
            Services.EndpointMockHelper.TestTransaction_EnableMockMode();
            var response = await Services.DomainFacade.TestMismatchValidationParamsAsync();
            Type[] expectedTypes = new Type[] { typeof(UnitTestDbParams) };
            Assert.True(response.HasError);
            Assert.Equal(DbExecutionExceptionType.ValidationError, response.ErrorInfo.ExceptionType);
            Assert.Equal("Guid values failed to pass validation for command 'TestTransaction'", response.ErrorInfo.ErrorMessage);
            Assert.Equal($"Unable to validate parameters: expected model of type(s) {expectedTypes.TypeNames()} but got {typeof(Guid).TypeName()}", response.ErrorInfo.ErrorDetails);
            await Task.CompletedTask;
        }


        [Fact]
        public async Task SuccessfullyFetchesDataWithOutputAsync()
        {
            Services.EndpointMockHelper.TestFetchDataWithOutput_EnableMockMode();
            var tupple = await Services.DomainFacade.TestFetchDataWithOutputAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchData model = data.FirstOrDefault();
            Assert.Equal("test string", model.MyString);
            Assert.NotEqual(default, model.PublicKey);
            Assert.Equal(response.GetOutputValue("MyStringOutputParam"), "output response");
            Assert.Equal(response.GetOutputValue("MyStringOutputParam"), "output response");
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyFetchesDataAndIgnoresBadColumnNameAsync()
        {
            Services.EndpointMockHelper.TestFetchDataWithBadDbColumn_EnableMockMode();
            var tupple = await Services.DomainFacade.TestFetchDataWithBadDbColumnAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchDataWithBadDbColumn model = data.FirstOrDefault();
            Assert.Null(model.MyString);
            Assert.Equal(default, model.Integer);
            Assert.Null(model.IntegerOptional);
            Assert.Null(model.IntegerOptionalNull);
            Assert.Null(model.MyString);
            Assert.Equal(default, model.PublicKey);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyFetchesNestedDataAsync()
        {
            Services.EndpointMockHelper.TestFetchDataWithNested_EnableMockMode();
            var tupple = await Services.DomainFacade.TestFetchDataWithNestedAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchDataWithNested model = data.FirstOrDefault();
            await ValidateEmailAsync(new MailAddress("MyTestEmail@gmail.com"), model.EmailData.Email);
            await CheckEnumerableAsync(["1", "12", "123", "1234"], model.EnumerableData.Data);
            await CheckEnumerableAsync<short>([1, 12, 123, 1234], model.EnumerableData.ShortData);
            await CheckEnumerableAsync([1, 12, 123, 1234], model.EnumerableData.IntData);
            await CheckEnumerableAsync(new long[] { 1, 12, 123, 1234 }, model.EnumerableData.LongData);
            await CheckEnumerableAsync(new float[] { 1, 12, 123, 1234 }, model.EnumerableData.FloatData);
            await CheckEnumerableAsync(new double[] { 1, 12, 123, 1234 }, model.EnumerableData.DoubleData);
            await CheckEnumerableAsync(new decimal[] { 1, 12, 123, 1234 }, model.EnumerableData.DecimalData);
            await CheckEnumerableAsync(["1", "12", "123", "1234"], model.EnumerableData.DataCustom);
            Assert.True(model.FlagData.Flag);
            Assert.True(model.FlagData.FlagInt);
            Assert.False(model.FlagData.FlagFalse);
            Assert.False(model.FlagData.FlagIntFalse);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task SuccessfullyHandlesTransactionAsync()
        {
            Services.EndpointMockHelper.TestTransaction_EnableMockMode();
            var tupple = await Services.DomainFacade.TestTransactionAsync("my param");
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.Empty(data);
            await Task.CompletedTask;
        }
        
        [Fact]
        public async Task CatchesValidationErrorAsync()
        {
            Services.EndpointMockHelper.TestTransaction_EnableMockMode();
            var tupple = await Services.DomainFacade.TestTransactionAsync(null);
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.True(response.HasError);
            Assert.Empty(data);
            ValidationException valEx = response.ErrorInfo.Error is ValidationException ex ? ex : null;
            Assert.NotNull(valEx);
            Assert.Single(valEx.ValidationErrors);
            string errorMessage = valEx.ValidationErrors.FirstOrDefault();
            Assert.NotNull(errorMessage);
            Assert.Equal("CustomString is required.", errorMessage);
            await Task.CompletedTask;
        }

        [Fact]
        public async Task VerifyFetchCallAsTransactionAsync()
        {
            Services.EndpointMockHelper.TestNoData_EnableMockMode();
            var response = await Services.DomainFacade.TestNoDataAsync();
            Assert.False(response.HasError);
            await Task.CompletedTask;
        }

        [Fact]
        public async Task VerifyTestParametersAsync()
        {
            Services.EndpointMockHelper.TestParameters_EnableMockMode();
            var response = await Services.DomainFacade.TestParametersAsync();
            Assert.False(response.HasError);
            Assert.Equal("AnsiString", response.GetOutputValue("AnsiString_OUT"));
            Assert.Equal("AnsiStringFixedLength", response.GetOutputValue("AnsiStringFixedLength_OUT"));
            Assert.Equal("StringFixedLength", response.GetOutputValue("StringFixedLength_OUT"));
            Assert.Equal(123.34m, response.GetOutputValue("Currency_OUT"));
            Assert.Equal(123.34m, response.GetOutputValue<decimal>("Currency_OUT"));

            Assert.Equal("AnsiString", response.GetOutputValue("AnsiString_IN_OUT").ToString().Trim());
            Assert.Equal("AnsiStringFixedLength", response.GetOutputValue("AnsiStringFixedLength_IN_OUT").ToString().Trim());
            Assert.Equal("StringFixedLength", response.GetOutputValue("StringFixedLength_IN_OUT").ToString().Trim());
            var cur = response.GetOutputValue("Currency_IN_OUT");
            Assert.Equal(123.34m, response.GetOutputValue("Currency_IN_OUT"));

            OutputModelTest outputModel = response.GetOutputModel<OutputModelTest>();
            Assert.Equal("AnsiString", outputModel.AnsiString);
            Assert.Equal("AnsiStringFixedLength", outputModel.AnsiStringFixedLength);
            Assert.Equal("StringFixedLength", outputModel.StringFixedLength);
            Assert.Equal(123.34m, outputModel.Currency);
            Assert.Null(outputModel.NullVal);
            Assert.NotNull(outputModel.MyIndexRefValue);
            Assert.Null(outputModel.OutOfRangeHighIndexRefValue);
            Assert.Null(outputModel.OutOfRangeLowIndexRefValue);

            OutputModelThrowErrorTest outputModel2 = response.GetOutputModel<OutputModelThrowErrorTest>();
            Assert.Equal(default(OutputModelThrowErrorTest), outputModel2);

            OutputModelThrowErrorTest outputModel3 = response.GetOutputModel<OutputModelThrowErrorTest>((m, c) => {
                m.AnsiString = c.GetValue<string>("AnsiString_OUT");
                throw new Exception("testing bad model");
            });
            Assert.Equal(default(OutputModelThrowErrorTest), outputModel3);

            await Task.CompletedTask;
        }

        [Fact]
        public async Task VerifyTestRealReturnsErrorAsync()
        {
            //Do Not Enable mock mode on this call 
            var response = await Services.DomainFacade.TestRealAsync();
            Assert.True(response.HasError);
            var errorInfo = response.ErrorInfo;
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, errorInfo.ExceptionType);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task VerifyTestBadConnStrReturnsErrorAsync()
        {
            //Do Not Enable mock mode on this call
            var response = await Services.DomainFacade.TestBadConnStrAsync();
            Assert.True(response.HasError);
            var errorInfo = response.ErrorInfo;
            Assert.Equal(DbExecutionExceptionType.FacadeException, errorInfo.ExceptionType);
            Assert.Equal("Unable to create connection", errorInfo.ErrorMessage);
            Assert.Equal("No Connection String provided", errorInfo.ErrorDetails);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task VerifyTestValidationPassesValidationAsync()
        {
            Services.EndpointMockHelper.TestValidation_EnableMockMode();
            var response = await Services.DomainFacade.TestValidationAsync();
            Assert.False(response.HasError);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task VerifyTestValidationFailsValidationAsync()
        {
            Services.EndpointMockHelper.TestValidationFail_EnableMockMode();
            var response = await Services.DomainFacade.TestValidationFailAsync();
            Assert.True(response.HasError);
            int errorCount = response.ErrorInfo.Error is ValidationException ex ? ex.Count : 0;
            Assert.Equal(6, errorCount);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task VerifyTestTableDirectAsync()
        {
            Services.EndpointMockHelper.TestTableDirect_EnableMockMode();
            var tupple = await Services.DomainFacade.TestTableDirectAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.NotEmpty(data);

            Assert.Equal(3, data.Count());

            FetchData model = data.First();
            Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
            Assert.Equal("test string", model.MyString);
            Assert.Equal(5, model.Integer);

            FetchData model2 = data.ElementAt(1);
            Assert.Equal(FetchDataEnum.Fail, model2.MyEnum);
            Assert.Equal("test string 2", model2.MyString);
            Assert.Equal(10, model2.Integer);

            FetchData model3 = data.ElementAt(2);
            Assert.Equal(FetchDataEnum.Pass, model3.MyEnum);
            Assert.Equal("test string 3", model3.MyString);
            Assert.Equal(15, model3.Integer);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task VerifyTestSqlCredendialsAsync()
        {
            Services.EndpointMockHelper.TestSqlCredendials_EnableMockMode();
            var response = await Services.DomainFacade.TestSqlCredendialsAsync();
            Assert.False(response.HasError);
            Assert.Equal(10, response.ReturnValue);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task VerifyTestXmlAsync()
        {
            Services.EndpointMockHelper.TestXml_EnableMockMode();
            var tupple = await Services.DomainFacade.TestXmlAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
            Assert.False(response.HasError);
            Assert.NotEmpty(data);

            Assert.Equal(3, data.Count());

            FetchData model = data.First();
            Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
            Assert.Equal("test string", model.MyString);
            Assert.Equal(5, model.Integer);

            FetchData model2 = data.ElementAt(1);
            Assert.Equal(FetchDataEnum.Fail, model2.MyEnum);
            Assert.Equal("test string 2", model2.MyString);
            Assert.Equal(10, model2.Integer);

            FetchData model3 = data.ElementAt(2);
            Assert.Equal(FetchDataEnum.Pass, model3.MyEnum);
            Assert.Equal("test string 3", model3.MyString);
            Assert.Equal(15, model3.Integer);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task VerifyTestScalarAsync()
        {
            Services.EndpointMockHelper.TestScalar_EnableMockMode();
            var response = await Services.DomainFacade.TestScalarAsync();
            Assert.False(response.HasError);
            Assert.Equal("MyTestScalarReturnValue", response.ScalarReturnValue);
            await Task.CompletedTask;
        }

        [Fact]
        public async Task VerifyTestQueryAsync()
        {
            Services.EndpointMockHelper.TestQuery_EnableMockMode();
            var response = await Services.DomainFacade.TestQueryAsync();
            Assert.False(response.HasError);
            await Task.CompletedTask;
        }

        [Fact]
        public async Task ExecutesGroupMethodsAsync()
        {
            Services.EndpointMockHelper.TestFetchData_EnableMockMode();
            Services.EndpointMockHelper.TestMultipleDataSets_EnableMockMode();
            var list = await Services.DomainFacade.TestExecuteGroupAsync();
            Assert.NotEmpty(list);
            Assert.Equal(2, list.Count());

            var responseFirst = list.ElementAt(0);
            var dataFirst = responseFirst.ToDbDataModelList<FetchData>();
            Assert.False(responseFirst.HasError);
            Assert.Equal(11, responseFirst.ReturnValue);
            Assert.NotEmpty(dataFirst);
            FetchData model = dataFirst.First();
            Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
            Assert.Equal("test string", model.MyString);
            Assert.NotEqual(default, model.PublicKey);
            Assert.NotNull(model.MyIndexRefValue);
            Assert.Null(model.OutOfRangeHighIndexRefValue);
            Assert.Null(model.OutOfRangeLowIndexRefValue);


            var responseSecond = list.ElementAt(1);
            var dataSecond = responseSecond.ToDbDataModelList<UserData>();
            Assert.False(responseSecond.HasError);
            Assert.NotEmpty(dataSecond);
            Assert.Equal(2, dataSecond.Count());
            var user1 = dataSecond.ElementAt(0);
            var user2 = dataSecond.ElementAt(1);
            Assert.Equal(1, user1.Id);
            Assert.Equal("Test User", user1.Name);
            Assert.Equal(2, user2.Id);
            Assert.Equal("Other User", user2.Name);

            var roles = responseSecond.ToDbDataModelList<UserRole>(1);
            Assert.NotEmpty(roles);
            Assert.Equal(2, roles.Count());
            var role1 = roles.ElementAt(0);
            var role2 = roles.ElementAt(1);
            Assert.Equal("R1", role1.Value);
            Assert.Equal("Role One", role1.Name);
            Assert.Equal("R2", role2.Value);
            Assert.Equal("Role Two", role2.Name);

            var indexOutOfRangeData = responseSecond.ToDbDataModelList<UserRole>(-1);
            Assert.Empty(indexOutOfRangeData);
            var indexOutOfRangeData2 = responseSecond.ToDbDataModelList<UserRole>(100);
            Assert.Empty(indexOutOfRangeData2);
            var indexOutOfRangeData3 = responseSecond.ToDbDataModelList<UserRole>((m, c) => { }, -1);
            Assert.Empty(indexOutOfRangeData3);
            var indexOutOfRangeData4 = responseSecond.ToDbDataModelList<UserRole>((m, c) => { }, 100);
            Assert.Empty(indexOutOfRangeData4);
            await Task.CompletedTask;
        }

        [Fact]
        public async Task ExecutesGroupMethodsWithConnectionErrorAsync()
        {
            Services.EndpointMockHelper.TestFetchData_EnableMockMode();
            var list = await Services.DomainFacade.TestExecuteGroupWithConnectionErrorAsync();
            Assert.NotEmpty(list);
            Assert.Single(list);

            var response = list.ElementAt(0);
            Assert.True(response.HasError);
            var errorInfo = response.ErrorInfo;
            Assert.Equal(DbExecutionExceptionType.FacadeException, errorInfo.ExceptionType);
            Assert.Equal("Unable to create connection", errorInfo.ErrorMessage);
            Assert.Equal("No Connection String provided", errorInfo.ErrorDetails);
            await Task.CompletedTask;

        }
    }
}
