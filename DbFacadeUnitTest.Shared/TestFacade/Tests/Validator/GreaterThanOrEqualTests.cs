using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class GreaterThanOrEqualTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringInvalidNum, Double10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

        [TestMethod]
        public void WithNonNullableValue()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringNum, Double9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Short, Short9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Int, Int9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Long, Long9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UShort, UShort9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UInt, UInt9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ULong, ULong9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Double, Double9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Float, Float9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Decimal, Decimal9));

                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringNum, Double10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Short, Short10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Int, Int10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Long, Long10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UShort, UShort10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UInt, UInt10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ULong, ULong10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Double, Double10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Float, Float10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Decimal, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringNum, Double11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Short, Short11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Int, Int11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Long, Long11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UShort, UShort11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UInt, UInt11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ULong, ULong11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Double, Double11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Float, Float11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.Decimal, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringNum, Double9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ShortOptional, Short9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.IntOptional, Int9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.LongOptional, Long9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UShortOptional, UShort9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UIntOptional, UInt9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ULongOptional, ULong9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DoubleOptional, Double9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.FloatOptional, Float9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DecimalOptional, Decimal9));

                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringNum, Double10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ShortOptional, Short10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.IntOptional, Int10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.LongOptional, Long10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UShortOptional, UShort10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UIntOptional, UInt10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ULongOptional, ULong10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DoubleOptional, Double10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.FloatOptional, Float10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DecimalOptional, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringNumNull, Double9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ShortOptionalNull, Short9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.IntOptionalNull, Int9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.LongOptionalNull, Long9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UShortOptionalNull, UShort9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UIntOptionalNull, UInt9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ULongOptionalNull, ULong9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DoubleOptionalNull, Double9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.FloatOptionalNull, Float9));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DecimalOptionalNull, Decimal9));

                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringNumNull, Double10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ShortOptionalNull, Short10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.IntOptionalNull, Int10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.LongOptionalNull, Long10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UShortOptionalNull, UShort10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UIntOptionalNull, UInt10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ULongOptionalNull, ULong10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DoubleOptionalNull, Double10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.FloatOptionalNull, Float10));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DecimalOptionalNull, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            var Validator = MakeValidator<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringNum, Double11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ShortOptional, Short11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.IntOptional, Int11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.LongOptional, Long11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UShortOptional, UShort11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.UIntOptional, UInt11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.ULongOptional, ULong11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DoubleOptional, Double11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.FloatOptional, Float11));
                v.Add(v.Rules.GreaterThanOrEqual(model => model.DecimalOptional, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
    }
}
