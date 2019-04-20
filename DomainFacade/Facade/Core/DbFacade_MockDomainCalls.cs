using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;
using System.Data;

namespace DomainFacade.Facade.Core
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <seealso cref="DomainFacade.Facade.Core.DbFacadeBase{TDbManifest}" />
    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {

        /// <summary>
        /// Gets the test parameters.
        /// </summary>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="testResponseData">The test response data.</param>
        /// <param name="ReturnValue">The return value.</param>
        /// <returns></returns>
        private MockParamsModel<DbParams> GetTestParams<DbParams>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
             where DbParams : IDbParamsModel
        {
            return new MockParamsModel<DbParams>(parameters, testResponseData, ReturnValue);
        }
        /// <summary>
        /// Gets the test parameters.
        /// </summary>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="testResponseData">The test response data.</param>
        /// <returns></returns>
        private MockParamsModel<DbParams> GetTestParams<DbParams>(DbParams parameters, IDataReader testResponseData)
            where DbParams : IDbParamsModel
        {
            return new MockParamsModel<DbParams>(parameters, testResponseData);
        }
        /// <summary>
        /// Mocks the fetch.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="testResponseData">The test response data.</param>
        /// <returns></returns>
        protected sealed override IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData)
        {
            return Fetch<TDbDataModel, MockParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
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
        protected sealed override IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
        {
            return Fetch<TDbDataModel, MockParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        /// <summary>
        /// Mocks the fetch.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="testResponseData">The test response data.</param>
        /// <returns></returns>
        protected sealed override IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbMethod>(IDataReader testResponseData)
        {
            return Fetch<TDbDataModel, MockParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData));
        }
        /// <summary>
        /// Mocks the fetch.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="testResponseData">The test response data.</param>
        /// <param name="ReturnValue">The return value.</param>
        /// <returns></returns>
        protected sealed override IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbMethod>(IDataReader testResponseData, object ReturnValue)
        {
            return Fetch<TDbDataModel, MockParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData, ReturnValue));
        }
        /// <summary>
        /// Mocks the transaction.
        /// </summary>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected sealed override IDbResponse MockTransaction<DbParams, DbMethod>(DbParams parameters)
        {
            return Transaction<MockParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, MockDbTable.EmptyTable));
        }
        /// <summary>
        /// Mocks the transaction.
        /// </summary>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="ReturnValue">The return value.</param>
        /// <returns></returns>
        protected sealed override IDbResponse MockTransaction<DbParams, DbMethod>(DbParams parameters, object ReturnValue)
        {
            return Transaction<MockParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, MockDbTable.EmptyTable));
        }
        /// <summary>
        /// Mocks the transaction.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <returns></returns>
        protected sealed override IDbResponse MockTransaction<DbMethod>()
        {
            return Transaction<MockParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, MockDbTable.EmptyTable));
        }
        /// <summary>
        /// Mocks the transaction.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="testResponseData">The test response data.</param>
        /// <param name="ReturnValue">The return value.</param>
        /// <returns></returns>
        protected sealed override IDbResponse MockTransaction<DbMethod>(IDataReader testResponseData, object ReturnValue)
        {
            return Transaction<MockParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, MockDbTable.EmptyTable, ReturnValue));
        }

    }
}
