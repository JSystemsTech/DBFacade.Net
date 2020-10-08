using DbFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class NotEqualToTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.NotEqualTo(model => model.StringInvalidNum, Double10)
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
                UnitTestRules.NotEqualTo(model => model.StringNum, Double11),
                UnitTestRules.NotEqualTo(model => model.Short, Short11),
                UnitTestRules.NotEqualTo(model => model.Int, Int11),
                UnitTestRules.NotEqualTo(model => model.Long, Long11),
                UnitTestRules.NotEqualTo(model => model.UShort, UShort11),
                UnitTestRules.NotEqualTo(model => model.UInt, UInt11),
                UnitTestRules.NotEqualTo(model => model.ULong, ULong11),
                UnitTestRules.NotEqualTo(model => model.Double, Double11),
                UnitTestRules.NotEqualTo(model => model.Float, Float11),
                UnitTestRules.NotEqualTo(model => model.Decimal, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.NotEqualTo(model => model.StringNum, Double10),
                UnitTestRules.NotEqualTo(model => model.Short, Short10),
                UnitTestRules.NotEqualTo(model => model.Int, Int10),
                UnitTestRules.NotEqualTo(model => model.Long, Long10),
                UnitTestRules.NotEqualTo(model => model.UShort, UShort10),
                UnitTestRules.NotEqualTo(model => model.UInt, UInt10),
                UnitTestRules.NotEqualTo(model => model.ULong, ULong10),
                UnitTestRules.NotEqualTo(model => model.Double, Double10),
                UnitTestRules.NotEqualTo(model => model.Float, Float10),
                UnitTestRules.NotEqualTo(model => model.Decimal, Decimal10)
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
                UnitTestRules.NotEqualTo(model => model.StringNum, Double11),
                UnitTestRules.NotEqualTo(model => model.ShortOptional, Short11),
                UnitTestRules.NotEqualTo(model => model.IntOptional, Int11),
                UnitTestRules.NotEqualTo(model => model.LongOptional, Long11),
                UnitTestRules.NotEqualTo(model => model.UShortOptional, UShort11),
                UnitTestRules.NotEqualTo(model => model.UIntOptional, UInt11),
                UnitTestRules.NotEqualTo(model => model.ULongOptional, ULong11),
                UnitTestRules.NotEqualTo(model => model.DoubleOptional, Double11),
                UnitTestRules.NotEqualTo(model => model.FloatOptional, Float11),
                UnitTestRules.NotEqualTo(model => model.DecimalOptional, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.NotEqualTo(model => model.StringNumNull, Double11),
                UnitTestRules.NotEqualTo(model => model.ShortOptionalNull, Short11),
                UnitTestRules.NotEqualTo(model => model.IntOptionalNull, Int11),
                UnitTestRules.NotEqualTo(model => model.LongOptionalNull, Long11),
                UnitTestRules.NotEqualTo(model => model.UShortOptionalNull, UShort11),
                UnitTestRules.NotEqualTo(model => model.UIntOptionalNull, UInt11),
                UnitTestRules.NotEqualTo(model => model.ULongOptionalNull, ULong11),
                UnitTestRules.NotEqualTo(model => model.DoubleOptionalNull, Double11),
                UnitTestRules.NotEqualTo(model => model.FloatOptionalNull, Float11),
                UnitTestRules.NotEqualTo(model => model.DecimalOptionalNull, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.NotEqualTo(model => model.StringNum, Double10),
                UnitTestRules.NotEqualTo(model => model.ShortOptional, Short10),
                UnitTestRules.NotEqualTo(model => model.IntOptional, Int10),
                UnitTestRules.NotEqualTo(model => model.LongOptional, Long10),
                UnitTestRules.NotEqualTo(model => model.UShortOptional, UShort10),
                UnitTestRules.NotEqualTo(model => model.UIntOptional, UInt10),
                UnitTestRules.NotEqualTo(model => model.ULongOptional, ULong10),
                UnitTestRules.NotEqualTo(model => model.DoubleOptional, Double10),
                UnitTestRules.NotEqualTo(model => model.FloatOptional, Float10),
                UnitTestRules.NotEqualTo(model => model.DecimalOptional, Decimal10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }        


        #region async tests
        [TestMethod]
        public void WithInvalidStringNumAsync()
        {
            RunAsAsyc(async () =>
            {
                var validator = await UnitTestValidator.CreateAsync(
                    await UnitTestRules.NotEqualToAsync(model => model.StringInvalidNum, Double10)
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
                    await UnitTestRules.NotEqualToAsync(model => model.StringNum, Double11),
                    await UnitTestRules.NotEqualToAsync(model => model.Short, Short11),
                    await UnitTestRules.NotEqualToAsync(model => model.Int, Int11),
                    await UnitTestRules.NotEqualToAsync(model => model.Long, Long11),
                    await UnitTestRules.NotEqualToAsync(model => model.UShort, UShort11),
                    await UnitTestRules.NotEqualToAsync(model => model.UInt, UInt11),
                    await UnitTestRules.NotEqualToAsync(model => model.ULong, ULong11),
                    await UnitTestRules.NotEqualToAsync(model => model.Double, Double11),
                    await UnitTestRules.NotEqualToAsync(model => model.Float, Float11),
                    await UnitTestRules.NotEqualToAsync(model => model.Decimal, Decimal11)
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
                    await UnitTestRules.NotEqualToAsync(model => model.StringNum, Double10),
                    await UnitTestRules.NotEqualToAsync(model => model.Short, Short10),
                    await UnitTestRules.NotEqualToAsync(model => model.Int, Int10),
                    await UnitTestRules.NotEqualToAsync(model => model.Long, Long10),
                    await UnitTestRules.NotEqualToAsync(model => model.UShort, UShort10),
                    await UnitTestRules.NotEqualToAsync(model => model.UInt, UInt10),
                    await UnitTestRules.NotEqualToAsync(model => model.ULong, ULong10),
                    await UnitTestRules.NotEqualToAsync(model => model.Double, Double10),
                    await UnitTestRules.NotEqualToAsync(model => model.Float, Float10),
                    await UnitTestRules.NotEqualToAsync(model => model.Decimal, Decimal10)
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
                    await UnitTestRules.NotEqualToAsync(model => model.StringNum, Double11),
                    await UnitTestRules.NotEqualToAsync(model => model.ShortOptional, Short11),
                    await UnitTestRules.NotEqualToAsync(model => model.IntOptional, Int11),
                    await UnitTestRules.NotEqualToAsync(model => model.LongOptional, Long11),
                    await UnitTestRules.NotEqualToAsync(model => model.UShortOptional, UShort11),
                    await UnitTestRules.NotEqualToAsync(model => model.UIntOptional, UInt11),
                    await UnitTestRules.NotEqualToAsync(model => model.ULongOptional, ULong11),
                    await UnitTestRules.NotEqualToAsync(model => model.DoubleOptional, Double11),
                    await UnitTestRules.NotEqualToAsync(model => model.FloatOptional, Float11),
                    await UnitTestRules.NotEqualToAsync(model => model.DecimalOptional, Decimal11)
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
                    await UnitTestRules.NotEqualToAsync(model => model.StringNumNull, Double11),
                    await UnitTestRules.NotEqualToAsync(model => model.ShortOptionalNull, Short11),
                    await UnitTestRules.NotEqualToAsync(model => model.IntOptionalNull, Int11),
                    await UnitTestRules.NotEqualToAsync(model => model.LongOptionalNull, Long11),
                    await UnitTestRules.NotEqualToAsync(model => model.UShortOptionalNull, UShort11),
                    await UnitTestRules.NotEqualToAsync(model => model.UIntOptionalNull, UInt11),
                    await UnitTestRules.NotEqualToAsync(model => model.ULongOptionalNull, ULong11),
                    await UnitTestRules.NotEqualToAsync(model => model.DoubleOptionalNull, Double11),
                    await UnitTestRules.NotEqualToAsync(model => model.FloatOptionalNull, Float11),
                    await UnitTestRules.NotEqualToAsync(model => model.DecimalOptionalNull, Decimal11)
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
                    await UnitTestRules.NotEqualToAsync(model => model.StringNum, Double10),
                    await UnitTestRules.NotEqualToAsync(model => model.ShortOptional, Short10),
                    await UnitTestRules.NotEqualToAsync(model => model.IntOptional, Int10),
                    await UnitTestRules.NotEqualToAsync(model => model.LongOptional, Long10),
                    await UnitTestRules.NotEqualToAsync(model => model.UShortOptional, UShort10),
                    await UnitTestRules.NotEqualToAsync(model => model.UIntOptional, UInt10),
                    await UnitTestRules.NotEqualToAsync(model => model.ULongOptional, ULong10),
                    await UnitTestRules.NotEqualToAsync(model => model.DoubleOptional, Double10),
                    await UnitTestRules.NotEqualToAsync(model => model.FloatOptional, Float10),
                    await UnitTestRules.NotEqualToAsync(model => model.DecimalOptional, Decimal10)
                    );
                var result = await validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
        #endregion
    }
}
