using DbFacade.DataLayer.ConnectionService;
using DbFacade.Extensions;
using System.Data;
using System.Net.Mail;

namespace DbFacade.UnitTest.Services
{
    internal class DbConnectionConfigService
    {
        private readonly ConnectionStringService ConnectionStringService;
        internal readonly DbConnectionConfig SQL;
        internal readonly DbConnectionConfig SQL_WithCredendials;
        internal readonly DbConnectionConfig SQL_BadConnStr;
        public DbConnectionConfigService()
        {
            ConnectionStringService = new ConnectionStringService("UnitTestAppId");
            SQL = CreateDbConnectionConfig(new SqlDbConnectionProvider());
            SQL_BadConnStr = CreateDbConnectionConfig_BadConnStr(new SqlDbConnectionProvider());

            var sqlProvider_WithCredendials = new SqlDbConnectionProvider();
            sqlProvider_WithCredendials.BindCredentials("user_name","password");
            SQL_WithCredendials = CreateDbConnectionConfig(new SqlDbConnectionProvider());

        }
        private DbConnectionConfig CreateDbConnectionConfig(IDbConnectionProvider provider)
        {
            provider.BindConnectionString("fakeConnStr");//for unit test coverage only
            provider.BindConnectionString(ConnectionStringService.GetConnectionString);
            provider.BindErrorHandler(OnError);
            provider.DbTypeCollection.Add<int?>(DbType.Int32);
            provider.DbTypeCollection.Add<MailAddress>(DbType.String, m => m.Address);
            return provider.DbConnectionConfig;
        }
        private DbConnectionConfig CreateDbConnectionConfig_BadConnStr(IDbConnectionProvider provider)
        {
            provider.BindErrorHandler(OnError);
            return provider.DbConnectionConfig;
        }
        private void OnError(EndpointErrorInfo errorInfo)
        {

        }
    }
}
