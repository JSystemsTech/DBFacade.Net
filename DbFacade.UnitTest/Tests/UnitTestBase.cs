using DbFacade.UnitTest.DataLayer.Models.Parameters;

namespace DbFacade.UnitTest.Tests
{
    public class UnitTestBase
    {
        protected readonly ServiceFixture Services;
        protected CancellationToken TestCancellationToken => TestContext.Current.CancellationToken;
        public UnitTestBase(ServiceFixture services)
        {
            Services = services;
        }
        #region Static Properties
        internal static UnitTestDbParams Parameters = new UnitTestDbParams();

        protected static DateTime DateTimeNow { get { return DateTime.Now; } }
        protected static DateTime DateTime { get { return DateTime.Now; } }
        protected static DateTime Today { get { return DateTime.Now.Date; } }
        protected static DateTime Yesterday { get { return DateTime.Now.Date.AddDays(-1); } }
        protected static DateTime Tomorrow { get { return DateTime.Now.Date.AddDays(1); } }

        protected static TimeSpan Noon { get { return DateTime.Parse("01/01/1979 12:00:00").TimeOfDay; } }
        protected static TimeSpan Morning { get { return DateTime.Parse("01/01/1979 9:00:00").TimeOfDay; } }
        protected static TimeSpan Afternoon { get { return DateTime.Parse("01/01/1979 15:00:00").TimeOfDay; } }

        protected static DateTime DateTime1979 = DateTime.Parse("01/01/1979");
        protected static DateTime DateTime1979Alt = DateTime.Parse("12/30/1979");

        protected const string DateFormat = "dd/MM/yyyy";
        protected const string DateFormatAlt = "dd-MM-yyyy";

        protected const short Short9 = 9;
        protected const int Int9 = 9;
        protected const long Long9 = 9;
        protected const ushort UShort9 = 9;
        protected const uint UInt9 = 9;
        protected const ulong ULong9 = 9;
        protected const double Double9 = 9;
        protected const float Float9 = 9;
        protected const decimal Decimal9 = 9;

        protected const short Short10 = 10;
        protected const int Int10 = 10;
        protected const long Long10 = 10;
        protected const ushort UShort10 = 10;
        protected const uint UInt10 = 10;
        protected const ulong ULong10 = 10;
        protected const double Double10 = 10;
        protected const float Float10 = 10;
        protected const decimal Decimal10 = 10;

        protected const short Short11 = 11;
        protected const int Int11 = 11;
        protected const long Long11 = 11;
        protected const ushort UShort11 = 11;
        protected const uint UInt11 = 11;
        protected const ulong ULong11 = 11;
        protected const double Double11 = 11;
        protected const float Float11 = 11;
        protected const decimal Decimal11 = 11;


        #endregion

    }
}
