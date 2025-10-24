using DbFacade.DataLayer.ConnectionService;
using DbFacade.Extensions;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class Schema
    {
        private readonly string SchemaName;
        internal readonly IDbConnectionProvider DbConnectionProvider;
        internal Schema(DbConnectionConfig dbConnection, string schemaName)
        {
            SchemaName = schemaName.FormatCommandTextPart();
            DbConnectionProvider = dbConnection.DbConnectionProvider;
        }
        internal string BuildCommandText(string storedProcedureName) => $"{SchemaName}.{storedProcedureName.FormatCommandTextPart()}";
    }
}
