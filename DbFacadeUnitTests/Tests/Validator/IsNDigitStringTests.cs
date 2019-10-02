using DBFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsNDigitStringTests : ValidatorTestBase
    {
        [TestMethod]
        public void NDigitString()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNDigitString(model => model.FiveDigitString, 5),
                UnitTestRules.IsNDigitString(model => model.TenDigitString, 10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void NDigitStringFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNDigitString(model => model.FiveDigitString, 10),
                UnitTestRules.IsNDigitString(model => model.TenDigitString, 5),
                UnitTestRules.IsNDigitString(model => model.TenDigitString, -1),
                UnitTestRules.IsNDigitString(model => model.String, 5)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 4);
        }
        [TestMethod]
        public void NDigitStringOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNDigitString(model => model.FiveDigitString, 5, true),
                UnitTestRules.IsNDigitString(model => model.TenDigitString, 10,true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNDigitString(model => model.StringNumNull, 5, true),
                UnitTestRules.IsNDigitString(model => model.StringNumNull, 10,true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNDigitString(model => model.FiveDigitString, 10, true),
                UnitTestRules.IsNDigitString(model => model.TenDigitString, 5,true),
                UnitTestRules.IsNDigitString(model => model.TenDigitString, -1, true),
                UnitTestRules.IsNDigitString(model => model.String, 5, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 4);
        }
    }
}
