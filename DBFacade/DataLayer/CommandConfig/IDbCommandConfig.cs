using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using System;
using System.Data.Common;

namespace DBFacade.DataLayer.CommandConfig
{
    public interface IDbCommandConfig: IDisposable
    {
        /// <summary>
        /// Gets the database command.
        /// </summary>
        /// <typeparam name="TDbConnection">The type of the database connection.</typeparam>
        /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
        /// <typeparam name="TDbParameter">The type of the database parameter.</typeparam>
        /// <param name="TDbManifestMethodParams">The t database manifest method parameters.</param>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        TDbCommand GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(IDbParamsModel TDbManifestMethodParams, TDbConnection dbConnection)
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter;
        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        object GetReturnValue<TDbCommand>(TDbCommand dbCommand)
            where TDbCommand : DbCommand;
        /// <summary>
        /// Gets the database connection configuration.
        /// </summary>
        /// <returns></returns>
        IDbConnectionConfig GetDBConnectionConfig();
        /// <summary>
        /// Gets the database command text.
        /// </summary>
        /// <returns></returns>
        IDbCommandText GetDbCommandText();
        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        IValidationResult Validate(IDbParamsModel paramsModel);
        /// <summary>
        /// Determines whether this instance is transaction.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is transaction; otherwise, <c>false</c>.
        /// </returns>
        bool IsTransaction();
    }
}
