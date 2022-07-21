using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbFacadeShared.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataSet: IEnumerable<IDictionary<string, object>>
    {
        /// <summary>
        /// Converts to dbdatamodellist.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        IEnumerable<TDbDataModel> ToDbDataModelList<TDbDataModel>() where TDbDataModel : DbDataModel;
        /// <summary>
        /// Converts to dbdatamodellistasync.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        Task<IEnumerable<TDbDataModel>> ToDbDataModelListAsync<TDbDataModel>() where TDbDataModel : DbDataModel;
    }
    /// <summary>
    ///   <br />
    /// </summary>
    internal class DbDataSet: List<IDictionary<string, object>>, IDbDataSet
    {
        /// <summary>
        /// Gets or sets the database command settings.
        /// </summary>
        /// <value>
        /// The database command settings.
        /// </value>
        [JsonIgnore]
        private IDbCommandSettings DbCommandSettings { get; set; }

        /// <summary>
        /// Creates the specified database command settings.
        /// </summary>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <returns></returns>
        internal static DbDataSet Create(IDbCommandSettings dbCommandSettings) => new DbDataSet() { DbCommandSettings = dbCommandSettings };
        /// <summary>
        /// Converts to dbdatamodellist.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        public IEnumerable<TDbDataModel> ToDbDataModelList<TDbDataModel>()
            where TDbDataModel : DbDataModel
        => this.Select(dataRow => DbDataModel.ToDbDataModel<TDbDataModel>(DbCommandSettings, dataRow));

        /// <summary>
        /// Converts to dbdatamodellistasync.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TDbDataModel>> ToDbDataModelListAsync<TDbDataModel>()
            where TDbDataModel : DbDataModel
        {
            List<TDbDataModel> data = new List<TDbDataModel>();
            foreach(var dataRow in this)
            {
                TDbDataModel model = await DbDataModel.ToDbDataModelAsync<TDbDataModel>(DbCommandSettings, dataRow);
                data.Add(model);
            }
            return data;
        }
    }
}
