using DbFacade.DataLayer.ConnectionService;
using DbFacade.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
namespace DbFacade.DataLayer.Models
{
    public abstract partial class DbDataModel : IDbDataModel
    {
        public async Task<string> ToJsonAsync()
        {
            string json = JsonConvert.SerializeObject(this);
            await Task.CompletedTask;
            return json;
        }
        protected async Task<bool> IsDbCommandAsync(IDbCommandConfig config)
        {
            bool result = config.CommandId == CommandId;
            await Task.CompletedTask;
            return result;
        }
        internal static async Task<TDbDataModel> ToDbDataModelAsync<TDbDataModel>(Guid commandId, IDictionary<string, object> data)
            where TDbDataModel : DbDataModel
        {
            var model = await GenericInstance.GetInstanceAsync<TDbDataModel>();
            model.Data = data;
            model.CommandId = commandId;
            await model.InitAsync();
            return model;
        }
        protected async Task<TDbDataModel> CreateNestedModelAsync<TDbDataModel>()
            where TDbDataModel : DbDataModel
        {
            var model = await GenericInstance.GetInstanceAsync<TDbDataModel>();
            model.Data = Data;
            model.CommandId = CommandId;
            await model.InitAsync();
            return model;
        }

        protected async Task<T> GetColumnAsync<T>(string col, Func<string, T> convert, T defaultValue = default)
            => await GetColumnAsync<T, string>(col, convert, defaultValue);
        protected async Task<T> GetColumnAsync<T, TParse>(string col, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
        {
            T result = GetColumn(col, convert, defaultValue);
            await Task.CompletedTask;
            return result;
        }
        protected async Task<T> GetColumnAsync<T>(string col, T defaultValue = default)
        {
            T result = GetColumn(col, defaultValue);
            await Task.CompletedTask;
            return result;
        }
        protected async Task<IEnumerable<T>> GetEnumerableColumnAsync<T>(string col, string delimeter = ",")
        {
            IEnumerable<T> result = GetEnumerableColumn<T>(col, delimeter);
            await Task.CompletedTask;
            return result;
        }
        protected async Task<DateTime?> GetDateTimeColumnAsync(string col, string format, DateTimeStyles style = DateTimeStyles.None)
            => await GetDateTimeColumnAsync(col, format, CultureInfo.InvariantCulture, style);
        protected async Task<DateTime?> GetDateTimeColumnAsync(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
        {
            DateTime? result = GetDateTimeColumn(col, format, provider, style);
            await Task.CompletedTask;
            return result;
        }
        protected async Task<string> GetFormattedDateTimeStringColumnAsync(string col, string format)
        {
            string result = GetFormattedDateTimeStringColumn(col, format);
            await Task.CompletedTask;
            return result;
        }
        protected async Task<bool> GetFlagColumnAsync<T>(string col, T trueValue)
            where T : IComparable
        {
            bool result = GetFlagColumn(col, trueValue);
            await Task.CompletedTask;
            return result;
        }
        protected virtual async Task InitAsync() { await Task.CompletedTask; }
    }
}