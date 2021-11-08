using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.Utils;
using DbFacadeShared.Extensions;
using Newtonsoft.Json;

namespace DbFacade.DataLayer.Models
{
    public interface IDbDataModel
    {
        string ToJson();
        Task<string> ToJsonAsync();
        [JsonIgnore]
        IEnumerable<IDbDataModelBindingError> DataBindingErrors { get; }
        bool HasDataBindingErrors { get;  }
    }

    [JsonObject]
    [Serializable]
    public abstract partial class DbDataModel: IDbDataModel
    {
        public string ToJson()=> JsonConvert.SerializeObject(this);
        [JsonIgnore]
        public IEnumerable<IDbDataModelBindingError> DataBindingErrors { get => _DataBindingErrors; }
        private List<IDbDataModelBindingError> _DataBindingErrors { get; set; }
        private void AddDataBindingError(Exception ex)
        {
            _DataBindingErrors = _DataBindingErrors ?? new List<IDbDataModelBindingError>();
            _DataBindingErrors.Add(DbDataModelBindingError.Create(ex.InnerException,GetType()));
        }
        [JsonIgnore]
        public bool HasDataBindingErrors { get => DataBindingErrors != null && DataBindingErrors.Any(); }
        [JsonIgnore]
        internal IDictionary<string, object> Data { get; set; }
        [JsonIgnore]
        internal Guid CommandId { get; set; }

        protected bool IsDbCommand(IDbCommandConfig config) => config.CommandId == CommandId;
        internal static TDbDataModel ToDbDataModel<TDbDataModel>(Guid commandId, IDictionary<string, object> data)
            where TDbDataModel : DbDataModel 
        {
            var model = GenericInstance.GetInstance<TDbDataModel>();
            model.Data = data;
            model.CommandId = commandId;
            model.Init();
            return model;
        }
        protected TDbDataModel CreateNestedModel<TDbDataModel>()
            where TDbDataModel : DbDataModel
        {
            var model = GenericInstance.GetInstance<TDbDataModel>();
            model.Data = Data;
            model.CommandId = CommandId;
            model.Init();
            return model;
        }
        protected virtual void Init() { }

        private T TryGetColumn<T>(Func<T> handler)
        {
            try
            {
                return handler();
            }
            catch (Exception ex)
            {
                AddDataBindingError(ex);
                return default(T);
            }
        }
        protected T GetColumn<T>(string col, Func<string, T> convert, T defaultValue = default)
            => TryGetColumn(() => Data.GetValue<T, string>(col, convert, defaultValue));
        protected T GetColumn<T, TParse>(string col, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
            => TryGetColumn(() => Data.GetValue(col, convert, defaultValue));
        protected T GetColumn<T>(string col, T defaultValue = default)
            => TryGetColumn(() => Data.GetValue(col, defaultValue));

        protected DateTime? GetDateTimeColumn(string col, string format, DateTimeStyles style = DateTimeStyles.None)
            => TryGetColumn(() => Data.GetDateTimeValue(col,format, style));
        protected DateTime? GetDateTimeColumn(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
            => TryGetColumn(() => Data.GetDateTimeValue(col, format, provider, style));
        protected string GetFormattedDateTimeStringColumn(string col, string format)
            => TryGetColumn(() => Data.GetFormattedDateTimeStringValue(col, format));
        protected IEnumerable<T> GetEnumerableColumn<T>(string col, string delimeter = ",")
            => TryGetColumn(() =>Data.GetEnumerableValue<T>(col,delimeter));
        protected bool GetFlagColumn<T>(string col, T trueValue)
            where T: IComparable
            => TryGetColumn(() => Data.GetFlagValue<T>(col, trueValue));
    }

    
}