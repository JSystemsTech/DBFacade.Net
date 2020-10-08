using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class MaxLengthTests:ValidatorTestBase
    {
        [TestMethod]
        public void MaxLength()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.String, 20)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MaxLengthFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.String, 10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void MaxLengthOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.String, 20, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MaxLengthOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.StringNumNull, 10, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MaxLengthOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.String, 10, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }




        [TestMethod]
        public void MaxLengthAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.String, 20)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MaxLengthFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.String, 10)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void MaxLengthOptionalValueAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.String, 20, true)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MaxLengthOptionalNullAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.StringNumNull, 10, true)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MaxLengthOptionalFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.MaxLength(model => model.String, 10, true)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
    }
}
