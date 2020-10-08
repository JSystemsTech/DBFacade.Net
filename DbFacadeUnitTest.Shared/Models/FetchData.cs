using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace DbFacadeUnitTests.Models
{
    internal class FetchData : DbDataModel
    {
        [DbColumn("MyString")]
        public string MyString { get; internal set; }
        [DbColumn("Integer")]
        public int Integer { get; internal set; }
        [DbColumn("Integer")]
        public int? IntegerOptional { get; internal set; }
        [DbColumn("IntegerOptional")]
        public int? IntegerOptionalNull { get; internal set; }

        [DbColumn("PublicKey")]
        public Guid PublicKey { get; internal set; }
        [DbColumn("MyByte")]
        public byte MyByte { get; internal set; }
        [DbColumn("MyByte")]
        public int MyByteAsInt { get; internal set; }
    }
    internal class FetchDataWithBadDbColumn : DbDataModel
    {
        [DbColumn("BadMyString")]
        public string MyString { get; internal set; }
        [DbColumn("BadInteger")]
        public int Integer { get; internal set; }
        [DbColumn("BadInteger")]
        public int? IntegerOptional { get; internal set; }
        [DbColumn("BadIntegerOptional")]
        public int? IntegerOptionalNull { get; internal set; }

        [DbColumn("BadPublicKey")]
        public Guid PublicKey { get; internal set; }
    }
    internal class FetchDataWithNested : DbDataModel
    {
        public FetchDataEnumerable EnumerableData { get; internal set; }
        
        public FetchDataDates DateData { get; internal set; }
        
        public FetchDataEmail EmailData { get; internal set; }
        
        public FetchDataFlags FlagData { get; internal set; }

    }
    internal class FetchDataEnumerable : DbDataModel
    {


        [DbColumn("EnumerableData")]
        public IEnumerable<string> Data { get; internal set; }

        [DbColumn("EnumerableData")]
        public IEnumerable<short> ShortData { get; internal set; }

        [DbColumn("EnumerableData")]
        public IEnumerable<int> IntData { get; internal set; }

        [DbColumn("EnumerableData")]
        public IEnumerable<long> LongData { get; internal set; }

        [DbColumn("EnumerableData")]
        public IEnumerable<double> DoubleData { get; internal set; }

        [DbColumn("EnumerableData")]
        public IEnumerable<float> FloatData { get; internal set; }

        [DbColumn("EnumerableData")]
        public IEnumerable<decimal> DecimalData { get; internal set; }

        [DbColumn("EnumerableDataCustom", ';')]
        public IEnumerable<string> DataCustom { get; internal set; }

    }
    internal class FetchDataDates : DbDataModel
    {
        [DbDateStringColumn("DateString", "MM/dd/yyyy")]
        public DateTime DateTimeFromString { get; internal set; }

        [DbDateStringColumn("Date", "MM/dd/yyyy")]
        public string FormattedDate { get; internal set; }
    }
    internal class FetchDataEmail : DbDataModel
    {
        [DbColumn("Email")]
        public MailAddress Email { get; internal set; }
        [DbColumn("EmailList")]
        public IEnumerable<MailAddress> EmailList { get; internal set; }

    }
    internal class FetchDataFlags : DbDataModel
    {
        [DbFlagColumn("Flag", "TRUE")]
        public bool Flag { get; internal set; }

        [DbFlagColumn("FlagInt", 1)]
        public bool FlagInt { get; internal set; }

        [DbFlagColumn("FlagFalse", "TRUE")]
        public bool FlagFalse { get; internal set; }

        [DbFlagColumn("FlagIntFalse", 1)]
        public bool FlagIntFalse { get; internal set; }

    }
}
