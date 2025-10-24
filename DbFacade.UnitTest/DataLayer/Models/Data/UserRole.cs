using DbFacade.DataLayer.Models;
using DbFacade.Extensions;
namespace DbFacade.UnitTest.DataLayer.Models.Data
{
    internal class UserRole : IDbDataModel
    {
        public string Name { get; internal set; }
        public string Value { get; internal set; }
        public void Init(IDataCollection collection)
        {
            Name = collection.GetValue<string>("Name");
            Value = collection.GetValue<string>("Value");
        }
    }
}
