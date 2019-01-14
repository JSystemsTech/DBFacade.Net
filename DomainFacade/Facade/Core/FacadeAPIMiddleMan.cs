using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;

namespace DomainFacade.Facade.Core
{
    public abstract class FacadeAPIMiddleMan<E, T> : FacadeAPI<E>
        where E : DbMethodsCore
        where T : FacadeAPI<E>
    {
        protected override R CallDbMethodCore<U, R>(U parameters, E dbMethod)
        {
            OnBeforeForward(parameters, dbMethod);
            return CallFacadeAPIDbMethod<U, T, R>(parameters, dbMethod);
        }
        protected abstract void OnBeforeForward<U>(U parameters, E dbMethod) where U:IDbParamsModel;

        public class SimpleForward : FacadeAPIMiddleMan<E, T>
        {
            protected override void OnBeforeForward<U>(U parameters, E dbMethod){ }
        }
    }
}
