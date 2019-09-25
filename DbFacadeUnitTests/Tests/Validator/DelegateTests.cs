using DBFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class BasicStringTests : ValidatorTestBase
    {
        [TestMethod]
        public void IsNullOrEmpty()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNullOrEmpty(model => model.StringNumNull),
                UnitTestRules.IsNullOrEmpty(model => string.Empty)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsNullOrEmptyFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNullOrEmpty(model => model.String)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void IsNullOrWhiteSpace()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNullOrWhiteSpace(model => model.StringNumNull),
                UnitTestRules.IsNullOrWhiteSpace(model => string.Empty),
                UnitTestRules.IsNullOrWhiteSpace(model => " "),
                UnitTestRules.IsNullOrWhiteSpace(model => "      ")
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsNullOrWhiteSpaceFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNullOrWhiteSpace(model => model.String)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
    }        
}
