using DbFacade.DataLayer.ConnectionService;
using DbFacade.DataLayer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbFacadeShared.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataSet
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
    internal class DbDataSet: IDbDataSet
    {
        /// <summary>
        /// Gets or sets the database command settings.
        /// </summary>
        /// <value>
        /// The database command settings.
        /// </value>
        [JsonIgnore]
        private IDbCommandSettings DbCommandSettings { get; set; }

        public DataTable DataTable { get; private set; }
        private static IDbDataSet Create(IDbCommandSettings dbCommandSettings, DataTable dt)
        {
            DbDataSet dataSet = new DbDataSet() { DbCommandSettings = dbCommandSettings, DataTable = dt };
            
            return dataSet;
        }
        public static IEnumerable<IDbDataSet> CreateDataSets(IDbCommandSettings dbCommandSettings, DataSet dataSet)
        {
            List<IDbDataSet> dataSets = new List<IDbDataSet>();
            foreach (DataTable dt in dataSet.Tables)
            {
                dataSets.Add(Create(dbCommandSettings, dt)); //Add first set of Data
            }
            return dataSets;
        }
        /// <summary>
        /// Converts to dbdatamodellist.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        public IEnumerable<TDbDataModel> ToDbDataModelList<TDbDataModel>()
            where TDbDataModel : DbDataModel
        {
            List<TDbDataModel> data = new List<TDbDataModel>();
            foreach (DataRow dataRow in DataTable.Rows)
            {
               TDbDataModel model = DbDataModel.ToDbDataModel<TDbDataModel>(DbCommandSettings, dataRow);
               data.Add(model);
            }
            return data;
        }

        /// <summary>
        /// Converts to dbdatamodellistasync.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TDbDataModel>> ToDbDataModelListAsync<TDbDataModel>()
            where TDbDataModel : DbDataModel
        {
            List<TDbDataModel> data = new List<TDbDataModel>();
            foreach (DataRow dataRow in DataTable.Rows)
            {
                TDbDataModel model = await DbDataModel.ToDbDataModelAsync<TDbDataModel>(DbCommandSettings, dataRow);
                data.Add(model);
            }
            return data;
        }
    }
}
