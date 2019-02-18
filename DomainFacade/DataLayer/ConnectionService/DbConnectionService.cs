using DomainFacade.DataLayer.CommandConfig;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Attributes;
using DomainFacade.Facade;
using DomainFacade.Utils;
using System;

namespace DomainFacade.DataLayer.ConnectionService
{
    
    public sealed class DbConnectionService: InstanceResolver<DbConnectionCore>
    {        
        public static TConnection GetDbConnection<TConnection>()
            where TConnection : DbConnectionCore
        {
            return GetInstance<TConnection>();
        }
        public static DbConnectionStoredProcedure[] GetAvailableStoredProcedured<TConnection>()
            where TConnection : DbConnectionCore
        {
            TConnection Connection = GetDbConnection<TConnection>();
            if(Connection.AvailableStoredProcs == null)
            {
                DbConnectionMetaDomainFacade<TConnection> META_FACADE = new DbConnectionMetaDomainFacade<TConnection>();
                Connection.SetAvailableStoredProcs(META_FACADE.GetAvailableStoredPrcedures());
            }
            return Connection.AvailableStoredProcs;
        }
        private abstract class DbConnectionMetaMethods<TConnection> : DbManifest
            where TConnection : DbConnectionCore
        {            
            public  sealed  class GetAvailableStoredProcs: DbConnectionMetaMethods<TConnection>
            {
                protected override IDbCommandConfig GetConfigCore()
                {
                    return new DbCommandConfigForDbConnection();
                }
            }
            public sealed class GetAvailableStoredProcsAdditionalMeta : DbConnectionMetaMethods<TConnection>
            {
                protected override IDbCommandConfig GetConfigCore()
                {
                    return new DbCommandConfigForDbConnectionSPParams();
                }
            }
            private class DbCommandConfigForDbConnection : DbCommandConfigBase, IDbCommandConfig
            {
                public override Type GetDbMethodCallType() { return typeof(DbMethodCallType.FetchRecords); }
                public DbCommandConfigForDbConnection() { }
                public TConnection GetDbConnection()
                {
                    return DbConnectionService.GetDbConnection<TConnection>();
                }
                protected override Con GetDbConnectionCore<Con>()
                {
                    return (Con)GetDbConnection().GetDbConnection();
                }
                protected override bool HasStoredProcedureCore()
                {
                    return true;
                }
                protected override Type GetDBConnectionTypeCore()
                {
                    return typeof(TConnection);
                }

                public bool HasReturnValue()
                {
                    return false;
                }


                protected override bool ValidateCore(IDbParamsModel paramsModel)
                {
                    return true;
                }

                protected override Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
                {
                    Cmd dbCommand = (Cmd)dbConnection.CreateCommand();
                    dbCommand.CommandText = GetDbConnection().GetAvailableStoredProcCommandText();
                    dbCommand.CommandType = GetDbConnection().GetAvailableStoredProcCommandType();
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
            }
            private sealed class DbCommandConfigForDbConnectionSPParams : DbCommandConfigForDbConnection
            {
                protected override Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
                {
                    Cmd dbCommand = (Cmd)dbConnection.CreateCommand();
                    SimpleDbParamsModel<string> nameParams =(SimpleDbParamsModel<string>) dbMethodParams;
                    dbCommand.CommandText = GetDbConnection().GetAvailableStoredProcAdditionalMetaCommandText(nameParams.Param1);
                    dbCommand.CommandType = GetDbConnection().GetAvailableStoredProcCommandType();
                    return dbCommand;
                }
            }
        }
        public sealed class DbConnectionStoredProcedure : DbDataModel
        {
            [DbColumn("StoredProcedureName")]
            public string Name { get; private set; }

            public DbConnectionStoredProcedureParamMeta[] Params { get; private set; }
            public void SetParams(DbConnectionStoredProcedureParamMeta[] spParams)
            {
                Params = spParams;
            }
        }
        public sealed class DbConnectionStoredProcedureParamMeta : DbDataModel
        {
            [DbColumn("name")]
            public string Name { get; private set; }
            [DbColumn("type")]
            public string Type { get; private set; }
            [DbFlagColumn("isRequired", 1)]
            public bool IsRequired { get; private set; }
        }
        private sealed class DbConnectionMetaDomainFacade<C> : DomainFacade<DbConnectionMetaMethods<C>>
            where C : DbConnectionCore
        {
            
            public DbConnectionStoredProcedure[] GetAvailableStoredPrcedures()
            {
                DbConnectionStoredProcedure[] data = FetchRecords<DbConnectionStoredProcedure, DbConnectionMetaMethods<C>.GetAvailableStoredProcs>().GetResponseAsArray();
                
                foreach (DbConnectionStoredProcedure sproc in data)
                {
                    sproc.SetParams(GetAvailableStoredPrcedureParamsMeta(sproc.Name));
                }
                return data;
            }
            private DbConnectionStoredProcedureParamMeta[] GetAvailableStoredPrcedureParamsMeta(string storedPRocName)
            {
                return FetchRecords<DbConnectionStoredProcedureParamMeta,SimpleDbParamsModel<string>, DbConnectionMetaMethods<C>.GetAvailableStoredProcsAdditionalMeta>(new SimpleDbParamsModel<string>(storedPRocName)).GetResponseAsArray();
            }
            
        }
    }
}
