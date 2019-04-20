using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using DBFacade.Facade.Core;

namespace DBFacade.Facade
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <seealso cref="Facade.DomainFacade{DomainManager{TDbManifest}, TDbManifest}" />
    public abstract class DomainFacade<TDbManifest> : DomainFacade<DomainManager<TDbManifest>, TDbManifest>
    where TDbManifest : DbManifest
    { }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="M"></typeparam>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <seealso cref="Facade.DomainFacade{DomainManager{TDbManifest}, TDbManifest}" />
    public abstract class DomainFacade<M, TDbManifest> : DbFacade<TDbManifest>
    where M : DomainManager<TDbManifest>
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
            return CallFacadeAPIDbMethod<M, TDbDataModel, TDbParams, DbMethod>(parameters);
        }

    }
}
