using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;
using System.Data;

namespace DomainFacade.Facade.Core
{
    
    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {

        private MockParamsModel<DbParams> GetTestParams<DbParams>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
             where DbParams : IDbParamsModel
        {
            return new MockParamsModel<DbParams>(parameters, testResponseData, ReturnValue);
        }
        private MockParamsModel<DbParams> GetTestParams<DbParams>(DbParams parameters, IDataReader testResponseData)
            where DbParams : IDbParamsModel
        {
            return new MockParamsModel<DbParams>(parameters, testResponseData);
        }
        protected sealed override IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData)
        {
            return Fetch<TDbDataModel, MockParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected sealed override IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
        {
            return Fetch<TDbDataModel, MockParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected sealed override IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbMethod>(IDataReader testResponseData)
        {
            return Fetch<TDbDataModel, MockParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData));
        }
        protected sealed override IDbResponse<TDbDataModel> MockFetch<TDbDataModel, DbMethod>(IDataReader testResponseData, object ReturnValue)
        {
            return Fetch<TDbDataModel, MockParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData, ReturnValue));
        }
        protected sealed override IDbResponse MockTransaction<DbParams, DbMethod>(DbParams parameters)
        {
            return Transaction<MockParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, MockDbTable.EmptyTable));
        }
        protected sealed override IDbResponse MockTransaction<DbParams, DbMethod>(DbParams parameters, object ReturnValue)
        {
            return Transaction<MockParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, MockDbTable.EmptyTable));
        }
        protected sealed override IDbResponse MockTransaction<DbMethod>()
        {
            return Transaction<MockParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, MockDbTable.EmptyTable));
        }
        protected sealed override IDbResponse MockTransaction<DbMethod>(IDataReader testResponseData, object ReturnValue)
        {
            return Transaction<MockParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, MockDbTable.EmptyTable, ReturnValue));
        }

    }
}
