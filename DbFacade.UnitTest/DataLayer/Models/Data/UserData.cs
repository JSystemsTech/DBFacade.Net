using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.UnitTest.DataLayer.Models.Data
{
    internal class UserData : IDbDataModel
    {
        public string Name { get; internal set; }
        public int Id { get; internal set; }
        public void Init(IDataCollection collection)
        {
            Name = collection.GetValue<string>("Name");
            Id = collection.GetValue<int>("Id");
        }
    }
}
