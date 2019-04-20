using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;

namespace DBFacade.Facade.Core
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <seealso cref="Core.DbFacadeBase{TDbManifest}" />
    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {

        /// <summary>
        /// Fetches the specified parameters.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected sealed override IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbParams, DbMethod>(DbParams parameters)
        {
            return CallDbMethod<TDbDataModel, DbParams, DbMethod>(parameters);
        }
        /// <summary>
        /// Fetches this instance.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <returns></returns>
        protected sealed override IDbResponse<TDbDataModel> Fetch<TDbDataModel, DbMethod>()
        {
            return CallDbMethod<TDbDataModel, DbMethod>();
        }
        /// <summary>
        /// Transactions the specified parameters.
        /// </summary>
        /// <typeparam name="DbParams">The type of the b parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected sealed override IDbResponse Transaction<DbParams, DbMethod>(DbParams parameters)
        {
            return CallDbMethod<DbDataModel, DbParams, DbMethod>(parameters);
        }
        /// <summary>
        /// Transactions this instance.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <returns></returns>
        protected sealed override IDbResponse Transaction<DbMethod>()
        {
            return CallDbMethod<DbDataModel, DbMethod>();
        }

    }
}
