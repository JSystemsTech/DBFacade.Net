using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Attributes;
using DomainFacade.Facade;
using DomainFacade.Utils;
using System;
using System.Collections.Generic;
using static DomainFacade.DataLayer.Models.DbResponse;

namespace DomainFacade.DataLayer
{
    public class InstanceResolver<T>
    {
        public static Dictionary<Type, T> Instances = new Dictionary<Type, T>();
        public static C GetInstance<C>()
            where C : T
        {
            if (!Instances.ContainsKey(typeof(C)))
            {
                Instances.Add(typeof(C), GenericInstance<C>.GetInstance());
            }
            return (C)Instances[typeof(C)];
        }
    }
    public sealed class DbConnectionService: InstanceResolver<DbConnectionCore>
    {        
        public static C GetDbConnection<C>()
            where C : DbConnectionCore
        {
            return GetInstance<C>();
        }
        public static DbConnectionStoredProcedure[] GetAvailableStoredProcedured<C>()
            where C : DbConnectionCore
        {
            C Connection = GetDbConnection<C>();
            if(Connection.AvailableStoredProcs == null)
            {
                DbConnectionMetaDomainFacade<C> META_FACADE = new DbConnectionMetaDomainFacade<C>();
                Connection.SetAvailableStoredProcs(META_FACADE.GetAvailableStoredPrcedures());
            }
            return Connection.AvailableStoredProcs;
        }
        private abstract class DbConnectionMetaMethods<C> : DbMethodsCore
            where C : DbConnectionCore
        {            
            public  sealed  class GetAvailableStoredProcs: DbConnectionMetaMethods<C>
            {
                protected override DbCommandConfig GetConfigCore()
                {
                    return new DbCommandConfigForDbConnection();
                }
            }
            public sealed class GetAvailableStoredProcsAdditionalMeta : DbConnectionMetaMethods<C>
            {
                protected override DbCommandConfig GetConfigCore()
                {
                    return new DbCommandConfigForDbConnectionSPParams();
                }
            }
            private class DbCommandConfigForDbConnection : DbCommandConfig
            {
                
                public DbCommandConfigForDbConnection()
                {
                    SetDbMethod(DbMethodType.FetchRecords);
                }
                public C GetDbConnection()
                {
                    return DbConnectionService.GetDbConnection<C>();
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
                    return typeof(C);
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
