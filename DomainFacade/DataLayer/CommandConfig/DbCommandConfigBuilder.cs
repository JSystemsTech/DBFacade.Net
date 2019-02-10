using DomainFacade.DataLayer.CommandConfig.Parameters;
using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;

namespace DomainFacade.DataLayer.CommandConfig
{
    public sealed class DbCommandConfigBuilder
    {
        public static IDbCommandConfig GetFetchRecordConfig<Con>(DbCommandText<Con> dbCommandText)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.FetchRecord>(dbCommandText);
        }
        public static IDbCommandConfig GetFetchRecordConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.FetchRecordWithReturn>(dbCommandText, returnParam);
        }
        public static IDbCommandConfig GetFetchRecordsConfig<Con>(DbCommandText<Con> dbCommandText)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.FetchRecords>(dbCommandText);
        }
        public static IDbCommandConfig GetFetchRecordsConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.FetchRecordsWithReturn>(dbCommandText, returnParam);
        }
        public static IDbCommandConfig GetTransactionConfig<Con>(DbCommandText<Con> dbCommandText)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.Transaction>(dbCommandText);
        }
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.TransactionWithReturn>(dbCommandText, returnParam);
        }
    }


    public class DbCommandConfigBuilder<TDbParams> 
        where TDbParams : IDbParamsModel
    {
        
        public static IDbCommandConfig GetFetchRecordConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams)
        where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.FetchRecord>(dbCommandText, dbParams);
        }
        public static IDbCommandConfig GetFetchRecordConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.FetchRecord>(dbCommandText, dbParams, validator);
        }
        public static IDbCommandConfig GetFetchRecordConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.FetchRecordWithReturn>(dbCommandText, dbParams, returnParam);
        }
        public static IDbCommandConfig GetFetchRecordConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam, Validator<TDbParams> validator)
        where Con : DbConnectionCore            
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.FetchRecordWithReturn>(dbCommandText, dbParams, returnParam, validator);
        }
        public static IDbCommandConfig GetFetchRecordsConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.FetchRecords>(dbCommandText, dbParams);
        }
        public static IDbCommandConfig GetFetchRecordsConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.FetchRecords>(dbCommandText, dbParams, validator);
        }
        
        public static IDbCommandConfig GetFetchRecordsConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.FetchRecordsWithReturn>(dbCommandText, dbParams, returnParam);
        }
        public static IDbCommandConfig GetFetchRecordsConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, string returnParam, Validator<TDbParams> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.FetchRecordsWithReturn>(dbCommandText, dbParams, returnParam, validator);
        }
        
        public static IDbCommandConfig GetTransactionConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.Transaction>(dbCommandText, dbParams);
        }
        public static IDbCommandConfig GetTransactionConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.Transaction>(dbCommandText, dbParams, validator);
        }
        
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam, IDbCommandConfigParams<TDbParams> dbParams)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.TransactionWithReturn>(dbCommandText, dbParams, returnParam);
        }
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<TDbParams, Con, DbMethodCallType.TransactionWithReturn>(dbCommandText, dbParams, returnParam, validator);
        }
    }
}
