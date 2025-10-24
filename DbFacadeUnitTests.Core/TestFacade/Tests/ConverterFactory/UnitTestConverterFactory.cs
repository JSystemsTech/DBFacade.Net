using DbFacade.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace DbFacadeUnitTests.Core.TestFacade.Tests.ConverterFactory
{
    public abstract class UnitTestConverterBase<TFrom> : UnitTestBase
    {
        protected virtual TFrom Transform<TTo>(TTo value) => value is TFrom val ? val : default(TFrom);
        protected void TestValueConverter<TTo>(TTo expected)
        => TestValueConverter(expected, (expected, actual) => Assert.AreEqual(expected, actual));
        protected void TestValueConverter<TTo>(TTo expected, Func<TTo, TFrom> transform, Action<TTo, TTo> validator)
            => TestValueConverter(expected == null ? default(TFrom) : transform(expected), expected, validator);
        protected void TestValueConverter<TTo>(TTo expected, Action<TTo, TTo> validator)
            => TestValueConverter(expected == null ? default(TFrom) : Transform(expected), expected, validator);
        protected void TestValueConverter<TTo>(TFrom input, TTo expected)
        => TestValueConverter(input, expected, (expected, actual) => Assert.AreEqual(expected, actual));
        protected void TestValueConverter<TTo>(TFrom input, TTo expected, Action<TTo, TTo> validator)
        {
            TTo actual = ConversionFactory.GetValue<TTo>(input);
            validator(expected, actual);
        }
    }
    [TestClass]
    public class UnitTest_StringConverter : UnitTestConverterBase<string>
    {

        protected override string Transform<TTo>(TTo value)
        => value.ToString();


        [TestMethod]
        public void ParseByte() => TestValueConverter((byte)1);
        [TestMethod]
        public void ParseSbyte() => TestValueConverter((sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter((short)5);
        [TestMethod]
        public void ParseUshort() => TestValueConverter((ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter((int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter((uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter((double)10.10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter((decimal)0.1234);
        [TestMethod]
        public void ParseFloat() => TestValueConverter((float)10.1234);
        [TestMethod]
        public void ParseDateTime() => TestValueConverter(DateTime.Parse("2024-03-02 21:36:33"));
        [TestMethod]
        public void ParseDateTimeOffset() => TestValueConverter(DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"));
        [TestMethod]
        public void ParseTimeSpan() => TestValueConverter(DateTime.Now.TimeOfDay);
        [TestMethod]
        public void ParseGuid() => TestValueConverter(Guid.NewGuid());
        [TestMethod]
        public void ParseChar() => TestValueConverter('@');
        [TestMethod]
        public void ParseBool() => TestValueConverter(true);
        [TestMethod]
        public void ParseString() => TestValueConverter("test value");
        [TestMethod]
        public void ParseTestEnum() => TestValueConverter(TestEnum.Yes);
        [TestMethod]
        public void ParseTestEnumYes() => TestValueConverter("1",TestEnum.Yes);
        [TestMethod]
        public void ParseTestEnumNo() => TestValueConverter("0",TestEnum.No);


        [TestMethod]
        public void ParseByteOptional() => TestValueConverter((byte?)1);
        [TestMethod]
        public void ParseSbyteOptional() => TestValueConverter((sbyte?)1);
        [TestMethod]
        public void ParseShortOptional() => TestValueConverter((short?)5);
        [TestMethod]
        public void ParseUshortOptional() => TestValueConverter((ushort?)10);
        [TestMethod]
        public void ParseIntOptional() => TestValueConverter((int?)10);
        [TestMethod]
        public void ParseUintOptional() => TestValueConverter((uint?)10);
        [TestMethod]
        public void ParseDoubleOptional() => TestValueConverter((double?)10.10);
        [TestMethod]
        public void ParseDecimalOptional() => TestValueConverter((decimal?)0.1234);
        [TestMethod]
        public void ParseDateTimeOptional() => TestValueConverter((DateTime?)DateTime.Parse("2024-03-02 21:36:33"));
        [TestMethod]
        public void ParseDateTimeOffsetOptional() => TestValueConverter((DateTimeOffset?)DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"));
        [TestMethod]
        public void ParseTimeSpanOptional() => TestValueConverter((TimeSpan?)DateTime.Now.TimeOfDay);
        [TestMethod]
        public void ParseGuidOptional() => TestValueConverter((Guid?)Guid.NewGuid());
        [TestMethod]
        public void ParseCharOptional() => TestValueConverter((char?)'@');
        [TestMethod]
        public void ParseBoolOptional() => TestValueConverter((bool?)true);



        [TestMethod]
        public void ParseByteOptionalNull() => TestValueConverter((byte?)null);
        [TestMethod]
        public void ParseSbyteOptionalNull() => TestValueConverter((sbyte?)null);
        [TestMethod]
        public void ParseShortOptionalNull() => TestValueConverter((short?)null);
        [TestMethod]
        public void ParseUshortOptionalNull() => TestValueConverter((ushort?)null);
        [TestMethod]
        public void ParseIntOptionalNull() => TestValueConverter((int?)null);
        [TestMethod]
        public void ParseUintOptionalNull() => TestValueConverter((uint?)null);
        [TestMethod]
        public void ParseDoubleOptionalNull() => TestValueConverter((double?)null);
        [TestMethod]
        public void ParseDecimalOptionalNull() => TestValueConverter((decimal?)null);
        [TestMethod]
        public void ParseDateTimeOptionalNull() => TestValueConverter((DateTime?)null);
        [TestMethod]
        public void ParseDateTimeOffsetOptionalNull() => TestValueConverter((DateTimeOffset?)null);
        [TestMethod]
        public void ParseTimeSpanOptionalNull() => TestValueConverter((TimeSpan?)null);
        [TestMethod]
        public void ParseGuidOptionalNull() => TestValueConverter((Guid?)null);
        [TestMethod]
        public void ParseCharOptionalNull() => TestValueConverter((char?)null);
        [TestMethod]
        public void ParseBoolOptionalNull() => TestValueConverter((bool?)null);


    }
    [TestClass]
    public class UnitTest_ByteConverter : UnitTestConverterBase<byte>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter(1, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_SByteConverter : UnitTestConverterBase<sbyte>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter(1, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_ShortConverter : UnitTestConverterBase<short>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter(1, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_UShortConverter : UnitTestConverterBase<ushort>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter(1, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_IntegerConverter : UnitTestConverterBase<int>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter(1, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_UIntegerConverter : UnitTestConverterBase<uint>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter(1, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_LongConverter : UnitTestConverterBase<long>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter(1, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_ULongConverter : UnitTestConverterBase<ulong>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter(1, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_DoubleConverter : UnitTestConverterBase<double>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter((double)1.0, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_FloatConverter : UnitTestConverterBase<float>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter((float)1.0, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_DecimalConverter : UnitTestConverterBase<decimal>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [TestMethod]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [TestMethod]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [TestMethod]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [TestMethod]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [TestMethod]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [TestMethod]
        public void ParseChar() => TestValueConverter((decimal)1.0, '1');
        [TestMethod]
        public void ParseString() => TestValueConverter(10, "10");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    [TestClass]
    public class UnitTest_BooleanConverter_True : UnitTestConverterBase<bool>
    {
        [TestMethod]
        public void ParseString() => TestValueConverter(true, "True");

    }
    [TestClass]
    public class UnitTest_BooleanConverter_False : UnitTestConverterBase<bool>
    {
        [TestMethod]
        public void ParseString() => TestValueConverter(false, "False");
    }
    [TestClass]
    public class UnitTest_CharConverter : UnitTestConverterBase<char>
    {
        [TestMethod]
        public void ParseByte() => TestValueConverter('1', (byte)1);
        [TestMethod]
        public void ParseSByte() => TestValueConverter('1', (sbyte)1);
        [TestMethod]
        public void ParseShort() => TestValueConverter('1', (short)1);
        [TestMethod]
        public void ParseUshort() => TestValueConverter('1', (ushort)1);
        [TestMethod]
        public void ParseInt() => TestValueConverter('1', 1);
        [TestMethod]
        public void ParseUint() => TestValueConverter('1', (uint)1);
        [TestMethod]
        public void ParseDouble() => TestValueConverter('1', (double)1);
        [TestMethod]
        public void ParseDecimal() => TestValueConverter('1', (decimal)1);
        [TestMethod]
        public void ParseFloat() => TestValueConverter('1', (float)1);
        [TestMethod]
        public void ParseChar() => TestValueConverter('1', '1');
        [TestMethod]
        public void ParseString() => TestValueConverter('1', "1");
        [TestMethod]
        public void ParseTestEnum0() => TestValueConverter('0', TestEnumChar.No);
        [TestMethod]
        public void ParseTestEnum1() => TestValueConverter('1', TestEnumChar.Yes);

    }

    public abstract class UnitTestEnumerableConverterBase : UnitTestBase
    {
        private Random Randomizer { get; set; }
        private TTo RandomVal<TTo>(TTo expected, bool allowNull)
            => allowNull && Randomizer.Next(2) == 0 ? (TTo)(object)null : expected;


        private IEnumerable<TTo> RandomList<TTo>(TTo expected, bool allowNull = false)
            => Enumerable.Range(1, Randomizer.Next(20, 100)).Select(m => RandomVal(expected, allowNull));
        public UnitTestEnumerableConverterBase()
        {
            Randomizer = new Random();
        }

        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, char delimeter, bool allowNull = false)
        => TestStringEnumerableValueConverter(expected, delimeter, m => m.ToString(), (expected, actual) => Assert.AreEqual(expected, actual), allowNull);
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, char delimeter, Func<TTo, string> toString, bool allowNull = false)
        => TestStringEnumerableValueConverter(expected, delimeter, toString, (expected, actual) => Assert.AreEqual(expected, actual), allowNull);
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, char delimeter, Func<TTo, string> toString, Action<TTo, TTo> validator, bool allowNull = false)
        {
            TTo[] expectedArr = RandomList(expected, allowNull).ToArray();
            string input = string.Join(delimeter, expectedArr.Select(m => m == null ? (string)null : toString(m)));
            TTo[] actual = ConversionFactory.ToEnumerable<TTo>(input, separator: delimeter).ToArray();
            Assert.AreEqual(expectedArr.Count(), actual.Count());
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
            Assert.AreEqual(expectedArr.Count(),actual.Count());
            for (int i = 0; i < expectedArr.Count(); i++)
            {
                var exp = expectedArr[i];
                var acc = actual[i];
                Assert.AreEqual(exp, acc);
            }
        }
    }
    [TestClass]
    public class UnitTestEnumerableConverter : UnitTestEnumerableConverterBase
    {
        [TestMethod]
        public void ParseByte() => TestStringEnumerableValueConverter((byte)1);
        [TestMethod]
        public void ParseSbyte() => TestStringEnumerableValueConverter((sbyte)1);
        [TestMethod]
        public void ParseShort() => TestStringEnumerableValueConverter((short)5);
        [TestMethod]
        public void ParseUshort() => TestStringEnumerableValueConverter((ushort)10);
        [TestMethod]
        public void ParseInt() => TestStringEnumerableValueConverter((int)10);
        [TestMethod]
        public void ParseUint() => TestStringEnumerableValueConverter((uint)10);
        [TestMethod]
        public void ParseDouble() => TestStringEnumerableValueConverter((double)10.10);
        [TestMethod]
        public void ParseDecimal() => TestStringEnumerableValueConverter((decimal)0.1234);
        [TestMethod]
        public void ParseFloat() => TestStringEnumerableValueConverter((float)10.1234);
        [TestMethod]
        public void ParseDateTime() => TestStringEnumerableValueConverter(DateTime.Parse("2024-03-02 21:36:33"));
        [TestMethod]
        public void ParseDateTimeOffset() => TestStringEnumerableValueConverter(DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"));
        [TestMethod]
        public void ParseTimeSpan() => TestStringEnumerableValueConverter(DateTime.Now.TimeOfDay);
        [TestMethod]
        public void ParseGuid() => TestStringEnumerableValueConverter(Guid.NewGuid());
        [TestMethod]
        public void ParseChar() => TestStringEnumerableValueConverter('@');
        [TestMethod]
        public void ParseBool() => TestStringEnumerableValueConverter(true);
        [TestMethod]
        public void ParseString() => TestStringEnumerableValueConverter("test value");


        [TestMethod]
        public void ParseByte_Nullable() => TestStringEnumerableValueConverter((byte?)1, true);
        [TestMethod]
        public void ParseSbyte_Nullable() => TestStringEnumerableValueConverter((sbyte?)1, true);
        [TestMethod]
        public void ParseShort_Nullable() => TestStringEnumerableValueConverter((short?)5, true);
        [TestMethod]
        public void ParseUshort_Nullable() => TestStringEnumerableValueConverter((ushort?)10, true);
        [TestMethod]
        public void ParseInt_Nullable() => TestStringEnumerableValueConverter((int?)10, true);
        [TestMethod]
        public void ParseUint_Nullable() => TestStringEnumerableValueConverter((uint?)10, true);
        [TestMethod]
        public void ParseDouble_Nullable() => TestStringEnumerableValueConverter((double?)10.10, true);
        [TestMethod]
        public void ParseDecimal_Nullable() => TestStringEnumerableValueConverter((decimal?)0.1234, true);
        [TestMethod]
        public void ParseFloat_Nullable() => TestStringEnumerableValueConverter((float?)10.1234, true);
        [TestMethod]
        public void ParseDateTime_Nullable() => TestStringEnumerableValueConverter((DateTime?)DateTime.Parse("2024-03-02 21:36:33"), true);
        [TestMethod]
        public void ParseDateTimeOffset_Nullable() => TestStringEnumerableValueConverter((DateTimeOffset?)DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"), true);
        [TestMethod]
        public void ParseTimeSpan_Nullable() => TestStringEnumerableValueConverter((TimeSpan?)DateTime.Now.TimeOfDay, true);
        [TestMethod]
        public void ParseGuid_Nullable() => TestStringEnumerableValueConverter((Guid?)Guid.NewGuid(), true);
        [TestMethod]
        public void ParseChar_Nullable() => TestStringEnumerableValueConverter((char?)'@', true);
        [TestMethod]
        public void ParseBool_Nullable() => TestStringEnumerableValueConverter((bool?)true, true);
        [TestMethod]
        public void ParseString_Nullable() => TestStringEnumerableValueConverter("test value", ',', m => m, (expected, actual) => {
            if (expected == null)
            {
                Assert.AreEqual("", actual);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
        }, true);
        
    }
}
