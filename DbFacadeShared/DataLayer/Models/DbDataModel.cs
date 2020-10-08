using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DbFacade.DataLayer.Manifest;
using DbFacade.DataLayer.Models.Attributes;
using DbFacade.Exceptions;
using DbFacade.Utils;
using DbFacadeShared.DataLayer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DbFacade.DataLayer.Models
{
    public interface IDbDataModel
    {
        string ToJson();
        Task<string> ToJsonAsync();
        IEnumerable<IDbDataModelBindingError> DataBindingErrors { get; }
        bool HasDataBindingErrors { get;  }
        void Init<TDbMethodManifestMethod>(IDataRecord data)
            where TDbMethodManifestMethod : IDbManifestMethod;
        Task InitAsync<TDbMethodManifestMethod>(IDataRecord data)
           where TDbMethodManifestMethod : IDbManifestMethod;
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
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static TDbDataModel ToDbDataModel<TDbDataModel, TDbMethodManifestMethod>(IDataRecord data)
            where TDbDataModel : DbDataModel where TDbMethodManifestMethod : IDbManifestMethod
        {
            if (GetConstructorInfo<TDbMethodManifestMethod>(typeof(TDbDataModel)).Count > 0)
                return Create<TDbDataModel, TDbMethodManifestMethod>(data);
            var model = GenericInstance.GetInstance<TDbDataModel>();
            model.Init<TDbMethodManifestMethod>(data);
            return model;
        }
        internal static async Task<TDbDataModel> ToDbDataModelAsync<TDbDataModel, TDbMethodManifestMethod>(IDataRecord data)
            where TDbDataModel : DbDataModel where TDbMethodManifestMethod : IDbManifestMethod
        {
            List<ConstructorInfo> ConstructorInfo = await GetConstructorInfoAsync<TDbMethodManifestMethod>(typeof(TDbDataModel));
            if (ConstructorInfo.Count > 0)
                return await CreateAsync<TDbDataModel, TDbMethodManifestMethod>(data);
            var model = await GenericInstance.GetInstanceAsync<TDbDataModel>();
            await model.InitAsync<TDbMethodManifestMethod>(data);
            return model;
        }
        /// <summary>
        ///     Creates the specified data.
        /// </summary>
        /// <typeparam name="TDbDataModel"></typeparam>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static TDbDataModel Create<TDbDataModel, TDbMethodManifestMethod>(IDataRecord data)
            where TDbDataModel : DbDataModel where TDbMethodManifestMethod : IDbManifestMethod
            => Create<TDbMethodManifestMethod>(typeof(TDbDataModel), data) is TDbDataModel model ? model: null;
        private static async Task<TDbDataModel> CreateAsync<TDbDataModel, TDbMethodManifestMethod>(IDataRecord data)
            where TDbDataModel : DbDataModel where TDbMethodManifestMethod : IDbManifestMethod
            => await CreateAsync<TDbMethodManifestMethod>(typeof(TDbDataModel), data) is TDbDataModel model ? model : null;

        /// <summary>
        ///     Gets the constructor information.
        /// </summary>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="dbDataModelType">Type of the database data model.</param>
        /// <returns></returns>
        private static List<ConstructorInfo> GetConstructorInfo<TDbMethodManifestMethod>(Type dbDataModelType)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            return dbDataModelType.GetConstructors().ToList().FindAll(constructor =>
                constructor.GetCustomAttributes<DbColumn>().Any() &&
                constructor.GetParameters().Count() ==
                constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                    column.BoundToTDbMethodManifestMethodType && column.TDbMethodManifestMethodType.FullName ==
                    typeof(TDbMethodManifestMethod).FullName).Count
            );
        }
        private static async Task<List<ConstructorInfo>> GetConstructorInfoAsync<TDbMethodManifestMethod>(Type dbDataModelType)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            List<ConstructorInfo> info=  dbDataModelType.GetConstructors().ToList().FindAll(constructor =>
                constructor.GetCustomAttributes<DbColumn>().Any() &&
                constructor.GetParameters().Count() ==
                constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                    column.BoundToTDbMethodManifestMethodType && column.TDbMethodManifestMethodType.FullName ==
                    typeof(TDbMethodManifestMethod).FullName).Count
            );
            await Task.CompletedTask;
            return info;
        }

        /// <summary>
        ///     Creates the specified database data model type.
        /// </summary>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="dbDataModelType">Type of the database data model.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static IDbDataModel Create<TDbMethodManifestMethod>(Type dbDataModelType, IDataRecord data)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            var constructorInfo = GetConstructorInfo<TDbMethodManifestMethod>(dbDataModelType);
            if (constructorInfo.Count > 0)
            {             
                
                try
                {
                    var constructor = constructorInfo.First();
                    var paramInfo = constructor.GetParameters().ToList();
                    var columns = constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                        column.BoundToTDbMethodManifestMethodType && column.TDbMethodManifestMethodType.FullName ==
                        typeof(TDbMethodManifestMethod).FullName);

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
        private static async Task<IDbDataModel> CreateAsync<TDbMethodManifestMethod>(Type dbDataModelType, IDataRecord data)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            var constructorInfo = GetConstructorInfo<TDbMethodManifestMethod>(dbDataModelType);
            if (constructorInfo.Count > 0)
            {
                try
                {
                    var constructor = constructorInfo.First();
                    var paramInfo = constructor.GetParameters().ToList();
                    var columns = constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                        column.BoundToTDbMethodManifestMethodType && column.TDbMethodManifestMethodType.FullName ==
                        typeof(TDbMethodManifestMethod).FullName);

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
        /// <summary>
        ///     Gets the column attribute.
        /// </summary>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        private IDbColumn GetColumnAttribute<TDbMethodManifestMethod>(PropertyInfo property)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            var columnAttrs = property.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                column.BoundToTDbMethodManifestMethodType && column.TDbMethodManifestMethodType.FullName ==
                typeof(TDbMethodManifestMethod).FullName);

            if (columnAttrs.Any()) return columnAttrs.First();

            var commonColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList()
                .FindAll(column => !column.BoundToTDbMethodManifestMethodType);
            if (commonColumnAttrs.Any()) return commonColumnAttrs.First();
            return null;
        }
        private async Task<IDbColumn> GetColumnAttributeAsync<TDbMethodManifestMethod>(PropertyInfo property)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            var columnAttrs = property.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                column.BoundToTDbMethodManifestMethodType && column.TDbMethodManifestMethodType.FullName ==
                typeof(TDbMethodManifestMethod).FullName);

            if (columnAttrs.Any()) {
                await Task.CompletedTask;
                return columnAttrs.First(); 
            }

            var commonColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList()
                .FindAll(column => !column.BoundToTDbMethodManifestMethodType);
            if (commonColumnAttrs.Any())
            {
                await Task.CompletedTask;
                return commonColumnAttrs.First();
            }
            await Task.CompletedTask;
            return null;
        }

        /// <summary>
        ///     Gets the bindable properties.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<PropertyInfo> GetBindableProperties()
        {
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        private async Task<IEnumerable<PropertyInfo>> GetBindablePropertiesAsync()
        {
            IEnumerable<PropertyInfo> properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            await Task.CompletedTask;
            return properties;
        }

        /// <summary>
        ///     Populates the nested properties.
        /// </summary>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        private void PopulateNestedProperties<TDbMethodManifestMethod>(IDataRecord data)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            var nestedProperties = GetBindableProperties().Where(prop =>
                prop.PropertyType.IsSubclassOf(typeof(DbDataModel))
            );
            foreach (var property in nestedProperties)
            {
                if (GetConstructorInfo<TDbMethodManifestMethod>(property.PropertyType).Count > 0)
                {
                    if (Create<TDbMethodManifestMethod>(property.PropertyType, data) is IDbDataModel model)
                    {
                        property.SetValue(this, model, null);
                        HasNestedDataBindingErrors = model.HasDataBindingErrors;
                    }
                }
                else
                {
                    var instance = GenericInstance.GetInstance(property.PropertyType);
                    if (instance is IDbDataModel nestedModel)
                    {
                        nestedModel.Init<TDbMethodManifestMethod>(data);
                        property.SetValue(this, instance, null);
                        HasNestedDataBindingErrors = nestedModel.HasDataBindingErrors;
                    }
                }
            }
        }
        private async Task PopulateNestedPropertiesAsync<TDbMethodManifestMethod>(IDataRecord data)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            var nestedProperties = (await GetBindablePropertiesAsync()).Where(prop =>
                prop.PropertyType.IsSubclassOf(typeof(DbDataModel))
            );
            foreach (var property in nestedProperties)
            {
                if ((await GetConstructorInfoAsync<TDbMethodManifestMethod>(property.PropertyType)).Count > 0)
                {
                    if (await CreateAsync<TDbMethodManifestMethod>(property.PropertyType, data) is IDbDataModel model)
                    {
                        property.SetValue(this, model, null);
                        HasNestedDataBindingErrors = model.HasDataBindingErrors;
                    }
                    
                }
                else
                {
                    var instance = await GenericInstance.GetInstanceAsync(property.PropertyType);
                    if (instance is IDbDataModel nestedModel)
                    {
                        await nestedModel.InitAsync<TDbMethodManifestMethod>(data);
                        property.SetValue(this, instance, null);
                        HasNestedDataBindingErrors = nestedModel.HasDataBindingErrors;
                    }
                }
            }
            await Task.CompletedTask;
        }

        public void Init<TDbMethodManifestMethod>(IDataRecord data)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            var properties = GetBindableProperties().Where(prop =>
                prop.GetCustomAttributes<DbColumn>().Any()
            );
            foreach (var property in properties)
            {
                try
                {
                    var columnAttribute = GetColumnAttribute<TDbMethodManifestMethod>(property);

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
            PopulateNestedProperties<TDbMethodManifestMethod>(data);
        }
        public async Task InitAsync<TDbMethodManifestMethod>(IDataRecord data)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            var properties = (await GetBindablePropertiesAsync()).Where(prop =>
                prop.GetCustomAttributes<DbColumn>().Any()
            );
            
            foreach (var property in properties)
            {
                try
                {
                    var columnAttribute = await GetColumnAttributeAsync<TDbMethodManifestMethod>(property);

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
            await PopulateNestedPropertiesAsync<TDbMethodManifestMethod>(data);
            await Task.CompletedTask;
        }


    }
}