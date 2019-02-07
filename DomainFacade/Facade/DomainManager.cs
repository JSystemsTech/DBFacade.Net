using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Facade.Core;
using System;
using System.Text;

namespace DomainFacade.Facade
{
    public class DomainManager<DbMethodGroup> : DbFacade<DbMethodGroup>.Forwarder<DomainManagerCore<DbMethodGroup>>
    where DbMethodGroup : DbMethodsCore
    {
        protected override void OnBeforeForward<DbParams, DbMethod>(DbParams parameters){}
    }

    public sealed class DomainManagerCore<DbMethodGroup> : DbFacade<DbMethodGroup>.Forwarder<DbConnectionManager<DbMethodGroup>>
    where DbMethodGroup : DbMethodsCore
    {
        protected override void OnBeforeForward<DbParams, DbMethod>(DbParams parameters)
        {
            if (!DbMethodsCache.GetInstance<DbMethod>().GetConfig().HasStoredProcedure())
            {
                throw new Exception("");
            }
            else if (!DbMethodsCache.GetInstance<DbMethod>().GetConfig().Validate(parameters))
            {
                string paramsType = parameters.GetType().Name;
                string MethodType = DbMethodsCache.GetInstance<DbMethod>().GetType().Name;
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} values failed to pass validation for method {1}",
                paramsType,
                MethodType);

                throw new Exception(builder.ToString());
            }
        }
    }
}
