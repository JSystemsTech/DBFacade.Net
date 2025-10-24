using DbFacade.Factories;
using DbFacade.UnitTest.DataLayer.Models.Data;

namespace DbFacade.UnitTest.Tests.ConverterFactory
{
    public abstract class UnitTestConverterBase<TFrom> : UnitTestBase
    {
        public UnitTestConverterBase(ServiceFixture services) : base(services) { }
        protected virtual TFrom Transform<TTo>(TTo value) => value is TFrom val ? val : default;
        protected void TestValueConverter<TTo>(TTo expected)
        => TestValueConverter(expected, (expected, actual) => Assert.Equal(expected, actual));
        protected void TestValueConverter<TTo>(TTo expected, Func<TTo, TFrom> transform, Action<TTo, TTo> validator)
            => TestValueConverter(expected == null ? default : transform(expected), expected, validator);
        protected void TestValueConverter<TTo>(TTo expected, Action<TTo, TTo> validator)
            => TestValueConverter(expected == null ? default : Transform(expected), expected, validator);
        protected void TestValueConverter<TTo>(TFrom input, TTo expected)
        => TestValueConverter(input, expected, (expected, actual) => Assert.Equal(expected, actual));
        protected void TestValueConverter<TTo>(TFrom input, TTo expected, Action<TTo, TTo> validator)
        {
            TTo actual = ConversionFactory.GetValue<TTo>(input);
            validator(expected, actual);
        }
    }

    public class UnitTestConverterMisc : UnitTestBase
    {
        public UnitTestConverterMisc(ServiceFixture services) : base(services) { }
        
        [Fact]
        public void TestInvalidConvertReturnsNull()
        {
            object result = ConversionFactory.GetValue<byte?>(DateTime.Now);
            Assert.Null(result);
        }
        [Fact]
        public void TestInvalidEnumConvertReturnsDefault()
        {
            object result = ConversionFactory.GetValue<TestEnum>("badEnumStr");
            Assert.Equal(TestEnum.No,result);
        }
        [Fact]
        public void TestIsNumericNullableType()
        {
            int? value = 1;
            Assert.True(ConversionFactory.IsNumericValue(value));
        }
    }
}
