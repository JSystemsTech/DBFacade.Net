﻿using DBFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsOnOrBeforeTests : ValidatorTestBase
    {
        [TestMethod]
        public void WithValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.Today, Tomorrow)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.Today, Yesterday)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.TodayOptional, Tomorrow)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.DateTimeNull, Today)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.TodayOptional, Yesterday)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithStringValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.DateTimeString, DateTime1979.AddYears(1)),
                UnitTestRules.IsOnOrBefore(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(1), "dd-MM-yyyy")
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.DateTimeString, DateTime1979.AddYears(-1)),
                UnitTestRules.IsOnOrBefore(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(-1), "dd-MM-yyyy")
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
        [TestMethod]
        public void WithStringOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.DateTimeString, DateTime1979.AddYears(1), null, true),
                UnitTestRules.IsOnOrBefore(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(1), "dd-MM-yyyy", true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalNullValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.StringNumNull, DateTime1979.AddYears(1), null, true),
                UnitTestRules.IsOnOrBefore(model => model.StringNumNull, DateTime1979Alt.AddYears(1), "dd-MM-yyyy", true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsOnOrBefore(model => model.DateTimeString, DateTime1979.AddYears(-1), null, true),
                UnitTestRules.IsOnOrBefore(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(-1), "dd-MM-yyyy", true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
    }
}
