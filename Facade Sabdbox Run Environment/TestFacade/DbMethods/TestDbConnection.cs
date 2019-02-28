using DomainFacade.DataLayer.ConnectionService;

namespace DomainFacade.SampleDomainFacade.DbMethods
{
    public class TestDbConnection : DbConnectionConfig.SQL
    {
        protected override string GetConnectionStringName()
        {
            return "DefaultConnection";
        }
        protected override string GetDbConnectionProviderInvariantCore()
        {
            return string.Empty;
        }

        protected override string GetDbConnectionStringCore()
        {
            return string.Empty;
        }
        public static DbCommandText<TestDbConnection> GetAllSimpleData = new DbCommandText<TestDbConnection>("SIMPLE_DATA_GET_ALL");
        public static DbCommandText<TestDbConnection> GetAllMoreData = new DbCommandText<TestDbConnection>("MORE_DATA_GET_ALL");
        public static DbCommandText<TestDbConnection> AddSimpleData = new DbCommandText<TestDbConnection>("SIMPLE_DATA_ADD");
        public static DbCommandText<TestDbConnection> AddMoreData = new DbCommandText<TestDbConnection>("MORE_DATA_ADD");
    }
}
