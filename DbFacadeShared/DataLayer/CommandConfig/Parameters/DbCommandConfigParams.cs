using DbFacade.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    internal class DbCommandConfigParams<TDbParams> : Dictionary<string, IDbCommandParameterConfig<TDbParams>>,
        IDbCommandConfigParams<TDbParams>
    {
        /// <summary>
        /// The return parameter default
        /// </summary>
        private static string ReturnParamDefault = "DbFacade_DbCallReturn";

        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        public DbCommandParameterConfigFactory<TDbParams> Factory { get; private set; }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public async Task AddAsync(string key, IDbCommandParameterConfig<TDbParams> value)
        {
            Add(key, value);
            await Task.CompletedTask;
        }
        /// <summary>
        /// Creates the specified parameters initializer.
        /// </summary>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="parametersInitializer">The parameters initializer.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Resolves the return value parameter.
        /// </summary>
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
        /// <summary>
        /// Resolves the return value parameter asynchronous.
        /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
    internal class DbCommandConfigParams : DbCommandConfigParams<object> { }
}