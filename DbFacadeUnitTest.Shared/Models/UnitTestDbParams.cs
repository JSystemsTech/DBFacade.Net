
using DbFacade.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Models
{
    interface IUnitTestDbParamsModel
    {
        bool IsValidModel { get; }
        bool StopAtStep1 { get; }
        bool StopAtStep2 { get; }
        bool StopAtStep3 { get; }
    }
    public class UnitTestDbParamsForManager : DbParamsModel, IUnitTestDbParamsModel
    {
        public bool IsValidModel { get; internal set; }
        public bool StopAtStep1 { get; internal set; }
        public bool StopAtStep2 { get; internal set; }
        public bool StopAtStep3 { get; internal set; }
    }
    public class UnitTestDbParams : DbParamsModel
    {
        public object Null = null;
        public object EmptyString = string.Empty;
        public string String = "my test string";
        public string FiveDigitString = "12345";
        public string TenDigitString = "1234567890";
        public string SSN = "234-23-1234";
        public string SSNNoDashes = "234231235";
        public string InvalidSSN = "ABC-23-1234";
        public string InvalidSSNNoDashes = "ABC231234";

        public string StringInvalidNum = "A_Bad_Number";
        public string StringInvalidDate = "A_Bad_Date";
        public string StringNum = "10";
        public string StringNumNull = null;
        public string Email = "mytestemail@gmail.com";
        public string EmailNull = null;
        public string InvalidEmail = "myinvalidemail";
        public string ForbiddenEmail = "myforbiddenemail@forbidden.net";
        public string FormatedString = "Formatted0123456789";

        public short Short = 10;
        public int Int = 10;
        public long Long = 10;
        public ushort UShort = 10;
        public uint UInt = 10;
        public ulong ULong = 10;
        public double Double = 10;
        public float Float = 10;
        public decimal Decimal = 10;
        public DateTime DateTime { get { return DateTime.Now; } }
        public DateTime Today { get { return DateTime.Now.Date; } }
        public DateTime? TodayOptional { get { return DateTime.Now.Date; } }
        public DateTime Yesterday { get { return DateTime.Now.Date.AddDays(-1); } }
        public DateTime? YesterdayOptional { get { return DateTime.Now.Date.AddDays(-1); } }
        public DateTime Tomorrow { get { return DateTime.Now.Date.AddDays(1); } }
        public DateTime? TomorrowOptional { get { return DateTime.Now.Date.AddDays(1); } }
        public DateTime? DateTimeNull = null;
        public string DateTimeString = "01/01/1979";
        public string DateTimeStringAlt = "30-12-1979";


        public short? ShortOptional = 10;
        public int? IntOptional = 10;
        public long? LongOptional = 10;
        public ushort? UShortOptional = 10;
        public uint? UIntOptional = 10;
        public ulong? ULongOptional = 10;
        public double? DoubleOptional = 10;
        public float? FloatOptional = 10;
        public decimal? DecimalOptional = 10;
        public DateTime? DateTimeOptional { get { return DateTime.Now; } }

        public short? ShortOptionalNull = null;
        public int? IntOptionalNull = null;
        public long? LongOptionalNull = null;
        public ushort? UShortOptionalNull = null;
        public uint? UIntOptionalNull = null;
        public ulong? ULongOptionalNull = null;
        public double? DoubleOptionalNull = null;
        public float? FloatOptionalNull = null;
        public decimal? DecimalOptionalNull = null;
        public DateTime? DateTimeOptionalNull = null;

        public string GetStringValue(string value = null) {
            return value;
        }
        public async Task<string> GetStringValueAsync(string value = null)
        {
            await Task.CompletedTask;
            return value;
        }
    }
}
