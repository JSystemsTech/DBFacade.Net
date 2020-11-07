using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class Equals:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Equals(model => model.StringInvalidNum, Double10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

        [TestMethod]
        public void WithNonNullableValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Equals(model => model.StringNum, Double10));
                v.Add(v.Rules.Equals(model => model.Short, Short10));
                v.Add(v.Rules.Equals(model => model.Int, Int10));
                v.Add(v.Rules.Equals(model => model.Long, Long10));
                v.Add(v.Rules.Equals(model => model.UShort, UShort10));
                v.Add(v.Rules.Equals(model => model.UInt, UInt10));
                v.Add(v.Rules.Equals(model => model.ULong, ULong10));
                v.Add(v.Rules.Equals(model => model.Double, Double10));
                v.Add(v.Rules.Equals(model => model.Float, Float10));
                v.Add(v.Rules.Equals(model => model.Decimal, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Equals(model => model.StringNum, Double11));
                v.Add(v.Rules.Equals(model => model.Short, Short11));
                v.Add(v.Rules.Equals(model => model.Int, Int11));
                v.Add(v.Rules.Equals(model => model.Long, Long11));
                v.Add(v.Rules.Equals(model => model.UShort, UShort11));
                v.Add(v.Rules.Equals(model => model.UInt, UInt11));
                v.Add(v.Rules.Equals(model => model.ULong, ULong11));
                v.Add(v.Rules.Equals(model => model.Double, Double11));
                v.Add(v.Rules.Equals(model => model.Float, Float11));
                v.Add(v.Rules.Equals(model => model.Decimal, Decimal11));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Equals(model => model.StringNum, Double10));
                v.Add(v.Rules.Equals(model => model.ShortOptional, Short10));
                v.Add(v.Rules.Equals(model => model.IntOptional, Int10));
                v.Add(v.Rules.Equals(model => model.LongOptional, Long10));
                v.Add(v.Rules.Equals(model => model.UShortOptional, UShort10));
                v.Add(v.Rules.Equals(model => model.UIntOptional, UInt10));
                v.Add(v.Rules.Equals(model => model.ULongOptional, ULong10));
                v.Add(v.Rules.Equals(model => model.DoubleOptional, Double10));
                v.Add(v.Rules.Equals(model => model.FloatOptional, Float10));
                v.Add(v.Rules.Equals(model => model.DecimalOptional, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Equals(model => model.StringNumNull, Double10));
                v.Add(v.Rules.Equals(model => model.ShortOptionalNull, Short10));
                v.Add(v.Rules.Equals(model => model.IntOptionalNull, Int10));
                v.Add(v.Rules.Equals(model => model.LongOptionalNull, Long10));
                v.Add(v.Rules.Equals(model => model.UShortOptionalNull, UShort10));
                v.Add(v.Rules.Equals(model => model.UIntOptionalNull, UInt10));
                v.Add(v.Rules.Equals(model => model.ULongOptionalNull, ULong10));
                v.Add(v.Rules.Equals(model => model.DoubleOptionalNull, Double10));
                v.Add(v.Rules.Equals(model => model.FloatOptionalNull, Float10));
                v.Add(v.Rules.Equals(model => model.DecimalOptionalNull, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.Equals(model => model.StringNum, Double11));
                v.Add(v.Rules.Equals(model => model.ShortOptional, Short11));
                v.Add(v.Rules.Equals(model => model.IntOptional, Int11));
                v.Add(v.Rules.Equals(model => model.LongOptional, Long11));
                v.Add(v.Rules.Equals(model => model.UShortOptional, UShort11));
                v.Add(v.Rules.Equals(model => model.UIntOptional, UInt11));
                v.Add(v.Rules.Equals(model => model.ULongOptional, ULong11));
                v.Add(v.Rules.Equals(model => model.DoubleOptional, Double11));
                v.Add(v.Rules.Equals(model => model.FloatOptional, Float11));
                v.Add(v.Rules.Equals(model => model.DecimalOptional, Decimal11));
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
                IValidator<UnitTestDbParams> Validator = await ValidatorFactory.CreateAsync <UnitTestDbParams>(async v => {
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.StringInvalidNum, Double10));
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
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.StringNum, Double10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Short, Short10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Int, Int10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Long, Long10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UShort, UShort10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UInt, UInt10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.ULong, ULong10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Double, Double10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Float, Float10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Decimal, Decimal10));
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
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Short, Short11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Int, Int11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Long, Long11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UShort, UShort11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UInt, UInt11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.ULong, ULong11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Double, Double11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Float, Float11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.Decimal, Decimal11));
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
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.StringNum, Double10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.ShortOptional, Short10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.IntOptional, Int10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.LongOptional, Long10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UShortOptional, UShort10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UIntOptional, UInt10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.ULongOptional, ULong10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.DoubleOptional, Double10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.FloatOptional, Float10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.DecimalOptional, Decimal10));
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
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.StringNumNull, Double10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.ShortOptionalNull, Short10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.IntOptionalNull, Int10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.LongOptionalNull, Long10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UShortOptionalNull, UShort10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UIntOptionalNull, UInt10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.ULongOptionalNull, ULong10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.DoubleOptionalNull, Double10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.FloatOptionalNull, Float10));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.DecimalOptionalNull, Decimal10));
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
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.ShortOptional, Short11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.IntOptional, Int11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.LongOptional, Long11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UShortOptional, UShort11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.UIntOptional, UInt11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.ULongOptional, ULong11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.DoubleOptional, Double11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.FloatOptional, Float11));
                    await v.AddAsync(await v.Rules.EqualsAsync(model => model.DecimalOptional, Decimal11));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
        #endregion


    }
}
