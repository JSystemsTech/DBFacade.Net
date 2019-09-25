using DBFacade.DataLayer.Models.Validators;
using DBFacade.DataLayer.Models.Validators.Rules;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class EmailTests:ValidatorTestBase
    {
        [TestMethod]
        public void IsEmail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsEmailFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.InvalidEmail)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WhitelistEmail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email,new string[]{ "gmail.com"}, EmailDomainMode.Whitelist)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WhitelistEmailFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email,new string[]{ "hotmail.com"}, EmailDomainMode.Whitelist)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void BlacklistEmail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email,new string[]{ "hotmail.com"}, EmailDomainMode.Blacklist)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void BlacklistEmailFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email,new string[]{ "gmail.com"}, EmailDomainMode.Blacklist)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void IsEmailOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsEmailOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.EmailNull, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void IsEmailOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.InvalidEmail, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WhitelistEmailOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email,new string[]{ "gmail.com"}, EmailDomainMode.Whitelist, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WhitelistEmailOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.EmailNull,new string[]{ "gmail.com"}, EmailDomainMode.Whitelist, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WhitelistEmailOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email,new string[]{ "hotmail.com"}, EmailDomainMode.Whitelist)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void BlacklistEmailOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email,new string[]{ "hotmail.com"}, EmailDomainMode.Blacklist, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void BlacklistEmailOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.EmailNull,new string[]{ "hotmail.com"}, EmailDomainMode.Blacklist, true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void BlacklistEmailOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Email(model => model.Email,new string[]{ "gmail.com"}, EmailDomainMode.Blacklist,true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
    }
}
