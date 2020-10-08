using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class LessThanOrEqualTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LessThanOrEqual(model => model.StringInvalidNum, Double10)
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
                UnitTestRules.LessThanOrEqual(model => model.StringNum, Double11),
                UnitTestRules.LessThanOrEqual(model => model.Short, Short11),
                UnitTestRules.LessThanOrEqual(model => model.Int, Int11),
                UnitTestRules.LessThanOrEqual(model => model.Long, Long11),
                UnitTestRules.LessThanOrEqual(model => model.UShort, UShort11),
                UnitTestRules.LessThanOrEqual(model => model.UInt, UInt11),
                UnitTestRules.LessThanOrEqual(model => model.ULong, ULong11),
                UnitTestRules.LessThanOrEqual(model => model.Double, Double11),
                UnitTestRules.LessThanOrEqual(model => model.Float, Float11),
                UnitTestRules.LessThanOrEqual(model => model.Decimal, Decimal11),

                UnitTestRules.LessThanOrEqual(model => model.StringNum, Double10),
                UnitTestRules.LessThanOrEqual(model => model.Short, Short10),
                UnitTestRules.LessThanOrEqual(model => model.Int, Int10),
                UnitTestRules.LessThanOrEqual(model => model.Long, Long10),
                UnitTestRules.LessThanOrEqual(model => model.UShort, UShort10),
                UnitTestRules.LessThanOrEqual(model => model.UInt, UInt10),
                UnitTestRules.LessThanOrEqual(model => model.ULong, ULong10),
                UnitTestRules.LessThanOrEqual(model => model.Double, Double10),
                UnitTestRules.LessThanOrEqual(model => model.Float, Float10),
                UnitTestRules.LessThanOrEqual(model => model.Decimal, Decimal10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LessThanOrEqual (model => model.StringNum, Double9),
                UnitTestRules.LessThanOrEqual (model => model.Short, Short9),
                UnitTestRules.LessThanOrEqual (model => model.Int, Int9),
                UnitTestRules.LessThanOrEqual (model => model.Long, Long9),
                UnitTestRules.LessThanOrEqual (model => model.UShort, UShort9),
                UnitTestRules.LessThanOrEqual (model => model.UInt, UInt9),
                UnitTestRules.LessThanOrEqual (model => model.ULong, ULong9),
                UnitTestRules.LessThanOrEqual (model => model.Double, Double9),
                UnitTestRules.LessThanOrEqual (model => model.Float, Float9),
                UnitTestRules.LessThanOrEqual (model => model.Decimal, Decimal9)
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
                UnitTestRules.LessThanOrEqual(model => model.StringNum, Double11),
                UnitTestRules.LessThanOrEqual(model => model.ShortOptional, Short11),
                UnitTestRules.LessThanOrEqual(model => model.IntOptional, Int11),
                UnitTestRules.LessThanOrEqual(model => model.LongOptional, Long11),
                UnitTestRules.LessThanOrEqual(model => model.UShortOptional, UShort11),
                UnitTestRules.LessThanOrEqual(model => model.UIntOptional, UInt11),
                UnitTestRules.LessThanOrEqual(model => model.ULongOptional, ULong11),
                UnitTestRules.LessThanOrEqual(model => model.DoubleOptional, Double11),
                UnitTestRules.LessThanOrEqual(model => model.FloatOptional, Float11),
                UnitTestRules.LessThanOrEqual(model => model.DecimalOptional, Decimal11),

                UnitTestRules.LessThanOrEqual(model => model.StringNum, Double10),
                UnitTestRules.LessThanOrEqual(model => model.ShortOptional, Short10),
                UnitTestRules.LessThanOrEqual(model => model.IntOptional, Int10),
                UnitTestRules.LessThanOrEqual(model => model.LongOptional, Long10),
                UnitTestRules.LessThanOrEqual(model => model.UShortOptional, UShort10),
                UnitTestRules.LessThanOrEqual(model => model.UIntOptional, UInt10),
                UnitTestRules.LessThanOrEqual(model => model.ULongOptional, ULong10),
                UnitTestRules.LessThanOrEqual(model => model.DoubleOptional, Double10),
                UnitTestRules.LessThanOrEqual(model => model.FloatOptional, Float10),
                UnitTestRules.LessThanOrEqual(model => model.DecimalOptional, Decimal10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LessThanOrEqual(model => model.StringNumNull, Double11),
                UnitTestRules.LessThanOrEqual(model => model.ShortOptionalNull, Short11),
                UnitTestRules.LessThanOrEqual(model => model.IntOptionalNull, Int11),
                UnitTestRules.LessThanOrEqual(model => model.LongOptionalNull, Long11),
                UnitTestRules.LessThanOrEqual(model => model.UShortOptionalNull, UShort11),
                UnitTestRules.LessThanOrEqual(model => model.UIntOptionalNull, UInt11),
                UnitTestRules.LessThanOrEqual(model => model.ULongOptionalNull, ULong11),
                UnitTestRules.LessThanOrEqual(model => model.DoubleOptionalNull, Double11),
                UnitTestRules.LessThanOrEqual(model => model.FloatOptionalNull, Float11),
                UnitTestRules.LessThanOrEqual(model => model.DecimalOptionalNull, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LessThanOrEqual(model => model.StringNum, Double9),
                UnitTestRules.LessThanOrEqual(model => model.ShortOptional, Short9),
                UnitTestRules.LessThanOrEqual(model => model.IntOptional, Int9),
                UnitTestRules.LessThanOrEqual(model => model.LongOptional, Long9),
                UnitTestRules.LessThanOrEqual(model => model.UShortOptional, UShort9),
                UnitTestRules.LessThanOrEqual(model => model.UIntOptional, UInt9),
                UnitTestRules.LessThanOrEqual(model => model.ULongOptional, ULong9),
                UnitTestRules.LessThanOrEqual(model => model.DoubleOptional, Double9),
                UnitTestRules.LessThanOrEqual(model => model.FloatOptional, Float9),
                UnitTestRules.LessThanOrEqual(model => model.DecimalOptional, Decimal9)
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
                    await UnitTestRules.LessThanOrEqualAsync(model => model.StringInvalidNum, Double10)
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
                    await UnitTestRules.LessThanOrEqualAsync(model => model.StringNum, Double11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Short, Short11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Int, Int11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Long, Long11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UShort, UShort11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UInt, UInt11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ULong, ULong11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Double, Double11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Float, Float11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Decimal, Decimal11),

                    await UnitTestRules.LessThanOrEqualAsync(model => model.StringNum, Double10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Short, Short10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Int, Int10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Long, Long10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UShort, UShort10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UInt, UInt10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ULong, ULong10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Double, Double10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Float, Float10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Decimal, Decimal10)
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
                    await UnitTestRules.LessThanOrEqualAsync(model => model.StringNum, Double9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Short, Short9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Int, Int9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Long, Long9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UShort, UShort9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UInt, UInt9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ULong, ULong9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Double, Double9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Float, Float9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.Decimal, Decimal9)
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
                    await UnitTestRules.LessThanOrEqualAsync(model => model.StringNum, Double11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ShortOptional, Short11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.IntOptional, Int11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.LongOptional, Long11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UShortOptional, UShort11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UIntOptional, UInt11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ULongOptional, ULong11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.DoubleOptional, Double11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.FloatOptional, Float11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.DecimalOptional, Decimal11),

                    await UnitTestRules.LessThanOrEqualAsync(model => model.StringNum, Double10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ShortOptional, Short10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.IntOptional, Int10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.LongOptional, Long10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UShortOptional, UShort10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UIntOptional, UInt10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ULongOptional, ULong10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.DoubleOptional, Double10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.FloatOptional, Float10),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.DecimalOptional, Decimal10)
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
                    await UnitTestRules.LessThanOrEqualAsync(model => model.StringNumNull, Double11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ShortOptionalNull, Short11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.IntOptionalNull, Int11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.LongOptionalNull, Long11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UShortOptionalNull, UShort11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UIntOptionalNull, UInt11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ULongOptionalNull, ULong11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.DoubleOptionalNull, Double11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.FloatOptionalNull, Float11),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.DecimalOptionalNull, Decimal11)
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
                    await UnitTestRules.LessThanOrEqualAsync(model => model.StringNum, Double9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ShortOptional, Short9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.IntOptional, Int9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.LongOptional, Long9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UShortOptional, UShort9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.UIntOptional, UInt9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.ULongOptional, ULong9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.DoubleOptional, Double9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.FloatOptional, Float9),
                    await UnitTestRules.LessThanOrEqualAsync(model => model.DecimalOptional, Decimal9)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
    }
}
