using DomainFacade.DataLayer;
using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{
    
    public abstract class FacadeAPI<E> : FacadeAPIBase<E> where E : DbMethodsCore
    {        
        private DbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();
        
        protected override R CallDbMethod<R, Em>()
        {
            return CallDbMethod<DbParamsModel, R, Em>(DEFAULT_PARAMETERS);
        }
        protected override R CallDbMethod<U,R, Em>(U parameters)
        {
            return CallDbMethodCore<U,R, Em>(parameters);
        }         
        protected override R CallFacadeAPIDbMethod<U, F, R, Em>(U parameters)
        {
            return FacadeAPIService.GetInstance<F>().CallDbMethod<U, R, Em>(parameters);
           // return GenericInstance<F>.GetInstance().CallDbMethod<U, R>(parameters, dbMethod);
        }
        
        protected abstract R CallDbMethodCore<U, R, Em>(U parameters)
            where R : DbResponse
            where U : IDbParamsModel
            where Em:E;


        public class Forwarder<T> : FacadeAPI<E>
        where T : FacadeAPI<E>
        {
            protected override R CallDbMethodCore<U, R, Em>(U parameters)
            {
                OnBeforeForward<U, Em>(parameters);
                return CallFacadeAPIDbMethod<U, T, R, Em>(parameters);
            }
            protected virtual void OnBeforeForward<U, Em>(U parameters) 
                where U : IDbParamsModel
            where Em : E
            { }            
        }

        private sealed class FacadeAPIService : InstanceResolver<FacadeAPI<E>>{}

        
    }    
}
