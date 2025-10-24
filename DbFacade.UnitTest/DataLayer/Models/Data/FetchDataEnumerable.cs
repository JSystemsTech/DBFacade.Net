using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
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
            DataCustom = collection.ToEnumerable<string>("EnumerableDataCustom", separator: ';');
        }
    }
}
