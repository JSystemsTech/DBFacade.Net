using System.Data;
using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;

namespace DBFacade.DataLayer.CommandConfig
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
        
    }
}