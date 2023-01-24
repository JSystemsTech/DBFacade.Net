using DbFacade.DataLayer.ConnectionService;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DbFacade.Oracle.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public abstract class
        OracleConnectionConfig<TDbConnectionConfig> : DbConnectionConfig<TDbConnectionConfig>
        where TDbConnectionConfig : DbConnectionConfigBase
    {
        protected sealed override IDbDataAdapter GetDbDataAdapter() => new OracleDataAdapter();
        protected sealed override IDbConnection CreateDbConnection(string connectionString)
            => GetCredential() is OracleCredential credential ?
            new OracleConnection(connectionString, credential) :
            new OracleConnection(connectionString);


        /// <summary>Gets the credential.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        protected virtual OracleCredential GetCredential() => null;
    }
}
