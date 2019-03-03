using DomainFacade.DataLayer.CommandConfig;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.Exceptions;
using DomainFacade.Facade.Core;
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
            IDbCommandConfig config = DbMethodsCache.GetInstance<DbMethod>().GetConfig();
            IValidationResult validationResult = config.Validate(parameters);
            string paramsType = parameters.GetType().Name;
            string MethodType = DbMethodsCache.GetInstance<DbMethod>().GetType().Name;
            StringBuilder builder = new StringBuilder();
            if (!config.HasStoredProcedure())
            {
                builder.AppendFormat("Invalid stored procedure for method {0}: ", MethodType);
                throw config.GetMissingStoredProcedureException(builder.ToString());
            }
            else if (!validationResult.IsValid())
            {
                builder.AppendFormat("{0} values failed to pass validation for method {1}",
                paramsType,
                MethodType);
                throw new ValidationException<DbParams>(validationResult, parameters, builder.ToString());
            }
        }
    }
}
