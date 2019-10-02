using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.DataLayer.Models.Validators;
using DBFacade.Exceptions;
using DBFacade.Facade.Core;
using System.Threading.Tasks;

namespace DBFacade.Facade
{
    public sealed class DefaultDomainManager<TDbMethodManifest> : DomainManager<TDbMethodManifest>
    where TDbMethodManifest : DbMethodManifest
    { }
    public abstract class DomainManager<TDbMethodManifest> : DbFacade<TDbMethodManifest>
    where TDbMethodManifest : DbMethodManifest
    {
        internal sealed override async Task OnBeforeNextInnerAsync<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            IValidationResult validationResult = await method.Config.ValidateAsync(parameters);
            if (!validationResult.IsValid)
            {
                throw new ValidationException<TDbParams>(validationResult, parameters, $"{parameters.GetType().Name} values failed to pass validation for method {method.GetType().Name}");
            }
        }
        internal sealed override void OnBeforeNextInner<TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters) {
            IValidationResult validationResult = method.Config.Validate(parameters);
            if (!validationResult.IsValid)
            {
                throw new ValidationException<TDbParams>(validationResult, parameters, $"{parameters.GetType().Name} values failed to pass validation for method {method.GetType().Name}");
            }
        }        
        internal sealed override IDbResponse<TDbDataModel> ExecuteNextCore<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            return ExecuteNext<DbConnectionManager<TDbMethodManifest>, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        internal sealed async override Task<IDbResponse<TDbDataModel>> ExecuteNextCoreAsync<TDbDataModel, TDbParams, TDbMethodManifestMethod>(TDbMethodManifestMethod method, TDbParams parameters)
        {
            return await ExecuteNextAsync<DbConnectionManager<TDbMethodManifest>, TDbDataModel, TDbParams, TDbMethodManifestMethod>(method, parameters);
        }
        protected override void OnDispose(bool calledFromDispose)
        {
            GetDbFacade<DbConnectionManager<TDbMethodManifest>>().Dispose(calledFromDispose);
            base.OnDispose(calledFromDispose);
        }
    }
}
