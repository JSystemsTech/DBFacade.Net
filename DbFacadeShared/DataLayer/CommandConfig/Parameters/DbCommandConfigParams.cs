using DbFacade.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    internal class DbCommandConfigParams<TDbParams>: IDbCommandConfigParams<TDbParams>
    {
        private static string ReturnParamDefault = "DbFacade_DbCallReturn";

        public DbCommandParameterConfigFactory<TDbParams> Factory => DbCommandParameterConfigFactory<TDbParams>.Instance;

        private readonly Action<IDbCommandConfigParams<TDbParams>> ParametersInitializer;

        internal readonly IDictionary<string, DbCommandParameterConfig<TDbParams>> Parameters;

        internal DbCommandConfigParams(Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null)
        {
            Parameters = new Dictionary<string, DbCommandParameterConfig<TDbParams>>();
            ParametersInitializer = parametersInitializer != null ? parametersInitializer : p => { };
            ParametersInitializer(this);
            ResolveReturnValueParameter();
        }
        private void ResolveReturnValueParameter()
        {
            bool hasReturnValue = Parameters.Any(item => item.Value.ParameterDirection == System.Data.ParameterDirection.ReturnValue);
            if (!hasReturnValue)
            {
                Parameters.Add(ReturnParamDefault, DbCommandParameterConfig<TDbParams>.CreateReturnValue());
            }
        }

        public void Add(string name, IDbCommandParameterConfig<TDbParams> value)
        { 
            if(value is DbCommandParameterConfig<TDbParams> config)
            {
                Parameters.Add(name, config);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    internal class DbCommandConfigParams : DbCommandConfigParams<object> { }
}