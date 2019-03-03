using System.IO;
using System.Reflection;

namespace DomainFacade.DataLayer.ConnectionService.SQL
{
    internal class QueryLoader
    {
        public static string GetSprocMetaQuery()
        {
            return LoadSQLStatement("SprocMetaQuery.sql");
        }
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
