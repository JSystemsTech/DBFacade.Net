using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.Exceptions;
using System;
using System.Data.Common;

namespace DomainFacade.DataLayer.CommandConfig
{
    /// <summary>
    /// 
    /// </summary>
    internal abstract class DbCommandConfigBase
    {
        /// <summary>
        /// Determines whether this instance is transaction.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is transaction; otherwise, <c>false</c>.
        /// </returns>
        public bool IsTransaction()
        {
            return Transaction;
        }
        /// <summary>
        /// The transaction
        /// </summary>
        internal bool Transaction = false;
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <returns></returns>
        public Con GetDbConnection<Con>()
            where Con : DbConnection
        {
            return GetDbConnectionCore<Con>();
        }
        /// <summary>
        /// Gets the database connection core.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <returns></returns>
        protected abstract Con GetDbConnectionCore<Con>() where Con : DbConnection;
        /// <summary>
        /// Determines whether [has stored procedure].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has stored procedure]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasStoredProcedure()
        {
            return HasStoredProcedureCore();
        }
        /// <summary>
        /// Determines whether [has stored procedure core].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has stored procedure core]; otherwise, <c>false</c>.
        /// </returns>
        protected abstract bool HasStoredProcedureCore();
        /// <summary>
        /// Gets the database command.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <typeparam name="Prm">The type of the rm.</typeparam>
        /// <param name="dbMethodParams">The database method parameters.</param>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        public Cmd GetDbCommand<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
            where Con : DbConnection
            where Cmd : DbCommand
            where Prm : DbParameter
        {
            return GetDbCommandCore<Con, Cmd, Prm>(dbMethodParams, dbConnection);
        }
        /// <summary>
        /// Gets the database command core.
        /// </summary>
        /// <typeparam name="Con">The type of the on.</typeparam>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <typeparam name="Prm">The type of the rm.</typeparam>
        /// <param name="dbMethodParams">The database method parameters.</param>
        /// <param name="dbConnection">The database connection.</param>
        /// <returns></returns>
        protected abstract Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
            where Con : DbConnection
            where Cmd : DbCommand
            where Prm : DbParameter;
        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        public object GetReturnValue<Cmd>(Cmd dbCommand)
            where Cmd : DbCommand
        {
            return GetReturnValueCore(dbCommand);
        }
        /// <summary>
        /// Gets the return value core.
        /// </summary>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        protected abstract object GetReturnValueCore<Cmd>(Cmd dbCommand) where Cmd : DbCommand;

        /// <summary>
        /// Sets the return value.
        /// </summary>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="value">The value.</param>
        public void SetReturnValue<Cmd>(Cmd dbCommand, object value)
            where Cmd : DbCommand
        {
            SetReturnValueCore(dbCommand, value);
        }
        /// <summary>
        /// Sets the return value core.
        /// </summary>
        /// <typeparam name="Cmd">The type of the md.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="value">The value.</param>
        protected abstract void SetReturnValueCore<Cmd>(Cmd dbCommand, object value) where Cmd : DbCommand;

        /// <summary>
        /// Gets the database connection configuration.
        /// </summary>
        /// <returns></returns>
        public IDbConnectionConfig GetDBConnectionConfig() { return GetDBConnectionConfigCore(); }
        /// <summary>
        /// Gets the database connection configuration core.
        /// </summary>
        /// <returns></returns>
        protected abstract IDbConnectionConfig GetDBConnectionConfigCore();

        /// <summary>
        /// Validates the specified parameters model.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        public IValidationResult Validate(IDbParamsModel paramsModel) { return ValidateCore(paramsModel); }
        /// <summary>
        /// Validates the core.
        /// </summary>
        /// <param name="paramsModel">The parameters model.</param>
        /// <returns></returns>
        protected abstract IValidationResult ValidateCore(IDbParamsModel paramsModel);

        /// <summary>
        /// Gets the missing stored procedure exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public MissingStoredProcedureException GetMissingStoredProcedureException(string message)
        {
            return GetMissingStoredProcedureExceptionCore(message);
        }
        /// <summary>
        /// Gets the missing stored procedure exception core.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        protected abstract MissingStoredProcedureException GetMissingStoredProcedureExceptionCore(string message);
        /// <summary>
        /// Gets the SQL execution exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public SQLExecutionException GetSQLExecutionException(string message, Exception e)
        {
            return GetSQLExecutionExceptionCore(message, e);
        }
        /// <summary>
        /// Gets the SQL execution exception core.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        protected abstract SQLExecutionException GetSQLExecutionExceptionCore(string message, Exception e);
    }
}
