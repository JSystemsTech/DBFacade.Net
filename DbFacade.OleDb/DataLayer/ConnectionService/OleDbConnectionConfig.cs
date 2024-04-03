using DbFacade.DataLayer.ConnectionService;
using System;
using System.Data.OleDb;

namespace DbFacade.OleDb.DataLayer.ConnectionService
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
#if (NET8_0_OR_GREATER)
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif
    public abstract class
        OleDbConnectionConfig<TDbConnectionConfig> : DbConnectionConfigFull<TDbConnectionConfig> where TDbConnectionConfig : IDbConnectionConfig
    {
        public static void Configure(OnGetConnectionString getConnectionString, Action<IErrorHandlerOptions> handler)
        {
            handler(GetConnectionOptions().ErrorHandlerOptions);
            GetConnectionOptions().SetOnGetDbDataAdapter(() => new OleDbDataAdapter());
            GetConnectionOptions().SetOnGetConnectionString(getConnectionString);
            GetConnectionOptions().SetOnCreateDbConnection(connectionString => new OleDbConnection(connectionString));
        }
    }
}
