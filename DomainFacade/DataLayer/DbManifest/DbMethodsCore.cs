using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.DataLayer.DbManifest
{

    public abstract class DbMethodsCore : Enumeration
    {

        protected DbMethodsCore(int id) : base(id) { }      

        protected DbCommandConfig Config { get; private set; }
        public DbCommandConfig GetConfig()
        {
            if(Config == null)
            {
                Config = GetConfigCore();
            }
            return Config;
        }
        protected abstract DbCommandConfig GetConfigCore();
        public static DbCommandConfig<IDbParamsModel, Con> GetFetchRecordConfig<Con>(DbCommandText<Con> dbCommandText)
            where Con: DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con>.FetchRecordConfig(dbCommandText);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordConfig<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con>.FetchRecordConfig(dbCommandText, dbParams);
        }
        public static DbCommandConfig<IDbParamsModel, Con> GetFetchRecordConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con>.FetchRecordConfigWithReturn(dbCommandText, returnParam);
        }
        public static DbCommandConfig<Par, Con> GetFetchRecordConfigWithReturn<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams, string returnParam)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con>.FetchRecordConfigWithReturn(dbCommandText, dbParams, returnParam);
        }



        public DbCommandConfig<IDbParamsModel, Con> GetFetchRecordsConfig<Con>(DbCommandText<Con> dbCommandText)            
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con>.FetchRecordsConfig(dbCommandText);
        }
        public DbCommandConfig<Par, Con> GetFetchRecordsConfig<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con>.FetchRecordsConfig(dbCommandText, dbParams);
        }
        public DbCommandConfig<IDbParamsModel, Con> GetFetchRecordsConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con>.FetchRecordsConfigWithReturn(dbCommandText, returnParam);
        }
        public DbCommandConfig<Par, Con> GetFetchRecordsConfigWithReturn<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams, string returnParam)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con>.FetchRecordsConfigWithReturn(dbCommandText, dbParams, returnParam);
        }

        public DbCommandConfig<IDbParamsModel, Con> GetTransactionConfig<Con>(DbCommandText<Con> dbCommandText)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con>.TransactionConfig(dbCommandText);
        }
        public DbCommandConfig<Par, Con> GetTransactionConfig<Par, Con>(DbCommandText<Con> dbCommandText, DbCommandConfigParams<Par> dbParams)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con>.TransactionConfig(dbCommandText, dbParams);
        }
        public DbCommandConfig<IDbParamsModel, Con> GetTransactionConfigWithReturn<Con>(DbCommandText<Con> dbCommandText, string returnParam)
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<IDbParamsModel, Con>.TransactionConfigWithReturn(dbCommandText, returnParam);
        }
        public DbCommandConfig<Par, Con> GetTransactionConfigWithReturn<Par, Con>(DbCommandText<Con> dbCommandText, string returnParam, DbCommandConfigParams<Par> dbParams)
            where Par : IDbParamsModel
            where Con : DbConnectionCore
        {
            return new DbCommandConfig<Par, Con>.TransactionConfigWithReturn(dbCommandText, dbParams, returnParam);
        }
    }
}
