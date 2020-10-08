using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsSocialSecurityNumberTests : ValidatorTestBase
    {
        [TestMethod]
        public void IsSocialSecurityNumber()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsSocialSecurityNumber(model => model.SSN),
                UnitTestRules.IsSocialSecurityNumber(model => model.SSNNoDashes),
                UnitTestRules.IsSocialSecurityNumber(model => model.SSNNoDashes, false)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsSocialSecurityNumberFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsSocialSecurityNumber(model => model.InvalidSSN),
                UnitTestRules.IsSocialSecurityNumber(model => model.InvalidSSNNoDashes),
                UnitTestRules.IsSocialSecurityNumber(model => model.InvalidSSN, false),
                UnitTestRules.IsSocialSecurityNumber(model => model.InvalidSSNNoDashes, false),
                UnitTestRules.IsSocialSecurityNumber(model => model.SSN, false)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 5);
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsSocialSecurityNumber(model => model.SSN, true, true),
                UnitTestRules.IsSocialSecurityNumber(model => model.SSNNoDashes, true, true),
                UnitTestRules.IsSocialSecurityNumber(model => model.SSNNoDashes, false, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsSocialSecurityNumber(model => model.StringNumNull, true, true),
                UnitTestRules.IsSocialSecurityNumber(model => model.StringNumNull, true, true),
                UnitTestRules.IsSocialSecurityNumber(model => model.StringNumNull, false, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsSocialSecurityNumber(model => model.InvalidSSN, true, true),
                UnitTestRules.IsSocialSecurityNumber(model => model.InvalidSSNNoDashes, true, true),
                UnitTestRules.IsSocialSecurityNumber(model => model.InvalidSSN, false, true),
                UnitTestRules.IsSocialSecurityNumber(model => model.InvalidSSNNoDashes, false, true),
                UnitTestRules.IsSocialSecurityNumber(model => model.SSN, false, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 5);
        }



        [TestMethod]
        public void IsSocialSecurityNumberAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.SSN),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.SSNNoDashes),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.SSNNoDashes, false)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsSocialSecurityNumberFailAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.InvalidSSN),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.InvalidSSNNoDashes),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.InvalidSSN, false),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.InvalidSSNNoDashes, false),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.SSN, false)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 5);
            });
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalValueAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.SSN, true, true),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.SSNNoDashes, true, true),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.SSNNoDashes, false, true)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalNullAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.StringNumNull, true, true),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.StringNumNull, true, true),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.StringNumNull, false, true)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalFailAsync()
        {            
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.InvalidSSN, true, true),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.InvalidSSNNoDashes, true, true),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.InvalidSSN, false, true),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.InvalidSSNNoDashes, false, true),
                    await UnitTestRules.IsSocialSecurityNumberAsync(model => model.SSN, false, true)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 5);
            });
        }
    }
}
