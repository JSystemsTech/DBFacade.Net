using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using static DomainFacade.DataLayer.Models.DbResponse;

namespace DomainFacade.Facade
{
    
    public class DomainFacade<TDbManifest> : DomainFacade<DomainManager<TDbManifest>, TDbManifest>
    where TDbManifest : DbManifest
    { }
    
    public class DomainFacade<M, TDbManifest> : DbFacade<TDbManifest>.Forwarder<DomainFacadeCore<M, TDbManifest>>
    where M : DomainManager<TDbManifest>
    where TDbManifest : DbManifest
    {
        protected FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse,DbParams, DbMethod>(DbParams parameters)
            where TDbResponse : DbDataModel
            where DbParams: IDbParamsModel
            where DbMethod: TDbManifest
        {
            return CallDbMethod<FetchRecordModel<TDbResponse, DbMethod>, DbParams, DbMethod>(parameters);
        }
        protected FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse, DbMethod>() where TDbResponse : DbDataModel where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordModel<TDbResponse, DbMethod>, DbMethod>();
        }

        protected FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbParams, DbMethod>(DbParams parameters) where TDbResponse : DbDataModel where DbParams : IDbParamsModel where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordsModel<TDbResponse, DbMethod>, DbParams, DbMethod>(parameters);
        }
        protected FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbMethod>() where TDbResponse : DbDataModel where DbMethod : TDbManifest
        {
            return CallDbMethod<FetchRecordsModel<TDbResponse, DbMethod>, DbMethod>();
        }

        protected TransactionModel Transaction<DbParams, DbMethod>(DbParams parameters) where DbParams : IDbParamsModel where DbMethod : TDbManifest
        {
            return CallDbMethod<TransactionModel, DbParams, DbMethod>(parameters);
        }
        protected TransactionModel Transaction<DbMethod>() where DbMethod : TDbManifest
        {
            return CallDbMethod<TransactionModel, DbMethod>();
        }
    }

    public sealed class DomainFacadeCore<M, TDbManifest> : DbFacade<TDbManifest>.Forwarder<M>
    where M : DomainManager<TDbManifest>
    where TDbManifest : DbManifest
    {
    }
}
