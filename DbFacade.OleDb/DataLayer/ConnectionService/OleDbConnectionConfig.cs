using DbFacade.DataLayer.ConnectionService;
using System.Data;
using System.Data.OleDb;
using System.Runtime.Versioning;

namespace DbFacade.OleDb.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OleDbConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig> where TDbConnectionConfig : DbConnectionConfigBase
    {
        protected sealed override IDbDataAdapter GetDbDataAdapter() => new OleDbDataAdapter();
        protected sealed override IDbConnection CreateDbConnection(string connectionString) => new OleDbConnection(connectionString);
    }
}
