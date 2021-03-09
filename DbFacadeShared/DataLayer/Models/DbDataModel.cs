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
    internal static class DbDataModelExtensions
    {
        public static List<ConstructorInfo> GetConstructorInfo(this Type dbDataModelType)
        {
            return dbDataModelType.GetConstructors().ToList().FindAll(constructor =>
                constructor.GetConstructorDbColumns() is List<DbColumn> columns && columns.Any() &&
                constructor.GetParameters().Count() == columns.Count
            );
        }
        public static async Task<List<ConstructorInfo>> GetConstructorInfoAsync(this Type dbDataModelType)
        {
            List<ConstructorInfo> info = dbDataModelType.GetConstructors().ToList().FindAll(constructor =>
                constructor.GetCustomAttributes<DbColumn>() is List<DbColumn> columns && columns.Any() &&
                constructor.GetParameters().Count() == columns.Count
            );
            await Task.CompletedTask;
            return info;
        }
        public static List<DbColumn> GetConstructorDbColumns(this ConstructorInfo constructor)
            => constructor.GetCustomAttributes<DbColumn>().ToList();
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
        public static IEnumerable<PropertyInfo> GetBindableProperties(this Type type)
        => type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop => prop.GetPropertyDbColumns().Any());

        public static async Task<IEnumerable<PropertyInfo>> GetBindablePropertiesAsync(this Type type)
        {
            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop => prop.GetPropertyDbColumns().Any());
            await Task.CompletedTask;
            return properties;
        }
        public static IEnumerable<PropertyInfo> GetNestedProperties(this Type type)
        => type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop=> prop.PropertyType.IsSubclassOf(typeof(DbDataModel)));

        public static async Task<IEnumerable<PropertyInfo>> GetNestedPropertiesAsync(this Type type)
        {
            IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop => prop.PropertyType.IsSubclassOf(typeof(DbDataModel)));
            await Task.CompletedTask;
            return properties;
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
            if (typeof(TDbDataModel).GetConstructorInfo().Count > 0)
                return Create(typeof(TDbDataModel), data) is TDbDataModel tDbDataModel ? tDbDataModel : null;
            var model = GenericInstance.GetInstance<TDbDataModel>();
            model.Init(data);
            return model;
        }
        internal static async Task<TDbDataModel> ToDbDataModelAsync<TDbDataModel>(IDataRecord data)
            where TDbDataModel : DbDataModel
        {
            List<ConstructorInfo> ConstructorInfo = await typeof(TDbDataModel).GetConstructorInfoAsync();
            if (ConstructorInfo.Count > 0)
                return await CreateAsync(typeof(TDbDataModel), data) is TDbDataModel tDbDataModel ? tDbDataModel : null; ;
            var model = await GenericInstance.GetInstanceAsync<TDbDataModel>();
            await model.InitAsync(data);
            return model;
        }

        

        /// <summary>
        ///     Creates the specified database data model type.
        /// </summary>
        /// <param name="dbDataModelType">Type of the database data model.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static IDbDataModel Create(Type dbDataModelType, IDataRecord data)
        {
            var constructorInfo = dbDataModelType.GetConstructorInfo();
            if (constructorInfo.Count > 0)
            {             
                
                try
                {
                    var constructor = constructorInfo.First();
                    var paramInfo = constructor.GetParameters().ToList();
                    var columns = constructor.GetConstructorDbColumns();

                    var args = paramInfo.Select(param => columns[paramInfo.IndexOf(param)].GetValue(data, param.ParameterType));

                    return GenericInstance.GetInstanceWithArgArray(dbDataModelType, args.ToArray()) is IDbDataModel model
                    ? model
                    : null;
                }
                catch (Exception e)
                {
                    DbDataModel model = (DbDataModel) GenericInstance.GetInstance(dbDataModelType);
                    model.AddDataBindingError(DbDataModelBindingError.Create(e, dbDataModelType));
                }
            }
            return null;
        }
        private static async Task<IDbDataModel> CreateAsync(Type dbDataModelType, IDataRecord data)
        {
            var constructorInfo = dbDataModelType.GetConstructorInfo();
            if (constructorInfo.Count > 0)
            {
                try
                {
                    var constructor = constructorInfo.First();
                    var paramInfo = constructor.GetParameters().ToList();
                    var columns = constructor.GetConstructorDbColumns();

                    var args = paramInfo.Select(param => columns[paramInfo.IndexOf(param)].GetValue(data, param.ParameterType));

                    return await GenericInstance.GetInstanceWithArgArrayAsync(dbDataModelType, args.ToArray()) is IDbDataModel model
                    ? model
                    : null;
                }
                catch (Exception e)
                {
                    DbDataModel model = (DbDataModel) await GenericInstance.GetInstanceAsync(dbDataModelType);
                    await model.AddDataBindingErrorAsync(await DbDataModelBindingError.CreateAsync(e, dbDataModelType));
                }
            }
            await Task.CompletedTask;
            return null;
        }
        

        

        private void PopulateNestedProperties(IDataRecord data)
        {
            foreach (var property in GetType().GetNestedProperties())
            {
                if (property.PropertyType.GetConstructorInfo().Count > 0)
                {
                    if (Create(property.PropertyType, data) is DbDataModel model)
                    {
                        property.SetValue(this, model, null);
                        HasNestedDataBindingErrors = model.HasDataBindingErrors;
                    }
                }
                else
                {
                    var instance = GenericInstance.GetInstance(property.PropertyType);
                    if (instance is DbDataModel nestedModel)
                    {
                        nestedModel.Init(data);
                        property.SetValue(this, instance, null);
                        HasNestedDataBindingErrors = nestedModel.HasDataBindingErrors;
                    }
                }
            }
        }
        private async Task PopulateNestedPropertiesAsync(IDataRecord data)
        {
            Type type = GetType();
            foreach (var property in await type.GetNestedPropertiesAsync())
            {
                if ((await property.PropertyType.GetConstructorInfoAsync()).Count > 0)
                {
                    if (await CreateAsync(property.PropertyType, data) is DbDataModel model)
                    {
                        property.SetValue(this, model, null);
                        HasNestedDataBindingErrors = model.HasDataBindingErrors;
                    }
                    
                }
                else
                {
                    var instance = await GenericInstance.GetInstanceAsync(property.PropertyType);
                    if (instance is DbDataModel nestedModel)
                    {
                        await nestedModel.InitAsync(data);
                        property.SetValue(this, instance, null);
                        HasNestedDataBindingErrors = nestedModel.HasDataBindingErrors;
                    }
                }
            }
            await Task.CompletedTask;
        }

        private void Init(IDataRecord data)
        {
            foreach (var property in GetType().GetBindableProperties())
            {
                try
                {
                    var columnAttribute = property.GetColumnAttribute();

                    var propType = property.PropertyType;
                    object value = null;
                    if (columnAttribute != null) value = columnAttribute.GetValue(data, propType);
                    if (value is IDbDataModelBindingError dataBindingError)
                    {
                        AddDataBindingError(dataBindingError);
                    }
                    else if (value != null) 
                    { 
                        property.SetValue(this, value, null);
                    }
                }
                catch (Exception e)
                {
                    AddDataBindingError(DbDataModelBindingError.Create(e, GetType()));
                }
            }
            PopulateNestedProperties(data);
        }
        private async Task InitAsync(IDataRecord data)
        {
            Type type = GetType();
            foreach (var property in await type.GetBindablePropertiesAsync())
            {
                try
                {
                    var columnAttribute = await property.GetColumnAttributeAsync();

                    var propType = property.PropertyType;
                    object value = null;
                    if (columnAttribute != null) value = await columnAttribute.GetValueAsync(data, propType);
                    if (value is IDbDataModelBindingError dataBindingError)
                    {
                        await AddDataBindingErrorAsync(dataBindingError);
                    }
                    else if (value != null)
                    {
                        property.SetValue(this, value, null);
                    }
                }
                catch (Exception e)
                {
                    await AddDataBindingErrorAsync(await DbDataModelBindingError.CreateAsync(e, GetType()));
                }
            }
            await PopulateNestedPropertiesAsync(data);
            await Task.CompletedTask;
        }


    }
}