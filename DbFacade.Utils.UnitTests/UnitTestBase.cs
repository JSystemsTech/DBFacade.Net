using System;
using System.Net.Mail;

namespace DbFacade.Utils.UnitTests
{
    public abstract class UnitTestBase
    {
        protected class UserName
        {
            public string First { get; set; }
            public string Middle { get; set; }
            public string Last { get; set; }
            public UserName() { }
            public UserName(string userName)
            {
                var data = userName.Split(',');
                First = data.Length > 0 ? data[0] : null;
                Middle = data.Length > 1 ? data[1] : null;
                Last = data.Length > 2 ? data[2] : null;
            }
            
            public UserName(string first, string middle, string last)
            {
                First = first;
                Middle = middle;
                Last = last;
            }
            public UserName(IDataCollection collection)
            {
                First = collection.TryGetValue("First", out string val1) ? val1: null;
                Middle = collection.TryGetValue("Middle", out string val2) ? val2 : null;
                Last = collection.TryGetValue("Last", out string val3) ? val3 : null;
            }
            public override string ToString()
            => $"{First},{Middle},{Last}";
            public static UserName Value1 = new UserName("First1", "Middle1", "Last1");
            public static UserName Value2 = new UserName("First2", "Middle2", "Last2");
            public static UserName Value3 = new UserName("First3", "Middle3", "Last3");
            public static UserName Value4 = new UserName("First4", "Middle4", "Last4");
            public static IEnumerable<UserName> List = new UserName[] { Value1, Value2, Value3, Value4 };
            
        }
        protected class TestClassForCollection
        {
            public string String { get; set; }
            public int Integer { get; set; }
            public double? Double { get; set; }
            public Guid Guid { get; set; }
            public TestEnum TestEnum { get; set; }
            public TestClassForCollection() { }
            public string StrList => "1,2,3,4,5,6,7,8,9";

            private TestClassForCollection(string str, int integer, Guid guid, TestEnum testEnum)
            {
                String = str;
                Integer = integer;
                Guid = guid;
                TestEnum = testEnum;
            }
            public TestClassForCollection(IDataCollection collection)
            {
                String = collection.TryGetValue("str", out string val1) ? val1 : null;
                Integer = collection.TryGetValue("integer", out int val2) ? val2 : default(int);
                Guid = collection.TryGetValue("guid", out Guid val3) ? val3 : default(Guid);
                TestEnum = collection.TryGetValue("testEnum", out TestEnum val4) ? val4 : default(TestEnum);
            }
            public static TestClassForCollection Value1 = new TestClassForCollection("Value1", 10, Guid.NewGuid(), TestEnum.No);
            public static TestClassForCollection Value2 = new TestClassForCollection("Value2", 20, Guid.NewGuid(), TestEnum.Yes);
            public static TestClassForCollection Value3 = new TestClassForCollection("Value3", 30, Guid.NewGuid(), TestEnum.Yes);
            public static TestClassForCollection Value4 = new TestClassForCollection("Value4", 40, Guid.NewGuid(), TestEnum.No);
            public static IEnumerable<TestClassForCollection> List = new TestClassForCollection[] { Value1, Value2, Value3, Value4 };
            
        }
        protected class TestClass2
        {
            public string String { get; set; }
            public int Integer { get; set; }
            public Guid Guid { get; set; }
            public TestEnum TestEnum { get; set; }
            public TestClass2() { }
            public IEnumerable<int> StrList { get; set; }

            private TestClass2(string str, int integer, Guid guid, TestEnum testEnum, IEnumerable<int> strList)
            {
                String = str;
                Integer = integer;
                Guid = guid;
                TestEnum = testEnum;
                StrList = strList;
            }
            public TestClass2(IDataCollection collection)
            {
                String = collection.TryGetValue("str", out string val1) ? val1 : null;
                Integer = collection.TryGetValue("integer", out int val2) ? val2 : default(int);
                Guid = collection.TryGetValue("guid", out Guid val3) ? val3 : default(Guid);
                TestEnum = collection.TryGetValue("testEnum", out TestEnum val4) ? val4 : default(TestEnum);
                StrList = collection.TryGetEnumerable("strList", out IEnumerable<int> val5) ? val5 : default(IEnumerable<int>);
            }
            private static int[] numList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            public static TestClass2 Value1 = new TestClass2("Value1", 10, Guid.NewGuid(), TestEnum.No, numList);
            public static TestClass2 Value2 = new TestClass2("Value2", 20, Guid.NewGuid(), TestEnum.Yes, numList);
            public static TestClass2 Value3 = new TestClass2("Value3", 30, Guid.NewGuid(), TestEnum.Yes, numList);
            public static TestClass2 Value4 = new TestClass2("Value4", 40, Guid.NewGuid(), TestEnum.No, numList);
            public static IEnumerable<TestClass2> List = new TestClass2[] { Value1, Value2, Value3, Value4 };
            
        }
        protected class TestClass3: IDataCollectionModel
        {
            public string String { get; set; }
            public int Integer { get; set; }
            public Guid Guid { get; set; }
            public TestEnum TestEnum { get; set; }
            public TestClass3() { }
            public IEnumerable<int> StrList { get; set; }

