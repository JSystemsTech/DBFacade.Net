using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
    internal class UserName2 : IDbDataModel
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public UserName2() { }


        private UserName2(string first, string middle, string last)
        {
            First = first;
            Middle = middle;
            Last = last;
        }
        public void Init(IDataCollection collection)
        {
            First = collection.GetValue<string>("First");
            Middle = collection.GetValue<string>("Middle");
            Last = collection.GetValue<string>("Last");
        }
        public override string ToString()
        => $"{First},{Middle},{Last}";
        public static UserName2 Value1 = new UserName2("First1", "Middle1", "Last1");
        public static UserName2 Value2 = new UserName2("First2", "Middle2", "Last2");
        public static UserName2 Value3 = new UserName2("First3", "Middle3", "Last3");
        public static UserName2 Value4 = new UserName2("First4", "Middle4", "Last4");
        public static IEnumerable<UserName2> List = [Value1, Value2, Value3, Value4];

    }
}
