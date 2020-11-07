using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsSocialSecurityNumberTests : ValidatorTestBase
    {
        [TestMethod]
        public void IsSocialSecurityNumber()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.SSN));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.SSNNoDashes));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.SSNNoDashes,false));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsSocialSecurityNumberFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.InvalidSSN));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.InvalidSSNNoDashes));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.InvalidSSN, false));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.InvalidSSNNoDashes, false));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.SSN, false));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 5);
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.SSN, true, true));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.SSNNoDashes, true, true));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.SSNNoDashes, false, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.StringNumNull, true, true));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.StringNumNull, true, true));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.StringNumNull, false, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.InvalidSSN, true, true));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.InvalidSSNNoDashes, true, true));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.InvalidSSN, false, true));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.InvalidSSNNoDashes, false, true));
                v.Add(v.Rules.IsSocialSecurityNumber(model => model.SSN, false, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 5);
        }



        [TestMethod]
        public void IsSocialSecurityNumberAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.SSN));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.SSNNoDashes));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.SSNNoDashes, false));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsSocialSecurityNumberFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.InvalidSSN));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.InvalidSSNNoDashes));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.InvalidSSN, false));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.InvalidSSNNoDashes, false));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.SSN, false));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 5);
            });
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalValueAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.SSN, true, true));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.SSNNoDashes, true, true));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.SSNNoDashes, false,true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalNullAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.StringNumNull, true, true));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.StringNumNull, true, true));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.StringNumNull, false, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void IsSocialSecurityNumberOptionalFailAsync()
        {            
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.InvalidSSN, true, true));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.InvalidSSNNoDashes, true, true));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.InvalidSSN, false, true));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.InvalidSSNNoDashes, false, true));
                    await v.AddAsync(await v.Rules.IsSocialSecurityNumberAsync(model => model.SSN, false, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 5);
            });
        }
    }
}
