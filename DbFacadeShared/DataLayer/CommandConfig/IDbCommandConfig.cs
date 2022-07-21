using DbFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.CommandConfig
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IDbCommandMethod<TDbParams,TDbDataModel>
        where TDbDataModel : DbDataModel
    {

        /// <summary>
        /// Executes the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Execute(TDbParams parameters);

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(TDbParams parameters);


        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
    public interface IParameterlessDbCommandMethod<TDbDataModel>
        where TDbDataModel : DbDataModel
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Execute();
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> ExecuteAsync();
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
    public interface IDbCommandMethod<TDbParams>
    {
        /// <summary>
        /// Executes the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IDbResponse Execute(TDbParams parameters);
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IDbResponse> ExecuteAsync(TDbParams parameters);
       
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDbCommandMethod
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        IDbResponse Execute();
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IDbResponse> ExecuteAsync();
        
    }

    
}