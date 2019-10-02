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
    public interface IDbDataModel
    {
        string ToJson();
    }
    interface IDbDataModelInternal: IDbDataModel
    {
        void InitializeData<TDbMethodManifestMethod>(IDataRecord data) where TDbMethodManifestMethod : IDbManifestMethod;
    }
    [JsonObject]
    [Serializable]
    public abstract class DbDataModel : IDbDataModelInternal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbDataModel"/> class.
        /// </summary>
        public DbDataModel() { }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
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
        /// <typeparam name="TDbDataModel"></typeparam>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="DataModelConstructionException">Failed to create data model</exception>
        public static TDbDataModel ToDbDataModel<TDbDataModel, TDbMethodManifestMethod>(IDataRecord data) where TDbDataModel : DbDataModel where TDbMethodManifestMethod : IDbManifestMethod
        {
            try
            {
                if (GetConstructorInfo<TDbMethodManifestMethod>(typeof(TDbDataModel)).Count > 0)
                {
                    return Create<TDbDataModel, TDbMethodManifestMethod>(data);
                }
                TDbDataModel model = GenericInstance<TDbDataModel>.GetInstance();
                model.InitializeData<TDbMethodManifestMethod>(data);
                return model;
            }
            catch (Exception e)
            {
                throw new DataModelConstructionException($"Failed to create {typeof(TDbDataModel).Name} data model", e);
            }

        }
        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        public void InitializeData<TDbMethodManifestMethod>(IDataRecord data) where TDbMethodManifestMethod : IDbManifestMethod
        {
            PopulateProperties<TDbMethodManifestMethod>(data);
        }
        /// <summary>
        /// Creates the specified data.
        /// </summary>
        /// <typeparam name="TDbDataModel"></typeparam>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static TDbDataModel Create<TDbDataModel, TDbMethodManifestMethod>(IDataRecord data) where TDbDataModel : DbDataModel where TDbMethodManifestMethod : IDbManifestMethod
        {
            return (TDbDataModel)Create<TDbMethodManifestMethod>(typeof(TDbDataModel), data);
        }
        /// <summary>
        /// Gets the constructor information.
        /// </summary>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="dbDataModelType">Type of the database data model.</param>
        /// <returns></returns>
        private static List<ConstructorInfo> GetConstructorInfo<TDbMethodManifestMethod>(Type dbDataModelType) where TDbMethodManifestMethod : IDbManifestMethod
        {
            return dbDataModelType.GetConstructors().ToList().FindAll(constructor =>
                constructor.GetCustomAttributes<DbColumn>().Count() > 0 &&
                constructor.GetParameters().Count() ==
                    constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                        column.BoundToTDbMethodManifestMethodType && column.GetTDbMethodManifestMethodType().FullName == typeof(TDbMethodManifestMethod).FullName).Count
            );
        }
        /// <summary>
        /// Creates the specified database data model type.
        /// </summary>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="dbDataModelType">Type of the database data model.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private static IDbDataModel Create<TDbMethodManifestMethod>(Type dbDataModelType, IDataRecord data) where TDbMethodManifestMethod : IDbManifestMethod
        {
            List<ConstructorInfo> constructorInfo = GetConstructorInfo<TDbMethodManifestMethod>(dbDataModelType);
            if (constructorInfo.Count > 0)
            {
                ConstructorInfo constructor = constructorInfo.First();
                List<ParameterInfo> paramInfo = constructor.GetParameters().ToList();
                List<DbColumn> columns = constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                        column.BoundToTDbMethodManifestMethodType && column.GetTDbMethodManifestMethodType().FullName == typeof(TDbMethodManifestMethod).FullName);

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
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        private IDbColumn GetColumnAttribute<TDbMethodManifestMethod>(PropertyInfo property) where TDbMethodManifestMethod : IDbManifestMethod
        {
            List<DbColumn> ColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList().FindAll(column => column.BoundToTDbMethodManifestMethodType && column.GetTDbMethodManifestMethodType().FullName == typeof(TDbMethodManifestMethod).FullName);

            if (ColumnAttrs.Count > 0)
            {
                return ColumnAttrs.First();
            }
            else
            {
                List<DbColumn> CommonColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList().FindAll(column => !column.BoundToTDbMethodManifestMethodType);
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
        private IEnumerable<PropertyInfo> GetBindableProperties()
        {
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        /// <summary>
        /// Populates the nested properties.
        /// </summary>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        private void PopulateNestedProperties<TDbMethodManifestMethod>(IDataRecord data) where TDbMethodManifestMethod : IDbManifestMethod
        {
            IEnumerable<PropertyInfo> NestedProperties = GetBindableProperties().Where(prop =>
                prop.GetCustomAttributes<NestedModel>().Count() > 0 &&
                prop.PropertyType.BaseType == typeof(DbDataModel)
                );
            foreach (PropertyInfo property in NestedProperties)
            {
                if (GetConstructorInfo<TDbMethodManifestMethod>(property.PropertyType).Count > 0)
                {
                    property.SetValue(this, Create<TDbMethodManifestMethod>(property.PropertyType, data), null);
                }
                else
                {
                    IDbDataModelInternal instance = (IDbDataModelInternal)GenericInstance.GetInstance(property.PropertyType);
                    instance.InitializeData<TDbMethodManifestMethod>(data);
                    property.SetValue(this, instance, null);
                }

            }
        }
        
        protected void PopulateProperties<TDbMethodManifestMethod>(IDataRecord data) where TDbMethodManifestMethod : IDbManifestMethod
        {
            IEnumerable<PropertyInfo> properties = GetBindableProperties().Where(prop =>
                prop.GetCustomAttributes<DbColumn>().Count() > 0
            );

            foreach (PropertyInfo property in properties)
            {
                IDbColumn columnAttribute = GetColumnAttribute<TDbMethodManifestMethod>(property);

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
            PopulateNestedProperties<TDbMethodManifestMethod>(data);
        }
    }
}
