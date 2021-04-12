using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DbFacade.DataLayer.Models.Attributes;
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
    internal class DbDataModelPropertyProfile
    {
        public Type Type { get; set; }
        public PropertyInfo Property { get; set; }
        public DbColumn DbColumn { get; set; }
        internal static DbDataModelPropertyProfile Create(Type type, PropertyInfo property)
        => new DbDataModelPropertyProfile()
        {
            Type = type,
            Property = property,
            DbColumn = property.GetCustomAttributes<DbColumn>() is IEnumerable<DbColumn> colAttrs && colAttrs.Any() ? colAttrs.First() : null
        };
    }
    internal static class DbDataModelExtensions
    {
        public static List<DbColumn> GetPropertyDbColumns(this PropertyInfo property)
            => property.GetCustomAttributes<DbColumn>().ToList();

        public static DbColumn GetColumnAttribute(this PropertyInfo property)
            => property.GetPropertyDbColumns() is IEnumerable<DbColumn> colAttrs && colAttrs.Any() ? colAttrs.First() : null;

        public static async Task<DbColumn> GetColumnAttributeAsync(this PropertyInfo property)
        {
            var columnAttrs = property.GetPropertyDbColumns() is IEnumerable<DbColumn> colAttrs && colAttrs.Any() ? colAttrs.First() : null;
            await Task.CompletedTask;
            return columnAttrs;
        }
        
        private static IDictionary<Type, IEnumerable<DbDataModelPropertyProfile>> DbDataModelTypePropertiesMap = new Dictionary<Type, IEnumerable<DbDataModelPropertyProfile>>();
        private static void AddDbDataModelTypeProperties(Type type)
            => DbDataModelTypePropertiesMap.Add(type, type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop => prop.GetPropertyDbColumns().Any()).Select(p=> DbDataModelPropertyProfile.Create(type, p)));
        public static IEnumerable<DbDataModelPropertyProfile> GetBindableProperties(this Type type)
        {
            if (!DbDataModelTypePropertiesMap.ContainsKey(type))
            {
                AddDbDataModelTypeProperties(type);
            }
            
            return DbDataModelTypePropertiesMap[type];
        }

        public static async Task<IEnumerable<DbDataModelPropertyProfile>> GetBindablePropertiesAsync(this Type type)
        {
            if (!DbDataModelTypePropertiesMap.ContainsKey(type))
            {
                AddDbDataModelTypeProperties(type);
            }
            await Task.CompletedTask;
            return DbDataModelTypePropertiesMap[type];
        }
        private static IDictionary<Type, IEnumerable<PropertyInfo>> DbDataModelTypeNestedPropertiesMap = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        private static void AddDbDataModelTypeNestedProperties(Type type)
            => DbDataModelTypeNestedPropertiesMap.Add(type, type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop => prop.PropertyType.IsSubclassOf(typeof(DbDataModel))));
        public static IEnumerable<PropertyInfo> GetNestedProperties(this Type type)
        {
            if (!DbDataModelTypeNestedPropertiesMap.ContainsKey(type))
            {
                AddDbDataModelTypeNestedProperties(type);
            }
            return DbDataModelTypeNestedPropertiesMap[type];
        }

        public static async Task<IEnumerable<PropertyInfo>> GetNestedPropertiesAsync(this Type type)
        {
            if (!DbDataModelTypeNestedPropertiesMap.ContainsKey(type))
            {
                AddDbDataModelTypeNestedProperties(type);
            }
            await Task.CompletedTask;
            return DbDataModelTypeNestedPropertiesMap[type];
        }
    }
    [JsonObject]
    [Serializable]
    public abstract class DbDataModel: IDbDataModel
    {
        public string ToJson()=> JsonConvert.SerializeObject(this);
        public async Task<string> ToJsonAsync() {
            string json = JsonConvert.SerializeObject(this);
            await Task.CompletedTask;
            return json;
        }
        [JsonIgnore]
        public IEnumerable<IDbDataModelBindingError> DataBindingErrors { get => _DataBindingErrors; }
        private List<IDbDataModelBindingError> _DataBindingErrors { get; set; }
        private void AddDataBindingError(IDbDataModelBindingError e)
        {
            _DataBindingErrors = _DataBindingErrors ?? new List<IDbDataModelBindingError>();
            _DataBindingErrors.Add(e);
        }
        private async Task AddDataBindingErrorAsync(IDbDataModelBindingError e)
        {
            _DataBindingErrors = _DataBindingErrors ?? new List<IDbDataModelBindingError>();
            _DataBindingErrors.Add(e);
            await Task.CompletedTask;
        }



        public bool HasDataBindingErrors { get => HasNestedDataBindingErrors ||(DataBindingErrors != null && DataBindingErrors.Any()); }
        private bool HasNestedDataBindingErrors { get; set; }


        /// <summary>
        ///     Converts to DbDataModel.
        /// </summary>
        /// <typeparam name="TDbDataModel"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static TDbDataModel ToDbDataModel<TDbDataModel>(IDataRecord data)
            where TDbDataModel : DbDataModel 
        {
            var model = GenericInstance.GetInstance<TDbDataModel>();

            model.InitBase(data);
            return model;
        }
        internal static async Task<TDbDataModel> ToDbDataModelAsync<TDbDataModel>(IDataRecord data)
            where TDbDataModel : DbDataModel
        {
            var model = await GenericInstance.GetInstanceAsync<TDbDataModel>();
            await model.InitBaseAsync(data);
            return model;
        }

        
        private void PopulateNestedProperties(IDataRecord data)
        {
            foreach (var property in GetType().GetNestedProperties())
            {
                
                var instance = GenericInstance.GetInstance(property.PropertyType);
                if (instance is DbDataModel nestedModel)
                {
                    nestedModel.InitBase(data);
                    property.SetValue(this, instance, null);
                    HasNestedDataBindingErrors = nestedModel.HasDataBindingErrors;
                }
            }
        }
        private async Task PopulateNestedPropertiesAsync(IDataRecord data)
        {
            Type type = GetType();
            foreach (var property in await type.GetNestedPropertiesAsync())
            {               
                var instance = await GenericInstance.GetInstanceAsync(property.PropertyType);
                if (instance is DbDataModel nestedModel)
                {
                    await nestedModel.InitBaseAsync(data);
                    property.SetValue(this, instance, null);
                    HasNestedDataBindingErrors = nestedModel.HasDataBindingErrors;
                }
            }
            await Task.CompletedTask;
        }
        protected virtual void Init(IDataRecord data)
        {
            foreach (var propertyProfile in GetType().GetBindableProperties())
            {
                var propType = propertyProfile.Property.PropertyType;
                object value = null;
                object currentValue = propertyProfile.Property.GetValue(this);
                if (propertyProfile.DbColumn != null) value = propertyProfile.DbColumn.GetValue(data, propType, currentValue);

                if (value is IDbDataModelBindingError dataBindingError)
                {
                    AddDataBindingError(dataBindingError);
                }
                else if (value != null)
                {
                    propertyProfile.Property.SetValue(this, value, null);
                }
            }
        }
        private void InitBase(IDataRecord data)
        {
            try
            {
                Init(data);
            }
            catch (Exception e)
            {
                AddDataBindingError(DbDataModelBindingError.Create(e, GetType()));
            }
            
            PopulateNestedProperties(data);
        }

       
        protected virtual async Task InitAsync(IDataRecord data)
        {
            Type type = GetType();
            foreach (var propertyProfile in await type.GetBindablePropertiesAsync())
            {
                var propType = propertyProfile.Property.PropertyType;
                object value = null;
                object currentValue = propertyProfile.Property.GetValue(this);
                if (propertyProfile.DbColumn != null) value = await propertyProfile.DbColumn.GetValueAsync(data, propType, currentValue);
                if (value is IDbDataModelBindingError dataBindingError)
                {
                    await AddDataBindingErrorAsync(dataBindingError);
                }
                else if (value != null)
                {
                    propertyProfile.Property.SetValue(this, value, null);
                }
            }
        }
        private async Task InitBaseAsync(IDataRecord data)
        {
            try
            {
                await InitAsync(data);
            }
            catch (Exception e)
            {
                await AddDataBindingErrorAsync(await DbDataModelBindingError.CreateAsync(e, GetType()));
            }

            await PopulateNestedPropertiesAsync(data);
            await Task.CompletedTask;
        }
    }
}