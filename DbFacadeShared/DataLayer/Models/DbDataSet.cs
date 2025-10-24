using DbFacade.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataSet
    {
        /// <summary>Converts to dbdatamodellist.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        IEnumerable<T> ToDbDataModelList<T>(Action<T, IDbDataCollection> initialize) where T : class;
        /// <summary>Converts to dbdatamodellist.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///   <br />
        /// </returns>
        IEnumerable<T> ToDbDataModelList<T>() where T : class, IDbDataModel;
    }
    
    internal class DbDataSet : IDbDataSet
    {
        private IEnumerable<IDbDataCollection> DbDataCollections { get; set; }
        private DbDataSet(DataTable dt)
        {
            IDataTableParser dataTableParser = dt.ToDataTableParser();
            List<IDbDataCollection> dbDataCollections = new List<IDbDataCollection>();
            foreach (DataRow dataRow in dt.Rows)
            {
                dbDataCollections.Add(Utils.Utils.MakeInstance<DbDataCollection>(dataRow, dataTableParser));
            }

            DbDataCollections = dbDataCollections;
        }

        internal static IEnumerable<IDbDataSet> CreateDataSets(DataSet dataSet)
        {
            List<IDbDataSet> dataSets = new List<IDbDataSet>();
            foreach (DataTable dt in dataSet.Tables)
            {
                dataSets.Add(new DbDataSet(dt));
            }
            return dataSets;
        }

        public IEnumerable<T> ToDbDataModelList<T>(Action<T, IDbDataCollection> initialize)
            where T : class
        => DbDataCollections.Select(c => c.ToDbDataModel(initialize));
        public IEnumerable<T> ToDbDataModelList<T>()
            where T : class, IDbDataModel
        => DbDataCollections.Select(c => c.ToDbDataModel<T>());
    }
}
