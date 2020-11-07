using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models;
using DbFacade.Factories;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary></summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal class DbCommandConfigParams<TDbParams> : Dictionary<string, IDbCommandParameterConfig<TDbParams>>,
        IDbCommandConfigParams<TDbParams>
        where TDbParams : DbParamsModel
    {
        private static string ReturnParamDefault = "DbFacade_DbCallReturn";

        public DbCommandParameterConfigFactory<TDbParams> Factory { get; private set; }

        public async Task AddAsync(string key, IDbCommandParameterConfig<TDbParams> value)
        {
            Add(key, value);
            await Task.CompletedTask;
        }
        public static DbCommandConfigParams<TDbParams> Create(Action<IDbCommandConfigParams<TDbParams>> parametersInitializer = null)
        {
            DbCommandConfigParams<TDbParams> dbParams = new DbCommandConfigParams<TDbParams>();
            dbParams.Factory = new DbCommandParameterConfigFactory<TDbParams>();
            Action<IDbCommandConfigParams<TDbParams>> _parametersInitializer =
                parametersInitializer != null ? parametersInitializer : p => { };
            _parametersInitializer(dbParams);
            dbParams.ResolveReturnValueParameter();            
            return dbParams;
        }
        public static async Task<DbCommandConfigParams<TDbParams>> CreateAsync(Func<IDbCommandConfigParams<TDbParams>, Task> parametersInitializer = null)
        {
            DbCommandConfigParams<TDbParams> dbParams = new DbCommandConfigParams<TDbParams>();
            dbParams.Factory = await DbCommandParameterConfigFactory<TDbParams>.CreateAsync();
            Func<IDbCommandConfigParams<TDbParams>, Task> _parametersInitializer =
                parametersInitializer != null ? parametersInitializer : async p => { await Task.CompletedTask; };
            await _parametersInitializer(dbParams);
            await dbParams.ResolveReturnValueParameterAsync();            
            return dbParams;
        }
        private void ResolveReturnValueParameter()
        {
            bool hasReturnValue = this.Any(
                item =>
                item.Value is IInternalDbCommandParameterConfig<TDbParams> paramConfig &&
                paramConfig.ParameterDirection == System.Data.ParameterDirection.ReturnValue
                );
            if (!hasReturnValue)
            {
                Add(ReturnParamDefault, DbCommandParameterConfig<TDbParams>.CreateReturnValue());
            }
        }
        private async Task ResolveReturnValueParameterAsync()
        {
            bool hasReturnValue = this.Any(
                item =>
                item.Value is IInternalDbCommandParameterConfig<TDbParams> paramConfig &&
                paramConfig.ParameterDirection == System.Data.ParameterDirection.ReturnValue
                );
            if (!hasReturnValue)
            {
                Add(ReturnParamDefault, await DbCommandParameterConfig<TDbParams>.CreateReturnValueAsync());
            }
            await Task.CompletedTask;
        }

        
    }
    internal class DbCommandConfigParams : DbCommandConfigParams<DbParamsModel> { }
}