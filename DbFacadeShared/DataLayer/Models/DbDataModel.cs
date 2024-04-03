using DbFacade.DataLayer.ConnectionService;
using DbFacade.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataModel:IDataCollectionModel
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns></returns>
        string ToJson();
        /// <summary>
        /// Converts to jsonasync.
        /// </summary>
        /// <returns></returns>
        Task<string> ToJsonAsync();
        /// <summary>
        /// Gets the data binding errors.
        /// </summary>
        /// <value>
        /// The data binding errors.
        /// </value>
        [JsonIgnore]
        IEnumerable<string> DataBindingErrors { get; }
        /// <summary>
        /// Gets a value indicating whether this instance has data binding errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has data binding errors; otherwise, <c>false</c>.
        /// </value>
        bool HasDataBindingErrors { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonObject]
    [Serializable]
    public abstract partial class DbDataModel : IDbDataModel
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns></returns>
        public string ToJson() => JsonConvert.SerializeObject(this);
        /// <summary>
        /// Gets the data binding errors.
        /// </summary>
        /// <value>
        /// The data binding errors.
        /// </value>
        [JsonIgnore]
        public IEnumerable<string> DataBindingErrors { get => _DataBindingErrors; }
        /// <summary>
        /// Gets or sets the data binding errors.
        /// </summary>
        /// <value>
        /// The data binding errors.
        /// </value>
        private List<string> _DataBindingErrors { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [enable data binding errors logging].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable data binding errors logging]; otherwise, <c>false</c>.
        /// </value>
        private bool EnableDataBindingErrorsLogging { get; set; }
        /// <summary>
        /// Adds the data binding error.
        /// </summary>
        /// <param name="error">The error.</param>
        private void AddDataBindingError(string error)
        {
            if (EnableDataBindingErrorsLogging)
            {
                _DataBindingErrors = _DataBindingErrors ?? new List<string>();
                _DataBindingErrors.Add(error);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has data binding errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has data binding errors; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool HasDataBindingErrors { get => DataBindingErrors != null && DataBindingErrors.Any(); }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonIgnore]
        internal IDataCollection Collection { get; set; }

        /// <summary>
        /// Gets or sets the database command settings.
        /// </summary>
        /// <value>
        /// The database command settings.
        /// </value>
        [JsonIgnore]
        internal IDbCommandSettings DbCommandSettings { get; set; }

        /// <summary>
        /// Determines whether [is database command] [the specified configuration].
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>
        ///   <c>true</c> if [is database command] [the specified configuration]; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsDbCommand(IDbCommandConfig config) => config.CommandId == DbCommandSettings.CommandId;

        /// <summary>Converts to dbdatamodel.</summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="dbCommandSettings">The database command settings.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        internal static TDbDataModel ToDbDataModel<TDbDataModel>(IDbCommandSettings dbCommandSettings, object data)
            where TDbDataModel : DbDataModel
        {
            if (Utils.Utils.TryMakeInstance(out TDbDataModel model, data))
            {
                model.DbCommandSettings = dbCommandSettings;
                model.InitInternal();
                return model;
            }
            return default(TDbDataModel);
        }
        /// <summary>
        /// Creates the nested model.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        protected TDbDataModel CreateNestedModel<TDbDataModel>()
            where TDbDataModel : DbDataModel
        {
            TDbDataModel model = Utils.Utils.MakeInstance<TDbDataModel>(Collection);
            if (model != null)
            {
                model.DbCommandSettings = DbCommandSettings;
                model.InitInternal();
                return model;
            }
            return default(TDbDataModel);
        }
        internal void InitInternal() {
            
            try
            {
                EnableDataBindingErrorsLogging = true;
                Init();
            }
            catch (Exception ex)
            {
                AddDataBindingError(ex.Message);
            }
            finally
            {
                EnableDataBindingErrorsLogging = false;
            }  
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected virtual void Init() { }
        /// <summary>
        /// Initializes the data collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void InitDataCollection(IDataCollection collection)
        {
            Collection = collection;
        }

        /// <summary>
        /// Tries the get column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">The handler.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private T TryGetColumn<T>(Func<(T value, string error)> handler)
        {
            var result = handler();
            if (!string.IsNullOrWhiteSpace(result.error))
            {
                AddDataBindingError(result.error);
            }
            return result.value;
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected T GetColumn<T>(string col, T defaultValue = default)
            => TryGetColumn(() => Collection.TryGetValue(col,out T val, defaultValue) ? (val,null): (defaultValue, $"Unable to parse column {col} to type {typeof(T).Name}"));

        /// <summary>
        /// Gets the date time column.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        protected DateTime? GetDateTimeColumn(string col, string format, DateTimeStyles style = DateTimeStyles.None)
            => TryGetColumn(() => Collection.TryGetDateTimeValue(col, format,out DateTime? val, style) ? (val, null) : (default(DateTime?), $"Unable to parse DateTime column {col}"));
        /// <summary>
        /// Gets the date time column.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        protected DateTime? GetDateTimeColumn(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
            => TryGetColumn(() => Collection.TryGetDateTimeValue(col, format, provider, out DateTime? val, style) ? (val, null) : (default(DateTime?), $"Unable to parse DateTime column {col}"));
        /// <summary>
        /// Gets the formatted date time string column.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        protected string GetFormattedDateTimeStringColumn(string col, string format)
            => TryGetColumn(() => Collection.TryGetFormattedDateTimeStringValue(col, format, out string val) ? (val, null) : (default(string), $"Unable to parse formatted DateTime string column {col}"));
        /// <summary>
        /// Gets the enumerable column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        protected IEnumerable<T> GetEnumerableColumn<T>(string col, string delimeter)
            => TryGetColumn(() => Collection.TryGetEnumerable(col, delimeter[0], out IEnumerable<T> val) ?(val,null) :((IEnumerable<T>)Array.Empty<T>(), $"Unable to parse column {col} to Enumerable type {typeof(T).Name}"));
        /// <summary>
        /// Gets the enumerable column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        protected IEnumerable<T> GetEnumerableColumn<T>(string col, char delimeter)
            => TryGetColumn(() => Collection.TryGetEnumerable(col, delimeter, out IEnumerable<T> val) ? (val, null) : ((IEnumerable<T>)Array.Empty<T>(), $"Unable to parse column {col} to Enumerable type {typeof(T).Name}"));
        /// <summary>
        /// Gets the enumerable column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <returns></returns>
        protected IEnumerable<T> GetEnumerableColumn<T>(string col)
            => TryGetColumn(() => Collection.TryGetEnumerable(col, out IEnumerable<T> val) ? (val, null) : ((IEnumerable<T>)Array.Empty<T>(), $"Unable to parse column {col} to Enumerable type {typeof(T).Name}"));
        /// <summary>
        /// Gets the flag column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="trueValue">The true value.</param>
        /// <returns></returns>
        protected bool GetFlagColumn<T>(string col, T trueValue)
            where T : IComparable
            => TryGetColumn(() => Collection.TryGetFlagValue(col, trueValue, out bool val) ? (val, null) :(false, $"Unable to parse flag column {col}"));

        
    }


}