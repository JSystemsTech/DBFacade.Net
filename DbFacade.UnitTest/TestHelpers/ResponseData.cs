namespace DbFacade.UnitTest.TestHelpers
{
    public class ResponseData
    {
        public int MyEnum { get; set; }
        public string MyString { get; set; }

        public string EnumerableData = "1,12,123,1234";
        public string EnumerableDataCustom = "1;12;123;1234";
        public string DateString = "01/01/1979";
        public DateTime Date = DateTime.Today;
        public DateTimeOffset DateOffset = DateTimeOffset.Now;
        public string Email = "MyTestEmail@gmail.com";
        public string EmailList = "MyTestEmail@gmail.com,MyOtherEmail@hotmail.com,MyLastEmail@yahoo.com";

        public byte MyByte = 3;
        public sbyte MySByte = 3;
        public short Short = 100;
        public ushort UShort = 100;
        public int Integer = 100;
        public uint UInteger = 100;
        public long Long = 100;
        public ulong ULong = 100;
        public double Double = 100;
        public float Float = 100.0f;
        public decimal Decimal = 100.00m;
        public bool Boolean = true;

        public int? IntegerOptional = null;
        public Guid PublicKey = Guid.NewGuid();

        public int FlagInt = 1;
        public string Flag = "TRUE";
        public int FlagIntFalse = 0;
        public string FlagFalse = "FALSE";
        public char MyChar = 'c';
    }
    internal class ResponseDataAlt
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
        public char MyCharAlt = 'c';
    }

    internal class ResponseDataMulti1
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
    }
    internal class ResponseDataMulti2
    {
        public string Name { get; internal set; }
        public string Value { get; internal set; }
    }
}
