﻿using DbFacade.DataLayer.Models.Validators;
using DbFacade.Factories;
using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbFacadeUnitTests.Tests.Validator
{
    [TestClass]
    public class LessThanOrEqualTests:ValidatorTestBase
    {
        [TestMethod]
        public void WithInvalidStringNum()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThanOrEqual(model => model.StringInvalidNum, Double10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 1);
        }

        [TestMethod]
        public void WithNonNullableValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThanOrEqual(model => model.StringNum, Double11));
                v.Add(v.Rules.LessThanOrEqual(model => model.Short, Short11));
                v.Add(v.Rules.LessThanOrEqual(model => model.Int, Int11));
                v.Add(v.Rules.LessThanOrEqual(model => model.Long, Long11));
                v.Add(v.Rules.LessThanOrEqual(model => model.UShort, UShort11));
                v.Add(v.Rules.LessThanOrEqual(model => model.UInt, UInt11));
                v.Add(v.Rules.LessThanOrEqual(model => model.ULong, ULong11));
                v.Add(v.Rules.LessThanOrEqual(model => model.Double, Double11));
                v.Add(v.Rules.LessThanOrEqual(model => model.Float, Float11));
                v.Add(v.Rules.LessThanOrEqual(model => model.Decimal, Decimal11));

                v.Add(v.Rules.LessThanOrEqual(model => model.StringNum, Double10));
                v.Add(v.Rules.LessThanOrEqual(model => model.Short, Short10));
                v.Add(v.Rules.LessThanOrEqual(model => model.Int, Int10));
                v.Add(v.Rules.LessThanOrEqual(model => model.Long, Long10));
                v.Add(v.Rules.LessThanOrEqual(model => model.UShort, UShort10));
                v.Add(v.Rules.LessThanOrEqual(model => model.UInt, UInt10));
                v.Add(v.Rules.LessThanOrEqual(model => model.ULong, ULong10));
                v.Add(v.Rules.LessThanOrEqual(model => model.Double, Double10));
                v.Add(v.Rules.LessThanOrEqual(model => model.Float, Float10));
                v.Add(v.Rules.LessThanOrEqual(model => model.Decimal, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithNonNullableValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThanOrEqual(model => model.StringNum, Double9));
                v.Add(v.Rules.LessThanOrEqual(model => model.Short, Short9));
                v.Add(v.Rules.LessThanOrEqual(model => model.Int, Int9));
                v.Add(v.Rules.LessThanOrEqual(model => model.Long, Long9));
                v.Add(v.Rules.LessThanOrEqual(model => model.UShort, UShort9));
                v.Add(v.Rules.LessThanOrEqual(model => model.UInt, UInt9));
                v.Add(v.Rules.LessThanOrEqual(model => model.ULong, ULong9));
                v.Add(v.Rules.LessThanOrEqual(model => model.Double, Double9));
                v.Add(v.Rules.LessThanOrEqual(model => model.Float, Float9));
                v.Add(v.Rules.LessThanOrEqual(model => model.Decimal, Decimal9));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsInvalid(result);
            HasCorrectErrorCount(result, 10);
        }
        [TestMethod]
        public void WithOptionalValue()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThanOrEqual(model => model.StringNum, Double11));
                v.Add(v.Rules.LessThanOrEqual(model => model.ShortOptional, Short11));
                v.Add(v.Rules.LessThanOrEqual(model => model.IntOptional, Int11));
                v.Add(v.Rules.LessThanOrEqual(model => model.LongOptional, Long11));
                v.Add(v.Rules.LessThanOrEqual(model => model.UShortOptional, UShort11));
                v.Add(v.Rules.LessThanOrEqual(model => model.UIntOptional, UInt11));
                v.Add(v.Rules.LessThanOrEqual(model => model.ULongOptional, ULong11));
                v.Add(v.Rules.LessThanOrEqual(model => model.DoubleOptional, Double11));
                v.Add(v.Rules.LessThanOrEqual(model => model.FloatOptional, Float11));
                v.Add(v.Rules.LessThanOrEqual(model => model.DecimalOptional, Decimal11));

                v.Add(v.Rules.LessThanOrEqual(model => model.StringNum, Double10));
                v.Add(v.Rules.LessThanOrEqual(model => model.ShortOptional, Short10));
                v.Add(v.Rules.LessThanOrEqual(model => model.IntOptional, Int10));
                v.Add(v.Rules.LessThanOrEqual(model => model.LongOptional, Long10));
                v.Add(v.Rules.LessThanOrEqual(model => model.UShortOptional, UShort10));
                v.Add(v.Rules.LessThanOrEqual(model => model.UIntOptional, UInt10));
                v.Add(v.Rules.LessThanOrEqual(model => model.ULongOptional, ULong10));
                v.Add(v.Rules.LessThanOrEqual(model => model.DoubleOptional, Double10));
                v.Add(v.Rules.LessThanOrEqual(model => model.FloatOptional, Float10));
                v.Add(v.Rules.LessThanOrEqual(model => model.DecimalOptional, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueNull()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThanOrEqual(model => model.StringNumNull, Double11));
                v.Add(v.Rules.LessThanOrEqual(model => model.ShortOptionalNull, Short11));
                v.Add(v.Rules.LessThanOrEqual(model => model.IntOptionalNull, Int11));
                v.Add(v.Rules.LessThanOrEqual(model => model.LongOptionalNull, Long11));
                v.Add(v.Rules.LessThanOrEqual(model => model.UShortOptionalNull, UShort11));
                v.Add(v.Rules.LessThanOrEqual(model => model.UIntOptionalNull, UInt11));
                v.Add(v.Rules.LessThanOrEqual(model => model.ULongOptionalNull, ULong11));
                v.Add(v.Rules.LessThanOrEqual(model => model.DoubleOptionalNull, Double11));
                v.Add(v.Rules.LessThanOrEqual(model => model.FloatOptionalNull, Float11));
                v.Add(v.Rules.LessThanOrEqual(model => model.DecimalOptionalNull, Decimal11));

                v.Add(v.Rules.LessThanOrEqual(model => model.StringNumNull, Double10));
                v.Add(v.Rules.LessThanOrEqual(model => model.ShortOptionalNull, Short10));
                v.Add(v.Rules.LessThanOrEqual(model => model.IntOptionalNull, Int10));
                v.Add(v.Rules.LessThanOrEqual(model => model.LongOptionalNull, Long10));
                v.Add(v.Rules.LessThanOrEqual(model => model.UShortOptionalNull, UShort10));
                v.Add(v.Rules.LessThanOrEqual(model => model.UIntOptionalNull, UInt10));
                v.Add(v.Rules.LessThanOrEqual(model => model.ULongOptionalNull, ULong10));
                v.Add(v.Rules.LessThanOrEqual(model => model.DoubleOptionalNull, Double10));
                v.Add(v.Rules.LessThanOrEqual(model => model.FloatOptionalNull, Float10));
                v.Add(v.Rules.LessThanOrEqual(model => model.DecimalOptionalNull, Decimal10));
            });
            IValidationResult result = Validator.Validate(Parameters);
            IsValid(result);
        }
        [TestMethod]
        public void WithOptionalValueFail()
        {
            IValidator<UnitTestDbParams> Validator = ValidatorFactory.Create<UnitTestDbParams>(v => {
                v.Add(v.Rules.LessThanOrEqual(model => model.StringNum, Double9));
                v.Add(v.Rules.LessThanOrEqual(model => model.ShortOptional, Short9));
                v.Add(v.Rules.LessThanOrEqual(model => model.IntOptional, Int9));
                v.Add(v.Rules.LessThanOrEqual(model => model.LongOptional, Long9));
                v.Add(v.Rules.LessThanOrEqual(model => model.UShortOptional, UShort9));
                v.Add(v.Rules.LessThanOrEqual(model => model.UIntOptional, UInt9));
                v.Add(v.Rules.LessThanOrEqual(model => model.ULongOptional, ULong9));
                v.Add(v.Rules.LessThanOrEqual(model => model.DoubleOptional, Double9));
                v.Add(v.Rules.LessThanOrEqual(model => model.FloatOptional, Float9));
                v.Add(v.Rules.LessThanOrEqual(model => model.DecimalOptional, Decimal9));
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
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.StringInvalidNum, Double10));
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
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Short, Short11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Int, Int11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Long, Long11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UShort, UShort11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UInt, UInt11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ULong, ULong11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Double, Double11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Float, Float11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Decimal, Decimal11));

                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.StringNum, Double10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Short, Short10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Int, Int10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Long, Long10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UShort, UShort10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UInt, UInt10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ULong, ULong10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Double, Double10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Float, Float10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Decimal, Decimal10));
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
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.StringNum, Double9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Short, Short9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Int, Int9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Long, Long9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UShort, UShort9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UInt, UInt9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ULong, ULong9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Double, Double9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Float, Float9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.Decimal, Decimal9));
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
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.StringNum, Double11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ShortOptional, Short11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.IntOptional, Int11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.LongOptional, Long11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UShortOptional, UShort11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UIntOptional, UInt11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ULongOptional, ULong11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DoubleOptional, Double11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.FloatOptional, Float11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DecimalOptional, Decimal11));

                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.StringNum, Double10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ShortOptional, Short10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.IntOptional, Int10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.LongOptional, Long10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UShortOptional, UShort10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UIntOptional, UInt10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ULongOptional, ULong10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DoubleOptional, Double10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.FloatOptional, Float10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DecimalOptional, Decimal10));
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
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.StringNumNull, Double11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ShortOptionalNull, Short11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.IntOptionalNull, Int11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.LongOptionalNull, Long11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UShortOptionalNull, UShort11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UIntOptionalNull, UInt11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ULongOptionalNull, ULong11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DoubleOptionalNull, Double11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.FloatOptionalNull, Float11));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DecimalOptionalNull, Decimal11));

                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.StringNumNull, Double10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ShortOptionalNull, Short10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.IntOptionalNull, Int10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.LongOptionalNull, Long10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UShortOptionalNull, UShort10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UIntOptionalNull, UInt10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ULongOptionalNull, ULong10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DoubleOptionalNull, Double10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.FloatOptionalNull, Float10));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DecimalOptionalNull, Decimal10));
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
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.StringNum, Double9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ShortOptional, Short9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.IntOptional, Int9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.LongOptional, Long9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UShortOptional, UShort9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.UIntOptional, UInt9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.ULongOptional, ULong9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DoubleOptional, Double9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.FloatOptional, Float9));
                    await v.AddAsync(await v.Rules.LessThanOrEqualAsync(model => model.DecimalOptional, Decimal9));
                });
                IValidationResult result = await Validator.ValidateAsync(Parameters);
                await IsInvalidAsync(result);
                await HasCorrectErrorCountAsync(result, 10);
            });
        }
        #endregion
    }
}
