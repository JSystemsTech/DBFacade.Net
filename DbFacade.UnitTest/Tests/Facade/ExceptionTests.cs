using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using DbFacade.UnitTest.DataLayer.Models.Data;
using DbFacade.UnitTest.Services;
using System.Data.Common;
using Xunit.Sdk;

namespace DbFacade.UnitTest.Tests.Facade
{
    public partial class ExceptionTests : UnitTestBase
    {
        private class CustomErrorData : IDbDataModel
        {
            public bool IsCustom { get; private set; }
            public string Message { get; private set; }
            public DateTime LogDate { get; private set; }
            public void Init(IDataCollection collection)
            {
                IsCustom = collection.GetValue<bool>("IsCustom");
                Message = collection.GetValue<string>("Message");
                LogDate = collection.GetValue<DateTime>("LogDate");
            }
        }
        private class TestParameters
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
        public ExceptionTests(ServiceFixture services) : base(services) { }
        private void TestException(Exception ex, Action<IDbResponse> validate)
        {
            Services.EndpointMockHelper.TestException_EnableMockMode();            
            var expectedDbExecutionExceptionType =
                ex is ValidationException ? DbExecutionExceptionType.ValidationError :
                ex is DbExecutionException ? DbExecutionExceptionType.DbExecutionError :
                ex is FacadeException ? DbExecutionExceptionType.FacadeException : 
                DbExecutionExceptionType.Error;
            var response = Services.DomainFacade.TestException(ex);
            var data = response.ErrorInfo.ErrorData.ToDbDataModel<CustomErrorData>();

            Assert.True(response.HasError);
            Assert.Equal(expectedDbExecutionExceptionType, response.ErrorInfo.ExceptionType);
            Assert.True(data.IsCustom);
            Assert.Equal("custom message", data.Message);
            Assert.Equal("01/01/2025", data.LogDate.ToString("dd/MM/yyyy"));
            validate(response);
        }
        private const string ValidationErrorMessage = "testing validation error";
        private const string DbExecutionErrorMessage = "testing db execution error";
        private const string FacadeErrorMessage = "testing facade error";
        private const string GenericErrorMessage = "testing generic error";
        private const string InnerErrorMessage = "inner exception error";
        private static string[] ValidationErrors = new string[] { "validation error 1", "validation error 2", "validation error 3" };
        private static TestParameters TestParams = new TestParameters() { Name = "Test", Value = "Params" };

        [Fact]
        public void TestValidationException() => TestException(
            new ValidationException(ValidationErrors, TestParams, ValidationErrorMessage), 
            response => {
                Assert.True(response.ErrorInfo.Error is ValidationException);
                ValidationException ex = response.ErrorInfo.Error is ValidationException valEx ? valEx : null;
                Assert.Equal(ValidationErrorMessage, response.ErrorInfo.ErrorMessage);
                Assert.Equal($"{ValidationErrors.ElementAt(0)} {ValidationErrors.ElementAt(1)} {ValidationErrors.ElementAt(2)}", response.ErrorInfo.ErrorDetails);

                Assert.True(ex.Parameters is TestParameters);
                TestParameters actualTestParams = ex.Parameters is TestParameters tp ? tp : null;

                Assert.Equal(TestParams.Name, actualTestParams.Name);
                Assert.Equal(TestParams.Value, actualTestParams.Value);
                Assert.Equal("TestException", response.ErrorInfo.EndpointSettings.Name);
                Assert.Equal(ConnectionStringIds.SQLUnitTest, response.ErrorInfo.EndpointSettings.ConnectionStringId);
            });
        [Fact]
        public void TestDbExecutionException() => TestException(new DbExecutionException(DbExecutionErrorMessage, new Exception(InnerErrorMessage)),
            response => {
                Assert.True(response.ErrorInfo.Error is DbExecutionException);
                DbExecutionException ex = response.ErrorInfo.Error is DbExecutionException valEx ? valEx : null;
                Assert.Equal(DbExecutionErrorMessage, response.ErrorInfo.ErrorMessage);
                Assert.Equal(InnerErrorMessage, response.ErrorInfo.ErrorDetails);
            });
        [Fact]
        public void TestFacadeException() => TestException(new FacadeException(FacadeErrorMessage, new Exception(InnerErrorMessage)),
            response => {
                Assert.True(response.ErrorInfo.Error is FacadeException);
                FacadeException ex = response.ErrorInfo.Error is FacadeException valEx ? valEx : null;
                Assert.Equal(FacadeErrorMessage, response.ErrorInfo.ErrorMessage);
                Assert.Equal(InnerErrorMessage, response.ErrorInfo.ErrorDetails);
            });
        [Fact]
        public void TestGenericException() => TestException(new Exception(GenericErrorMessage, new Exception(InnerErrorMessage)),
            response => {
                Exception ex = response.ErrorInfo.Error;
                Assert.Equal(GenericErrorMessage, response.ErrorInfo.ErrorMessage);
                Assert.Equal(InnerErrorMessage, response.ErrorInfo.ErrorDetails);
                Assert.Equal(GenericErrorMessage, response.ErrorInfo.ErrorMessage);
            });
        [Fact]
        public void TestFacadeExceptionBase()
        {
            var ex = new FacadeException("test");
            Assert.Equal("test", ex.Message);
            Assert.Equal("", ex.ErrorDetails);

            var innerEx = new Exception("inner");
            var ex2 = new FacadeException("test", innerEx);
            Assert.Equal("test", ex2.Message);
            Assert.Equal("inner", ex2.ErrorDetails);
        }

