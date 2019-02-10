using DomainFacade.DataLayer.CommandConfig.Parameters;
using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.DbManifest;
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


    public class DbCommandConfigBuilder<Par> 
        where Par: IDbParamsModel
    {
        
        public static IDbCommandConfig GetFetchRecordConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams)
        where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecord>(dbCommandText, dbParams);
        }
        public static IDbCommandConfig GetFetchRecordConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams, Validator<Par> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecord>(dbCommandText, dbParams, validator);
        }
        public static IDbCommandConfig GetFetchRecordConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecordWithReturn>(dbCommandText, dbParams, returnParam);
        }
        public static IDbCommandConfig GetFetchRecordConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams, string returnParam, Validator<Par> validator)
        where Con : DbConnectionCore            
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecordWithReturn>(dbCommandText, dbParams, returnParam, validator);
        }
        public static IDbCommandConfig GetFetchRecordsConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecords>(dbCommandText, dbParams);
        }
        public static IDbCommandConfig GetFetchRecordsConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams, Validator<Par> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecords>(dbCommandText, dbParams, validator);
        }
        
        public static IDbCommandConfig GetFetchRecordsConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecordsWithReturn>(dbCommandText, dbParams, returnParam);
        }
        public static IDbCommandConfig GetFetchRecordsConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams, string returnParam, Validator<Par> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecordsWithReturn>(dbCommandText, dbParams, returnParam, validator);
        }
        
        public static IDbCommandConfig GetTransactionConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.Transaction>(dbCommandText, dbParams);
        }
        public static IDbCommandConfig GetTransactionConfig<Con>(DbCommandText<Con> dbCommandText, IDbCommandConfigParams<Par> dbParams, Validator<Par> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.Transaction>(dbCommandText, dbParams, validator);
        }
        
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam, IDbCommandConfigParams<Par> dbParams)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.TransactionWithReturn>(dbCommandText, dbParams, returnParam);
        }
        public static IDbCommandConfig GetTransactionConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam, IDbCommandConfigParams<Par> dbParams, Validator<Par> validator)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.TransactionWithReturn>(dbCommandText, dbParams, returnParam, validator);
        }
    }
}
