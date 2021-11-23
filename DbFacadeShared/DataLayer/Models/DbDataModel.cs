using DbFacade.DataLayer.ConnectionService;
using DbFacade.Utils;
using DbFacadeShared.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataModel
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
        bool HasDataBindingErrors { get;  }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonObject]
    [Serializable]
    public abstract partial class DbDataModel: IDbDataModel
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns></returns>
        public string ToJson()=> JsonConvert.SerializeObject(this);
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
        /// Adds the data binding error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void AddDataBindingError(string error)
        {
            _DataBindingErrors = _DataBindingErrors ?? new List<string>();
            _DataBindingErrors.Add(error);
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
        internal IDictionary<string, object> Data { get; set; }
        /// <summary>
        /// Gets or sets the command identifier.
        /// </summary>
        /// <value>
        /// The command identifier.
        /// </value>
        [JsonIgnore]
        internal Guid CommandId { get; set; }

        /// <summary>
        /// Determines whether [is database command] [the specified configuration].
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>
        ///   <c>true</c> if [is database command] [the specified configuration]; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsDbCommand(IDbCommandConfig config) => config.CommandId == CommandId;
        /// <summary>
        /// Converts to dbdatamodel.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        internal static TDbDataModel ToDbDataModel<TDbDataModel>(Guid commandId, IDictionary<string, object> data)
            where TDbDataModel : DbDataModel 
        {
            var model = GenericInstance.GetInstance<TDbDataModel>();
            model.Data = data;
            model.CommandId = commandId;
            model.Init();
            return model;
        }
        /// <summary>
        /// Creates the nested model.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        protected TDbDataModel CreateNestedModel<TDbDataModel>()
            where TDbDataModel : DbDataModel
        {
            var model = GenericInstance.GetInstance<TDbDataModel>();
            model.Data = Data;
            model.CommandId = CommandId;
            model.Init();
            return model;
        }
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected virtual void Init() { }

        /// <summary>Tries the get column.</summary>
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
        /// <param name="convert">The convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected T GetColumn<T>(string col, Func<string, T> convert, T defaultValue = default)
            => TryGetColumn(() => Data.GetValue<T, string>(col, convert, defaultValue));
        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParse">The type of the parse.</typeparam>
        /// <param name="col">The col.</param>
        /// <param name="convert">The convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected T GetColumn<T, TParse>(string col, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
            => TryGetColumn(() => Data.GetValue(col, convert, defaultValue));
        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected T GetColumn<T>(string col, T defaultValue = default)
            => TryGetColumn(() => Data.GetValue(col, defaultValue));

        /// <summary>
        /// Gets the date time column.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        protected DateTime? GetDateTimeColumn(string col, string format, DateTimeStyles style = DateTimeStyles.None)
            => TryGetColumn(() => Data.GetDateTimeValue(col,format, style));
        /// <summary>
        /// Gets the date time column.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        protected DateTime? GetDateTimeColumn(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
            => TryGetColumn(() => Data.GetDateTimeValue(col, format, provider, style));
        /// <summary>
        /// Gets the formatted date time string column.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        protected string GetFormattedDateTimeStringColumn(string col, string format)
            => TryGetColumn(() => Data.GetFormattedDateTimeStringValue(col, format));
        /// <summary>
        /// Gets the enumerable column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        protected IEnumerable<T> GetEnumerableColumn<T>(string col, string delimeter = ",")
            => TryGetColumn(() =>Data.GetEnumerableValue<T>(col,delimeter));
        /// <summary>
        /// Gets the flag column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="trueValue">The true value.</param>
        /// <returns></returns>
        protected bool GetFlagColumn<T>(string col, T trueValue)
            where T: IComparable
            => TryGetColumn(() => Data.GetFlagValue<T>(col, trueValue));
    }

    
}