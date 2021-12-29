using DbFacade.DataLayer.ConnectionService;
using System.Data;
using System.Threading.Tasks;

namespace DbFacade.Factories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    public class DbCommandConfigFactory<TDbConnectionConfig>
        where TDbConnectionConfig: DbConnectionConfigBase
    {

        /// <summary>
        /// Creates the fetch command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        public static IDbCommandConfig CreateFetchCommand(string commandText, string label, bool requiresValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, false, requiresValidation);
        /// <summary>
        /// Creates the transaction command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        public static IDbCommandConfig CreateTransactionCommand(string commandText, string label, bool requiresValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, true, requiresValidation);
        /// <summary>
        /// Creates the fetch command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        public static IDbCommandConfig CreateFetchCommand(string commandText, string label, CommandType commandType, bool requiresValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, false, requiresValidation);
        /// <summary>
        /// Creates the transaction command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        protected static IDbCommandConfig CreateTransactionCommand(string commandText, string label, CommandType commandType, bool requiresValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, true, requiresValidation);
        
    }
}
