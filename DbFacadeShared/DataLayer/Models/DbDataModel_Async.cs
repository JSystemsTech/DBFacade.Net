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
        private async Task AddDataBindingErrorAsync(Exception ex)
        {
            _DataBindingErrors = _DataBindingErrors ?? new List<IDbDataModelBindingError>();
            _DataBindingErrors.Add(await DbDataModelBindingError.CreateAsync(ex.InnerException, GetType()));
        }
        private async Task<T> TryGetColumnAsync<T>(Func<Task<T>> handler)
        {
            try{
                return await handler();
            }
            catch(Exception ex)
            {
                await AddDataBindingErrorAsync(ex);
                return default(T);
            }
        }
        protected async Task<T> GetColumnAsync<T>(string col, Func<string, T> convert, T defaultValue = default)
            => await TryGetColumnAsync(async () => await Data.GetValueAsync(col, convert, defaultValue));
        protected async Task<T> GetColumnAsync<T, TParse>(string col, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
            => await TryGetColumnAsync(async () => await Data.GetValueAsync(col, convert, defaultValue));
        protected async Task<T> GetColumnAsync<T>(string col, T defaultValue = default)
        => await TryGetColumnAsync(async () => await Data.GetValueAsync(col, defaultValue));
        protected async Task<IEnumerable<T>> GetEnumerableColumnAsync<T>(string col, string delimeter = ",")
            => await TryGetColumnAsync(async () => await Data.GetEnumerableValueAsync<T>(col, delimeter));
        protected async Task<DateTime?> GetDateTimeColumnAsync(string col, string format, DateTimeStyles style = DateTimeStyles.None)
            => await TryGetColumnAsync(async () => await Data.GetDateTimeValueAsync(col, format, style));
        protected async Task<DateTime?> GetDateTimeColumnAsync(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
            => await TryGetColumnAsync(async () => await Data.GetDateTimeValueAsync(col, format, provider, style));
        protected async Task<string> GetFormattedDateTimeStringColumnAsync(string col, string format)
            => await TryGetColumnAsync(async () => await Data.GetFormattedDateTimeStringValueAsync(col, format));
        protected async Task<bool> GetFlagColumnAsync<T>(string col, T trueValue)
        where T : IComparable
            => await TryGetColumnAsync(async () => await Data.GetFlagValueAsync(col, trueValue));
        protected virtual async Task InitAsync() { await Task.CompletedTask; }
    }
}