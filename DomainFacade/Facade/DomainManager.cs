using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Facade.Core;
using System;
using System.Text;

namespace DomainFacade.Facade
{
    public class DomainManager<E> : FacadeAPI<E>.Forwarder<DomainManagerCore<E>>
    where E : DbMethodsCore
    {
        protected override void OnBeforeForward<U, Em>(U parameters){}
    }

    public sealed class DomainManagerCore<E> : FacadeAPI<E>.Forwarder<DbConnectionManager<E>>
    where E : DbMethodsCore
    {
        protected override void OnBeforeForward<U, Em>(U parameters)
        {
            if (!DbMethodsService.GetInstance<Em>().GetConfig().HasStoredProcedure())
            {
                throw new Exception("");
            }
            else if (!DbMethodsService.GetInstance<Em>().GetConfig().Validate(parameters))
            {
                string paramsType = parameters.GetType().Name;
                string MethodType = DbMethodsService.GetInstance<Em>().GetType().Name;
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} values failed to pass validation for method {1}",
                paramsType,
                MethodType);

                throw new Exception(builder.ToString());
            }
        }
    }
}
