using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
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
}
