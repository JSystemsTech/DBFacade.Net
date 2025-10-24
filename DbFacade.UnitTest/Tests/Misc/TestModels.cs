using DbFacade.DataLayer.Models.Parameters;

namespace DbFacade.UnitTest.Tests.Misc
{
    public class TestModels : UnitTestBase
    {
        public TestModels(ServiceFixture services) : base(services) { }

        [Fact]
        public void TestAnsiString()
        {
            var value = "Test";
            AnsiString model = new AnsiString(value);
            Assert.Equal(value, model.Value);
            Assert.Equal(value, model.ToString());
        }
        [Fact]
        public void TestAnsiStringFixedLength()
        {
            var value = "Test";
            AnsiStringFixedLength model = new AnsiStringFixedLength(value);
            Assert.Equal(value, model.Value);
            Assert.Equal(value, model.ToString());
        }
        [Fact]
        public void TestStringFixedLength()
        {
            var value = "Test";
            StringFixedLength model = new StringFixedLength(value);
            Assert.Equal(value, model.Value);
            Assert.Equal(value, model.ToString());
        }
        [Fact]
        public void TestCurrency()
        {
            var value = 123.456m;
            Currency model = new Currency(value);
            Assert.Equal(value, model.Value);
            Assert.Equal("123.456", model.ToString());
        }
    }
}
