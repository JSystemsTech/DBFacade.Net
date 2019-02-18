using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using System.Data;
using static DomainFacade.DataLayer.Models.DbResponse;

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
        protected FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData)
            where TDbResponse : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordModel<TDbResponse, DbMethod>, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
            where TDbResponse : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordModel<TDbResponse, DbMethod>, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse, DbMethod>(IDataReader testResponseData)
            where TDbResponse : DbDataModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordModel<TDbResponse, DbMethod>, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData));
        }
        protected FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse, DbMethod>(IDataReader testResponseData, object ReturnValue)
            where TDbResponse : DbDataModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordModel<TDbResponse, DbMethod>, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData, ReturnValue));
        }

        protected FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData)
            where TDbResponse : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordsModel<TDbResponse, DbMethod>, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
            where TDbResponse : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordsModel<TDbResponse, DbMethod>, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbMethod>(IDataReader testResponseData)
            where TDbResponse : DbDataModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordsModel<TDbResponse, DbMethod>, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData));
        }
        protected FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbMethod>(IDataReader testResponseData, object ReturnValue)
            where TDbResponse : DbDataModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordsModel<TDbResponse, DbMethod>, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData, ReturnValue));
        }

        protected TransactionModel Transaction<TDbResponse, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData)
            where TDbResponse : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<TransactionModel, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected TransactionModel Transaction<TDbResponse, DbParams, DbMethod>(DbParams parameters, IDataReader testResponseData, object ReturnValue)
            where TDbResponse : DbDataModel
            where DbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<TransactionModel, DbFunctionalTestParamsModel<DbParams>, DbMethod>(GetTestParams(parameters, testResponseData));
        }
        protected TransactionModel Transaction<TDbResponse, DbMethod>(IDataReader testResponseData)
            where TDbResponse : DbDataModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<TransactionModel, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData));
        }
        protected TransactionModel Transaction<TDbResponse, DbMethod>(IDataReader testResponseData, object ReturnValue)
            where TDbResponse : DbDataModel
            where DbMethod : TDbManifest
        {
            return CallDbMethod<TransactionModel, DbFunctionalTestParamsModel<DbParamsModel>, DbMethod>(GetTestParams(DEFAULT_PARAMETERS, testResponseData, ReturnValue));
        }

        /* Overridden Methods to prevent actual db calls*/
        protected override FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse, DbParams, DbMethod>(DbParams parameters){return null;}
        protected override FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse, DbMethod>() { return null; }
        protected override FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbParams, DbMethod>(DbParams parameters) { return null; }
        protected override FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbMethod>() { return null; }
        protected override TransactionModel Transaction<DbParams, DbMethod>(DbParams parameters) { return null; }
        protected override TransactionModel Transaction<DbMethod>() { return null; }        
    }
}
