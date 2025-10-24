using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class LengthEqualsTests : ValidatorTestBase
    {
        [TestMethod]
        public void LengthEquals()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.LengthEquals(model => model.String, 14));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void LengthEqualsFail()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.LengthEquals(model => model.String, 20));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void LengthEqualsOptionalValue()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.LengthEquals(model => model.String, 14, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void LengthEqualsOptionalNull()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.LengthEquals(model => model.StringNumNull, 14, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void LengthEqualsOptionalFail()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.LengthEquals(model => model.String, 20, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

    }
}
