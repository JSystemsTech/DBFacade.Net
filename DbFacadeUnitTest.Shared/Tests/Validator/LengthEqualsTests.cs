using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class LengthEqualsTests : ValidatorTestBase
    {
        [TestMethod]
        public void EqualTo()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.String, 14)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.String, 20)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void MinLengthOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.String, 14, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.StringNumNull, 14, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.String, 20, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }



        [TestMethod]
        public void EqualToAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.String, 14)
            };
            var result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.String, 20)
            };
            var result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void MinLengthOptionalValueAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.String, 14, true)
            };
            var result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalNullAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.StringNumNull, 14, true)
            };
            var result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LengthEquals(model => model.String, 20, true)
            };
            var result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
    }
}
