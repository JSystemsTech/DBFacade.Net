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

    }
}
