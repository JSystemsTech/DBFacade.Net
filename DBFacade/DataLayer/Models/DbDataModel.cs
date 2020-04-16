using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using DBFacade.DataLayer.Manifest;
using DBFacade.DataLayer.Models.Attributes;
using DBFacade.Exceptions;
using DBFacade.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DBFacade.DataLayer.Models
{
    public interface IDbDataModel
    {
        string ToJson();
        IEnumerable<DataModelConstructionException> DataBindingErrors { get; }
        bool HasDataBindingErrors { get;  }
    }

    [JsonObject]
    [Serializable]
    public abstract class DbDataModel: IDbDataModel
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public IEnumerable<DataModelConstructionException> DataBindingErrors { get; private set; }

        

        public bool HasDataBindingErrors { get; private set; }

        /// <summary>
        ///     Initializes the data.
        /// </summary>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        internal void InitializeData<TDbMethodManifestMethod>(IDataRecord data)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            PopulateProperties<TDbMethodManifestMethod>(data);
        }

        /// <summary>
        ///     Converts to DbDataModel.
        /// </summary>
        /// <typeparam name="TDbDataModel"></typeparam>
        /// <typeparam name="TDbMethodManifestMethod">The type of the b method.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        /// <exception cref="DataModelConstructionException">Failed to create data model</exception>
        public static TDbDataModel ToDbDataModel<TDbDataModel, TDbMethodManifestMethod>(IDataRecord data)
            where TDbDataModel : DbDataModel where TDbMethodManifestMethod : IDbManifestMethod
        {
            try
            {
                if (GetConstructorInfo<TDbMethodManifestMethod>(typeof(TDbDataModel)).Count > 0)
                    return Create<TDbDataModel, TDbMethodManifestMethod>(data);
                var model = GenericInstance<TDbDataModel>.GetInstance();
                model.InitializeData<TDbMethodManifestMethod>(data);
                return model;
            }
            catch (Exception e)
            {
                throw new DataModelConstructionException($"Failed to create {typeof(TDbDataModel).Name} data model", e);
            }
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
                    column.BoundToTDbMethodManifestMethodType && column.GetTDbMethodManifestMethodType().FullName ==
                    typeof(TDbMethodManifestMethod).FullName).Count
            );
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
                var constructor = constructorInfo.First();
                var paramInfo = constructor.GetParameters().ToList();
                var columns = constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                    column.BoundToTDbMethodManifestMethodType && column.GetTDbMethodManifestMethodType().FullName ==
                    typeof(TDbMethodManifestMethod).FullName);

                var args = new List<object>();

                foreach (var param in paramInfo)
                {
                    var index = paramInfo.IndexOf(param);
                    var value = columns[index].GetColumnValueCore(data, param.ParameterType);
                    args.Add(value);
                }

                return GenericInstance.GetInstanceWithArgArray(dbDataModelType, args.ToArray()) is IDbDataModel model
                    ? model
                    : null;
            }
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
                column.BoundToTDbMethodManifestMethodType && column.GetTDbMethodManifestMethodType().FullName ==
                typeof(TDbMethodManifestMethod).FullName);

            if (columnAttrs.Any()) return columnAttrs.First();

            var commonColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList()
                .FindAll(column => !column.BoundToTDbMethodManifestMethodType);
            if (commonColumnAttrs.Any()) return commonColumnAttrs.First();
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
                        HasDataBindingErrors = HasDataBindingErrors || model.HasDataBindingErrors || model.DataBindingErrors.Any();
                    }
                }
                else
                {
                    var instance = GenericInstance.GetInstance(property.PropertyType);
                    MethodInfo method = property.PropertyType.GetMethod(
                        "InitializeData",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method != null)
                    {
                        MethodInfo methodToInvoke = method.MakeGenericMethod(typeof(TDbMethodManifestMethod));
                        methodToInvoke.Invoke(instance, new object[] { data });
                    }
                    property.SetValue(this, instance, null);
                    if (instance is IDbDataModel nestedModel)
                    {
                        HasDataBindingErrors = HasDataBindingErrors || nestedModel.HasDataBindingErrors || nestedModel.DataBindingErrors.Any();
                    }
                }
            }
        }

        protected void PopulateProperties<TDbMethodManifestMethod>(IDataRecord data)
            where TDbMethodManifestMethod : IDbManifestMethod
        {
            var properties = GetBindableProperties().Where(prop =>
                prop.GetCustomAttributes<DbColumn>().Any()
            );
            DataBindingErrors = new DataModelConstructionException[0];
            List<DataModelConstructionException> dataBindingErrors = new List<DataModelConstructionException>();

            foreach (var property in properties)
            {
                try
                {
                    var columnAttribute = GetColumnAttribute<TDbMethodManifestMethod>(property);

                    var propType = property.PropertyType;
                    object value = null;
                    if (columnAttribute != null) value = columnAttribute.GetColumnValueCore(data, propType);
                    if (value != null) property.SetValue(this, value, null);
                }
                catch (DataModelConstructionException e)
                {
                    dataBindingErrors.Add(e);
                }
            }
            if (dataBindingErrors.Any())
            {
                DataBindingErrors = dataBindingErrors;
                HasDataBindingErrors = true;
            }
            PopulateNestedProperties<TDbMethodManifestMethod>(data);
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
    }
}