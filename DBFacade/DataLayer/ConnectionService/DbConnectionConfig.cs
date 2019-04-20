using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;

namespace DBFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    internal interface ISQL { }
    /// <summary>
    /// 
    /// </summary>
    internal interface ISQLite { }
    /// <summary>
    /// 
    /// </summary>
    internal interface IOleDb { }
    /// <summary>
    /// 
    /// </summary>
    internal interface IOdbc { }
    /// <summary>
    /// 
    /// </summary>
    internal interface IOracle { }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TConnection">The type of the connection.</typeparam>
    /// <seealso cref="DbConnectionConfigCore" />
    public abstract class DbConnectionConfig<TConnection> : DbConnectionConfigCore where TConnection : IDbConnectionConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbConnectionConfigCore" />
        public interface IDbCommandText : IDbCommandText<TConnection> { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbConnectionConfigCore" />
        private class DbCommandText : DbCommandText<TConnection>, IDbCommandText
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DbCommandText"/> class.
            /// </summary>
            /// <param name="commandText">The command text.</param>
            /// <param name="label">The label.</param>
            public DbCommandText(string commandText, string label) : base(commandText, label) { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbConnection">The type of the database connection.</typeparam>
        /// <seealso cref="DbConnectionConfigCore" />
        public abstract class GenericBase<TDbConnection> : DbConnectionConfig<TConnection>
            where TDbConnection : DbConnection
        {
            /// <summary>
            /// Gets the database connection.
            /// </summary>
            /// <returns></returns>
            public override IDbConnection GetDbConnection()
            {
                return GetDbConnectionCore<TDbConnection>();
            }
        }
        /// <summary>
        /// Creates the command text.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public static IDbCommandText CreateCommandText(string commandText, string label) { return new DbCommandText(commandText, label); }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbConnectionConfigCore" />
        public abstract class SQL : GenericBase<SqlConnection>, ISQL { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbConnectionConfigCore" />
        public abstract class SQLite : GenericBase<SQLiteConnection>, ISQLite { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbConnectionConfigCore" />
        public abstract class OleDb : GenericBase<OleDbConnection>, IOleDb { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbConnectionConfigCore" />
        public abstract class Odbc : GenericBase<OdbcConnection>, IOdbc { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbConnectionConfigCore" />
        public abstract class Oracle : GenericBase<OracleConnection>, IOracle { }
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DbConnectionConfigCore" />
        public abstract class Generic : GenericBase<DbConnection> { }
    }

}
