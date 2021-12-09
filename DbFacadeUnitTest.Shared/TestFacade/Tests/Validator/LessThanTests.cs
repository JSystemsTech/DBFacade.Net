using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class LessThanTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThan(model => model.StringInvalidNum, Double10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

        [TestMethod]
        public void WithNonNullableValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThan(model => model.StringNum, Double11));
                v.Add(v.Rules.LessThan(model => model.Short, Short11));
                v.Add(v.Rules.LessThan(model => model.Int, Int11));
                v.Add(v.Rules.LessThan(model => model.Long, Long11));
                v.Add(v.Rules.LessThan(model => model.UShort, UShort11));
                v.Add(v.Rules.LessThan(model => model.UInt, UInt11));
                v.Add(v.Rules.LessThan(model => model.ULong, ULong11));
                v.Add(v.Rules.LessThan(model => model.Double, Double11));
                v.Add(v.Rules.LessThan(model => model.Float, Float11));
                v.Add(v.Rules.LessThan(model => model.Decimal, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThan(model => model.StringNum, Double9));
                v.Add(v.Rules.LessThan(model => model.Short, Short9));
                v.Add(v.Rules.LessThan(model => model.Int, Int9));
                v.Add(v.Rules.LessThan(model => model.Long, Long9));
                v.Add(v.Rules.LessThan(model => model.UShort, UShort9));
                v.Add(v.Rules.LessThan(model => model.UInt, UInt9));
                v.Add(v.Rules.LessThan(model => model.ULong, ULong9));
                v.Add(v.Rules.LessThan(model => model.Double, Double9));
                v.Add(v.Rules.LessThan(model => model.Float, Float9));
                v.Add(v.Rules.LessThan(model => model.Decimal, Decimal9));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThan(model => model.StringNum, Double11));
                v.Add(v.Rules.LessThan(model => model.ShortOptional, Short11));
                v.Add(v.Rules.LessThan(model => model.IntOptional, Int11));
                v.Add(v.Rules.LessThan(model => model.LongOptional, Long11));
                v.Add(v.Rules.LessThan(model => model.UShortOptional, UShort11));
                v.Add(v.Rules.LessThan(model => model.UIntOptional, UInt11));
                v.Add(v.Rules.LessThan(model => model.ULongOptional, ULong11));
                v.Add(v.Rules.LessThan(model => model.DoubleOptional, Double11));
                v.Add(v.Rules.LessThan(model => model.FloatOptional, Float11));
                v.Add(v.Rules.LessThan(model => model.DecimalOptional, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThan(model => model.StringNumNull, Double11));
                v.Add(v.Rules.LessThan(model => model.ShortOptionalNull, Short11));
                v.Add(v.Rules.LessThan(model => model.IntOptionalNull, Int11));
                v.Add(v.Rules.LessThan(model => model.LongOptionalNull, Long11));
                v.Add(v.Rules.LessThan(model => model.UShortOptionalNull, UShort11));
                v.Add(v.Rules.LessThan(model => model.UIntOptionalNull, UInt11));
                v.Add(v.Rules.LessThan(model => model.ULongOptionalNull, ULong11));
                v.Add(v.Rules.LessThan(model => model.DoubleOptionalNull, Double11));
                v.Add(v.Rules.LessThan(model => model.FloatOptionalNull, Float11));
                v.Add(v.Rules.LessThan(model => model.DecimalOptionalNull, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThan(model => model.StringNum, Double9));
                v.Add(v.Rules.LessThan(model => model.ShortOptional, Short9));
                v.Add(v.Rules.LessThan(model => model.IntOptional, Int9));
                v.Add(v.Rules.LessThan(model => model.LongOptional, Long9));
                v.Add(v.Rules.LessThan(model => model.UShortOptional, UShort9));
                v.Add(v.Rules.LessThan(model => model.UIntOptional, UInt9));
                v.Add(v.Rules.LessThan(model => model.ULongOptional, ULong9));
                v.Add(v.Rules.LessThan(model => model.DoubleOptional, Double9));
                v.Add(v.Rules.LessThan(model => model.FloatOptional, Float9));
                v.Add(v.Rules.LessThan(model => model.DecimalOptional, Decimal9));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }

    }
}
