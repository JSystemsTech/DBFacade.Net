using DbFacade.DataLayer.CommandConfig;
using DbFacade.Extensions;
using DbFacade.UnitTest.Services;

namespace DbFacade.UnitTest.DataLayer.EndpointLayer
{
    internal partial class Endpoints
    {

        internal IDbCommandMethod TestException { get; private set; }

        internal IDbCommandMethod TestMockException_ExecuteQuery { get; private set; }
        internal IDbCommandMethod TestMockException_ExecuteNonQuery { get; private set; }
        internal IDbCommandMethod TestMockException_ExecuteXml { get; private set; }
        internal IDbCommandMethod TestMockException_ExecuteScalar{ get; private set; }
        internal IDbCommandMethod TestMockException_Transaction{ get; private set; }

        private void OnInit_Exceptions()
        {
            TestException = SQLConnection.Dbo.DefineEndpoint("TestException", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestException")
                .WithParameters<Exception>(o => { })
                .BindOnBeforeExecute((cmd, m) => { 
                    if(m is Exception ex)
                    {
                        throw ex; //forceably throw exception passed is for testing
                    }
                })
                .BindErrorHandler(error => {
                    error.ErrorData["IsCustom"] = true;
                    error.ErrorData["Message"] = "custom message";
                    error.ErrorData["LogDate"] = DateTime.Parse("01/01/2025");
                });
            });
            TestMockException_ExecuteQuery = SQLConnection.DefineEndpoint("TestMockException_ExecuteQuery", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsQuery("SELECT * FROM [dbo].[TestMockException_ExecuteQuery]");                
            });
            TestMockException_ExecuteNonQuery = SQLConnection.DefineEndpoint("TestMockException_ExecuteNonQuery", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsNonQuery("SELECT * FROM [dbo].[TestMockException_ExecuteNonQuery]");
            });
            TestMockException_ExecuteScalar = SQLConnection.DefineEndpoint("TestMockException_ExecuteScalar", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsScalar("SELECT * FROM [dbo].[TestMockException_ExecuteScalar]");
            });
            TestMockException_ExecuteXml = SQLConnection.DefineEndpoint("TestMockException_ExecuteXml", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsXml("SELECT * FROM [dbo].[TestMockException_ExecuteXml] FOR XML AUTO, XMLDATA");
            });
            TestMockException_Transaction = SQLConnection.DefineEndpoint("TestMockException_Transaction", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsNonQuery("SELECT * FROM [dbo].[TestMockException_Transaction]")
                .AsTransaction();
            });
        }

    }
}
