using DBFacade.DataLayer.ConnectionService;

namespace DocumentationExamples.DataLayer.Connection
{
    internal class MySqlDbConnection : SqlConnectionConfig<MySqlDbConnection>
    {
        /* Define MySqlDbConnection private variables */
        private string ConnectionString { get; set; }
        private string ConnectionProvider { get; set; }

        /* Define MySqlDbConnection constructor */
        public MySqlDbConnection(string connectionString, string connectionProvider)
        {
            ConnectionString = connectionString;
            ConnectionProvider = connectionProvider;
        }
        /* Point connection settings to private variables */
        protected override string GetDbConnectionString() => ConnectionString;
        protected override string GetDbConnectionProvider() => ConnectionProvider;

        /*
         * Begin Define Database calls associated with this database connection 
         * 
         * Use CreateCommandText Helper
         * CreateCommandText(<Defined SQL Command Text>, <Label for Command Text>);
         */
        public static IDbCommandText GetData = CreateCommandText("[dbo].[Data_Get]", "Get Data");
        public static IDbCommandText GetDataWithParameters = CreateCommandText("[dbo].[Data_GetWithParameters]", "Get Data with Parameters");
        public static IDbCommandText GetDataWithOutputParameters = CreateCommandText("[dbo].[Data_GetWithOutputParameters]", "Get Data with output parameters");
        public static IDbCommandText ExecuteMyTransaction = CreateCommandText("[dbo].[MyTransaction_Execute]", "Execute My Transaction");

        /* Add any more desired command Text Calls Here*/
    }
}
