using DbFacade.UnitTest.DataLayer.Models.Data;

namespace DbFacade.UnitTest.Tests.Facade
{
    public partial class BenchmarkTests : UnitTestBase
    {
        private async Task TestBenchmarkAsync(int count, double threshold, string text)
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
            var tupple = await Services.DomainFacade.TestBenchmarkAsync();
            var response = tupple.Item1;
            var data = tupple.Item2;
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
        public async Task TestBenchmarkTinyAsync() => await TestBenchmarkAsync(BenchmarkConstants.TinyCount, BenchmarkConstants.Tiny, BenchmarkConstants.TinyText);
        [Fact]
        public async Task TestBenchmarkVerySmallAsync() => await TestBenchmarkAsync(BenchmarkConstants.VerySmallCount, BenchmarkConstants.VerySmall, BenchmarkConstants.VerySmallText);
        [Fact]
        public async Task TestBenchmarkSmallAsync() => await TestBenchmarkAsync(BenchmarkConstants.SmallCount, BenchmarkConstants.Small, BenchmarkConstants.SmallText);
        [Fact]
        public async Task TestBenchmarkMediumAsync() => await TestBenchmarkAsync(BenchmarkConstants.MediumCount, BenchmarkConstants.Medium, BenchmarkConstants.MediumText);
        [Fact]
        public async Task TestBenchmarkLargeAsync() => await TestBenchmarkAsync(BenchmarkConstants.LargeCount, BenchmarkConstants.Large, BenchmarkConstants.LargeText);
        [Fact]
        public async Task TestBenchmarkVeryLargeAsync() => await TestBenchmarkAsync(BenchmarkConstants.VeryLargeCount, BenchmarkConstants.VeryLarge, BenchmarkConstants.VeryLargeText);
        [Fact]
        public async Task TestBenchmarkMassiveAsync() => await TestBenchmarkAsync(BenchmarkConstants.MassiveCount, BenchmarkConstants.Massive, BenchmarkConstants.MassiveText);
    }
}
