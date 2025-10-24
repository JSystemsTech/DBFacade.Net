using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace RealisticExampleProject.Services
{

    public interface IConnectionStringService
    {
        dynamic ConnectionStringIdStore { get; }
        string GetConnectionString(string connectionStringId);
    }
    internal class ConnectionStringService: IConnectionStringService
    {
        private readonly IConfiguration ApplicationConfiguration;
        private readonly IConfiguration Configuration;
        private readonly IConfiguration ConnectionStrings;
        private readonly string AppId;
        private readonly string[] ConnectionStringIds;
        public dynamic ConnectionStringIdStore { get; private set; }
        public ConnectionStringService(IConfiguration configuration)
        {
            ApplicationConfiguration = configuration;
            Configuration = ApplicationConfiguration.GetRequiredSection("ConnectionStringService");

            //Loads the "ConnectionStrings" section from configuration. This is typically stored in secrects.json  
            ConnectionStrings = ApplicationConfiguration.GetRequiredSection("ConnectionStrings");
            AppId = Configuration["AppId"];
            ConnectionStringIds = Configuration.GetRequiredSection("ConnectionStringIds").GetChildren().ToArray().Select(c => c.Value).ToArray();
            ConnectionStringIdStore = new ConnectionStringIdStore(ConnectionStringIds);
        }
        public string GetConnectionString(string connectionStringId)
        => !ConnectionStringIds.Contains(connectionStringId) ? string.Empty : //Ignore values not preconfigured
                    TryGetConnectionStringFromConnectedService(connectionStringId, out string connSvcConnStr) ? connSvcConnStr :
                    TryGetConnectionStringFromConfig(connectionStringId, out string configConnStr) ? configConnStr :
                    string.Empty;
        private bool TryGetConnectionStringFromConfig(string connectionStringId, out string connectionString)
        {
            connectionString = ConnectionStrings[connectionStringId];
            return !string.IsNullOrWhiteSpace(connectionString);
        }
        private bool TryGetConnectionStringFromConnectedService(string connectionStringId, out string connectionString)
        {
            try
            {
                connectionString = null;
                //TODO: call connected service
                //ConnectionStringResult response = SomeConnectedServiceOrAPI.GetConnectionString(AppId, connectionStringId);
                return !string.IsNullOrWhiteSpace(connectionString);
            }
            catch (Exception ex)
            {
                connectionString = null;
                return false;
            }
        }
    }
}