using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class IsAfterTests : ValidatorTestBase
    {
        [TestMethod]
        public void WithValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsAfter(model => model.Today, Yesterday)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsAfter(model => model.Today, Tomorrow)
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
                UnitTestRules.IsAfter(model => model.TodayOptional, Yesterday)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsAfter(model => model.DateTimeNull, Today)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsAfter(model => model.TodayOptional, Tomorrow)
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
                UnitTestRules.IsAfter(model => model.DateTimeString, DateTime1979.AddYears(-1),"dd/MM/yyyy"),
                UnitTestRules.IsAfter(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(-1), "dd-MM-yyyy")
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsAfter(model => model.DateTimeString, DateTime1979,"dd/MM/yyyy"),
                UnitTestRules.IsAfter(model => model.DateTimeStringAlt, DateTime1979Alt, "dd-MM-yyyy")
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
                UnitTestRules.IsAfter(model => model.DateTimeString, DateTime1979.AddYears(-1), null, true),
                UnitTestRules.IsAfter(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(-1), "dd-MM-yyyy", true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalNullValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsAfter(model => model.StringNumNull, DateTime1979.AddYears(-1), null, true),
                UnitTestRules.IsAfter(model => model.StringNumNull, DateTime1979Alt.AddYears(-1), "dd-MM-yyyy", true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithStringOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.IsAfter(model => model.DateTimeString, DateTime1979, null, true),
                UnitTestRules.IsAfter(model => model.DateTimeStringAlt, DateTime1979Alt, "dd-MM-yyyy", true)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 2);
        }



        [TestMethod]
        public void WithValueAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.Today, Yesterday)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithValueFailAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.Today, Tomorrow)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
        [TestMethod]
        public void WithOptionalValueAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.TodayOptional, Yesterday)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithOptionalValueNullAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.DateTimeNull, Today)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithOptionalValueFailAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.TodayOptional, Tomorrow)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }
        [TestMethod]
        public void WithStringValueAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.DateTimeString, DateTime1979.AddYears(-1), "dd/MM/yyyy"),
                    await UnitTestRules.IsAfterAsync(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(-1), "dd-MM-yyyy")
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithStringValueFailAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.DateTimeString, DateTime1979.AddYears(1), "dd/MM/yyyy"),
                    await UnitTestRules.IsAfterAsync(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(1), "dd-MM-yyyy")
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 2);
            });
        }
        [TestMethod]
        public void WithStringOptionalValueAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.DateTimeString, DateTime1979.AddYears(-1), "dd/MM/yyyy", true),
                    await UnitTestRules.IsAfterAsync(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(-1), "dd-MM-yyyy", true)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithStringOptionalNullValueAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.StringNumNull, DateTime1979.AddYears(-1), "dd/MM/yyyy", true),
                    await UnitTestRules.IsAfterAsync(model => model.StringNumNull, DateTime1979Alt.AddYears(-1), "dd-MM-yyyy", true)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithStringOptionalValueFailAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.IsAfterAsync(model => model.DateTimeString, DateTime1979.AddYears(1), "dd/MM/yyyy", true),
                    await UnitTestRules.IsAfterAsync(model => model.DateTimeStringAlt, DateTime1979Alt.AddYears(1), "dd-MM-yyyy", true)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 2);
            });
        }
    }
}
