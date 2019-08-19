using System.Data.Common;
using System.Configuration;
using System.Data;

namespace DBFacade.DataLayer.ConnectionService
{
    public abstract class DbConnectionConfigCore : DbConnectionConfigBase, IDbConnectionConfig
    {
        public sealed override IDbConnection GetDbConnection() => GetDbConnectionCore<DbConnection>();
        protected Con GetDbConnectionCore<Con>() where Con : DbConnection
        {
            string provider = GetDbConnectionProviderInvariantCore();
            string connectionString = GetDbConnectionStringCore();

            if (!string.IsNullOrEmpty(GetConnectionStringName()))
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
        protected ConnectionStringSettings GetConnectionStringSettings() => ConfigurationManager.ConnectionStrings[GetConnectionStringName()];    
    }
}
