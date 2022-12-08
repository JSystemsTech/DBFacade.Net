using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DbFacade.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ParameterConfigExtensions
    {
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="model">The model.</param>
        public static void AddParameter<TDbParams>(
            this IDbCommand dbCommand, 
            KeyValuePair<string, IDbCommandParameterConfig<TDbParams>> config,
            TDbParams model)
        {
            if (config.Value is IInternalDbCommandParameterConfig<TDbParams> paramConfig)
            {
                IDbDataParameter parameter = dbCommand.CreateParameter();
                if (parameter != null)
                {
                    parameter.Direction = paramConfig.ParameterDirection;
                    parameter.ParameterName = $"@{config.Key}";
                    parameter.DbType = paramConfig.DbType;
                    if (parameter.Direction != ParameterDirection.ReturnValue)
                    {
                        if(parameter is DbParameter dbParameter)
                        {
                            dbParameter.IsNullable = paramConfig.IsNullable;
                        }                        
                    }
                    if (parameter.Direction == ParameterDirection.Input)
                    {
                        parameter.DbType = paramConfig.DbType;
                        parameter.Value = paramConfig.Value(model);
                    }
                    else if(parameter.Direction == ParameterDirection.Output)
                    {
                        parameter.Size = paramConfig.OutputSize is int outputSize ? outputSize : int.MaxValue;
                    }
                }
                dbCommand.Parameters.Add(parameter);
            }
        }
        /// <summary>
        /// Adds the parameter asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="model">The model.</param>
        public static async Task AddParameterAsync<TDbParams>(
            this IDbCommand dbCommand,
            KeyValuePair<string, IDbCommandParameterConfig<TDbParams>> config,
            TDbParams model)
        {
            if (config.Value is IInternalDbCommandParameterConfig<TDbParams> paramConfig)
            {
                var parameter = dbCommand.CreateParameter();
                if (parameter != null)
                {
                    parameter.Direction = paramConfig.ParameterDirection;
                    parameter.ParameterName = $"@{config.Key}";
                    parameter.DbType = paramConfig.DbType;
                    if (parameter.Direction != ParameterDirection.ReturnValue)
                    {
                        if(parameter is DbParameter dbParameter)
                        {
                            dbParameter.IsNullable = paramConfig.IsNullable;
                        }                        
                    }
                    if (parameter.Direction == ParameterDirection.Input)                    {
                        
                        parameter.Value = await paramConfig.ValueAsync(model);
                    }
                    else if (parameter.Direction == ParameterDirection.Output)
                    {
                        parameter.Size = paramConfig.OutputSize is int outputSize ? outputSize : int.MaxValue;
                    }
                    dbCommand.Parameters.Add(parameter);
                }                
            }
            await Task.CompletedTask; 
        }


        /// <summary>
        /// Adds the parameters.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="model">The model.</param>
        public static void AddParams<TDbParams>(
            this IDbCommand dbCommand,
            IDbCommandConfigParams<TDbParams> dbParams,
            TDbParams model
           )
        {
            foreach (var config in dbParams)
            {
                dbCommand.AddParameter(config, model);
            }
        }
        /// <summary>
        /// Adds the parameters asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="model">The model.</param>
        public static async Task AddParamsAsync<TDbParams>(
            this IDbCommand dbCommand,
            IDbCommandConfigParams<TDbParams> dbParams,
            TDbParams model)
        {
            foreach (var config in dbParams)
            {
                await dbCommand.AddParameterAsync(config, model);
            }
        }


        /// <summary>
        /// Gets the return value.
        /// </summary>
        /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        public static int GetReturnValue(this IDbCommand dbCommand)
            => GetReturnValueParam(dbCommand) is IDbDataParameter parameter && parameter.Value is int value ? value : -1;

        /// <summary>
        /// Gets the return value parameter.
        /// </summary>
        /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        private static IDbDataParameter GetReturnValueParam(this IDbCommand dbCommand)
        {
            IDbDataParameter parameter = null;
            foreach (IDbDataParameter dbParameter in dbCommand.Parameters)
            {
                if (dbParameter.Direction == ParameterDirection.ReturnValue)
                {                    
                    parameter = dbParameter;
                    break;
                }
            }
            return parameter;
        }

        /// <summary>
        /// Gets the return value asynchronous.
        /// </summary>
        /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        public static async Task<int> GetReturnValueAsync(this IDbCommand dbCommand)
            => await GetReturnValueParamAsync(dbCommand) is IDbDataParameter parameter && parameter.Value is int value ? value : -1;

        /// <summary>
        /// Gets the return value parameter asynchronous.
        /// </summary>
        /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        private static async Task<IDbDataParameter> GetReturnValueParamAsync(this IDbCommand dbCommand)
        {
            IDbDataParameter parameter = dbCommand.GetReturnValueParam();
            await Task.CompletedTask;
            return parameter;
        }

        /// <summary>
        /// Gets the output parameters.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        private static IEnumerable<IDbDataParameter> GetOutputParams(this IDbCommand dbCommand)
        {
            List<IDbDataParameter> list = new List<IDbDataParameter>();
            foreach(IDbDataParameter parameter in dbCommand.Parameters)
            {
                if(parameter.Direction == ParameterDirection.Output)
                {
                    list.Add(parameter);
                }
            }
            return list;
        }
        /// <summary>
        /// Gets the output values.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        public static IDictionary<string, object> GetOutputValues(this IDbCommand dbCommand)
        {
            Dictionary<string, object> outputValues = new Dictionary<string, object>();
            foreach (IDbDataParameter outputParam in GetOutputParams(dbCommand))
            {
                string key = outputParam.ParameterName.Replace("@", string.Empty);
                outputValues.Add(key, outputParam.Value);
            }
            return outputValues;
        }
        /// <summary>
        /// Gets the output parameters asynchronous.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        private static async Task<IEnumerable<IDbDataParameter>> GetOutputParamsAsync(this IDbCommand dbCommand)
        {
            IEnumerable<IDbDataParameter> parameters = dbCommand.GetOutputParams();
            await Task.CompletedTask;
            return parameters;
        }

        /// <summary>
        /// Gets the output values asynchronous.
        /// </summary>
        /// <typeparam name="TDbCommand">The type of the database command.</typeparam>
        /// <param name="dbCommand">The database command.</param>
        /// <returns></returns>
        public static async Task<IDictionary<string, object>> GetOutputValuesAsync(this IDbCommand dbCommand)
        {
            Dictionary<string, object> outputValues = new Dictionary<string, object>();
            foreach (IDbDataParameter outputParam in await GetOutputParamsAsync(dbCommand))
            {
                string key = outputParam.ParameterName.Replace("@", string.Empty);
                outputValues.Add(key, outputParam.Value);
            }
            await Task.CompletedTask;
            return outputValues;
        }


        /// <summary>
        /// Gets the database command.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static IDbCommand GetDbCommand<TDbParams>(
            this IDbConnection dbConnection,
            DbCommandSettingsBase dbCommandSettings,
            IDbCommandConfigParams<TDbParams> dbParams,
            TDbParams model
            )
        {
            var dbCommand = dbConnection.CreateCommand();
            dbCommand.AddParams(dbParams, model);
            dbCommand.CommandText = dbCommandSettings.CommandText;
            dbCommand.CommandType = dbCommandSettings.CommandType;
            return dbCommand;
        }
        /// <summary>
        /// Gets the database command asynchronous.
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="dbParams">The database parameters.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static async Task<IDbCommand> GetDbCommandAsync<TDbParams>(
            this IDbConnection dbConnection,
            DbCommandSettingsBase dbCommandSettings,
            IDbCommandConfigParams<TDbParams> dbParams,
            TDbParams model
            )
        {
            var dbCommand = dbConnection.CreateCommand();
            await dbCommand.AddParamsAsync(dbParams, model);
            dbCommand.CommandText = dbCommandSettings.CommandText;
            dbCommand.CommandType = dbCommandSettings.CommandType;
            await Task.CompletedTask;
            return dbCommand;
        }
    }
}
