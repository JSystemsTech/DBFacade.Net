using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Facade.Core;
using System;
using System.Text;

namespace DomainFacade.Facade
{
    public class DomainManager<E> : FacadeAPIMiddleMan<E, DomainManagerCore<E>>
    where E : DbMethodsCore
    {
        protected override void OnBeforeForward<U>(U parameters, E dbMethod){}
    }

    public class DomainManagerCore<E> : FacadeAPIMiddleMan<E, DbConnectionManager<E>>
    where E : DbMethodsCore
    {
        protected override void OnBeforeForward<U>(U parameters, E dbMethod)
        {
            if (!dbMethod.GetConfig().Validate(parameters))
            {
                string paramsType = parameters.GetType().Name;
                string MethodType = dbMethod.GetType().Name;
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} values failed to pass validation for method {1}",
                paramsType,
                MethodType);

                throw new Exception(builder.ToString());
            }
        }
    }
}
