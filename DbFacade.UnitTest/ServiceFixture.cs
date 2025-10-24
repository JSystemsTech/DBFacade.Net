using DbFacade.UnitTest;
using DbFacade.UnitTest.DataLayer;
using DbFacade.UnitTest.DataLayer.EndpointLayer;
using DbFacade.UnitTest.Services;
using DbFacade.UnitTest.TestHelpers;

[assembly: AssemblyFixture(typeof(ServiceFixture))]
namespace DbFacade.UnitTest
{
    public class ServiceFixture : IDisposable
    {
        private readonly DbConnectionConfigService DbConnectionConfigService;
        internal readonly Endpoints Endpoints;
        internal readonly DomainFacade DomainFacade;
        internal readonly EndpointMockHelper EndpointMockHelper;
        public ServiceFixture() 
        {
            DbConnectionConfigService = new DbConnectionConfigService();
            Endpoints = new Endpoints(DbConnectionConfigService);
            DomainFacade = new DomainFacade(Endpoints);
            EndpointMockHelper = new EndpointMockHelper(Endpoints);
        }
        public void Dispose() {
            EndpointMockHelper.DisableMockMode();
        }
    }
}
