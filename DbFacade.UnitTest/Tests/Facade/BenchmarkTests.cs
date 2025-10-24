using DbFacade.UnitTest.DataLayer.Models.Data;

namespace DbFacade.UnitTest.Tests.Facade
{
    public partial class BenchmarkTests : UnitTestBase
    {
        public BenchmarkTests(ServiceFixture services) : base(services) { }

        private class BenchmarkConstants
        {
            public const int TinyCount = 10;
            public const int VerySmallCount = 50;
            public const int SmallCount = 100;
            public const int MediumCount = 1000;
            public const int LargeCount = 10000;
            public const int VeryLargeCount = 20000;
            public const int MassiveCount = 500000;

            public const double Tiny = 0.005;
            public const double VerySmall = 0.02;
            public const double Small = 0.05;
            public const double Medium = 0.1;
            public const double Large = 0.5;
            public const double VeryLarge = 1.5;
            public const double Massive = 5;

            public const string TinyText = "Tiny";
            public const string VerySmallText = "Very Small";
            public const string SmallText = "Small";
            public const string MediumText = "Medium";
            public const string LargeText = "Large";
            public const string VeryLargeText = "Very Large";
            public const string MassiveText = "Massive";
        }

        private void TestBenchmark(int count, double threshold, string text)
        {
            var testModel = new
            {
                str = TestClass2.Value1.String,
                integer = TestClass2.Value1.Integer,
                guid = TestClass2.Value1.Guid,
                testEnum = TestClass2.Value1.TestEnum,
                strList = string.Join(",", TestClass2.Value1.StrList.Select(m => m.ToString()))
            };
            var list = Enumerable.Range(1, count).Select(m => testModel);
            Services.Endpoints.TestBenchmark.EnableMockMode(b =>
            {
                b.Add(list);
                b.ReturnValue = 0;
            });

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            var response = Services.DomainFacade.TestBenchmark(out IEnumerable<TestClass2> data);
            stopwatch.Stop();

            double seconds = stopwatch.Elapsed.TotalSeconds;
            Assert.False(response.HasError);
            bool isThresholdValid = seconds < threshold;
            string thresholdError = !isThresholdValid ? $"TestBenchmark: Parsing {text} data set took {seconds} seconds but was expected to be less than ${threshold} seconds" : "";
            Assert.True(isThresholdValid, thresholdError);
            string errorInfo = response.HasError ? $"Response Error: {response.ErrorInfo.ErrorMessage} Details: {response.ErrorInfo.ErrorDetails}" : "";
            Assert.False(response.HasError, errorInfo);
            Assert.Equal(count, data.Count());
        }
        [Fact]
        public void TestBenchmarkTiny() => TestBenchmark(BenchmarkConstants.TinyCount, BenchmarkConstants.Tiny, BenchmarkConstants.TinyText);
        [Fact]
        public void TestBenchmarkVerySmall() => TestBenchmark(BenchmarkConstants.VerySmallCount, BenchmarkConstants.VerySmall, BenchmarkConstants.VerySmallText);
        [Fact]
        public void TestBenchmarkSmall() => TestBenchmark(BenchmarkConstants.SmallCount, BenchmarkConstants.Small, BenchmarkConstants.SmallText);
        [Fact]
        public void TestBenchmarkMedium() => TestBenchmark(BenchmarkConstants.MediumCount, BenchmarkConstants.Medium, BenchmarkConstants.MediumText);
        [Fact]
        public void TestBenchmarkLarge() => TestBenchmark(BenchmarkConstants.LargeCount, BenchmarkConstants.Large, BenchmarkConstants.LargeText);
        [Fact]
        public void TestBenchmarkVeryLarge() => TestBenchmark(BenchmarkConstants.VeryLargeCount, BenchmarkConstants.VeryLarge, BenchmarkConstants.VeryLargeText);
        [Fact]
        public void TestBenchmarkMassive() => TestBenchmark(BenchmarkConstants.MassiveCount, BenchmarkConstants.Massive, BenchmarkConstants.MassiveText);
    }
}
