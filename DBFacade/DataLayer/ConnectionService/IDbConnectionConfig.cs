using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models;
using System.Data;
using System.Threading.Tasks;

namespace DBFacade.DataLayer.ConnectionService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbConnectionConfig
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection GetDbConnection();
        /// <summary>
        /// Executes the database action.
        /// </summary>
        /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbManifestMethod">The type of the database manifest method.</typeparam>
        /// <param name="method">The method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IDbResponse<TDbDataModel> ExecuteDbAction<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;

        /// <summary>
        /// Executes the database action asynchronous.
        /// </summary>
        /// <typeparam name="TDbManifest">The type of the database manifest.</typeparam>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <typeparam name="TDbParams">The type of the database parameters.</typeparam>
        /// <typeparam name="TDbManifestMethod">The type of the database manifest method.</typeparam>
        /// <param name="method">The method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IDbResponse<TDbDataModel>> ExecuteDbActionAsync<TDbManifest, TDbDataModel, TDbParams, TDbManifestMethod>(TDbManifestMethod method, TDbParams parameters)
            where TDbManifest : DbManifest
            where TDbDataModel : DbDataModel
            where TDbParams : IDbParamsModel
            where TDbManifestMethod : TDbManifest;
    }
}
