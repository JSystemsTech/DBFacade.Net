using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DbFacade.Utils.UnitTests
{
    public abstract class UnitTests_InstanceFactoryBase: UnitTestBase
    {
        [SetUp]
        public override void Setup() { }

        protected void TestMakeInstance<T>()
            => TestMakeInstance(default(T));
        protected void TestMakeInstance<T>(T expected)
            => TestMakeInstance(expected, (expected, actual) => Assert.That(actual, Is.EqualTo(expected)));
        protected void TestMakeInstance<T>(T expected, Action<T, T> validator)
        {
            T actual = Utils.MakeInstance<T>();
            validator(expected, actual);
        }
        protected void TestMakeInstance<T>(T expected, params object[] parameters)
            => TestMakeInstance(expected, (expected, actual) => Assert.That(actual, Is.EqualTo(expected)), parameters);
        protected void TestMakeInstance<T>(T expected, Action<T, T> validator, params object[] parameters)
        {
            T actual = Utils.MakeInstance<T>(parameters);
            validator(expected, actual);
        }

    }
    public class UnitTests_InstanceFactory : UnitTests_InstanceFactoryBase
    {

        [Test]
        public void MakeByte() => TestMakeInstance<byte>();
        [Test]
        public void MakeSbyte() => TestMakeInstance<sbyte>();
        [Test]
        public void MakeShort() => TestMakeInstance<short>();
        [Test]
        public void MakeUshort() => TestMakeInstance<ushort>();
        [Test]
        public void MakeInt() => TestMakeInstance<int>();
        [Test]
        public void MakeUint() => TestMakeInstance<uint>();
        [Test]
        public void MakeLong() => TestMakeInstance<long>();
        [Test]
        public void MakeUlong() => TestMakeInstance<ulong>();
        [Test]
        public void MakeDouble() => TestMakeInstance<double>();
        [Test]
        public void MakeDecimal() => TestMakeInstance<decimal>();
        [Test]
        public void MakeFloat() => TestMakeInstance<float>();
        [Test]
        public void MakeDateTime() => TestMakeInstance<DateTime>();
        [Test]
        public void MakeDateTimeOffset() => TestMakeInstance<DateTimeOffset>();
        [Test]
        public void MakeTimeSpan() => TestMakeInstance<TimeSpan>();
        [Test]
        public void MakeGuid() => TestMakeInstance<Guid>();
        [Test]
        public void MakeChar() => TestMakeInstance<char>();
        [Test]
        public void MakeBool() => TestMakeInstance<bool>();
        [Test]
        public void MakeString() => TestMakeInstance<string>();
        [Test]
        public void MakeTestEnum() => TestMakeInstance<TestEnum>();
        [Test]
        public void MakeUserName() => TestMakeInstance(new UserName("Test", "R", "User"), (expected, actual) => {
            Assert.That(actual.First, Is.EqualTo(expected.First));
            Assert.That(actual.Middle, Is.EqualTo(expected.Middle));
            Assert.That(actual.Last, Is.EqualTo(expected.Last));
        }, "Test","R","User");
        [Test]
        public void MakeCustom_Basic()
        {
            (string first, string middle, string last) expected = ("Test", "R", "User");
            TestMakeInstance(expected, (exp, act) =>
            {
                Assert.That(act.first, Is.EqualTo(exp.first));
                Assert.That(act.middle, Is.EqualTo(exp.middle));
                Assert.That(act.last, Is.EqualTo(exp.last));
            }, "Test", "R", "User");

            (string test1, string test2, string test3) expected2 = ("1", "2", "3");
            TestMakeInstance(expected2, (exp, act) =>
            {
                Assert.That(act.test1, Is.EqualTo(exp.test1));
                Assert.That(act.test2, Is.EqualTo(exp.test2));
                Assert.That(act.test3, Is.EqualTo(exp.test3));
            }, "1", "2", "3");
        }
        [Test]
        public void MakeCustom_Complex()
        {
            DateTime testDate = DateTime.Now;
            (int test1, UserName user, DateTime createDate) expected3 = (1, new UserName("Test", "R", "User"), testDate);
            TestMakeInstance(expected3, (exp, act) =>
            {
                Assert.That(act.test1, Is.EqualTo(exp.test1));
                Assert.That(act.user.First, Is.EqualTo(exp.user.First));
                Assert.That(act.user.Middle, Is.EqualTo(exp.user.Middle));
                Assert.That(act.user.Last, Is.EqualTo(exp.user.Last));
                Assert.That(act.createDate, Is.EqualTo(exp.createDate));
            }, 1, new UserName("Test", "R", "User"), testDate);
        }
        [Test]
        public void MakeCustom_ReturnsDefault()
        {
            DateTime testDate = DateTime.Now;
            (int test1, UserName user, DateTime createDate) expected4 = (1, new UserName("Test", "R", "User"), testDate);
            TestMakeInstance(expected4, (exp, act) =>
            {
                Assert.That(act.test1, Is.Not.EqualTo(exp.test1));
                Assert.That(act.user, Is.Not.EqualTo(exp.user));
                Assert.That(act.createDate, Is.Not.EqualTo(exp.createDate));
            }, 1, new UserName("Test", "R", "User"), testDate, true);
        }

        [Test]
        public void MakeByteOptional() => TestMakeInstance<byte?>();
        [Test]
        public void MakeSbyteOptional() => TestMakeInstance<sbyte?>();
        [Test]
        public void MakeShortOptional() => TestMakeInstance<short?>();
        [Test]
        public void MakeUshortOptional() => TestMakeInstance<ushort?>();
        [Test]
        public void MakeIntOptional() => TestMakeInstance<int?>();
        [Test]
        public void MakeUintOptional() => TestMakeInstance<uint?>();
        [Test]
        public void MakeLongOptional() => TestMakeInstance<long?>();
        [Test]
        public void MakeUlongOptional() => TestMakeInstance<ulong?>();
        [Test]
        public void MakeDoubleOptional() => TestMakeInstance<double?>();
        [Test]
        public void MakeDecimalOptional() => TestMakeInstance<decimal?>();
        [Test]
        public void MakeFloatOptional() => TestMakeInstance<float?>();
        [Test]
        public void MakeDateTimeOptional() => TestMakeInstance<DateTime?>();
        [Test]
        public void MakeDateTimeOffsetOptional() => TestMakeInstance<DateTimeOffset?>();
        [Test]
        public void MakeTimeSpanOptional() => TestMakeInstance<TimeSpan?>();
        [Test]
        public void MakeGuidOptional() => TestMakeInstance<Guid?>();
        [Test]
        public void MakeCharOptional() => TestMakeInstance<char?>();
        [Test]
        public void MakeBoolOptional() => TestMakeInstance<bool?>();




    }
}
