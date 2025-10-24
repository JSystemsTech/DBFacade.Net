using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    internal class MockDbException : DbException
    {
        public MockDbException(string message):base(message) { }
    }
}
