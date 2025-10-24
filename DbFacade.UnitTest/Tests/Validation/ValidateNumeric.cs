using DbFacade.Extensions;

namespace DbFacade.UnitTest.Tests.Validation
{
    public class ValidateNumeric : UnitTestBase
    {

        private class Numerics
        {
            internal const byte Byte = 123;
            internal const sbyte SByte = 123;
            internal const short Short = 123;
            internal const ushort Ushort = 123;
            internal const int Int = 123;
            internal const uint Uint = 123;
            internal const long Long = 123;
            internal const ulong Ulong = 123;
            internal const float Float = 123.123f;
            internal const double Double = 1234567;
            internal const decimal Decimal = 123.123m;

            internal static byte? ByteNullable = 123;
            internal static sbyte? SByteNullable = 123;
            internal static short? ShortNullable = 123;
            internal static ushort? UshortNullable = 123;
            internal static int? IntNullable = 123;
            internal static uint? UintNullable = 123;
            internal static long? LongNullable = 123;
            internal static ulong? UlongNullable = 123;
            internal static float? FloatNullable = 123.123f;
            internal static double? DoubleNullable = 1234567;
            internal static decimal? DecimalNullable = 123.123m;

            internal static string IntStr = int.MaxValue.ToString();
            internal static string LongStr = long.MaxValue.ToString();
            internal static string FloatStr = Float.ToString();
            internal static string DoubleStr = double.MaxValue.ToString();
            internal static string DecimalStr = decimal.MaxValue.ToString();


            internal static byte? ByteNull = null;
            internal static sbyte? SByteNull = null;
            internal static short? ShortNull = null;
            internal static ushort? UshortNull = null;
            internal static int? IntNull = null;
            internal static uint? UintNull = null;
            internal static long? LongNull = null;
            internal static ulong? UlongNull = null;
            internal static float? FloatNull = null;
            internal static double? DoubleNull = null;
            internal static decimal? DecimalNull = null;
        }
        public ValidateNumeric(ServiceFixture services) : base(services) { }

        private void CheckNumeric(object value)
        {
            Assert.True(value.IsNumeric(), $"{value.GetType().Name} value of {value} is expected to be numeric");
        }
        private void CheckNotNumeric(object value)
        {
            string name = value == null ? "NULL": value.GetType().Name; 
            string valueStr = value == null ? "null" : value.ToString();
            Assert.False(value.IsNumeric(), $"{name} value of {valueStr} is incorrectly marked numeric");
        }
        [Fact]
        public void ValidateIsNumeric()
        {
            CheckNumeric(Numerics.Byte);
            CheckNumeric(Numerics.SByte);
            CheckNumeric(Numerics.Short);
            CheckNumeric(Numerics.Ushort);
            CheckNumeric(Numerics.Int);
            CheckNumeric(Numerics.Uint);
            CheckNumeric(Numerics.Long);
            CheckNumeric(Numerics.Ulong);
            CheckNumeric(Numerics.Float);
            CheckNumeric(Numerics.Double);
            CheckNumeric(Numerics.Decimal);

            CheckNumeric(Numerics.ByteNullable);
            CheckNumeric(Numerics.SByteNullable);
            CheckNumeric(Numerics.ShortNullable);
            CheckNumeric(Numerics.UshortNullable);
            CheckNumeric(Numerics.IntNullable);
            CheckNumeric(Numerics.UintNullable);
            CheckNumeric(Numerics.LongNullable);
            CheckNumeric(Numerics.UlongNullable);
            CheckNumeric(Numerics.FloatNullable);
            CheckNumeric(Numerics.DoubleNullable);
            CheckNumeric(Numerics.DecimalNullable);

            CheckNumeric(Numerics.IntStr);
            CheckNumeric(Numerics.LongStr);
            CheckNumeric(Numerics.FloatStr);
            CheckNumeric(Numerics.DoubleStr);
            CheckNumeric(Numerics.DecimalStr);
        }
        [Fact]
        public void ValidateIsNotNumeric()
        {
            CheckNotNumeric("ABC123");
            CheckNotNumeric("1234.345t");
            CheckNotNumeric(null);
            CheckNotNumeric(Numerics.ByteNull);
            CheckNotNumeric(Numerics.SByteNull);
            CheckNotNumeric(Numerics.ShortNull);
            CheckNotNumeric(Numerics.UshortNull);
            CheckNotNumeric(Numerics.IntNull);
            CheckNotNumeric(Numerics.UintNull);
            CheckNotNumeric(Numerics.LongNull);
            CheckNotNumeric(Numerics.UlongNull);
            CheckNotNumeric(Numerics.FloatNull);
            CheckNotNumeric(Numerics.DoubleNull);
            CheckNotNumeric(Numerics.DecimalNull);
            CheckNotNumeric(DateTime.Now);
        }
    }
}
