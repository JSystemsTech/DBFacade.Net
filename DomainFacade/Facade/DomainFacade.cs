using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using static DomainFacade.DataLayer.Models.DbResponse;

namespace DomainFacade.Facade
{
    
    public class DomainFacade<E> : DomainFacade<DomainManager<E>, E>
    where E : DbMethodsCore
    { }

    public interface IDomainFacade { }
    public class DomainFacade<M, E> : FacadeAPIMiddleMan<E, DomainFacadeCore<M, E>>, IDomainFacade
    where M : DomainManager<E>
    where E : DbMethodsCore
    {
        protected FetchRecordModel<R> FetchRecord<R,U>(U parameters, E dbMethod)
            where R: DbDataModel
            where U: IDbParamsModel
        {
            return CallDbMethod<U,FetchRecordModel<R>>(parameters, dbMethod);
        }
        protected FetchRecordModel<R> FetchRecord<R>(E dbMethod) where R : DbDataModel
        {
            return CallDbMethod<FetchRecordModel<R>>(dbMethod);
        }

        protected FetchRecordsModel<R> FetchRecords<R, U>(U parameters, E dbMethod) where R : DbDataModel where U : IDbParamsModel
        {
            return CallDbMethod<U, FetchRecordsModel<R>>(parameters, dbMethod);
        }
        protected FetchRecordsModel<R> FetchRecords<R>(E dbMethod) where R : DbDataModel
        {
            return CallDbMethod<FetchRecordsModel<R>>(dbMethod);
        }

        protected TransactionModel Transaction<U>(U parameters, E dbMethod) where U : IDbParamsModel
        {
            return CallDbMethod<U, TransactionModel>(parameters, dbMethod);
        }
        protected TransactionModel Transaction(E dbMethod)
        {
            return CallDbMethod<TransactionModel>(dbMethod);
        }

        protected override void OnBeforeForward<U>(U parameters, E dbMethod) { }
    }

    public class DomainFacadeCore<M, E> : FacadeAPIMiddleMan<E, M>
    where M : DomainManager<E>
    where E : DbMethodsCore
    {
        protected override void OnBeforeForward<U>(U parameters, E dbMethod)
        {
            
        }
    }
}
