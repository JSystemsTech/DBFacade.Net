using DbFacade.DataLayer.ConnectionService;
using DbFacade.Extensions;

namespace DbFacade.Factories
{

    public class Schema
    {
        private readonly string SchemaName;
        internal DbConnectionConfigBase DbConnection { get; private set; }
        internal Schema(DbConnectionConfig dbConnection, string schemaName)
        {
            SchemaName = schemaName.FormatCommandTextPart();
            DbConnection = dbConnection.DbConnection;
        }
        internal string BuildCommandText(string storedProcedureName) => $"{SchemaName}.{storedProcedureName.FormatCommandTextPart()}";
    }
}
