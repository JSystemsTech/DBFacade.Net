using DbFacade.UnitTest.DataLayer.Models.Data;

namespace DbFacade.UnitTest.Tests.ConverterFactory
{
    public class UnitTest_StringConverter : UnitTestConverterBase<string>
    {
        public UnitTest_StringConverter(ServiceFixture services) : base(services) { }
        protected override string Transform<TTo>(TTo value)
        => value.ToString();


        [Fact]
        public void ParseByte() => TestValueConverter((byte)1);
        [Fact]
        public void ParseSbyte() => TestValueConverter((sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter((short)5);
        [Fact]
        public void ParseUshort() => TestValueConverter((ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10);
        [Fact]
        public void ParseUint() => TestValueConverter((uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter((double)10.10);
        [Fact]
        public void ParseDecimal() => TestValueConverter((decimal)0.1234);
        [Fact]
        public void ParseFloat() => TestValueConverter((float)10.1234);
        [Fact]
        public void ParseDateTime() => TestValueConverter(DateTime.Parse("2024-03-02 21:36:33"));
        [Fact]
        public void ParseDateTimeOffset() => TestValueConverter(DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"));
        [Fact]
        public void ParseTimeSpan() => TestValueConverter(DateTime.Now.TimeOfDay);
        [Fact]
        public void ParseGuid() => TestValueConverter(Guid.NewGuid());
        [Fact]
        public void ParseChar() => TestValueConverter('@');
        [Fact]
        public void ParseBool() => TestValueConverter(true);
        [Fact]
        public void ParseString() => TestValueConverter("test value");
        [Fact]
        public void ParseTestEnum() => TestValueConverter(TestEnum.Yes);
        [Fact]
        public void ParseTestEnumYes() => TestValueConverter("1", TestEnum.Yes);
        [Fact]
        public void ParseTestEnumNo() => TestValueConverter("0", TestEnum.No);

        [Fact]
        public void FailsEnumParse() => TestValueConverter("3", TestEnum.Yes, (expected, actual) => {
            Assert.NotEqual(expected, actual);
        });


        [Fact]
        public void ParseByteOptional() => TestValueConverter((byte?)1);
        [Fact]
        public void ParseSbyteOptional() => TestValueConverter((sbyte?)1);
        [Fact]
        public void ParseShortOptional() => TestValueConverter((short?)5);
        [Fact]
        public void ParseUshortOptional() => TestValueConverter((ushort?)10);
        [Fact]
        public void ParseIntOptional() => TestValueConverter((int?)10);
        [Fact]
        public void ParseUintOptional() => TestValueConverter((uint?)10);
        [Fact]
        public void ParseDoubleOptional() => TestValueConverter((double?)10.10);
        [Fact]
        public void ParseDecimalOptional() => TestValueConverter((decimal?)0.1234);
        [Fact]
        public void ParseDateTimeOptional() => TestValueConverter((DateTime?)DateTime.Parse("2024-03-02 21:36:33"));
        [Fact]
        public void ParseDateTimeOffsetOptional() => TestValueConverter((DateTimeOffset?)DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"));
        [Fact]
        public void ParseTimeSpanOptional() => TestValueConverter((TimeSpan?)DateTime.Now.TimeOfDay);
        [Fact]
        public void ParseGuidOptional() => TestValueConverter((Guid?)Guid.NewGuid());
        [Fact]
        public void ParseCharOptional() => TestValueConverter((char?)'@');
        [Fact]
        public void ParseBoolOptional() => TestValueConverter((bool?)true);



        [Fact]
        public void ParseByteOptionalNull() => TestValueConverter((byte?)null);
        [Fact]
        public void ParseSbyteOptionalNull() => TestValueConverter((sbyte?)null);
        [Fact]
        public void ParseShortOptionalNull() => TestValueConverter((short?)null);
        [Fact]
        public void ParseUshortOptionalNull() => TestValueConverter((ushort?)null);
        [Fact]
        public void ParseIntOptionalNull() => TestValueConverter((int?)null);
        [Fact]
        public void ParseUintOptionalNull() => TestValueConverter((uint?)null);
        [Fact]
        public void ParseDoubleOptionalNull() => TestValueConverter((double?)null);
        [Fact]
        public void ParseDecimalOptionalNull() => TestValueConverter((decimal?)null);
        [Fact]
        public void ParseDateTimeOptionalNull() => TestValueConverter((DateTime?)null);
        [Fact]
        public void ParseDateTimeOffsetOptionalNull() => TestValueConverter((DateTimeOffset?)null);
        [Fact]
        public void ParseTimeSpanOptionalNull() => TestValueConverter((TimeSpan?)null);
        [Fact]
        public void ParseGuidOptionalNull() => TestValueConverter((Guid?)null);
        [Fact]
        public void ParseCharOptionalNull() => TestValueConverter((char?)null);
        [Fact]
        public void ParseBoolOptionalNull() => TestValueConverter((bool?)null);


    }

    public class UnitTest_ByteConverter : UnitTestConverterBase<byte>
    {
        public UnitTest_ByteConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter(1, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) => {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_SByteConverter : UnitTestConverterBase<sbyte>
    {
        public UnitTest_SByteConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter(1, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_ShortConverter : UnitTestConverterBase<short>
    {
        public UnitTest_ShortConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter(1, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_UShortConverter : UnitTestConverterBase<ushort>
    {
        public UnitTest_UShortConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter(1, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_IntegerConverter : UnitTestConverterBase<int>
    {
        public UnitTest_IntegerConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter(1, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_UIntegerConverter : UnitTestConverterBase<uint>
    {
        public UnitTest_UIntegerConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter(1, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_LongConverter : UnitTestConverterBase<long>
    {
        public UnitTest_LongConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter(1, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_ULongConverter : UnitTestConverterBase<ulong>
    {
        public UnitTest_ULongConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter(1, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_DoubleConverter : UnitTestConverterBase<double>
    {
        public UnitTest_DoubleConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter((double)1.0, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_FloatConverter : UnitTestConverterBase<float>
    {
        public UnitTest_FloatConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter((float)1.0, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_DecimalConverter : UnitTestConverterBase<decimal>
    {
        public UnitTest_DecimalConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Fact]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Fact]
        public void ParseInt() => TestValueConverter(10, 10);
        [Fact]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Fact]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Fact]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Fact]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Fact]
        public void ParseChar() => TestValueConverter((decimal)1.0, '1');
        [Fact]
        public void ParseString() => TestValueConverter(10, "10");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter(3, TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }

    public class UnitTest_BooleanConverter_True : UnitTestConverterBase<bool>
    {
        public UnitTest_BooleanConverter_True(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseString() => TestValueConverter(true, "True");

    }

    public class UnitTest_BooleanConverter_False : UnitTestConverterBase<bool>
    {
        public UnitTest_BooleanConverter_False(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseString() => TestValueConverter(false, "False");
    }

    public class UnitTest_CharConverter : UnitTestConverterBase<char>
    {
        public UnitTest_CharConverter(ServiceFixture services) : base(services) { }
        [Fact]
        public void ParseByte() => TestValueConverter('1', (byte)1);
        [Fact]
        public void ParseSByte() => TestValueConverter('1', (sbyte)1);
        [Fact]
        public void ParseShort() => TestValueConverter('1', (short)1);
        [Fact]
        public void ParseUshort() => TestValueConverter('1', (ushort)1);
        [Fact]
        public void ParseInt() => TestValueConverter('1', 1);
        [Fact]
        public void ParseUint() => TestValueConverter('1', (uint)1);
        [Fact]
        public void ParseDouble() => TestValueConverter('1', (double)1);
        [Fact]
        public void ParseDecimal() => TestValueConverter('1', (decimal)1);
        [Fact]
        public void ParseFloat() => TestValueConverter('1', (float)1);
        [Fact]
        public void ParseChar() => TestValueConverter('1', '1');
        [Fact]
        public void ParseString() => TestValueConverter('1', "1");
        [Fact]
        public void ParseTestEnum0() => TestValueConverter('0', TestEnumChar.No);
        [Fact]
        public void ParseTestEnum1() => TestValueConverter('1', TestEnumChar.Yes);
        [Fact]
        public void FailsEnumParse() => TestValueConverter('3', TestEnum.Yes, (expected, actual) =>
        {
            Assert.NotEqual(expected, actual);
        });

    }
}
