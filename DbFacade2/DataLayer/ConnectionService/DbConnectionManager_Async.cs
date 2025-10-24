using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.ConnectionService.MockDb;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace DbFacade.DataLayer.ConnectionService
{
    internal partial class DbConnectionManager
    {       
        internal static async Task<IDbResponse> ExecuteDbActionAsync(DbCommandMethod commandConfig, object parameters, CancellationToken cancellationToken)
        {
            try
            {
                using (IDbConnection dbConnection = commandConfig.GetConnection())
                {
                    TryOpenConnection(dbConnection);
                    return await TryExecuteAsync(dbConnection, commandConfig, parameters, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                return await HandleErrorResponseAsync(ex, commandConfig, parameters);
            }
        }
        internal static async Task <IEnumerable<IDbResponse>> ExecuteDbActionsAsync(
            IEnumerable<DbCommandMethod> configs,
            object parameters, 
            CancellationToken cancellationToken)
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
                        var tasks = configs.Select(cfg => TryExecuteAsync(dbConnection, cfg, parameters, cancellationToken));
                        Task.WaitAll(tasks.ToArray(), cancellationToken);
                        foreach (var task in tasks)
                        {
                            responses.Add(task.Result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    responses.Add(HandleErrorResponse(ex, config, parameters));
                }
            }
            await Task.CompletedTask;
            return responses;
        }
        private static async Task<IDbResponse> TryExecuteAsync(
            IDbConnection dbConnection,
            DbCommandMethod config,
            object parameters,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                return await ProcessDbActionAsync(dbConnection, config, parameters, cancellationToken);
            }
            catch (Exception ex)
            {
                return HandleErrorResponse(ex, config, parameters);
            }
        }
        private static async Task<IDbResponse> HandleErrorResponseAsync(Exception ex, DbCommandMethod commandConfig, object parameters)
        {
            var response = HandleErrorResponse(ex, commandConfig, parameters);
            await Task.CompletedTask;
            return response;
        }
        private static async Task<IDbResponse> WithTransactionAsync(
            IDbConnection dbConnection,
            DbCommandMethod config,
            IDbCommand dbCommand,
            CancellationToken cancellationToken,
            Func<DbCommandMethod, IDbCommand, CancellationToken, Task<IDbResponse>> executeAsync,
            Action<Exception> throwErrorCallback)
        {
            IDbTransaction transaction = null;
            try
            {
                transaction = dbConnection is MockDbConnection mockConn ? await mockConn.BeginTransactionAsync(config.EndpointSettings.IsolationLevel, cancellationToken) :
                    await config.EndpointSettings.DbConnectionProvider.BeginTransactionAsync(dbConnection, cancellationToken, config.EndpointSettings.IsolationLevel);
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
                        var response = await executeAsync(config, dbCommand, cancellationToken);
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
        private static async Task<IDbResponse> ExecuteNonQueryAsync(
            DbCommandMethod config,
            IDbCommand dbCommand,
            CancellationToken cancellationToken)
        {
            int returnValue = dbCommand is MockDbCommand mockCmd ? await mockCmd.ExecuteNonQueryAsync() :
                await config.EndpointSettings.DbConnectionProvider.ExecuteNonQueryAsync(dbCommand, cancellationToken);
            return config.MakeDbResponse(returnValue);
        }

        private static async Task<IDbResponse> ExecuteQueryAsync(
            DbCommandMethod config,
            IDbCommand dbCommand, 
            CancellationToken cancellationToken)
        {
            using (var cancelRegistration = cancellationToken.Register(() => dbCommand.Cancel()))
            {
                var response = config.MakeDbResponse(dbCommand);
                await Task.CompletedTask;
                return response;
            }
        }
        private static async Task<IDbResponse> ExecuteScalarAsync(
            DbCommandMethod config,
            IDbCommand dbCommand,
            CancellationToken cancellationToken)
        {
            using (var cancelRegistration = cancellationToken.Register(() => dbCommand.Cancel()))
            {
                object returnObject = dbCommand is MockDbCommand mockCmd ? await mockCmd.ExecuteScalarAsync() :
                    await config.EndpointSettings.DbConnectionProvider.ExecuteScalarAsync(dbCommand, cancellationToken);
                var response = config.MakeDbResponse(returnObject);
                await Task.CompletedTask;
                return response;
            }
        }
        private static async Task<IDbResponse> ExecuteXmlAsync(
            DbCommandMethod config,
            IDbCommand dbCommand, 
            CancellationToken cancellationToken)
        {
            var reader = dbCommand is MockDbCommand mockCmd ? await mockCmd.ExecuteXmlReaderAsync() :
                    await config.EndpointSettings.DbConnectionProvider.ExecuteXmlReaderAsync(dbCommand, cancellationToken);

            var response = config.MakeDbResponse(dbCommand, reader);
            await Task.CompletedTask;
            return response;
        }

        private static async Task<IDbResponse> ProcessDbActionAsync(
            IDbConnection dbConnection,
            DbCommandMethod config,
            object parameters,
            CancellationToken cancellationToken)
        {
            config.Validate(parameters);
            using (var dbCommand = dbConnection.GetDbCommand(config, parameters))
            {
                await config.EndpointSettings.OnBeforeExecuteAsync(dbCommand, parameters, cancellationToken);
                bool canExecuteXml = dbCommand is MockDbCommand || config.EndpointSettings.DbConnectionProvider.CanExecuteXmlReader(dbCommand);
                Action<Exception> throwErrorCallback = ErrorHelper.ThrowOnExecuteNonQueryError;
                Func<DbCommandMethod, IDbCommand, CancellationToken, Task<IDbResponse>> executeMethodAsync = ExecuteNonQueryAsync;

                if (config.EndpointSettings.ExecutionMethod == ExecutionMethod.Xml && canExecuteXml)
                {
                    throwErrorCallback = ErrorHelper.ThrowOnExecuteXmlError;
                    executeMethodAsync = ExecuteXmlAsync;
                }
                else if (config.EndpointSettings.ExecutionMethod == ExecutionMethod.Query || config.EndpointSettings.ExecutionMethod == ExecutionMethod.Xml)
                {
                    throwErrorCallback = ErrorHelper.ThrowOnExecuteQueryError;
                    executeMethodAsync = ExecuteQueryAsync;
                }
                else if (config.EndpointSettings.ExecutionMethod == ExecutionMethod.Scalar)
                {
                    throwErrorCallback = ErrorHelper.ThrowOnExecuteScalarError;
                    executeMethodAsync = ExecuteScalarAsync;
                }

                if (config.EndpointSettings.IsTransaction)
                {
                    return await WithTransactionAsync(dbConnection, config, dbCommand, cancellationToken, executeMethodAsync, throwErrorCallback);
                }

                try
                {
                    return await executeMethodAsync(config, dbCommand, cancellationToken);
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