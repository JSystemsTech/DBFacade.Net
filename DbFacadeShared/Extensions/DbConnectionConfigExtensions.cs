using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models.Validators;
using DbFacade.Exceptions;
using DbFacade.Factories;
using System;
using System.Data;

namespace DbFacade.Extensions
{

    public class EndpointSettings
    {
        public string ConnectionStringId { get; set; }
        public string Label { get; internal set; }

        internal Func<object,string> BuildCommandText { get; set; }
        internal bool RequiresValidation { get; set; }
        internal CommandType CommandType { get; set; }
        internal bool IsTransaction { get; set; }
        internal Action<IDbCommand, object> AddParams { get; set; }
        internal IValidator Validator { get; set; }
        internal readonly Schema Schema;
        internal EndpointSettings(Schema schema = null)
        {
            Schema = schema;
            AddParams = (dbCommand, model) => { };
        }
    }

    public static class EndpointSettingsExtensions
    {
        public static EndpointSettings AsStoredProcedure(this EndpointSettings settings, string commandText)
        {
            settings.CommandType = CommandType.StoredProcedure;
            string text = settings.Schema != null ? settings.Schema.BuildCommandText(commandText) : commandText;
            settings.BuildCommandText = (model) => text;
            return settings;
        }
        public static EndpointSettings AsText<T>(this EndpointSettings settings, Func<T, string> builder)
        {
            settings.CommandType = CommandType.Text;
            settings.WithCommandText(builder);
            return settings;
        }
        public static EndpointSettings AsTableDirect(this EndpointSettings settings)
        {
            settings.CommandType = CommandType.TableDirect;
            return settings;
        }
        public static EndpointSettings AsTransaction(this EndpointSettings settings)
        {
            settings.IsTransaction = true;
            return settings;
        }
        public static EndpointSettings WithLabel(this EndpointSettings settings, string label)
        {
            settings.Label = label;
            return settings;
        }
        private static void WithCommandText<T>(this EndpointSettings settings, Func<T, string> builder)
        {
            settings.BuildCommandText = (paramsModel) => {
                if (paramsModel is T model)
                {;
                    return builder(model);
                }
                else
                {
                    throw new FacadeException($"Unable to add command text: expected model of type {typeof(T).TypeName()} but got {paramsModel.TypeName()}");
                }
            };
        }
        public static EndpointSettings WithParameters(this EndpointSettings settings, Action<IDbCommandConfigParams<object>> parametersBuilder)
        {
            var dbParams = new DbCommandConfigParams<object>(parametersBuilder);
            settings.AddParams = (dbCommand, paramsModel) => {
                foreach (var config in dbParams.Parameters)
                {
                    dbCommand.AddParameter(config.Key, config.Value, paramsModel);
                }
            };
            return settings;
        }
        public static EndpointSettings WithParameters<T>(this EndpointSettings settings, Action<IDbCommandConfigParams<T>> parametersBuilder)
        {
            var dbParams = new DbCommandConfigParams<T>(parametersBuilder);
            settings.AddParams = (dbCommand, paramsModel) => ResolveParams(dbCommand, dbParams, paramsModel);
            return settings;
        }
        public static EndpointSettings WithValidation<T>(this EndpointSettings settings, Action<IValidator<T>> validatorInitializer)
        where T : class
        {
            var validator = ValidatorFactory.Create(validatorInitializer);
            settings.Validator = validator;
            settings.RequiresValidation = true;
            return settings;
        }

        private static void ResolveParams<T>(
            IDbCommand dbCommand,
            DbCommandConfigParams<T> dbParams,
            object paramsModel
           )
        {
            if (paramsModel is T model)
            {
                foreach (var config in dbParams.Parameters)
                {
                    dbCommand.AddParameter(config.Key, config.Value, model);
                }
            }
            else
            {
                throw new FacadeException($"Unable to add parameters: expected model of type {typeof(T).TypeName()} but got {paramsModel.TypeName()}");
            }
        }
    }


    public static class DbConnectionConfigExtensions
    {

        /// <summary>Creates the schema factory.</summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="schema">The schema.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static Schema CreateSchemaFactory(this DbConnectionConfig dbConnection, string schema)
            => new Schema(dbConnection, schema);
        /// <summary>Creates the method.</summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="configure">The configure.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbCommandMethod CreateMethod(this DbConnectionConfig dbConnection, Action<EndpointSettings> configure)
        {
            var settings = new EndpointSettings();
            configure(settings);
            return new DbCommandMethod(dbConnection.DbConnection, settings);
        }
        /// <summary>Creates the method.</summary>
        /// <param name="schema">The schema.</param>
        /// <param name="configure">The configure.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IDbCommandMethod CreateMethod(this Schema schema, Action<EndpointSettings> configure)
        {
            var settings = new EndpointSettings(schema);
            configure(settings);
            return new DbCommandMethod(schema.DbConnection, settings);
        }        
    }
}
