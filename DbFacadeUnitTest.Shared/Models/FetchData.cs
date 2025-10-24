using DbFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace DbFacadeUnitTests.Models
{
    public enum FetchDataEnum
    {
        Fail = 0,
        Pass = 1,
    }
    internal class FetchData : IDbDataModel
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
        protected bool IsAltCall { get; set; }
        public FetchData(bool isAltCall) { IsAltCall = isAltCall; }
        public FetchData():this(false) {  }
        public virtual void Init(IDbDataCollection collection)
        {
            MyEnum = collection.GetColumn<FetchDataEnum>(IsAltCall ? "MyEnumAlt" : "MyEnum");
            MyString = collection.GetColumn<string>(IsAltCall ? "MyStringAlt" : "MyString");
            MyChar = collection.GetColumn<string>(IsAltCall ? "MyCharAlt" : "MyChar");
            Integer = collection.GetColumn<int>(IsAltCall ? "IntegerAlt" : "Integer");
            IntegerOptional = collection.GetColumn<int?>(IsAltCall ? "IntegerOptionalAlt" : "IntegerOptional");
            IntegerOptionalNull = collection.GetColumn<int?>(IsAltCall ? "IntegerOptionalAlt" : "IntegerOptional");
            PublicKey = collection.GetColumn<Guid>(IsAltCall ? "PublicKeyAlt" : "PublicKey");
            MyByte = collection.GetColumn<byte>(IsAltCall ? "MyByteAlt" : "MyByte");
            MyByteAsInt = collection.GetColumn<int>(IsAltCall ? "MyByteAlt" : "MyByte");
        }
    }

    internal class FetchDataAlt : FetchData
    {
        public FetchDataAlt() : base(true) { }
    }
    internal class FetchDataWithBadDbColumn : FetchData
    {
        public FetchDataWithBadDbColumn() : base() { }
        public override void Init(IDbDataCollection collection)
        {
            MyString = collection.GetColumn<string>("BadMyString");
            Integer = collection.GetColumn<int>("BadInteger");
            IntegerOptional = collection.GetColumn<int?>("BadIntegerOptional");
            IntegerOptionalNull = collection.GetColumn<int?>("BadIntegerOptional");
            PublicKey = collection.GetColumn<Guid>("BadPublicKey");
            MyByte = collection.GetColumn<byte>("BadMyByte");
            MyByteAsInt = collection.GetColumn<int>("BadMyByte");
            
        }
    }
    internal class FetchDataWithNested : IDbDataModel
    {
        public FetchDataWithNested() : base() { }
        public FetchDataEnumerable EnumerableData { get; internal set; }
        
        public FetchDataDates DateData { get; internal set; }
        
        public FetchDataEmail EmailData { get; internal set; }
        
        public FetchDataFlags FlagData { get; internal set; }
        public void Init(IDbDataCollection collection)
        {
            EnumerableData = collection.ToDbDataModel<FetchDataEnumerable>();
            DateData = collection.ToDbDataModel<FetchDataDates>();
            EmailData = collection.ToDbDataModel<FetchDataEmail>();
            FlagData = collection.ToDbDataModel<FetchDataFlags>();
        }
    }
    internal class FetchDataEnumerable : IDbDataModel
    {
        public IEnumerable<string> Data { get; internal set; }
        public IEnumerable<short> ShortData { get; internal set; }
        public IEnumerable<int> IntData { get; internal set; }
        public IEnumerable<long> LongData { get; internal set; }
        public IEnumerable<double> DoubleData { get; internal set; }
        public IEnumerable<float> FloatData { get; internal set; }
        public IEnumerable<decimal> DecimalData { get; internal set; }
        public IEnumerable<string> DataCustom { get; internal set; }

        public void Init(IDbDataCollection collection)
        {
            Data = collection.GetEnumerableColumn<string>("EnumerableData");
            ShortData = collection.GetEnumerableColumn<short>("EnumerableData");
            IntData = collection.GetEnumerableColumn<int>("EnumerableData");
            LongData = collection.GetEnumerableColumn<long>("EnumerableData");
            DoubleData = collection.GetEnumerableColumn<double>("EnumerableData");
            FloatData = collection.GetEnumerableColumn<float>("EnumerableData");
            DecimalData = collection.GetEnumerableColumn<decimal>("EnumerableData");
            DataCustom = collection.GetEnumerableColumn<string>("EnumerableDataCustom",";");
        }
    }
    internal class FetchDataDates : IDbDataModel
    {
        public DateTime? DateTimeFromString { get; internal set; }
        public string FormattedDate { get; internal set; }
        public void Init(IDbDataCollection collection)
        {
            DateTimeFromString = collection.GetDateTimeColumn("DateString", "MM/dd/yyyy");
            FormattedDate = collection.GetFormattedDateTimeStringColumn("Date", "MM/dd/yyyy");
        }
    }
    internal class FetchDataEmail : IDbDataModel
    {
        public MailAddress Email { get; internal set; }
        public IEnumerable<MailAddress> EmailList { get; internal set; }
        public void Init(IDbDataCollection collection)
        {
            Email = collection.GetColumn<MailAddress>("Email");
            EmailList = collection.GetEnumerableColumn<MailAddress>("EmailList");
        }
    }
    internal class FetchDataFlags : IDbDataModel
    {
        public bool Flag { get; internal set; }
        public bool FlagInt { get; internal set; }
        public bool FlagFalse { get; internal set; }
        public bool FlagIntFalse { get; internal set; }
        public void Init(IDbDataCollection collection)
        {
            Flag = collection.GetFlagColumn("Flag", "TRUE");
            FlagInt = collection.GetFlagColumn("FlagInt", 1);
            FlagFalse = collection.GetFlagColumn("FlagFalse", "TRUE");
            FlagIntFalse = collection.GetFlagColumn("FlagIntFalse", 1);
        }
    }
    internal class UserData : IDbDataModel
    {
        public string Name { get; internal set; }
        public int Id { get; internal set; }
        public void Init(IDbDataCollection collection)
        {
            Name = collection.GetColumn<string>("Name");
            Id = collection.GetColumn<int>("Id");
        }
    }
    internal class UserRole : IDbDataModel
    {
        public string Name { get; internal set; }
        public string Value { get; internal set; }
        public void Init(IDbDataCollection collection)
        {
            Name = collection.GetColumn<string>("Name");
            Value = collection.GetColumn<string>("Value");
        }
    }

    internal enum TestEnum
    {
        No,
        Yes
    }
    internal enum TestEnumChar
    {
        No = 48,
        Yes = 49
    }
    internal class UserName2: IDbDataModel
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
        public void Init(IDbDataCollection collection)
        {
            First = collection.GetColumn<string>("First");
            Middle = collection.GetColumn<string>("Middle");
            Last = collection.GetColumn<string>("Last");
        }
        public override string ToString()
        => $"{First},{Middle},{Last}";
        public static UserName2 Value1 = new UserName2("First1", "Middle1", "Last1");
        public static UserName2 Value2 = new UserName2("First2", "Middle2", "Last2");
        public static UserName2 Value3 = new UserName2("First3", "Middle3", "Last3");
        public static UserName2 Value4 = new UserName2("First4", "Middle4", "Last4");
        public static IEnumerable<UserName2> List = [Value1, Value2, Value3, Value4 ];

    }
    internal class TestClass2:IDbDataModel
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
        public void Init(IDbDataCollection collection)
        {
            String = collection.GetColumn<string>("str");
            Integer = collection.GetColumn<int>("integer");
            Guid = collection.GetColumn<Guid>("guid");
            TestEnum = collection.GetColumn<TestEnum>("testEnum");
            StrList = collection.GetEnumerableColumn<int>("strList");
        }
        

        private static readonly int[] numList = [1, 2, 3, 4, 5, 6, 7, 8, 9 ];
        public static readonly TestClass2 Value1 = new TestClass2("Value1", 10, Guid.NewGuid(), TestEnum.No, numList);
        public static readonly TestClass2 Value2 = new TestClass2("Value2", 20, Guid.NewGuid(), TestEnum.Yes, numList);
        public static readonly TestClass2 Value3 = new TestClass2("Value3", 30, Guid.NewGuid(), TestEnum.Yes, numList);
        public static readonly TestClass2 Value4 = new TestClass2("Value4", 40, Guid.NewGuid(), TestEnum.No, numList);
        public static readonly IEnumerable<TestClass2> List = [Value1, Value2, Value3, Value4];

    }
}
