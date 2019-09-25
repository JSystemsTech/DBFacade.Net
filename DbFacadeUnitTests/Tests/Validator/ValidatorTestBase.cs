using DBFacade.DataLayer.Models.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class ValidatorTestBase: UnitTestBase
    {
        protected void IsValid(IValidationResult result)
        {
            Assert.AreEqual(true, result.IsValid);
        }
        protected void IsInvalid(IValidationResult result)
        {
            Assert.AreEqual(false, result.IsValid);
        }
        protected void HasCorrectErrorCount(IValidationResult result, int expectedErrorCount)
        {
            Assert.AreEqual(expectedErrorCount, result.Errors.Count());
        }
        
    }
}
