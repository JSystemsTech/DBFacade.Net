using DbFacade.DataLayer.ConnectionService;
using DbFacade.Utils;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbFacade.Factories
{

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
    }
    /// <summary>
    /// 
    /// </summary>
    internal class DbCommandConfigSchemaFactory : IDbCommandConfigSchemaFactory
    {
        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>
        /// The schema.
        /// </value>
        private string Schema { get; set; }
        /// <summary>Gets or sets the database connection.</summary>
        /// <value>The database connection.</value>
        protected IDbConnectionCore DbConnection { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DbCommandConfigSchemaFactory" /> class.</summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="schema">The schema.</param>
        public DbCommandConfigSchemaFactory(IDbConnectionCore dbConnection, string schema)
        {
            Schema = FormatCommandTextPart(schema);
            DbConnection = dbConnection;
        }
        /// <summary>
        /// Formats the command text part.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private string FormatCommandTextPart(string value) => $"{(value.StartsWith("[") ? "" : "[")}{value}{(value.EndsWith("]") ? "" : "]")}";
        /// <summary>
        /// Gets the command text.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns></returns>
        protected string GetCommandText(string storedProcedureName) => $"{Schema}.{FormatCommandTextPart(storedProcedureName)}";
        /// <summary>
        /// Creates the fetch command.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        public IDbCommandConfig CreateFetchCommand(string storedProcedureName, string label, bool requiresValidation = false)
        => DbCommandSettings.Create(DbConnection,GetCommandText(storedProcedureName), label, CommandType.StoredProcedure, false, requiresValidation);
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDbCommandConfigSchemaFactoryFull: IDbCommandConfigSchemaFactory
    {
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
    internal class DbCommandConfigSchemaFactoryFull : DbCommandConfigSchemaFactory, IDbCommandConfigSchemaFactoryFull
    {
        /// <summary>Initializes a new instance of the <see cref="DbCommandConfigSchemaFactory" /> class.</summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="schema">The schema.</param>
        public DbCommandConfigSchemaFactoryFull(IDbConnectionCore dbConnection, string schema) : base(dbConnection, schema) { }

        /// <summary>
        /// Creates the transaction command.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="label">The label.</param>
        /// <param name="requiresValidation">if set to <c>true</c> [requires validation].</param>
        /// <returns></returns>
        public IDbCommandConfig CreateTransactionCommand(string storedProcedureName, string label, bool requiresValidation = true)
        => DbCommandSettings.Create(DbConnection, GetCommandText(storedProcedureName), label, CommandType.StoredProcedure, true, requiresValidation);

    }
}
