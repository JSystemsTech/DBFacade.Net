using DbFacade.DataLayer.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbFacade.Extensions
{
    internal static class DataTableExtensions
    {
        internal static IEnumerable<IDataCollection> ToDataCollectionList(this DataTable dt)
        {
            string[] keys = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
            return dt.Rows.Cast<DataRow>().Select(dr => new DataRowDataCollection(dr, keys));
        }
        internal static IEnumerable<IDbDataTable> ToDbDataTables(this DataSet dataSet)
        {
            List<IDbDataTable> dataSets = new List<IDbDataTable>();
            foreach (DataTable dt in dataSet.Tables)
            {
                dataSets.Add(new DbDataTable(dt));
            }
            return dataSets;
        }
    }
}