        public class TestDbException : DbException
        {
            public TestDbException(string message):base(message) { }
        }
        [Fact]
        public void TestErrorHelper_ThrowDbExecutionException()
        {
            var innerException = new TestDbException("Testing DbException");
            string message = "Testing ThrowDbExecutionException";
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowDbExecutionException(message, innerException);
            }
            catch(DbExecutionException ex)
            {
                threwException = true;
                Assert.Equal(message, ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Fact]
        public void TestErrorHelper_ThrowFacadeException()
        {
            string message = "Testing ThrowFacadeException";
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowFacadeException(message);
            }
            catch (FacadeException ex)
            {
                threwException = true;
                Assert.Equal(message, ex.Message);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Fact]
        public void TestErrorHelper_ThrowFacadeException_WithInnerException()
        {
            var innerException = new TestDbException("Testing DbException");
            string message = "Testing ThrowFacadeException";
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowFacadeException(message, innerException);
            }
            catch (FacadeException ex)
            {
                threwException = true;
                Assert.Equal(message, ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Theory]
        [InlineData("Unable to execute non query", true)]
        [InlineData("Unknown Error while attempting to execute non query", false)]
        public void TestErrorHelper_ThrowOnExecuteNonQueryError(string expectedMessage, bool useDbException)
        {
            var innerException = useDbException ? new TestDbException("Testing DbException") : new Exception("Testing Exception");
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowOnExecuteNonQueryError(innerException);
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal(expectedMessage, ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Fact]
        public void TestErrorHelper_ThrowOnRollbackTransactionError()
        {
            var innerException = new Exception("Testing Exception");
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowOnRollbackTransactionError(innerException);
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal("Unable to rollback transaction", ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Fact]
        public void TestErrorHelper_ThrowOnCreateTransactionError()
        {
            var innerException = new Exception("Testing Exception");
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowOnCreateTransactionError(innerException);
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal("Unable to create transaction", ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Fact]
        public void TestErrorHelper_ThrowInvalidTransactionError()
        {
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowInvalidTransactionError();
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal("Invalid Transaction Definition", ex.Message);
            }
            finally
            {
                Assert.True(threwException);
            }
        }


        [Theory]
        [InlineData("Unable to execute query", true)]
        [InlineData("Unknown Error while attempting to execute query", false)]
        public void TestErrorHelper_ThrowOnExecuteQueryError(string expectedMessage, bool useDbException)
        {
            var innerException = useDbException ? new TestDbException("Testing DbException") : new Exception("Testing Exception");
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowOnExecuteQueryError(innerException);
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal(expectedMessage, ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }

        [Theory]
        [InlineData("Unable to execute xml query", true)]
        [InlineData("Unknown Error while attempting to execute xml query", false)]
        public void TestErrorHelper_ThrowOnExecuteXmlError(string expectedMessage, bool useDbException)
        {
            var innerException = useDbException ? new TestDbException("Testing DbException") : new Exception("Testing Exception");
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowOnExecuteXmlError(innerException);
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal(expectedMessage, ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Theory]
        [InlineData("Unable to execute scalar", true)]
        [InlineData("Unknown Error while attempting to execute scalar", false)]
        public void TestErrorHelper_ThrowOnExecuteScalarError(string expectedMessage, bool useDbException)
        {
            var innerException = useDbException ? new TestDbException("Testing DbException") : new Exception("Testing Exception");
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowOnExecuteScalarError(innerException);
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal(expectedMessage, ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }

        [Fact]
        public void TestErrorHelper_ThrowInvalidConnectionError()
        {
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowInvalidConnectionError();
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal("Invalid Connection Definition", ex.Message);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Fact]
        public void TestErrorHelper_ThrowUnableToOpenConnectionError()
        {
            var innerException = new Exception("Testing Exception");
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowUnableToOpenConnectionError(innerException);
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal("Unable to open connection", ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Fact]
        public void TestErrorHelper_ThrowUnableToCreateConnectionError()
        {
            var innerException = new Exception("Testing Exception");
            bool threwException = false;
            try
            {
                ErrorHelper.ThrowUnableToCreateConnectionError(innerException);
            }
            catch (Exception ex)
            {
                threwException = true;
                Assert.Equal("Unable to create connection", ex.Message);
                Assert.Equal(innerException, ex.InnerException);
            }
            finally
            {
                Assert.True(threwException);
            }
        }
        [Theory]
        [InlineData("Test Mock Exception", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        public void TestErrorHelper_CheckThrowMockException(string expectedMessage, bool throwException)
        {
            bool threwException = false;
            try
            {
                ErrorHelper.CheckThrowMockException(expectedMessage);
            }
            catch (MockDbException ex)
            {
                threwException = true;
                Assert.Equal(expectedMessage, ex.Message);
            }
            finally
            {
                Assert.Equal(throwException, threwException);
            }
        }


        [Fact]
        public void TestErrorHelper_TestMockException_ExecuteQuery()
        {
            string innerExceptionMessage = "Test ExecuteQuery Error";
            Services.EndpointMockHelper.TestMockException_ExecuteQuery_EnableMockMode(o => {
                o.ExecuteQueryError = innerExceptionMessage;
            });
            var response = Services.DomainFacade.TestMockException_ExecuteQuery();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute query", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
        }
        [Fact]
        public void TestErrorHelper_TestMockException_ExecuteNonQuery()
        {
            string innerExceptionMessage = "Test ExecuteNonQuery Error";
            Services.EndpointMockHelper.TestMockException_ExecuteNonQuery_EnableMockMode(o => {
                o.ExecuteNonQueryError = innerExceptionMessage;
            });
            var response = Services.DomainFacade.TestMockException_ExecuteNonQuery();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute non query", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
        }

        [Fact]
        public void TestErrorHelper_TestMockException_ExecuteXml()
        {
            string innerExceptionMessage = "Test ExecuteXml Error";
            Services.EndpointMockHelper.TestMockException_ExecuteXml_EnableMockMode(o => {
                o.ExecuteXmlError = innerExceptionMessage;
            });
            var response = Services.DomainFacade.TestMockException_ExecuteXml();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute xml query", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
        }

        [Fact]
        public void TestErrorHelper_TestMockException_ExecuteScalar()
        {
            string innerExceptionMessage = "Test ExecuteScalar Error";
            Services.EndpointMockHelper.TestMockException_ExecuteScalar_EnableMockMode(o => {
                o.ExecuteScalarError = innerExceptionMessage;
            });
            var response = Services.DomainFacade.TestMockException_ExecuteScalar();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute scalar", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
        }

        [Fact]
        public void TestErrorHelper_TestMockException_Transaction()
        {
            string innerExceptionMessage = "Test Transaction Error";
            Services.EndpointMockHelper.TestMockException_Transaction_EnableMockMode(o => {
                o.ExecuteNonQueryError = innerExceptionMessage;
            });
            var response = Services.DomainFacade.TestMockException_Transaction();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute non query", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
        }

        [Fact]
        public void TestErrorHelper_TestMockException_BeginTransaction()
        {
            string innerExceptionMessage = "Test Transaction Error";
            Services.EndpointMockHelper.TestMockException_Transaction_EnableMockMode(o => {
                o.BeginTransactionError = innerExceptionMessage;
            });
            var response = Services.DomainFacade.TestMockException_Transaction();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to create transaction", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
        }
        [Fact]
        public void TestErrorHelper_TestMockException_Transaction_Null()
        {
            Services.EndpointMockHelper.TestMockException_Transaction_EnableMockMode(o => {
                o.UseNullTransaction = true;
            });
            var response = Services.DomainFacade.TestMockException_Transaction();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Invalid Transaction Definition", info.ErrorMessage);
            Assert.Equal("", info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.FacadeException, info.ExceptionType);
        }
        [Fact]
        public void TestErrorHelper_TestMockException_Transaction_Rollback()
        {
            string innerExceptionMessage = "Test Transaction Error";
            Services.EndpointMockHelper.TestMockException_Transaction_EnableMockMode(o => {
                o.ExecuteNonQueryError = "some error";
                o.TransactionRollbackError = innerExceptionMessage;
            });
            var response = Services.DomainFacade.TestMockException_Transaction();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to rollback transaction", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
        }

        [Fact]
        public void TestErrorHelper_TestMockException_ConnectionCreation()
        {
            string innerExceptionMessage = "Test ConnectionCreation Error";
            Services.EndpointMockHelper.TestMockException_ExecuteNonQuery_EnableMockMode(o => {
                o.ConnectionCreationError = innerExceptionMessage;
            });
            var response = Services.DomainFacade.TestMockException_ExecuteNonQuery();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to create connection", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.FacadeException, info.ExceptionType);
        }
        [Fact]
        public void TestErrorHelper_TestMockException_NullConnection()
        {
            Services.EndpointMockHelper.TestMockException_ExecuteNonQuery_EnableMockMode(o => {
                o.UseNullConnection = true;
            });
            var response = Services.DomainFacade.TestMockException_ExecuteNonQuery();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Invalid Connection Definition", info.ErrorMessage);
            Assert.Equal(DbExecutionExceptionType.FacadeException, info.ExceptionType);
        }
        [Fact]
        public void TestErrorHelper_TestMockException_ConnectionOpen()
        {
            string innerExceptionMessage = "Test ConnectionOpen Error";
            Services.EndpointMockHelper.TestMockException_ExecuteNonQuery_EnableMockMode(o => {
                o.ConnectionOpenError = innerExceptionMessage;
            });
            var response = Services.DomainFacade.TestMockException_ExecuteNonQuery();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to open connection", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
        }
        #region Async
        [Fact]
        public async Task TestErrorHelper_TestMockException_ExecuteQueryAsync()
        {
            string innerExceptionMessage = "Test ExecuteQuery Error";
            Services.EndpointMockHelper.TestMockException_ExecuteQuery_EnableMockMode(o => {
                o.ExecuteQueryError = innerExceptionMessage;
            });
            var response = await Services.DomainFacade.TestMockException_ExecuteQueryAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute query", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task TestErrorHelper_TestMockException_ExecuteNonQueryAsync()
        {
            string innerExceptionMessage = "Test ExecuteNonQuery Error";
            Services.EndpointMockHelper.TestMockException_ExecuteNonQuery_EnableMockMode(o => {
                o.ExecuteNonQueryError = innerExceptionMessage;
            });
            var response = await Services.DomainFacade.TestMockException_ExecuteNonQueryAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute non query", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
            await Task.CompletedTask;
        }

        [Fact]
        public async Task TestErrorHelper_TestMockException_ExecuteXmlAsync()
        {
            string innerExceptionMessage = "Test ExecuteXml Error";
            Services.EndpointMockHelper.TestMockException_ExecuteXml_EnableMockMode(o => {
                o.ExecuteXmlError = innerExceptionMessage;
            });
            var response = await Services.DomainFacade.TestMockException_ExecuteXmlAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute xml query", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
            await Task.CompletedTask;
        }

        [Fact]
        public async Task TestErrorHelper_TestMockException_ExecuteScalarAsync()
        {
            string innerExceptionMessage = "Test ExecuteScalar Error";
            Services.EndpointMockHelper.TestMockException_ExecuteScalar_EnableMockMode(o => {
                o.ExecuteScalarError = innerExceptionMessage;
            });
            var response = await Services.DomainFacade.TestMockException_ExecuteScalarAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute scalar", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task TestErrorHelper_TestMockException_TransactionAsync()
        {
            string innerExceptionMessage = "Test Transaction Error";
            Services.EndpointMockHelper.TestMockException_Transaction_EnableMockMode(o => {
                o.ExecuteNonQueryError = innerExceptionMessage;
            });
            var response = await Services.DomainFacade.TestMockException_TransactionAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to execute non query", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task TestErrorHelper_TestMockException_BeginTransactionAsync()
        {
            string innerExceptionMessage = "Test Transaction Error";
            Services.EndpointMockHelper.TestMockException_Transaction_EnableMockMode(o => {
                o.BeginTransactionError = innerExceptionMessage;
            });
            var response = await Services.DomainFacade.TestMockException_TransactionAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to create transaction", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task TestErrorHelper_TestMockException_Transaction_NullAsync()
        {
            Services.EndpointMockHelper.TestMockException_Transaction_EnableMockMode(o => {
                o.UseNullTransaction = true;
            });
            var response = await Services.DomainFacade.TestMockException_TransactionAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Invalid Transaction Definition", info.ErrorMessage);
            Assert.Equal("", info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.FacadeException, info.ExceptionType);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task TestErrorHelper_TestMockException_Transaction_RollbackAsync()
        {
            string innerExceptionMessage = "Test Transaction Error";
            Services.EndpointMockHelper.TestMockException_Transaction_EnableMockMode(o => {
                o.ExecuteNonQueryError = "some error";
                o.TransactionRollbackError = innerExceptionMessage;
            });
            var response = await Services.DomainFacade.TestMockException_TransactionAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to rollback transaction", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
            await Task.CompletedTask;
        }

        [Fact]
        public async Task TestErrorHelper_TestMockException_ConnectionCreationAsync()
        {
            string innerExceptionMessage = "Test ConnectionCreation Error";
            Services.EndpointMockHelper.TestMockException_ExecuteNonQuery_EnableMockMode(o => {
                o.ConnectionCreationError = innerExceptionMessage;
            });
            var response = await Services.DomainFacade.TestMockException_ExecuteNonQueryAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to create connection", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.FacadeException, info.ExceptionType);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task TestErrorHelper_TestMockException_NullConnectionAsync()
        {
            Services.EndpointMockHelper.TestMockException_ExecuteNonQuery_EnableMockMode(o => {
                o.UseNullConnection = true;
            });
            var response = await Services.DomainFacade.TestMockException_ExecuteNonQueryAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Invalid Connection Definition", info.ErrorMessage);
            Assert.Equal(DbExecutionExceptionType.FacadeException, info.ExceptionType);
            await Task.CompletedTask;
        }
        [Fact]
        public async Task TestErrorHelper_TestMockException_ConnectionOpenAsync()
        {
            string innerExceptionMessage = "Test ConnectionOpen Error";
            Services.EndpointMockHelper.TestMockException_ExecuteNonQuery_EnableMockMode(o => {
                o.ConnectionOpenError = innerExceptionMessage;
            });
            var response = await Services.DomainFacade.TestMockException_ExecuteNonQueryAsync();
            Assert.True(response.HasError);
            var info = response.ErrorInfo;

            Assert.Equal("Unable to open connection", info.ErrorMessage);
            Assert.Equal(innerExceptionMessage, info.ErrorDetails);
            Assert.Equal(DbExecutionExceptionType.DbExecutionError, info.ExceptionType);
            await Task.CompletedTask;
        }
        #endregion
    }
}
