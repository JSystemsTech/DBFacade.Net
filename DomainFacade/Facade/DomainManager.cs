using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Facade.Core;
using System;
using System.Text;

namespace DomainFacade.Facade
{
    public class DomainManager<TDbManifest> : DbFacade<TDbManifest>
    where TDbManifest : DbManifest
    {
        protected sealed override IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
        {
            return CallFacadeAPIDbMethod<DomainManagerCore<TDbManifest>, TDbDataModel, TDbParams, DbMethod>(parameters);
        }
    }

    internal sealed class DomainManagerCore<TDbManifest> : Forwarder<TDbManifest, DbConnectionManager<TDbManifest>>
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
