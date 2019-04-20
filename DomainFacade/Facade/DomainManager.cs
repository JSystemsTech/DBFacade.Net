using DomainFacade.DataLayer.CommandConfig;
using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.DataLayer.Models.Validators;
using DomainFacade.Exceptions;
using DomainFacade.Facade.Core;
using System.Text;

namespace DomainFacade.Facade
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <seealso cref="DomainFacade.Facade.Core.DbFacade{TDbManifest}" />
    public class DomainManager<TDbManifest> : DbFacade<TDbManifest>
    where TDbManifest : DbManifest
    {
        /// <summary>
        /// Calls the database method core.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected sealed override IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
        {
            return CallFacadeAPIDbMethod<DomainManagerCore<TDbManifest>, TDbDataModel, TDbParams, DbMethod>(parameters);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <seealso cref="DomainFacade.Facade.Core.Forwarder{TDbManifest, DomainFacade.Facade.DbConnectionManager{TDbManifest}}" />
    internal sealed class DomainManagerCore<TDbManifest> : Forwarder<TDbManifest, DbConnectionManager<TDbManifest>>
    where TDbManifest : DbManifest
    {
        /// <summary>
        /// Called when [before forward].
        /// </summary>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="ValidationException{DbParams}"></exception>
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
