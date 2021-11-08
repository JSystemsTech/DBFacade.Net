using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DbFacade.Extensions
{
    internal static class ParameterConfigExtensions
    {
        public static void AddParameter<TDbCommand, TDbParameter, TDbParams>(
            this TDbCommand dbCommand, 
            KeyValuePair<string, IDbCommandParameterConfig<TDbParams>> config,
            TDbParams model)
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
            where TDbParams : DbParamsModel
        {
            if (config.Value is IInternalDbCommandParameterConfig<TDbParams> paramConfig)
            {
                var parameter = dbCommand.CreateParameter() as TDbParameter;
                if (parameter != null)
                {
                    parameter.Direction = paramConfig.ParameterDirection;
                    parameter.ParameterName = $"@{config.Key}";
                    parameter.DbType = paramConfig.DbType;
                    if (parameter.Direction != ParameterDirection.ReturnValue)
                    {
                        parameter.IsNullable = paramConfig.IsNullable;
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
        public static async Task AddParameterAsync<TDbCommand, TDbParameter, TDbParams>(
            this TDbCommand dbCommand,
            KeyValuePair<string, IDbCommandParameterConfig<TDbParams>> config,
            TDbParams model)            
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
            where TDbParams : DbParamsModel
        {
            if (config.Value is IInternalDbCommandParameterConfig<TDbParams> paramConfig)
            {
                var parameter = dbCommand.CreateParameter() as TDbParameter;
                if (parameter != null)
                {
                    parameter.Direction = paramConfig.ParameterDirection;
                    parameter.ParameterName = $"@{config.Key}";
                    parameter.DbType = paramConfig.DbType;
                    if (parameter.Direction != ParameterDirection.ReturnValue)
                    {
                        parameter.IsNullable = paramConfig.IsNullable;
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


        public static void AddParams<TDbCommand, TDbParameter, TDbParams>(
            this TDbCommand dbCommand,
            IDbCommandConfigParams<TDbParams> dbParams,
            TDbParams model
           )
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
            where TDbParams : DbParamsModel
        {
            foreach (var config in dbParams)
            {
                dbCommand.AddParameter<TDbCommand, TDbParameter, TDbParams>(config, model);
            }
        }
        public static async Task AddParamsAsync<TDbCommand, TDbParameter, TDbParams>(
            this TDbCommand dbCommand,
            IDbCommandConfigParams<TDbParams> dbParams,
            TDbParams model)
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
            where TDbParams : DbParamsModel
        {
            foreach (var config in dbParams)
            {
                await dbCommand.AddParameterAsync<TDbCommand, TDbParameter, TDbParams>(config, model);
            }
        }


        public static int GetReturnValue<TDbCommand>(this TDbCommand dbCommand)
            where TDbCommand : DbCommand
            => GetReturnValueParam(dbCommand) is DbParameter parameter && parameter.Value is int value ? value : -1;

        private static DbParameter GetReturnValueParam<TDbCommand>(this TDbCommand dbCommand)
            where TDbCommand : DbCommand
        => dbCommand.Parameters.Cast<DbParameter>().Where(entry => entry.Direction == ParameterDirection.ReturnValue).FirstOrDefault();

        public static async Task<int> GetReturnValueAsync<TDbCommand>(this TDbCommand dbCommand)
            where TDbCommand : DbCommand
            => await GetReturnValueParamAsync(dbCommand) is DbParameter parameter && parameter.Value is int value ? value : -1;

        private static async Task<DbParameter> GetReturnValueParamAsync<TDbCommand>(this TDbCommand dbCommand)
            where TDbCommand : DbCommand
        {
            DbParameter parameter = dbCommand.Parameters.Cast<DbParameter>().Where(entry => entry.Direction == ParameterDirection.ReturnValue).FirstOrDefault();
            await Task.CompletedTask;
            return parameter;
        }

        private static IEnumerable<DbParameter> GetOutputParams<TDbCommand>(this TDbCommand dbCommand)
            where TDbCommand : DbCommand
        => dbCommand.Parameters.Cast<DbParameter>().Where(entry => entry.Direction == ParameterDirection.Output);
        public static IDictionary<string, object> GetOutputValues<TDbCommand>(this TDbCommand dbCommand)
            where TDbCommand : DbCommand
        {
            Dictionary<string, object> outputValues = new Dictionary<string, object>();
            foreach (DbParameter outputParam in GetOutputParams(dbCommand))
            {
                string key = outputParam.ParameterName.Replace("@", string.Empty);
                outputValues.Add(key, dbCommand.Parameters[outputParam.ParameterName].Value);
            }
            return outputValues;
        }
        private static async Task<IEnumerable<DbParameter>> GetOutputParamsAsync<TDbCommand>(this TDbCommand dbCommand)
            where TDbCommand : DbCommand
        {
            IEnumerable<DbParameter> parameters = dbCommand.Parameters.Cast<DbParameter>().Where(entry => entry.Direction == ParameterDirection.Output);
            await Task.CompletedTask;
            return parameters;
        }

        public static async Task<IDictionary<string, object>> GetOutputValuesAsync<TDbCommand>(this TDbCommand dbCommand)
            where TDbCommand : DbCommand
        {
            Dictionary<string, object> outputValues = new Dictionary<string, object>();
            foreach (DbParameter outputParam in await GetOutputParamsAsync(dbCommand))
            {
                string key = outputParam.ParameterName.Replace("@", string.Empty);
                outputValues.Add(key, dbCommand.Parameters[outputParam.ParameterName].Value);
            }
            await Task.CompletedTask;
            return outputValues;
        }


        public static TDbCommand GetDbCommand<TDbConnection, TDbCommand, TDbParameter, TDbParams>(
            this TDbConnection dbConnection,
            DbCommandSettingsBase dbCommandSettings,
            IDbCommandConfigParams<TDbParams> dbParams,
            TDbParams model
            )
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
            where TDbParams : DbParamsModel
        {
            var dbCommand = dbConnection.CreateCommand() as TDbCommand;
            dbCommand.AddParams<TDbCommand, TDbParameter, TDbParams>(dbParams, model);
            dbCommand.CommandText = dbCommandSettings.CommandText;
            dbCommand.CommandType = dbCommandSettings.CommandType;
            return dbCommand;
        }
        public static async Task<TDbCommand> GetDbCommandAsync<TDbConnection, TDbCommand, TDbParameter, TDbParams>(
            this TDbConnection dbConnection,
            DbCommandSettingsBase dbCommandSettings,
            IDbCommandConfigParams<TDbParams> dbParams,
            TDbParams model
            )
            where TDbConnection : DbConnection
            where TDbCommand : DbCommand
            where TDbParameter : DbParameter
            where TDbParams : DbParamsModel
        {
            var dbCommand = dbConnection.CreateCommand() as TDbCommand;
            await dbCommand.AddParamsAsync<TDbCommand, TDbParameter, TDbParams>(dbParams, model);
            dbCommand.CommandText = dbCommandSettings.CommandText;
            dbCommand.CommandType = dbCommandSettings.CommandType;
            await Task.CompletedTask;
            return dbCommand;
        }
    }
}
