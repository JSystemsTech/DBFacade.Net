using DbFacade.DataLayer.Models.Validators;
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
        

        [TestMethod]
        public void IsNullOrEmptyAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsNullOrEmptyAsync(model => model.StringNumNull),
                    await UnitTestRules.IsNullOrEmptyAsync(model => string.Empty)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsNullOrEmptyFailAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsNullOrEmptyAsync(model => model.String)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }

        [TestMethod]
        public void IsNullOrWhiteSpaceAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsNullOrWhiteSpaceAsync(model => model.StringNumNull),
                    await UnitTestRules.IsNullOrWhiteSpaceAsync(model => string.Empty),
                    await UnitTestRules.IsNullOrWhiteSpaceAsync(model => " "),
                    await UnitTestRules.IsNullOrWhiteSpaceAsync(model => "      ")
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsNullOrWhiteSpaceFailAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsNullOrWhiteSpaceAsync(model => model.String)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
    }        
}
