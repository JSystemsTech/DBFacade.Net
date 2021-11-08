using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacadeUnitTests.Models;
using DbFacadeUnitTests.TestFacade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Tests.Facade
{
    [TestClass]
    public class FacadeTests: UnitTestBase
    {

        UnitTestDomainFacade DomainFacade = new UnitTestDomainFacade();

        public void CheckEnumerable<T>(IEnumerable<T>expectedData, IEnumerable<T> data)
        {
            Assert.IsNotNull(data);
            Assert.AreEqual(expectedData.Count(), data.Count());
            Assert.IsTrue(expectedData.SequenceEqual(data));
            
        }
        public async Task CheckEnumerableAsync<T>(IEnumerable<T> expectedData, IEnumerable<T> data)
        {
            Assert.IsNotNull(data);
            Assert.AreEqual(expectedData.Count(), data.Count());
            Assert.IsTrue(expectedData.SequenceEqual(data));
            await Task.CompletedTask;

        }
        public void ValidateEmail(MailAddress expectedEmail, MailAddress email)
        {
            Assert.IsNotNull(email);
            Assert.AreEqual(expectedEmail.Address, email.Address);
            Assert.AreEqual(expectedEmail.Host, email.Host);
            Assert.AreEqual(expectedEmail.User, email.User);
        }
        public async Task  ValidateEmailAsync(MailAddress expectedEmail, MailAddress email)
        {
            Assert.IsNotNull(email);
            Assert.AreEqual(expectedEmail.Address, email.Address);
            Assert.AreEqual(expectedEmail.Host, email.Host);
            Assert.AreEqual(expectedEmail.User, email.User);

            await Task.CompletedTask;
        }
        [TestMethod]
        public void SuccessfullyCatchesUnregisteredConnectionException()
        {
            try
            {
                IDbResponse data = DomainFacade.TestUnregisteredConnectionCall();
                Assert.Fail();
            }
            catch (DbConnectionConfigNotRegisteredException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                if(e.InnerException is DbConnectionConfigNotRegisteredException)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail();
                }
                
            }
        }
        [TestMethod]
        public void SuccessfullyFetchesData()
        {
            IDbResponse<FetchData> data = DomainFacade.TestFetchData();
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count());
            FetchData model = data.First();
            Assert.AreEqual(FetchDataEnum.Pass, model.MyEnum);
            Assert.AreEqual("test string", model.MyString);
            Assert.IsNotNull(model.PublicKey);
            Assert.AreNotEqual(new Guid(), model.PublicKey);
        }
        [TestMethod]
        public void SuccessfullyFetchesAltData()
        {
            IDbResponse<FetchData> data = DomainFacade.TestFetchDataAlt();
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count());
            FetchData model = data.First();
            Assert.AreEqual(FetchDataEnum.Pass, model.MyEnum);
            Assert.AreEqual("test string", model.MyString);
            Assert.IsNotNull(model.PublicKey);
            Assert.AreNotEqual(new Guid(), model.PublicKey);
        }
        [TestMethod]
        public void SuccessfullyFetchesDataWithOutput()
        {
            IDbResponse<FetchData> data = DomainFacade.TestFetchDataWithOutput();
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count());
            FetchData model = data.First();
            Assert.AreEqual("test string", model.MyString);
            Assert.IsNotNull(model.PublicKey);
            Assert.AreNotEqual(new Guid(), model.PublicKey);

            Assert.AreEqual(data.GetOutputValue("MyStringOutputParam"), "output response");
            Assert.AreEqual(data.GetOutputValue<string>("MyStringOutputParam"), "output response");
        }
        [TestMethod]
        public void SuccessfullyFetchesDataAndIgnoresBadColumnName()
        {
            IDbResponse<FetchDataWithBadDbColumn> data = DomainFacade.TestFetchDataWithBadDbColumn();
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count());
            FetchDataWithBadDbColumn model = data.First();
            Assert.IsTrue(model.HasDataBindingErrors);
            Assert.AreEqual(7, model.DataBindingErrors.Count());
            Assert.IsNull(model.MyString);
            Assert.AreEqual( default(int), model.Integer);
            Assert.IsNull(model.IntegerOptional);
            Assert.IsNull(model.IntegerOptionalNull);
            Assert.IsNull(model.MyString);
            Assert.IsNotNull(model.PublicKey);
            Assert.AreEqual(new Guid(), model.PublicKey);
        }
        [TestMethod]
        public void SuccessfullyFetchesNestedData()
        {
            IDbResponse<FetchDataWithNested> data = DomainFacade.TestFetchDataWithNested();
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count());
            FetchDataWithNested model = data.First();

            ValidateEmail(new MailAddress("MyTestEmail@gmail.com"), model.EmailData.Email);


            CheckEnumerable(new[] { "1", "12", "123", "1234" }, model.EnumerableData.Data);
            CheckEnumerable(new short[] { 1, 12, 123, 1234 }, model.EnumerableData.ShortData);
            CheckEnumerable(new int[] { 1, 12, 123, 1234 }, model.EnumerableData.IntData);
            CheckEnumerable(new long[] { 1, 12, 123, 1234 }, model.EnumerableData.LongData);
            CheckEnumerable(new float[] { 1, 12, 123, 1234 }, model.EnumerableData.FloatData);
            CheckEnumerable(new double[] { 1, 12, 123, 1234 }, model.EnumerableData.DoubleData);
            CheckEnumerable(new decimal[] { 1, 12, 123, 1234 }, model.EnumerableData.DecimalData);
            CheckEnumerable(new[] { "1", "12", "123", "1234" }, model.EnumerableData.DataCustom);

            Assert.IsTrue(model.FlagData.Flag);
            Assert.IsTrue(model.FlagData.FlagInt);
            Assert.IsFalse(model.FlagData.FlagFalse);
            Assert.IsFalse(model.FlagData.FlagIntFalse);

        }
        [TestMethod]
        public void SuccessfullyHandlesTransaction()
        {
            IDbResponse<DbDataModel> response = DomainFacade.TestTransaction("my param");
            Assert.IsFalse(response.IsNull);
            Assert.AreEqual(10, response.ReturnValue);
        }
        [TestMethod]
        public void SuccessfullyHandlesTransactionWithOutput()
        {
            IDbResponse<DbDataModel> response = DomainFacade.TestTransactionWithOutput("my param");
            Assert.IsFalse(response.IsNull);
            Assert.AreEqual(10, response.ReturnValue);

            Assert.AreEqual(response.GetOutputValue("MyStringOutputParam"), "output response");
            Assert.AreEqual(response.GetOutputValue<string>("MyStringOutputParam"), "output response");
        }
        [TestMethod]
        public void CatchesValidationError()
        {
            try
            {
                IDbResponse<DbDataModel> response = DomainFacade.TestTransaction(null);
                Assert.Fail();
            }
            catch(ValidationException<string> e)
            {
                Assert.AreEqual(1, e.ValidationErrors.Count());
                Assert.IsNotNull(e.ValidationErrors.First().ErrorMessage);
                Assert.AreEqual("is required.", e.ValidationErrors.First().ErrorMessage);
            }        
        }
        
        
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep1()
        {
            try
            {
                IDbResponse<DbDataModel> response = DomainFacade.TestFetchDataWithModelProcessParams(true, false, false);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                Assert.AreEqual("Stopping at step 1", e.InnerException.Message);
                
            }

        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep2()
        {
            try
            {
                IDbResponse<DbDataModel> response = DomainFacade.TestFetchDataWithModelProcessParams(false, true, false);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                Assert.AreEqual("Stopping at step 2", e.InnerException.Message);
            }

        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep3()
        {
            try
            {
                IDbResponse<DbDataModel> response = DomainFacade.TestFetchDataWithModelProcessParams(false, false, true);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                Assert.AreEqual("Stopping at step 3", e.InnerException.Message);
            }
        }






        [TestMethod]
        public void SuccessfullyFetchesDataAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<FetchData> response = await DomainFacade.TestFetchDataAsync();
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Count());
                FetchData model = response.First();
                Assert.AreEqual(FetchDataEnum.Pass, model.MyEnum);
                Assert.AreEqual("test string", model.MyString);
                Assert.IsNotNull(model.PublicKey);
                Assert.AreNotEqual(new Guid(), model.PublicKey);
                await Task.CompletedTask;
            });

        }
        [TestMethod]
        public void SuccessfullyFetchesAltDataAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<FetchData> response = await DomainFacade.TestFetchDataAltAsync();
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Count());
                FetchData model = response.First();
                Assert.AreEqual(FetchDataEnum.Pass, model.MyEnum);
                Assert.AreEqual("test string", model.MyString);
                Assert.IsNotNull(model.PublicKey);
                Assert.AreNotEqual(new Guid(), model.PublicKey);
                await Task.CompletedTask;
            });

        }
        [TestMethod]
        public void SuccessfullyFetchesDataWithOutputAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<FetchData> response = await DomainFacade.TestFetchDataWithOutputAsync();
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Count());
                FetchData model = response.First();
                Assert.AreEqual("test string", model.MyString);
                Assert.IsNotNull(model.PublicKey);
                Assert.AreNotEqual(new Guid(), model.PublicKey);

                Assert.AreEqual(response.GetOutputValue("MyStringOutputParam"), "output response");
                Assert.AreEqual(response.GetOutputValue<string>("MyStringOutputParam"), "output response");

                await Task.CompletedTask;
            });
        }
        [TestMethod]
        public void SuccessfullyFetchesDataAndIgnoresBadColumnNameAsync()
        {
            RunAsAsyc(async () =>
            {
                try
                {
                    IDbResponse<FetchDataWithBadDbColumn> response = await DomainFacade.TestFetchDataWithBadDbColumnAsync();
                    Assert.IsNotNull(response);
                    Assert.AreEqual(1, response.Count());
                    FetchDataWithBadDbColumn model = response.First();
                    Assert.IsTrue(model.HasDataBindingErrors);
                    Assert.AreEqual(7, model.DataBindingErrors.Count());
                    Assert.IsNull(model.MyString);
                    Assert.AreEqual(default(int), model.Integer);
                    Assert.IsNull(model.IntegerOptional);
                    Assert.IsNull(model.IntegerOptionalNull);
                    Assert.IsNull(model.MyString);
                    Assert.IsNotNull(model.PublicKey);
                    Assert.AreEqual(new Guid(), model.PublicKey);

                    await Task.CompletedTask;
                }
                catch(Exception)
                {
                    Assert.Fail();
                    await Task.CompletedTask;
                }
               
            });
        }
        [TestMethod]
        public void SuccessfullyFetchesNestedDataAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<FetchDataWithNested> response = await DomainFacade.TestFetchDataWithNestedAsync();
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Count());
                FetchDataWithNested model = response.First();

                await ValidateEmailAsync(new MailAddress("MyTestEmail@gmail.com"), model.EmailData.Email);

                await CheckEnumerableAsync(new[] { "1", "12", "123", "1234" }, model.EnumerableData.Data);
                await CheckEnumerableAsync(new short[] { 1, 12, 123, 1234 }, model.EnumerableData.ShortData);
                await CheckEnumerableAsync(new int[] { 1, 12, 123, 1234 }, model.EnumerableData.IntData);
                await CheckEnumerableAsync(new long[] { 1, 12, 123, 1234 }, model.EnumerableData.LongData);
                await CheckEnumerableAsync(new float[] { 1, 12, 123, 1234 }, model.EnumerableData.FloatData);
                await CheckEnumerableAsync(new double[] { 1, 12, 123, 1234 }, model.EnumerableData.DoubleData);
                await CheckEnumerableAsync(new decimal[] { 1, 12, 123, 1234 }, model.EnumerableData.DecimalData);
                await CheckEnumerableAsync(new[] { "1", "12", "123", "1234" }, model.EnumerableData.DataCustom);

                Assert.IsTrue(model.FlagData.Flag);
                Assert.IsTrue(model.FlagData.FlagInt);
                Assert.IsFalse(model.FlagData.FlagFalse);
                Assert.IsFalse(model.FlagData.FlagIntFalse);

                await Task.CompletedTask;
            });

        }
        [TestMethod]
        public void SuccessfullyHandlesTransactionAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<DbDataModel> response = await DomainFacade.TestTransactionAsync("my param");
                Assert.IsFalse(response.IsNull);
                Assert.AreEqual(10, response.ReturnValue);
                await Task.CompletedTask;
            });
        }
        [TestMethod]
        public void SuccessfullyHandlesTransactionWithOutputAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<DbDataModel> response = await DomainFacade.TestTransactionWithOutputAsync("my param");
                Assert.IsFalse(response.IsNull);
                Assert.AreEqual(10, response.ReturnValue);

                Assert.AreEqual(response.GetOutputValue("MyStringOutputParam"), "output response");
                Assert.AreEqual(response.GetOutputValue<string>("MyStringOutputParam"), "output response");
                await Task.CompletedTask;
            });
        }
        [TestMethod]
        public void CatchesValidationErrorAsync()
        {
            RunAsAsyc(async () =>
            {
                try
                {
                    IDbResponse<DbDataModel> response = await DomainFacade.TestTransactionAsync(null);
                    Assert.Fail();
                    await Task.CompletedTask;
                }
                catch (ValidationException<string> e)
                {
                    Assert.AreEqual(1, e.ValidationErrors.Count());
                    Assert.IsNotNull(e.ValidationErrors.First().ErrorMessage);
                    Assert.AreEqual("is required.", e.ValidationErrors.First().ErrorMessage);
                    await Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is ValidationException<string> e)
                    {
                        Assert.AreEqual(1, e.ValidationErrors.Count());
                        Assert.IsNotNull(e.ValidationErrors.First().ErrorMessage);
                        Assert.AreEqual("is required.", e.ValidationErrors.First().ErrorMessage);
                        await Task.CompletedTask;
                    }
                    else
                    {
                        Assert.Fail();
                        await Task.CompletedTask;
                    }
                }

            });
        }
        
        
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep1Async()
        {
            RunAsAsyc(async () =>
            {
                try
                {
                    IDbResponse<DbDataModel> response = await DomainFacade.TestFetchDataWithModelProcessParamsAsync(true, false, false);
                    Assert.Fail();
                    await Task.CompletedTask;
                }
                catch (Exception e)
                {
                    Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                    Assert.AreEqual("Stopping at step 1", e.InnerException.Message);
                    await Task.CompletedTask;
                }

            });

        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep2Async()
        {
            RunAsAsyc(async () =>
            {
                try
                {
                    IDbResponse<DbDataModel> response = await DomainFacade.TestFetchDataWithModelProcessParamsAsync(false, true, false);
                    Assert.Fail();
                    await Task.CompletedTask;
                }
                catch (Exception e)
                {
                    Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                    Assert.AreEqual("Stopping at step 2", e.InnerException.Message);
                    await Task.CompletedTask;
                }

            });
        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep3Async()
        {
            RunAsAsyc(async () =>
            {
                try
                {
                    IDbResponse<DbDataModel> response = await DomainFacade.TestFetchDataWithModelProcessParamsAsync(false, false, true);
                    Assert.Fail();
                    await Task.CompletedTask;
                }
                catch (Exception e)
                {
                    Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                    Assert.AreEqual("Stopping at step 3", e.InnerException.Message);
                    await Task.CompletedTask;
                }
            });
        }
    }
}
