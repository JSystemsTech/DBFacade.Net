using System.Data.Common;
using System.Configuration;
using System.Data;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.ConnectionService
{
    public abstract class DbConnectionConfigBase: IDbConnectionConfig
    {
        public virtual IDbConnection GetDbConnection() => GetDbConnectionCore<DbConnection>();
        public virtual IDbResponse<TDbDataModel> ExecuteDbAction<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        => null;
        public async virtual Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest
        => await Task.Run(() => new DbResponse<TDbManifestMethod, TDbDataModel>());
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
