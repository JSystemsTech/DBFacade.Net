using DBFacade.DataLayer.ConnectionService;
using System;

namespace DomainFacadeTestLocal.Domain.Connection
{
    internal class MyConnection : SQLConnectionConfig<MyConnection>
    {
        protected override string GetConnectionStringName()
        {
            throw new NotImplementedException();
        }

        protected override string GetDbConnectionProviderInvariantCore()
        {
            throw new NotImplementedException();
        }

        protected override string GetDbConnectionStringCore()
        {
            throw new NotImplementedException();
        }
        public static IDbCommandText GetMyData = CreateCommandText("GetMyData", "Get My Data");
    }
}
