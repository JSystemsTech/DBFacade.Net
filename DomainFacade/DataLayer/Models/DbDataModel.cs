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
        
        public DbDataModel(IDataRecord data) { PopulateProperties(data); }
        public DbDataModel(object columnValue) {  }
        protected void PopulateProperties(IDataRecord data)
        {
            
            Type type = this.GetType();
            
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            List<PropertyInfo> properties = type.GetProperties(flags).ToList().FindAll(prop => prop.GetCustomAttributes<DbColumn>().Count() > 0);

            foreach (PropertyInfo property in properties)
            {
                DbColumn columnAttribute = property.GetCustomAttributes<DbColumn>().ToList().First();
                object value = null;
                
                Type propType = property.PropertyType;
                if (columnAttribute.HasColumn(data) && !data.IsDBNull(columnAttribute.GetOrdinal(data)))
                { 
                    
                    if (DbColumnConversion.HasConverter(propType))
                    {
                        if (DbColumnConversion.isDelimitedArray(propType))
                        {
                            value = DbColumnConversion.Converters[propType].DynamicInvoke(data, columnAttribute.GetOrdinal(data), columnAttribute.Delimeter);
                        }
                        else if (propType == typeof(string) && columnAttribute.DateFormat != null)
                        {
                            DateTime dateTime = (DateTime)DbColumnConversion.Converters[typeof(DateTime)].DynamicInvoke(data, columnAttribute.GetOrdinal(data));
                            value = dateTime.ToString(columnAttribute.DateFormat);
                        }
                        else
                        {
                            value = DbColumnConversion.Converters[propType].DynamicInvoke(data, columnAttribute.GetOrdinal(data));
                        }
                    }
                    else if(property.PropertyType.BaseType == typeof(DbDataModel))
                    {
                        object columnValue = DbColumnConversion.Converters[typeof(object)].DynamicInvoke(data, columnAttribute.GetOrdinal(data));
                        object[] args = new object[1] { columnValue };
                        Type[] types = new Type[1] { typeof(object) };
                        value = property.PropertyType.GetConstructor(types).Invoke(args);
                    }                    
                }
                if (value == null && columnAttribute.DefaultValue != null)
                {
                    value = columnAttribute.DefaultValue;
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
                    object[] args = new object[1] { data };
                    Type[] types = new Type[1] { typeof(IDataRecord) };
                    property.SetValue(this, property.PropertyType.GetConstructor(types).Invoke(args), null);
                }
               
            }
        }
    }
}
