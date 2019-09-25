using DBFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class RequiredTests:ValidatorTestBase
    {
        
        [TestMethod]
        public void Constant()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Required(model => "MyString")
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void ConstantFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Required(model => null)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void Method()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Required(model => model.GetStringValue("MyString"))
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MethodFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Required(model => model.GetStringValue(null))
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithNonNullValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Required(model => model.String)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNullValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Required(model => model.Null)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
    }
}
