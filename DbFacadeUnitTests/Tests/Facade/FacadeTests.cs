using DBFacade.DataLayer.Models;
using DBFacade.Exceptions;
using DbFacadeUnitTests.Models;
using DbFacadeUnitTests.TestFacade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

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
        public void ValidateEmail(MailAddress expectedEmail, MailAddress email)
        {
            Assert.IsNotNull(email);
            Assert.AreEqual(expectedEmail.Address, email.Address);
            Assert.AreEqual(expectedEmail.Host, email.Host);
            Assert.AreEqual(expectedEmail.User, email.User);
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
        public void SuccessfullyFetchesNestedData()
        {
            IDbResponse<FetchDataWithNested> data = DomainFacade.TestFetchDataWithNsted();
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
    }
}
