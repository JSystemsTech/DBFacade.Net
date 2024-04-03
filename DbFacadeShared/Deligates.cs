using DbFacade.DataLayer.CommandConfig;
using DbFacade.DataLayer.Models;
using DbFacade.Exceptions;
using System;
using System.Data;
using System.Threading.Tasks;
using DbFacade.DataLayer.ConnectionService;

namespace DbFacade
{
    public delegate object OnExecuteDbAction<TDbDataModel, TDbParams>(IDbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false) where TDbDataModel : DbDataModel;
    public delegate Task<object> OnExecuteDbActionAsync<TDbDataModel, TDbParams>(IDbCommandMethod<TDbParams, TDbDataModel> commandConfig, TDbParams parameters, bool rawDataOnly = false) where TDbDataModel : DbDataModel;
    public delegate IDbDataAdapter OnGetDbDataAdapter();
    public delegate IDbConnection OnCreateDbConnection(string connectionString);
    public delegate string OnGetConnectionString();
    public delegate void OnErrorHandler(Exception ex, IDbCommandSettings commandSettings);
    public delegate void OnValidationErrorHandler(ValidationException ex, IDbCommandSettings commandSettings);
    public delegate void OnSQLExecutionErrorHandler(SQLExecutionException ex, IDbCommandSettings commandSettings);
    public delegate void OnFacadeErrorHandler(FacadeException ex, IDbCommandSettings commandSettings);
    public delegate Task OnErrorHandlerAsync(Exception ex, IDbCommandSettings commandSettings);
    public delegate Task OnValidationErrorHandlerAsync(ValidationException ex, IDbCommandSettings commandSettings);
    public delegate Task OnSQLExecutionErrorHandlerAsync(SQLExecutionException ex, IDbCommandSettings commandSettings);
    public delegate Task OnFacadeErrorHandlerAsync(FacadeException ex, IDbCommandSettings commandSettings);
}
