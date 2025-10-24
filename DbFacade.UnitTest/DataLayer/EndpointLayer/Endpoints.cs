using DbFacade.DataLayer.ConnectionService;
using DbFacade.UnitTest.Services;

namespace DbFacade.UnitTest.DataLayer.EndpointLayer
{
    internal partial class Endpoints
    {
        private readonly DbConnectionConfig SQLConnection;
        private readonly DbConnectionConfig SQLConnection_WithSqlCredendials;
        private readonly DbConnectionConfig SQL_BadConnStr;
        internal Endpoints(DbConnectionConfigService dbConnectionConfigService)
        {
            SQLConnection = dbConnectionConfigService.SQL;
            SQLConnection_WithSqlCredendials = dbConnectionConfigService.SQL_WithCredendials;
            SQL_BadConnStr = dbConnectionConfigService.SQL_BadConnStr;

            OnInit();
        }
        private void OnInit()
        {
            OnInit_Benchmark();
            OnInit_FetchData();
            OnInit_Transaction();
            OnInit_Exceptions();
        }
    }
}
