using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace DbFacade.DataLayer.ConnectionService.MockDb
{
    [ExcludeFromCodeCoverage]
    internal class MockDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable { 
    
        internal MockDataAdapter(MockResponse mockResponse) {
            ErrorHelper.CheckThrowMockException(mockResponse.Settings.ExecuteQueryError);
        }
    }
}
