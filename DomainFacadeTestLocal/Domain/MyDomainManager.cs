using DBFacade.Facade;
using DomainFacadeTestLocal.Domain.Methods;

namespace DomainFacadeTestLocal.Domain
{
    internal class MyDomainManager: DomainManager<DomainMethods>
    {
        protected override void OnBeforeNext<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            base.OnBeforeNext(method, parameters);
        }
    }    
}
