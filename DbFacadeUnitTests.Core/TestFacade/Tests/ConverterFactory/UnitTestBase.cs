using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DbFacadeUnitTests.Core.TestFacade.Tests.ConverterFactory
{
    [TestClass]
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
                First = collection.GetValue<string>("First");
                Middle = collection.GetValue<string>("Middle");
                Last = collection.GetValue<string>("Last");
            }
            public override string ToString()
            => $"{First},{Middle},{Last}";
            public static UserName Value1 = new UserName("First1", "Middle1", "Last1");
            public static UserName Value2 = new UserName("First2", "Middle2", "Last2");
            public static UserName Value3 = new UserName("First3", "Middle3", "Last3");
            public static UserName Value4 = new UserName("First4", "Middle4", "Last4");
            public static IEnumerable<UserName> List = new UserName[] { Value1, Value2, Value3, Value4 };
            
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
    }
}