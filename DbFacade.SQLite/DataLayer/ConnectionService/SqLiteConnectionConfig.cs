using DbFacade.DataLayer.ConnectionService;
using System;
using System.Data.SQLite;

namespace DbFacade.SQLite.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        SqLiteConnectionConfig<TDbConnectionConfig> : DbConnectionConfigFull<TDbConnectionConfig>
        where TDbConnectionConfig : IDbConnectionConfig
    {
        public static void Configure(OnGetConnectionString getConnectionString, Action<IErrorHandlerOptions> handler)
        {
            handler(GetConnectionOptions().ErrorHandlerOptions);
            GetConnectionOptions().SetOnGetDbDataAdapter(() => new SQLiteDataAdapter());
            GetConnectionOptions().SetOnGetConnectionString(getConnectionString);
            GetConnectionOptions().SetOnCreateDbConnection(connectionString => new SQLiteConnection(connectionString));
        }
    }
}
