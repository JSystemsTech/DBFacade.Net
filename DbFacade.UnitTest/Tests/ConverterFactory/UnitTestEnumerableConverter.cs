using DbFacade.Factories;

namespace DbFacade.UnitTest.Tests.ConverterFactory
{
    public class UnitTestEnumerableConverter : UnitTestEnumerableConverterBase
    {
        public UnitTestEnumerableConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestStringEnumerableValueConverter((byte)1);
        [Fact]
        public void ParseSbyte() => TestStringEnumerableValueConverter((sbyte)1);
        [Fact]
        public void ParseShort() => TestStringEnumerableValueConverter((short)5);
        [Fact]
        public void ParseUshort() => TestStringEnumerableValueConverter((ushort)10);
        [Fact]
        public void ParseInt() => TestStringEnumerableValueConverter(10);
        [Fact]
        public void ParseUint() => TestStringEnumerableValueConverter((uint)10);
        [Fact]
        public void ParseDouble() => TestStringEnumerableValueConverter((double)10.10);
        [Fact]
        public void ParseDecimal() => TestStringEnumerableValueConverter((decimal)0.1234);
        [Fact]
        public void ParseFloat() => TestStringEnumerableValueConverter((float)10.1234);
        [Fact]
        public void ParseDateTime() => TestStringEnumerableValueConverter(DateTime.Parse("2024-03-02 21:36:33"));
        [Fact]
        public void ParseDateTimeOffset() => TestStringEnumerableValueConverter(DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"));
        [Fact]
        public void ParseTimeSpan() => TestStringEnumerableValueConverter(DateTime.Now.TimeOfDay);
        [Fact]
        public void ParseGuid() => TestStringEnumerableValueConverter(Guid.NewGuid());
        [Fact]
        public void ParseChar() => TestStringEnumerableValueConverter('@');
        [Fact]
        public void ParseBool() => TestStringEnumerableValueConverter(true);
        [Fact]
        public void ParseString() => TestStringEnumerableValueConverter("test value");


        [Fact]
        public void ParseByte_Nullable() => TestStringEnumerableValueConverter((byte?)1, true);
        [Fact]
        public void ParseSbyte_Nullable() => TestStringEnumerableValueConverter((sbyte?)1, true);
        [Fact]
        public void ParseShort_Nullable() => TestStringEnumerableValueConverter((short?)5, true);
        [Fact]
        public void ParseUshort_Nullable() => TestStringEnumerableValueConverter((ushort?)10, true);
        [Fact]
        public void ParseInt_Nullable() => TestStringEnumerableValueConverter((int?)10, true);
        [Fact]
        public void ParseUint_Nullable() => TestStringEnumerableValueConverter((uint?)10, true);
        [Fact]
        public void ParseDouble_Nullable() => TestStringEnumerableValueConverter((double?)10.10, true);
        [Fact]
        public void ParseDecimal_Nullable() => TestStringEnumerableValueConverter((decimal?)0.1234, true);
        [Fact]
        public void ParseFloat_Nullable() => TestStringEnumerableValueConverter((float?)10.1234, true);
        [Fact]
        public void ParseDateTime_Nullable() => TestStringEnumerableValueConverter((DateTime?)DateTime.Parse("2024-03-02 21:36:33"), true);
        [Fact]
        public void ParseDateTimeOffset_Nullable() => TestStringEnumerableValueConverter((DateTimeOffset?)DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"), true);
        [Fact]
        public void ParseTimeSpan_Nullable() => TestStringEnumerableValueConverter((TimeSpan?)DateTime.Now.TimeOfDay, true);
        [Fact]
        public void ParseGuid_Nullable() => TestStringEnumerableValueConverter((Guid?)Guid.NewGuid(), true);
        [Fact]
        public void ParseChar_Nullable() => TestStringEnumerableValueConverter((char?)'@', true);
        [Fact]
        public void ParseBool_Nullable() => TestStringEnumerableValueConverter((bool?)true, true);
        [Fact]
        public void ParseString_Nullable() => TestStringEnumerableValueConverter("test value", ',', m => m, (expected, actual) =>
        {
            if (expected == null)
            {
                Assert.Equal("", actual);
            }
            else
            {
                Assert.Equal(expected, actual);
            }
        }, true);

        [Fact]
        public void ValidateEmptyStringArray()
        {
            var arr = ConversionFactory.ToEnumerable<string>("").ToArray();
            Assert.Empty(arr);
            
        }
        [Fact]
        public void ValidateWhiteSpaceArray()
        {
            var arr = ConversionFactory.ToEnumerable<string>(" ").ToArray();
            Assert.Empty(arr);

        }
        [Fact]
        public void ValidateNullArray()
        {
            var arr = ConversionFactory.ToEnumerable<string>(null).ToArray();
            Assert.Empty(arr);

        }

    }
}
