using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
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
                UnitTestRules.Delegate(model => model.String, value=> false),
                UnitTestRules.Delegate(model => model.Short, value=> value == 11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 3);
        }


        [TestMethod]
        public void DelegateAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.DelegateAsync(model => model.String, ValidatesAsync)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        
        [TestMethod]
        public void DelegateFailAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.DelegateAsync(model => model.String, InvalidatesAsync),
                    await UnitTestRules.DelegateAsync(model => model.String, async value => await Task.Run(()=>false)),
                    await UnitTestRules.DelegateAsync(model => model.Short, async value => await Task.Run(() => value == 11))
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 3);
            });
        }
    }        
}
