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
            IDbCommandText<TDbConnectionConfig> dbCommandText, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return FetchConfig(dbCommandText, DbCommandTypeDefault, returnParam, isOutput);
        }

        public static IDbCommandConfig FetchConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType, string returnParam = null,
            bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, TDbConnectionConfig>(dbCommandText, dbCommandType, returnParam,
                isOutput);
        }

        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return TransactionConfig(dbCommandText, DbCommandTypeDefault, returnParam, isOutput);
        }

        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType, string returnParam = null,
            bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return new DbCommandConfig<IDbParamsModel, TDbConnectionConfig>(dbCommandText, dbCommandType, returnParam,
                isOutput, true);
        }
    }

    public class DbCommandConfigFactory<TDbParams>
        where TDbParams : IDbParamsModel
    {
        private const CommandType DbCommandTypeDefault = CommandType.StoredProcedure;

        public static IDbCommandConfig FetchConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams,
            string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return FetchConfig(dbCommandText, DbCommandTypeDefault, dbParams, returnParam, isOutput);
        }

        public static IDbCommandConfig FetchConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbCommandType, dbParams,
                returnParam, isOutput);
        }

        public static IDbCommandConfig FetchConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams,
            Validator<TDbParams> validator, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return FetchConfig(dbCommandText, DbCommandTypeDefault, dbParams, validator, returnParam, isOutput);
        }

        public static IDbCommandConfig FetchConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null,
            bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbCommandType, dbParams,
                validator, returnParam, isOutput);
        }

        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams,
            string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return TransactionConfig(dbCommandText, DbCommandTypeDefault, dbParams, returnParam, isOutput);
        }

        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbCommandType, dbParams,
                returnParam, isOutput, true);
        }

        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams,
            Validator<TDbParams> validator, string returnParam = null, bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return TransactionConfig(dbCommandText, DbCommandTypeDefault, dbParams, validator, returnParam, isOutput);
        }

        public static IDbCommandConfig TransactionConfig<TDbConnectionConfig>(
            IDbCommandText<TDbConnectionConfig> dbCommandText, CommandType dbCommandType,
            IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator, string returnParam = null,
            bool isOutput = false)
            where TDbConnectionConfig : IDbConnectionConfig
        {
            return new DbCommandConfig<TDbParams, TDbConnectionConfig>(dbCommandText, dbCommandType, dbParams,
                validator, returnParam, isOutput, true);
        }
    }
}