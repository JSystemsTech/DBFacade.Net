using DBFacade.DataLayer.Models;
using DBFacade.Facade;
using DBFacade.Facade.Core;
using DomainFacadeTestLocal.Domain.Methods;

namespace DomainFacadeTestLocal.Domain
{
    internal class MyDomainFacade : DomainFacade<MyDomainManager, DomainMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            return Next<Step1, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
    }
    internal class Step1 : DbFacade<DomainMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            return Next<Step2, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
    }
    internal class Step2 : DbFacade<DomainMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            return Next<Step3, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
    }
    internal class Step3 : DbFacade<DomainMethods>
    {
        protected override IDbResponse<TDbDataModel> Process<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            return Next<MyDomainManager, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
    }
}
