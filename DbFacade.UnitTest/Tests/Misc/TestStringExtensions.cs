using DbFacade.DataLayer.Models.Parameters;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.Tests.Misc
{
    public class TestStringExtensions : UnitTestBase
    {
        public TestStringExtensions(ServiceFixture services) : base(services) { }

        [Fact]
        public void TestFormatCommandTextPart()
        {
            var expected = "[Test]";
            Assert.Equal(expected, "Test".FormatCommandTextPart());
            Assert.Equal(expected, "[Test".FormatCommandTextPart());
            Assert.Equal(expected, "Test]".FormatCommandTextPart());
            Assert.Equal(expected, "[Test]".FormatCommandTextPart());
        }

        [Fact]
        public void TestTypeName()
        {
            Type type = typeof(string);
            Assert.Equal("(null)", ((object)null).TypeName());
            Assert.Equal("String", type.TypeName());
            Assert.Equal("String", "test".TypeName());
        }
    }
}
