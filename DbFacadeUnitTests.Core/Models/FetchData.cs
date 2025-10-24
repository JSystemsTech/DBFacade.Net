using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace DbFacadeUnitTests.Core.Models
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
        public virtual void Init(IDataCollection collection)
        {
            MyEnum = collection.GetValue<FetchDataEnum>("MyEnum");
            MyString = collection.GetValue<string>("MyString");
            MyChar = collection.GetValue<string>("MyChar");
            Integer = collection.GetValue<int>("Integer");
            IntegerOptional = collection.GetValue<int?>("IntegerOptional");
            IntegerOptionalNull = collection.GetValue<int?>("IntegerOptional");
            PublicKey = collection.GetValue<Guid>("PublicKey");
            MyByte = collection.GetValue<byte>("MyByte");
            MyByteAsInt = collection.GetValue<int>("MyByte");
        }
    }

    internal class FetchDataAlt : IDbDataModel
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
        public void Init(IDataCollection collection)
        {
            MyEnum = collection.GetValue<FetchDataEnum>("MyEnumAlt");
            MyString = collection.GetValue<string>("MyStringAlt");
            MyChar = collection.GetValue<string>("MyCharAlt");
            Integer = collection.GetValue<int>("IntegerAlt");
            IntegerOptional = collection.GetValue<int?>("IntegerOptionalAlt");
            IntegerOptionalNull = collection.GetValue<int?>("IntegerOptionalAlt");
            PublicKey = collection.GetValue<Guid>("PublicKeyAlt");
            MyByte = collection.GetValue<byte>("MyByteAlt");
            MyByteAsInt = collection.GetValue<int>("MyByteAlt");
        }
    }
    internal class FetchDataWithBadDbColumn : FetchData
    {
        public FetchDataWithBadDbColumn() : base() { }
        public override void Init(IDataCollection collection)
        {
            MyString = collection.GetValue<string>("BadMyString");
            Integer = collection.GetValue<int>("BadInteger");
            IntegerOptional = collection.GetValue<int?>("BadIntegerOptional");
            IntegerOptionalNull = collection.GetValue<int?>("BadIntegerOptional");
            PublicKey = collection.GetValue<Guid>("BadPublicKey");
            MyByte = collection.GetValue<byte>("BadMyByte");
            MyByteAsInt = collection.GetValue<int>("BadMyByte");
            
        }
    }
    internal class FetchDataWithNested : IDbDataModel
    {
        public FetchDataWithNested() : base() { }
        public FetchDataEnumerable EnumerableData { get; internal set; }
        
        public FetchDataDates DateData { get; internal set; }
        
        public FetchDataEmail EmailData { get; internal set; }
        
        public FetchDataFlags FlagData { get; internal set; }
        public void Init(IDataCollection collection)
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

        public void Init(IDataCollection collection)
        {
            Data = collection.ToEnumerable<string>("EnumerableData");
            ShortData = collection.ToEnumerable<short>("EnumerableData");
            IntData = collection.ToEnumerable<int>("EnumerableData");
            LongData = collection.ToEnumerable<long>("EnumerableData");
            DoubleData = collection.ToEnumerable<double>("EnumerableData");
            FloatData = collection.ToEnumerable<float>("EnumerableData");
            DecimalData = collection.ToEnumerable<decimal>("EnumerableData");
            DataCustom = collection.ToEnumerable<string>("EnumerableDataCustom",separator: ';');
        }
    }
    internal class FetchDataDates : IDbDataModel
    {
        public DateTime? DateTimeFromString { get; internal set; }
        public string FormattedDate { get; internal set; }
        public void Init(IDataCollection collection)
        {
            DateTimeFromString = collection.ToDateTime("DateString", "MM/dd/yyyy");
            FormattedDate = collection.ToDateTimeString("Date", "MM/dd/yyyy");
        }
    }
    internal class FetchDataEmail : IDbDataModel
    {
        public MailAddress Email { get; internal set; }
        public IEnumerable<MailAddress> EmailList { get; internal set; }
        public void Init(IDataCollection collection)
        {
            Email = new MailAddress(collection.GetValue<string>("Email"));
            EmailList = collection.ToEnumerable<string>("EmailList").Select(m=> new MailAddress(m));
        }
    }
    internal class FetchDataFlags : IDbDataModel
    {
        public bool Flag { get; internal set; }
        public bool FlagInt { get; internal set; }
        public bool FlagFalse { get; internal set; }
        public bool FlagIntFalse { get; internal set; }
        public void Init(IDataCollection collection)
        {
            Flag = collection.ToBoolean("Flag", "TRUE");
            FlagInt = collection.ToBoolean("FlagInt", 1);
            FlagFalse = collection.ToBoolean("FlagFalse", "TRUE");
            FlagIntFalse = collection.ToBoolean("FlagIntFalse", 1);
        }
    }
    internal class UserData : IDbDataModel
    {
        public string Name { get; internal set; }
        public int Id { get; internal set; }
        public void Init(IDataCollection collection)
        {
            Name = collection.GetValue<string>("Name");
            Id = collection.GetValue<int>("Id");
        }
    }
    internal class UserRole : IDbDataModel
    {
        public string Name { get; internal set; }
        public string Value { get; internal set; }
        public void Init(IDataCollection collection)
        {
            Name = collection.GetValue<string>("Name");
            Value = collection.GetValue<string>("Value");
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
        public void Init(IDataCollection collection)
        {
            String = collection.GetValue<string>("str");
            Integer = collection.GetValue<int>("integer");
            Guid = collection.GetValue<Guid>("guid");
            TestEnum = collection.GetValue<TestEnum>("testEnum");
            StrList = collection.ToEnumerable<int>("strList");
        }
        

        private static readonly int[] numList = [1, 2, 3, 4, 5, 6, 7, 8, 9 ];
        public static readonly TestClass2 Value1 = new TestClass2("Value1", 10, Guid.NewGuid(), TestEnum.No, numList);
        public static readonly TestClass2 Value2 = new TestClass2("Value2", 20, Guid.NewGuid(), TestEnum.Yes, numList);
        public static readonly TestClass2 Value3 = new TestClass2("Value3", 30, Guid.NewGuid(), TestEnum.Yes, numList);
        public static readonly TestClass2 Value4 = new TestClass2("Value4", 40, Guid.NewGuid(), TestEnum.No, numList);
        public static readonly IEnumerable<TestClass2> List = [Value1, Value2, Value3, Value4];

    }
}
