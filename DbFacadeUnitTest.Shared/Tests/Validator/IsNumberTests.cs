using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsNumberTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithStringNum()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNumber(model => model.StringNum));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringNumFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNumber(model => model.StringInvalidNum));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithOptional()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNumber(model => model.StringNum, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNumber(model => model.StringNumNull, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNumber(model => model.StringInvalidNum, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }


        [TestMethod]
        public void WithStringNumAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNumberAsync(model => model.StringNum));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithStringNumFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNumberAsync(model => model.StringInvalidNum));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
        [TestMethod]
        public void WithOptionalAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNumberAsync(model => model.StringNum, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });

        }
        [TestMethod]
        public void WithOptionalNullAsync()
        {            
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNumberAsync(model => model.StringNumNull, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithOptionalFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsNumberAsync(model => model.StringInvalidNum, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
    }
}
