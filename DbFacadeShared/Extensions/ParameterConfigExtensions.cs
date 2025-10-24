using DbFacade.DataLayer.CommandConfig;
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
        internal static void AddParameter<TDbParams>(
            this IDbCommand dbCommand,
            string key,
            DbCommandParameterConfig<TDbParams> paramConfig,
            TDbParams model)
        {
            IDbDataParameter parameter = dbCommand.CreateParameter();
            if (parameter != null)
            {
                parameter.Direction = paramConfig.ParameterDirection;
                parameter.ParameterName = $"@{key}";
                parameter.DbType = paramConfig.DbType;
                if (parameter.Direction != ParameterDirection.ReturnValue)
                {
                    if (parameter is DbParameter dbParameter)
                    {
                        dbParameter.IsNullable = paramConfig.IsNullable;
                    }
                }
                if (parameter.Direction == ParameterDirection.Input)
                {
                    parameter.DbType = paramConfig.DbType;
                    parameter.Value = paramConfig.Value(model);
                }
                else if (parameter.Direction == ParameterDirection.Output)
                {
                    parameter.Size = paramConfig.OutputSize is int outputSize ? outputSize : int.MaxValue;
                }
            }
            dbCommand.Parameters.Add(parameter);
        }

        internal static void AddParams<TDbParams>(
            this IDbCommand dbCommand,
            DbCommandConfigParams<TDbParams> dbParams,
            TDbParams model
           )
        {
            foreach (var config in dbParams.Parameters)
            {
                dbCommand.AddParameter(config.Key, config.Value, model);
            }
        }

        internal static int GetReturnValue(this IDbCommand dbCommand)
            => GetReturnValueParam(dbCommand) is IDbDataParameter parameter && parameter.Value is int value ? value : -1;
                
        internal static IDbDataParameter GetReturnValueParam(this IDbCommand dbCommand)
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

        internal static async Task<int> GetReturnValueAsync(this IDbCommand dbCommand)
            => await GetReturnValueParamAsync(dbCommand) is IDbDataParameter parameter && parameter.Value is int value ? value : -1;

        private static async Task<IDbDataParameter> GetReturnValueParamAsync(this IDbCommand dbCommand)
        {
            IDbDataParameter parameter = dbCommand.GetReturnValueParam();
            await Task.CompletedTask;
            return parameter;
        }

        private static IEnumerable<IDbDataParameter> GetOutputParams(this IDbCommand dbCommand)
        {
            List<IDbDataParameter> list = new List<IDbDataParameter>();
            foreach (IDbDataParameter parameter in dbCommand.Parameters)
            {
                if (parameter.Direction == ParameterDirection.Output)
                {
                    list.Add(parameter);
                }
            }
            return list;
        }
        internal static IDictionary<string, object> GetOutputValues(this IDbCommand dbCommand)
        {
            Dictionary<string, object> outputValues = new Dictionary<string, object>();
            foreach (IDbDataParameter outputParam in GetOutputParams(dbCommand))
            {
                string key = outputParam.ParameterName.Replace("@", string.Empty);
                outputValues.Add(key, outputParam.Value);
            }
            return outputValues;
        }
        private static async Task<IEnumerable<IDbDataParameter>> GetOutputParamsAsync(this IDbCommand dbCommand)
        {
            IEnumerable<IDbDataParameter> parameters = dbCommand.GetOutputParams();
            await Task.CompletedTask;
            return parameters;
        }

        internal static async Task<IDictionary<string, object>> GetOutputValuesAsync(this IDbCommand dbCommand)
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

        internal static IDbCommand GetDbCommand(
            this IDbConnection dbConnection,
            DbCommandMethod dbCommandMethod,
            object model
            )
        {
            var dbCommand = dbConnection.CreateCommand();
            dbCommandMethod.DbCommandSettings.AddParams(dbCommand, model);
            dbCommand.CommandText = dbCommandMethod.DbCommandSettings.BuildCommandText(model);
            dbCommand.CommandType = dbCommandMethod.DbCommandSettings.CommandType;
            return dbCommand;
        }
    }
}
