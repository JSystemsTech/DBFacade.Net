using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;


namespace DbFacade.DataLayer.ConnectionService
{
    internal partial class DbConnectionManager
    {
        internal static IDbResponse ExecuteDbAction(DbCommandMethod commandConfig, object parameters)
        {            
            try
            {                
                using (IDbConnection dbConnection = commandConfig.GetConnection())
                {
                    TryOpenConnection(dbConnection);                    
                    return TryExecute(dbConnection, commandConfig, parameters);
                }
            }
            catch (Exception ex)
            {
                return HandleErrorResponse(ex, commandConfig, parameters);
            }
        }

        internal static IEnumerable<IDbResponse> ExecuteDbActions(
            IEnumerable<DbCommandMethod> configs,
            object parameters)
        {
            List<IDbResponse> responses = new List<IDbResponse>();

            var connectionGroups = configs.GroupBy(c => c.EndpointSettings.GetConnectionGroupKey());
            foreach (var configGroup in connectionGroups)
            {
                var config = configGroup.FirstOrDefault();
                try
                {
                    using (IDbConnection dbConnection = config.GetConnection())
                    {
                        TryOpenConnection(dbConnection);
                        foreach (var cfg in configs)
                        {
                            IDbResponse response = TryExecute(dbConnection, cfg, parameters);
                            responses.Add(response);
                        }
                    }
                }
                catch (Exception ex)
                {
                    responses.Add(HandleErrorResponse(ex, config, parameters));
                }
            }
            return responses;
        }
        private static IDbResponse TryExecute(
            IDbConnection dbConnection,
            DbCommandMethod config,
            object parameters)
        {
            try
            {
                return ProcessDbAction(dbConnection, config, parameters);
            }
            catch (Exception ex)
            {
                return HandleErrorResponse(ex, config, parameters);
            }
        }

        private static IDbResponse HandleErrorResponse(Exception ex, DbCommandMethod commandConfig, object parameters)
        {            
            EndpointErrorInfo info = new EndpointErrorInfo(ex, commandConfig.EndpointSettings, parameters);
            commandConfig.EndpointSettings.OnError(info);
            commandConfig.EndpointSettings.DbConnectionProvider.OnError(info);
            return DbResponse.Create(info);
        }
        private static void OnRollbackTransaction(IDbTransaction transaction)
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception exRollback)
            {
                ErrorHelper.ThrowOnRollbackTransactionError(exRollback);
            }            
        }
        private static IDbResponse WithTransaction(
            IDbConnection dbConnection, 
            DbCommandMethod config, 
            IDbCommand dbCommand, 
            Func<DbCommandMethod, IDbCommand, IDbResponse> execute,
            Action<Exception> throwErrorCallback)
        {
            IDbTransaction transaction = null;
            try
            {
                transaction = dbConnection is MockDbConnection mockConn ? mockConn.BeginTransaction(config.EndpointSettings.IsolationLevel) :
                    config.EndpointSettings.DbConnectionProvider.BeginTransaction(dbConnection, config.EndpointSettings.IsolationLevel);
            }
            catch (Exception ex)
            {
                ErrorHelper.ThrowOnCreateTransactionError(ex);
            }
            if (transaction != null)
            {
                try
                {
                    using (transaction)
                    {
                        dbCommand.Transaction = transaction;
                        var response = execute(config, dbCommand);
                        transaction.Commit();
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    OnRollbackTransaction(transaction);
                    throwErrorCallback(ex);
                }
            }
            else
            {
                ErrorHelper.ThrowInvalidTransactionError();
            }
            return null;
        }
        private static IDbResponse ExecuteNonQuery(
            DbCommandMethod config,
            IDbCommand dbCommand)
        {
            int returnValue = dbCommand is MockDbCommand mockCmd ? mockCmd.ExecuteNonQuery() :
                config.EndpointSettings.DbConnectionProvider.ExecuteNonQuery(dbCommand);
            return config.MakeDbResponse(returnValue);
        }

        private static IDbResponse ExecuteQuery(
            DbCommandMethod config,
            IDbCommand dbCommand)
        => config.MakeDbResponse(dbCommand);

        private static IDbResponse ExecuteScalar(
            DbCommandMethod config,
            IDbCommand dbCommand)
        {
            object returnObject = dbCommand is MockDbCommand mockCmd ? mockCmd.ExecuteScalar() :
                    config.EndpointSettings.DbConnectionProvider.ExecuteScalar(dbCommand);
            return config.MakeDbResponse(returnObject);
        }
        private static IDbResponse ExecuteXml(
            DbCommandMethod config,
            IDbCommand dbCommand)
        {
            var reader = dbCommand is MockDbCommand mockCmd ? mockCmd.ExecuteXmlReader() :
                    config.EndpointSettings.DbConnectionProvider.ExecuteXmlReader(dbCommand);

            return config.MakeDbResponse(dbCommand, reader);
        }        

        private static void TryOpenConnection(IDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                ErrorHelper.ThrowInvalidConnectionError();
            }
            try
            {
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                ErrorHelper.ThrowUnableToOpenConnectionError(ex);
            }
        }

        private static IDbResponse ProcessDbAction(
            IDbConnection dbConnection,
            DbCommandMethod config,
            object parameters)
        {
            config.Validate(parameters);
            using (var dbCommand = dbConnection.GetDbCommand(config, parameters))
            {
                config.EndpointSettings.OnBeforeExecute(dbCommand, parameters);
                bool canExecuteXml = dbCommand is MockDbCommand || config.EndpointSettings.DbConnectionProvider.CanExecuteXmlReader(dbCommand);

                Action<Exception> throwErrorCallback = ErrorHelper.ThrowOnExecuteNonQueryError;
                Func<DbCommandMethod, IDbCommand, IDbResponse> executeMethod = ExecuteNonQuery;

                if (config.EndpointSettings.ExecutionMethod == ExecutionMethod.Xml && canExecuteXml)
                {
                    throwErrorCallback = ErrorHelper.ThrowOnExecuteXmlError;
                    executeMethod = ExecuteXml;
                }
                else if (config.EndpointSettings.ExecutionMethod == ExecutionMethod.Query || config.EndpointSettings.ExecutionMethod == ExecutionMethod.Xml)
                {
                    throwErrorCallback = ErrorHelper.ThrowOnExecuteQueryError;
                    executeMethod = ExecuteQuery;
                }
                else if (config.EndpointSettings.ExecutionMethod == ExecutionMethod.Scalar)
                {
                    throwErrorCallback = ErrorHelper.ThrowOnExecuteScalarError;
                    executeMethod = ExecuteScalar;
                }

                if (config.EndpointSettings.IsTransaction)
                {
                    return WithTransaction(dbConnection, config, dbCommand, executeMethod, throwErrorCallback);
                }

                try
                {
                    return executeMethod(config, dbCommand);
                }
                catch (Exception ex)
                {
                    throwErrorCallback(ex);
                    return null;
                }
            }
        }
    }
}