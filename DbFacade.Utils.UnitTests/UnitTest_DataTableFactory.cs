using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace DbFacade.Utils.UnitTests
{
    public class UnitTest_DataTableFactory : UnitTestBase
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Metrics.Clear();
        }
        private void AssertHasColumn(DataTable dt, string columnName)
        {
            Assert.IsTrue(dt.Columns.Contains(columnName), $"data table does not contain expected column '{columnName}'");
        }
        private void AssertHasValue(DataRow row, string columnName, object expected)
        {
            object actual = row[columnName] == DBNull.Value ? null: row[columnName];
            Assert.That(actual, Is.EqualTo(expected));
        }
        private void AssertTryGetValue<T>(DataRow row, string columnName, T expected)
        {
            bool success = row.TryGetValue(columnName, out T actual);
            Assert.IsTrue(success, $"unable to find or parse key '{columnName}'");
            Assert.That(actual, Is.EqualTo(expected));
        }
        private void AssertTryGetEnumerable<T>(DataRow row, string columnName, IEnumerable<T> expected)
        {
            bool success = row.TryGetEnumerable(columnName, out IEnumerable<T> actual);
            Assert.IsTrue(success, $"unable to find or parse key '{columnName}'");
            Assert.That(actual.Count(), Is.EqualTo(expected.Count()));
            for (int i = 0; i < actual.Count(); i++)
            {
                object expectedVal = expected.ElementAt(i);
                object actualVal = expected.ElementAt(i);
                Assert.That(actualVal, Is.EqualTo(expectedVal));
            }
        }
        private class ResponseData
        {
            public int MyEnum { get; internal set; }
            public string MyString { get; internal set; }

            public string EnumerableData = "1,12,123,1234";
            public string EnumerableDataCustom = "1;12;123;1234";
            public string DateString = "01/01/1979";
            public DateTime Date = DateTime.Today;
            public string Email = "MyTestEmail@gmail.com";
            public string EmailList = "MyTestEmail@gmail.com,MyOtherEmail@hotmail.com,MyLastEmail@yahoo.com";

            public byte MyByte = 3;
            public int Integer = 100;
            public int? IntegerOptional = null;
            public Guid PublicKey = Guid.NewGuid();

            public int FlagInt = 1;
            public string Flag = "TRUE";
            public int FlagIntFalse = 0;
            public string FlagFalse = "FALSE";
            public char MyChar = 'c';
        }
        [Test]
        public void TestTryGetDataTableComplex()
        {
            var list = new ResponseData[] { new ResponseData { MyString = "test string", MyEnum = 1 } };
            bool success = list.TryGetDataTable(out DataTable dt);
            Assert.IsTrue(success, $"expected collection parser to generate a Data Table");
            Assert.That(dt.Rows.Count, Is.EqualTo(1));

        }
        [Test]
        public void TestTryGetDataTable() {
            bool success = TestClassForCollection.List.TryGetDataTable(out DataTable dt);
            Assert.IsTrue(success, $"expected collection parser to generate a Data Table");
            Assert.That(dt.Rows.Count, Is.EqualTo(4));

            AssertHasColumn(dt, "String");
            AssertHasColumn(dt, "Integer");
            AssertHasColumn(dt, "Guid");
            AssertHasColumn(dt, "TestEnum");
            AssertHasColumn(dt, "StrList");
            AssertHasColumn(dt, "Double");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TestClassForCollection expectedModel = TestClassForCollection.List.ElementAt(i);
                DataRow row = dt.Rows[i];
                AssertHasValue(row, "String", expectedModel.String);
                AssertHasValue(row, "Integer", expectedModel.Integer);
                AssertHasValue(row, "Guid", expectedModel.Guid);
                AssertHasValue(row, "TestEnum", (int)expectedModel.TestEnum);
                AssertHasValue(row, "StrList", expectedModel.StrList);
                AssertHasValue(row, "Double", expectedModel.Double);

            }
        }
        [Test]
        public void TestTryGetValue()
        {
            TestClassForCollection.List.TryGetDataTable(out DataTable dt);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TestClassForCollection expectedModel = TestClassForCollection.List.ElementAt(i);
                DataRow row = dt.Rows[i];
                AssertTryGetValue(row, "String", expectedModel.String);
                AssertTryGetValue(row, "Integer", expectedModel.Integer);
                AssertTryGetValue(row, "Guid", expectedModel.Guid);
                AssertTryGetValue(row, "TestEnum", expectedModel.TestEnum);
                AssertTryGetEnumerable(row, "StrList", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                AssertTryGetValue(row, "Double", expectedModel.Double);

            }
        }
        
        [Test]
        public void TestParseCollection()
        {
            UserName.List.TryGetDataTable(out DataTable dt);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserName expectedModel = UserName.List.ElementAt(i);
                DataRow row = dt.Rows[i];
                UserName actualModel = Utils.MakeInstance<UserName>(row);
                Assert.IsTrue(actualModel != null, $"expected collection parser to generate a UserName object");
                Assert.That(actualModel.First, Is.EqualTo(expectedModel.First));
                Assert.That(actualModel.Middle, Is.EqualTo(expectedModel.Middle));
                Assert.That(actualModel.Last, Is.EqualTo(expectedModel.Last));

            }
        }
        [Test]
        public void TestParseCollection2()
        {
            var data = new
            {
                First = "First",
                Middle = (string)null,
                Last = "Last"
            };
            bool hasDt = Utils.TryGetDataTable(new[] { data }, out DataTable dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserName expectedModel = UserName.List.ElementAt(i);
                DataRow row = dt.Rows[i];
                UserName actualModel = Utils.MakeInstance<UserName>(row);
                Assert.IsTrue(actualModel != null, $"expected collection parser to generate a UserName object");
                Assert.That(actualModel.First, Is.EqualTo("First"));
                Assert.That(actualModel.Middle, Is.EqualTo(null));
                Assert.That(actualModel.Last, Is.EqualTo("Last"));

            }
        }
        [Test]
        public void TestParseFromDictionary()
        {
            IDictionary<string, object> data = new Dictionary<string, object>() {
                { "First", UserName.Value1.First },
                { "Middle", UserName.Value1.Middle },
                { "Last", UserName.Value1.Last }
            };
            UserName actualModel = Utils.MakeInstance<UserName>(data);
            Assert.IsTrue(actualModel != null, $"expected collection parser to generate a UserName object");

            Assert.That(actualModel.First, Is.EqualTo(UserName.Value1.First));
            Assert.That(actualModel.Middle, Is.EqualTo(UserName.Value1.Middle));
            Assert.That(actualModel.Last, Is.EqualTo(UserName.Value1.Last));
        }
        [Test]
        public void TestParseFromDictionary2()
        {
            IDictionary<string, object> data = new Dictionary<string, object>() {
                { "str", TestClass2.Value1.String },
                { "Integer", TestClass2.Value1.Integer },
                { "Guid", TestClass2.Value1.Guid },
                { "TestEnum", TestClass2.Value1.TestEnum },
                { "StrList", "1,2,3,4,5,6,7,8,9" }
            };
            bool success = Utils.TryMakeInstance(out TestClass2 actualModel, data);
            Assert.IsTrue(success, $"expected collection parser to generate a UserName object");

            Assert.That(actualModel.String, Is.EqualTo(TestClass2.Value1.String));
            Assert.That(actualModel.Integer, Is.EqualTo(TestClass2.Value1.Integer));
            Assert.That(actualModel.Guid, Is.EqualTo(TestClass2.Value1.Guid));
            Assert.That(actualModel.TestEnum, Is.EqualTo(TestClass2.Value1.TestEnum));
            Assert.That(actualModel.StrList, Is.EqualTo(TestClass2.Value1.StrList));
        }
        [Test]
        public void TestParseFromNameValueCollection()
        {
            NameValueCollection data = new NameValueCollection() {
                { "First", UserName.Value1.First },
                { "Middle", UserName.Value1.Middle },
                { "Last", UserName.Value1.Last }
            };
            UserName actualModel = Utils.MakeInstance<UserName>(data);
            Assert.IsTrue(actualModel != null, $"expected collection parser to generate a UserName object");

            Assert.That(actualModel.First, Is.EqualTo(UserName.Value1.First));
            Assert.That(actualModel.Middle, Is.EqualTo(UserName.Value1.Middle));
            Assert.That(actualModel.Last, Is.EqualTo(UserName.Value1.Last));
        }
        [Test]
        public void TestParseFromNameValueCollection2()
        {
            NameValueCollection data = new NameValueCollection() {
                { "str", TestClass2.Value1.String },
                { "Integer", TestClass2.Value1.Integer.ToString() },
                { "Guid", TestClass2.Value1.Guid.ToString() },
                { "TestEnum", TestClass2.Value1.TestEnum.ToString() },
                { "StrList", "1,2,3,4,5,6,7,8,9" }
            };
            bool success = Utils.TryMakeInstance(out TestClass2 actualModel, data);
            Assert.IsTrue(success, $"expected collection parser to generate a UserName object");

            Assert.That(actualModel.String, Is.EqualTo(TestClass2.Value1.String));
            Assert.That(actualModel.Integer, Is.EqualTo(TestClass2.Value1.Integer));
            Assert.That(actualModel.Guid, Is.EqualTo(TestClass2.Value1.Guid));
            Assert.That(actualModel.TestEnum, Is.EqualTo(TestClass2.Value1.TestEnum));
            Assert.That(actualModel.StrList, Is.EqualTo(TestClass2.Value1.StrList));
        }
        [Test]
        public void TestParseFromDataRow()
        {
            var data = new {
                str = TestClass2.Value1.String,
                Integer = TestClass2.Value1.Integer,
                Guid = TestClass2.Value1.Guid,
                TestEnum = TestClass2.Value1.TestEnum,
                StrList = "1,2,3,4,5,6,7,8,9"
            };
            bool hasDt = Utils.TryGetDataTable(new[] { data }, out DataTable dt);
            Assert.IsTrue(hasDt, $"expected new DataTable");

            bool success = Utils.TryMakeInstance(out TestClass2 actualModel, dt.Rows[0]);
            Assert.IsTrue(success, $"expected collection parser to generate a UserName object");

            Assert.That(actualModel.String, Is.EqualTo(TestClass2.Value1.String));
            Assert.That(actualModel.Integer, Is.EqualTo(TestClass2.Value1.Integer));
            Assert.That(actualModel.Guid, Is.EqualTo(TestClass2.Value1.Guid));
            Assert.That(actualModel.TestEnum, Is.EqualTo(TestClass2.Value1.TestEnum));
            Assert.That(actualModel.StrList, Is.EqualTo(TestClass2.Value1.StrList));
        }
        [Test]
        public void TestParseFromDataTable()
        {
            var data = new
            {
                str = TestClass2.Value1.String,
                Integer = TestClass2.Value1.Integer,
                Guid = TestClass2.Value1.Guid,
                TestEnum = TestClass2.Value1.TestEnum,
                StrList = "1,2,3,4,5,6,7,8,9"
            };
            var list = Enumerable.Range(1, 100).Select(m => data);
            bool hasDt = Utils.TryGetDataTable(list, out DataTable dt);
            Assert.IsTrue(hasDt, $"expected new DataTable");
            foreach (var dr in dt.Rows)
            {
                bool success = Utils.TryMakeInstance(out TestClass2 actualModel, dr);
                Assert.IsTrue(success, $"expected collection parser to generate a UserName object");

                Assert.That(actualModel.String, Is.EqualTo(TestClass2.Value1.String));
                Assert.That(actualModel.Integer, Is.EqualTo(TestClass2.Value1.Integer));
                Assert.That(actualModel.Guid, Is.EqualTo(TestClass2.Value1.Guid));
                Assert.That(actualModel.TestEnum, Is.EqualTo(TestClass2.Value1.TestEnum));
                Assert.That(actualModel.StrList, Is.EqualTo(TestClass2.Value1.StrList));
            }


        }
        [Test]
        public void TestParseNested()
        {
            var data = new
            {
                str = TestClass4.Value1.String,
                Integer = TestClass4.Value1.Integer,
                Guid = TestClass4.Value1.Guid,
                TestEnum = TestClass4.Value1.TestEnum,
                StrList = "1,2,3,4,5,6,7,8,9",
                First = UserName.Value1.First,
                Middle = UserName.Value1.Middle,
                Last = UserName.Value1.Last
            };
            bool hasDt = Utils.TryGetDataTable(new[] { data }, out DataTable dt);
            Assert.IsTrue(hasDt, $"expected new DataTable");

            bool success = Utils.TryMakeInstance(out TestClass4 actualModel, dt.Rows[0]);
            Assert.IsTrue(success, $"expected collection parser to generate a UserName object");

            Assert.That(actualModel.String, Is.EqualTo(TestClass4.Value1.String));
            Assert.That(actualModel.Integer, Is.EqualTo(TestClass4.Value1.Integer));
            Assert.That(actualModel.Guid, Is.EqualTo(TestClass4.Value1.Guid));
            Assert.That(actualModel.TestEnum, Is.EqualTo(TestClass4.Value1.TestEnum));
            Assert.That(actualModel.StrList, Is.EqualTo(TestClass4.Value1.StrList));
            Assert.That(actualModel.UserName, Is.Not.Null);
            Assert.That(actualModel.UserName.First, Is.EqualTo(TestClass4.Value1.UserName.First));
            Assert.That(actualModel.UserName.Middle, Is.EqualTo(TestClass4.Value1.UserName.Middle));
            Assert.That(actualModel.UserName.Last, Is.EqualTo(TestClass4.Value1.UserName.Last));
        }

        private void TestBenchmark(int count, double threshold, string text)
        {
            var data = new
            {
                str = TestClass3.Value1.String,
                Integer = TestClass3.Value1.Integer,
                Guid = TestClass3.Value1.Guid,
                TestEnum = TestClass3.Value1.TestEnum,
                StrList = "1,2,3,4,5,6,7,8,9"
            };
            var list = Enumerable.Range(1, count).Select(m => data);
            bool hasDt = Utils.TryGetDataTable(list, out DataTable dt);
            Assert.IsTrue(hasDt, $"expected new DataTable");
            Assert.That(dt.Rows.Count, Is.EqualTo(count));
            string metricsKey = "TryMakeInstance";
            var done = Metrics.Begin(metricsKey);
            IDataTableParser dtp = dt.ToDataTableParser();
            var rows = dt.Rows.Cast<DataRow>();

            IEnumerable<TestClass3> results = rows.Select(dr => Utils.MakeInstance<TestClass3>(dr, dtp));
            foreach (var m in results)
            {
                m.InitCustom();
            }
            done();
            Assert.That(results.Count(), Is.EqualTo(count), $"Expected count to be {count} but was {results.Count()}");
            double seconds = Metrics.MetricsMap[metricsKey];
            Assert.True(seconds < threshold, $"took {seconds} seconds");
            TestContext.WriteLine($"TestBenchmark: Parsing {text} data set took {seconds} seconds");
        }

        [Test]
        public void TestBenchmarkTiny() => TestBenchmark(BenchmarkConstants.TinyCount, BenchmarkConstants.Tiny, BenchmarkConstants.TinyText);
        [Test]
        public void TestBenchmarkVerySmall() => TestBenchmark(BenchmarkConstants.VerySmallCount, BenchmarkConstants.VerySmall, BenchmarkConstants.VerySmallText);
        [Test]
        public void TestBenchmarkSmall() => TestBenchmark(BenchmarkConstants.SmallCount, BenchmarkConstants.Small, BenchmarkConstants.SmallText);
        [Test]
        public void TestBenchmarkMedium() => TestBenchmark(BenchmarkConstants.MediumCount, BenchmarkConstants.Medium, BenchmarkConstants.MediumText);
        [Test]
        public void TestBenchmarkLarge() => TestBenchmark(BenchmarkConstants.LargeCount, BenchmarkConstants.Large, BenchmarkConstants.LargeText);
        [Test]
        public void TestBenchmarkVeryLarge() => TestBenchmark(BenchmarkConstants.VeryLargeCount, BenchmarkConstants.VeryLarge, BenchmarkConstants.VeryLargeText);
        [Test]
        public void TestBenchmarkMassive() => TestBenchmark(BenchmarkConstants.MassiveCount, BenchmarkConstants.Massive, BenchmarkConstants.MassiveText);


        private class BenchmarkConstants {
            public const int TinyCount = 10;
            public const int VerySmallCount = 50;
            public const int SmallCount = 100;
            public const int MediumCount = 1000;
            public const int LargeCount = 10000;
            public const int VeryLargeCount =20000;
            public const int MassiveCount = 50000;

            public const double Tiny = 0.001;
            public const double VerySmall = 0.001;
            public const double Small = 0.2;
            public const double Medium = 0.5;
            public const double Large = 1;
            public const double VeryLarge = 3;
            public const double Massive = 5;

            public const string TinyText = "Tiny";
            public const string VerySmallText = "Very Small";
            public const string SmallText = "Small";
            public const string MediumText = "Medium";
            public const string LargeText = "Large";
            public const string VeryLargeText = "Very Large";
            public const string MassiveText = "Massive";
        }
    }
    


}
