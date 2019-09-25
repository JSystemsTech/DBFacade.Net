using System;

namespace DbFacadeUnitTests.Models
{
    class ResponseData
    {
        public string MyString { get; internal set; }

        public string EnumerableData = "1,12,123,1234";
        public string EnumerableDataCustom = "1;12;123;1234";
        public string DateString = "01/01/1979";
        public DateTime Date = DateTime.Today;
        public string Email = "MyTestEmail@gmail.com";
        public string EmailList = "MyTestEmail@gmail.com,MyOtherEmail@hotmail.com,MyLastEmail@yahoo.com";

        public int Integer = 100;
        public int? IntegerOptional = null;
        public Guid PublicKey = Guid.NewGuid();

        public int FlagInt = 1;
        public string Flag = "TRUE";
        public int FlagIntFalse = 0;
        public string FlagFalse = "FALSE";
    }
}
