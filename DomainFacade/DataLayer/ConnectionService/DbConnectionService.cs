﻿using DomainFacade.DataLayer.CommandConfig;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Attributes;
using DomainFacade.Facade;
using DomainFacade.Utils;
using System;
using System.Linq;

namespace DomainFacade.DataLayer.ConnectionService
{
    
    public sealed class DbConnectionService: InstanceResolver<DbConnectionConfig>
    {        
        public static TConnection GetDbConnection<TConnection>()
            where TConnection : DbConnectionConfig
        {
            return GetInstance<TConnection>();
        }
        public static DbConnectionStoredProcedure[] GetAvailableStoredProcedured<TConnection>()
            where TConnection : DbConnectionConfig
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
            where TConnection : DbConnectionConfig
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


                protected override bool ValidateCore(IDbParamsModel paramsModel)
                {
                    return true;
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

                protected override DbConnectionConfigCore GetDBConnectionConfigCore()
                {
                    return DbConnectionService.GetDbConnection<TConnection>();
                }
            }
            private sealed class DbCommandConfigForDbConnectionSPParams : DbCommandConfigForDbConnection
            {
                protected override Cmd GetDbCommandCore<Con, Cmd, Prm>(IDbParamsModel dbMethodParams, Con dbConnection)
                {
                    Cmd dbCommand = (Cmd)dbConnection.CreateCommand();
                    SimpleDbParamsModel<string> nameParams =(SimpleDbParamsModel<string>) dbMethodParams;
                    dbCommand.CommandText = GetDBConnectionConfigCore().GetAvailableStoredProcAdditionalMetaCommandText(nameParams.Param1);
                    dbCommand.CommandType = GetDBConnectionConfigCore().GetAvailableStoredProcCommandType();
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
            where C : DbConnectionConfig
        {
            
            public DbConnectionStoredProcedure[] GetAvailableStoredPrcedures()
            {
                DbConnectionStoredProcedure[] data = Fetch<DbConnectionStoredProcedure, DbConnectionMetaMethods<C>.GetAvailableStoredProcs>().Results().ToArray();
                
                foreach (DbConnectionStoredProcedure sproc in data)
                {
                    sproc.SetParams(GetAvailableStoredPrcedureParamsMeta(sproc.Name));
                }
                return data;
            }
            private DbConnectionStoredProcedureParamMeta[] GetAvailableStoredPrcedureParamsMeta(string storedPRocName)
            {
                return Fetch<DbConnectionStoredProcedureParamMeta,SimpleDbParamsModel<string>, DbConnectionMetaMethods<C>.GetAvailableStoredProcsAdditionalMeta>(new SimpleDbParamsModel<string>(storedPRocName)).Results().ToArray();
            }
            
        }
    }
}
