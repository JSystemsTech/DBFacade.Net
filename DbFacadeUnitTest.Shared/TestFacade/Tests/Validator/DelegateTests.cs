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
                v.Add(v.Rules.Delegate(model => {
                    return Validates(model.String) || model.Short == 10;
                }, "Did not pass custom validation"));
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
                v.Add(v.Rules.Delegate(model => {
                    return Invalidates(model.String) || model.Short == 11;
                }, "Did not pass custom validation"));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 4);
        }

    }        
}
