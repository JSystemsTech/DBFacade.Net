using DomainFacade.DataLayer.DbManifest;
using DomainFacade.DataLayer.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DomainFacade.DataLayer.Models
{
    
    public abstract class DbDataModel
    {        
        private DbDataModel() { }
        
        public DbDataModel(IDataRecord data, IDbMethod dbMethod) { PopulateProperties(data, dbMethod); }
        public DbDataModel(object columnValue) {  }
        
        
        protected IDbColumn GetColumnAttribute(PropertyInfo property, IDbMethod dbMethod)
        {
            List<DbColumn> ColumnAttrs = property.GetCustomAttributes<DbColumn>().ToList().FindAll(column => column.BoundToDbMethodType && column.GetDbMethodType().FullName == dbMethod.GetType().FullName);
            
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
        protected void PopulateProperties(IDataRecord data, IDbMethod dbMethod)
        {
            
            Type type = this.GetType();
            
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            List<PropertyInfo> properties = type.GetProperties(flags).ToList().FindAll(prop => prop.GetCustomAttributes<DbColumn>().Count() > 0);

            foreach (PropertyInfo property in properties)
            {
                IDbColumn columnAttribute = GetColumnAttribute(property, dbMethod);
                
                Type propType = property.PropertyType;
                object value = null;
                if(columnAttribute != null)
                {
                    if (property.PropertyType.BaseType == typeof(DbDataModel))
                    {
                        object columnValue = DbColumnConversion.Converters[typeof(object)].DynamicInvoke(data, columnAttribute.GetOrdinal(data));
                        object[] args = new object[1] { columnValue };
                        Type[] types = new Type[1] { typeof(object) };
                        value = property.PropertyType.GetConstructor(types).Invoke(args);
                    }
                    else
                    {
                        value = columnAttribute.GetColumnValueCore(data, propType);
                    }
                }                
                if (value != null)
                {
                    property.SetValue(this, value, null);
                }

            }
            
            List<PropertyInfo> NestedProperties = type.GetProperties(flags).ToList().FindAll(prop => prop.GetCustomAttributes<NestedModel>().Count() > 0);
            foreach (PropertyInfo property in NestedProperties)
            {
                if(property.PropertyType.BaseType == typeof(DbDataModel))
                {
                    object[] args = new object[2] { data, dbMethod };
                    Type[] types = new Type[2] { typeof(IDataRecord), typeof(IDbMethod) };
                    property.SetValue(this, property.PropertyType.GetConstructor(types).Invoke(args), null);
                }
               
            }
        }
    }
}
