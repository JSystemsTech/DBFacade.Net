using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace DbFacade.Utils.UnitTests
{
    public abstract class UnitTestConverterBase<TFrom> : UnitTestBase
    {
        protected virtual TFrom Transform<TTo>(TTo value) => value is TFrom val ? val : default(TFrom);
        protected void TestValueConverter<TTo>(TTo expected)
        => TestValueConverter(expected, (expected, actual) => Assert.That(actual, Is.EqualTo(expected)));
        protected void TestValueConverter<TTo>(TTo expected, Func<TTo, TFrom> transform, Action<TTo, TTo> validator)
            => TestValueConverter(expected == null ? default(TFrom) : transform(expected), expected, validator);
        protected void TestValueConverter<TTo>(TTo expected, Action<TTo, TTo> validator)
            => TestValueConverter(expected == null ? default(TFrom) : Transform(expected), expected, validator);
        protected void TestValueConverter<TTo>(TFrom input, TTo expected)
        => TestValueConverter(input, expected, (expected, actual) => Assert.That(actual, Is.EqualTo(expected)));
        protected void TestValueConverter<TTo>(TFrom input, TTo expected, Action<TTo, TTo> validator)
        {
            bool success = input.TryParse(out TTo actual);
            Assert.That(success, Is.True, $"expected {expected} but got {actual}");
            validator(expected, actual);
        }
    }
    public class UnitTest_StringConverter: UnitTestConverterBase<string>
    {

        protected override string Transform<TTo>(TTo value)
        => value.ToString();
              
    
        [Test]
        public void ParseByte() => TestValueConverter((byte)1);
        [Test]
        public void ParseSbyte() => TestValueConverter((sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter((short)5);
        [Test]
        public void ParseUshort() => TestValueConverter((ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter((int)10);
        [Test]
        public void ParseUint() => TestValueConverter((uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter((double)10.10);
        [Test]
        public void ParseDecimal() => TestValueConverter((decimal)0.1234);
        [Test]
        public void ParseFloat() => TestValueConverter((float)10.1234);
        [Test]
        public void ParseDateTime() => TestValueConverter(DateTime.Parse("2024-03-02 21:36:33"));
        [Test]
        public void ParseDateTimeOffset() => TestValueConverter(DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"));
        [Test]
        public void ParseTimeSpan() => TestValueConverter(DateTime.Now.TimeOfDay);
        [Test]
        public void ParseGuid() => TestValueConverter(Guid.NewGuid());
        [Test]
        public void ParseChar() => TestValueConverter('@');
        [Test]
        public void ParseBool() => TestValueConverter(true);
        [Test]
        public void ParseString() => TestValueConverter("test value");
        [Test]
        public void ParseMailAddress() => TestValueConverter(new MailAddress("some.test@email.com"));
        [Test]
        public void ParseTestEnum() => TestValueConverter(TestEnum.Yes);
        [Test]
        public void ParseUserName() => TestValueConverter(new UserName("Test,R,User"), m => m.ToString(), (expected, actual) => {
            Assert.That(actual.First, Is.EqualTo(expected.First));
            Assert.That(actual.Middle, Is.EqualTo(expected.Middle));
            Assert.That(actual.Last, Is.EqualTo(expected.Last));
        });
        [Test]
        public void ParseCustom()
        {
            (string first, string middle, string last) expected = ("Test", "R", "User");
            TestValueConverter(expected, m => $"{m.first},{m.middle},{m.last}", (expected, actual) => {
                Assert.That(actual.first, Is.EqualTo(expected.first));
                Assert.That(actual.middle, Is.EqualTo(expected.middle));
                Assert.That(actual.last, Is.EqualTo(expected.last));
            });
        }
    
    
        [Test]
        public void ParseByteOptional() => TestValueConverter((byte?)1);
        [Test]
        public void ParseSbyteOptional() => TestValueConverter((sbyte?)1);
        [Test]
        public void ParseShortOptional() => TestValueConverter((short?)5);
        [Test]
        public void ParseUshortOptional() => TestValueConverter((ushort?)10);
        [Test]
        public void ParseIntOptional() => TestValueConverter((int?)10);
        [Test]
        public void ParseUintOptional() => TestValueConverter((uint?)10);
        [Test]
        public void ParseDoubleOptional() => TestValueConverter((double?)10.10);
        [Test]
        public void ParseDecimalOptional() => TestValueConverter((decimal?)0.1234);
        [Test]
        public void ParseDateTimeOptional() => TestValueConverter((DateTime?)DateTime.Parse("2024-03-02 21:36:33"));
        [Test]
        public void ParseDateTimeOffsetOptional() => TestValueConverter((DateTimeOffset?)DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"));
        [Test]
        public void ParseTimeSpanOptional() => TestValueConverter((TimeSpan?)DateTime.Now.TimeOfDay);
        [Test]
        public void ParseGuidOptional() => TestValueConverter((Guid?)Guid.NewGuid());
        [Test]
        public void ParseCharOptional() => TestValueConverter((char?)'@');
        [Test]
        public void ParseBoolOptional() => TestValueConverter((bool?)true);
    
    
    
        [Test]
        public void ParseByteOptionalNull() => TestValueConverter((byte?)null);
        [Test]
        public void ParseSbyteOptionalNull() => TestValueConverter((sbyte?)null);
        [Test]
        public void ParseShortOptionalNull() => TestValueConverter((short?)null);
        [Test]
        public void ParseUshortOptionalNull() => TestValueConverter((ushort?)null);
        [Test]
        public void ParseIntOptionalNull() => TestValueConverter((int?)null);
        [Test]
        public void ParseUintOptionalNull() => TestValueConverter((uint?)null);
        [Test]
        public void ParseDoubleOptionalNull() => TestValueConverter((double?)null);
        [Test]
        public void ParseDecimalOptionalNull() => TestValueConverter((decimal?)null);
        [Test]
        public void ParseDateTimeOptionalNull() => TestValueConverter((DateTime?)null);
        [Test]
        public void ParseDateTimeOffsetOptionalNull() => TestValueConverter((DateTimeOffset?)null);
        [Test]
        public void ParseTimeSpanOptionalNull() => TestValueConverter((TimeSpan?)null);
        [Test]
        public void ParseGuidOptionalNull() => TestValueConverter((Guid?)null);
        [Test]
        public void ParseCharOptionalNull() => TestValueConverter((char?)null);
        [Test]
        public void ParseBoolOptionalNull() => TestValueConverter((bool?)null);

        
    }
    public class UnitTest_ShortConverter : UnitTestConverterBase<short>
    {
        [Test]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Test]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [Test]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Test]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Test]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Test]
        public void ParseChar() => TestValueConverter(1, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(10, "10");
        [Test]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    public class UnitTest_UShortConverter : UnitTestConverterBase<ushort>
    {
        [Test]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Test]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [Test]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Test]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Test]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Test]
        public void ParseChar() => TestValueConverter(1, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(10, "10");
        [Test]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    public class UnitTest_IntegerConverter : UnitTestConverterBase<int>
    {
        [Test]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Test]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [Test]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Test]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Test]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Test]
        public void ParseChar() => TestValueConverter(1, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(10, "10");
        [Test]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    public class UnitTest_UIntegerConverter : UnitTestConverterBase<uint>
    {
        [Test]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Test]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [Test]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Test]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Test]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Test]
        public void ParseChar() => TestValueConverter(1, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(10, "10");
        [Test]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    public class UnitTest_LongConverter : UnitTestConverterBase<long>
    {
        [Test]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Test]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [Test]
        public void ParseUint() => TestValueConverter(10,(uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Test]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Test]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Test]
        public void ParseChar() => TestValueConverter(1, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(10, "10");
        [Test]
        public void ParseTestEnum0() => TestValueConverter(0,TestEnum.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    public class UnitTest_ULongConverter : UnitTestConverterBase<ulong>
    {
        [Test]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Test]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [Test]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Test]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Test]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Test]
        public void ParseChar() => TestValueConverter(1, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(10, "10");
        [Test]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    public class UnitTest_DoubleConverter : UnitTestConverterBase<double>
    {
        [Test]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Test]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [Test]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Test]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10);
        [Test]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Test]
        public void ParseChar() => TestValueConverter((double)1.0, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(10, "10");
        [Test]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    public class UnitTest_FloatConverter : UnitTestConverterBase<float>
    {
        [Test]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Test]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [Test]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Test]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10); 
        [Test]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Test]
        public void ParseChar() => TestValueConverter((float)1.0, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(10, "10");
        [Test]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    public class UnitTest_DecimalConverter : UnitTestConverterBase<decimal>
    {
        [Test]
        public void ParseByte() => TestValueConverter(1, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(1, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(10, (short)10);
        [Test]
        public void ParseUshort() => TestValueConverter(10, (ushort)10);
        [Test]
        public void ParseInt() => TestValueConverter(10, (int)10);
        [Test]
        public void ParseUint() => TestValueConverter(10, (uint)10);
        [Test]
        public void ParseDouble() => TestValueConverter(10, (double)10);
        [Test]
        public void ParseDecimal() => TestValueConverter(10, (decimal)10); 
        [Test]
        public void ParseFloat() => TestValueConverter(10, (float)10);
        [Test]
        public void ParseChar() => TestValueConverter((decimal)1.0, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(10, "10");
        [Test]
        public void ParseTestEnum0() => TestValueConverter(0, TestEnum.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter(1, TestEnum.Yes);

    }
    public class UnitTest_BooleanConverter_True : UnitTestConverterBase<bool>
    {
        [Test]
        public void ParseByte() => TestValueConverter(true, (byte)1);
        [Test]
        public void ParseSByte() => TestValueConverter(true, (sbyte)1);
        [Test]
        public void ParseShort() => TestValueConverter(true, (short)1);
        [Test]
        public void ParseUshort() => TestValueConverter(true, (ushort)1);
        [Test]
        public void ParseInt() => TestValueConverter(true, (int)1);
        [Test]
        public void ParseUint() => TestValueConverter(true, (uint)1);
        [Test]
        public void ParseDouble() => TestValueConverter(true, (double)1);
        [Test]
        public void ParseDecimal() => TestValueConverter(true, (decimal)1);
        [Test]
        public void ParseFloat() => TestValueConverter(true, (float)1);
        [Test]
        public void ParseChar() => TestValueConverter(true, (char)1);
        [Test]
        public void ParseString() => TestValueConverter(true, "True");
        [Test]
        public void ParseTestEnum1() => TestValueConverter(true, TestEnum.Yes);

    }
    public class UnitTest_BooleanConverter_False : UnitTestConverterBase<bool>
    {
        [Test]
        public void ParseByte() => TestValueConverter(false, (byte)0);
        [Test]
        public void ParseSByte() => TestValueConverter(false, (sbyte)0);
        [Test]
        public void ParseShort() => TestValueConverter(false, (short)0);
        [Test]
        public void ParseUshort() => TestValueConverter(false, (ushort)0);
        [Test]
        public void ParseInt() => TestValueConverter(false, (int)0);
        [Test]
        public void ParseUint() => TestValueConverter(false, (uint)0);
        [Test]
        public void ParseDouble() => TestValueConverter(false, (double)0);
        [Test]
        public void ParseDecimal() => TestValueConverter(false, (decimal)0);
        [Test]
        public void ParseFloat() => TestValueConverter(false, (float)0);
        [Test]
        public void ParseChar() => TestValueConverter(false, (char)0);
        [Test]
        public void ParseString() => TestValueConverter(false, "False");
        [Test]
        public void ParseTestEnum1() => TestValueConverter(false, TestEnum.No);

    }
    public class UnitTest_CharConverter : UnitTestConverterBase<char>
    {
        [Test]
        public void ParseByte() => TestValueConverter('1', (byte)49);
        [Test]
        public void ParseSByte() => TestValueConverter('1', (sbyte)49);
        [Test]
        public void ParseShort() => TestValueConverter('1', (short)49);
        [Test]
        public void ParseUshort() => TestValueConverter('1', (ushort)49);
        [Test]
        public void ParseInt() => TestValueConverter('1', (int)49);
        [Test]
        public void ParseUint() => TestValueConverter('1', (uint)49);
        [Test]
        public void ParseDouble() => TestValueConverter('1', (double)49);
        [Test]
        public void ParseDecimal() => TestValueConverter('1', (decimal)49);
        [Test]
        public void ParseFloat() => TestValueConverter('1', (float)49);
        [Test]
        public void ParseChar() => TestValueConverter('1', '1');
        [Test]
        public void ParseString() => TestValueConverter('1', "1");
        [Test]
        public void ParseTestEnum0() => TestValueConverter('0', TestEnumChar.No);
        [Test]
        public void ParseTestEnum1() => TestValueConverter('1', TestEnumChar.Yes);

    }

    public abstract class UnitTestEnumerableConverterBase : UnitTestBase
    {
        private  Random Randomizer { get; set; }
        private TTo RandomVal<TTo>(TTo expected, bool allowNull)
            => allowNull && Randomizer.Next(0, 1) == 0 ? (TTo)(object)null : expected;

        
        private IEnumerable<TTo> RandomList<TTo>(TTo expected, bool allowNull = false)
            => Enumerable.Range(1, Randomizer.Next(20, 100)).Select(m => RandomVal(expected, allowNull));
        public UnitTestEnumerableConverterBase()
        {
            Randomizer = new Random();
        }
        
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, char delimeter, bool allowNull = false)
        => TestStringEnumerableValueConverter(expected, delimeter, m => m.ToString(), (expected, actual) => Assert.That(actual, Is.EqualTo(expected)), allowNull);
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, char delimeter, Func<TTo, string> toString, bool allowNull = false)
        => TestStringEnumerableValueConverter(expected, delimeter, toString, (expected, actual) => Assert.That(actual, Is.EqualTo(expected)), allowNull);
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, char delimeter,  Func<TTo, string> toString, Action<TTo, TTo> validator, bool allowNull = false)
        {
            IEnumerable<TTo> expectedArr = RandomList(expected, allowNull);
            string input = string.Join(delimeter, expectedArr.Select(m => m == null ? (string)null : toString(m)));
            bool success = input.TryParseEnumerable(delimeter, out IEnumerable<TTo> actual);
            Assert.That(success, Is.True);
            Assert.That(actual.Count(), Is.EqualTo(expectedArr.Count()));
            for (int i = 0; i < expectedArr.Count(); i++)
            {
                var exp = expectedArr.ElementAt(i);
                var acc = actual.ElementAt(i);
                validator(exp, acc);
            }
        }
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, bool allowNull = false)
        => TestStringEnumerableValueConverter(expected, m => m.ToString(), (expected, actual) => Assert.That(actual, Is.EqualTo(expected)), allowNull);
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, Func<TTo, string> toString, bool allowNull = false)
        => TestStringEnumerableValueConverter(expected, toString, (expected, actual) => Assert.That(actual, Is.EqualTo(expected)), allowNull);
        protected void TestStringEnumerableValueConverter<TTo>(TTo expected, Func<TTo, string> toString, Action<TTo, TTo> validator, bool allowNull = false)
        {
            IEnumerable<TTo> expectedArr = RandomList(expected, allowNull);
            string input = string.Join(',', expectedArr.Select(m => m == null ? (string)null : toString(m)));
            bool success = input.TryParseEnumerable(out IEnumerable<TTo> actual);
            Assert.That(success, Is.True);
            Assert.That(actual.Count(), Is.EqualTo(expectedArr.Count()));
            for (int i = 0; i < expectedArr.Count(); i++)
            {
                var exp = expectedArr.ElementAt(i);
                var acc = actual.ElementAt(i);
                validator(exp, acc);
            }
        }
    }
    public class UnitTestEnumerableConverter: UnitTestEnumerableConverterBase
    {
        [Test]
        public void ParseByte() => TestStringEnumerableValueConverter((byte)1);
        [Test]
        public void ParseSbyte() => TestStringEnumerableValueConverter((sbyte)1);
        [Test]
        public void ParseShort() => TestStringEnumerableValueConverter((short)5);
        [Test]
        public void ParseUshort() => TestStringEnumerableValueConverter((ushort)10);
        [Test]
        public void ParseInt() => TestStringEnumerableValueConverter((int)10);
        [Test]
        public void ParseUint() => TestStringEnumerableValueConverter((uint)10);
        [Test]
        public void ParseDouble() => TestStringEnumerableValueConverter((double)10.10);
        [Test]
        public void ParseDecimal() => TestStringEnumerableValueConverter((decimal)0.1234);
        [Test]
        public void ParseFloat() => TestStringEnumerableValueConverter((float)10.1234);
        [Test]
        public void ParseDateTime() => TestStringEnumerableValueConverter(DateTime.Parse("2024-03-02 21:36:33"));
        [Test]
        public void ParseDateTimeOffset() => TestStringEnumerableValueConverter(DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"));
        [Test]
        public void ParseTimeSpan() => TestStringEnumerableValueConverter(DateTime.Now.TimeOfDay);
        [Test]
        public void ParseGuid() => TestStringEnumerableValueConverter(Guid.NewGuid());
        [Test]
        public void ParseChar() => TestStringEnumerableValueConverter('@');
        [Test]
        public void ParseBool() => TestStringEnumerableValueConverter(true);
        [Test]
        public void ParseString() => TestStringEnumerableValueConverter("test value");
        [Test]
        public void ParseMailAddress() => TestStringEnumerableValueConverter(new MailAddress("some.test@email.com"));
        [Test]
        public void ParseUserName() => TestStringEnumerableValueConverter(new UserName("Test,R,User"), ';', m => m.ToString(), (expected, actual) => {
            Assert.That(actual.First, Is.EqualTo(expected.First));
            Assert.That(actual.Middle, Is.EqualTo(expected.Middle));
            Assert.That(actual.Last, Is.EqualTo(expected.Last));
        });
        [Test]
        public void ParseCustom()
        {
            (string first, string middle, string last) expected = ("Test", "R", "User");
            TestStringEnumerableValueConverter(expected,';', m => $"{m.first},{m.middle},{m.last}", (expected, actual) =>
            {
                Assert.That(actual.first, Is.EqualTo(expected.first));
                Assert.That(actual.middle, Is.EqualTo(expected.middle));
                Assert.That(actual.last, Is.EqualTo(expected.last));
            });
        }


        [Test]
        public void ParseByte_Nullable() => TestStringEnumerableValueConverter((byte?)1,true);
        [Test]
        public void ParseSbyte_Nullable() => TestStringEnumerableValueConverter((sbyte?)1, true);
        [Test]
        public void ParseShort_Nullable() => TestStringEnumerableValueConverter((short?)5, true);
        [Test]
        public void ParseUshort_Nullable() => TestStringEnumerableValueConverter((ushort?)10, true);
        [Test]
        public void ParseInt_Nullable() => TestStringEnumerableValueConverter((int?)10, true);
        [Test]
        public void ParseUint_Nullable() => TestStringEnumerableValueConverter((uint?)10, true);
        [Test]
        public void ParseDouble_Nullable() => TestStringEnumerableValueConverter((double?)10.10, true);
        [Test]
        public void ParseDecimal_Nullable() => TestStringEnumerableValueConverter((decimal?)0.1234, true);
        [Test]
        public void ParseFloat_Nullable() => TestStringEnumerableValueConverter((float?)10.1234, true);
        [Test]
        public void ParseDateTime_Nullable() => TestStringEnumerableValueConverter((DateTime?)DateTime.Parse("2024-03-02 21:36:33"), true);
        [Test]
        public void ParseDateTimeOffset_Nullable() => TestStringEnumerableValueConverter((DateTimeOffset?)DateTimeOffset.Parse("2024-03-02 21:36:33-05:00"), true);
        [Test]
        public void ParseTimeSpan_Nullable() => TestStringEnumerableValueConverter((TimeSpan?)DateTime.Now.TimeOfDay, true);
        [Test]
        public void ParseGuid_Nullable() => TestStringEnumerableValueConverter((Guid?)Guid.NewGuid(), true);
        [Test]
        public void ParseChar_Nullable() => TestStringEnumerableValueConverter((char?)'@', true);
        [Test]
        public void ParseBool_Nullable() => TestStringEnumerableValueConverter((bool?)true, true);
        [Test]
        public void ParseString_Nullable() => TestStringEnumerableValueConverter("test value",',', m=> m, (expected, actual) => {
            if (expected == null)
            {
                Assert.That(actual, Is.EqualTo(""));
            }
            else
            {
                Assert.That(actual, Is.EqualTo(expected));
            }
        },true);
        [Test]
        public void ParseMailAddress_Nullable() => TestStringEnumerableValueConverter(new MailAddress("some.test@email.com"), true);
        [Test]
        public void ParseUserName_Nullable() => TestStringEnumerableValueConverter(new UserName("Test,R,User"), ';', m => m.ToString(), (expected, actual) => {
            if (expected == null)
            {
                Assert.That(expected, Is.Null);
                Assert.That(actual, Is.Not.Null);
            }
            else
            {
                Assert.That(actual.First, Is.EqualTo(expected.First));
                Assert.That(actual.Middle, Is.EqualTo(expected.Middle));
                Assert.That(actual.Last, Is.EqualTo(expected.Last));
            }            
        }, true);
        [Test]
        public void ParseCustom_Nullable()
        {
            (string first, string middle, string last)? expected = ("Test", "R", "User");
            TestStringEnumerableValueConverter(expected, ';', m => m is (string first, string middle, string last) m2? $"{m2.first},{m2.middle},{m2.last}": null, (expected, actual) =>
            {
                if(expected is (string first, string middle, string last) exp && actual is (string f, string m, string l) act)
                {
                    Assert.That(act.first, Is.EqualTo(exp.first));
                    Assert.That(act.middle, Is.EqualTo(exp.middle));
                    Assert.That(act.last, Is.EqualTo(exp.last));
                }
                else
                {
                    Assert.That(expected, Is.Null);
                    Assert.That(actual, Is.Null);
                }
                
            }, true);
        }
    }
}
