using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class MatchTests:ValidatorTestBase
    {
        [TestMethod]
        public void Match()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Match(model => model.FormatedString, "Formatted[0-9]{10}"));
                v.Add(v.Rules.Match(model => model.FormatedString.ToLower(), "Formatted[0-9]{10}", RegexOptions.IgnoreCase));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MatchFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Match(model => model.FormatedString, "Bad[0-9]{10}"));
                v.Add(v.Rules.Match(model => model.FormatedString.ToLower(), "Bad[0-9]{10}", RegexOptions.IgnoreCase));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
        [TestMethod]
        public void MatchOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Match(model => model.FormatedString, "Formatted[0-9]{10}", true));
                v.Add(v.Rules.Match(model => model.FormatedString.ToLower(), "Formatted[0-9]{10}", RegexOptions.IgnoreCase, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MatchOptionalNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Match(model => model.StringNumNull, "Formatted[0-9]{10}", true));
                v.Add(v.Rules.Match(model => model.StringNumNull, "Formatted[0-9]{10}", RegexOptions.IgnoreCase, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MatchOptionalFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Match(model => model.FormatedString, "Bad[0-9]{10}", true));
                v.Add(v.Rules.Match(model => model.FormatedString.ToLower(), "Bad[0-9]{10}", RegexOptions.IgnoreCase, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }

    }
}
