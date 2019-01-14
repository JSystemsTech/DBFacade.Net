using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using static DomainFacade.DataLayer.Models.DbResponse;

namespace DomainFacade.Facade
{
    
    public class DomainBase<E> : DomainBase<Manager<E>, E>
    where E : DbMethodsCore
    { }
    public class DomainBase<M, E> : DomainBase<M, DataManager<E>, E>
    where M : Manager<E>
    where E : DbMethodsCore
    { }

    public interface IDomainFacade { }
    public class DomainBase<M, DM, E> : FacadeAPIMiddleMan<E, DomainBaseCore<M, DM, E>>, IDomainFacade
    where M : Manager<DM, E>
    where DM : DataManager<E>
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

    public class DomainBaseCore<M, DM, E> : FacadeAPIMiddleMan<E, M>
    where M : Manager<DM, E>
    where DM : DataManager<E>
    where E : DbMethodsCore
    {
        protected override void OnBeforeForward<U>(U parameters, E dbMethod)
        {
            
        }
    }
}
