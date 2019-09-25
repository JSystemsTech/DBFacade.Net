using DBFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class DelegateTests : ValidatorTestBase
    {
        private bool Validates(string value) => true;
        private bool Invalidates(string value) => !Validates(value);
        [TestMethod]
        public void Delegate()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Delegate(model => model.String, Validates),
                UnitTestRules.Delegate(model => model.String, value=> true),
                UnitTestRules.Delegate(model => model.Short, value=> value == 10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void DelegateFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Delegate(model => model.String, Invalidates),
                UnitTestRules.Delegate(model => model.String, value=> value != value),
                UnitTestRules.Delegate(model => model.Short, value=> value == 11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 3);
        }
    }        
}
