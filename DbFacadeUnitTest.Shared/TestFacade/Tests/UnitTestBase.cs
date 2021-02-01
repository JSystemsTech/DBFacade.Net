using DbFacadeUnitTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Tests
{
    [TestClass]
    public partial class UnitTestBase
    {
        internal static UnitTestDbParams Parameters = new UnitTestDbParams();
        
        protected static DateTime DateTimeNow { get { return DateTime.Now; } }
        protected static DateTime DateTime { get { return DateTime.Now; } }
        protected static DateTime Today { get { return DateTime.Now.Date; } }
        protected static DateTime Yesterday { get { return DateTime.Now.Date.AddDays(-1); } }
        protected static DateTime Tomorrow { get { return DateTime.Now.Date.AddDays(1); } }
        protected static DateTime DateTime1979 = DateTime.Parse("01/01/1979");
        protected static DateTime DateTime1979Alt = DateTime.Parse("12/30/1979");

        protected static string DateFormat = "dd/MM/yyyy";
        protected static string DateFormatAlt = "dd-MM-yyyy";

        protected static short Short9 = 9;
        protected static int Int9 = 9;
        protected static long Long9 = 9;
        protected static ushort UShort9 = 9;
        protected static uint UInt9 = 9;
        protected static ulong ULong9 = 9;
        protected static double Double9 = 9;
        protected static float Float9 = 9;
        protected static decimal Decimal9 = 9;

        protected static short Short10 = 10;
        protected static int Int10 = 10;
        protected static long Long10 = 10;
        protected static ushort UShort10 = 10;
        protected static uint UInt10 = 10;
        protected static ulong ULong10 = 10;
        protected static double Double10 = 10;
        protected static float Float10 = 10;
        protected static decimal Decimal10 = 10;

        protected static short Short11 = 11;
        protected static int Int11 = 11;
        protected static long Long11 = 11;
        protected static ushort UShort11 = 11;
        protected static uint UInt11 = 11;
        protected static ulong ULong11 = 11;
        protected static double Double11 = 11;
        protected static float Float11 = 11;
        protected static decimal Decimal11 = 11;

        protected void RunAsAsyc(Func<Task> asyncUnitTest)
        {
            Task task = Task.Run(async () => await asyncUnitTest());
            task.Wait();
        }
    }
}
