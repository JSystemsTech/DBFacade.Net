using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
    internal class TestClass2 : IDbDataModel
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
        public void Init(IDataCollection collection)
        {
            String = collection.GetValue<string>("str");
            Integer = collection.GetValue<int>("integer");
            Guid = collection.GetValue<Guid>("guid");
            TestEnum = collection.GetValue<TestEnum>("testEnum");
            StrList = collection.ToEnumerable<int>("strList");
        }


        private static readonly int[] numList = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        public static readonly TestClass2 Value1 = new TestClass2("Value1", 10, Guid.NewGuid(), TestEnum.No, numList);
        public static readonly TestClass2 Value2 = new TestClass2("Value2", 20, Guid.NewGuid(), TestEnum.Yes, numList);
        public static readonly TestClass2 Value3 = new TestClass2("Value3", 30, Guid.NewGuid(), TestEnum.Yes, numList);
        public static readonly TestClass2 Value4 = new TestClass2("Value4", 40, Guid.NewGuid(), TestEnum.No, numList);
        public static readonly IEnumerable<TestClass2> List = [Value1, Value2, Value3, Value4];

    }
}
