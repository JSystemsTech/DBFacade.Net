using DBFacade.DataLayer.CommandConfig.Parameters;
using DBFacade.DataLayer.ConnectionService;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using System.Data;

namespace DBFacade.DataLayer.CommandConfig
{
    public sealed class DbCommandConfigBuilder
    {
        
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, string returnParam = null, bool isOutput = false)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText, returnParam, isOutput);
        }
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText, dbCommandType, returnParam, isOutput);
        }

        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, string returnParam = null, bool isOutput = false)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText, returnParam, isOutput, true);
        }
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
            where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, Con>(dbCommandText, dbCommandType, returnParam, isOutput, true);
        }
    }
    
    public class DbCommandConfigBuilder<TDbParams>
        where TDbParams : IDbParamsModel
    {
        

        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam, isOutput);
        }
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, dbCommandType, returnParam, isOutput);
        }
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null, bool isOutput = false)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, validator, returnParam, isOutput);
        }
        public static IDbCommandConfig GetFetchConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams,validator, dbCommandType, returnParam, isOutput);
        }

        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, returnParam, isOutput, true);
        }
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, dbCommandType, returnParam, isOutput, true);
        }
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null, bool isOutput = false)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, validator, returnParam, isOutput, true);
        }
        public static IDbCommandConfig GetTransactionConfig<Con>(IDbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, CommandType dbCommandType, string returnParam = null, bool isOutput = false)
        where Con : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, Con>(dbCommandText, dbParams, validator, dbCommandType, returnParam, isOutput, true);
        }        
    }
}
