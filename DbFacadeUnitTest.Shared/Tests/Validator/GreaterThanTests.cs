using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class GreaterThanTests : ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThan(model => model.StringInvalidNum, Double10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

        [TestMethod]
        public void WithNonNullableValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThan(model => model.StringNum, Double9));
                v.Add(v.Rules.GreaterThan(model => model.Short, Short9));
                v.Add(v.Rules.GreaterThan(model => model.Int, Int9));
                v.Add(v.Rules.GreaterThan(model => model.Long, Long9));
                v.Add(v.Rules.GreaterThan(model => model.UShort, UShort9));
                v.Add(v.Rules.GreaterThan(model => model.UInt, UInt9));
                v.Add(v.Rules.GreaterThan(model => model.ULong, ULong9));
                v.Add(v.Rules.GreaterThan(model => model.Double, Double9));
                v.Add(v.Rules.GreaterThan(model => model.Float, Float9));
                v.Add(v.Rules.GreaterThan(model => model.Decimal, Decimal9));

            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThan(model => model.StringNum, Double11));
                v.Add(v.Rules.GreaterThan(model => model.Short, Short11));
                v.Add(v.Rules.GreaterThan(model => model.Int, Int11));
                v.Add(v.Rules.GreaterThan(model => model.Long, Long11));
                v.Add(v.Rules.GreaterThan(model => model.UShort, UShort11));
                v.Add(v.Rules.GreaterThan(model => model.UInt, UInt11));
                v.Add(v.Rules.GreaterThan(model => model.ULong, ULong11));
                v.Add(v.Rules.GreaterThan(model => model.Double, Double11));
                v.Add(v.Rules.GreaterThan(model => model.Float, Float11));
                v.Add(v.Rules.GreaterThan(model => model.Decimal, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThan(model => model.StringNum, Double9));
                v.Add(v.Rules.GreaterThan(model => model.ShortOptional, Short9));
                v.Add(v.Rules.GreaterThan(model => model.IntOptional, Int9));
                v.Add(v.Rules.GreaterThan(model => model.LongOptional, Long9));
                v.Add(v.Rules.GreaterThan(model => model.UShortOptional, UShort9));
                v.Add(v.Rules.GreaterThan(model => model.UIntOptional, UInt9));
                v.Add(v.Rules.GreaterThan(model => model.ULongOptional, ULong9));
                v.Add(v.Rules.GreaterThan(model => model.DoubleOptional, Double9));
                v.Add(v.Rules.GreaterThan(model => model.FloatOptional, Float9));
                v.Add(v.Rules.GreaterThan(model => model.DecimalOptional, Decimal9));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThan(model => model.StringNumNull, Double9));
                v.Add(v.Rules.GreaterThan(model => model.ShortOptionalNull, Short9));
                v.Add(v.Rules.GreaterThan(model => model.IntOptionalNull, Int9));
                v.Add(v.Rules.GreaterThan(model => model.LongOptionalNull, Long9));
                v.Add(v.Rules.GreaterThan(model => model.UShortOptionalNull, UShort9));
                v.Add(v.Rules.GreaterThan(model => model.UIntOptionalNull, UInt9));
                v.Add(v.Rules.GreaterThan(model => model.ULongOptionalNull, ULong9));
                v.Add(v.Rules.GreaterThan(model => model.DoubleOptionalNull, Double9));
                v.Add(v.Rules.GreaterThan(model => model.FloatOptionalNull, Float9));
                v.Add(v.Rules.GreaterThan(model => model.DecimalOptionalNull, Decimal9));

            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThan(model => model.StringNum, Double11));
                v.Add(v.Rules.GreaterThan(model => model.ShortOptional, Short11));
                v.Add(v.Rules.GreaterThan(model => model.IntOptional, Int11));
                v.Add(v.Rules.GreaterThan(model => model.LongOptional, Long11));
                v.Add(v.Rules.GreaterThan(model => model.UShortOptional, UShort11));
                v.Add(v.Rules.GreaterThan(model => model.UIntOptional, UInt11));
                v.Add(v.Rules.GreaterThan(model => model.ULongOptional, ULong11));
                v.Add(v.Rules.GreaterThan(model => model.DoubleOptional, Double11));
                v.Add(v.Rules.GreaterThan(model => model.FloatOptional, Float11));
                v.Add(v.Rules.GreaterThan(model => model.DecimalOptional, Decimal11));
            });
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
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.StringInvalidNum, Double10));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 1);
            });
        }

        [TestMethod]
        public void WithNonNullableValueAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.StringNum, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Short, Short9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Int, Int9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Long, Long9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UShort, UShort9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UInt, UInt9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.ULong, ULong9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Double, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Float, Float9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Decimal, Decimal9));

                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithNonNullableValueFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Short, Short11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Int, Int11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Long, Long11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UShort, UShort11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UInt, UInt11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.ULong, ULong11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Double, Double11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Float, Float11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.Decimal, Decimal11));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
        [TestMethod]
        public void WithOptionalValueAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.StringNum, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.ShortOptional, Short9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.IntOptional, Int9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.LongOptional, Long9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UShortOptional, UShort9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UIntOptional, UInt9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.ULongOptional, ULong9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.DoubleOptional, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.FloatOptional, Float9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.DecimalOptional, Decimal9));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithOptionalValueNullAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.StringNumNull, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.ShortOptionalNull, Short9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.IntOptionalNull, Int9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.LongOptionalNull, Long9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UShortOptionalNull, UShort9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UIntOptionalNull, UInt9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.ULongOptionalNull, ULong9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.DoubleOptionalNull, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.FloatOptionalNull, Float9));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.DecimalOptionalNull, Decimal9));

                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsValidAsync(result);
            });
        }
        [TestMethod]
        public void WithOptionalValueFailAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.ShortOptional, Short11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.IntOptional, Int11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.LongOptional, Long11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UShortOptional, UShort11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.UIntOptional, UInt11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.ULongOptional, ULong11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.DoubleOptional, Double11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.FloatOptional, Float11));
                    await v.AddAsync(await v.Rules.GreaterThanAsync(model => model.DecimalOptional, Decimal11));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
        #endregion
    }
}
