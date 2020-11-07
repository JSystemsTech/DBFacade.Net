using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsNDigitStringTests : ValidatorTestBase
    {
        [TestMethod]
        public void NDigitString()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNDigitString(model => model.FiveDigitString, 5));
                v.Add(v.Rules.IsNDigitString(model => model.TenDigitString, 10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void NDigitStringFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNDigitString(model => model.FiveDigitString, 10));
                v.Add(v.Rules.IsNDigitString(model => model.TenDigitString, 5));
                v.Add(v.Rules.IsNDigitString(model => model.TenDigitString, -1));
                v.Add(v.Rules.IsNDigitString(model => model.String, 5));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 4);
        }
        [TestMethod]
        public void NDigitStringOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNDigitString(model => model.FiveDigitString, 5, true));
                v.Add(v.Rules.IsNDigitString(model => model.TenDigitString, 10, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNDigitString(model => model.StringNumNull, 5, true));
                v.Add(v.Rules.IsNDigitString(model => model.StringNumNull, 10, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MinLengthOptionalFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNDigitString(model => model.FiveDigitString, 10, true));
                v.Add(v.Rules.IsNDigitString(model => model.TenDigitString, 5, true));
                v.Add(v.Rules.IsNDigitString(model => model.TenDigitString, -1, true));
                v.Add(v.Rules.IsNDigitString(model => model.String, 5, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 4);
        }

        [TestMethod]
        public void NDigitStringAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.FiveDigitString, 5));
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.TenDigitString, 10));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void NDigitStringFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.FiveDigitString, 10));
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.TenDigitString, 5));
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.TenDigitString, -1));
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.String, 5));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 4);
            });
        }
        [TestMethod]
        public void NDigitStringOptionalValueAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.FiveDigitString, 5, true));
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.TenDigitString, 10, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void MinLengthOptionalNullAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.StringNumNull, 5, true));
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.StringNumNull, 10, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void MinLengthOptionalFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.FiveDigitString, 10, true));
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.TenDigitString, 5, true));
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.TenDigitString, -1, true));
                    await v.AddAsync(await v.Rules.IsNDigitStringAsync(model => model.String, 5, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 4);
            });
        }
    }
}
