using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class BasicStringTests : ValidatorTestBase
    {
        [TestMethod]
        public void IsNullOrEmpty()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v=> {
                v.Add(v.Rules.IsNullOrEmpty(model => model.StringNumNull));
                v.Add(v.Rules.IsNullOrEmpty(model => string.Empty));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsNullOrEmptyFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNullOrEmpty(model => model.String));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void IsNullOrWhiteSpace()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNullOrWhiteSpace(model => model.StringNumNull));
                v.Add(v.Rules.IsNullOrWhiteSpace(model => string.Empty));
                v.Add(v.Rules.IsNullOrWhiteSpace(model => " "));
                v.Add(v.Rules.IsNullOrWhiteSpace(model => "      "));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsNullOrWhiteSpaceFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNullOrWhiteSpace(model => model.String));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        

        [TestMethod]
        public void IsNullOrEmptyAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync (await v.Rules.IsNullOrEmptyAsync(model => model.StringNumNull));
                    await v.AddAsync (await v.Rules.IsNullOrEmptyAsync(model => string.Empty));
                });
                var result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsNullOrEmptyFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNullOrEmptyAsync(model => model.String));
                });
                var result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }

        [TestMethod]
        public void IsNullOrWhiteSpaceAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNullOrWhiteSpaceAsync(model => model.StringNumNull));
                    await v.AddAsync(await v.Rules.IsNullOrWhiteSpaceAsync(model => string.Empty));
                    await v.AddAsync(await v.Rules.IsNullOrWhiteSpaceAsync(model => " "));
                    await v.AddAsync(await v.Rules.IsNullOrWhiteSpaceAsync(model => "      "));
                });
                var result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsNullOrWhiteSpaceFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNullOrWhiteSpaceAsync(model => model.String));
                });
                var result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
    }        
}
