using DbFacade.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbFacade.DataLayer.Models
{    
    internal class DbDataTable : IDbDataTable
    {
        private readonly IEnumerable<IDataCollection> DbDataCollections;
        internal DbDataTable(DataTable dt)
        {
            DbDataCollections = dt.ToDataCollectionList();
        }

        public IEnumerable<T> ToDbDataModelList<T>(Action<T, IDataCollection> initialize)
            where T : class
        => DbDataCollections.Select(c => c.ToDbDataModel(initialize));
        public IEnumerable<T> ToDbDataModelList<T>()
            where T : class, IDbDataModel
        => DbDataCollections.Select(c => c.ToDbDataModel<T>());
    }
}
