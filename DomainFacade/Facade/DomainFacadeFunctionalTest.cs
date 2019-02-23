using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using System.Data;

namespace DomainFacade.Facade
{

    public abstract class DomainFacadeFunctionalTest<TDbManifest> : DomainFacadeFunctionalTest<DomainManager<TDbManifest>, TDbManifest>
    where TDbManifest : DbManifest
    { }
    
    public abstract class DomainFacadeFunctionalTest<M, TDbManifest> : DomainFacade<M, TDbManifest>
    where M : DomainManager<TDbManifest>
    where TDbManifest : DbManifest
    {
        private DbFunctionalTestParamsModel<DbParams> GetTestParams<DbParams>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
            where DbParams : IDbParamsModel
        {
            return new DbFunctionalTestParamsModel<DbParams>(parameters, testResponseData, ReturnValue);
        }
        private DbFunctionalTestParamsModel<DbParams> GetTestParams<DbParams>(DbParams parameters, IDataReader testResponseData)
            where DbParams : IDbParamsModel
        {
            return new DbFunctionalTestParamsModel<DbParams>(parameters, testResponseData);
        } 
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData)
            where TDbDataModel : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<TDbDataModel, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
            where TDbDataModel : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<TDbDataModel, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbMethod>(IDataReader testResponseData)
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<TDbDataModel, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData));
        }
        protected IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbMethod>(IDataReader testResponseData, object ReturnValue)
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<TDbDataModel, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData, ReturnValue));
        }

        

        protected IDbResponse Transaction<DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData)
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<DbDataModel, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected IDbResponse Transaction<DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<DbDataModel, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected IDbResponse Transaction<DbMethod>(IDataReader testResponseData)
            where DbMethod : TDbManifest
        {
            return CallDbMethod<DbDataModel, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData));
        }
        protected IDbResponse Transactionn<DbMethod>(IDataReader testResponseData, object ReturnValue)
            where DbMethod : TDbManifest
        {
            return CallDbMethod<DbDataModel, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData, ReturnValue));
        }

        /* Overridden Methods to prevent actual db calls*/
        protected override IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters){return null;}
        protected override IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbMethod>() { return null; }
        protected override IDbResponse Transaction<DbParams, DbMethod>(DbParams parameters) { return null; }
        protected override IDbResponse Transaction<DbMethod>() { return null; }        
    }
}
