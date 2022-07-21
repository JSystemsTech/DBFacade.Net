using DbFacade.DataLayer.ConnectionService;
using System;
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

        /// <summary>Creates the fetch command.</summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Obsolete("use {TDbConnectionConfig}.CreateFetchCommand(string commandText, string label, bool requiresValidation = false) instead", false)]
        public static IDbCommandConfig CreateFetchCommand(string commandText, string label, bool requiresValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, false, requiresValidation);

        /// <summary>Creates the transaction command.</summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Obsolete("use {TDbConnectionConfig}.CreateTransactionCommand(string commandText, string label, bool requiresValidation = true) instead", false)]
        public static IDbCommandConfig CreateTransactionCommand(string commandText, string label, bool requiresValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, CommandType.StoredProcedure, true, requiresValidation);

        /// <summary>Creates the fetch command.</summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Obsolete("use {TDbConnectionConfig}.CreateFetchCommand(string commandText, string label, CommandType commandType, bool requiresValidation = false) instead", false)]
        public static IDbCommandConfig CreateFetchCommand(string commandText, string label, CommandType commandType, bool requiresValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, false, requiresValidation);

        /// <summary>Creates the transaction command.</summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Obsolete("use {TDbConnectionConfig}.CreateTransactionCommand(string commandText, string label, CommandType commandType, bool requiresValidation = true) instead", false)]
        public static IDbCommandConfig CreateTransactionCommand(string commandText, string label, CommandType commandType, bool requiresValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(commandText, label, commandType, true, requiresValidation);
        
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDbCommandConfigSchemaFactory
    {
        /// <summary>
        /// Creates the fetch command.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        IDbCommandConfig CreateFetchCommand(string storedProcedureName, string label, bool requiresValidation = false);

        /// <summary>
        /// Creates the transaction command.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        IDbCommandConfig CreateTransactionCommand(string storedProcedureName, string label, bool requiresValidation = true);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbConnectionConfig">The type of the database connection configuration.</typeparam>
    internal class DbCommandConfigSchemaFactory<TDbConnectionConfig>: IDbCommandConfigSchemaFactory
        where TDbConnectionConfig : DbConnectionConfigBase
    {
        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>
        /// The schema.
        /// </value>
        private string Schema { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfigSchemaFactory{TDbConnectionConfig}"/> class.
        /// </summary>
        /// <param name="schema">The schema.</param>
        public DbCommandConfigSchemaFactory(string schema)
        {
            Schema = FormatCommandTextPart(schema);
        }
        /// <summary>
        /// Formats the command text part.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private string FormatCommandTextPart(string value) => $"{(value.StartsWith("[") ? "":"[")}{value}{(value.EndsWith("]") ? "" : "]")}";
        /// <summary>
        /// Gets the command text.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns></returns>
        private string GetCommandText(string storedProcedureName) => $"{Schema}.{FormatCommandTextPart(storedProcedureName)}";
        /// <summary>
        /// Creates the fetch command.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        public IDbCommandConfig CreateFetchCommand(string storedProcedureName, string label, bool requiresValidation = false)
        => DbCommand<TDbConnectionConfig>.Create(GetCommandText(storedProcedureName), label, CommandType.StoredProcedure, false, requiresValidation);

        /// <summary>
        /// Creates the transaction command.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        public IDbCommandConfig CreateTransactionCommand(string storedProcedureName, string label, bool requiresValidation = true)
        => DbCommand<TDbConnectionConfig>.Create(GetCommandText(storedProcedureName), label, CommandType.StoredProcedure, true, requiresValidation);
    }
}
