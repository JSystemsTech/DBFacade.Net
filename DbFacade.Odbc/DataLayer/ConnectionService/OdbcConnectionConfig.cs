using DbFacade.DataLayer.ConnectionService;
using System;
using System.Data.Odbc;

namespace DbFacade.Odbc.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OdbcConnectionConfig<TDbConnectionConfig> : DbConnectionConfigFull<TDbConnectionConfig> 
        where TDbConnectionConfig : IDbConnectionConfig
    {
        public static void Configure(OnGetConnectionString getConnectionString, Action<IErrorHandlerOptions> handler)
        {
            handler(GetConnectionOptions().ErrorHandlerOptions);
            GetConnectionOptions().SetOnGetDbDataAdapter(() => new OdbcDataAdapter());
            GetConnectionOptions().SetOnGetConnectionString(getConnectionString);
            GetConnectionOptions().SetOnCreateDbConnection(connectionString => new OdbcConnection(connectionString));
        }
    }
}
