using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacadeUnitTests.Models;
using DbFacadeUnitTests.TestFacade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using DbFacade.DataLayer.ConnectionService;
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
            IDbResponse data = DomainFacade.TestUnregisteredConnectionCall();
            Assert.IsTrue(data.HasError);
            Assert.IsTrue(data.Error is DbConnectionConfigNotRegisteredException);
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
        public void SuccessfullyFetchesTestDataConsistantly()
        {
            for(int i =  0; i < 10; i++)
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
        }
        [TestMethod]
        public void SuccessfullyFetchesMultipleDataSets()
        {
            IDbResponse<UserData> users = DomainFacade.TestMultipleDataSets();
            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count());
            var user1 = users.ElementAt(0);
            var user2 = users.ElementAt(1);
            Assert.AreEqual(1, user1.Id);
            Assert.AreEqual("Test User", user1.Name);
            Assert.AreEqual(2, user2.Id);
            Assert.AreEqual("Other User", user2.Name);

            Assert.AreEqual(2, users.DataSets.Count());
            var roles = users.DataSets.ElementAt(1).ToDbDataModelList<UserRole>();
            Assert.IsNotNull(roles);
            Assert.AreEqual(2, roles.Count());
            var role1 = roles.ElementAt(0);
            var role2 = roles.ElementAt(1);
            Assert.AreEqual("R1", role1.Value);
            Assert.AreEqual("Role One", role1.Name);
            Assert.AreEqual("R2", role2.Value);
            Assert.AreEqual("Role Two", role2.Name);
        }
        //[TestMethod]
        //public void SuccessfullyFetchesMultipleDataSets2()
        //{
        //    IDbResponse<UserData> users = DomainFacade.TestMultipleDataSets();
        //    Assert.IsNotNull(users);
        //    Assert.AreEqual(2, users.Count());
        //    var user1 = users.ElementAt(0);
        //    var user2 = users.ElementAt(1);
        //    Assert.AreEqual(1, user1.Id);
        //    Assert.AreEqual("Test User", user1.Name);
        //    Assert.AreEqual(2, user2.Id);
        //    Assert.AreEqual("Other User", user2.Name);

        //    Assert.AreEqual(2, users.DataSets.Count());
        //    var roles = users.DataSets.ElementAt(1).ToDbDataModelList<UserRole>();
        //    Assert.IsNotNull(roles);
        //    Assert.AreEqual(2, roles.Count());
        //    var role1 = roles.ElementAt(0);
        //    var role2 = roles.ElementAt(1);
        //    Assert.AreEqual("R1", role1.Value);
        //    Assert.AreEqual("Role One", role1.Name);
        //    Assert.AreEqual("R2", role2.Value);
        //    Assert.AreEqual("Role Two", role2.Name);
        //}
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
            IDbResponse<DbDataModel> response = DomainFacade.TestTransaction(null);
            if (response.Error is ValidationException e)
            {
                Assert.AreEqual(1, e.ValidationErrors.Count());
                Assert.IsNotNull(e.ValidationErrors.First().ErrorMessage);
                Assert.AreEqual("Unspecified Parameter is required.", e.ValidationErrors.First().ErrorMessage);
            }
            else
            {
                Assert.Fail();
            }
        }
        
        
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep1()
        {
            IDbResponse<DbDataModel> response = DomainFacade.TestFetchDataWithModelProcessParams(true, false, false);
            if (response.Error is Exception e)
            {
                Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                Assert.AreEqual("Stopping at step 1", e.InnerException.Message);
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep2()
        {
            IDbResponse<DbDataModel> response = DomainFacade.TestFetchDataWithModelProcessParams(false, true, false);
            if (response.Error is Exception e)
            {
                Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                Assert.AreEqual("Stopping at step 2", e.InnerException.Message);
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep3()
        {
            IDbResponse<DbDataModel> response = DomainFacade.TestFetchDataWithModelProcessParams(false, false, true);
            if (response.Error is Exception e)
            {
                Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                Assert.AreEqual("Stopping at step 3", e.InnerException.Message);
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void VerifyFetchCallAsTransaction()
        {
            IDbResponse response = DomainFacade.TestNoData();
            if (response.Error is Exception e)
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsFalse(response.HasError);
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
        public void SuccessfullyFetchesTestDataConsistantlyAsync()
        {
            RunAsAsyc(async () =>
            {
                for (int i = 0; i < 10; i++)
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
                }
            });

        }
        [TestMethod]
        public void SuccessfullyFetchesMultipleDataSetsAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<UserData> users = await DomainFacade.TestMultipleDataSetsAsync();
                Assert.IsNotNull(users);
                Assert.AreEqual(2, users.Count());
                var user1 = users.ElementAt(0);
                var user2 = users.ElementAt(1);
                Assert.AreEqual(1, user1.Id);
                Assert.AreEqual("Test User", user1.Name);
                Assert.AreEqual(2, user2.Id);
                Assert.AreEqual("Other User", user2.Name);

                Assert.AreEqual(2, users.DataSets.Count());
                var roles = await users.DataSets.ElementAt(1).ToDbDataModelListAsync<UserRole>();
                Assert.IsNotNull(roles);
                Assert.AreEqual(2, roles.Count());
                var role1 = roles.ElementAt(0);
                var role2 = roles.ElementAt(1);
                Assert.AreEqual("R1", role1.Value);
                Assert.AreEqual("Role One", role1.Name);
                Assert.AreEqual("R2", role2.Value);
                Assert.AreEqual("Role Two", role2.Name);
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
                IDbResponse<DbDataModel> response = await DomainFacade.TestTransactionAsync(null);
                if (response.Error is ValidationException e)
                {
                    Assert.AreEqual(1, e.ValidationErrors.Count());
                    Assert.IsNotNull(e.ValidationErrors.First().ErrorMessage);
                    Assert.AreEqual("Unspecified Parameter is required.", e.ValidationErrors.First().ErrorMessage);
                    await Task.CompletedTask;
                }
                else
                {
                    Assert.Fail();
                    await Task.CompletedTask;
                }
            });
        }
        
        
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep1Async()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<DbDataModel> response = await DomainFacade.TestFetchDataWithModelProcessParamsAsync(true, false, false);
                if (response.Error is Exception e)
                {
                    Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                    Assert.AreEqual("Stopping at step 1", e.InnerException.Message);
                    await Task.CompletedTask;
                }
                else
                {
                    Assert.Fail();
                    await Task.CompletedTask;
                }
            });

        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep2Async()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<DbDataModel> response = await DomainFacade.TestFetchDataWithModelProcessParamsAsync(false, true, false);
                if (response.Error is Exception e)
                {
                    Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                    Assert.AreEqual("Stopping at step 2", e.InnerException.Message);
                    await Task.CompletedTask;
                }
                else
                {
                    Assert.Fail();
                    await Task.CompletedTask;
                }
            });
        }
        [TestMethod]
        public void CustomFacadeVerifyStopsAtStep3Async()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse<DbDataModel> response = await DomainFacade.TestFetchDataWithModelProcessParamsAsync(false, false, true);
                if (response.Error is Exception e)
                {
                    Assert.AreEqual("An Error occured before calling 'Test Fetch Data'", e.Message);
                    Assert.AreEqual("Stopping at step 3", e.InnerException.Message);
                    await Task.CompletedTask;
                }
                else
                {
                    Assert.Fail();
                    await Task.CompletedTask;
                }
            });
        }

        [TestMethod]
        public void VerifyFetchCallAsTransactionAsync()
        {
            RunAsAsyc(async () =>
            {
                IDbResponse response = await DomainFacade.TestNoDataAsync();
                if (response.Error is Exception e)
                {
                    Assert.Fail();
                    await Task.CompletedTask;
                }
                else
                {
                    Assert.IsFalse(response.HasError); 
                    await Task.CompletedTask;
                }
            });
            
        }
        private class BenchmarkConstants
        {
            public const int TinyCount = 10;
            public const int VerySmallCount = 50;
            public const int SmallCount = 100;
            public const int MediumCount = 1000;
            public const int LargeCount = 10000;
            public const int VeryLargeCount = 20000;
            public const int MassiveCount = 50000;

            public const double Tiny = 0.05;
            public const double VerySmall = 0.1;
            public const double Small = 0.3;
            public const double Medium = 0.5;
            public const double Large = 1.5;
            public const double VeryLarge = 10;
            public const double Massive = 180; //3 min

            public const string TinyText = "Tiny";
            public const string VerySmallText = "Very Small";
            public const string SmallText = "Small";
            public const string MediumText = "Medium";
            public const string LargeText = "Large";
            public const string VeryLargeText = "Very Large";
            public const string MassiveText = "Massive";
        }
        
        private void TestBenchmark(int count, double threshold, string text)
        {
            DbFacade.Metrics.Clear();
            var presetDone = DbFacade.Metrics.Begin($"Test Benchmark {text}: Preset Data");
            var testModel = new
            {
                str = TestClass2.Value1.String,
                integer = TestClass2.Value1.Integer,
                guid = TestClass2.Value1.Guid,
                testEnum = TestClass2.Value1.TestEnum,
                strList = string.Join(",", TestClass2.Value1.StrList.Select(m => m.ToString()))
            };
            var list = Enumerable.Range(1, count).Select(m => testModel);
            var mock = MockResponseData.Create(list, null, 0);
            UnitTestConnection.TestBenchmark.AddMockResponseData(mock);
            UnitTestConnection.EnableMockMode();
            presetDone();

            DateTime start = DateTime.Now;
            var response = DomainFacade.TestBenchmark();
            DateTime end = DateTime.Now;           

            double seconds = (end - start).TotalSeconds;
            var metricsMap = DbFacade.Metrics.MetricsMap;
            var metricsList = DbFacade.Metrics.MetricsList;
            Assert.IsTrue(seconds < threshold, $"TestBenchmark: Parsing {text} data set took {seconds} seconds");
            Assert.IsFalse(response.HasError, $"Response Error: {response.ErrorMessage} Details: {response.ErrorDetails}");
            Assert.AreEqual(count, response.Count());
        }
        [TestMethod]
        public void TestBenchmarkTiny() => TestBenchmark(BenchmarkConstants.TinyCount, BenchmarkConstants.Tiny, BenchmarkConstants.TinyText);
        [TestMethod]
        public void TestBenchmarkVerySmall() => TestBenchmark(BenchmarkConstants.VerySmallCount, BenchmarkConstants.VerySmall, BenchmarkConstants.VerySmallText);
        [TestMethod]
        public void TestBenchmarkSmall() => TestBenchmark(BenchmarkConstants.SmallCount, BenchmarkConstants.Small, BenchmarkConstants.SmallText);
        [TestMethod]
        public void TestBenchmarkMedium() => TestBenchmark(BenchmarkConstants.MediumCount, BenchmarkConstants.Medium, BenchmarkConstants.MediumText);
        [TestMethod]
        public void TestBenchmarkLarge() => TestBenchmark(BenchmarkConstants.LargeCount, BenchmarkConstants.Large, BenchmarkConstants.LargeText);
        [TestMethod]
        public void TestBenchmarkVeryLarge() => TestBenchmark(BenchmarkConstants.VeryLargeCount, BenchmarkConstants.VeryLarge, BenchmarkConstants.VeryLargeText);
        [TestMethod]
        public void TestBenchmarkMassive() => TestBenchmark(BenchmarkConstants.MassiveCount, BenchmarkConstants.Massive, BenchmarkConstants.MassiveText);


        private void TestBenchmarkAsync(int count, double threshold, string text)
        {
            var testModel = new
            {
                str = TestClass2.Value1.String,
                integer = TestClass2.Value1.Integer,
                guid = TestClass2.Value1.Guid,
                testEnum = TestClass2.Value1.TestEnum,
                strList = string.Join(",", TestClass2.Value1.StrList.Select(m => m.ToString()))
            };
            var list = Enumerable.Range(1, count).Select(m => testModel);
            var mock = MockResponseData.Create(list, null, 0);
            UnitTestConnection.TestBenchmark.AddMockResponseData(mock);
            UnitTestConnection.EnableMockMode();

            RunAsAsyc(async () =>
            {
                DateTime start = DateTime.Now;
                var response = await DomainFacade.TestBenchmarkAsync();
                DateTime end = DateTime.Now;

                double seconds = (end - start).TotalSeconds;
                Assert.IsTrue(seconds < threshold, $"TestBenchmark: Parsing {text} data set took {seconds} seconds");
                Assert.IsFalse(response.HasError, $"Response Error: {response.ErrorMessage} Details: {response.ErrorDetails}");
                Assert.AreEqual(count, response.Count());
            });

            
        }
        [TestMethod]
        public void TestBenchmarkTinyAsync() => TestBenchmarkAsync(BenchmarkConstants.TinyCount, BenchmarkConstants.Tiny, BenchmarkConstants.TinyText);
        [TestMethod]
        public void TestBenchmarkVerySmallAsync() => TestBenchmarkAsync(BenchmarkConstants.VerySmallCount, BenchmarkConstants.VerySmall, BenchmarkConstants.VerySmallText);
        [TestMethod]
        public void TestBenchmarkSmallAsync() => TestBenchmarkAsync(BenchmarkConstants.SmallCount, BenchmarkConstants.Small, BenchmarkConstants.SmallText);
        [TestMethod]
        public void TestBenchmarkMediumAsync() => TestBenchmarkAsync(BenchmarkConstants.MediumCount, BenchmarkConstants.Medium, BenchmarkConstants.MediumText);
        [TestMethod]
        public void TestBenchmarkLargeAsync() => TestBenchmarkAsync(BenchmarkConstants.LargeCount, BenchmarkConstants.Large, BenchmarkConstants.LargeText);
        [TestMethod]
        public void TestBenchmarkVeryLargeAsync() => TestBenchmarkAsync(BenchmarkConstants.VeryLargeCount, BenchmarkConstants.VeryLarge, BenchmarkConstants.VeryLargeText);
        [TestMethod]
        public void TestBenchmarkMassiveAsync() => TestBenchmarkAsync(BenchmarkConstants.MassiveCount, BenchmarkConstants.Massive, BenchmarkConstants.MassiveText);
    }
}
