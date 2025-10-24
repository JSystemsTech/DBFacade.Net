using DbFacade.Extensions;
using DbFacade.Factories;

namespace DbFacade.DataLayer.ConnectionService
{
    /// <summary>
    ///   <br />
    /// </summary>
    public abstract class DbConnectionConfig
    {
        /// <summary>
        /// The dbo
        /// </summary>
        public readonly Schema Dbo;
        internal readonly DbConnectionConfigBase DbConnection; 
        public DbConnectionConfig(IDbConnectionProvider dbConnectionProvider)
        {
            DbConnection = new DbConnectionConfigBase(dbConnectionProvider);
            Dbo = this.CreateSchemaFactory("dbo");
        }
        /// <summary>Enables the mock mode.</summary>
        public void EnableMockMode() => DbConnection.EnableMockMode();
        /// <summary>Disables the mock mode.</summary>
        public void DisableMockMode() => DbConnection.DisableMockMode();
    }
}
