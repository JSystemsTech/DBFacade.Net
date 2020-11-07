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
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.Today, ValidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.Today, InvalidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.TodayOptional, ValidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.DateTimeNull, ValidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.TodayOptional, InvalidValue));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }
        [TestMethod]
        public void WithStringValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.DateTimeString, ValidDateTimeValue, DateFormat));
                v.Add(v.Rules.IsBefore(model => model.DateTimeStringAlt, ValidDateTimeValueAlt, DateFormatAlt));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
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
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.DateTimeString, ValidDateTimeValue, DateFormat, true));
                v.Add(v.Rules.IsBefore(model => model.DateTimeStringAlt, ValidDateTimeValueAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalNullValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.StringNumNull, ValidDateTimeValue, DateFormat, true));
                v.Add(v.Rules.IsBefore(model => model.StringNumNull, ValidDateTimeValueAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.IsBefore(model => model.DateTimeString, InvalidDateTimeValue, DateFormat, true));
                v.Add(v.Rules.IsBefore(model => model.DateTimeStringAlt, InvalidDateTimeValueAlt, DateFormatAlt, true));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }

        [TestMethod]
        public void WithValueAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.Today, ValidValue));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithValueFailAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.Today, InvalidValue));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
        [TestMethod]
        public void WithOptionalValueAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.TodayOptional, ValidValue));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithOptionalValueNullAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.DateTimeNull, ValidValue));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithOptionalValueFailAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.TodayOptional, InvalidValue));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
        [TestMethod]
        public void WithStringValueAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.DateTimeString, ValidDateTimeValue, DateFormat));
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.DateTimeStringAlt, ValidDateTimeValueAlt, DateFormatAlt));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithStringValueFailAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.DateTimeString, InvalidDateTimeValue, DateFormat));
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.DateTimeStringAlt, InvalidDateTimeValueAlt, DateFormatAlt));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 2);
            });
        }
        [TestMethod]
        public void WithStringOptionalValueAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.DateTimeString, ValidDateTimeValue, DateFormat, true));
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.DateTimeStringAlt, ValidDateTimeValueAlt, DateFormatAlt, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithStringOptionalNullValueAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.StringNumNull, ValidDateTimeValue, DateFormat, true));
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.StringNumNull, ValidDateTimeValueAlt, DateFormatAlt, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithStringOptionalValueFailAsync()
        {
            RunAsAsyc(async () => {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.DateTimeString, InvalidDateTimeValue, DateFormat, true));
                    await v.AddAsync(await v.Rules.IsBeforeAsync(model => model.DateTimeStringAlt, InvalidDateTimeValueAlt, DateFormatAlt, true));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 2);
            });
        }
    }
}
