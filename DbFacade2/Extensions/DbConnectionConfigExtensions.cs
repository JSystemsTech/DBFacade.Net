using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using System;

namespace DbFacade.Extensions
{
    public static class DbConnectionConfigExtensions
    {
        /// <summary>Creates the schema.</summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="schema">The schema.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static Schema CreateSchema(this DbConnectionConfig dbConnection, string schema)
            => new Schema(dbConnection, schema);
        /// <summary>Creates the method.</summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="name"></param>
        /// <param name="configure">The configure.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbCommandMethod DefineEndpoint(this DbConnectionConfig dbConnection, string name, Action<EndpointSettings> configure)
        {
            var settings = new EndpointSettings(dbConnection.DbConnectionProvider, name);
            configure(settings);
            return new DbCommandMethod(settings);
        }
        /// <summary>Creates the method.</summary>
        /// <param name="schema">The schema.</param>
        /// <param name="name"></param>
        /// <param name="configure">The configure.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbCommandMethod DefineEndpoint(this Schema schema, string name, Action<EndpointSettings> configure)
        {
            var settings = new EndpointSettings(schema, name);
            configure(settings);
            return new DbCommandMethod(settings);
        }

    }
}
