using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;

namespace DomainFacade.Facade.Core
{
    public abstract class Facade { }
    public abstract class FacadeAPIBase<E> : Facade where E : DbMethodsCore
    {

        protected abstract R CallDbMethod<R>(E dbMethod) 
            where R : DbResponse;

        protected abstract R CallDbMethod<U, R>(U parameters, E dbMethod) 
            where R : DbResponse
            where U : IDbParamsModel;
        protected abstract R CallFacadeAPIDbMethod<U, F, R>(U parameters, E dbMethod) 
            where R: DbResponse
            where U : IDbParamsModel
            where F : FacadeAPI<E>;
        
    }
}
