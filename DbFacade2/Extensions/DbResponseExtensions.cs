using DbFacade.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DbFacade.Extensions
{
    public static class DbResponseExtensions
    {
        /// <summary>Converts to dbdatamodellist.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <param name="initialize">The initialize.</param>
        /// <param name="index">The index.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IEnumerable<T> ToDbDataModelList<T>(this IDbResponse response, Action<T, IDataCollection> initialize, int index = 0)
            where T : class
        => index >= 0 && response.DbDataTables.Count() > index ? response.DbDataTables.ElementAt(index).ToDbDataModelList(initialize) : Array.Empty<T>();
        /// <summary>Converts to dbdatamodellist.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <param name="index">The index.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IEnumerable<T> ToDbDataModelList<T>(this IDbResponse response, int index = 0)
            where T : class, IDbDataModel
        => !response.HasError && index >= 0 && response.DbDataTables.Count() > index ? response.DbDataTables.ElementAt(index).ToDbDataModelList<T>() : Array.Empty<T>();

    }
}
