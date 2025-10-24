namespace DbFacade.UnitTest.Services
{
    internal class ConnectionStringService
    {
        private readonly string AppId;
        private const string SQLConnectionStringTest = "Data Source=SqlServerName\\MSSQL1;Initial Catalog=DatabaseName;Persist Security Info=True;User ID=UserName;Password=password;Trusted_Connection=True;TrustServerCertificate=True";
        public ConnectionStringService(string appId)
        {
            AppId = appId;
        }
        internal string GetConnectionString(string connectionStringId)
        {
            return connectionStringId == ConnectionStringIds.SQLUnitTest ? SQLConnectionStringTest : null;
        }
    }
}
