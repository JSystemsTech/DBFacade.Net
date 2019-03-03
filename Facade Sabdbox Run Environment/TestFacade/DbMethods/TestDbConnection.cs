using DomainFacade.DataLayer.ConnectionService;

namespace DomainFacade.SampleDomainFacade.DbMethods
{
    public class TestDbConnection : DbConnectionConfig<TestDbConnection>.SQL
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
        
        public static IDbCommandText GetAllSimpleData = CreateCommandText("SIMPLE_DATA_GET_ALL", "Get simple Data");
        public static IDbCommandText GetAllMoreData = CreateCommandText("MORE_DATA_GET_ALL", "Get more Data");
        public static IDbCommandText AddSimpleData = CreateCommandText("SIMPLE_DATA_ADD", "Add simple Data");
        public static IDbCommandText AddMoreData = CreateCommandText("MORE_DATA_ADD", "Add more Data");
        public static IDbCommandText MissingSproc2 = CreateCommandText("SOME_MISSING_SPROC", "Some Missing Sproc");
    }
}
