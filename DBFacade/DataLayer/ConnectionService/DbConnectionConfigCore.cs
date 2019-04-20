using System.Data.Common;
using System.Configuration;
using System.Data;
using static DBFacade.DataLayer.ConnectionService.DbConnectionService;

namespace DBFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DbConnectionConfigBase" />
    /// <seealso cref="IDbConnectionConfig" />
    public abstract class DbConnectionConfigCore : DbConnectionConfigBase, IDbConnectionConfig
    {
        /// <summary>
        /// Gets or sets the available stored procs value.
        /// </summary>
        /// <value>
        /// The available stored procs value.
        /// </value>
        private DbConnectionStoredProcedure[] AvailableStoredProcsValue { get; set; }
        /// <summary>
        /// Availables the stored procs.
        /// </summary>
        /// <returns></returns>
        public DbConnectionStoredProcedure[] AvailableStoredProcs() { return AvailableStoredProcsValue; }
        /// <summary>
        /// Sets the available stored procs.
        /// </summary>
        /// <param name="storedProcs">The stored procs.</param>
        public override void SetAvailableStoredProcs(DbConnectionStoredProcedure[] storedProcs)
        {
            if (AvailableStoredProcsValue == null)
            {
                AvailableStoredProcsValue = storedProcs;
            }
        }
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetDbConnection()
        {
            return GetDbConnectionCore<DbConnection>();
        }
        /// <summary>
        /// Gets the database connection core.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <returns></returns>
        protected Con GetDbConnectionCore<Con>() where Con : DbConnection
        {
            string provider = GetDbConnectionProviderInvariantCore();
            string connectionString = GetDbConnectionStringCore();

            if (GetConnectionStringName() != string.Empty)
            {
                provider = GetConnectionStringSettings().ProviderName;
                connectionString = GetConnectionStringSettings().ConnectionString;
            }

            Con dbConnection = (Con)DbProviderFactories.GetFactory(provider).CreateConnection();
            dbConnection.ConnectionString = connectionString;
            return dbConnection;
        }
        /// <summary>
        /// Gets the database connection string core.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDbConnectionStringCore();
        /// <summary>
        /// Gets the database connection provider invariant core.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDbConnectionProviderInvariantCore();

        /// <summary>
        /// Gets the name of the connection string.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetConnectionStringName();
        /// <summary>
        /// Gets the connection string settings.
        /// </summary>
        /// <returns></returns>
        protected ConnectionStringSettings GetConnectionStringSettings()
        {
            return ConfigurationManager.ConnectionStrings[GetConnectionStringName()];
        }
        /// <summary>
        /// Gets the available stored proc command text.
        /// </summary>
        /// <returns></returns>
        public override string GetAvailableStoredProcCommandText()
        {

            return GetDefaultAvailableStoredProcCommandText();
        }
        /// <summary>
        /// Gets the type of the available stored proc command.
        /// </summary>
        /// <returns></returns>
        public override CommandType GetAvailableStoredProcCommandType()
        {
            return CommandType.Text;
        }
        /// <summary>
        /// Checks the stored proc availability.
        /// </summary>
        /// <returns></returns>
        public override bool CheckStoredProcAvailability()
        {
            return true;
        }
    }
}
