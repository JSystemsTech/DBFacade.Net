using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using System.Data;

namespace DBFacade.DataLayer.CommandConfig
{
    public sealed class DbCommandConfigBuilder
    {        
        public static IDbCommandConfig GetFetchConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<IDbParamsModel, TDbConnectionConfig>(dbCommandText, returnParam, isOutput);
        
        public static IDbCommandConfig GetFetchConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<IDbParamsModel, TDbConnectionConfig>(dbCommandText, dbCommandType, returnParam, isOutput);        

        public static IDbCommandConfig GetTransactionConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<IDbParamsModel, TDbConnectionConfig>(dbCommandText, returnParam, isOutput, true);
        
        public static IDbCommandConfig GetTransactionConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<IDbParamsModel, TDbConnectionConfig>(dbCommandText, dbCommandType, returnParam, isOutput, true);     
    }
    
    public class DbCommandConfigBuilder<TDbParams>
        where TDbParams : IDbParamsModel
    {  
        public static IDbCommandConfig GetFetchConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbParams, returnParam, isOutput);
        
        public static IDbCommandConfig GetFetchConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbParams, dbCommandType, returnParam, isOutput);
        
        public static IDbCommandConfig GetFetchConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbParams, validator, returnParam, isOutput);
        
        public static IDbCommandConfig GetFetchConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbParams,validator, dbCommandType, returnParam, isOutput);        

        public static IDbCommandConfig GetTransactionConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbParams, returnParam, isOutput, true);
        
        public static IDbCommandConfig GetTransactionConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
        where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbParams, dbCommandType, returnParam, isOutput, true);
        
        public static IDbCommandConfig GetTransactionConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbParams, validator, returnParam, isOutput, true);
        
        public static IDbCommandConfig GetTransactionConfig<TDbConnectionConfig>(IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
        where TDbConnectionConfig : IDbConnectionConfig
            => new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbParams, validator, dbCommandType, returnParam, isOutput, true);        
    }
}
