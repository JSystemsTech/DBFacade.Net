using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
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
        public object MyIndexRefValue { get; internal set; }
        public object OutOfRangeLowIndexRefValue { get; internal set; }
        public object OutOfRangeHighIndexRefValue { get; internal set; }
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
            MyIndexRefValue = collection[2];
            OutOfRangeLowIndexRefValue = collection[-1];
            OutOfRangeHighIndexRefValue = collection[500];
        }
    }
}
