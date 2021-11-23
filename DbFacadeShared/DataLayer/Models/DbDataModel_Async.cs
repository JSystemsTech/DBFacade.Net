using DbFacade.DataLayer.ConnectionService;
using DbFacade.Utils;
using DbFacadeShared.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
namespace DbFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract partial class DbDataModel : IDbDataModel
    {
        /// <summary>
        /// Converts to jsonasync.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ToJsonAsync()
        {
            string json = JsonConvert.SerializeObject(this);
            await Task.CompletedTask;
            return json;
        }
        /// <summary>
        /// Determines whether [is database command asynchronous] [the specified configuration].
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>
        ///   <c>true</c> if [is database command asynchronous] [the specified configuration]; otherwise, <c>false</c>.
        /// </returns>
        protected async Task<bool> IsDbCommandAsync(IDbCommandConfig config)
        {
            bool result = config.CommandId == CommandId;
            await Task.CompletedTask;
            return result;
        }
        /// <summary>
        /// Converts to dbdatamodelasync.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        internal static async Task<TDbDataModel> ToDbDataModelAsync<TDbDataModel>(Guid commandId, IDictionary<string, object> data)
            where TDbDataModel : DbDataModel
        {
            var model = await GenericInstance.GetInstanceAsync<TDbDataModel>();
            model.Data = data;
            model.CommandId = commandId;
            await model.InitAsync();
            return model;
        }
        /// <summary>
        /// Creates the nested model asynchronous.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        protected async Task<TDbDataModel> CreateNestedModelAsync<TDbDataModel>()
            where TDbDataModel : DbDataModel
        {
            var model = await GenericInstance.GetInstanceAsync<TDbDataModel>();
            model.Data = Data;
            model.CommandId = CommandId;
            await model.InitAsync();
            return model;
        }
        /// <summary>
        /// Adds the data binding error asynchronous.
        /// </summary>
        /// <param name="error">The error.</param>
        private async Task AddDataBindingErrorAsync(string error)
        {
            _DataBindingErrors = _DataBindingErrors ?? new List<string>();
            _DataBindingErrors.Add(error);
            await Task.CompletedTask;
        }
        /// <summary>
        /// Tries the get column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">The handler.</param>
        /// <returns></returns>
        private async Task<T> TryGetColumnAsync<T>(Func<Task<(T value, string error)>> handler)
        {
            var result = await handler();
            if (!string.IsNullOrWhiteSpace(result.error))
            {
                await AddDataBindingErrorAsync(result.error);
            }
            return result.value;
        }
        /// <summary>
        /// Gets the column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="convert">The convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected async Task<T> GetColumnAsync<T>(string col, Func<string, T> convert, T defaultValue = default)
            => await TryGetColumnAsync(async () => await Data.GetValueAsync(col, convert, defaultValue));
        /// <summary>
        /// Gets the column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParse">The type of the parse.</typeparam>
        /// <param name="col">The col.</param>
        /// <param name="convert">The convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected async Task<T> GetColumnAsync<T, TParse>(string col, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
            => await TryGetColumnAsync(async () => await Data.GetValueAsync(col, convert, defaultValue));
        /// <summary>
        /// Gets the column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected async Task<T> GetColumnAsync<T>(string col, T defaultValue = default)
        => await TryGetColumnAsync(async () => await Data.GetValueAsync(col, defaultValue));
        /// <summary>
        /// Gets the enumerable column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> GetEnumerableColumnAsync<T>(string col, string delimeter = ",")
            => await TryGetColumnAsync(async () => await Data.GetEnumerableValueAsync<T>(col, delimeter));
        /// <summary>
        /// Gets the date time column asynchronous.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        protected async Task<DateTime?> GetDateTimeColumnAsync(string col, string format, DateTimeStyles style = DateTimeStyles.None)
            => await TryGetColumnAsync(async () => await Data.GetDateTimeValueAsync(col, format, style));
        /// <summary>
        /// Gets the date time column asynchronous.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        protected async Task<DateTime?> GetDateTimeColumnAsync(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
            => await TryGetColumnAsync(async () => await Data.GetDateTimeValueAsync(col, format, provider, style));
        /// <summary>
        /// Gets the formatted date time string column asynchronous.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        protected async Task<string> GetFormattedDateTimeStringColumnAsync(string col, string format)
            => await TryGetColumnAsync(async () => await Data.GetFormattedDateTimeStringValueAsync(col, format));
        /// <summary>
        /// Gets the flag column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="trueValue">The true value.</param>
        /// <returns></returns>
        protected async Task<bool> GetFlagColumnAsync<T>(string col, T trueValue)
        where T : IComparable
            => await TryGetColumnAsync(async () => await Data.GetFlagValueAsync(col, trueValue));
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        protected virtual async Task InitAsync() { await Task.CompletedTask; }
    }
}