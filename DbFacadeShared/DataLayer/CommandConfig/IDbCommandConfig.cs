using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models;

namespace DbFacade.DataLayer.CommandConfig
{
    public interface IDbCommandMethod<TDbParams,TDbDataModel> : ISafeDisposable
        where TDbDataModel : DbDataModel
        where TDbParams : DbParamsModel
    {
        
        IDbResponse<TDbDataModel> Execute(TDbParams parameters);
        
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(TDbParams parameters);

        
        IDbResponse<TDbDataModel> Mock(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        IDbResponse<TDbDataModel> Mock<T>(TDbParams parameters, T responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        
        Task<IDbResponse<TDbDataModel>> MockAsync(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(TDbParams parameters, T responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);
    }
    public interface IParameterlessDbCommandMethod<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        IDbResponse<TDbDataModel> Execute();
        Task<IDbResponse<TDbDataModel>> ExecuteAsync();
        IDbResponse<TDbDataModel> Mock(int returnValue, Action<IDictionary<string, object>> outputValues = null);
        IDbResponse<TDbDataModel> Mock<T>(IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        IDbResponse<TDbDataModel> Mock<T>(T responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);

        Task<IDbResponse<TDbDataModel>> MockAsync(int returnValue, Action<IDictionary<string, object>> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(IEnumerable<T> responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        Task<IDbResponse<TDbDataModel>> MockAsync<T>(T responseData, int returnValue, Action<IDictionary<string, object>> outputValues = null);
    }
    public interface IDbCommandMethod<TDbParams>
        where TDbParams : DbParamsModel
    {
        IDbResponse Execute(TDbParams parameters);
        Task<IDbResponse> ExecuteAsync(TDbParams parameters);
        IDbResponse Mock(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null);
        Task<IDbResponse> MockAsync(TDbParams parameters, int returnValue, Action<IDictionary<string, object>> outputValues = null);
    }
    public interface IDbCommandMethod
    {
        IDbResponse Execute();
        Task<IDbResponse> ExecuteAsync();
        IDbResponse Mock(int returnValue, Action<IDictionary<string, object>> outputValues = null);
        Task<IDbResponse> MockAsync(int returnValue, Action<IDictionary<string, object>> outputValues = null);
    }

    
}