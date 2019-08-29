using DBFacade.DataLayer.CommandConfig;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.Exceptions;
using DBFacade.Facade.Core;
using System.Threading.Tasks;

namespace DBFacade.Facade
{
    public abstract class DomainManager<TDbManifest> : DbFacade<TDbManifest>
    where TDbManifest : DbManifest
    {
        internal sealed override async Task OnBeforeNextInnerAsync<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            IValidationResult validationResult = await method.GetConfig().ValidateAsync(parameters);
            if (!validationResult.IsValid())
            {
                throw new ValidationException<TDbParams>(validationResult, parameters, $"{parameters.GetType().Name} values failed to pass validation for method {method.GetType().Name}");
            }
        }
        internal sealed override void OnBeforeNextInner<TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters) {
            IValidationResult validationResult = method.GetConfig().Validate(parameters);
            if (!validationResult.IsValid())
            {
                throw new ValidationException<TDbParams>(validationResult, parameters, $"{parameters.GetType().Name} values failed to pass validation for method {method.GetType().Name}");
            }
        }        
        internal sealed override IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            return ExecuteNext<DbConnectionManager<TDbManifest>, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
        internal sealed async override Task<IDbResponse<TDbDataModel>> ExecuteNextCoreAsync<TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
        {
            return await ExecuteNextAsync<DbConnectionManager<TDbManifest>, TDbDataModel, TDbParams, TDbManifestMethod>(method, parameters);
        }
        protected override void OnDispose(bool calledFromDispose)
        {
            GetDbFacade<DbConnectionManager<TDbManifest>>().Dispose(calledFromDispose);
            base.OnDispose(calledFromDispose);
        }
    }
}
