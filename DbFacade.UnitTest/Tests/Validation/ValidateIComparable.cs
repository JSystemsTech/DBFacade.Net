using DbFacade.Extensions;

namespace DbFacade.UnitTest.Tests.Validation
{
    public abstract class ValidateIComparable<T> : UnitTestBase
        where T : IComparable
    {
        protected abstract T Value { get; }
        protected abstract T CompareToLess { get; }
        protected abstract T CompareToGreater { get; }
        public ValidateIComparable(ServiceFixture services) : base(services) { }

        [Fact]
        public void IsEqualTo()
        {
            Assert.True(Value.IsEqualTo(Value));
            Assert.False(CompareToLess.IsEqualTo(Value));
            Assert.False(CompareToGreater.IsEqualTo(Value));
        }
        [Fact]
        public void IsNotEqual()
        {
            Assert.False(Value.IsNotEqual(Value));
            Assert.True(CompareToLess.IsNotEqual(Value));
            Assert.True(CompareToGreater.IsNotEqual(Value));
        }
        [Fact]
        public void IsGreatorThan()
        {
            Assert.True(CompareToGreater.IsGreatorThan(Value));
            Assert.False(Value.IsGreatorThan(Value));
            Assert.False(CompareToLess.IsGreatorThan(Value));
        }
        [Fact]
        public void IsGreaterThanOrEqualTo()
        {
            Assert.True(Value.IsGreaterThanOrEqualTo(Value));
            Assert.True(CompareToGreater.IsGreaterThanOrEqualTo(Value));
            Assert.False(CompareToLess.IsGreaterThanOrEqualTo(Value));
        }
        [Fact]
        public void IsLessThan()
        {
            Assert.True(CompareToLess.IsLessThan(Value));
            Assert.False(CompareToGreater.IsLessThan(Value));
            Assert.False(Value.IsLessThan(Value));
        }
        [Fact]
        public void IsLessThanOrEqualTo()
        {
            Assert.True(Value.IsLessThanOrEqualTo(Value));
            Assert.True(CompareToLess.IsLessThanOrEqualTo(Value));
            Assert.False(CompareToGreater.IsLessThanOrEqualTo(Value));
        }
    }
}
