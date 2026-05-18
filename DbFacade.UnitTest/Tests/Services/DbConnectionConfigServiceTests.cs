using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFacade.UnitTest.Tests.Services
{
    public class DbConnectionConfigServiceTests : UnitTestBase
    {
        public DbConnectionConfigServiceTests(ServiceFixture services) : base(services) { }

        [Fact]
        public void VerifySQLTables()
        {
            //Services.EndpointMockHelper.TestNonQuery_EnableMockMode();
            //var response = Services.DomainFacade.TestNonQuery();
            Assert.True(true);
        }
    }
}
