using DbFacade.DataLayer.Models;
using DbFacade.Factories;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DbFacade.Extensions
{

    public static class DbDataCollectionExtensions
    {
        /// <summary>Gets the value.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static T GetValue<T>(this IDataCollection collection, string key, T defaultValue = default)
        => ConversionFactory.GetValue(collection[key], defaultValue);

        /// <summary>Converts to datetime.</summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <param name="style">The style.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static DateTime? ToDateTime(this IDataCollection collection, string key, string format, DateTimeStyles style = DateTimeStyles.None)
        => collection.ToDateTime(key, format, CultureInfo.InvariantCulture, style);
        /// <summary>Converts to datetime.</summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static DateTime? ToDateTime(this IDataCollection collection, string key, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
        {
            string str = collection.GetValue<string>(key);
            return string.IsNullOrWhiteSpace(str) ? default(DateTime?) :
                DateTime.TryParseExact(str, format, provider, style, out DateTime outValue) ? outValue : default(DateTime?);

        }
        /// <summary>Converts to datetimestring.</summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static string ToDateTimeString(this IDataCollection collection, string key, string format)
        => collection.GetValue<DateTime?>(key) is DateTime dt ? dt.ToString(format) : null;

        /// <summary>Converts to enumerable.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static IEnumerable<T> ToEnumerable<T>(this IDataCollection collection, string key, char separator = ',')
        => ConversionFactory.ToEnumerable<T>(collection.GetValue<string>(key), separator);

        /// <summary>Converts to boolean.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <param name="trueValue">The true value.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static bool ToBoolean<T>(this IDataCollection collection, string key, T trueValue)
            where T : IComparable
        {
            T val = collection.GetValue<T>(key);
            return val != null && val.Equals(trueValue);
        }
        /// <summary>Converts to dbdatamodel.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="createInstance">The create instance.</param>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static T ToDbDataModel<T>(this IDataCollection collection, Func<T> createInstance, Action<T, IDataCollection> initialize)
        {
            try
            {
                T model = createInstance();
                initialize(model, collection);
                return model;
            }
            catch
            {
                return default;
            }
        }
        /// <summary>Converts to dbdatamodel.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static T ToDbDataModel<T>(this IDataCollection collection, Action<T, IDataCollection> initialize)
            where T : class
        => collection.ToDbDataModel(() => TypeFactory.CreateInstance<T>(), initialize);
        /// <summary>Converts to dbdatamodel.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="createInstance">The create instance.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static T ToDbDataModel<T>(this IDataCollection collection, Func<T> createInstance)
            where T : IDbDataModel
        {
            try
            {
                T model = createInstance();
                model.Init(collection);
                return model;
            }
            catch
            {
                return default;
            }
        }
        /// <summary>Converts to dbdatamodel.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static T ToDbDataModel<T>(this IDataCollection collection)
            where T : class, IDbDataModel
        => collection.ToDbDataModel(() => TypeFactory.CreateInstance<T>());
    }
}
