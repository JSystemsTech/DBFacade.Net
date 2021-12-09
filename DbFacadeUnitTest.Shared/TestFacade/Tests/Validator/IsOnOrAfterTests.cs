﻿using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsOnOrAfterTests : ValidatorTestBase
    {
        private static DateTime ValidValue = Yesterday;
        private static DateTime InvalidValue = Tomorrow;
        private static DateTime ValidDateTimeValue = DateTime1979.AddYears(-1);
        private static DateTime ValidDateTimeValueAlt = DateTime1979Alt.AddYears(-1);
        private static DateTime InvalidDateTimeValue = DateTime1979.AddYears(1);
        private static DateTime InvalidDateTimeValueAlt = DateTime1979Alt.AddYears(1);

        [TestMethod]
        public void WithValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.Today, ValidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.Today, InvalidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.TodayOptional, ValidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.DateTimeNull, ValidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.TodayOptional, InvalidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithStringValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.DateTimeString, ValidDateTimeValue, DateFormat));
                v.Add(v.Rules.IsOnOrAfter(model => model.DateTimeStringAlt, ValidDateTimeValueAlt, DateFormatAlt));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.DateTimeString, InvalidDateTimeValue, DateFormat));
                v.Add(v.Rules.IsOnOrAfter(model => model.DateTimeStringAlt, InvalidDateTimeValueAlt, DateFormatAlt));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
        [TestMethod]
        public void WithStringOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.DateTimeString, ValidDateTimeValue, DateFormat, true));
                v.Add(v.Rules.IsOnOrAfter(model => model.DateTimeStringAlt, ValidDateTimeValueAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalNullValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.StringNumNull, ValidDateTimeValue, DateFormat, true));
                v.Add(v.Rules.IsOnOrAfter(model => model.StringNumNull, ValidDateTimeValueAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsOnOrAfter(model => model.DateTimeString, InvalidDateTimeValue, DateFormat, true));
                v.Add(v.Rules.IsOnOrAfter(model => model.DateTimeStringAlt, InvalidDateTimeValueAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }

    }
}
