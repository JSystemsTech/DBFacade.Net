using DbFacade.DataLayer.CommandConfig;
using DbFacade.Extensions;
using DbFacade.UnitTest.Services;

namespace DbFacade.UnitTest.DataLayer.EndpointLayer
{
    internal partial class Endpoints
    {
        internal IDbCommandMethod TestBenchmark { get; private set; }
        
        private void OnInit_Benchmark()
        {
            TestBenchmark = SQLConnection.Dbo.DefineEndpoint("TestBenchmark", o => {
                o.ConnectionStringId = ConnectionStringIds.SQLUnitTest;
                o.AsStoredProcedure("TestBenchmark");
            });
        }
    }
}
