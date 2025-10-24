using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsBeforeTests : ValidatorTestBase
    {
        private static DateTime ValidValue = Tomorrow;
        private static DateTime InvalidValue = Yesterday;
        private static DateTime ValidDateTimeValue = DateTime1979.AddYears(1);
        private static DateTime ValidDateTimeValueAlt = DateTime1979Alt.AddYears(1);
        private static DateTime InvalidDateTimeValue = DateTime1979.AddYears(-1);
        private static DateTime InvalidDateTimeValueAlt = DateTime1979Alt.AddYears(-1);

        [TestMethod]
        public void WithValue()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.Today, ValidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithValueFail()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.Today, InvalidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.TodayOptional, ValidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.DateTimeNull, ValidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.TodayOptional, InvalidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithStringValue()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.DateTimeString, ValidDateTimeValue, DateFormat));
                v.Add(v.Rules.IsBefore(model => model.DateTimeStringAlt, ValidDateTimeValueAlt, DateFormatAlt));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringValueFail()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.DateTimeString, InvalidDateTimeValue, DateFormat));
                v.Add(v.Rules.IsBefore(model => model.DateTimeStringAlt, InvalidDateTimeValueAlt, DateFormatAlt));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }
        [TestMethod]
        public void WithStringOptionalValue()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.DateTimeString, ValidDateTimeValue, DateFormat, true));
                v.Add(v.Rules.IsBefore(model => model.DateTimeStringAlt, ValidDateTimeValueAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalNullValue()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.StringNumNull, ValidDateTimeValue, DateFormat, true));
                v.Add(v.Rules.IsBefore(model => model.StringNumNull, ValidDateTimeValueAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalValueFail()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.DateTimeString, InvalidDateTimeValue, DateFormat, true));
                v.Add(v.Rules.IsBefore(model => model.DateTimeStringAlt, InvalidDateTimeValueAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }

    }
}
