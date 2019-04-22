using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Attributes;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.Exceptions;
using DBFacade.Facade;
using DBFacade.Utils;
using System;
using System.Linq;

namespace DBFacade.DataLayer.ConnectionService
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Utils.InstanceResolver{IDbConnectionConfig}" />
    public sealed class DbConnectionService : InstanceResolver<IDbConnectionConfig>
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <typeparam name="TConnection">The type of the connection.</typeparam>
        /// <returns></returns>
        public static TConnection GetDbConnection<TConnection>()
            where TConnection : IDbConnectionConfig
        {
            return GetInstance<TConnection>();
        }
        /// <summary>
        /// Gets the available stored procedured.
        /// </summary>
        /// <typeparam name="TConnection">The type of the connection.</typeparam>
        /// <returns></returns>
        public static DbConnectionStoredProcedure[] GetAvailableStoredProcedured<TConnection>()
            where TConnection : IDbConnectionConfig
        {
            TConnection Connection = GetDbConnection<TConnection>();
            if (Connection.AvailableStoredProcs() == null)
            {
                DbConnectionMetaDomainFacade<TConnection> META_FACADE = new DbConnectionMetaDomainFacade<TConnection>();
                Connection.SetAvailableStoredProcs(META_FACADE.GetAvailableStoredPrcedures());
            }
            return Connection.AvailableStoredProcs();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TConnection">The type of the connection.</typeparam>
        /// <seealso cref="DbManifest" />
        private abstract class DbConnectionMetaMethods<TConnection> : DbManifest
            where TConnection : IDbConnectionConfig
        {
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DbManifest" />
            public sealed class GetAvailableStoredProcs : DbConnectionMetaMethods<TConnection>
            {
                /// <summary>
                /// Gets the configuration core.
                /// </summary>
                /// <returns></returns>
                protected override IDbCommandConfig GetConfigCore()
                {
                    return new DbCommandConfigForDbConnection();
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <seealso cref="DbManifest" />
            private class DbCommandConfigForDbConnection : DbCommandConfigBase, IDbCommandConfig
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="DbCommandConfigForDbConnection"/> class.
                /// </summary>
                public DbCommandConfigForDbConnection() { }
                /// <summary>
                /// Gets the database connection core.
                /// </summary>
                /// <typeparam name="Con">The type of the on.</typeparam>
                /// <returns></returns>
                protected override Con GetDbConnectionCore<Con>()
                {
                    return (Con)GetDBConnectionConfigCore().GetDbConnection();
                }
                /// <summary>
                /// Determines whether [has stored procedure core].
                /// </summary>
                /// <returns>
                ///   <c>true</c> if [has stored procedure core]; otherwise, <c>false</c>.
                /// </returns>
                protected override bool HasStoredProcedureCore()
                {
                    return true;
                }

                /// <summary>
                /// Determines whether [has return value].
                /// </summary>
                /// <returns>
                ///   <c>true</c> if [has return value]; otherwise, <c>false</c>.
                /// </returns>
                public bool HasReturnValue()
                {
                    return false;
                }


                /// <summary>
                /// Validates the core.
                /// </summary>
                /// <param name="paramsModel">The parameters model.</param>
                /// <returns></returns>
                protected override IValidationResult ValidateCore(IDbParamsModel paramsModel)
                {
                    return ValidationResult.PassingValidation();
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
                protected override Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
                {
                    Cmd dbCommand = (Cmd)dbConnection.CreateCommand();
                    dbCommand.CommandText = GetDBConnectionConfigCore().GetAvailableStoredProcCommandText();
                    dbCommand.CommandType = GetDBConnectionConfigCore().GetAvailableStoredProcCommandType();
                    return dbCommand;
                }

                /// <summary>
                /// Gets the return value core.
                /// </summary>
                /// <typeparam name="Cmd">The type of the md.</typeparam>
                /// <param name="dbCommand">The database command.</param>
                /// <returns></returns>
                protected override object GetReturnValueCore<Cmd>(Cmd dbCommand)
                {
                    return null;
                }

                /// <summary>
                /// Sets the return value core.
                /// </summary>
                /// <typeparam name="Cmd">The type of the md.</typeparam>
                /// <param name="dbCommand">The database command.</param>
                /// <param name="value">The value.</param>
                protected override void SetReturnValueCore<Cmd>(Cmd dbCommand, object value)
                {
                    return;
                }

                /// <summary>
                /// Gets the database connection configuration core.
                /// </summary>
                /// <returns></returns>
                protected override IDbConnectionConfig GetDBConnectionConfigCore()
                {
                    return DbConnectionService.GetDbConnection<TConnection>();
                }

                /// <summary>
                /// Gets the missing stored procedure exception core.
                /// </summary>
                /// <param name="message">The message.</param>
                /// <returns></returns>
                /// <exception cref="MissingStoredProcedureException"></exception>
                protected override MissingStoredProcedureException GetMissingStoredProcedureExceptionCore(string message)
                {
                    throw new MissingStoredProcedureException();
                }

                /// <summary>
                /// Gets the SQL execution exception core.
                /// </summary>
                /// <param name="message">The message.</param>
                /// <param name="e">The e.</param>
                /// <returns></returns>
                /// <exception cref="SQLExecutionException"></exception>
                protected override SQLExecutionException GetSQLExecutionExceptionCore(string message, Exception e)
                {
                    throw new SQLExecutionException();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbDataModel" />
        public sealed class DbConnectionStoredProcedure : DbDataModel
        {
            /// <summary>
            /// Gets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            [DbColumn("StoredProcedureName")]
            public string Name { get; private set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TConnection">The type of the connection.</typeparam>
        /// <seealso cref="Facade.DBFacade{DbConnectionMetaMethods{TConnection}}" />
        private sealed class DbConnectionMetaDomainFacade<TConnection> : DomainFacade<DbConnectionMetaMethods<TConnection>>
            where TConnection : IDbConnectionConfig
        {
            /// <summary>
            /// Gets the available stored prcedures.
            /// </summary>
            /// <returns></returns>
            public DbConnectionStoredProcedure[] GetAvailableStoredPrcedures()
            {
                return Fetch<DbConnectionStoredProcedure, DbConnectionMetaMethods<TConnection>.GetAvailableStoredProcs>().Results().ToArray();
            }
        }
    }
}
