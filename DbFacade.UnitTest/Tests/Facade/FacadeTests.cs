using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using DbFacade.UnitTest.DataLayer.Models.Data;
using DbFacade.UnitTest.DataLayer.Models.Parameters;
using System.Net.Mail;
using System.Reflection;

namespace DbFacade.UnitTest.Tests.Facade
{
    public partial class FacadeTests : UnitTestBase
    {
        public FacadeTests(ServiceFixture services) : base(services) { }
        protected static void CheckEnumerable<T>(IEnumerable<T> expectedData, IEnumerable<T> data)
        {
            Assert.NotNull(data);
            Assert.Equal(expectedData.Count(), data.Count());
            Assert.True(expectedData.SequenceEqual(data));

        }
        
        protected static void ValidateEmail(MailAddress expectedEmail, MailAddress email)
        {
            Assert.NotNull(email);
            Assert.Equal(expectedEmail.Address, email.Address);
            Assert.Equal(expectedEmail.Host, email.Host);
            Assert.Equal(expectedEmail.User, email.User);
        }
        

        [Fact]
        public void SuccessfullyFetchesData()
        {
            Services.EndpointMockHelper.TestFetchData_EnableMockMode();
            var response = Services.DomainFacade.TestFetchData(out IEnumerable<FetchData> data);
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
        [Fact]
        public void SuccessfullyFetchesDataNoSchema()
        {
            Services.EndpointMockHelper.TestFetchDataNoSchema_EnableMockMode();
            var response = Services.DomainFacade.TestFetchDataNoSchema(out IEnumerable<FetchData> data);
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
        [Fact]
        public void SuccessfullyFetchesTestDataConsistantly()
        {
            Services.EndpointMockHelper.TestFetchData_EnableMockMode();
            int runForNTimes = 100;
            for (int i = 0; i < runForNTimes; i++)
            {
                var response = Services.DomainFacade.TestFetchData(out IEnumerable<FetchData> data);
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
        }

        [Fact]
        public void SuccessfullyFetchesMultipleDataSets()
        {
            Services.EndpointMockHelper.TestMultipleDataSets_EnableMockMode();
            var response = Services.DomainFacade.TestMultipleDataSets(out IEnumerable<UserData> data);
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
            var indexOutOfRangeData3 = response.ToDbDataModelList<UserRole>((m, c) => { }, - 1);
            Assert.Empty(indexOutOfRangeData3);
            var indexOutOfRangeData4 = response.ToDbDataModelList<UserRole>((m, c) => { }, 100);
            Assert.Empty(indexOutOfRangeData4);
        }
        [Fact]
        public void SuccessfullyFetchesDataAlt()
        {
            Services.EndpointMockHelper.TestFetchDataAlt_EnableMockMode();
            var response = Services.DomainFacade.TestFetchDataAlt(out IEnumerable<FetchDataAlt> data);
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchDataAlt model = data.First();
            Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
            Assert.Equal("test string", model.MyString);
            Assert.NotEqual(default, model.PublicKey);
        }
        [Fact]
        public void SuccessfullyFetchesDataAltWithParams()
        {
            Services.EndpointMockHelper.TestFetchDataAltWithParams_EnableMockMode();
            var response = Services.DomainFacade.TestFetchDataAltWithParams(out IEnumerable<FetchDataAlt> data);
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchDataAlt model = data.First();
            Assert.Equal(FetchDataEnum.Pass, model.MyEnum);
            Assert.Equal("test string", model.MyString);
            Assert.NotEqual(default, model.PublicKey);
        }
        [Fact]
        public void SuccessfullyCatchesMismatchParams()
        {
            Services.EndpointMockHelper.TestFetchDataAltWithParams_EnableMockMode();
            var response = Services.DomainFacade.TestMismatchParams();
            Type[] expectedTypes = new Type[] { typeof(UnitTestDbParams) };
            Assert.True(response.HasError);
            Assert.Equal(DbExecutionExceptionType.FacadeException, response.ErrorInfo.ExceptionType);
            Assert.Equal($"Unable to add parameters: expected model of type(s) {expectedTypes.TypeNames()} but got {typeof(Guid).TypeName()}", response.ErrorInfo.ErrorMessage);
            Assert.Equal("", response.ErrorInfo.ErrorDetails);
        }
        [Fact]
        public void SuccessfullyCatchesMismatchParams2()
        {
            Services.EndpointMockHelper.TestParametersMismatch_EnableMockMode();
            var response = Services.DomainFacade.TestMismatchParams2();
            Type[] expectedTypes = new Type[] { typeof(UnitTestDbParams) };
            Assert.True(response.HasError);
            Assert.Equal(DbExecutionExceptionType.FacadeException, response.ErrorInfo.ExceptionType);
            Assert.Equal($"Unable to add parameters: expected model of type(s) {expectedTypes.TypeNames()} but got {typeof(Guid).TypeName()}", response.ErrorInfo.ErrorMessage);
            Assert.Equal("", response.ErrorInfo.ErrorDetails);
        }
        [Fact]
        public void SuccessfullyCatchesMismatchValidationParams()
        {
            Services.EndpointMockHelper.TestTransaction_EnableMockMode();
            var response = Services.DomainFacade.TestMismatchValidationParams();
            Type[] expectedTypes = new Type[] { typeof(UnitTestDbParams) };
            Assert.True(response.HasError);
            Assert.Equal(DbExecutionExceptionType.ValidationError, response.ErrorInfo.ExceptionType);
            Assert.Equal("Guid values failed to pass validation for command 'TestTransaction'", response.ErrorInfo.ErrorMessage);
            Assert.Equal($"Unable to validate parameters: expected model of type(s) {expectedTypes.TypeNames()} but got {typeof(Guid).TypeName()}", response.ErrorInfo.ErrorDetails);
        }

        [Fact]
        public void SuccessfullyFetchesDataWithOutput()
        {
            Services.EndpointMockHelper.TestFetchDataWithOutput_EnableMockMode();
            var response = Services.DomainFacade.TestFetchDataWithOutput(out IEnumerable<FetchData> data);
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchData model = data.FirstOrDefault();
            Assert.Equal("test string", model.MyString);
            Assert.NotEqual(default, model.PublicKey);
            Assert.Equal("output response", response.GetOutputValue("MyStringOutputParam"));
        }
        [Fact]
        public void SuccessfullyFetchesDataAndIgnoresBadColumnName()
        {
            Services.EndpointMockHelper.TestFetchDataWithBadDbColumn_EnableMockMode();
            var response = Services.DomainFacade.TestFetchDataWithBadDbColumn(out IEnumerable<FetchDataWithBadDbColumn> data);
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchDataWithBadDbColumn model = data.FirstOrDefault();
            Assert.Null(model.MyString);
            Assert.Equal(default, model.Integer);
            Assert.Null(model.IntegerOptional);
            Assert.Null(model.IntegerOptionalNull);
            Assert.Null(model.MyString);
            Assert.Equal(default, model.PublicKey);
        }
        [Fact]
        public void SuccessfullyFetchesNestedData()
        {
            Services.EndpointMockHelper.TestFetchDataWithNested_EnableMockMode();
            var response = Services.DomainFacade.TestFetchDataWithNested(out IEnumerable<FetchDataWithNested> data);
            Assert.False(response.HasError);
            Assert.NotEmpty(data);
            FetchDataWithNested model = data.FirstOrDefault();
            ValidateEmail(new MailAddress("MyTestEmail@gmail.com"), model.EmailData.Email);
            CheckEnumerable(["1", "12", "123", "1234"], model.EnumerableData.Data);
            CheckEnumerable<short>([1, 12, 123, 1234], model.EnumerableData.ShortData);
            CheckEnumerable([1, 12, 123, 1234], model.EnumerableData.IntData);
            CheckEnumerable(new long[] { 1, 12, 123, 1234 }, model.EnumerableData.LongData);
            CheckEnumerable(new float[] { 1, 12, 123, 1234 }, model.EnumerableData.FloatData);
            CheckEnumerable(new double[] { 1, 12, 123, 1234 }, model.EnumerableData.DoubleData);
            CheckEnumerable(new decimal[] { 1, 12, 123, 1234 }, model.EnumerableData.DecimalData);
            CheckEnumerable(["1", "12", "123", "1234"], model.EnumerableData.DataCustom);
            Assert.True(model.FlagData.Flag);
            Assert.True(model.FlagData.FlagInt);
            Assert.False(model.FlagData.FlagFalse);
            Assert.False(model.FlagData.FlagIntFalse);

        }
        [Fact]
        public void SuccessfullyHandlesTransaction()
        {
            Services.EndpointMockHelper.TestTransaction_EnableMockMode();
            var response = Services.DomainFacade.TestTransaction("my param", out IEnumerable<UserData> data);
            Assert.False(response.HasError);
            Assert.Empty(data);
        }

        [Fact]
        public void CatchesValidationError()
        {
            Services.EndpointMockHelper.TestTransaction_EnableMockMode();
            var response = Services.DomainFacade.TestTransaction(null, out IEnumerable<UserData> data);
            Assert.True(response.HasError);
            Assert.Empty(data);
            ValidationException valEx = response.ErrorInfo.Error is ValidationException ex ? ex : null;
            Assert.NotNull(valEx);
            Assert.Single(valEx.ValidationErrors);
            string errorMessage = valEx.ValidationErrors.FirstOrDefault();
            Assert.NotNull(errorMessage);
            Assert.Equal("CustomString is required.", errorMessage);
        }

        [Fact]
        public void VerifyFetchCallAsTransaction()
        {
            Services.EndpointMockHelper.TestNoData_EnableMockMode();
            var response = Services.DomainFacade.TestNoData();
            Assert.False(response.HasError);
        }

        private class OutputModelTest : IDbDataModel
        {
            internal string AnsiString { get; private set; }
            internal string AnsiStringFixedLength { get; private set; }
            internal string StringFixedLength { get; private set; }
            internal decimal Currency { get; private set; }
            internal string NullVal { get; private set; }
            public object MyIndexRefValue { get; internal set; }
            public object OutOfRangeLowIndexRefValue { get; internal set; }
            public object OutOfRangeHighIndexRefValue { get; internal set; }
            public void Init(IDataCollection collection)
            {
                AnsiString = collection.GetValue<string>("AnsiString_OUT");
                AnsiStringFixedLength = collection.GetValue<string>("AnsiStringFixedLength_OUT");
                StringFixedLength = collection.GetValue<string>("StringFixedLength_OUT");
                Currency = collection.GetValue<decimal>("Currency_OUT");
                NullVal = collection.GetValue<string>("NULL_OUT");
                MyIndexRefValue = collection[2];
                OutOfRangeLowIndexRefValue = collection[-1];
                OutOfRangeHighIndexRefValue = collection[500];
            }
        }
        private class OutputModelThrowErrorTest : IDbDataModel
        {
            internal string AnsiString { get; set; }
            public void Init(IDataCollection collection)
            {
                AnsiString = collection.GetValue<string>("AnsiString_OUT");
                throw new Exception("testing bad model");
            }
        }
        [Fact]
        public void VerifyTestParameters()
        {
            Services.EndpointMockHelper.TestParameters_EnableMockMode();
            var response = Services.DomainFacade.TestParameters();
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
        }

        [Fact]
        public void VerifyTestRealReturnsError()
        {
            //Do Not Enable mock mode on this call 
            var response = Services.DomainFacade.TestReal();
            Assert.True(response.HasError);
            var errorInfo = response.ErrorInfo;
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, errorInfo.ExceptionType);
        }
        [Fact]
        public void VerifyTestBadConnStrReturnsError()
        {
            //Do Not Enable mock mode on this call
            var response = Services.DomainFacade.TestBadConnStr();
            Assert.True(response.HasError);
            var errorInfo = response.ErrorInfo;
            Assert.Equal(DbExecutionExceptionType.FacadeException, errorInfo.ExceptionType);
            Assert.Equal("Unable to create connection", errorInfo.ErrorMessage);
            Assert.Equal("No Connection String provided", errorInfo.ErrorDetails);
        }
        [Fact]
        public void VerifyTestValidationPassesValidation()
        {
            Services.EndpointMockHelper.TestValidation_EnableMockMode();
            var response = Services.DomainFacade.TestValidation();
            Assert.False(response.HasError);
        }
        [Fact]
        public void VerifyTestValidationFailsValidation()
        {
            Services.EndpointMockHelper.TestValidationFail_EnableMockMode();
            var response = Services.DomainFacade.TestValidationFail();
            Assert.True(response.HasError);
            int errorCount = response.ErrorInfo.Error is ValidationException ex ? ex.Count : 0;
            Assert.Equal(6, errorCount);
        }
        [Fact]
        public void VerifyTestTableDirect()
        {
            Services.EndpointMockHelper.TestTableDirect_EnableMockMode();
            var response = Services.DomainFacade.TestTableDirect(out IEnumerable<FetchData> data);
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
        }

        [Fact]
        public void VerifyTestSqlCredendials()
        {
            Services.EndpointMockHelper.TestSqlCredendials_EnableMockMode();
            var response = Services.DomainFacade.TestSqlCredendials();
            Assert.False(response.HasError);
            Assert.Equal(10, response.ReturnValue);
        }
        [Fact]
        public void VerifyTestXml()
        {
            Services.EndpointMockHelper.TestXml_EnableMockMode();
            var response = Services.DomainFacade.TestXml(out IEnumerable<FetchData> data);
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
        }
        [Fact]
        public void VerifyTestScalar()
        {
            Services.EndpointMockHelper.TestScalar_EnableMockMode();
            var response = Services.DomainFacade.TestScalar();
            Assert.False(response.HasError);
            Assert.Equal("MyTestScalarReturnValue", response.ScalarReturnValue);
        }
        [Fact]
        public void VerifyTestQuery()
        {
            Services.EndpointMockHelper.TestQuery_EnableMockMode();
            var response = Services.DomainFacade.TestQuery();
            Assert.False(response.HasError);
        }

        [Fact]
        public void VerifyTestNonQuery()
        {
            Services.EndpointMockHelper.TestNonQuery_EnableMockMode();
            var response = Services.DomainFacade.TestNonQuery();
            Assert.False(response.HasError);
        }

        [Fact]
        public void ExecutesGroupMethods()
        {
            Services.EndpointMockHelper.TestFetchData_EnableMockMode();
            Services.EndpointMockHelper.TestMultipleDataSets_EnableMockMode();
            var list = Services.DomainFacade.TestExecuteGroup();
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
        }

        [Fact]
        public void ExecutesGroupMethodsWithConnectionError()
        {
            Services.EndpointMockHelper.TestFetchData_EnableMockMode();
            var list = Services.DomainFacade.TestExecuteGroupWithConnectionError();
            Assert.NotEmpty(list);
            Assert.Single(list);

            var response = list.ElementAt(0);
            Assert.True(response.HasError);
            var errorInfo = response.ErrorInfo;
            Assert.Equal(DbExecutionExceptionType.FacadeException, errorInfo.ExceptionType);
            Assert.Equal("Unable to create connection", errorInfo.ErrorMessage);
            Assert.Equal("No Connection String provided", errorInfo.ErrorDetails);
        }
    }
}
