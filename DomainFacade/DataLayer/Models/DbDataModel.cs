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
        void InitializeData<DbMethod>(IDataRecord data) where DbMethod : IDbMethod;
    }
    public abstract class DbDataModel: IDbDataModel
    {
        public DbDataModel() { }
        

        public static T ToDbDataModel<T, DbMethod>(IDataRecord data) where T : DbDataModel where DbMethod : IDbMethod
        {
            if(GetConstructorInfo<DbMethod>(typeof(T)).Count > 0)
            {
                return Create<T, DbMethod>(data);
            }
            T model = GenericInstance<T>.GetInstance();
            model.InitializeData<DbMethod>(data);
            return model;
        }
        public void InitializeData<DbMethod>(IDataRecord data) where DbMethod : IDbMethod
        {
            PopulateProperties<DbMethod>(data);
        }
        private static T Create<T, DbMethod>(IDataRecord data) where T : DbDataModel where DbMethod : IDbMethod
        {
            return (T)Create<DbMethod>(typeof(T), data);
        }
        private static List<ConstructorInfo> GetConstructorInfo<DbMethod>(Type dbDataModelType) where DbMethod : IDbMethod
        {
            return dbDataModelType.GetConstructors().ToList().FindAll(constructor =>
                constructor.GetCustomAttributes<DbColumn>().Count() > 0 &&
                constructor.GetParameters().Count() ==
                    constructor.GetCustomAttributes<DbColumn>().ToList().FindAll(column =>
                        column.BoundToDbMethodType && column.GetDbMethodType().FullName == typeof(DbMethod).FullName).Count
            );
        }
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
        protected IDbColumn GetColumnAttribute<DbMethod>(PropertyInfo property) where DbMethod : IDbMethod
        {
            List<DbColumn> ColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList().FindAll(column => column.BoundToDbMethodType && column.GetDbMethodType().FullName ==typeof(DbMethod).FullName);

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
