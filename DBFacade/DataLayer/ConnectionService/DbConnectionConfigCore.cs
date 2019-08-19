using System.Data.Common;
using System.Configuration;
using System.Data;

namespace DBFacade.DataLayer.ConnectionService
{
    public abstract class DbConnectionConfigCore : DbConnectionConfigBase, IDbConnectionConfig
    {
        public override IDbConnection GetDbConnection() => GetDbConnectionCore<DbConnection>();
        protected TDbConnection GetDbConnectionCore<TDbConnection>() where TDbConnection : DbConnection
        {
            string provider = GetDbConnectionProviderInvariantCore();
            string connectionString = GetDbConnectionStringCore();

            if (!string.IsNullOrEmpty(GetConnectionStringName()))
            {
                provider = GetConnectionStringSettings().ProviderName;
                connectionString = GetConnectionStringSettings().ConnectionString;
            }

            TDbConnection dbConnection = DbProviderFactories.GetFactory(provider).CreateConnection() as TDbConnection;
            dbConnection.ConnectionString = connectionString;
            return dbConnection;
        }
        protected abstract string GetDbConnectionStringCore();
        protected abstract string GetDbConnectionProviderInvariantCore();
        protected abstract string GetConnectionStringName();
        protected ConnectionStringSettings GetConnectionStringSettings() => ConfigurationManager.ConnectionStrings[GetConnectionStringName()];    
    }
}
