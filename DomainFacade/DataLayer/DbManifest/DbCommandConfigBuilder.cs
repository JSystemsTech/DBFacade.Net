using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;

namespace DomainFacade.DataLayer.DbManifest
{
    public sealed class DbCommandConfigBuilder
    {
        public static DbCommandConfig<IDbParamsModel, Con> GetFetchRecordConfig<Con>(DbCommandText<Con> dbCommandText)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.FetchRecord>(dbCommandText);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordConfig<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams)
        where Par : IDbParamsModel
        where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecord>(dbCommandText, dbParams);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordConfig<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams, Validator<Par> validator)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecord>(dbCommandText, dbParams, validator);
        }
        public static DbCommandConfig<IDbParamsModel, Con> GetFetchRecordConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.FetchRecordWithReturn>(dbCommandText, returnParam);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordConfigWithReturn<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams, string returnParam)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecordWithReturn>(dbCommandText, dbParams, returnParam);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordConfigWithReturn<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams, string returnParam, Validator<Par> validator)
        where Par : IDbParamsModel
        where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecordWithReturn>(dbCommandText, dbParams, returnParam, validator);
        }



        public static DbCommandConfig<IDbParamsModel, Con> GetFetchRecordsConfig<Con>(DbCommandText<Con> dbCommandText)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.FetchRecords>(dbCommandText);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordsConfig<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecords>(dbCommandText, dbParams);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordsConfig<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams, Validator<Par> validator)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecords>(dbCommandText, dbParams, validator);
        }
        public static DbCommandConfig<IDbParamsModel, Con> GetFetchRecordsConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.FetchRecordsWithReturn>(dbCommandText, returnParam);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordsConfigWithReturn<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams, string returnParam)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecordsWithReturn>(dbCommandText, dbParams, returnParam);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordsConfigWithReturn<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams, string returnParam, Validator<Par> validator)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.FetchRecordsWithReturn>(dbCommandText, dbParams, returnParam, validator);
        }

        public static DbCommandConfig<IDbParamsModel, Con> GetTransactionConfig<Con>(DbCommandText<Con> dbCommandText)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.Transaction>(dbCommandText);
        }
        public static DbCommandConfig<Par, Con> GetTransactionConfig<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.Transaction>(dbCommandText, dbParams);
        }
        public static DbCommandConfig<Par, Con> GetTransactionConfig<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams, Validator<Par> validator)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.Transaction>(dbCommandText, dbParams, validator);
        }
        public static DbCommandConfig<IDbParamsModel, Con> GetTransactionConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con, DbMethodCallType.TransactionWithReturn>(dbCommandText, returnParam);
        }
        public static DbCommandConfig<Par, Con> GetTransactionConfigWithReturn<Par, Con>(DbCommandText<Con> dbCommandText, string returnParam, DbCommandConfigParams<Par> dbParams)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.TransactionWithReturn>(dbCommandText, dbParams, returnParam);
        }
        public static DbCommandConfig<Par, Con> GetTransactionConfigWithReturn<Par, Con>(DbCommandText<Con> dbCommandText, string returnParam, DbCommandConfigParams<Par> dbParams, Validator<Par> validator)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con, DbMethodCallType.TransactionWithReturn>(dbCommandText, dbParams, returnParam, validator);
        }
    }
}
