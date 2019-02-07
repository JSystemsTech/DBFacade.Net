using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Facade.Core;
using System;
using System.Text;

namespace DomainFacade.Facade
{
    public class DomainManager<DbMethodGroup> : FacadeAPI<DbMethodGroup>.Forwarder<DomainManagerCore<DbMethodGroup>>
    where DbMethodGroup : DbMethodsCore
    {
        protected override void OnBeforeForward<U, DbMethod>(U parameters){}
    }

    public sealed class DomainManagerCore<DbMethodGroup> : FacadeAPI<DbMethodGroup>.Forwarder<DbConnectionManager<DbMethodGroup>>
    where DbMethodGroup : DbMethodsCore
    {
        protected override void OnBeforeForward<U, DbMethod>(U parameters)
        {
            if (!DbMethodsService.GetInstance<DbMethod>().GetConfig().HasStoredProcedure())
            {
                throw new Exception("");
            }
            else if (!DbMethodsService.GetInstance<DbMethod>().GetConfig().Validate(parameters))
            {
                string paramsType = parameters.GetType().Name;
                string MethodType = DbMethodsService.GetInstance<DbMethod>().GetType().Name;
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} values failed to pass validation for method {1}",
                paramsType,
                MethodType);

                throw new Exception(builder.ToString());
            }
        }
    }
}
