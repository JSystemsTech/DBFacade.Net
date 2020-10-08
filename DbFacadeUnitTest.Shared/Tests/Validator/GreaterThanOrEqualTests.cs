using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class GreaterThanOrEqualTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThanOrEqual(model => model.StringInvalidNum, Double10)
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
                UnitTestRules.GreaterThanOrEqual(model => model.StringNum, Double9),
                UnitTestRules.GreaterThanOrEqual(model => model.Short, Short9),
                UnitTestRules.GreaterThanOrEqual(model => model.Int, Int9),
                UnitTestRules.GreaterThanOrEqual(model => model.Long, Long9),
                UnitTestRules.GreaterThanOrEqual(model => model.UShort, UShort9),
                UnitTestRules.GreaterThanOrEqual(model => model.UInt, UInt9),
                UnitTestRules.GreaterThanOrEqual(model => model.ULong, ULong9),
                UnitTestRules.GreaterThanOrEqual(model => model.Double, Double9),
                UnitTestRules.GreaterThanOrEqual(model => model.Float, Float9),
                UnitTestRules.GreaterThanOrEqual(model => model.Decimal, Decimal9),

                UnitTestRules.GreaterThanOrEqual(model => model.StringNum, Double10),
                UnitTestRules.GreaterThanOrEqual(model => model.Short, Short10),
                UnitTestRules.GreaterThanOrEqual(model => model.Int, Int10),
                UnitTestRules.GreaterThanOrEqual(model => model.Long, Long10),
                UnitTestRules.GreaterThanOrEqual(model => model.UShort, UShort10),
                UnitTestRules.GreaterThanOrEqual(model => model.UInt, UInt10),
                UnitTestRules.GreaterThanOrEqual(model => model.ULong, ULong10),
                UnitTestRules.GreaterThanOrEqual(model => model.Double, Double10),
                UnitTestRules.GreaterThanOrEqual(model => model.Float, Float10),
                UnitTestRules.GreaterThanOrEqual(model => model.Decimal, Decimal10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThanOrEqual(model => model.StringNum, Double11),
                UnitTestRules.GreaterThanOrEqual(model => model.Short, Short11),
                UnitTestRules.GreaterThanOrEqual(model => model.Int, Int11),
                UnitTestRules.GreaterThanOrEqual(model => model.Long, Long11),
                UnitTestRules.GreaterThanOrEqual(model => model.UShort, UShort11),
                UnitTestRules.GreaterThanOrEqual(model => model.UInt, UInt11),
                UnitTestRules.GreaterThanOrEqual(model => model.ULong, ULong11),
                UnitTestRules.GreaterThanOrEqual(model => model.Double, Double11),
                UnitTestRules.GreaterThanOrEqual(model => model.Float, Float11),
                UnitTestRules.GreaterThanOrEqual(model => model.Decimal, Decimal11)
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
                UnitTestRules.GreaterThanOrEqual(model => model.StringNum, Double9),
                UnitTestRules.GreaterThanOrEqual(model => model.ShortOptional, Short9),
                UnitTestRules.GreaterThanOrEqual(model => model.IntOptional, Int9),
                UnitTestRules.GreaterThanOrEqual(model => model.LongOptional, Long9),
                UnitTestRules.GreaterThanOrEqual(model => model.UShortOptional, UShort9),
                UnitTestRules.GreaterThanOrEqual(model => model.UIntOptional, UInt9),
                UnitTestRules.GreaterThanOrEqual(model => model.ULongOptional, ULong9),
                UnitTestRules.GreaterThanOrEqual(model => model.DoubleOptional, Double9),
                UnitTestRules.GreaterThanOrEqual(model => model.FloatOptional, Float9),
                UnitTestRules.GreaterThanOrEqual(model => model.DecimalOptional, Decimal9),

                UnitTestRules.GreaterThanOrEqual(model => model.StringNum, Double10),
                UnitTestRules.GreaterThanOrEqual(model => model.ShortOptional, Short10),
                UnitTestRules.GreaterThanOrEqual(model => model.IntOptional, Int10),
                UnitTestRules.GreaterThanOrEqual(model => model.LongOptional, Long10),
                UnitTestRules.GreaterThanOrEqual(model => model.UShortOptional, UShort10),
                UnitTestRules.GreaterThanOrEqual(model => model.UIntOptional, UInt10),
                UnitTestRules.GreaterThanOrEqual(model => model.ULongOptional, ULong10),
                UnitTestRules.GreaterThanOrEqual(model => model.DoubleOptional, Double10),
                UnitTestRules.GreaterThanOrEqual(model => model.FloatOptional, Float10),
                UnitTestRules.GreaterThanOrEqual(model => model.DecimalOptional, Decimal10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThanOrEqual(model => model.StringNumNull, Double9),
                UnitTestRules.GreaterThanOrEqual(model => model.ShortOptionalNull, Short9),
                UnitTestRules.GreaterThanOrEqual(model => model.IntOptionalNull, Int9),
                UnitTestRules.GreaterThanOrEqual(model => model.LongOptionalNull, Long9),
                UnitTestRules.GreaterThanOrEqual(model => model.UShortOptionalNull, UShort9),
                UnitTestRules.GreaterThanOrEqual(model => model.UIntOptionalNull, UInt9),
                UnitTestRules.GreaterThanOrEqual(model => model.ULongOptionalNull, ULong9),
                UnitTestRules.GreaterThanOrEqual(model => model.DoubleOptionalNull, Double9),
                UnitTestRules.GreaterThanOrEqual(model => model.FloatOptionalNull, Float9),
                UnitTestRules.GreaterThanOrEqual(model => model.DecimalOptionalNull, Decimal9)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThanOrEqual(model => model.StringNum, Double11),
                UnitTestRules.GreaterThanOrEqual(model => model.ShortOptional, Short11),
                UnitTestRules.GreaterThanOrEqual(model => model.IntOptional, Int11),
                UnitTestRules.GreaterThanOrEqual(model => model.LongOptional, Long11),
                UnitTestRules.GreaterThanOrEqual(model => model.UShortOptional, UShort11),
                UnitTestRules.GreaterThanOrEqual(model => model.UIntOptional, UInt11),
                UnitTestRules.GreaterThanOrEqual(model => model.ULongOptional, ULong11),
                UnitTestRules.GreaterThanOrEqual(model => model.DoubleOptional, Double11),
                UnitTestRules.GreaterThanOrEqual(model => model.FloatOptional, Float11),
                UnitTestRules.GreaterThanOrEqual(model => model.DecimalOptional, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }



        [TestMethod]
        public void WithInvalidStringNumAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.StringInvalidNum, Double10)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }

        [TestMethod]
        public void WithNonNullableValueAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.StringNum, Double9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Short, Short9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Int, Int9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Long, Long9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UShort, UShort9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UInt, UInt9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ULong, ULong9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Double, Double9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Float, Float9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Decimal, Decimal9),

                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.StringNum, Double10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Short, Short10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Int, Int10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Long, Long10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UShort, UShort10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UInt, UInt10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ULong, ULong10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Double, Double10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Float, Float10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Decimal, Decimal10)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithNonNullableValueFailAsync()
        {
           RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.StringNum, Double11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Short, Short11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Int, Int11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Long, Long11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UShort, UShort11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UInt, UInt11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ULong, ULong11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Double, Double11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Float, Float11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.Decimal, Decimal11)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
        [TestMethod]
        public void WithOptionalValueAsync()
        {
            RunAsAsyc(async () => {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.StringNum, Double9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ShortOptional, Short9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.IntOptional, Int9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.LongOptional, Long9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UShortOptional, UShort9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UIntOptional, UInt9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ULongOptional, ULong9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.DoubleOptional, Double9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.FloatOptional, Float9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.DecimalOptional, Decimal9),

                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.StringNum, Double10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ShortOptional, Short10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.IntOptional, Int10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.LongOptional, Long10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UShortOptional, UShort10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UIntOptional, UInt10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ULongOptional, ULong10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.DoubleOptional, Double10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.FloatOptional, Float10),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.DecimalOptional, Decimal10)
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
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.StringNumNull, Double9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ShortOptionalNull, Short9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.IntOptionalNull, Int9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.LongOptionalNull, Long9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UShortOptionalNull, UShort9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UIntOptionalNull, UInt9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ULongOptionalNull, ULong9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.DoubleOptionalNull, Double9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.FloatOptionalNull, Float9),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.DecimalOptionalNull, Decimal9)
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
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.StringNum, Double11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ShortOptional, Short11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.IntOptional, Int11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.LongOptional, Long11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UShortOptional, UShort11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.UIntOptional, UInt11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.ULongOptional, ULong11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.DoubleOptional, Double11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.FloatOptional, Float11),
                    await UnitTestRules.GreaterThanOrEqualAsync(model => model.DecimalOptional, Decimal11)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
    }
}
