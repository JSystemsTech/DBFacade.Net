using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using static DomainFacade.DataLayer.Models.DbResponse;

namespace DomainFacade.Facade
{
    
    public class DomainFacade<E> : DomainFacade<DomainManager<E>, E>
    where E : DbMethodsCore
    { }
    
    public class DomainFacade<M, E> : FacadeAPI<E>.Forwarder<DomainFacadeCore<M, E>>
    where M : DomainManager<E>
    where E : DbMethodsCore
    {
        protected FetchRecordModel<R, Em> FetchRecord<R,U, Em>(U parameters)
            where R: DbDataModel
            where U: IDbParamsModel
            where Em: E
        {
            return CallDbMethod<U,FetchRecordModel<R, Em>, Em>(parameters);
        }
        protected FetchRecordModel<R, Em> FetchRecord<R, Em>() where R : DbDataModel where Em : E
        {
            return CallDbMethod<FetchRecordModel<R, Em>, Em>();
        }

        protected FetchRecordsModel<R, Em> FetchRecords<R, U, Em>(U parameters) where R : DbDataModel where U : IDbParamsModel where Em : E
        {
            return CallDbMethod<U, FetchRecordsModel<R, Em>, Em>(parameters);
        }
        protected FetchRecordsModel<R, Em> FetchRecords<R, Em>() where R : DbDataModel where Em : E
        {
            return CallDbMethod<FetchRecordsModel<R, Em>, Em>();
        }

        protected TransactionModel Transaction<U, Em>(U parameters) where U : IDbParamsModel where Em : E
        {
            return CallDbMethod<U, TransactionModel, Em>(parameters);
        }
        protected TransactionModel Transaction<Em>() where Em : E
        {
            return CallDbMethod<TransactionModel, Em>();
        }
    }

    public sealed class DomainFacadeCore<M, E> : FacadeAPI<E>.Forwarder<M>
    where M : DomainManager<E>
    where E : DbMethodsCore
    {
    }
}
