using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using static DomainFacade.DataLayer.Models.DbResponse;

namespace DomainFacade.Facade
{
    
    public class DomainFacade<DbMethodGroup> : DomainFacade<DomainManager<DbMethodGroup>, DbMethodGroup>
    where DbMethodGroup : DbMethodsCore
    { }
    
    public class DomainFacade<M, DbMethodGroup> : FacadeAPI<DbMethodGroup>.Forwarder<DomainFacadeCore<M, DbMethodGroup>>
    where M : DomainManager<DbMethodGroup>
    where DbMethodGroup : DbMethodsCore
    {
        protected FetchRecordModel<R, DbMethod> FetchRecord<R,U, DbMethod>(U parameters)
            where R: DbDataModel
            where U: IDbParamsModel
            where DbMethod: DbMethodGroup
        {
            return CallDbMethod<U,FetchRecordModel<R, DbMethod>, DbMethod>(parameters);
        }
        protected FetchRecordModel<R, DbMethod> FetchRecord<R, DbMethod>() where R : DbDataModel where DbMethod : DbMethodGroup
        {
            return CallDbMethod<FetchRecordModel<R, DbMethod>, DbMethod>();
        }

        protected FetchRecordsModel<R, DbMethod> FetchRecords<R, U, DbMethod>(U parameters) where R : DbDataModel where U : IDbParamsModel where DbMethod : DbMethodGroup
        {
            return CallDbMethod<U, FetchRecordsModel<R, DbMethod>, DbMethod>(parameters);
        }
        protected FetchRecordsModel<R, DbMethod> FetchRecords<R, DbMethod>() where R : DbDataModel where DbMethod : DbMethodGroup
        {
            return CallDbMethod<FetchRecordsModel<R, DbMethod>, DbMethod>();
        }

        protected TransactionModel Transaction<U, DbMethod>(U parameters) where U : IDbParamsModel where DbMethod : DbMethodGroup
        {
            return CallDbMethod<U, TransactionModel, DbMethod>(parameters);
        }
        protected TransactionModel Transaction<DbMethod>() where DbMethod : DbMethodGroup
        {
            return CallDbMethod<TransactionModel, DbMethod>();
        }
    }

    public sealed class DomainFacadeCore<M, DbMethodGroup> : FacadeAPI<DbMethodGroup>.Forwarder<M>
    where M : DomainManager<DbMethodGroup>
    where DbMethodGroup : DbMethodsCore
    {
    }
}
