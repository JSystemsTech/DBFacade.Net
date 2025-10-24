using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
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
}
