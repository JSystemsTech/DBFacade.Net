using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using System.Data;

namespace DBFacade.DataLayer.CommandConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    /// <typeparam name="TConnection">The type of the connection.</typeparam>
    /// <seealso cref="CommandConfig.DbCommandConfigCore{TDbParams, TConnection}" />
    internal class DbCommandConfig<TDbParams, TConnection> : DbCommandConfigCore<TDbParams, TConnection>
        where TDbParams : IDbParamsModel
        where TConnection : IDbConnectionConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand) : base(dbCommand) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams) : base(dbCommand, dbParams) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams) : base(dbCommand, dbCommandType, dbParams) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="validator">The validator.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator) : base(dbCommand, dbParams, validator) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="validator">The validator.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator) : base(dbCommand, dbCommandType, dbParams, validator) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="returnValue">The return value.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, string returnValue) : base(dbCommand, returnValue) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnValue">The return value.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, string returnValue) : base(dbCommand, dbParams, returnValue) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnValue">The return value.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue) : base(dbCommand, dbCommandType, dbParams, returnValue) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="validator">The validator.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator) : base(dbCommand, dbParams, returnValue, validator) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandConfig{TDbParams, TConnection}"/> class.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbCommandType">Type of the database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="returnValue">The return value.</param>
        /// <param name="validator">The validator.</param>
        public DbCommandConfig(IDbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator) : base(dbCommand, dbCommandType, dbParams, returnValue, validator) { }

        /// <summary>
        /// Ases the transaction.
        /// </summary>
        /// <returns></returns>
        public DbCommandConfig<TDbParams, TConnection> AsTransaction()
        {
            Transaction = true;
            return this;
        }
    }
}
