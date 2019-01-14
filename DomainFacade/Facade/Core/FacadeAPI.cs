using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{

    public abstract class FacadeAPI<E> : FacadeAPIBase<E> where E : DbMethodsCore
    {
        
        private DbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();

        
        protected override R CallDbMethod<R>(E dbMethod)
        {
            return CallDbMethod<DbParamsModel, R>(DEFAULT_PARAMETERS, dbMethod);
        }
        protected override R CallDbMethod<U,R>(U parameters, E dbMethod)
        {
            return CallDbMethodCore<U,R>(parameters, dbMethod);
        }
        protected override R CallFacadeAPIDbMethod<U, F, R>(U parameters, E dbMethod)
        {
            return GenericInstance<F>.GetInstance().CallDbMethod<U, R>(parameters, dbMethod);
        }
        protected abstract R CallDbMethodCore<U, R>(U parameters, E dbMethod)
            where R : DbResponse
            where U : IDbParamsModel;
    }
}
