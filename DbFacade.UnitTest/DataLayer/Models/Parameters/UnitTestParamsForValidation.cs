using System.Net.Mail;

namespace DbFacade.UnitTest.DataLayer.Models.Parameters
{
    internal class UnitTestParamsForValidation
    {
        internal DateTime DateTimeNow { get { return DateTime.Now; } }
        internal DateTime DateTime { get { return DateTime.Now; } }
        internal DateTime Today { get { return DateTime.Now.Date; } }
        internal DateTime Yesterday { get { return DateTime.Now.Date.AddDays(-1); } }
        internal DateTime Tomorrow { get { return DateTime.Now.Date.AddDays(1); } }
        
        internal TimeSpan Noon { get { return DateTime.Parse("01/01/1979 12:00:00").TimeOfDay; } }
        internal TimeSpan Morning { get { return DateTime.Parse("01/01/1979 9:00:00").TimeOfDay; } }
        internal TimeSpan Afternoon { get { return DateTime.Parse("01/01/1979 15:00:00").TimeOfDay; } }
        
        internal DateTime DateTime1979 = DateTime.Parse("01/01/1979");
        internal DateTime DateTime1979Alt = DateTime.Parse("12/30/1979");
        internal DateTime? DateTimeNullable = DateTime.Parse("01/01/1979");

        internal string DateFormat = "dd/MM/yyyy";
        internal string DateFormatAlt = "dd-MM-yyyy";

        internal string DateStr = "01/01/1979";
        internal string DateStrAlt = "01-01-1979";

        internal short Short9 = 9;
        internal int Int9 = 9;
        internal long Long9 = 9;
        internal ushort UShort9 = 9;
        internal uint UInt9 = 9;
        internal ulong ULong9 = 9;
        internal double Double9 = 9;
        internal float Float9 = 9;
        internal decimal Decimal9 = 9;
        
        internal short Short10 = 10;
        internal int Int10 = 10;
        internal long Long10 = 10;
        internal ushort UShort10 = 10;
        internal uint UInt10 = 10;
        internal ulong ULong10 = 10;
        internal double Double10 = 10;
        internal float Float10 = 10;
        internal decimal Decimal10 = 10;
        
        internal short Short11 = 11;
        internal int Int11 = 11;
        internal long Long11 = 11;
        internal ushort UShort11 = 11;
        internal uint UInt11 = 11;
        internal ulong ULong11 = 11;
        internal double Double11 = 11;
        internal float Float11 = 11;
        internal decimal Decimal11 = 11;


        internal string StringLen10 = "1234567890";
        internal string StringNumeric = "1234567890";
        internal string StringNull = null;
        internal string StringEmpty = "";
        internal string StringWhiteSpace = " ";
        internal string StringAlphaNumeric = "abc123ABC";

        internal string Email = "my.name@testdomain.com";
        internal MailAddress MailAddress = new MailAddress("my.name@testdomain.com");
    }
}
