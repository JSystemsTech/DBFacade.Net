using System.Data;
using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig.Parameters;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Validators;

namespace DbFacade.DataLayer.CommandConfig
{
    public sealed class DbCommandConfigFactory
    {
        private const CommandType DbCommandTypeDefault = CommandType.StoredProcedure;

       
        public static IDbCommandConfig FetchConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText,
            IDbCommandConfigParams<DbParamsModel> dbParams=null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        =>  FetchConfig(dbCommandText, DbCommandTypeDefault, dbParams, returnParam);
        
        public static IDbCommandConfig FetchConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<DbParamsModel> dbParams = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        =>  DbCommandConfig<DbParamsModel, TDbConnectionConfig>.FetchConfig(dbCommandText, dbCommandType, dbParams, returnParam);
        
        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText,
            IDbCommandConfigParams<DbParamsModel> dbParams = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        =>  TransactionConfig(dbCommandText, DbCommandTypeDefault, dbParams, returnParam);
        
        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<DbParamsModel> dbParams = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        =>  DbCommandConfig<DbParamsModel, TDbConnectionConfig>.TransactionConfig(dbCommandText, dbCommandType, dbParams, returnParam);

        #region Async Methods
        public static async Task<IDbCommandConfig> FetchConfigAsync<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText,
            IDbCommandConfigParams<DbParamsModel> dbParams = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => await FetchConfigAsync(dbCommandText, DbCommandTypeDefault, dbParams, returnParam);

        public static async Task<IDbCommandConfig> FetchConfigAsync<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<DbParamsModel> dbParams = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => await DbCommandConfig<DbParamsModel, TDbConnectionConfig>.FetchConfigAsync(dbCommandText, dbCommandType, dbParams, returnParam);

        public static async Task<IDbCommandConfig> TransactionConfigAsync<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText,
            IDbCommandConfigParams<DbParamsModel> dbParams = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => await TransactionConfigAsync(dbCommandText, DbCommandTypeDefault, dbParams, returnParam);

        public static async Task<IDbCommandConfig> TransactionConfigAsync<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<DbParamsModel> dbParams = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => await DbCommandConfig<DbParamsModel, TDbConnectionConfig>.TransactionConfigAsync(dbCommandText, dbCommandType, dbParams, returnParam);
        #endregion

    }
    public class DbCommandConfigFactory<TDbParams>
        where TDbParams : IDbParamsModel
    {
        private const CommandType DbCommandTypeDefault = CommandType.StoredProcedure;

        
        public static IDbCommandConfig FetchConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        =>  FetchConfig(dbCommandText, DbCommandTypeDefault, dbParams,validator, returnParam);
        
        public static IDbCommandConfig FetchConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        =>  DbCommandConfig<TDbParams, TDbConnectionConfig>.FetchConfig(dbCommandText, dbCommandType, dbParams,
                validator,returnParam);
        

        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams,
            Validator<TDbParams> validator=null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => TransactionConfig(dbCommandText, DbCommandTypeDefault, dbParams, validator, returnParam);
        

        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => DbCommandConfig<TDbParams, TDbConnectionConfig>.TransactionConfig(dbCommandText, dbCommandType, dbParams,
                validator, returnParam);


        #region Async Methods
        public static async Task<IDbCommandConfig> FetchConfigAsync<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => await FetchConfigAsync(dbCommandText, DbCommandTypeDefault, dbParams, validator, returnParam);

        public static async Task<IDbCommandConfig> FetchConfigAsync<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => await DbCommandConfig<TDbParams, TDbConnectionConfig>.FetchConfigAsync(dbCommandText, dbCommandType, dbParams,
                validator, returnParam);


        public static async Task<IDbCommandConfig> TransactionConfigAsync<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams,
            Validator<TDbParams> validator = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => await TransactionConfigAsync(dbCommandText, DbCommandTypeDefault, dbParams, validator, returnParam);


        public static async Task<IDbCommandConfig> TransactionConfigAsync<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator = null, string returnParam = null)
            where TDbConnectionConfig : IDbConnectionConfig
        => await DbCommandConfig<TDbParams, TDbConnectionConfig>.TransactionConfigAsync(dbCommandText, dbCommandType, dbParams,
                validator, returnParam);
        #endregion

    }
}