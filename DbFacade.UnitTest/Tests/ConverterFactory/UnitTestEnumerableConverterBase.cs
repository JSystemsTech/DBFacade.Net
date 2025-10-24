using DbFacade.Factories;

namespace DbFacade.UnitTest.Tests.ConverterFactory
{
    public abstract class UnitTestEnumerableConverterBase : UnitTestBase
    {
        private Random Randomizer { get; set; }
        private TTo RandomVal<TTo>(TTo expected, bool allowNull)
            => allowNull && Randomizer.Next(2) == 0 ? (TTo)(object)null : expected;


        private IEnumerable<TTo> RandomList<TTo>(TTo expected, bool allowNull = false)
            => Enumerable.Range(1, Randomizer.Next(20, 100)).Select(m => RandomVal(expected, allowNull));
        public UnitTestEnumerableConverterBase(ServiceFixture services) : base(services)
        {
            Randomizer = new Random();
        }

        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, char delimeter, bool allowNull = false)
        => TestStringEnumerableValueConverter(expected, delimeter, m => m.ToString(), (expected, actual) => Assert.Equal(expected, actual), allowNull);
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, char delimeter, Func<TTo, string> toString, bool allowNull = false)
        => TestStringEnumerableValueConverter(expected, delimeter, toString, (expected, actual) => Assert.Equal(expected, actual), allowNull);
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, char delimeter, Func<TTo, string> toString, Action<TTo, TTo> validator, bool allowNull = false)
        {
            TTo[] expectedArr = RandomList(expected, allowNull).ToArray();
            string input = string.Join(delimeter, expectedArr.Select(m => m == null ? null : toString(m)));
            TTo[] actual = ConversionFactory.ToEnumerable<TTo>(input, separator: delimeter).ToArray();
            Assert.Equal(expectedArr.Count(), actual.Count());
            for (int i = 0; i < expectedArr.Count(); i++)
            {
                var exp = expectedArr[i];
                var acc = actual[i];
                validator(exp, acc);
            }
        }
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, bool allowNull = false)
        {
            TTo[] expectedArr = RandomList(expected, allowNull).ToArray();
            var strArr = expectedArr.Select(m => m == null ? "" : m.ToString());
            string input = string.Join(',', strArr);
            TTo[] actual = ConversionFactory.ToEnumerable<TTo>(input).ToArray();
            Assert.Equal(expectedArr.Count(), actual.Count());
            for (int i = 0; i < expectedArr.Count(); i++)
            {
                var exp = expectedArr[i];
                var acc = actual[i];
                Assert.Equal(exp, acc);
            }
        }
    }
}
