using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;

namespace DomainFacade.Facade.Core
{
      
    public abstract class FacadeAPIBase<E> where E : DbMethodsCore
    {

        protected abstract R CallDbMethod<R, Em>() 
            where R : DbResponse
            where Em : E;

        protected abstract R CallDbMethod<U, R, Em>(U parameters) 
            where R : DbResponse
            where U : IDbParamsModel
            where Em : E;
        protected abstract R CallFacadeAPIDbMethod<U, F, R, Em>(U parameters) 
            where R: DbResponse
            where U : IDbParamsModel
            where F : FacadeAPI<E>
            where Em:E;

        internal sealed class DbMethodsService : InstanceResolver<E>
        {

        }

    }
}
