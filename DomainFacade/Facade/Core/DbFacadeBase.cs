using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using System.Data;

namespace DomainFacade.Facade.Core
{
    public abstract class DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {

        /// <summary>
        /// Calls the facade API database method.
        /// </summary>
        /// <typeparam name="TDbFacade">The type of the database facade.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected abstract IDbResponse<TDbDataModel> CallFacadeAPIDbMethod<TDbFacade, TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbFacade : DbFacade<TDbManifest>
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        /// <summary>
        /// Fetches the specified parameters.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected abstract IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters)
            where TDbDataModel : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        /// <summary>
        /// Fetches this instance.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <returns></returns>
        protected abstract IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbMethod>() where TDbDataModel : DbDataModel where DbMethod : TDbManifest;
        /// <summary>
        /// Transactions the specified parameters.
        /// </summary>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected abstract IDbResponse Transaction<DbParams, DbMethod>(DbParams parameters) where DbParams : IDbParamsModel where DbMethod : TDbManifest;
        /// <summary>
        /// Transactions this instance.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <returns></returns>
        protected abstract IDbResponse Transaction<DbMethod>() where DbMethod : TDbManifest;


        /// <summary>
        /// Mocks the fetch.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="testResponseData">The test response data.</param>
        /// <returns></returns>
        protected abstract IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData)
            where TDbDataModel : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        /// <summary>
        /// Mocks the fetch.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="testResponseData">The test response data.</param>
        /// <param name="ReturnValue">The return value.</param>
        /// <returns></returns>
        protected abstract IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
            where TDbDataModel : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        /// <summary>
        /// Mocks the fetch.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="testResponseData">The test response data.</param>
        /// <returns></returns>
        protected abstract IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbMethod>(IDataReader testResponseData)
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest;
        /// <summary>
        /// Mocks the fetch.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="testResponseData">The test response data.</param>
        /// <param name="ReturnValue">The return value.</param>
        /// <returns></returns>
        protected abstract IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbMethod>(IDataReader testResponseData, object ReturnValue)
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest;
        /// <summary>
        /// Mocks the transaction.
        /// </summary>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected abstract IDbResponse MockTransaction<DbParams, DbMethod>(DbParams parameters)
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        /// <summary>
        /// Mocks the transaction.
        /// </summary>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="ReturnValue">The return value.</param>
        /// <returns></returns>
        protected abstract IDbResponse MockTransaction<DbParams, DbMethod>(DbParams parameters, object ReturnValue)
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest;
        /// <summary>
        /// Mocks the transaction.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <returns></returns>
        protected abstract IDbResponse MockTransaction<DbMethod>()
            where DbMethod : TDbManifest;
        /// <summary>
        /// Mocks the transaction.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="testResponseData">The test response data.</param>
        /// <param name="ReturnValue">The return value.</param>
        /// <returns></returns>
        protected abstract IDbResponse MockTransaction<DbMethod>(IDataReader testResponseData, object ReturnValue)
            where DbMethod : TDbManifest;
    }
}
