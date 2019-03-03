using DomainFacade.DataLayer.CommandConfig;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Attributes;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.Exceptions;
using DomainFacade.Facade;
using DomainFacade.Utils;
using System;
using System.Linq;

namespace DomainFacade.DataLayer.ConnectionService
{

    public sealed class DbConnectionService: InstanceResolver<IDbConnectionConfig>
    {        
        public static TConnection GetDbConnection<TConnection>()
            where TConnection : IDbConnectionConfig
        {
            return GetInstance<TConnection>();
        }
        public static DbConnectionStoredProcedure[] GetAvailableStoredProcedured<TConnection>()
            where TConnection : IDbConnectionConfig
        {
            TConnection Connection = GetDbConnection<TConnection>();
            if(Connection.AvailableStoredProcs() == null)
            {
                DbConnectionMetaDomainFacade<TConnection> META_FACADE = new DbConnectionMetaDomainFacade<TConnection>();
                Connection.SetAvailableStoredProcs(META_FACADE.GetAvailableStoredPrcedures());
            }
            return Connection.AvailableStoredProcs();
        }
        private abstract class DbConnectionMetaMethods<TConnection> : DbManifest
            where TConnection : IDbConnectionConfig
        {            
            public  sealed  class GetAvailableStoredProcs: DbConnectionMetaMethods<TConnection>
            {
                protected override IDbCommandConfig GetConfigCore()
                {
                    return new DbCommandConfigForDbConnection();
                }
            }
            private class DbCommandConfigForDbConnection : DbCommandConfigBase, IDbCommandConfig
            {
                public DbCommandConfigForDbConnection() { }
                protected override Con GetDbConnectionCore<Con>()
                {
                    return (Con)GetDBConnectionConfigCore().GetDbConnection();
                }
                protected override bool HasStoredProcedureCore()
                {
                    return true;
                }

                public bool HasReturnValue()
                {
                    return false;
                }


                protected override IValidationResult ValidateCore(IDbParamsModel paramsModel)
                {
                    return ValidationResult.PassingValidation();
                }

                protected override Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
                {
                    Cmd dbCommand = (Cmd)dbConnection.CreateCommand();
                    dbCommand.CommandText = GetDBConnectionConfigCore().GetAvailableStoredProcCommandText();
                    dbCommand.CommandType = GetDBConnectionConfigCore().GetAvailableStoredProcCommandType();
                    return dbCommand;
                }

                protected override object GetReturnValueCore<Cmd>(Cmd dbCommand)
                {
                    return null;
                }

                protected override void SetReturnValueCore<Cmd>(Cmd dbCommand, object value)
                {
                    return;
                }

                protected override IDbConnectionConfig GetDBConnectionConfigCore()
                {
                    return DbConnectionService.GetDbConnection<TConnection>();
                }

                protected override MissingStoredProcedureException GetMissingStoredProcedureExceptionCore(string message)
                {
                    throw new MissingStoredProcedureException();
                }

                protected override SQLExecutionException GetSQLExecutionExceptionCore(string message, Exception e)
                {
                    throw new SQLExecutionException();
                }                
            }
        }
        public sealed class DbConnectionStoredProcedure : DbDataModel
        {
            [DbColumn("StoredProcedureName")]
            public string Name { get; private set; }
        }        
        
        private sealed class DbConnectionMetaDomainFacade<TConnection> : DomainFacade<DbConnectionMetaMethods<TConnection>>
            where TConnection : IDbConnectionConfig
        {            
            public DbConnectionStoredProcedure[] GetAvailableStoredPrcedures()
            {
                return Fetch<DbConnectionStoredProcedure, DbConnectionMetaMethods<TConnection>.GetAvailableStoredProcs>().Results().ToArray();      
            }            
        }
    }
}
