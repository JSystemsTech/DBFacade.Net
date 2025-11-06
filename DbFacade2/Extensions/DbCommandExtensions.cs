using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.DataLayer.Models.Parameters;
using System;
using System.Data;
using System.Data.Common;
using System.Xml;

namespace DbFacade.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static class DbCommandExtensions
    {     
        internal static int GetReturnValue(this IDbCommand dbCommand)
        {
            IDbDataParameter parameter = null;
            foreach (IDbDataParameter dbParameter in dbCommand.Parameters)
            {
                if (dbParameter.Direction == ParameterDirection.ReturnValue)
                {
                    parameter = dbParameter;
                    break;
                }
            }
            return parameter != null && parameter.Value is int value ? value : -1;
        }
        internal static void AddParameters(
            this IDbCommand dbCommand,
            ParameterDataCollection collection,
            EndpointSettings settings
           )
        {
            foreach (var info in collection.Collection.Values)
            {
                dbCommand.AddParameter(info, settings);
            }
        }
        internal static IDbCommand GetDbCommand<T>(
            this IDbConnection dbConnection,
            DbCommandMethod dbCommandMethod,
            T model
            )
        {
            var dbCommand = dbConnection is MockDbConnection conn ? conn.CreateDbCommand(dbCommandMethod) : dbConnection.CreateCommand();
            dbCommand.CommandType = dbCommandMethod.EndpointSettings.CommandType;
            dbCommand.CommandText = dbCommandMethod.EndpointSettings.BuildCommandText(model);
            dbCommandMethod.EndpointSettings.AddParams(dbCommand, model);
            if (dbCommandMethod.EndpointSettings.UseReturnValue)
            {                
                dbCommand.AddReturnParameter(dbCommandMethod.EndpointSettings);
            }
            return dbCommand;
        }
        private static void AddParameter(
            this IDbCommand dbCommand,
            ParameterInfo info,
            EndpointSettings settings
            )
        {
            IDbDataParameter parameter = dbCommand.CreateParameter();
            if (parameter != null)
            {
                parameter.Direction = info.Direction;
                parameter.ParameterName = settings.DbConnectionProvider.ResolveParameterName(info.Name, true);
                var dbTypeInfo = ResolveTypeValue(info, settings);
                parameter.DbType = dbTypeInfo.dbType;

                if (parameter.Direction != ParameterDirection.ReturnValue)
                {
                    if (parameter is DbParameter dbParameter)
                    {
                        dbParameter.IsNullable = info.IsNullable;
                    }
                }
                if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput)
                {
                    parameter.Value = dbTypeInfo.value;
                }
                if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.InputOutput)
                {
                    parameter.Size = info.Size is int outputSize ? outputSize : int.MaxValue;
                }
            }
            dbCommand.Parameters.Add(parameter);
        }
        private static (DbType dbType, object value) ResolveTypeValue(ParameterInfo info, EndpointSettings settings)
        {
            var dbTypeInfo = settings.DbConnectionProvider.DbTypeCollection[info.Type];
            bool isXml = info.Value is XmlReader;
            DbType dbType = isXml ? DbType.Xml : dbTypeInfo.DbType;
            object value = 
                info.Value == null ? DBNull.Value : 
                isXml ? info.Value : 
                dbTypeInfo.ValueResolver(info.Value);
            return (dbType, value);
        }
        private static readonly string ReturnParamDefault = "DbFacade_DbCallReturn";
        private static void AddReturnParameter(
            this IDbCommand dbCommand,
            EndpointSettings settings
            )
        {
            IDbDataParameter parameter = dbCommand.CreateParameter();
            if (parameter != null)
            {
                parameter.Direction = ParameterDirection.ReturnValue;
                parameter.ParameterName = settings.DbConnectionProvider.ResolveParameterName(ReturnParamDefault, true);
                var dbTypeInfo = settings.DbConnectionProvider.DbTypeCollection[typeof(int)];
                parameter.DbType = dbTypeInfo.DbType;
            }
            dbCommand.Parameters.Add(parameter);
        }
    }
}
