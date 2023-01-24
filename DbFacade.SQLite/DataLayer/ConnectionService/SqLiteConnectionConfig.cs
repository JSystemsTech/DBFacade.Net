using DbFacade.DataLayer.ConnectionService;
using System.Data;
using System.Data.SQLite;

namespace DbFacade.SQLite.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        SqLiteConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig>
        where TDbConnectionConfig : DbConnectionConfigBase
    {
        protected sealed override IDbDataAdapter GetDbDataAdapter() => new SQLiteDataAdapter();
        protected sealed override IDbConnection CreateDbConnection(string connectionString) => new SQLiteConnection(connectionString);
    }
}
