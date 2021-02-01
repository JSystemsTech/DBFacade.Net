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
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.GreaterThanOrEqual(model => model.StringInvalidNum, Double10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

        [TestMethod]
        public void WithNonNullableValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
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
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
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
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
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
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
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
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
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


        #region async tests
        [TestMethod]
        public void WithInvalidStringNumAsync()
        {
            RunAsAsyc(async () =>
            {
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync<UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.StringInvalidNum, Double10));
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
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.StringNum, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Short, Short9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Int, Int9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Long, Long9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UShort, UShort9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UInt, UInt9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ULong, ULong9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Double, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Float, Float9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Decimal, Decimal9));

                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.StringNum, Double10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Short, Short10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Int, Int10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Long, Long10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UShort, UShort10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UInt, UInt10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ULong, ULong10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Double, Double10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Float, Float10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Decimal, Decimal10));
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
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Short, Short11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Int, Int11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Long, Long11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UShort, UShort11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UInt, UInt11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ULong, ULong11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Double, Double11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Float, Float11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.Decimal, Decimal11));
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
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.StringNum, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ShortOptional, Short9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.IntOptional, Int9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.LongOptional, Long9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UShortOptional, UShort9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UIntOptional, UInt9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ULongOptional, ULong9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DoubleOptional, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.FloatOptional, Float9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DecimalOptional, Decimal9));

                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.StringNum, Double10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ShortOptional, Short10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.IntOptional, Int10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.LongOptional, Long10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UShortOptional, UShort10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UIntOptional, UInt10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ULongOptional, ULong10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DoubleOptional, Double10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.FloatOptional, Float10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DecimalOptional, Decimal10));
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
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.StringNumNull, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ShortOptionalNull, Short9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.IntOptionalNull, Int9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.LongOptionalNull, Long9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UShortOptionalNull, UShort9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UIntOptionalNull, UInt9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ULongOptionalNull, ULong9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DoubleOptionalNull, Double9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.FloatOptionalNull, Float9));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DecimalOptionalNull, Decimal9));

                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.StringNumNull, Double10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ShortOptionalNull, Short10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.IntOptionalNull, Int10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.LongOptionalNull, Long10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UShortOptionalNull, UShort10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UIntOptionalNull, UInt10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ULongOptionalNull, ULong10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DoubleOptionalNull, Double10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.FloatOptionalNull, Float10));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DecimalOptionalNull, Decimal10));
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
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ShortOptional, Short11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.IntOptional, Int11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.LongOptional, Long11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UShortOptional, UShort11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.UIntOptional, UInt11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.ULongOptional, ULong11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DoubleOptional, Double11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.FloatOptional, Float11));
                    await v.AddAsync(await v.Rules.GreaterThanOrEqualAsync(model => model.DecimalOptional, Decimal11));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
        #endregion
    }
}
