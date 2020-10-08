using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

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

        #region async tests
        [TestMethod]
        public void ConstantAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.RequiredAsync(model => "MyString")
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void ConstantFailAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.RequiredAsync(model => null)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
        [TestMethod]
        public void MethodAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.RequiredAsync(model => model.GetStringValue("MyString"))
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void MethodFailAsync()
        {            
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.RequiredAsync(model => model.GetStringValue(null))
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
        [TestMethod]
        public void WithNonNullValueAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.RequiredAsync(model => model.String)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithNullValueAsync()
        {
           RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.RequiredAsync(model => model.Null)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
        #endregion
    }
}
