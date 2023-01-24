using DbFacade.DataLayer.ConnectionService;
using System.Data;
using System.Data.Odbc;

namespace DbFacade.Odbc.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OdbcConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
        protected sealed override IDbDataAdapter GetDbDataAdapter() => new OdbcDataAdapter();
        protected sealed override IDbConnection CreateDbConnection(string connectionString) => new OdbcConnection(connectionString);
    }
}
