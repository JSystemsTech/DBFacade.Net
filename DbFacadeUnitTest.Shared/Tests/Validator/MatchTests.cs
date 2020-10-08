using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class MatchTests:ValidatorTestBase
    {
        [TestMethod]
        public void Match()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.FormatedString,"Formatted[0-9]{10}"),
                UnitTestRules.Match(model => model.FormatedString.ToLower(),"Formatted[0-9]{10}", RegexOptions.IgnoreCase)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MatchFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.FormatedString,"Bad[0-9]{10}"),
                UnitTestRules.Match(model => model.FormatedString.ToLower(),"Bad[0-9]{10}", RegexOptions.IgnoreCase)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
        [TestMethod]
        public void MatchOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.FormatedString,"Formatted[0-9]{10}", true),
                UnitTestRules.Match(model => model.FormatedString.ToLower(),"Formatted[0-9]{10}", RegexOptions.IgnoreCase, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MatchOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.StringNumNull,"Formatted[0-9]{10}", true),
                UnitTestRules.Match(model => model.StringNumNull,"Formatted[0-9]{10}", RegexOptions.IgnoreCase, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MatchOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.FormatedString,"Bad[0-9]{10}",true),
                UnitTestRules.Match(model => model.FormatedString.ToLower(),"Bad[0-9]{10}", RegexOptions.IgnoreCase,true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }



        [TestMethod]
        public void MatchAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.FormatedString,"Formatted[0-9]{10}"),
                UnitTestRules.Match(model => model.FormatedString.ToLower(),"Formatted[0-9]{10}", RegexOptions.IgnoreCase)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MatchFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.FormatedString,"Bad[0-9]{10}"),
                UnitTestRules.Match(model => model.FormatedString.ToLower(),"Bad[0-9]{10}", RegexOptions.IgnoreCase)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
        [TestMethod]
        public void MatchOptionalValueAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.FormatedString,"Formatted[0-9]{10}", true),
                UnitTestRules.Match(model => model.FormatedString.ToLower(),"Formatted[0-9]{10}", RegexOptions.IgnoreCase, true)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MatchOptionalNullAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.StringNumNull,"Formatted[0-9]{10}", true),
                UnitTestRules.Match(model => model.StringNumNull,"Formatted[0-9]{10}", RegexOptions.IgnoreCase, true)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void MatchOptionalFailAsync()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Match(model => model.FormatedString,"Bad[0-9]{10}",true),
                UnitTestRules.Match(model => model.FormatedString.ToLower(),"Bad[0-9]{10}", RegexOptions.IgnoreCase,true)
            };
            Task<IValidationResult> result = Validator.ValidateAsync(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
    }
}
