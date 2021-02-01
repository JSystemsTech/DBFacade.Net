using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class DelegateTests : ValidatorTestBase
    {
        private bool Validates(string value) => true;
        private bool Invalidates(string value) => !Validates(value);

        private async Task<bool> ValidatesAsync(string value)
        {
            await Task.CompletedTask;
            return true;
        }
        private async Task<bool> InvalidatesAsync(string value)
        {
            await Task.CompletedTask;
            return false;
        }
        [TestMethod]
        public void Delegate()
        {            
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Delegate(model => model.String, Validates));
                v.Add(v.Rules.Delegate(model => model.String, value => true));
                v.Add(v.Rules.Delegate(model => model.Short, value => value == 10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void DelegateFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Delegate(model => model.String, Invalidates));
                v.Add(v.Rules.Delegate(model => model.String, value => false));
                v.Add(v.Rules.Delegate(model => model.Short, value => value == 11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 3);
        }


        [TestMethod]
        public void DelegateAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.DelegateAsync(model => model.String, ValidatesAsync));
                    await v.AddAsync(await v.Rules.DelegateAsync(model => model.String, async value => await Task.Run(() => true)));
                    await v.AddAsync(await v.Rules.DelegateAsync(model => model.Short, async value => await Task.Run(() => value == 10)));
                });
                var result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        
        [TestMethod]
        public void DelegateFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.DelegateAsync(model => model.String, InvalidatesAsync));
                    await v.AddAsync(await v.Rules.DelegateAsync(model => model.String, async value => await Task.Run(() => false)));
                    await v.AddAsync(await v.Rules.DelegateAsync(model => model.Short, async value => await Task.Run(() => value == 11)));
                });
                var result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 3);
            });
        }
    }        
}
