using System;
using System.Data.Common;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.CommandConfig;
using System.Text;
using System.Data.SqlClient;
using DBFacade.Exceptions;

namespace DBFacade.DataLayer.ConnectionService
{
    
    internal class DbConnectionHandler<TDbConnection, TDbCommand, TDbParameter, TDbTransaction, TDbDataReader, TDbManifest>        
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
        where TDbParameter : DbParameter
        where TDbTransaction : DbTransaction
        where TDbDataReader : DbDataReader
        where TDbManifest : DbManifest
    {
        public static IDbResponse<TDbDataModel> ExecuteDbAction<TDbDataModel, TDbParams, DbMethod>(DbMethod dbMethod, TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            IDbCommandConfig config = dbMethod.GetConfig();
            string paramsType = parameters.GetType().Name;
            string MethodType = dbMethod.GetType().Name;
            StringBuilder builder = new StringBuilder();
            bool rollback = false;

            TDbCommand dbCommand = null;
            TDbTransaction transaction = null;
            TDbDataReader dbDataReader = null;
            IDbResponse<TDbDataModel> response = null;
            if (parameters._GetRunMode() == MethodRunMode.Test)
            {
                DbDataReader data = parameters._GetResponseData() as DbDataReader;
                response = new DbResponse<DbMethod, TDbDataModel>(data, parameters._GetReturnValue());
                if (data != null) data.Close();
            }
            else
            {
                using (TDbConnection dbConnection = config.GetDBConnectionConfig().GetDbConnection() as TDbConnection)
                {
                    dbCommand = config.GetDbCommand<TDbConnection, TDbCommand, TDbParameter>(parameters, dbConnection);
                    try
                    {
                        dbConnection.Open();
                        if (config.IsTransaction())
                        {
                            transaction = dbConnection.BeginTransaction() as TDbTransaction;
                            dbCommand.Transaction = transaction;
                            dbCommand.ExecuteNonQuery();
                            transaction.Commit();

                            response = new DbResponse<DbMethod, TDbDataModel>(dbMethod.GetConfig().GetReturnValue(dbCommand));
                        }
                        else
                        {
                            dbDataReader = (TDbDataReader)dbCommand.ExecuteReader();
                            response = new DbResponse<DbMethod, TDbDataModel>(dbDataReader, dbMethod.GetConfig().GetReturnValue(dbCommand));
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        rollback = true;
                        throw new SQLExecutionException("A SQL Error has occurred", config.GetDbCommandText(), sqlEx);
                    }
                    catch (DataModelConstructionException DMCEx)
                    {
                        rollback = true;
                        throw DMCEx;
                    }
                    catch (Exception Ex)
                    {
                        rollback = true;
                        throw new FacadeException("Unknown Error", Ex);
                    }
                    finally
                    {
                        if (rollback && transaction != null) transaction.Rollback();
                        if (dbCommand != null) dbCommand.Dispose();
                        if (dbDataReader != null) dbDataReader.Close();
                        if (transaction != null) transaction.Dispose();
                    }                    
                }                
            }
            return response;
        }
    }
}
