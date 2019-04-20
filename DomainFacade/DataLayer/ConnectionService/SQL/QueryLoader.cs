using System.IO;
using System.Reflection;

namespace DomainFacade.DataLayer.ConnectionService.SQL
{
    /// <summary>
    /// 
    /// </summary>
    internal class QueryLoader
    {
        /// <summary>
        /// Gets the sproc meta query.
        /// </summary>
        /// <returns></returns>
        public static string GetSprocMetaQuery()
        {
            return LoadSQLStatement("SprocMetaQuery.sql");
        }
        /// <summary>
        /// Loads the SQL statement.
        /// </summary>
        /// <param name="statementName">Name of the statement.</param>
        /// <returns></returns>
        private static string LoadSQLStatement(string statementName)
        {
            string sqlStatement = string.Empty;

            string namespacePart = "DomainFacade.DataLayer.ConnectionService.SQL";
            string resourceName = namespacePart + "." + statementName;

            using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stm != null)
                {
                    sqlStatement = new StreamReader(stm).ReadToEnd();
                }
            }

            return sqlStatement;
        }
    }
}
