using DbFacade.DataLayer.ConnectionService;
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
            bool result = config.CommandId == DbCommandSettings.CommandId;
            await Task.CompletedTask;
            return result;
        }

        
        internal static async Task<TDbDataModel> ToDbDataModelAsync<TDbDataModel>(IDbCommandSettings dbCommandSettings, object data)
            where TDbDataModel : DbDataModel
        {
            if (Utils.Utils.TryMakeInstance(out TDbDataModel model, data))
            {
                model.DbCommandSettings = dbCommandSettings;
                await model.InitInternalAsync();
                return model;
            }
            await Task.CompletedTask;
            return default(TDbDataModel);
        }
        /// <summary>
        /// Creates the nested model asynchronous.
        /// </summary>
        /// <typeparam name="TDbDataModel">The type of the database data model.</typeparam>
        /// <returns></returns>
        protected async Task<TDbDataModel> CreateNestedModelAsync<TDbDataModel>()
            where TDbDataModel : DbDataModel
        {
            TDbDataModel model = Utils.Utils.MakeInstance<TDbDataModel>(Collection);
            if (model != null)
            {
                model.DbCommandSettings = DbCommandSettings;
                await model.InitInternalAsync();
                return model;
            }
            await Task.CompletedTask;
            return default(TDbDataModel);
        }
        internal async Task InitInternalAsync()
        {            
            try
            {
                EnableDataBindingErrorsLogging = true;                
                await InitAsync();
            }
            catch (Exception ex)
            {
                await AddDataBindingErrorAsync(ex.Message);
            }
            finally
            {
                EnableDataBindingErrorsLogging = false;
            }            
        }
        /// <summary>
        /// Adds the data binding error asynchronous.
        /// </summary>
        /// <param name="error">The error.</param>
        private async Task AddDataBindingErrorAsync(string error)
        {
            AddDataBindingError(error);
            await Task.CompletedTask;
        }


        /// <summary>
        /// Gets the column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        protected async Task<T> GetColumnAsync<T>(string col, T defaultValue = default)
        {
            await Task.CompletedTask;
            return GetColumn(col, defaultValue);
        }
        /// <summary>
        /// Gets the enumerable column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> GetEnumerableColumnAsync<T>(string col, string delimeter)
        {
            await Task.CompletedTask;
            return GetEnumerableColumn<T>(col, delimeter);
        }
        /// <summary>
        /// Gets the enumerable column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="delimeter">The delimeter.</param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> GetEnumerableColumnAsync<T>(string col, char delimeter)
        {
            await Task.CompletedTask;
            return GetEnumerableColumn<T>(col, delimeter);
        }
        /// <summary>
        /// Gets the enumerable column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> GetEnumerableColumnAsync<T>(string col)
        {
            await Task.CompletedTask;
            return GetEnumerableColumn<T>(col);
        }
        /// <summary>
        /// Gets the date time column asynchronous.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        protected async Task<DateTime?> GetDateTimeColumnAsync(string col, string format, DateTimeStyles style = DateTimeStyles.None)
        {
            await Task.CompletedTask;
            return GetDateTimeColumn(col, format, style);
        }
        /// <summary>
        /// Gets the date time column asynchronous.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        protected async Task<DateTime?> GetDateTimeColumnAsync(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
        {
            await Task.CompletedTask;
            return GetDateTimeColumn(col, format, provider, style);
        }
        /// <summary>
        /// Gets the formatted date time string column asynchronous.
        /// </summary>
        /// <param name="col">The col.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        protected async Task<string> GetFormattedDateTimeStringColumnAsync(string col, string format)
        {
            await Task.CompletedTask;
            return GetFormattedDateTimeStringColumn(col, format);
        }
        /// <summary>
        /// Gets the flag column asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="col">The col.</param>
        /// <param name="trueValue">The true value.</param>
        /// <returns></returns>
        protected async Task<bool> GetFlagColumnAsync<T>(string col, T trueValue)
        where T : IComparable
        {
            await Task.CompletedTask;
            return GetFlagColumn(col, trueValue);
        }
        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        protected virtual async Task InitAsync() { await Task.CompletedTask; }
    }
}