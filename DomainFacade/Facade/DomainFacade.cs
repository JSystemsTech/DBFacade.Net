using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using static DomainFacade.DataLayer.Models.DbResponse;

namespace DomainFacade.Facade
{
    
    public class DomainFacade<DbMethodGroup> : DomainFacade<DomainManager<DbMethodGroup>, DbMethodGroup>
    where DbMethodGroup : DbMethodsCore
    { }
    
    public class DomainFacade<M, DbMethodGroup> : DbFacade<DbMethodGroup>.Forwarder<DomainFacadeCore<M, DbMethodGroup>>
    where M : DomainManager<DbMethodGroup>
    where DbMethodGroup : DbMethodsCore
    {
        protected FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse,DbParams, DbMethod>(DbParams parameters)
            where TDbResponse : DbDataModel
            where DbParams: IDbParamsModel
            where DbMethod: DbMethodGroup
        {
            return CallDbMethod<FetchRecordModel<TDbResponse, DbMethod>, DbParams, DbMethod>(parameters);
        }
        protected FetchRecordModel<TDbResponse, DbMethod> FetchRecord<TDbResponse, DbMethod>() where TDbResponse : DbDataModel where DbMethod : DbMethodGroup
        {
            return CallDbMethod<FetchRecordModel<TDbResponse, DbMethod>, DbMethod>();
        }

        protected FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbParams, DbMethod>(DbParams parameters) where TDbResponse : DbDataModel where DbParams : IDbParamsModel where DbMethod : DbMethodGroup
        {
            return CallDbMethod<FetchRecordsModel<TDbResponse, DbMethod>, DbParams, DbMethod>(parameters);
        }
        protected FetchRecordsModel<TDbResponse, DbMethod> FetchRecords<TDbResponse, DbMethod>() where TDbResponse : DbDataModel where DbMethod : DbMethodGroup
        {
            return CallDbMethod<FetchRecordsModel<TDbResponse, DbMethod>, DbMethod>();
        }

        protected TransactionModel Transaction<DbParams, DbMethod>(DbParams parameters) where DbParams : IDbParamsModel where DbMethod : DbMethodGroup
        {
            return CallDbMethod<TransactionModel, DbParams, DbMethod>(parameters);
        }
        protected TransactionModel Transaction<DbMethod>() where DbMethod : DbMethodGroup
        {
            return CallDbMethod<TransactionModel, DbMethod>();
        }
    }

    public sealed class DomainFacadeCore<M, DbMethodGroup> : DbFacade<DbMethodGroup>.Forwarder<M>
    where M : DomainManager<DbMethodGroup>
    where DbMethodGroup : DbMethodsCore
    {
    }
}
