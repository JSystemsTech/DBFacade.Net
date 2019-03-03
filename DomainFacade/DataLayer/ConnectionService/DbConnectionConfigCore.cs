using System.Data.Common;
using System.Configuration;
using System.Data;
using static DomainFacade.DataLayer.ConnectionService.DbConnectionService;

namespace DomainFacade.DataLayer.ConnectionService
{
    public abstract class DbConnectionConfigCore: DbConnectionConfigBase, IDbConnectionConfig
    {
        private DbConnectionStoredProcedure[] AvailableStoredProcsValue { get; set; }
        public DbConnectionStoredProcedure[] AvailableStoredProcs() { return AvailableStoredProcsValue; }
        public override void SetAvailableStoredProcs(DbConnectionStoredProcedure[] storedProcs)
        {
            if(AvailableStoredProcsValue == null)
            {
                AvailableStoredProcsValue = storedProcs;
            }
        }
        public override IDbConnection GetDbConnection()
        {
            return GetDbConnectionCore<DbConnection>();
        }
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
        protected abstract string GetDbConnectionStringCore();
        protected abstract string GetDbConnectionProviderInvariantCore();

        protected abstract string GetConnectionStringName();
        protected ConnectionStringSettings GetConnectionStringSettings()
        {
            return ConfigurationManager.ConnectionStrings[GetConnectionStringName()];
        }
        public override string GetAvailableStoredProcCommandText() {
            
            return GetDefaultAvailableStoredProcCommandText();
        }
        public override CommandType GetAvailableStoredProcCommandType()
        {
            return CommandType.Text;
        }
        public override bool CheckStoredProcAvailability()
        {
            return true;
        }
    }    
}
