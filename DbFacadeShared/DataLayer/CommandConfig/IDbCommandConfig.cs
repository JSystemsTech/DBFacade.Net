using DbFacade.DataLayer.Models;
using System.Collections.Generic;
using System;
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
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Execute(TDbParams parameters, bool rawDataOnly = false);

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(TDbParams parameters, bool rawDataOnly = false);

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
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        IDbResponse<TDbDataModel> Execute(bool rawDataOnly = false);
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> ExecuteAsync(bool rawDataOnly = false);

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
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        IDbResponse Execute(TDbParams parameters,bool rawDataOnly = false);
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="rawDataOnly">if set to <c>true</c> [raw data only].</param>
        /// <returns></returns>
        Task<IDbResponse> ExecuteAsync(TDbParams parameters, bool rawDataOnly = false);

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