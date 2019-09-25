﻿using DBFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsNumberTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithStringNum()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNumber(model => model.StringNum)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringNumFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNumber(model => model.StringInvalidNum)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithOptional()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNumber(model => model.StringNum,true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNumber(model => model.StringNumNull,true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsNumber(model => model.StringInvalidNum,true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

    }
}
