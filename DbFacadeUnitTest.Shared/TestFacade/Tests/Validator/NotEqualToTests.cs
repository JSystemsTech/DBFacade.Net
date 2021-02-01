using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class NotEqualToTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.NotEqualTo(model => model.StringInvalidNum, Double11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

        [TestMethod]
        public void WithNonNullableValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.NotEqualTo(model => model.StringNum, Double11));
                v.Add(v.Rules.NotEqualTo(model => model.Short, Short11));
                v.Add(v.Rules.NotEqualTo(model => model.Int, Int11));
                v.Add(v.Rules.NotEqualTo(model => model.Long, Long11));
                v.Add(v.Rules.NotEqualTo(model => model.UShort, UShort11));
                v.Add(v.Rules.NotEqualTo(model => model.UInt, UInt11));
                v.Add(v.Rules.NotEqualTo(model => model.ULong, ULong11));
                v.Add(v.Rules.NotEqualTo(model => model.Double, Double11));
                v.Add(v.Rules.NotEqualTo(model => model.Float, Float11));
                v.Add(v.Rules.NotEqualTo(model => model.Decimal, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.NotEqualTo(model => model.StringNum, Double10));
                v.Add(v.Rules.NotEqualTo(model => model.Short, Short10));
                v.Add(v.Rules.NotEqualTo(model => model.Int, Int10));
                v.Add(v.Rules.NotEqualTo(model => model.Long, Long10));
                v.Add(v.Rules.NotEqualTo(model => model.UShort, UShort10));
                v.Add(v.Rules.NotEqualTo(model => model.UInt, UInt10));
                v.Add(v.Rules.NotEqualTo(model => model.ULong, ULong10));
                v.Add(v.Rules.NotEqualTo(model => model.Double, Double10));
                v.Add(v.Rules.NotEqualTo(model => model.Float, Float10));
                v.Add(v.Rules.NotEqualTo(model => model.Decimal, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.NotEqualTo(model => model.StringNum, Double11));
                v.Add(v.Rules.NotEqualTo(model => model.ShortOptional, Short11));
                v.Add(v.Rules.NotEqualTo(model => model.IntOptional, Int11));
                v.Add(v.Rules.NotEqualTo(model => model.LongOptional, Long11));
                v.Add(v.Rules.NotEqualTo(model => model.UShortOptional, UShort11));
                v.Add(v.Rules.NotEqualTo(model => model.UIntOptional, UInt11));
                v.Add(v.Rules.NotEqualTo(model => model.ULongOptional, ULong11));
                v.Add(v.Rules.NotEqualTo(model => model.DoubleOptional, Double11));
                v.Add(v.Rules.NotEqualTo(model => model.FloatOptional, Float11));
                v.Add(v.Rules.NotEqualTo(model => model.DecimalOptional, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.NotEqualTo(model => model.StringNumNull, Double11));
                v.Add(v.Rules.NotEqualTo(model => model.ShortOptionalNull, Short11));
                v.Add(v.Rules.NotEqualTo(model => model.IntOptionalNull, Int11));
                v.Add(v.Rules.NotEqualTo(model => model.LongOptionalNull, Long11));
                v.Add(v.Rules.NotEqualTo(model => model.UShortOptionalNull, UShort11));
                v.Add(v.Rules.NotEqualTo(model => model.UIntOptionalNull, UInt11));
                v.Add(v.Rules.NotEqualTo(model => model.ULongOptionalNull, ULong11));
                v.Add(v.Rules.NotEqualTo(model => model.DoubleOptionalNull, Double11));
                v.Add(v.Rules.NotEqualTo(model => model.FloatOptionalNull, Float11));
                v.Add(v.Rules.NotEqualTo(model => model.DecimalOptionalNull, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.NotEqualTo(model => model.StringNum, Double10));
                v.Add(v.Rules.NotEqualTo(model => model.ShortOptional, Short10));
                v.Add(v.Rules.NotEqualTo(model => model.IntOptional, Int10));
                v.Add(v.Rules.NotEqualTo(model => model.LongOptional, Long10));
                v.Add(v.Rules.NotEqualTo(model => model.UShortOptional, UShort10));
                v.Add(v.Rules.NotEqualTo(model => model.UIntOptional, UInt10));
                v.Add(v.Rules.NotEqualTo(model => model.ULongOptional, ULong10));
                v.Add(v.Rules.NotEqualTo(model => model.DoubleOptional, Double10));
                v.Add(v.Rules.NotEqualTo(model => model.FloatOptional, Float10));
                v.Add(v.Rules.NotEqualTo(model => model.DecimalOptional, Decimal10));
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
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.StringInvalidNum, Double11));
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
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Short, Short11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Int, Int11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Long, Long11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UShort, UShort11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UInt, UInt11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.ULong, ULong11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Double, Double11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Float, Float11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Decimal, Decimal11));
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
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.StringNum, Double10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Short, Short10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Int, Int10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Long, Long10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UShort, UShort10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UInt, UInt10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.ULong, ULong10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Double, Double10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Float, Float10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.Decimal, Decimal10));
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
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.ShortOptional, Short11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.IntOptional, Int11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.LongOptional, Long11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UShortOptional, UShort11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UIntOptional, UInt11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.ULongOptional, ULong11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.DoubleOptional, Double11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.FloatOptional, Float11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.DecimalOptional, Decimal11));
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
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.StringNumNull, Double11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.ShortOptionalNull, Short11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.IntOptionalNull, Int11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.LongOptionalNull, Long11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UShortOptionalNull, UShort11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UIntOptionalNull, UInt11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.ULongOptionalNull, ULong11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.DoubleOptionalNull, Double11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.FloatOptionalNull, Float11));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.DecimalOptionalNull, Decimal11));
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
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.StringNum, Double10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.ShortOptional, Short10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.IntOptional, Int10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.LongOptional, Long10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UShortOptional, UShort10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.UIntOptional, UInt10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.ULongOptional, ULong10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.DoubleOptional, Double10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.FloatOptional, Float10));
                    await v.AddAsync(await v.Rules.NotEqualToAsync(model => model.DecimalOptional, Decimal10));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
        #endregion


    }
}
