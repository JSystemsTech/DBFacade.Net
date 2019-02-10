using DomainFacade.DataLayer.Manifest;
using DomainFacade.Facade.Core;
using System;
using System.Text;

namespace DomainFacade.Facade
{
    public class DomainManager<TDbManifest> : DbFacade<TDbManifest>.Forwarder<DomainManagerCore<TDbManifest>>
    where TDbManifest : DbManifest
    {
        protected override void OnBeforeForward<DbParams, DbMethod>(DbParams parameters){}
    }

    public sealed class DomainManagerCore<TDbManifest> : DbFacade<TDbManifest>.Forwarder<DbConnectionManager<TDbManifest>>
    where TDbManifest : DbManifest
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
