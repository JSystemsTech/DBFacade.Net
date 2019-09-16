﻿using DBFacade.DataLayer.ConnectionService;
using System;

namespace DomainFacadeTestLocal.Domain.Connection
{
    internal class MyConnection : SQLConnectionConfig<MyConnection>
    {
        protected override string GetDbConnectionString()
        {
            throw new NotImplementedException();
        }

        protected override string GetDbConnectionProvider()
        {
            throw new NotImplementedException();
        }

        public static IDbCommandText GetMyData = CreateCommandText("GetMyData", "Get My Data");
    }
}
