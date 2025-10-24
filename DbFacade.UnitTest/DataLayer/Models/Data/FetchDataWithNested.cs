using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
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
}
