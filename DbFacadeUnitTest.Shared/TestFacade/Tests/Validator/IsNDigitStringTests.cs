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

    }
}
