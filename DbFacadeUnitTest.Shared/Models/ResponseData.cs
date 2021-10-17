using System;

namespace DbFacadeUnitTests.Models
{
    class ResponseData
    {
        public int MyEnum { get; internal set; }
        public string MyString { get; internal set; }

        public string EnumerableData = "1,12,123,1234";
        public string EnumerableDataCustom = "1;12;123;1234";
        public string DateString = "01/01/1979";
        public DateTime Date = DateTime.Today;
        public string Email = "MyTestEmail@gmail.com";
        public string EmailList = "MyTestEmail@gmail.com,MyOtherEmail@hotmail.com,MyLastEmail@yahoo.com";

        public byte MyByte = 3;
        public int Integer = 100;
        public int? IntegerOptional = null;
        public Guid PublicKey = Guid.NewGuid();

        public int FlagInt = 1;
        public string Flag = "TRUE";
        public int FlagIntFalse = 0;
        public string FlagFalse = "FALSE";
        public char MyChar = 'c';
    }
    class ResponseDataAlt
    {
        public int MyEnumAlt { get; internal set; }
        public string MyStringAlt { get; internal set; }

        public string EnumerableDataAlt = "1,12,123,1234";
        public string EnumerableDataCustomAlt = "1;12;123;1234";
        public string DateStringAlt = "01/01/1979";
        public DateTime DateAlt = DateTime.Today;
        public string EmailAlt = "MyTestEmail@gmail.com";
        public string EmailListAlt = "MyTestEmail@gmail.com,MyOtherEmail@hotmail.com,MyLastEmail@yahoo.com";

        public byte MyByteAlt = 3;
        public int IntegerAlt = 100;
        public int? IntegerOptionalAlt = null;
        public Guid PublicKeyAlt = Guid.NewGuid();

        public int FlagIntAlt = 1;
        public string FlagAlt = "TRUE";
        public int FlagIntFalseAlt = 0;
        public string FlagFalseAlt = "FALSE";
    }
}
