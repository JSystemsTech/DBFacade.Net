using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsDateTimeTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithStringNum()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.DateTimeString),
                UnitTestRules.IsDateTime(model => model.DateTimeStringAlt, "dd-MM-yyyy")
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringNumFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.StringInvalidDate),
                UnitTestRules.IsDateTime(model => model.StringInvalidDate, "dd-MM-yyyy")
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
        [TestMethod]
        public void WithOptional()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.DateTimeString, null, true),
                UnitTestRules.IsDateTime(model => model.DateTimeStringAlt, "dd-MM-yyyy", true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.StringNumNull, null, true),
                UnitTestRules.IsDateTime(model => model.StringNumNull, "dd-MM-yyyy", true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.StringInvalidDate, null, true),
                UnitTestRules.IsDateTime(model => model.StringInvalidDate, "dd-MM-yyyy", true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }




        [TestMethod]
        public void WithStringNumAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.DateTimeString),
                UnitTestRules.IsDateTime(model => model.DateTimeStringAlt, "dd-MM-yyyy")
            };
            var result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringNumFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.StringInvalidDate),
                UnitTestRules.IsDateTime(model => model.StringInvalidDate, "dd-MM-yyyy")
            };
            var result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
        [TestMethod]
        public void WithOptionalAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.DateTimeString, null, true),
                UnitTestRules.IsDateTime(model => model.DateTimeStringAlt, "dd-MM-yyyy", true)
            };
            var result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalNullAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.StringNumNull, null, true),
                UnitTestRules.IsDateTime(model => model.StringNumNull, "dd-MM-yyyy", true)
            };
            var result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsDateTime(model => model.StringInvalidDate, null, true),
                UnitTestRules.IsDateTime(model => model.StringInvalidDate, "dd-MM-yyyy", true)
            };
            var result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
    }
}
