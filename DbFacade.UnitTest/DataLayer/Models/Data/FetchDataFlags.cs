using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
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
}