            private TestClass3(string str, int integer, Guid guid, TestEnum testEnum, IEnumerable<int> strList)
            {
                String = str;
                Integer = integer;
                Guid = guid;
                TestEnum = testEnum;
                StrList = strList;
            }
            
            private static int[] numList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            public static TestClass3 Value1 = new TestClass3("Value1", 10, Guid.NewGuid(), TestEnum.No, numList);
            public static TestClass3 Value2 = new TestClass3("Value2", 20, Guid.NewGuid(), TestEnum.Yes, numList);
            public static TestClass3 Value3 = new TestClass3("Value3", 30, Guid.NewGuid(), TestEnum.Yes, numList);
            public static TestClass3 Value4 = new TestClass3("Value4", 40, Guid.NewGuid(), TestEnum.No, numList);
            public static IEnumerable<TestClass3> List = new TestClass3[] { Value1, Value2, Value3, Value4 };
            private IDataCollection Collection { get; set; }
            public void InitDataCollection(IDataCollection collection)
            {
                Collection = collection;
            }
            public void InitCustom()
            {
                String = Collection.TryGetValue("str", out string val1) ? val1 : null;
                Integer = Collection.TryGetValue("integer", out int val2) ? val2 : default(int);
                Guid = Collection.TryGetValue("guid", out Guid val3) ? val3 : default(Guid);
                TestEnum = Collection.TryGetValue("testEnum", out TestEnum val4) ? val4 : default(TestEnum);
                StrList = Collection.TryGetEnumerable("strList", out IEnumerable<int> val5) ? val5 : default(IEnumerable<int>);
            }
        }
        protected class TestClass4 : IDataCollectionModel
        {
            public string String { get; set; }
            public int Integer { get; set; }
            public Guid Guid { get; set; }
            public TestEnum TestEnum { get; set; }
            public TestClass4() { }
            public IEnumerable<int> StrList { get; set; }
            public UserName UserName { get; set; }

            private TestClass4(string str, int integer, Guid guid, TestEnum testEnum, IEnumerable<int> strList, UserName userName)
            {
                String = str;
                Integer = integer;
                Guid = guid;
                TestEnum = testEnum;
                StrList = strList;
                UserName = userName;
            }

            private static int[] numList = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            public static TestClass4 Value1 = new TestClass4("Value1", 10, Guid.NewGuid(), TestEnum.No, numList, UserName.Value1);
            public static TestClass4 Value2 = new TestClass4("Value2", 20, Guid.NewGuid(), TestEnum.Yes, numList, UserName.Value2);
            public static TestClass4 Value3 = new TestClass4("Value3", 30, Guid.NewGuid(), TestEnum.Yes, numList, UserName.Value3);
            public static TestClass4 Value4 = new TestClass4("Value4", 40, Guid.NewGuid(), TestEnum.No, numList, UserName.Value4);
            public static IEnumerable<TestClass4> List = new TestClass4[] { Value1, Value2, Value3, Value4 };

            public void InitDataCollection(IDataCollection collection)
            {
                String = collection.TryGetValue("str", out string val1) ? val1 : null;
                Integer = collection.TryGetValue("integer", out int val2) ? val2 : default(int);
                Guid = collection.TryGetValue("guid", out Guid val3) ? val3 : default(Guid);
                TestEnum = collection.TryGetValue("testEnum", out TestEnum val4) ? val4 : default(TestEnum);
                StrList = collection.TryGetEnumerable("strList", out IEnumerable<int> val5) ? val5 : default(IEnumerable<int>);
                UserName = Utils.TryMakeInstance(out UserName un, collection) ? un : default(UserName);
            }
        }
        protected enum TestEnum
        {
            No,
            Yes
        }
        protected enum TestEnumChar
        {
            No = 48,
            Yes = 49
        }
        [SetUp]
        public virtual void Setup()
        {
            Func<string, (string first, string middle, string last)> resolver = str => {
                var data = str.Split(',');
                string First = data.Length > 0 ? data[0] : null;
                string Middle = data.Length > 1 ? data[1] : null;
                string Last = data.Length > 2 ? data[2] : null;
                return (First, Middle, Last);
            };
            bool addedConverter = resolver.TryRegisterConverter();
            Assert.That(addedConverter, Is.True);
            Utils.RegisterInstanceBuilder<TestClass3>(() => new TestClass3());
        } 
    }
}