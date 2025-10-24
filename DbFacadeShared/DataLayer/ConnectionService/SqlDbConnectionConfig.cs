using DbFacade.Extensions;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace DbFacade.DataLayer.ConnectionService
{
    public class SqlDbConnectionProvider : IDbConnectionProvider
    {
        private readonly Func<string, string> GetConnectionString;
        private SqlCredential Credential { get; set; }
        public SqlDbConnectionProvider(Func<string,string> getConnectionString, SqlCredential credential = null)
        { 
            Credential = credential; 
            GetConnectionString = getConnectionString; 
        }
        public SqlDbConnectionProvider(string connectionString, SqlCredential credential = null) : this((connectionStringId) => connectionString, credential) { }

        public IDbConnection GetDbConnection(string connectionStringId)
        => Credential != null ?
            new SqlConnection(GetConnectionString(connectionStringId), Credential) :
            new SqlConnection(GetConnectionString(connectionStringId));

        public IDbDataAdapter GetDbDataAdapter()
        => new SqlDataAdapter();

        public virtual void OnError(Exception ex, EndpointSettings endpointSettings, object parameters, DbExecutionExceptionType type) { }
    }

    /// <summary>
    ///   <br />
    /// </summary>
    public class SqlDbConnectionConfig : DbConnectionConfig
    {
        /// <summary>Initializes a new instance of the <see cref="SqlDbConnectionConfig" /> class.</summary>
        /// <param name="options">The options.</param>
        public SqlDbConnectionConfig(SqlDbConnectionProvider options):base(options) { }
    }
}
