using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.Exceptions;
using System;
using System.Data.Common;

namespace DBFacade.DataLayer.CommandConfig
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbCommandConfig
    {
        /// <summary>
        /// Gets the database command.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <typeparam name="Prm">The type of the rm.</typeparam>
        /// <param name="dbMethodParams">The database method parameters.</param>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        Cmd GetDbCommand<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
            where Con : DbConnection
            where Cmd : DbCommand
            where Prm : DbParameter;
        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        object GetReturnValue<Cmd>(Cmd dbCommand)
            where Cmd : DbCommand;
        /// <summary>
        /// Sets the return value.
        /// </summary>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="value">The value.</param>
        void SetReturnValue<Cmd>(Cmd dbCommand, object value)
            where Cmd : DbCommand;
        /// <summary>
        /// Gets the database connection configuration.
        /// </summary>
        /// <returns></returns>
        IDbConnectionConfig GetDBConnectionConfig();
        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        IValidationResult Validate(IDbParamsModel paramsModel);
        /// <summary>
        /// Determines whether [has stored procedure].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has stored procedure]; otherwise, <c>false</c>.
        /// </returns>
        bool HasStoredProcedure();
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <returns></returns>
        Con GetDbConnection<Con>()
            where Con : DbConnection;
        /// <summary>
        /// Determines whether this instance is transaction.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is transaction; otherwise, <c>false</c>.
        /// </returns>
        bool IsTransaction();
        /// <summary>
        /// Gets the missing stored procedure exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        MissingStoredProcedureException GetMissingStoredProcedureException(string message);
        /// <summary>
        /// Gets the SQL execution exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        SQLExecutionException GetSQLExecutionException(string message, Exception e);
    }

}
