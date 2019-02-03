using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models.Attributes;
using DomainFacade.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DomainFacade.DataLayer.Models
{
    public interface IDbDataModel
    {
        void InitializeData<E>(IDataRecord data) where E : IDbMethod;
    }
    public abstract class DbDataModel: IDbDataModel
    {
        public DbDataModel() { }
        

        public static T ToDbDataModel<T, E>(IDataRecord data) where T : DbDataModel where E : IDbMethod
        {
            if(GetConstructorInfo<E>(typeof(T)).Count > 0)
            {
                return Create<T,E>(data);
            }
            T model = GenericInstance<T>.GetInstance();
            model.InitializeData<E>(data);
            return model;
        }
        public void InitializeData<E>(IDataRecord data) where E : IDbMethod
        {
            PopulateProperties<E>(data);
        }
        private static T Create<T, E>(IDataRecord data) where T : DbDataModel where E : IDbMethod
        {
            return (T)Create<E>(typeof(T), data);
        }
        private static List<ConstructorInfo> GetConstructorInfo<E>(Type dbDataModelType) where E : IDbMethod
        {
            return dbDataModelType.GetConstructors().ToList().FindAll(constructor =>
                constructor.GetCustomAttributes<DbColumn>().Count() > 0 &&
                constructor.GetParameters().Count() ==
                    constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                        column.BoundToDbMethodType && column.GetDbMethodType().FullName == typeof(E).FullName).Count
            );
        }
        private static IDbDataModel Create<E>(Type dbDataModelType, IDataRecord data) where E : IDbMethod
        {
            List<ConstructorInfo> constructorInfo = GetConstructorInfo<E>(dbDataModelType);
            if (constructorInfo.Count > 0)
            {
                ConstructorInfo constructor = constructorInfo.First();
                List<ParameterInfo> paramInfo = constructor.GetParameters().ToList();
                List<DbColumn> columns = constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                        column.BoundToDbMethodType && column.GetDbMethodType().FullName == typeof(E).FullName);

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
        protected IDbColumn GetColumnAttribute<E>(PropertyInfo property) where E : IDbMethod
        {
            List<DbColumn> ColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList().FindAll(column => column.BoundToDbMethodType && column.GetDbMethodType().FullName ==typeof(E).FullName);

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
        
        private  List<PropertyInfo> GetBindableProperties()
        {
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
        }
        private void PopulateNestedProperties<E>(IDataRecord data) where E : IDbMethod
        {
            List<PropertyInfo> NestedProperties = GetBindableProperties().FindAll(prop =>
                prop.GetCustomAttributes<NestedModel>().Count() > 0 &&
                prop.PropertyType.BaseType == typeof(DbDataModel)
                );
            foreach (PropertyInfo property in NestedProperties)
            {
                if (GetConstructorInfo<E>(property.PropertyType).Count > 0)
                {
                    property.SetValue(this, Create<E>(property.PropertyType, data), null);
                }
                else
                {
                    IDbDataModel instance = (IDbDataModel)GenericInstance.GetInstance(property.PropertyType);
                    instance.InitializeData<E>(data);
                    property.SetValue(this, instance, null);
                }

            }
        }

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
                if(columnAttribute != null)
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
