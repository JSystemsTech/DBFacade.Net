using DbFacade.DataLayer.Models;
using DbFacadeUnitTests.TestFacade;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DbFacadeUnitTests.Models
{
    public enum FetchDataEnum
    {
        Fail = 0,
        Pass = 1,
    }
    internal class FetchData : DbDataModel
    {
        public FetchDataEnum MyEnum { get; internal set; }
        public string MyString { get; internal set; }
        public string MyChar { get; internal set; }
        public int Integer { get; internal set; }
        public int? IntegerOptional { get; internal set; }
        public int? IntegerOptionalNull { get; internal set; }
        public Guid PublicKey { get; internal set; }
        public byte MyByte { get; internal set; }
        public int MyByteAsInt { get; internal set; }
        protected override void Init()
        {
            bool isAltCall = IsDbCommand(UnitTestConnection.TestFetchDataAlt);
            MyEnum = GetColumn<FetchDataEnum>(isAltCall ? "MyEnumAlt" : "MyEnum");
            MyString = GetColumn<string>(isAltCall ? "MyStringAlt" : "MyString");
            MyChar = GetColumn<string>(isAltCall ? "MyCharAlt" : "MyChar");
            Integer = GetColumn<int>(isAltCall ? "IntegerAlt" : "Integer");
            IntegerOptional = GetColumn<int?>(isAltCall ? "IntegerOptionalAlt" : "IntegerOptional");
            IntegerOptionalNull = GetColumn<int?>(isAltCall ? "IntegerOptionalAlt" : "IntegerOptional");
            PublicKey = GetColumn<Guid>(isAltCall ? "PublicKeyAlt" : "PublicKey");
            MyByte = GetColumn<byte>(isAltCall ? "MyByteAlt" : "MyByte");
            MyByteAsInt = GetColumn<int>(isAltCall ? "MyByteAlt" : "MyByte");
        }
        protected override async Task InitAsync()
        {
            bool isAltCall = await IsDbCommandAsync(UnitTestConnection.TestFetchDataAlt);
            MyEnum = await GetColumnAsync<FetchDataEnum>(isAltCall ? "MyEnumAlt" : "MyEnum");
            MyString = await GetColumnAsync<string>(isAltCall ? "MyStringAlt" : "MyString");
            Integer = await GetColumnAsync<int>(isAltCall ? "IntegerAlt" : "Integer");
            IntegerOptional = await GetColumnAsync<int?>(isAltCall ? "IntegerOptionalAlt" : "IntegerOptional");
            IntegerOptionalNull = await GetColumnAsync<int?>(isAltCall ? "IntegerOptionalAlt" : "IntegerOptional");
            PublicKey = await GetColumnAsync<Guid>(isAltCall ? "PublicKeyAlt" : "PublicKey");
            MyByte = await GetColumnAsync<byte>(isAltCall ? "MyByteAlt" : "MyByte");
            MyByteAsInt = await GetColumnAsync<int>(isAltCall ? "MyByteAlt" : "MyByte");
        }
    }
    internal class FetchDataWithBadDbColumn : FetchData
    {
        protected override void Init()
        {
            MyString = GetColumn<string>("BadMyString");
            Integer = GetColumn<int>("BadInteger");
            IntegerOptional = GetColumn<int?>("BadIntegerOptional");
            IntegerOptionalNull = GetColumn<int?>("BadIntegerOptional");
            PublicKey = GetColumn<Guid>("BadPublicKey");
            MyByte = GetColumn<byte>("BadMyByte");
            MyByteAsInt = GetColumn<int>("BadMyByte");
        }
        protected override async Task InitAsync()
        {
            MyString = await GetColumnAsync<string>("BadMyString");
            Integer = await GetColumnAsync<int>("BadInteger");
            IntegerOptional = await GetColumnAsync<int?>("BadIntegerOptional");
            IntegerOptionalNull = await GetColumnAsync<int?>("BadIntegerOptional");
            PublicKey = await GetColumnAsync<Guid>("BadPublicKey");
            MyByte = await GetColumnAsync<byte>("BadMyByte");
            MyByteAsInt = await GetColumnAsync<int>("BadMyByte");
        }
    }
    internal class FetchDataWithNested : DbDataModel
    {
        public FetchDataEnumerable EnumerableData { get; internal set; }
        
        public FetchDataDates DateData { get; internal set; }
        
        public FetchDataEmail EmailData { get; internal set; }
        
        public FetchDataFlags FlagData { get; internal set; }
        protected override void Init()
        {
            EnumerableData = CreateNestedModel<FetchDataEnumerable>();
            DateData = CreateNestedModel<FetchDataDates>();
            EmailData = CreateNestedModel<FetchDataEmail>();
            FlagData = CreateNestedModel<FetchDataFlags>();
        }
        protected override async Task InitAsync()
        {
            EnumerableData = await CreateNestedModelAsync<FetchDataEnumerable>();
            DateData = await CreateNestedModelAsync<FetchDataDates>();
            EmailData = await CreateNestedModelAsync<FetchDataEmail>();
            FlagData = await CreateNestedModelAsync<FetchDataFlags>();
        }
    }
    internal class FetchDataEnumerable : DbDataModel
    {
        public IEnumerable<string> Data { get; internal set; }
        public IEnumerable<short> ShortData { get; internal set; }
        public IEnumerable<int> IntData { get; internal set; }
        public IEnumerable<long> LongData { get; internal set; }
        public IEnumerable<double> DoubleData { get; internal set; }
        public IEnumerable<float> FloatData { get; internal set; }
        public IEnumerable<decimal> DecimalData { get; internal set; }
        public IEnumerable<string> DataCustom { get; internal set; }

        protected override void Init()
        {
            Data = GetEnumerableColumn<string>("EnumerableData");
            ShortData = GetEnumerableColumn<short>("EnumerableData");
            IntData = GetEnumerableColumn<int>("EnumerableData");
            LongData = GetEnumerableColumn<long>("EnumerableData");
            DoubleData = GetEnumerableColumn<double>("EnumerableData");
            FloatData = GetEnumerableColumn<float>("EnumerableData");
            DecimalData = GetEnumerableColumn<decimal>("EnumerableData");
            DataCustom = GetEnumerableColumn<string>("EnumerableDataCustom",";");
        }
        protected override async Task InitAsync()
        {
            Data = await GetEnumerableColumnAsync<string>("EnumerableData");
            ShortData = await GetEnumerableColumnAsync<short>("EnumerableData");
            IntData = await GetEnumerableColumnAsync<int>("EnumerableData");
            LongData = await GetEnumerableColumnAsync<long>("EnumerableData");
            DoubleData = await GetEnumerableColumnAsync<double>("EnumerableData");
            FloatData = await GetEnumerableColumnAsync<float>("EnumerableData");
            DecimalData = await GetEnumerableColumnAsync<decimal>("EnumerableData");
            DataCustom = await GetEnumerableColumnAsync<string>("EnumerableDataCustom", ";");
        }

    }
    internal class FetchDataDates : DbDataModel
    {
        public DateTime? DateTimeFromString { get; internal set; }
        public string FormattedDate { get; internal set; }
        protected override void Init()
        {
            DateTimeFromString = GetDateTimeColumn("DateString", "MM/dd/yyyy");
            FormattedDate = GetFormattedDateTimeStringColumn("Date", "MM/dd/yyyy");
        }
        protected override async Task InitAsync()
        {
            DateTimeFromString = await GetDateTimeColumnAsync("DateString", "MM/dd/yyyy");
            FormattedDate = await GetFormattedDateTimeStringColumnAsync("Date", "MM/dd/yyyy");
        }
    }
    internal class FetchDataEmail : DbDataModel
    {
        public MailAddress Email { get; internal set; }
        public IEnumerable<MailAddress> EmailList { get; internal set; }
        protected override void Init()
        {
            Email = GetColumn<MailAddress>("Email");
            EmailList = GetEnumerableColumn<MailAddress>("EmailList");
        }
        protected override async Task InitAsync()
        {
            Email = await GetColumnAsync<MailAddress>("Email");
            EmailList = await GetEnumerableColumnAsync<MailAddress>("EmailList");
        }
    }
    internal class FetchDataFlags : DbDataModel
    {
        public bool Flag { get; internal set; }
        public bool FlagInt { get; internal set; }
        public bool FlagFalse { get; internal set; }
        public bool FlagIntFalse { get; internal set; }
        protected override void Init()
        {
            Flag = GetFlagColumn("Flag", "TRUE");
            FlagInt = GetFlagColumn("FlagInt", 1);
            FlagFalse = GetFlagColumn("FlagFalse", "TRUE");
            FlagIntFalse = GetFlagColumn("FlagIntFalse", 1);
        }
        protected override async Task InitAsync()
        {
            Flag = await GetFlagColumnAsync("Flag", "TRUE");
            FlagInt = await GetFlagColumnAsync("FlagInt", 1);
            FlagFalse = await GetFlagColumnAsync("FlagFalse", "TRUE");
            FlagIntFalse = await GetFlagColumnAsync("FlagIntFalse", 1);
        }
    }
}
