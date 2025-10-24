using DbFacade.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    ///   <br />
    /// </summary>
    public interface IDbDataCollection
    {
        /// <summary>
        /// Gets the data binding errors.
        /// </summary>
        /// <value>
        /// The data binding errors.
        /// </value>
        IEnumerable<string> DataBindingErrors { get; }
        /// <summary>Converts to dbdatamodel.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initialize">The initialize.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        T ToDbDataModel<T>(Action<T, IDbDataCollection> initialize) where T : class;
        /// <summary>Converts to dbdatamodel.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        ///   <br />
        /// </returns>
        T ToDbDataModel<T>() where T : class,IDbDataModel;
        /// <summary>Gets the column.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        T GetColumn<T>(string col, T defaultValue = default);

        /// <summary>Gets the date time column.</summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="style">The style.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        DateTime? GetDateTimeColumn(string col, string format, DateTimeStyles style = DateTimeStyles.None);
        /// <summary>Gets the date time column.</summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        DateTime? GetDateTimeColumn(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None);
        /// <summary>Gets the formatted date time string column.</summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        string GetFormattedDateTimeStringColumn(string col, string format);
        /// <summary>Gets the enumerable column.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        IEnumerable<T> GetEnumerableColumn<T>(string col, string delimeter);
        /// <summary>Gets the enumerable column.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        IEnumerable<T> GetEnumerableColumn<T>(string col, char delimeter);
        /// <summary>Gets the enumerable column.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        IEnumerable<T> GetEnumerableColumn<T>(string col);
        /// <summary>Gets the flag column.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="trueValue">The true value.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool GetFlagColumn<T>(string col, T trueValue) where T : IComparable;
    }
    public interface IDbDataModel
    {
        void Init(IDbDataCollection collection);
    }
    internal class DbDataCollection : IDbDataCollection, IDataCollectionModel
    {
        internal static IDbDataCollection Empty = new DbDataCollection() { };
        private bool RecordDataBindingErrors { get; set; }
        public IEnumerable<string> DataBindingErrors { get => DataBindingErrorsList; }
        private List<string> DataBindingErrorsList { get; set; }
        private IDataCollection Collection { get; set; }
        public void InitDataCollection(IDataCollection collection)
        {
            Collection = collection;
            RecordDataBindingErrors = true;
        }
        public DbDataCollection() {
            DataBindingErrorsList = new List<string>();
        }

        public T ToDbDataModel<T>(Action<T, IDbDataCollection> initialize)
            where T : class
        {
            if (Utils.Utils.TryMakeInstance(out T model))
            {
                initialize(model, this);
                return model;
            }
            return default(T);
        }
        public T ToDbDataModel<T>()
            where T : class, IDbDataModel
        {
            if (Utils.Utils.TryMakeInstance(out T model))
            {
                model.Init(this);
                return model;
            }
            return default(T);
        }

        private T TryGetColumn<T>(Func<(T value, string error)> handler, T defaultValue = default(T))
        {
            if(Collection == null)
            {
                return defaultValue;
            }
            var result = handler();
            if (!string.IsNullOrWhiteSpace(result.error) && !DataBindingErrorsList.Contains(result.error))
            {
                DataBindingErrorsList.Add(result.error);
            }
            return result.value;
        }
        public T GetColumn<T>(string col, T defaultValue = default(T))
            => TryGetColumn(() => Collection.TryGetValue(col,out T val, defaultValue) ? (val,null): (defaultValue, $"Unable to parse column {col} to type {typeof(T).Name}"), defaultValue);

        public DateTime? GetDateTimeColumn(string col, string format, DateTimeStyles style = DateTimeStyles.None)
            => TryGetColumn(() => Collection.TryGetDateTimeValue(col, format,out DateTime? val, style) ? (val, null) : (default(DateTime?), $"Unable to parse DateTime column {col}"));
        
        public DateTime? GetDateTimeColumn(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
            => TryGetColumn(() => Collection.TryGetDateTimeValue(col, format, provider, out DateTime? val, style) ? (val, null) : (default(DateTime?), $"Unable to parse DateTime column {col}"));
       
        public string GetFormattedDateTimeStringColumn(string col, string format)
            => TryGetColumn(() => Collection.TryGetFormattedDateTimeStringValue(col, format, out string val) ? (val, null) : (default(string), $"Unable to parse formatted DateTime string column {col}"));
        
        public IEnumerable<T> GetEnumerableColumn<T>(string col, string delimeter)
            => TryGetColumn(() => Collection.TryGetEnumerable(col, delimeter[0], out IEnumerable<T> val) ?(val,null) :((IEnumerable<T>)Array.Empty<T>(), $"Unable to parse column {col} to Enumerable type {typeof(T).Name}"));
        
        public IEnumerable<T> GetEnumerableColumn<T>(string col, char delimeter)
            => TryGetColumn(() => Collection.TryGetEnumerable(col, delimeter, out IEnumerable<T> val) ? (val, null) : ((IEnumerable<T>)Array.Empty<T>(), $"Unable to parse column {col} to Enumerable type {typeof(T).Name}"));
        
        public IEnumerable<T> GetEnumerableColumn<T>(string col)
            => TryGetColumn(() => Collection.TryGetEnumerable(col, out IEnumerable<T> val) ? (val, null) : ((IEnumerable<T>)Array.Empty<T>(), $"Unable to parse column {col} to Enumerable type {typeof(T).Name}"));
        
        public bool GetFlagColumn<T>(string col, T trueValue)
            where T : IComparable
            => TryGetColumn(() => Collection.TryGetFlagValue(col, trueValue, out bool val) ? (val, null) :(false, $"Unable to parse flag column {col}"));
                
    }


}