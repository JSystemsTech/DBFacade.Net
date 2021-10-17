using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DbFacade.DataLayer.ConnectionService;
using DbFacade.Utils;
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
        private void AddDataBindingError(IDbDataModelBindingError e)
        {
            _DataBindingErrors = _DataBindingErrors ?? new List<IDbDataModelBindingError>();
            _DataBindingErrors.Add(e);
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
        private T Parse<T>(object value, T defaultValue = default)
        {
            try
            {
                Type returnType = typeof(T);
                Type underlyingType = Nullable.GetUnderlyingType(returnType);
                Type castType = underlyingType != null ? underlyingType : returnType;
                if (value is T tVal)
                {
                    return tVal;
                }
                if (castType == typeof(string))
                {
                    return (T)(object)value.ToString();
                }
                else if (castType == typeof(MailAddress))
                {
                    return (T)(object)new MailAddress(value.ToString());
                }
                return IsEnum(castType, value) && value != null && value is int intValue ?
                    (T)Enum.ToObject(castType, intValue) :
                    (T)Convert.ChangeType(value, castType);
            }
            catch
            {
                return defaultValue;
            }
        }
        private string GetColumnName(string col)
            => Data.ContainsKey(col) ? col :
            Data.Keys.FirstOrDefault(key => string.Equals(key, col, StringComparison.CurrentCultureIgnoreCase));
        private static bool IsEnum(Type enumType, object value)
        {
            try
            {
                return Enum.IsDefined(enumType, value);
            }
            catch
            {
                return false;
            }
        }
        protected T GetColumn<T>(string col, Func<string, T> convert, T defaultValue = default)
            => GetColumn<T, string>(col, convert, defaultValue);
        protected T GetColumn<T, TParse>(string col, Func<TParse, T> convert, T defaultValue = default)
            where TParse : IComparable
        {
            TParse defaultParseValue = default(TParse);
            TParse parseValue = GetColumn(col, defaultParseValue);
            return parseValue.CompareTo(defaultParseValue) == 0 ? defaultValue : convert(parseValue);
        }
        protected T GetColumn<T>(string col, T defaultValue = default)
        {
            string name = GetColumnName(col);
            if(name == null)
            {
                AddDataBindingError(DbDataModelBindingError.Create(new Exception($"No column named '{col}' exists"), GetType()));
            }
            return name != null &&
                Data.TryGetValue(name, out object value) && 
                value != null && 
                value != DBNull.Value ? 
                Parse(value, defaultValue) : defaultValue;
        }
        
        protected DateTime? GetDateTimeColumn(string col, string format, DateTimeStyles style = DateTimeStyles.None)
            => GetDateTimeColumn(col,format, CultureInfo.InvariantCulture, style);
        protected DateTime? GetDateTimeColumn(string col, string format, IFormatProvider provider, DateTimeStyles style = DateTimeStyles.None)
        => GetColumn(col, value =>
            !string.IsNullOrWhiteSpace(value) &&
            DateTime.TryParseExact(value, format, provider, style, out DateTime outValue) ? outValue : (DateTime?)null        
            );
        protected string GetFormattedDateTimeStringColumn(string col, string format)
        => GetColumn<DateTime?>(col) is DateTime convertedValue  ? convertedValue.ToString(format): null;
        protected IEnumerable<T> GetEnumerableColumn<T>(string col, string delimeter = ",")
        => GetColumn(col, value => value.Split(delimeter.ToArray()).Select(v => Parse<T>(v)));
        protected bool GetFlagColumn<T>(string col, T trueValue)
            where T: IComparable
        => GetColumn<T>(col).CompareTo(trueValue) == 0;
    }

    
}