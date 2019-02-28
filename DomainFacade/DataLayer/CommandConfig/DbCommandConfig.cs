using DomainFacade.DataLayer.CommandConfig.Parameters;
using DomainFacade.DataLayer.ConnectionService;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using System.Data;

namespace DomainFacade.DataLayer.CommandConfig
{
    internal  class DbCommandConfig<TDbParams, TConnection> : DbCommandConfigCore<TDbParams, TConnection>
        where TDbParams : IDbParamsModel 
        where TConnection : DbConnectionConfig
    {
        public DbCommandConfig(DbCommandText<TConnection> dbCommand) : base(dbCommand) { }
        public DbCommandConfig(DbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams) : base(dbCommand, dbParams) {  }
        public DbCommandConfig(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams) : base(dbCommand, dbCommandType, dbParams) {  }
        public DbCommandConfig(DbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator) : base(dbCommand, dbParams, validator) {  }
        public DbCommandConfig(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, Validator<TDbParams> validator) : base(dbCommand, dbCommandType, dbParams, validator) { }

        public DbCommandConfig(DbCommandText<TConnection> dbCommand, string returnValue) : base(dbCommand, returnValue) { }
        public DbCommandConfig(DbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, string returnValue) : base(dbCommand, dbParams, returnValue) { }
        public DbCommandConfig(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue) : base(dbCommand, dbCommandType, dbParams, returnValue) {  }
        public DbCommandConfig(DbCommandText<TConnection> dbCommand, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator) : base(dbCommand, dbParams, returnValue, validator) {  }
        public DbCommandConfig(DbCommandText<TConnection> dbCommand, CommandType dbCommandType, IDbCommandConfigParams<TDbParams> dbParams, string returnValue, Validator<TDbParams> validator) : base(dbCommand, dbCommandType, dbParams, returnValue, validator) { }
        
        public DbCommandConfig<TDbParams, TConnection> AsTransaction()
        {
            Transaction = true;
            return this;
        }
    }
}
