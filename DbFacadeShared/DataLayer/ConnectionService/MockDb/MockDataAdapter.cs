using System;
using System.Data;
using System.Data.Common;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    internal class MockDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable { }
}
