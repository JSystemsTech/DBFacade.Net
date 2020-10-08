using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class GreaterThanTests : ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThan(model => model.StringInvalidNum, Double10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

        [TestMethod]
        public void WithNonNullableValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThan(model => model.StringNum, Double9),
                UnitTestRules.GreaterThan(model => model.Short, Short9),
                UnitTestRules.GreaterThan(model => model.Int, Int9),
                UnitTestRules.GreaterThan(model => model.Long, Long9),
                UnitTestRules.GreaterThan(model => model.UShort, UShort9),
                UnitTestRules.GreaterThan(model => model.UInt, UInt9),
                UnitTestRules.GreaterThan(model => model.ULong, ULong9),
                UnitTestRules.GreaterThan(model => model.Double, Double9),
                UnitTestRules.GreaterThan(model => model.Float, Float9),
                UnitTestRules.GreaterThan(model => model.Decimal, Decimal9)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThan(model => model.StringNum, Double11),
                UnitTestRules.GreaterThan(model => model.Short, Short11),
                UnitTestRules.GreaterThan(model => model.Int, Int11),
                UnitTestRules.GreaterThan(model => model.Long, Long11),
                UnitTestRules.GreaterThan(model => model.UShort, UShort11),
                UnitTestRules.GreaterThan(model => model.UInt, UInt11),
                UnitTestRules.GreaterThan(model => model.ULong, ULong11),
                UnitTestRules.GreaterThan(model => model.Double, Double11),
                UnitTestRules.GreaterThan(model => model.Float, Float11),
                UnitTestRules.GreaterThan(model => model.Decimal, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThan(model => model.StringNum, Double9),
                UnitTestRules.GreaterThan(model => model.ShortOptional, Short9),
                UnitTestRules.GreaterThan(model => model.IntOptional, Int9),
                UnitTestRules.GreaterThan(model => model.LongOptional, Long9),
                UnitTestRules.GreaterThan(model => model.UShortOptional, UShort9),
                UnitTestRules.GreaterThan(model => model.UIntOptional, UInt9),
                UnitTestRules.GreaterThan(model => model.ULongOptional, ULong9),
                UnitTestRules.GreaterThan(model => model.DoubleOptional, Double9),
                UnitTestRules.GreaterThan(model => model.FloatOptional, Float9),
                UnitTestRules.GreaterThan(model => model.DecimalOptional, Decimal9)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThan(model => model.StringNumNull, Double9),
                UnitTestRules.GreaterThan(model => model.ShortOptionalNull, Short9),
                UnitTestRules.GreaterThan(model => model.IntOptionalNull, Int9),
                UnitTestRules.GreaterThan(model => model.LongOptionalNull, Long9),
                UnitTestRules.GreaterThan(model => model.UShortOptionalNull, UShort9),
                UnitTestRules.GreaterThan(model => model.UIntOptionalNull, UInt9),
                UnitTestRules.GreaterThan(model => model.ULongOptionalNull, ULong9),
                UnitTestRules.GreaterThan(model => model.DoubleOptionalNull, Double9),
                UnitTestRules.GreaterThan(model => model.FloatOptionalNull, Float9),
                UnitTestRules.GreaterThan(model => model.DecimalOptionalNull, Decimal9)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThan(model => model.StringNum, Double11),
                UnitTestRules.GreaterThan(model => model.ShortOptional, Short11),
                UnitTestRules.GreaterThan(model => model.IntOptional, Int11),
                UnitTestRules.GreaterThan(model => model.LongOptional, Long11),
                UnitTestRules.GreaterThan(model => model.UShortOptional, UShort11),
                UnitTestRules.GreaterThan(model => model.UIntOptional, UInt11),
                UnitTestRules.GreaterThan(model => model.ULongOptional, ULong11),
                UnitTestRules.GreaterThan(model => model.DoubleOptional, Double11),
                UnitTestRules.GreaterThan(model => model.FloatOptional, Float11),
                UnitTestRules.GreaterThan(model => model.DecimalOptional, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }





        [TestMethod]
        public void WithInvalidStringNumAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanAsync(model => model.StringInvalidNum, Double10)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }

        [TestMethod]
        public void WithNonNullableValueAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanAsync(model => model.StringNum, Double9),
                    await UnitTestRules.GreaterThanAsync(model => model.Short, Short9),
                    await UnitTestRules.GreaterThanAsync(model => model.Int, Int9),
                    await UnitTestRules.GreaterThanAsync(model => model.Long, Long9),
                    await UnitTestRules.GreaterThanAsync(model => model.UShort, UShort9),
                    await UnitTestRules.GreaterThanAsync(model => model.UInt, UInt9),
                    await UnitTestRules.GreaterThanAsync(model => model.ULong, ULong9),
                    await UnitTestRules.GreaterThanAsync(model => model.Double, Double9),
                    await UnitTestRules.GreaterThanAsync(model => model.Float, Float9),
                    await UnitTestRules.GreaterThanAsync(model => model.Decimal, Decimal9)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithNonNullableValueFailAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanAsync(model => model.StringNum, Double11),
                    await UnitTestRules.GreaterThanAsync(model => model.Short, Short11),
                    await UnitTestRules.GreaterThanAsync(model => model.Int, Int11),
                    await UnitTestRules.GreaterThanAsync(model => model.Long, Long11),
                    await UnitTestRules.GreaterThanAsync(model => model.UShort, UShort11),
                    await UnitTestRules.GreaterThanAsync(model => model.UInt, UInt11),
                    await UnitTestRules.GreaterThanAsync(model => model.ULong, ULong11),
                    await UnitTestRules.GreaterThanAsync(model => model.Double, Double11),
                    await UnitTestRules.GreaterThanAsync(model => model.Float, Float11),
                    await UnitTestRules.GreaterThanAsync(model => model.Decimal, Decimal11)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
        [TestMethod]
        public void WithOptionalValueAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanAsync(model => model.StringNum, Double9),
                    await UnitTestRules.GreaterThanAsync(model => model.ShortOptional, Short9),
                    await UnitTestRules.GreaterThanAsync(model => model.IntOptional, Int9),
                    await UnitTestRules.GreaterThanAsync(model => model.LongOptional, Long9),
                    await UnitTestRules.GreaterThanAsync(model => model.UShortOptional, UShort9),
                    await UnitTestRules.GreaterThanAsync(model => model.UIntOptional, UInt9),
                    await UnitTestRules.GreaterThanAsync(model => model.ULongOptional, ULong9),
                    await UnitTestRules.GreaterThanAsync(model => model.DoubleOptional, Double9),
                    await UnitTestRules.GreaterThanAsync(model => model.FloatOptional, Float9),
                    await UnitTestRules.GreaterThanAsync(model => model.DecimalOptional, Decimal9)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithOptionalValueNullAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanAsync(model => model.StringNumNull, Double9),
                    await UnitTestRules.GreaterThanAsync(model => model.ShortOptionalNull, Short9),
                    await UnitTestRules.GreaterThanAsync(model => model.IntOptionalNull, Int9),
                    await UnitTestRules.GreaterThanAsync(model => model.LongOptionalNull, Long9),
                    await UnitTestRules.GreaterThanAsync(model => model.UShortOptionalNull, UShort9),
                    await UnitTestRules.GreaterThanAsync(model => model.UIntOptionalNull, UInt9),
                    await UnitTestRules.GreaterThanAsync(model => model.ULongOptionalNull, ULong9),
                    await UnitTestRules.GreaterThanAsync(model => model.DoubleOptionalNull, Double9),
                    await UnitTestRules.GreaterThanAsync(model => model.FloatOptionalNull, Float9),
                    await UnitTestRules.GreaterThanAsync(model => model.DecimalOptionalNull, Decimal9)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithOptionalValueFailAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanAsync(model => model.StringNum, Double11),
                    await UnitTestRules.GreaterThanAsync(model => model.ShortOptional, Short11),
                    await UnitTestRules.GreaterThanAsync(model => model.IntOptional, Int11),
                    await UnitTestRules.GreaterThanAsync(model => model.LongOptional, Long11),
                    await UnitTestRules.GreaterThanAsync(model => model.UShortOptional, UShort11),
                    await UnitTestRules.GreaterThanAsync(model => model.UIntOptional, UInt11),
                    await UnitTestRules.GreaterThanAsync(model => model.ULongOptional, ULong11),
                    await UnitTestRules.GreaterThanAsync(model => model.DoubleOptional, Double11),
                    await UnitTestRules.GreaterThanAsync(model => model.FloatOptional, Float11),
                    await UnitTestRules.GreaterThanAsync(model => model.DecimalOptional, Decimal11)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
    }
}
