using DbFacade.DataLayer.ConnectionService;
using DbFacade.Extensions;
using Microsoft.Data.SqlClient;
using System;

namespace DbFacadeUnitTests.TestFacade
{
    internal class UnitTestConnectionProvider : SqlDbConnectionProvider
    {
        private static string GetDbConnectionString(string connectionStringId) => "MyUnitTestConnectionString";
        private readonly Action<Exception> ErrorHandler;
        public UnitTestConnectionProvider(Action<Exception> errorHandler):base(GetDbConnectionString) {
            ErrorHandler = errorHandler;
        }
        public SqlCredential Credential => null;

        public override void OnError(Exception ex, EndpointSettings endpointSettings, object parameters, DbExecutionExceptionType type)
        => ErrorHandler(ex);
    }
    internal class UnitTestConnection : SqlDbConnectionConfig
    {
        internal UnitTestConnection(UnitTestConnectionProvider options) : base(options) { }
    }
}
