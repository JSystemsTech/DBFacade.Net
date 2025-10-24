using System;
using System.Collections.Generic;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataTable
    {
        /// <summary>Converts to dbdatamodellist.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        IEnumerable<T> ToDbDataModelList<T>(Action<T, IDataCollection> initialize) where T : class;
        /// <summary>Converts to dbdatamodellist.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///   <br />
        /// </returns>
        IEnumerable<T> ToDbDataModelList<T>() where T : class, IDbDataModel;
    }
}
