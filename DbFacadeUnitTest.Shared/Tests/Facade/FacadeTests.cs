using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacadeUnitTests.Models;
using DbFacadeUnitTests.TestFacade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Tests.Facade
{
    [TestClass]
    public class FacadeTests: UnitTestBase
    {
        UnitTestDomainFacade DomainFacade = new UnitTestDomainFacade();
        UnitTestDomainFacadeWithManager DomainFacadeWithManager = new UnitTestDomainFacadeWithManager();
        UnitTestDomainFacadeCustom DomainFacadeCustom = new UnitTestDomainFacadeCustom();

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
        public void SuccessfullyFetchesData()
        {
            IDbResponse<FetchData> data = DomainFacade.TestFetchData();
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count());
            FetchData model = data.First();
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

            Assert.IsNotNull(data.OutputValues);
            Assert.IsTrue(data.OutputValues.Count > 0);
            Assert.IsTrue(data.OutputValues.ContainsKey("MyStringOutputParam"));
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
            Assert.AreEqual(5, model.DataBindingErrors.Count());
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
            IDbResponse response = DomainFacade.TestTransaction("my param");
            Assert.IsNotNull(response);
            Assert.AreEqual(10, response.ReturnValue);
        }
        [TestMethod]
        public void SuccessfullyHandlesTransactionWithOutput()
        {
            IDbResponse response = DomainFacade.TestTransactionWithOutput("my param");
            Assert.IsNotNull(response);
            Assert.AreEqual(10, response.ReturnValue);

            Assert.IsNotNull(response.OutputValues);
            Assert.IsTrue(response.OutputValues.Count > 0);
            Assert.IsTrue(response.OutputValues.ContainsKey("MyStringOutputParam"));
            Assert.AreEqual(response.GetOutputValue("MyStringOutputParam"), "output response");
            Assert.AreEqual(response.GetOutputValue<string>("MyStringOutputParam"), "output response");
        }
        [TestMethod]
        public void CatchesValidationError()
        {
            try
            {
                IDbResponse response = DomainFacade.TestTransaction(null);
                Assert.Fail();
            }
            catch(ValidationException<DbParamsModel<string>> e)
            {
                Assert.AreEqual(1, e.ValidationErrors.Count());
                Assert.IsNotNull(e.ValidationErrors.First().ErrorMessage);
                Assert.AreEqual("Param1 (Property Value) is required.", e.ValidationErrors.First().ErrorMessage);
            }         
        }
        [TestMethod]
        public void SuccessfullyPassesManager()
        {
            IDbResponse response = DomainFacadeWithManager.TestFetchData(true);
            Assert.IsNotNull(response);
        }
        [TestMethod]
        public void ManagerCatchesInvalidModel()
        {
            try
            {
                IDbResponse response = DomainFacadeWithManager.TestFetchData(false);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("UnitTestManager caught invalid model", e.Message);
            }
            
        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep1()
        {
            try
            {
                IDbResponse response = DomainFacadeCustom.TestFetchData(true, false, false);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Stopping at step 1", e.Message);
            }

        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep2()
        {
            try
            {
                IDbResponse response = DomainFacadeCustom.TestFetchData(false, true, false);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Stopping at step 2", e.Message);
            }

        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep3()
        {
            try
            {
                IDbResponse response = DomainFacadeCustom.TestFetchData(false, false, true);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Stopping at step 3", e.Message);
            }
        }






        [TestMethod]
        public void SuccessfullyFetchesDataAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse <FetchData> response = await DomainFacade.TestFetchDataAsync();
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Count());
                FetchData model = response.First();
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

                Assert.IsNotNull(response.OutputValues);
                Assert.IsTrue(response.OutputValues.Count > 0);
                Assert.IsTrue(response.OutputValues.ContainsKey("MyStringOutputParam"));
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
                IDbResponse<FetchDataWithBadDbColumn> response = await DomainFacade.TestFetchDataWithBadDbColumnAsync();
                Assert.IsNotNull(response);
                Assert.AreEqual(1, response.Count());
                FetchDataWithBadDbColumn model = response.First();
                Assert.IsTrue(model.HasDataBindingErrors);
                Assert.AreEqual(5, model.DataBindingErrors.Count());
                Assert.IsNull(model.MyString);
                Assert.AreEqual(default(int), model.Integer);
                Assert.IsNull(model.IntegerOptional);
                Assert.IsNull(model.IntegerOptionalNull);
                Assert.IsNull(model.MyString);
                Assert.IsNotNull(model.PublicKey);
                Assert.AreEqual(new Guid(), model.PublicKey);

                await Task.CompletedTask;
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
                IDbResponse response = await DomainFacade.TestTransactionAsync("my param");
                Assert.IsNotNull(response);
                Assert.AreEqual(10, response.ReturnValue);
                await Task.CompletedTask;
            });
        }
        [TestMethod]
        public void SuccessfullyHandlesTransactionWithOutputAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse response = await DomainFacade.TestTransactionWithOutputAsync("my param");
                Assert.IsNotNull(response);
                Assert.AreEqual(10, response.ReturnValue);

                Assert.IsNotNull(response.OutputValues);
                Assert.IsTrue(response.OutputValues.Count > 0);
                Assert.IsTrue(response.OutputValues.ContainsKey("MyStringOutputParam"));
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
                    IDbResponse response = await DomainFacade.TestTransactionAsync(null);
                    Assert.Fail();
                    await Task.CompletedTask;
                }
                catch (ValidationException<DbParamsModel<string>> e)
                {
                    Assert.AreEqual(1, e.ValidationErrors.Count());
                    Assert.IsNotNull(e.ValidationErrors.First().ErrorMessage);
                    Assert.AreEqual("Param1 (Property Value) is required.", e.ValidationErrors.First().ErrorMessage);
                    await Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    if(ex.InnerException is ValidationException<DbParamsModel<string>> e)
                    {
                        Assert.AreEqual(1, e.ValidationErrors.Count());
                        Assert.IsNotNull(e.ValidationErrors.First().ErrorMessage);
                        Assert.AreEqual("Param1 (Property Value) is required.", e.ValidationErrors.First().ErrorMessage);
                        await Task.CompletedTask;
                    }
                    else{
                        Assert.Fail();
                        await Task.CompletedTask;
                    }
                }
                
            });
        }
        [TestMethod]
        public void SuccessfullyPassesManagerAsync()
        {
            Task<IDbResponse<FetchData>> response = DomainFacadeWithManager.TestFetchDataAsync(true);
            response.Wait();
            Assert.IsNotNull(response);
        }
        [TestMethod]
        public void ManagerCatchesInvalidModelAsync()
        {
            RunAsAsyc(async () =>
            {
                try
                {
                    IDbResponse<FetchData> response = await DomainFacadeWithManager.TestFetchDataAsync(false);
                    Assert.Fail();
                    await Task.CompletedTask;
                }                
                catch (Exception e)
                {
                    if (e.InnerException is Exception ex)
                    {
                        Assert.AreEqual("UnitTestManager caught invalid model", ex.Message);
                        await Task.CompletedTask;
                    }
                    else
                    {
                        Assert.AreEqual("UnitTestManager caught invalid model", e.Message);
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
                    IDbResponse<FetchData> response = await DomainFacadeCustom.TestFetchDataAsync(true, false, false);
                    Assert.Fail();
                    await Task.CompletedTask;
                }
                catch (Exception e)
                {
                    if (e.InnerException is Exception ex)
                    {
                        Assert.AreEqual("Stopping at step 1", ex.Message);
                        await Task.CompletedTask;
                    }
                    else
                    {
                        Assert.AreEqual("Stopping at step 1", e.Message);
                        await Task.CompletedTask;
                    }
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
                    IDbResponse<FetchData> response = await DomainFacadeCustom.TestFetchDataAsync(false, true, false);
                    Assert.Fail();
                    await Task.CompletedTask;
                }
                catch (Exception e)
                {
                    if (e.InnerException is Exception ex)
                    {
                        Assert.AreEqual("Stopping at step 2", ex.Message);
                        await Task.CompletedTask;
                    }
                    else
                    {
                        Assert.AreEqual("Stopping at step 2", e.Message);
                        await Task.CompletedTask;
                    }
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
                    IDbResponse<FetchData> response = await DomainFacadeCustom.TestFetchDataAsync(false, false, true);
                    Assert.Fail();
                    await Task.CompletedTask;
                }
                catch (Exception e)
                {
                    if (e.InnerException is Exception ex)
                    {
                        Assert.AreEqual("Stopping at step 3", ex.Message);
                        await Task.CompletedTask;
                    }
                    else
                    {
                        Assert.AreEqual("Stopping at step 3", e.Message);
                        await Task.CompletedTask;
                    }
                }

            });
        }
    }
}
