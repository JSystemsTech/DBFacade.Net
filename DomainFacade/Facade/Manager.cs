using DomainFacade.DataLayer.DbManifest;
using DomainFacade.Facade.Core;
using System;
using System.Text;

namespace DomainFacade.Facade
{
    public class Manager<E> : Manager<DataManager<E>, E>
    where E : DbMethodsCore
    { }
    public class Manager<DM, E> : FacadeAPIMiddleMan<E, ManagerCore<DM, E>>
    where DM : DataManager<E>
    where E : DbMethodsCore
    {
        protected override void OnBeforeForward<U>(U parameters, E dbMethod){}
    }

    public class ManagerCore<DM, E> : FacadeAPIMiddleMan<E, DM>
    where DM : DataManager<E>
    where E : DbMethodsCore
    {
        protected override void OnBeforeForward<U>(U parameters, E dbMethod)
        {
            if (!parameters.Validate(dbMethod))
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
