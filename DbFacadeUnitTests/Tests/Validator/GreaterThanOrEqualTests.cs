using DBFacade.DataLayer.Models.Validators;
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
                UnitTestRules.GreaterThanOrEqual(model => model.Decimal, Decimal9)
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
                UnitTestRules.GreaterThanOrEqual(model => model.StringNum, Double9, true),
                UnitTestRules.GreaterThanOrEqual(model => model.ShortOptional, Short9),
                UnitTestRules.GreaterThanOrEqual(model => model.IntOptional, Int9),
                UnitTestRules.GreaterThanOrEqual(model => model.LongOptional, Long9),
                UnitTestRules.GreaterThanOrEqual(model => model.UShortOptional, UShort9),
                UnitTestRules.GreaterThanOrEqual(model => model.UIntOptional, UInt9),
                UnitTestRules.GreaterThanOrEqual(model => model.ULongOptional, ULong9),
                UnitTestRules.GreaterThanOrEqual(model => model.DoubleOptional, Double9),
                UnitTestRules.GreaterThanOrEqual(model => model.FloatOptional, Float9),
                UnitTestRules.GreaterThanOrEqual(model => model.DecimalOptional, Decimal9)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.GreaterThanOrEqual(model => model.StringNumNull, Double9, true),
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
                UnitTestRules.GreaterThanOrEqual(model => model.StringNum, Double11, true),
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
    }
}
