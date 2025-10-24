using System.Data;

namespace DbFacade.Utils.UnitTests
{
    public class UnitTest_Performance : UnitTestBase
    {
        [SetUp]
        public override void Setup() {
            Metrics.Clear();
        }
        private class BenchmarkConstants
        {
            public const int TinyCount = 10;
            public const int VerySmallCount = 50;
            public const int SmallCount = 100;
            public const int MediumCount = 1000;
            public const int LargeCount = 10000;
            public const int VeryLargeCount = 20000;
            public const int MassiveCount = 50000;

            public const double Tiny = 0.0001;
            public const double VerySmall = 0.001;
            public const double Small = 0.005;
            public const double Medium = 0.05;
            public const double Large = 0.8;
            public const double VeryLarge = 1;
            public const double Massive = 3;

            public const string TinyText = "Tiny";
            public const string VerySmallText = "Very Small";
            public const string SmallText = "Small";
            public const string MediumText = "Medium";
            public const string LargeText = "Large";
            public const string VeryLargeText = "Very Large";
            public const string MassiveText = "Massive";
        }
        
        private void TestStringParse(int count, double threshold, string text)
        {
            string data = "1,2,3,4,5,6,7,8,9";
            var list = Enumerable.Range(1, count).Select(m => data).ToArray().AsReadOnly();

            string splitKey = "TryParseEnumerable";
            var done = Metrics.Begin(splitKey);
            foreach (var str in list)
            {
                str.TryParseEnumerable(',', out IEnumerable<string> values);
            }
            
            done();
            Assert.That(Metrics.MetricsMap[splitKey] < threshold, Is.True, $"took {Metrics.MetricsMap[splitKey]} seconds");
            //TestContext.WriteLine($"{splitKey}: Parsing {text} data set took {Metrics.MetricsMap[splitKey]} seconds");
        }

        [Test]
        public void TestStringParseTiny() => TestStringParse(BenchmarkConstants.TinyCount, BenchmarkConstants.Tiny, BenchmarkConstants.TinyText);
        [Test]      
        public void TestStringParseVerySmall() => TestStringParse(BenchmarkConstants.VerySmallCount, BenchmarkConstants.VerySmall, BenchmarkConstants.VerySmallText);
        [Test]      
        public void TestStringParseSmall() => TestStringParse(BenchmarkConstants.SmallCount, BenchmarkConstants.Small, BenchmarkConstants.SmallText);
        [Test]      
        public void TestStringParseMedium() => TestStringParse(BenchmarkConstants.MediumCount, BenchmarkConstants.Medium, BenchmarkConstants.MediumText);
        [Test]      
        public void TestStringParseLarge() => TestStringParse(BenchmarkConstants.LargeCount, BenchmarkConstants.Large, BenchmarkConstants.LargeText);
        [Test]      
        public void TestStringParseVeryLarge() => TestStringParse(BenchmarkConstants.VeryLargeCount, BenchmarkConstants.VeryLarge, BenchmarkConstants.VeryLargeText);
        [Test]
        public void TestStringParseMassive() => TestStringParse(BenchmarkConstants.MassiveCount, BenchmarkConstants.Massive, BenchmarkConstants.MassiveText);

        private void TestIntParse(int count, double threshold, string text)
        {
            string data = "1,2,3,4,5,6,7,8,9";
            var list = Enumerable.Range(1, count).Select(m => data).ToArray().AsReadOnly();

            string splitKey = "TryParseEnumerable";
            var done = Metrics.Begin(splitKey);
            foreach (var str in list)
            {
                str.TryParseEnumerable(',', out IEnumerable<string> values);
            }
            done();
            Assert.That(Metrics.MetricsMap[splitKey] < threshold, Is.True, $"took {Metrics.MetricsMap[splitKey]} seconds");
            TestContext.WriteLine($"{splitKey}: Parsing {text} data set took {Metrics.MetricsMap[splitKey]} seconds");
        }

        [Test]
        public void TestIntParseTiny() => TestIntParse(BenchmarkConstants.TinyCount, BenchmarkConstants.Tiny, BenchmarkConstants.TinyText);
        [Test]
        public void TestIntParseVerySmall() => TestIntParse(BenchmarkConstants.VerySmallCount, BenchmarkConstants.VerySmall, BenchmarkConstants.VerySmallText);
        [Test]
        public void TestIntParseSmall() => TestIntParse(BenchmarkConstants.SmallCount, BenchmarkConstants.Small, BenchmarkConstants.SmallText);
        [Test]
        public void TestIntParseMedium() => TestIntParse(BenchmarkConstants.MediumCount, BenchmarkConstants.Medium, BenchmarkConstants.MediumText);
        [Test]
        public void TestIntParseLarge() => TestIntParse(BenchmarkConstants.LargeCount, BenchmarkConstants.Large, BenchmarkConstants.LargeText);
        [Test]
        public void TestIntParseVeryLarge() => TestIntParse(BenchmarkConstants.VeryLargeCount, BenchmarkConstants.VeryLarge, BenchmarkConstants.VeryLargeText);
        [Test]
        public void TestIntParseMassive() => TestIntParse(BenchmarkConstants.MassiveCount, BenchmarkConstants.Massive, BenchmarkConstants.MassiveText);
    }
}
