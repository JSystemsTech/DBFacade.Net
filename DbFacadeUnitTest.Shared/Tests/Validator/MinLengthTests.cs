using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class MinLengthTests:ValidatorTestBase
    {
        [TestMethod]
        public void MinLength()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MinLength(model => model.String, 10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MinLength(model => model.String, 20)
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
                UnitTestRules.MinLength(model => model.String, 10, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MinLength(model => model.StringNumNull, 20, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MinLength(model => model.String, 20, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }




        [TestMethod]
        public void MinLengthAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MinLength(model => model.String, 10)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MinLength(model => model.String, 20)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void MinLengthOptionalValueAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MinLength(model => model.String, 10, true)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalNullAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MinLength(model => model.StringNumNull, 20, true)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MinLength(model => model.String, 20, true)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
    }
}
