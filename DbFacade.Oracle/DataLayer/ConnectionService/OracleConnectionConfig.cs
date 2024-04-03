using DbFacade.DataLayer.ConnectionService;
using Oracle.ManagedDataAccess.Client;
using System;

namespace DbFacade.Oracle.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OracleConnectionConfig<TDbConnectionConfig> : DbConnectionConfigFull<TDbConnectionConfig>
        where TDbConnectionConfig : IDbConnectionConfig
    {        
        public static void Configure(OnGetConnectionString getConnectionString, Action<IErrorHandlerOptions> handler)
        {
            handler(GetConnectionOptions().ErrorHandlerOptions);
            GetConnectionOptions().SetOnGetDbDataAdapter(() => new OracleDataAdapter());
            GetConnectionOptions().SetOnGetConnectionString(getConnectionString);
            GetConnectionOptions().SetOnCreateDbConnection(connectionString => new OracleConnection(connectionString));
        }
        public static void Configure(OnGetConnectionString getConnectionString, OracleCredential credential, Action<IErrorHandlerOptions> handler)
        {
            handler(GetConnectionOptions().ErrorHandlerOptions);
            GetConnectionOptions().SetOnGetDbDataAdapter(() => new OracleDataAdapter());
            GetConnectionOptions().SetOnGetConnectionString(getConnectionString);
            GetConnectionOptions().SetOnCreateDbConnection(connectionString => new OracleConnection(connectionString, credential));
        }
    }
}
