using DbFacade.DataLayer.Models;
using DbFacade.Extensions;

namespace DbFacade.DataLayer.ConnectionService
{
    /// <summary>
    ///   <br />
    /// </summary>
    public sealed class DbConnectionConfig
    {
        /// <summary>
        /// The dbo
        /// </summary>
        public readonly Schema Dbo;

        internal readonly IDbConnectionProvider DbConnectionProvider;
        private DbConnectionConfig(IDbConnectionProvider dbConnectionProvider)
        {
            DbConnectionProvider = dbConnectionProvider;
            Dbo = this.CreateSchema("dbo");
        }
        /// <summary>Creates the specified database connection provider.</summary>
        /// <param name="dbConnectionProvider">The database connection provider.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static DbConnectionConfig Create(IDbConnectionProvider dbConnectionProvider)
        => new DbConnectionConfig(dbConnectionProvider);
    }
}
