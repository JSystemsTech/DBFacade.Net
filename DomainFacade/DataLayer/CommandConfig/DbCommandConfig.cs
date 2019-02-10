using DomainFacade.DataLayer.CommandConfig.Parameters;
using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using System.Data;

namespace DomainFacade.DataLayer.CommandConfig
{
    internal class DbCommandConfig<T,C, TDbMethod> : DbCommandConfigCore<T, C, TDbMethod>
        where T : IDbParamsModel 
        where C : DbConnectionCore
        where TDbMethod : DbMethodCallType
    {
        public DbCommandConfig(DbCommandText<C> dbCommand) : base(dbCommand) { }
        public DbCommandConfig(DbCommandText<C> dbCommand, IDbCommandConfigParams<T> dbParams) : base(dbCommand, dbParams) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<T> dbParams) : base(dbCommand, dbCommandType, dbParams) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, IDbCommandConfigParams<T> dbParams, Validator<T> validator) : base(dbCommand, dbParams, validator) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<T> dbParams, Validator<T> validator) : base(dbCommand, dbCommandType, dbParams, validator) { }

        public DbCommandConfig(DbCommandText<C> dbCommand, string returnValue) : base(dbCommand, returnValue) { }
        public DbCommandConfig(DbCommandText<C> dbCommand, IDbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbParams, returnValue) { }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<T> dbParams, string returnValue) : base(dbCommand, dbCommandType, dbParams, returnValue) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, IDbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator) : base(dbCommand, dbParams, returnValue, validator) {  }
        public DbCommandConfig(DbCommandText<C> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<T> dbParams, string returnValue, Validator<T> validator) : base(dbCommand, dbCommandType, dbParams, returnValue, validator) { }
    }
}
