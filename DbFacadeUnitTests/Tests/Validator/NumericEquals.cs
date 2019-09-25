using DBFacade.DataLayer.Models.Validators;
using DbFacadeUnitTests.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class NumericEquals:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Equals(model => model.StringInvalidNum, Double10)
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
                UnitTestRules.Equals(model => model.StringNum, Double10),
                UnitTestRules.Equals(model => model.Short, Short10),
                UnitTestRules.Equals(model => model.Int, Int10),
                UnitTestRules.Equals(model => model.Long, Long10),
                UnitTestRules.Equals(model => model.UShort, UShort10),
                UnitTestRules.Equals(model => model.UInt, UInt10),
                UnitTestRules.Equals(model => model.ULong, ULong10),
                UnitTestRules.Equals(model => model.Double, Double10),
                UnitTestRules.Equals(model => model.Float, Float10),
                UnitTestRules.Equals(model => model.Decimal, Decimal10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Equals(model => model.StringNum, Double11),
                UnitTestRules.Equals(model => model.Short, Short11),
                UnitTestRules.Equals(model => model.Int, Int11),
                UnitTestRules.Equals(model => model.Long, Long11),
                UnitTestRules.Equals(model => model.UShort, UShort11),
                UnitTestRules.Equals(model => model.UInt, UInt11),
                UnitTestRules.Equals(model => model.ULong, ULong11),
                UnitTestRules.Equals(model => model.Double, Double11),
                UnitTestRules.Equals(model => model.Float, Float11),
                UnitTestRules.Equals(model => model.Decimal, Decimal11)
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
                UnitTestRules.Equals(model => model.StringNum, Double10, true),
                UnitTestRules.Equals(model => model.ShortOptional, Short10),
                UnitTestRules.Equals(model => model.IntOptional, Int10),
                UnitTestRules.Equals(model => model.LongOptional, Long10),
                UnitTestRules.Equals(model => model.UShortOptional, UShort10),
                UnitTestRules.Equals(model => model.UIntOptional, UInt10),
                UnitTestRules.Equals(model => model.ULongOptional, ULong10),
                UnitTestRules.Equals(model => model.DoubleOptional, Double10),
                UnitTestRules.Equals(model => model.FloatOptional, Float10),
                UnitTestRules.Equals(model => model.DecimalOptional, Decimal10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Equals(model => model.StringNumNull, Double10, true),
                UnitTestRules.Equals(model => model.ShortOptionalNull, Short10),
                UnitTestRules.Equals(model => model.IntOptionalNull, Int10),
                UnitTestRules.Equals(model => model.LongOptionalNull, Long10),
                UnitTestRules.Equals(model => model.UShortOptionalNull, UShort10),
                UnitTestRules.Equals(model => model.UIntOptionalNull, UInt10),
                UnitTestRules.Equals(model => model.ULongOptionalNull, ULong10),
                UnitTestRules.Equals(model => model.DoubleOptionalNull, Double10),
                UnitTestRules.Equals(model => model.FloatOptionalNull, Float10),
                UnitTestRules.Equals(model => model.DecimalOptionalNull, Decimal10)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            UnitTestValidator Validator = new UnitTestValidator()
            {
                UnitTestRules.Equals(model => model.StringNum, Double11, true),
                UnitTestRules.Equals(model => model.ShortOptional, Short11),
                UnitTestRules.Equals(model => model.IntOptional, Int11),
                UnitTestRules.Equals(model => model.LongOptional, Long11),
                UnitTestRules.Equals(model => model.UShortOptional, UShort11),
                UnitTestRules.Equals(model => model.UIntOptional, UInt11),
                UnitTestRules.Equals(model => model.ULongOptional, ULong11),
                UnitTestRules.Equals(model => model.DoubleOptional, Double11),
                UnitTestRules.Equals(model => model.FloatOptional, Float11),
                UnitTestRules.Equals(model => model.DecimalOptional, Decimal11)
            };
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
    }
}
