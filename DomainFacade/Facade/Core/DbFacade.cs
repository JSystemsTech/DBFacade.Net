using DomainFacade.DataLayer.Manifest;
using DomainFacade.DataLayer.Models;
using DomainFacade.Exceptions;
using DomainFacade.Utils;

namespace DomainFacade.Facade.Core
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <seealso cref="DomainFacade.Facade.Core.DbFacadeBase{TDbManifest}" />
    public abstract partial class DbFacade<TDbManifest> : DbFacadeBase<TDbManifest> where TDbManifest : DbManifest
    {
        /// <summary>
        /// The default parameters
        /// </summary>
        internal DbParamsModel DEFAULT_PARAMETERS = new DbParamsModel();
        /// <summary>
        /// Calls the database method.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <returns></returns>
        private IDbResponse<TDbDataModel> CallDbMethod<TDbDataModel, DbMethod>()
            where TDbDataModel : DbDataModel
            where DbMethod : TDbManifest
        {
            IDbResponse <TDbDataModel> response = CallDbMethod<TDbDataModel, DbParamsModel, DbMethod>(DEFAULT_PARAMETERS);
            if (response.HasError())
            {
                OnError(response);
            }
            return response;
        }

        /// <summary>
        /// Calls the database method.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        private IDbResponse<TDbDataModel> CallDbMethod<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod : TDbManifest
        {
            IDbResponse<TDbDataModel> response = CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(parameters);
            if (response.HasError())
            {
                OnError(response);
            }
            return response;
        }

        /// <summary>
        /// Calls the facade API database method.
        /// </summary>
        /// <typeparam name="TDbFacade">The type of the database facade.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected override sealed IDbResponse<TDbDataModel> CallFacadeAPIDbMethod<TDbFacade, TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
        {
            try
            {
                OnBeforeForward<TDbParams, DbMethod>(parameters);
            }
            catch(FacadeException e)
            {
                return new DbResponse<DbMethod, TDbDataModel>(e);
            }         

            return DbFacadeCache.GetInstance<TDbFacade>().CallDbMethod<TDbDataModel, TDbParams, DbMethod>(parameters);
        }
        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="response">The response.</param>
        protected virtual void OnError(IDbResponse response) { }
        /// <summary>
        /// Called when [before forward].
        /// </summary>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        protected virtual void OnBeforeForward<TDbParams, DbMethod>(TDbParams parameters)
        where TDbParams : IDbParamsModel
        where DbMethod : TDbManifest
        { }
        /// <summary>
        /// Calls the database method core.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        protected abstract IDbResponse<TDbDataModel> CallDbMethodCore<TDbDataModel, TDbParams, DbMethod>(TDbParams parameters)
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where DbMethod: TDbManifest;

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.Facade.Core.DbFacadeBase{TDbManifest}" />
        private sealed class DbFacadeCache : InstanceResolver<DbFacade<TDbManifest>>{}
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="DomainFacade.Facade.Core.DbFacadeBase{TDbManifest}" />
        internal sealed class DbMethodsCache : InstanceResolver<TDbManifest> { }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
    /// <typeparam name="TDbFacade">The type of the database facade.</typeparam>
    /// <seealso cref="DomainFacade.Facade.Core.DbFacade{TDbManifest}" />
    internal class Forwarder<TDbManifest,TDbFacade> : DbFacade<TDbManifest> 
        where TDbManifest : DbManifest
        where TDbFacade : DbFacade<TDbManifest>
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
            return CallFacadeAPIDbMethod<TDbFacade, TDbDataModel, TDbParams, DbMethod>(parameters);
        }
    }
}
