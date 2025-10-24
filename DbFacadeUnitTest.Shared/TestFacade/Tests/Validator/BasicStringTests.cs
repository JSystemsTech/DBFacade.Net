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
            var Validator = MakeValidator<UnitTestDbParams>(v=> {
                v.Add(v.Rules.IsNullOrEmpty(model => model.StringNumNull));
                v.Add(v.Rules.IsNullOrEmpty(model => string.Empty));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsNullOrEmptyFail()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNullOrEmpty(model => model.String));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void IsNullOrWhiteSpace()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
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
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsNullOrWhiteSpace(model => model.String));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        } 

        
    }        
}
