using DBFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class LessThanTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LessThan(model => model.StringInvalidNum, Double10)
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
                UnitTestRules.LessThan(model => model.StringNum, Double11),
                UnitTestRules.LessThan(model => model.Short, Short11),
                UnitTestRules.LessThan(model => model.Int, Int11),
                UnitTestRules.LessThan(model => model.Long, Long11),
                UnitTestRules.LessThan(model => model.UShort, UShort11),
                UnitTestRules.LessThan(model => model.UInt, UInt11),
                UnitTestRules.LessThan(model => model.ULong, ULong11),
                UnitTestRules.LessThan(model => model.Double, Double11),
                UnitTestRules.LessThan(model => model.Float, Float11),
                UnitTestRules.LessThan(model => model.Decimal, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LessThan(model => model.StringNum, Double9),
                UnitTestRules.LessThan(model => model.Short, Short9),
                UnitTestRules.LessThan(model => model.Int, Int9),
                UnitTestRules.LessThan(model => model.Long, Long9),
                UnitTestRules.LessThan(model => model.UShort, UShort9),
                UnitTestRules.LessThan(model => model.UInt, UInt9),
                UnitTestRules.LessThan(model => model.ULong, ULong9),
                UnitTestRules.LessThan(model => model.Double, Double9),
                UnitTestRules.LessThan(model => model.Float, Float9),
                UnitTestRules.LessThan(model => model.Decimal, Decimal9)
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
                UnitTestRules.LessThan(model => model.StringNum, Double11, true),
                UnitTestRules.LessThan(model => model.ShortOptional, Short11),
                UnitTestRules.LessThan(model => model.IntOptional, Int11),
                UnitTestRules.LessThan(model => model.LongOptional, Long11),
                UnitTestRules.LessThan(model => model.UShortOptional, UShort11),
                UnitTestRules.LessThan(model => model.UIntOptional, UInt11),
                UnitTestRules.LessThan(model => model.ULongOptional, ULong11),
                UnitTestRules.LessThan(model => model.DoubleOptional, Double11),
                UnitTestRules.LessThan(model => model.FloatOptional, Float11),
                UnitTestRules.LessThan(model => model.DecimalOptional, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LessThan(model => model.StringNumNull, Double11, true),
                UnitTestRules.LessThan(model => model.ShortOptionalNull, Short11),
                UnitTestRules.LessThan(model => model.IntOptionalNull, Int11),
                UnitTestRules.LessThan(model => model.LongOptionalNull, Long11),
                UnitTestRules.LessThan(model => model.UShortOptionalNull, UShort11),
                UnitTestRules.LessThan(model => model.UIntOptionalNull, UInt11),
                UnitTestRules.LessThan(model => model.ULongOptionalNull, ULong11),
                UnitTestRules.LessThan(model => model.DoubleOptionalNull, Double11),
                UnitTestRules.LessThan(model => model.FloatOptionalNull, Float11),
                UnitTestRules.LessThan(model => model.DecimalOptionalNull, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.LessThan(model => model.StringNum, Double9, true),
                UnitTestRules.LessThan(model => model.ShortOptional, Short9),
                UnitTestRules.LessThan(model => model.IntOptional, Int9),
                UnitTestRules.LessThan(model => model.LongOptional, Long9),
                UnitTestRules.LessThan(model => model.UShortOptional, UShort9),
                UnitTestRules.LessThan(model => model.UIntOptional, UInt9),
                UnitTestRules.LessThan(model => model.ULongOptional, ULong9),
                UnitTestRules.LessThan(model => model.DoubleOptional, Double9),
                UnitTestRules.LessThan(model => model.FloatOptional, Float9),
                UnitTestRules.LessThan(model => model.DecimalOptional, Decimal9)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
    }
}
