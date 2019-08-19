using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.Exceptions;
using DBFacade.Facade.Core;
using System.Text;

namespace DBFacade.Facade
{
    public class DomainManager<TDbManifest> : DbFacade<TDbManifest>
    where TDbManifest : DbManifest
    {
        internal sealed override void OnBeforeNextInner<TDbParams, DbMethod>(DbMethod method, TDbParams parameters) {
            IDbCommandConfig config = method.GetConfig();
            IValidationResult validationResult = config.Validate(parameters);
            string paramsType = parameters.GetType().Name;
            string MethodType = method.GetType().Name;
            if (!validationResult.IsValid())
            {
                throw new ValidationException<TDbParams>(validationResult, parameters, $"{paramsType} values failed to pass validation for method {MethodType}");
            }
        }        
        internal sealed override IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, DbMethod>(DbMethod method, TDbParams parameters)
        {
            return ExecuteNext<DbConnectionManager<TDbManifest>, TDbDataModel, TDbParams, DbMethod>(method, parameters);
        }
        internal sealed override void HandleInnerDispose() => GetDbFacade<DbConnectionManager<TDbManifest>>().Dispose();
    }
}
