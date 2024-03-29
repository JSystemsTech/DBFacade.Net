﻿using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsDateTimeTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithStringNum()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsDateTime(model => model.DateTimeString));
                v.Add(v.Rules.IsDateTime(model => model.DateTimeStringAlt, DateFormatAlt));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringNumFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsDateTime(model => model.StringInvalidDate));
                v.Add(v.Rules.IsDateTime(model => model.StringInvalidDate, DateFormatAlt));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
        [TestMethod]
        public void WithOptional()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsDateTime(model => model.DateTimeString, null, true));
                v.Add(v.Rules.IsDateTime(model => model.DateTimeStringAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsDateTime(model => model.StringNumNull, null, true));
                v.Add(v.Rules.IsDateTime(model => model.StringNumNull, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsDateTime(model => model.StringInvalidDate, null, true));
                v.Add(v.Rules.IsDateTime(model => model.StringInvalidDate, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }

    }
}
