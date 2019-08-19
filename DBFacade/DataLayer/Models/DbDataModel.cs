using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models.Attributes;
using DBFacade.Exceptions;
using DBFacade.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DBFacade.DataLayer.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbDataModel
    {
        void InitializeData<DbMethod>(IDataRecord data) where DbMethod : IDbMethod;
        string ToJson();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IDbDataModel" />
    [JsonObject]
    [Serializable]
    public abstract class DbDataModel : IDbDataModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbDataModel"/> class.
        /// </summary>
        public DbDataModel() { }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// Parses the json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr">The json string.</param>
        /// <returns></returns>
        public static T ParseJson<T>(string jsonStr) where T : DbDataModel
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            settings.ContractResolver = new ContractResolverWithPrivates();
            return JsonConvert.DeserializeObject<T>(jsonStr, settings);
        }
        
        private class ContractResolverWithPrivates : CamelCasePropertyNamesContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var prop = base.CreateProperty(member, memberSerialization);
                if (!prop.Writable)
                {
                    var property = member as PropertyInfo;
                    if (property != null)
                    {
                        var hasPrivateSetter = property.GetSetMethod(true) != null;
                        prop.Writable = hasPrivateSetter;
                    }
                }
                return prop;
            }
        }

        /// <summary>
        /// Converts to dbdatamodel.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="DataModelConstructionException">Failed to create data model</exception>
        public static T ToDbDataModel<T, DbMethod>(IDataRecord data) where T : DbDataModel where DbMethod : IDbMethod
        {
            try
            {
                if (GetConstructorInfo<DbMethod>(typeof(T)).Count > 0)
                {
                    return Create<T, DbMethod>(data);
                }
                T model = GenericInstance<T>.GetInstance();
                model.InitializeData<DbMethod>(data);
                return model;
            }
            catch (Exception e)
            {
                throw new DataModelConstructionException($"Failed to create {typeof(T).Name} data model", e);
            }

        }
        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        public void InitializeData<DbMethod>(IDataRecord data) where DbMethod : IDbMethod
        {
            PopulateProperties<DbMethod>(data);
        }
        /// <summary>
        /// Creates the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static T Create<T, DbMethod>(IDataRecord data) where T : DbDataModel where DbMethod : IDbMethod
        {
            return (T)Create<DbMethod>(typeof(T), data);
        }
        /// <summary>
        /// Gets the constructor information.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="dbDataModelType">Type of the database data model.</param>
        /// <returns></returns>
        private static List<ConstructorInfo> GetConstructorInfo<DbMethod>(Type dbDataModelType) where DbMethod : IDbMethod
        {
            return dbDataModelType.GetConstructors().ToList().FindAll(constructor =>
                constructor.GetCustomAttributes<DbColumn>().Count() > 0 &&
                constructor.GetParameters().Count() ==
                    constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                        column.BoundToDbMethodType && column.GetDbMethodType().FullName == typeof(DbMethod).FullName).Count
            );
        }
        /// <summary>
        /// Creates the specified database data model type.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="dbDataModelType">Type of the database data model.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static IDbDataModel Create<DbMethod>(Type dbDataModelType, IDataRecord data) where DbMethod : IDbMethod
        {
            List<ConstructorInfo> constructorInfo = GetConstructorInfo<DbMethod>(dbDataModelType);
            if (constructorInfo.Count > 0)
            {
                ConstructorInfo constructor = constructorInfo.First();
                List<ParameterInfo> paramInfo = constructor.GetParameters().ToList();
                List<DbColumn> columns = constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                        column.BoundToDbMethodType && column.GetDbMethodType().FullName == typeof(DbMethod).FullName);

                List<object> args = new List<object>();

                foreach (ParameterInfo param in paramInfo)
                {
                    int index = paramInfo.IndexOf(param);
                    object value = columns[index].GetColumnValueCore(data, param.ParameterType);
                    args.Add(value);
                }
                return (IDbDataModel)GenericInstance.GetInstanceWithArgArray(dbDataModelType, args.ToArray());
            }
            return null;
        }
        /// <summary>
        /// Gets the column attribute.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        protected IDbColumn GetColumnAttribute<DbMethod>(PropertyInfo property) where DbMethod : IDbMethod
        {
            List<DbColumn> ColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList().FindAll(column => column.BoundToDbMethodType && column.GetDbMethodType().FullName == typeof(DbMethod).FullName);

            if (ColumnAttrs.Count > 0)
            {
                return ColumnAttrs.First();
            }
            else
            {
                List<DbColumn> CommonColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList().FindAll(column => !column.BoundToDbMethodType);
                if (CommonColumnAttrs.Count > 0)
                {
                    return CommonColumnAttrs.First();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the bindable properties.
        /// </summary>
        /// <returns></returns>
        private List<PropertyInfo> GetBindableProperties()
        {
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
        }
        /// <summary>
        /// Populates the nested properties.
        /// </summary>
        /// <typeparam name="DbMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        private void PopulateNestedProperties<DbMethod>(IDataRecord data) where DbMethod : IDbMethod
        {
            List<PropertyInfo> NestedProperties = GetBindableProperties().FindAll(prop =>
                prop.GetCustomAttributes<NestedModel>().Count() > 0 &&
                prop.PropertyType.BaseType == typeof(DbDataModel)
                );
            foreach (PropertyInfo property in NestedProperties)
            {
                if (GetConstructorInfo<DbMethod>(property.PropertyType).Count > 0)
                {
                    property.SetValue(this, Create<DbMethod>(property.PropertyType, data), null);
                }
                else
                {
                    IDbDataModel instance = (IDbDataModel)GenericInstance.GetInstance(property.PropertyType);
                    instance.InitializeData<DbMethod>(data);
                    property.SetValue(this, instance, null);
                }

            }
        }

        /// <summary>
        /// Populates the properties.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="data">The data.</param>
        protected void PopulateProperties<E>(IDataRecord data) where E : IDbMethod
        {
            List<PropertyInfo> properties = GetBindableProperties().FindAll(prop =>
                prop.GetCustomAttributes<DbColumn>().Count() > 0
            );

            foreach (PropertyInfo property in properties)
            {
                IDbColumn columnAttribute = GetColumnAttribute<E>(property);

                Type propType = property.PropertyType;
                object value = null;
                if (columnAttribute != null)
                {
                    value = columnAttribute.GetColumnValueCore(data, propType);
                }
                if (value != null)
                {
                    property.SetValue(this, value, null);
                }

            }
            PopulateNestedProperties<E>(data);
        }
    }
}
