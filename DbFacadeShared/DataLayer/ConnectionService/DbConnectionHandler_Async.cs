using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using DbFacade.Extensions;

namespace DbFacade.DataLayer.ConnectionService
{
    internal partial class DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader>
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
        where TDbTransaction : DbTransaction
        where TDbDataReader : DbDataReader
    {
        private static async Task<IDbResponse<TDbDataModel>> BuildRepsonseAsync<TDbDataModel>(
            int returnValue, DbDataReader dbDataReader = null,
            IDictionary<string, object> outputValues = null)
            where TDbDataModel : DbDataModel
        {
            var responseObj = await DbResponseFactory<TDbDataModel>.CreateAsync(returnValue, outputValues);
            if (responseObj is List<TDbDataModel> _responseObj && dbDataReader != null)
            {
                while (await dbDataReader.ReadAsync())
                    _responseObj.Add(await DbDataModel.ToDbDataModelAsync<TDbDataModel>(dbDataReader));
                dbDataReader.Close();
            }
            return responseObj;
        }

        public static async Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbDataModel, TDbParams>(DbCommandMethod<TDbParams, TDbDataModel> config, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : DbParamsModel
        {
            var connectionConfig = config.DbConnectionConfig;
            using (var dbConnection = await connectionConfig.GetDbConnectionAsync(parameters) as TDbConnection)
            {
                if (dbConnection != null)
                {
                    await dbConnection.OpenAsync();
                    using (var dbCommand =
                        await dbConnection.GetDbCommandAsync<TDbConnection, TDbCommand, TDbParameter, TDbParams>(config.DbCommandText, config.DbParams, parameters))
                    {
                        try
                        {
                            if (config.DbCommandText.IsTransaction)
                                using (var transaction = dbConnection.BeginTransaction() as TDbTransaction)
                                {
                                    if (transaction != null)
                                    {
                                        try
                                        {
                                            dbCommand.Transaction = transaction;
                                            await dbCommand.ExecuteNonQueryAsync();
                                        }
                                        catch
                                        {
                                            transaction.Rollback();
                                            throw;
                                        }

                                        transaction.Commit();
                                        return await BuildRepsonseAsync<TDbDataModel>(
                                            await dbCommand.GetReturnValueAsync(),
                                            null,
                                            await dbCommand.GetOutputValuesAsync());
                                    }

                                    throw new FacadeException("Invalid Transaction Definition");
                                }

                            using (var dbDataReader = await dbCommand.ExecuteReaderAsync() as TDbDataReader)
                            {
                                return await BuildRepsonseAsync<TDbDataModel>(
                                    await dbCommand.GetReturnValueAsync(),
                                    dbDataReader,
                                    await dbCommand.GetOutputValuesAsync());
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            throw new SQLExecutionException("A SQL Error has occurred", config.DbCommandText,
                                sqlEx);
                        }
                        catch (Exception ex)
                        {
                            throw new FacadeException("Unknown Error", ex);
                        }
                    }
                }

                throw new FacadeException("Invalid Connection Definition");
            }

            throw new FacadeException("Invalid Config");
        }
    }
}